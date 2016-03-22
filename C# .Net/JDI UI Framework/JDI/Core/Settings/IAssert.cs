using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.JDI.Core.Settings
{
    public interface IAssert
    {
        Exception Exception(string message, params object[] args);
        void AreEquals<T>(T actual, T expected);
        void Matches(string actual, string regEx);
        void Contains(string actual, string expected);
        void IsTrue(bool actual);
        void UsTrue(Func<bool> actual);

    }
}
