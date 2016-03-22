﻿using System;
using Epam.JDI.Core.Interfaces.Base;

//using System.IComparable instead of interface CharSequence

namespace Epam.JDI.Core.Interfaces.Common
{
    public interface ITextField : ISetValue, IText
    {
        /**
     * @param text Specify text to input to TextField
     *             Input text in textfield
     */
        //TODO [JDIAction]
        void Input(IComparable text);

        /**
         * @param text Specify text to send keys to TextField
         *             Input text in textfield
         */
        //TODO [JDIAction]
        //TODO SendKeys() was default
        void SendKeys(IComparable text);
        /**
         * @param text Specify text to input to TextField
         *             Clear and input text in textfield
         */
        //TODO [JDIAction]
        void NewInput(IComparable text);

        /**
         * Clear textfield
         */
        //TODO [JDIAction]
        void Clear();

        /**
         * Focus(click) on textfield
         */
        //TODO [JDIAction]
        void Focus();
    }
}
