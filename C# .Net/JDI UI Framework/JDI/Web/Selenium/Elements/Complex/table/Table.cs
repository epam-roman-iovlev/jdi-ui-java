using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Epam.JDI.Core.Settings;
using Epam.JDI.Web.Selenium.Elements.Common;
using Epam.JDI.Web.Selenium.Elements.Complex.table.interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp.Extensions;
using Epam.JDI.Web.Selenium.Elements.Base;
using SelectElement = Epam.JDI.Web.Selenium.Elements.Base.SelectElement;


namespace Epam.JDI.Web.Selenium.Elements.Complex.table
{
    public class Table : Text, ITable, ICloneable
    {
        private List<ICell> _allCells = new List<ICell>();
        private Columns _columns = new Columns();
        private Rows _rows = new Rows();
        private List<string> _footer;

        private List<ICell> AllCells
        {
            get
            {
                var result = new List<ICell>();
                var rows = Rows.Get();
                Columns.Headers.ForEach(columnName => Rows.Headers
                    .ForEach(rowName => {/* //result.Add(Rows.Get(rowName).Get(columnName)); TODO */}));
                if (Cache)
                    AllCells = result;
                return result;
            }
            set { _allCells = value; }
        }

        private Columns Columns
        {
            get { return _columns; }
            set { _columns.Update(value); }
        }

        private Rows Rows
        {
            get { return _rows; }
            set { _rows.Update(value); }
        }

        public bool Cache { set; get; }
        protected By CellLocatorTemplate { set; get; }
        public By FooterLocator { get; set; } = By.XPath(".//tfoot/tr/th");
        public Table()
        {
            Columns.Table = this;
            Rows.Table = this;
        }

        public Table(By locator) : base(locator)
        {
            Columns.Table = this;
            Rows.Table = this;
        }

        public Table(By columnHeader, By row, By column) : this()
        {
            if (column != null)
                Columns.LineTemplate = column;
            if (columnHeader != null)
                Columns.HeadersLocator = columnHeader;
            if (row != null)
                Rows.LineTemplate = row;
        }

        public Table(By rowHeader, By columnHeader, By row, By column, int rowStartIndex, int columnStartIndex) : this()
        {
            if (column != null)
                Columns.LineTemplate = column;
            if (columnHeader != null)
                Columns.HeadersLocator = columnHeader;
            if (row != null)
                Rows.LineTemplate = row;
            if (rowHeader != null)
                Rows.HeadersLocator = rowHeader;

            if (columnStartIndex > -1)
                Columns.StartIndex = columnStartIndex;
            if (rowStartIndex > -1)
                Rows.StartIndex = rowStartIndex;
        }

        public Table(By tableLocator, By cellLocatorTemplate) : this(tableLocator)
        {
            CellLocatorTemplate = cellLocatorTemplate;
        }

        public Table(By columnHeader, By rowHeader, By row, By column, By footer, TableSettings settings,
                     int columnStartIndex, int rowStartIndex) : this()
        {
            Columns.LineTemplate = column;
            if (columnHeader != null)
                Columns.HeadersLocator = columnHeader;
            Rows.LineTemplate = row;
            if (rowHeader != null)
                Rows.HeadersLocator = rowHeader;
            FooterLocator = footer;

            Columns.StartIndex = columnStartIndex;
            Rows.StartIndex = rowStartIndex;

            SetTableSettings(settings);
        }

        public Table(TableSettings settings) : this()
        {
            SetTableSettings(settings);
        }

        public Table Copy()
        {
            return (Table) Clone();
        }

        public object Clone()
        {
            //asserter.silent(()->super.clone()); // TODO
            var newTable = new Table();
            newTable.Rows = Rows.Clone(new Rows(), newTable);
            newTable.Columns = Columns.Clone(new Columns(), newTable);
            //newTable.avatar = new GetElementModule(getLocator(), getAvatar().context, newTable); // TODO
            return newTable;
        }

        public void SetTableSettings(TableSettings settings)
        {
            Rows.HasHeader = settings.RowHasHeaders;
            Rows.Headers = settings.RowHeaders;
            Rows.Count = settings.RowsCount;
            Columns.HasHeader = settings.ColumnHasHeaders;
            Columns.Headers = settings.ColumnHeaders;
            Columns.Count = settings.ColumnsCount;
        }

        public ITable UseCache()
        {
            Cache = true;
            return this;
        }

        public void Clean()
        {
            AllCells = new List<ICell>();
            Columns.Clean();
            Rows.Clean();
        }

        public void Clear()
        {
            Clean();
        }

        public Dictionary<string, ICell> Column(int colNum)
        {
            return Columns.GetColumn(colNum);
        }

        public Dictionary<string, ICell> Column(string colName)
        {
            return Columns.GetColumn(colName);
        }

        public Dictionary<string, ICell> Column(string value, Row row)
        {
            ICell columnCell = Cell(value, row);
            return columnCell != null ? Columns.GetColumn(columnCell.ColumnNum()) : null;
        }

        private object Row(string value, Column column)
        {
            ICell rowCell = Cell(value, column);
            return rowCell != null ? Rows.GetRow(rowCell.RowNum()) : null;
        }

        private ICell Cell(string value, Column column)
        {
            int colIndex = column.Get( name => Columns.Headers.IndexOf(name) + 1, num => num);
            return Columns.GetColumn(colIndex).First(pair => pair.Value.GetValue().Equals(value)).Value;
        }

        private ICell Cell(string value, Row row) //TODO
        {
            throw new NotImplementedException();
        }

        public List<string> ColumnValue(int colNum)
        {
            return Columns.GetColumnValue(colNum);
        }

        public List<string> ColumnValue(string colName)
        {
            return Columns.GetColumnValue(colName);
        }

        private Dictionary<string, ICell> Column(Column column)
        {
 
            return column.Get(Column, Column);
        }

        public Dictionary<string, ICell> Row(int rowNum)
        {
            return Rows.GetRow(rowNum);
        }

        public Dictionary<string, ICell> Row(string rowName)
        {
            return Rows.GetRow(rowName);
        }

        public List<string> RowValue(int rowNum)
        {
            return Rows.GetRowValue(rowNum);
        }

        public List<string> RowValue(string rowName)
        {
            return Rows.GetRowValue(rowName);
        }

        private Dictionary<string, ICell> Row(Row row)
        {
            return row.Get(Row, Row);
        }

        public ITable HasAllHeaders()
        {
            Columns.HasHeader = true;
            Rows.HasHeader = true;
            return this;
        }

        public ITable HasNoHeaders()
        {
            Columns.HasHeader = false;
            Rows.HasHeader = false;
            return this;
        }

        public ITable HasOnlyColumnHeaders()
        {
            Columns.HasHeader = true;
            Rows.HasHeader = false;
            return this;
        }

        public ITable HasOnlyRowHeaders()
        {
            Rows.HasHeader = true;
            return this;
        }


        public ITable HasColumnHeaders(List<String> value)
        {
            Columns.HasHeader = true;
            Columns.Headers = new List<string>(value);
            return this;
        }

        public ITable  HasColumnHeaders (Type headers)
        {
            return HasColumnHeaders(GetAllEnumNames(headers));
        }

        public ITable HasRowHeaders(List<String> value)
        {
            Rows.HasHeader = true;
            Rows.Headers = new List<string>(value);
            return this;
        }

        public ITable HasRowHeaders(Type headers)
        {
            return HasRowHeaders(GetAllEnumNames(headers));
        }

        private List<string> GetAllEnumNames(Type headers)
        {
            throw new NotImplementedException();
        }

        public ITable SetColumnsCount(int value)
        {
            Columns.Count = value;
            return this;
        }

        public ITable SetRowsCount(int value)
        {
            Rows.Count = value;
            return this;
        }

        protected List<String> GetFooterAction()
        {
            //return select(getWebElement().findElements(footerLocator), WebElement::getText);
            throw new NotImplementedException();
        }


        public List<string> Footer
        {
            get { return new List<string>(_footer); }
            set { _footer = new List<string>(value); }
        }

        public ReadOnlyDictionary<string, SelectElement> Header()
        {
            return Columns.Header();
        }

        public SelectElement Header(string name)
        {
            return Columns.Header(name);
        }

        public List<string> Headers()
        {
            return Columns.Headers;
        }

        public List<string> FooterInstance() // TODO
        {
            if (Footer != null)
                return Footer;
            //_footer = invoker.doJActionResult("Get Footer", this::getFooterAction); TODO
            if (Footer == null || Footer.Count == 0)
                return new List<string>();
            Columns.Count = Footer.Count;
            return Footer;
        }

        public ICell Cell(Column column, Row row)
        {
            int colIndex = column.Get(GetColumnIndex, num => num + Columns.StartIndex - 1);
            int rowIndex = (int) row.Get(GetRowIndex, num => num + Rows.StartIndex - 1);
            return AddCell(colIndex, rowIndex,
                    column.Get(name => Columns.Headers.IndexOf(name) + 1, num => num),
                    row.Get(name => Rows.Headers.IndexOf(name) + 1, num => num),
                    column.Get(name => name, num => ""),
                    row.Get(name => name, num => ""));
        }

        public ICell Cell(IWebElement webElement, Column column, Row row)
        {
            return AddCell(webElement,
                    column.Get(name => Columns.Headers.IndexOf(name) + 1, num => num),
                    row.Get(name => Rows.Headers.IndexOf(name) + 1, num => num),
                    column.Get(name => name, num => ""),
                    row.Get(name => name, num => ""));
        }

        private List<ICell> Matches(Collection<ICell> list, String regex)
        {
            return new List<ICell> (list.Where(cell => cell.Value.Matches(regex)));
        }

        public List<ICell> Cells(String value)
        {
            return new List<ICell>(GetCells().Where(cell => cell.Value.Equals(value)));
        }

        public List<ICell> CellsMatch(String regex)
        {
            return Matches(GetCells(), regex);
        }

        public ICell Cell(string value)
        {
            return Rows.Get().Select(row => row.Value.First(pair => pair.Value.GetText().Equals(value)).Value).FirstOrDefault(result => result != null);
        }

        public ICell CellMatch(String regex)
        {
            return Rows.Get().Select(row => row.Value.First(pair => pair.Value.GetText().Matches(regex)).Value).FirstOrDefault(result => result != null);
        }

        public Dictionary<String, Dictionary<string, ICell>> RowsTemp(params string[] colNameValues) //TODO
        {
            var result = new Dictionary<String, Dictionary<String, ICell>>();
            foreach (var row in Rows.Get())
            {
                bool matches = true;
                foreach (var colNameValue in colNameValues)
                {
                    if (!colNameValue.Matches("[^=]+=[^=]*"))
                        throw new Exception("Wrong searchCriteria for Cells: " + colNameValue);
                    String[] splitted = colNameValue.Split(Convert.ToChar("="));
                    String colName = splitted[0];
                    String colValue = splitted[1];
                    ICell cell = row.Value[colName];
                    if (cell == null || !cell.GetValue().Equals(colValue))
                    {
                        matches = false;
                        break;
                    }
                }
                if (matches) result.Add(row.Key, row.Value);
            }
            return result;
        }

        public Dictionary<String, Dictionary<String, ICell>> ColumnsTemp(params string[] rowNameValues) //TODO
        {
            var result = new Dictionary<String, Dictionary<String, ICell>>();
            foreach (var column in Columns.Get())
            {
                bool matches = true;
                foreach (String rowNameValue in rowNameValues)
                {
                    if (!rowNameValue.Matches("[^=]+=[^=]*"))
                        throw new Exception("Wrong searchCritaria for Cells: " + rowNameValue);
                    String[] splitted = rowNameValue.Split(Convert.ToChar("="));
                    String rowName = splitted[0];
                    String rowValue = splitted[1];
                    ICell cell = column.Value[rowName];
                    if (cell == null || !cell.GetValue().Equals(rowValue))
                    {
                        matches = false;
                        break;
                    }
                }
                if (matches) result.Add(column.Key, column.Value);
            }
            return result;
        }

        public bool WaitValue(String value, Row row)
        {
            return Timer.Wait(() => Column(value, row) != null);
        }

        public bool WaitValue(String value, Column column)
        {
            return Timer.Wait(() => Row(value, column) != null);
        }

        public bool IsEmpty()
        {
            new ChromeDriver().Manage().Timeouts().ImplicitlyWait(TimeSpan.Zero);
            int rowsCount = Rows.Count;
            new ChromeDriver().Manage().Timeouts().ImplicitlyWait(new TimeSpan(JDISettings.Timeouts.CurrentTimeoutSec));
            return rowsCount == 0;
        }

        private Collection<ICell> GetCells()
        {
           throw new NotImplementedException();
        }

        private ICell AddCell(IWebElement webElement, int colNum, int rowNum, String colName, String rowName)
        {
            throw new NotImplementedException();
        }

        private ICell AddCell(int colIndex, int rowIndex, int colNum, int rowNum, String colName, String rowName)
        {
            throw new NotImplementedException();
        }

        private object GetRowIndex(string name)
        {
            throw new NotImplementedException();
        }

        private int GetColumnIndex(string name)
        {
            throw new NotImplementedException();
        }
    }
}