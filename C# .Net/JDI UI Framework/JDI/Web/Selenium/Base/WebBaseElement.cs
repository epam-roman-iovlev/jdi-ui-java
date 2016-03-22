using System;
using System.Collections.Generic;
using System.Reflection;
using Epam.JDI.Commons;
using Epam.JDI.Core.Attributes;
using Epam.JDI.Core.Attributes.Functions;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Logging;
using Epam.JDI.Web.Selenium.Attributes;
using Epam.JDI.Web.Selenium.Elements.APIInteract;
using Epam.JDI.Web.Selenium.Elements.WebActions;
using OpenQA.Selenium;
using static System.String;
using static System.TimeSpan;
using static Epam.JDI.Core.Logging.LogLevels;
using static Epam.JDI.Core.Settings.JDISettings;
using static Epam.JDI.Web.Selenium.DriverFactory.WebDriverByUtils;

namespace Epam.JDI.Core
{
    public class WebBaseElement : IBaseElement
    {
        public By Locator => WebAvatar.ByLocator;
        public object Parent { get; set; }

        public static ActionScenrios ActionScenrios
        {
            set { ActionInvoker.ActionScenrios = value; }
        }

        public static Action<string, Action<string>> DoActionRule = (text, action) => {
            if (text == null) return;
            action.Invoke(text);
        };
        public static Action<string, Action<string>> SetValueEmptyAction = (text, action) => {
            if (text == null || text.Equals("")) return;
            action.Invoke(text.Equals("#CLEAR#") ? "" : text);
        };
        public Functions Function = Functions.None;
        public void SetFunction(Functions function) { Function = function; }
        public IAvatar Avatar { get; set; }
        public GetElementModule WebAvatar {
            get { return (GetElementModule) Avatar; }
            set { Avatar = value;  }
        }
        public ActionInvoker Invoker;
        public string Name { get; set; }
        public string ParentTypeName => Parent?.GetType().Name ?? "";
        protected GetElement GetElement;
        protected ElementsActions Actions;
        private string _varName;
        private string VarName => _varName ?? Name;
        private string _typeName;
        public string TypeName {
            get { return _typeName ?? GetType().Name; }
            set { _typeName = value;  }
        }
        protected Timer Timer => WebAvatar.Timer;

        public WebBaseElement() : this(By.Id("EMPTY"))
        {
            Invoker = new ActionInvoker(this);
            GetElement = new GetElement(this);
            Actions = new ElementsActions(this);
        }
        
        public WebBaseElement(By byLocator)
        {
            Avatar = new GetElementModule(this)
            {
                ByLocator = !byLocator.GetByLocator().Equals("EMPTY") 
                    ? byLocator 
                    : null
            };
        }

        public IWebDriver WebDriver => WebAvatar.WebDriver;

        public IWebElement WebElement
        {
            get { return WebAvatar.WebElement; }
            set { WebAvatar.WebElement = value; }
        }

        protected List<IWebElement> WebElements
        {
            get { return WebAvatar.WebElements; }
            set { WebAvatar.WebElements = value; }
        }
        public bool HasLocator() => WebAvatar.HasLocator;

        public void SetName(FieldInfo field)
        {
            Name = NameAttribute.GetElementName(field);
            _varName = field.Name;

        }
        public WebBaseElement SetAvatar(GetElementModule avatar, By byLocator = null)
        {
            Avatar = byLocator != null
                ? new GetElementModule(this, avatar.Context)
                {
                    ByLocator = byLocator,
                    LocalElementSearchCriteria = avatar.LocalElementSearchCriteria,
                    DriverName = avatar.DriverName
                }
                : avatar;
            return this;
        }
        public void SetWaitTimeout(long mSeconds)
        {
            Logger.Debug("Set wait timeout to " + mSeconds);
            WebDriver.Manage().Timeouts().ImplicitlyWait(FromMilliseconds(mSeconds));
            Timeouts.CurrentTimeoutSec = (int)(mSeconds / 1000);
        }
        public void RestoreWaitTimeout()
        {
            SetWaitTimeout(Timeouts.WaitElementSec);
        }

        public void DoAction(string actionName, Action action, LogLevels logLevels = Info)
        {

        }

        public void DoActionResult<TResult>(string actionName, Func<TResult> action,
            Func<TResult, string> logResult = null, LogLevels logLevels = Info)
        {
        }
        protected IJavaScriptExecutor JSExecutor()
        {
            return (IJavaScriptExecutor) WebDriver;
        }

        public void LogAction(string actionName, LogLevels level)
        {
            ToLog(Format(ShortLogMessagesFormat
                    ? "{0} for {1}"
                    : "Perform action '{0}' with WebElement ({1})", actionName, ToString()), level);
        }
        public void LogAction(string actionName)
        {
            LogAction(actionName, Info);
        }
        public new string ToString()
        {
            return Format(ShortLogMessagesFormat
                            ? "{1} '{0}' ({2}.{3}; {4})"
                            : "Name: '{0}', Type: '{1}' In: '{2}', {4}",
                    Name, TypeName, ParentTypeName, VarName, Avatar);
        }
    }
}

