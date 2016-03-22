using System;
using Epam.JDI.Core.Interfaces.Common;
using Epam.JDI.Web.Selenium.Elements.Base;

namespace Epam.JDI.Web.Selenium.Elements.Common
{
    public class Image : Clickable, IImage
    {
        public string GetSource()
        {
            throw new NotImplementedException();
        }

        public string GetAlt()
        {
            throw new NotImplementedException();
        }

    }
}
