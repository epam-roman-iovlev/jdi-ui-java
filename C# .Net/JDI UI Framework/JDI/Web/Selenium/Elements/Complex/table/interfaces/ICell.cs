using Epam.JDI.Core.Interfaces.Base;

namespace Epam.JDI.Web.Selenium.Elements.Complex.table.interfaces
{
    public interface ICell : ISelect
    {
        string Value { get; set; } // TODO

        int ColumnNum();

        int RowNum();
    }
}