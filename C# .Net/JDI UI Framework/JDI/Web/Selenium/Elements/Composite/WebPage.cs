using static Epam.JDI.Web.Selenium.Elements.Composite.CheckPageTypes;
using static Epam.JDI.Web.Settings.WebSettings;

namespace Epam.JDI.Web.Selenium.Elements.Composite
{
    public class WebPage
    {
        public string Url;
        public string Title;
        protected CheckPageTypes CheckUrlType = EQUAL;
        protected CheckPageTypes CheckTitleType = EQUAL;
        protected string UrlTemplate;
        public static string GetUrlFromUri(string uri)
        {
            return Domain.Replace("/*$", "") + "/" + uri.Replace("^/*", "");
        }
        public static string GetMatchFromDomain(string uri)
        {
            return Domain.Replace("/*$", "").Replace(".", "\\.") + "/" + uri.Replace("^/*", "");
        }
        public void UpdatePageData(string url, string title, CheckPageTypes checkUrlType, CheckPageTypes checkTitleType, string urlTemplate)
        {
            if (Url == null)
                Url = url;
            if (Title == null)
                Title = title;
            CheckUrlType = checkUrlType;
            CheckTitleType = checkTitleType;
            UrlTemplate = urlTemplate;
        }
    }
}
