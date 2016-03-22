using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Epam.JDI.Core.Interfaces.Base;
using Epam.JDI.Core.Interfaces.Settings;
using Epam.JDI.Core.Settings;
using Epam.JDI.Web.Selenium.Elements.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using static System.String;
using static Epam.JDI.Web.Selenium.DriverFactory.RunTypes;
using static Epam.Properties.Settings;

namespace Epam.JDI.Web.Selenium.DriverFactory
{
    public enum RunTypes { Local, Remote }
    public enum DriverTypes { Chrome, Firefox, IE }
    public class WebDriverFactory : IDriver<IWebDriver>
    {
        private Dictionary<string, Func<IWebDriver>> Drivers { get; } = new Dictionary<string, Func<IWebDriver>>();
        private Dictionary<string, IWebDriver> RunDrivers { get; } = new Dictionary<string, IWebDriver>();
        public string CurrentDriverName { get; set; }
        public string DriverPath { get; set; } = "C:/Selenium";
        public RunTypes RunType { get; set; } = Local;
        public HighlightSettings HighlightSettings = new HighlightSettings();
        public Func<IWebElement, bool> ElementSearchCriteria = el => el.Displayed;

        private readonly Dictionary<DriverTypes, string> _driversDictionary = new Dictionary<DriverTypes, string>
        {
            {DriverTypes.Chrome, "chrome"},
            {DriverTypes.Firefox, "firefox"},
            {DriverTypes.IE, "internet explorer"}
        };

        //TODO 
        private String RegisterLocalDriver(DriverTypes driverType)
        {
            var driverName = _driversDictionary[driverType];
            switch (driverType)
            {
                case DriverTypes.Chrome:
                    return RegisterDriver(driverName, () => new ChromeDriver(DriverPath));
                case DriverTypes.Firefox:
                    return RegisterDriver(driverName, () => new FirefoxDriver());
                case DriverTypes.IE:
                    return RegisterDriver(driverName, () => new InternetExplorerDriver(DriverPath));
            }
            throw new Exception(); // TODO
        }

        public string RegisterDriver(string driverName, Func<IWebDriver> driver)
        {
            try
            {
                Drivers.Add(driverName, driver);
                CurrentDriverName = driverName;
            }
            catch
            {
                throw new Exception($"Can't register WebDriver {driverName} . Driver with same name already registered");
            }
            return driverName;
        }

        public string RegisterDriver(Func<IWebDriver> driver)
        {
            return RegisterDriver("Driver" + Drivers.Count + 1, driver);
        }

        public IWebDriver GetDriver()
        {
            try
            {
                if (!IsNullOrEmpty(CurrentDriverName))
                    return GetDriver(CurrentDriverName);
                RegisterDriver(DriverTypes.Chrome);
                return GetDriver(DriverTypes.Chrome);
            }
            catch
            {
                throw new Exception(); // TODO
            }
        }

        public IWebDriver GetDriver(DriverTypes driverType)
        {
            return GetDriver(_driversDictionary[driverType]);
        }

        public IWebDriver GetDriver(string driverName)
        {
            if (!Drivers.ContainsKey(driverName))
                throw new Exception($"Can't find driver with name {driverName}");
            try
            {
                if (RunDrivers.ContainsKey(driverName))
                    return RunDrivers[driverName];
                var resultDriver = Drivers[driverName].Invoke();
                RunDrivers.Add(driverName, resultDriver);
                if (resultDriver == null)
                    throw new Exception($"Can't get Webdriver {driverName}. This Driver name not registered");
                return resultDriver;
            }
            catch
            {
                throw new Exception("Can't get driver.");
            }
        }
        
        public string RegisterDriver(string driverName)
        {
            try
            {
                var driverType = _driversDictionary.FirstOrDefault(x => x.Value == driverName).Key;
                return RegisterLocalDriver(driverType);
            }
            catch {
                throw new Exception(); // TODO
            }
        }

        public string RegisterDriver(DriverTypes driverType)
        {
            switch (RunType)
            {
                case Local:
                    return RegisterLocalDriver(driverType);
                case Remote:
                    return RegisterRemoteDriver(driverType);
            }
            throw new Exception(); // TODO
        }

        private string RegisterRemoteDriver(DriverTypes driverType)
        {
            var capabilities = new DesiredCapabilities(new Dictionary<string, object>
            {
                {"browserName", _driversDictionary[driverType]},
                {"version", Empty},
                {"javaScript", true}
            });

            return RegisterDriver("Remote_" + _driversDictionary[driverType],
                () => new RemoteWebDriver(new Uri(Default.remote_url), capabilities));
        }

        public void SwitchToDriver(string driverName)
        {
            if (Drivers.ContainsKey(driverName))
                CurrentDriverName = driverName;
            else
                throw new Exception($"Can't switch to Webdriver {driverName}. This Driver name not registered");
        }

        public void ReopenDriver()
        {
            ReopenDriver(CurrentDriverName);
        }

        public void ReopenDriver(string driverName)
        {
            if (RunDrivers.ContainsKey(driverName))
            {
                RunDrivers[driverName].Close();
                RunDrivers.Remove(driverName);
            }
            if (Drivers.ContainsKey(driverName))
                GetDriver(); // TODO
        }

        public void Close()
        {
            foreach (var driver in RunDrivers)
                driver.Value.Quit();
            RunDrivers.Clear();
        }

        public void SetRunType(string runType)
        {
            switch (runType)
            {
                case "local" : RunType = Local; return;
                case "remote" : RunType = Remote; return;
            }
            RunType = Local;
        }

        public bool HasDrivers()
        {
            return Drivers.Any();
        }

        public bool HasRunDrivers()
        {
            return RunDrivers.Any();
        }
        
        
        public void Highlight(IElement element)
        {
            Highlight(element, HighlightSettings);
        }

        public void Highlight(IElement element, HighlightSettings highlightSettings)
        {
            if (highlightSettings == null)
                highlightSettings = new HighlightSettings();
            string orig = ((WebElement) element).GetWebElement().GetAttribute("style");
            element.SetAttribute("style",
                $"border: 3px solid {highlightSettings.FrameColor}; background-color: {highlightSettings.BgColor};");
            Thread.Sleep(highlightSettings.TimeoutInSec * 1000);
            element.SetAttribute("style", orig);
        }
        
    }
}