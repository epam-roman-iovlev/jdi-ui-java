using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Epam.JDI.Core.Interfaces.Base;
using static Epam.JDI.Commons.ReflectionUtils;

namespace Epam.JDI.Core.Base
{
    public abstract class CascadeInit
    {
        protected abstract void SetElement(object parent, Type parentType, FieldInfo field, string driverName);


        protected Type[] Decorators = {typeof (IBaseElement), typeof (List<object>)};

        public void InitElements(object parent, string driverName)
        {
            SetFieldsForInit(GetFields(parent.GetType()), parent.GetType(), driverName);
        }

        private void SetFieldsForInit(List<FieldInfo> fields, Type parentType, string driverName)
        {
            fields.Where(field => Decorators.ToList().Any(type => field.GetType() == type)).ToList()
                .ForEach(field => SetElement(null, parentType, field, driverName));
        }


        public void InitStaticPages(Type parentType, string driverName)
        {
            SetFieldsForInit(StaticFields(parentType), parentType, driverName);
        }

        public T InitPage<T>(Type site, string driverName) where T : Application
        {
            var instance = (T) Activator.CreateInstance(site);
            instance.DriverName = driverName;
            InitElements(instance, driverName);
            return instance;
        }

        protected bool IsBaseElement(object obj)
        {
            return obj.IsInterfaceOf(typeof (IBaseElement));
        }
    }
}
