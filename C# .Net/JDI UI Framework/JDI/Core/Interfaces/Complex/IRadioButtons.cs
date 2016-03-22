using System;


namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface IRadioButtons<TEnum> : ISelector<TEnum>
        where TEnum : IConvertible
    {
    }
}
