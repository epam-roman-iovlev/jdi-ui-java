namespace Epam.JDI.Core.Interfaces.Base
{
    public interface ISetValue : IHasValue
    {
        /**
         * param - value Specify element value
         *              Set value to WebElement
         */
        void SetValue(string value);
    }
}
