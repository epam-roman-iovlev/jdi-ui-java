using System;
using Epam.JDI.Core.Interfaces.Common;
using Epam.JDI.Web.Selenium.Elements.Base;

namespace Epam.JDI.Web.Selenium.Elements.Common
{
    public class Links : ClickableText, ILink
    {
        public string GetReference()
        {
            throw new NotImplementedException();
        }

        public string WaitReferenceContains(string text)
        {
            throw new NotImplementedException();
        }

        public string WaitMatchReference(string regEx)
        {
            throw new NotImplementedException();
        }

        public string GetTooltip()
        {
            throw new NotImplementedException();
        }
    }
}
