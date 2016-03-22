using System;

namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface IMenu<TEnum> : ISelector<TEnum>
        where TEnum : IConvertible
    {
    }
}
