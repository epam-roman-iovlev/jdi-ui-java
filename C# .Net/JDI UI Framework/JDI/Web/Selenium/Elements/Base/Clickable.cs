using System;
using Epam.JDI.Core.Interfaces.Base;
using OpenQA.Selenium;

namespace Epam.JDI.Web.Selenium.Elements.Base
{
    public class Clickable : WebElement, IClickable
    {
        public By Locator { get; }

        
        public Clickable()
        {
        }

        public Clickable(By byLocator) 
        {
            this.Locator = byLocator;
        }
       
        protected void ClickAction()
        {
            throw new NotImplementedException();
        }
        public void Click()
        {
            throw new NotImplementedException();
        }
        protected void ClickJsAction()
        {
            throw new NotImplementedException();
        }
        public void ClickByXY(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
