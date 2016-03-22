using System;
using Epam.JDI.Core.Interfaces.Common;

namespace Epam.JDI.Web.Selenium.Elements.Common
{
    public class TextField : Text, ITextField
    {
        public void Input(IComparable text)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string value)
        {
            throw  new NotImplementedException();
        }

        public void SendKeys(IComparable text)
        {
            throw new NotImplementedException();
        }

        public void NewInput(IComparable text)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Focus()
        {
            throw new NotImplementedException();
        }
    }
}
