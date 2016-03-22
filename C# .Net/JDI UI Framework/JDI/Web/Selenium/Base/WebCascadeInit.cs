using System;
using System.Collections;
using System.Reflection;
using Epam.JDI.Commons;
using Epam.JDI.Commons.Pairs;
using Epam.JDI.Core;
using Epam.JDI.Core.Base;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Interfaces.Complex;
using Epam.JDI.Web.Attributes;
using Epam.JDI.Web.Selenium.Elements.APIInteract;
using Epam.JDI.Web.Selenium.Elements.Complex;
using Epam.JDI.Web.Selenium.Elements.Composite;
using Epam.JDI.Web.Settings;
using OpenQA.Selenium;
using RestSharp.Extensions;
using static System.String;
using static Epam.JDI.Commons.ExceptionUtils;
using static Epam.JDI.Core.Attributes.AttributesUtil;
using static Epam.JDI.Core.Settings.JDIData;
using static Epam.JDI.Core.Settings.JDISettings;

namespace Epam.JDI.Web.Selenium.Base
{
    public class WebCascadeInit : CascadeInit
    {
        protected override void SetElement(object parent, Type parentType, FieldInfo field, string driverName)
        {
            ActionWithException(() =>
            {
                var type = field.FieldType;
                var instance = type.IsInterfaceOf(typeof (IPage))
                    ? GetInstancePage(parent, field, type, parentType)
                    : GetInstanceElement(parent, type, parentType, field, driverName);
                instance.SetName(field);
                if (parent != null)
                    instance.Avatar.DriverName = driverName;
                if (IsNullOrEmpty(instance.Name))
                    instance.TypeName = type.Name;
                instance.Parent = parent;
                field.SetValue(parent, instance);
                if (field.IsInterfaceOf(typeof (IComposite)))
                    InitElements(instance, driverName);
            }, ex => $"Error in setElement for field '{field.Name}' with parent '{parentType?.Name ?? "NULL Class" + ex.FromNewLine()}'");
        }

        private static IBaseElement GetInstancePage(object parent, FieldInfo field, Type type, Type parentType)
        {
            var instance = (IBaseElement) field.GetValue(parent) ?? (IBaseElement) Activator.CreateInstance(type);
            var pageAttribute = field.GetAttribute<PageAttribute>();
            pageAttribute?.FillPage((WebPage) instance, parentType);
            return instance;
        }

        private IBaseElement GetInstanceElement(object parent, Type type, Type parentType, FieldInfo field,
            string driverName)
        {
            var instance = CreateChildFromFieldStatic(parent, parentType, field, type, driverName);
            instance.SetFunction(GetFunction(field));
            return instance;
        }

        private IBaseElement CreateChildFromFieldStatic(object parent, Type parentClass, FieldInfo field, Type type,
            string driverName)
        {
            var instance = (WebBaseElement) field.GetValue(parent);
            if (instance == null)
                instance = ActionWithException(
                    () => GetElementInstance(field, driverName), 
                    ex => $"Can't create child for parent '{parentClass.Name}' with type '{field.FieldType.Name}'. Exception: {ex}");
            else if (instance.Locator == null)
                instance.WebAvatar.ByLocator = GetNewLocator(field);
            instance.WebAvatar.Context = parent is IBaseElement
                ? ((WebBaseElement) parent).WebAvatar.Context.Copy()
                : new Pairs<ContextType, By>();
            if (parent == null || type != null)
            {
                var frameBy = FrameAttribute.GetFrame(field);
                if (frameBy != null)
                    instance.WebAvatar.Context.Add(ContextType.Frame, frameBy);
            }
            if (parent is IBaseElement)
            {
                var parentLocator = ((WebBaseElement) parent).Locator;
                if (parentLocator != null)
                    instance.WebAvatar.Context.Add(ContextType.Locator, parentLocator);
            }
            return instance;
        }

        private WebBaseElement GetElementInstance(FieldInfo field, string driverName)
        {
            var type = field.GetType();
            var fieldName = field.Name;
            var newLocator = GetNewLocator(field);
            try
            {
                WebBaseElement instance = null;
                if (type == typeof(IList))
                {
                    var elementClass = type.GetGenericArguments()[0];
                    /*if (elementClass.IsInterfaceOf)
                        elementClass = MapInterfaceToElement.ClassFromInterface(type);*/
                    if (elementClass != null)
                        instance = (WebBaseElement) Activator.CreateInstance(typeof(Elements<>).MakeGenericType(elementClass));
                }
            else
                {
                    if (type.IsInterface)
                        type = MapInterfaceToElement.ClassFromInterface(type);
                    if (type != null)
                    {
                        instance = (WebBaseElement) Activator.CreateInstance(type);
                        instance.WebAvatar.ByLocator = newLocator;
                    }
                }
                if (instance == null)
                    throw Exception("Unknown interface: " + type +
                                    ". Add relation interface -> class in VIElement.InterfaceTypeMap");
                instance.Avatar.DriverName = driverName;
                return instance;
            }
            catch (Exception ex)
            {
                throw Exception("Error in getElementInstance for field '%s' with type '%s'", fieldName,
                    type.Name + ex.Message.FromNewLine());
            }
        }

        private static By GetNewLocator(FieldInfo field)
        {
            return ActionWithException(() => 
            {
                var locatorGroup = AppVersion;
                if (locatorGroup != null)
                {
                    string groupName;
                    By byLocator;
                    JFindByAttribute.Get(field, out byLocator, out groupName);
                    if (groupName != null && locatorGroup.Equals(groupName) && byLocator != null)
                        return byLocator;
                }
                return FindByAttribute.FindsByLocator(field) ?? FindByAttribute.Locator(field);
            }, ex => $"Error in get locator for type '{field.Name + ex.FromNewLine()}'");
        }

    }
}
