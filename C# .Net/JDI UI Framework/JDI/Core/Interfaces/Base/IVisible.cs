namespace Epam.JDI.Core.Interfaces.Base
{
    public interface IVisible
    {
        /**
    *  Check is WebElement visible
    */
        //TODO:[JDIAction]
        bool IsDisplayed();

        /**
         * Check is WebElement hidden
         */
        //TODO:[JDIAction]
        bool IsHidden();

        /**
         * Waits while WebElement becomes visible
         */
        //TODO:[JDIAction]
        void WaitDisplayed();

        /**
         * Waits while WebElement becomes invisible
         */
        //TODO:[JDIAction]
        void WaitVanished();
    }
}
