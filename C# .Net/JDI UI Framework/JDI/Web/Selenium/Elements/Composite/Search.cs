using System;
using System.Collections.Generic;
using Epam.JDI.Core.Interfaces.Complex;
using Epam.JDI.Web.Selenium.Elements.Common;

namespace Epam.JDI.Web.Selenium.Elements.Composite
{
    public class Search : TextField, ISearch
    {
        public void ChooseSuggestion(string text, string selectValue)
        {
            throw new NotImplementedException();
        }

        public void ChooseSuggestion(string text, int selectIndex)
        {
            throw new NotImplementedException();
        }

        public void Find(string text)
        {
            throw new NotImplementedException();
        }

        public List<string> GetSuggesions(string text)
        {
            throw new NotImplementedException();
        }
    }
}
