﻿using System;
using System.Linq;
using Epam.JDI.Commons;
using Epam.JDI.Core;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using static Epam.JDI.Commons.ExceptionUtils;
using static Epam.JDI.Core.Logging.LogLevels;
using static Epam.JDI.Core.Settings.JDISettings;
using static Epam.JDI.Web.Settings.WebSettings;

namespace Epam.JDI.Web.Selenium.Elements.Base
{
    public class WebElement : WebBaseElement, IElement
    {
        private IWebElement _webElement;

        public WebElement(By byLocator = null, IWebElement webElement = null) : base(byLocator)
        {
            _webElement = webElement;
        }
        public static T Copy<T>(T element, By newLocator) where T : WebElement
        {
            return ActionWithException(() => 
            {
                var result = (T)Activator.CreateInstance(element.GetType());
                result.SetAvatar(element.WebAvatar, newLocator);
                return result;
            }, ex => $"Can't copy Element: {element}. Exception: {ex}");
        }

        /**
         * Specified Selenium Element for this Element
         */

        public IWebElement GetWebElement()
        {
            return Invoker.DoJActionResult("Get web element",
                () => WebElement ?? WebAvatar.WebElement, level: Debug);
        }

        public string GetAttribute(string name)
        {
            return GetWebElement().GetAttribute(name);
        }

        public void WaitAttribute(string name, string value)
        {
            Wait(el => el.GetAttribute(name).Equals(value));
        }

        public void SetAttribute(string attributeName, string value)
        {
            Invoker.DoJAction($"Set Attribute '{attributeName}'='{value}'",
                    () => JSExecutor().ExecuteScript($"arguments[0].setAttribute('{attributeName}',arguments[1]);",
                            WebElement, value));
        }

        protected bool IsDisplayedAction()
        {
            return Actions.FindImmediately(() => WebElement.Displayed, false);
        }

        protected void WaitDisplayedAction()
        {
            Wait(el => el.Displayed);
        }

        public bool IsDisplayed()
        {
            return Actions.IsDisplayed(IsDisplayedAction);
        }

        public bool IsHidden()
        {
            return Actions.IsDisplayed(() => !IsDisplayedAction());
        }

        public void WaitDisplayed()
        {
            Actions.WaitDisplayed(() => WebElement.Displayed);
        }

        public void WaitVanished()
        {
            Actions.WaitVanished(() => Timer.Wait(() => !IsDisplayedAction()));
        }

        public IWebElement GetInvisibleElement()
        {
            WebAvatar.SearchAll();
            return WebElement;
        }

        /**
         * @param resultFunc Specify expected function result
         * Waits while condition with WebElement happens during specified timeout and returns result using resultFunc
         */
        public void Wait(Func<IWebElement, bool> resultFunc)
        {
            var result = Wait(resultFunc, r => r);
            Asserter.IsTrue(result);
        }

        /**
         * @param resultFunc Specify expected function result
         * @param condition  Specify expected function condition
         * @return Waits while condition with WebElement happens and returns result using resultFunc
         */
        public T Wait<T>(Func<IWebElement, T> resultFunc, Func<T, bool> condition)
        {
            return Timer.GetResultByCondition(() => resultFunc.Invoke(GetWebElement()), condition.Invoke);
        }

        /**
         * @param resultFunc Specify expected function result
         * @param timeoutSec Specify timeout
         * Waits while condition with WebElement happens during specified timeout and returns wait result
         */
        public void Wait(Func<IWebElement, bool> resultFunc, int timeoutSec)
        {
            var result = Wait(resultFunc, r => r, timeoutSec);
            Asserter.IsTrue(result);
        }

        /**
         * @param resultFunc Specify expected function result
         * @param timeoutSec Specify timeout
         * @param condition  Specify expected function condition
         * @return Waits while condition with WebElement and returns wait result
         */
        public T Wait<T>(Func<IWebElement, T> resultFunc, Func<T, bool> condition, int timeoutSec)
        {
            SetWaitTimeout(timeoutSec);
            var result = new Timer(timeoutSec).GetResultByCondition(() => resultFunc.Invoke(GetWebElement()), condition.Invoke);
            RestoreWaitTimeout();
            return result;
        }

        public void Highlight()
        {
            WebDriverFactory.Highlight(this);
        }

        public void Highlight(HighlightSettings highlightSettings)
        {
            WebDriverFactory.Highlight(this, highlightSettings);
        }

        public void ClickWithKeys(params string[] keys)
        {
            Invoker.DoJAction("Ctrl click on Element",
                    () => {
                        var action = new Actions(WebDriver);
                        action = keys.Aggregate(action, (current, key) => current.KeyDown(key));
                        action = action.MoveToElement(WebElement).Click();
                        action = keys.Aggregate(action, (current, key) => current.KeyUp(key));
                        action.Perform();
            });
        }

        public void DoubleClick()
        {
            Invoker.DoJAction("Double click on Element", () => {
                var builder = new Actions(WebDriver);
                builder.DoubleClick(WebElement).Perform();
            });
        }

        public void RightClick()
        {
            Invoker.DoJAction("Right click on Element", () => {
                var builder = new Actions(WebDriver);
                builder.ContextClick(WebElement).Perform();
            });
        }

        public void ClickCenter()
        {
            Invoker.DoJAction("Click in Center of Element", () => {
                var builder = new Actions(WebDriver);
                builder.Click(WebElement).Perform();
            });
        }

        public void MouseOver()
        {
            Invoker.DoJAction("Move mouse over Element", () => {
                var builder = new Actions(WebDriver);
                builder.MoveToElement(WebElement).Build().Perform();
            });
        }

        public void Focus()
        {
            Invoker.DoJAction("Focus on Element", () => {
               var size = WebElement.Size;
                new Actions(WebDriver).MoveToElement(WebElement, size.Width / 2, size.Height / 2).Build().Perform();
            });
        }

        public void SelectArea(int x1, int y1, int x2, int y2)
        {
            Invoker.DoJAction($"Select area: from ({x1},{y1}) to ({x2},{y2})", () => {
                var element = WebElement;
                new Actions(WebDriver).MoveToElement(element, x1, y1).ClickAndHold()
                        .MoveToElement(WebElement, x2, y2).Release().Build().Perform();
            });
        }

        public void DragAndDrop(int x, int y)
        {
            Invoker.DoJAction($"Drag and drop Element: (x,y)=({x},{y})", () =>
            new Actions(WebDriver).DragAndDropToOffset(WebElement, x, y).Build().Perform());
        }
    }
    
}