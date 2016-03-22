using System;
using Epam.JDI.Web.Selenium.Elements.Base;
using Epam.JDI.Core.Interfaces.Common;
using OpenQA.Selenium;

namespace Epam.JDI.Web.Selenium.Elements.Common
{
    public class Text : WebElement, IText
    {
        public Text()
        {

        }

        public Text(By locator) : base(locator)
        {

        }

        public string GetValue()
        {
            return GetTextAction();
        }

        protected string GetTextAction()
        {
            throw new NotImplementedException();
        }

        public string GetText()
        {
            throw new System.NotImplementedException();
        }

        public string WaitText(string text)
        {
            throw new System.NotImplementedException();
        }

        public string WaitMatchText(string regEx)
        {
            throw new System.NotImplementedException();
        }
    }
}