using System;
using Epam.JDI.Core.Interfaces.Common;

namespace Epam.JDI.Core.Interfaces.Complex
{
    public interface IComboBox<TEnum> : IDropDown<TEnum>, ITextField
        where TEnum : IConvertible
    {
    }
}
