using System;
using Epam.JDI.Core.Interfaces.Common;
using OpenQA.Selenium;

namespace Epam.JDI.Web.Selenium.Elements.Base
{
    public class ClickableText : Clickable, IText
    {
        public ClickableText()
        {
        }
        public ClickableText(By byLocator) : base(byLocator)
        {
        }

        
        protected string GetTextAction()
        {
            throw new NotImplementedException();
        }

        public string GetValue()
        {

            throw new NotImplementedException();
        }

        public string GetText()
        {
            throw new NotImplementedException();
        }

        public string WaitText(string text)
        {
            throw new NotImplementedException();
        }

        public  string WaitMatchText(string regEx)
        {
            throw new NotImplementedException();
           
        }
    }
}
