using Epam.JDI.Core.Interfaces.Base;
namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface IPage : IComposite
    {
        /**
         * Check that page opened
         */
        //TODO[JDIAction]
         void CheckOpened();

        /**
         * Opens url specified for page
         */
        //TODO[JDIAction]
        T Open<T>() where T : IPage;
    }
}
