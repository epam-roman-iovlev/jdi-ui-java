using System;
using static Epam.JDI.Core.Settings.JDISettings;

namespace Epam.JDI.Commons
{
    public static class ExceptionUtils
    {
        public static void ActionWithException(Action action, Func<string, string> exception)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                throw Exception(exception.Invoke(ex.Message));
            }
        }
        public static T ActionWithException<T>(Func<T> func, Func<string, string> exception)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                throw Exception(exception.Invoke(ex.Message));
            }
        }
    }
}
