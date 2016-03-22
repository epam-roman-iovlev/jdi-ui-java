using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Epam.JDI.Core;
using Epam.JDI.Core.Attributes.Functions;
using Epam.JDI.Core.Interfaces.Common;
using Epam.JDI.Web.Selenium.Elements.Common;
using static Epam.JDI.Commons.ReflectionUtils;
using static Epam.JDI.Core.Settings.JDISettings;

namespace Epam.JDI.Web.Selenium.Attributes
{
    public class GetElement
    {
        private readonly WebBaseElement _element;

        public GetElement(WebBaseElement element)
        {
            this._element = element;
        }

        public static bool NamesEqual(string name1, string name2)
        {
            return name1.ToLower().Replace(" ", "").Equals(name2.ToLower().Replace(" ", ""));
        }

        public Button GetButton(string buttonName)
        {
            List<FieldInfo> fields = _element.GetFields(typeof(IButton));
            if (fields.Count == 1)
                return (Button) fields[0].GetValue(_element);
            var buttons = fields.Select(f => (Button) f.GetValue(_element));
            var button = buttons.First(b => NamesEqual(ToButton(b.Name), ToButton(buttonName)));
            if (button == null)
                throw Exception($"Can't find button '{buttonName}' for Element '{ToString()}'");
            return button;
        }

        private string ToButton(string buttonName)
        {
            return buttonName.ToLower().Contains("button") ? buttonName : buttonName + "button";
        }

        public Button GetButton(Functions funcName)
        {
            var fields = _element.GetFields(typeof(IButton));
            if (fields.Count == 1)
                return (Button) fields[0].GetValue(_element);
            var buttons = fields.Select(f => (Button) f.GetValue(_element));
            var button = buttons.First(b => b.Function.Equals(funcName));
            if (button != null) return button;
            var name = funcName.ToString();
            var buttonName = name.ToLower().Contains("button") ? name : name + "button";
            button = buttons.First(b => NamesEqual(b.Name, buttonName));
            if (button == null)
                throw Exception($"Can't find button '{name}' for Element '{ToString()}'");
            return button;
        }

        public Text GetTextElement()
        {
            var textField = this.GetFirstField(typeof(Text), typeof(IText));
            if (textField == null)
                throw Exception($"Can't find Text Element '{ToString()}'");
            return (Text) textField.GetValue(_element);
        }
    }
}
