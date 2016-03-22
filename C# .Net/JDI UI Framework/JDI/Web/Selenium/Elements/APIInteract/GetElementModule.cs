using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Epam.JDI.Commons;
using Epam.JDI.Commons.Pairs;
using Epam.JDI.Core;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Settings;
using Epam.JDI.Web.Selenium.DriverFactory;
using Epam.JDI.Web.Settings;
using OpenQA.Selenium;
using static Epam.JDI.Core.Settings.JDISettings;

namespace Epam.JDI.Web.Selenium.Elements.APIInteract
{
    public class GetElementModule : IAvatar
    {
        public By ByLocator;
        public Pairs<ContextType, By> Context;
        private readonly WebBaseElement _element;
        public WebBaseElement RootElement;
        public string DriverName { get; set; }
        public IWebDriver WebDriver 
            => WebSettings.WebDriverFactory.GetDriver(DriverName);
        public Func<IWebElement, bool> LocalElementSearchCriteria;

        public GetElementModule(WebBaseElement element, Pairs<ContextType, By> context = null)
        {
            _element = element;
            DriverName = JDISettings.DriverFactory.CurrentDriverName;
            Context = context ?? new Pairs<ContextType, By>();
        }

        public Timer Timer => new Timer(Timeouts.CurrentTimeoutSec*1000);
        public bool HasLocator => ByLocator != null;
        private IWebElement _webElement;
        private List<IWebElement> _webElements;
        public IWebElement WebElement
        {
            get
            {
                Logger.Debug($"Get Web Element: {_element}");
                var element = Timer.GetResultByCondition(GetWebElemetAction, el => el != null);
                Logger.Debug("OneElement found");
                return element;
            }
            set { _webElement = value; }
        }

        public List<IWebElement> WebElements
        {
            get
            {
                Logger.Debug($"Get Web Elements: {_element}");
                var elements = GetWebElemetsAction();
                Logger.Debug($"Found {elements.Count} elements");
                return elements;
            }
            set { _webElements = value; }
        }
        
        private IWebElement GetWebElemetAction()
        {
            if (_webElement != null)
                return _webElement;
            var timeout = Timeouts.CurrentTimeoutSec;
            var result = GetWebElemetsAction();
            switch (result.Count)
            {
                case 0:
                    throw Exception($"Can't find Element '{_element}' during {timeout} seconds");
                case 1:
                    return result[0];
                default:
                    throw Exception(
                        $"Find {result.Count} elements instead of one for Element '{_element}' during {timeout} seconds");
            }

        }
        private List<IWebElement> GetWebElemetsAction()
        {
            if (_webElements != null)
                return _webElements;
            var result = Timer.GetResultByCondition(
                    SearchElements,
                    els => els.Count(GetSearchCriteria) > 0);
            Timeouts.DropTimeouts();
            if (result == null)
                throw Exception("Can't get Web Elements");
            return result.Where(GetSearchCriteria).ToList();

        }

        private ISearchContext SearchContext(WebBaseElement element)
        {
            var searchContext = element.Parent is WebBaseElement
                ? SearchContext((WebBaseElement) element.Parent)
                : WebDriver.SwitchTo().DefaultContent();
            return element.Locator != null
                ? searchContext.FindElement(CorrectXPath(element.Locator))
                : searchContext;
        }

        public GetElementModule SearchAll()
        {
            LocalElementSearchCriteria = el => el != null;
            return this;
        }
        private List<IWebElement> SearchElements()
        {
            var context = _element.Parent is WebBaseElement
                ? SearchContext((WebBaseElement)_element.Parent)
                : WebDriver.SwitchTo().DefaultContent();
            return context.FindElements(CorrectXPath(ByLocator)).ToList();
        }
        private By CorrectXPath(By byValue)
        {
            return byValue.ToString().Contains("By.xpath: //")
                    ? byValue.GetByFunc()(new Regex("//").Replace(byValue.GetByLocator(), "./", 1))
                    : byValue;
        }

        private Func<IWebElement, bool> GetSearchCriteria 
            => LocalElementSearchCriteria ?? WebSettings.WebDriverFactory.ElementSearchCriteria;
        
    }
}
