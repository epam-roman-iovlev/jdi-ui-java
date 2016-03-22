using System;
using System.Collections.Generic;
using Epam.JDI.Core.Interfaces.Base;

namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface ITextList<TEnum> : IBaseElement, IHasValue, IVisible
        where TEnum : IConvertible
    {
        /**
     * @param name Specify string by string mechanic
     * @return Get textList’s text by specified param
     */
        //TODO[JDIAction]
        string getText(string name);

        /**
         * @param index Specify string by Integer mechanic
         * @return Get textList’s text by specified param
         */
        //TODO[JDIAction]
        string GetText(int index);

        /**
         * @param enumName Specify string by Enum mechanic
         * @return Get textList’s text by specified param
         */
        //TODO[JDIAction]
        string GetText(TEnum enumName);

        /**
         * @return Returns strings count
         */
        //TODO[JDIAction]
        int count();

        /**
         * @return Wait while TextList’s text contains expected text. Returns WebElement’s text
         */
        //TODO[JDIAction]
        List<string> WaitText(string str);

        /**
         * @return Return list of strings of TextList
         */
        //TODO[JDIAction]
        List<string> GetTextList();

        /**
         * @return Return first string in list
         */
        //TODO[JDIAction]
        string GetFirstText();

        /**
         * @return Return last string in list
         */
        //TODO[JDIAction]
        string GetLastText();
    }
}
