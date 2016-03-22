using System;
using Epam.JDI.Core.Interfaces.Base;

namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface IGroup<TEnum,TType> : IBaseElement 
        where TEnum : IConvertible
        where TType : IElement
    {
        TType Get(TEnum name);

        TType Get(string name);
    }
}
