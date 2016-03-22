using System;
using Epam.JDI.Core.Settings;
using Epam.JDI.Web.Selenium.DriverFactory;
using OpenQA.Selenium;
using static Epam.Properties.Settings;

namespace Epam.JDI.Web.Settings
{
    public class WebSettings : JDISettings
    {
        public static string Domain;
        public static bool HasDomain => Domain != null && Domain.Contains("://");

        public static IWebDriver WebDriver => WebDriverFactory.GetDriver();
        public static WebDriverFactory WebDriverFactory => (WebDriverFactory) DriverFactory;

        public static string UseDriver(DriverTypes driverName)
        {
            return WebDriverFactory.RegisterDriver(driverName);
        }

        public static string UseDriver(Func<IWebDriver> driver)
        {
            return WebDriverFactory.RegisterDriver(driver);
        }

        public static IJavaScriptExecutor JSExecutor => DriverFactory.GetDriver() as IJavaScriptExecutor;


        public static void Init()
        {
            DriverFactory = new WebDriverFactory();
            // TODO
            /*Asserter =;
            asserter = new Check()
            {
                @Override
                protected String doScreenshotGetMessage()
            {
                return ScreenshotMaker.doScreenshotGetMessage();
            }
            }.doScreenshot(SCREEN_ON_FAIL);
            Assert.setMatcher((BaseMatcher) asserter);
            Timeouts = new WebTimeoutSettings();
            Logger = new TestNGLogger();*/
            MapInterfaceToElement.Init(DefaultInterfacesMap);
        }

        public new static void InitFromProperties() 
        {
            Init();
            JDISettings.InitFromProperties();
            FillFromSettings(p => Domain = p, "domain");
            FillFromSettings(p => DriverFactory.DriverPath = p, "drivers.folder");
            var isMultithread = Default["multithread"].ToString();
            // TODO
            /*Logger = isMultithread.Equals("true") || isMultithread.Equals("1")
                ? new TestNGLogger("JDI Logger", s -> String.format("[ThreadId: %s] %s", Thread.currentThread().getId(), s))
                : new TestNGLogger("JDI Logger");*/
        }

        private static object[][] DefaultInterfacesMap = {/*
            {IElement.class, WebElement.class},
            {IButton.class, Button.class},
            {IClickable.class, Clickable.class},
            {IComboBox.class, ComboBox.class},
            {ILink.class, Link.class},
            {ISelector.class, Selector.class},
            {IText.class, Text.class},
            {IImage.class, Image.class},
            {ITextArea.class, TextArea.class},
            {ITextField.class, TextField.class},
            {ILabel.class, Label.class},
            {IDropDown.class, Dropdown.class},
            {IDropList.class, DropList.class},
            {IGroup.class, ElementsGroup.class},
            {ITable.class, Table.class},
            {ICheckBox.class, CheckBox.class},
            {IRadioButtons.class, RadioButtons.class},
            {ICheckList.class, CheckList.class},
            {ITextList.class, TextList.class},
            {ITabs.class, Tabs.class},
            {IMenu.class, Menu.class},
            {IFileInput.class, FileInput.class},
            {IDatePicker.class, DatePicker.class},*/
        };
    }
}
