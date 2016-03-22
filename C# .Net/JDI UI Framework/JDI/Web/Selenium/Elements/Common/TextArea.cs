using System;
using Epam.JDI.Core.Interfaces.Common;

namespace Epam.JDI.Web.Selenium.Elements.Common
{
    public class TextArea : TextField, ITextArea
    {
        public void InputLines(params string[] textLines)
        {
            throw new NotImplementedException();
        }

        public void AddNewLine(string textLine)
        {
            throw new NotImplementedException();
        }
        public String[] GetLines()
        {
            throw new NotImplementedException();
        }
    }
}
