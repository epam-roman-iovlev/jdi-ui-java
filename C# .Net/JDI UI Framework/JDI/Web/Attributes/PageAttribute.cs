using System;
using System.Reflection;
using Epam.JDI.Core.Settings;
using Epam.JDI.Web.Selenium.Elements.Composite;
using Epam.JDI.Web.Settings;
using static System.String;
using static Epam.JDI.Web.Selenium.Elements.Composite.CheckPageTypes;
using static Epam.JDI.Web.Selenium.Elements.Composite.WebPage;
using static Epam.JDI.Web.Settings.WebSettings;

namespace Epam.JDI.Web.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class PageAttribute : Attribute
    {
        public string Url           = "";
        public string UrlTemplate   = "";
        public string Title         = "";
        public CheckPageTypes CheckType        = NONE;
        public CheckPageTypes UrlCheckType     = NONE;
        public CheckPageTypes TitleCheckType   = NONE;
        
        public static PageAttribute Handler(FieldInfo field)
        {
            return field.GetCustomAttribute<PageAttribute>(false);
        }

        public static PageAttribute Handler(object obj)
        {
            return obj.GetType().GetCustomAttribute<PageAttribute>(false);
        }

        public void FillPage(WebPage page, Type parentClass)
        {
            var url = Url;
            url = url.Contains("://") || parentClass == null || !HasDomain
                    ? url
                    : GetUrlFromUri(url);
            var title = Title;
            var urlTemplate = UrlTemplate;
            if (!IsNullOrEmpty(urlTemplate))
                urlTemplate = (urlTemplate.Contains("://") || parentClass == null || !HasDomain)
                        ? urlTemplate
                        : GetMatchFromDomain(urlTemplate);
            var checkType = CheckType;
            var urlCheckType = UrlCheckType;
            var titleCheckType = TitleCheckType;
            if (urlCheckType == NONE)
                urlCheckType = checkType != NONE ? checkType : EQUAL;
            if (titleCheckType == NONE)
                titleCheckType = checkType != NONE ? checkType : EQUAL;
            if (urlCheckType == MATCH || urlCheckType == CONTAIN && IsNullOrEmpty(urlTemplate))
                urlTemplate = url;
            page.UpdatePageData(url, title, urlCheckType, titleCheckType, urlTemplate);
        }

    }

    public enum PageCheckType { NoCheck, Equal, Contains }
}
