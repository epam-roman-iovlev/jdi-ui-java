using System;
using Epam.JDI.Core.Interfaces.Base;

namespace Epam.JDI.Web.Selenium.Elements.Base
{
    public class SelectElement : ClickableText, ISelect
    {
        public SelectElement()
        {
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public bool IsSelected()
        {
            throw new NotImplementedException();
        }
    }
}
