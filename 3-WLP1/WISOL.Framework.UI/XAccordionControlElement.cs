using DevExpress.XtraBars.Navigation;
using System.Data;

namespace Wisol
{
    public class XAccordionControlElement : XAccordionControlElement<DataRow>
    {
        public string KeyColumn { get; set; }

        public string NameColumn { get; set; }

        public override DataRow DataRow
        {
            get => base.DataRow;
            set
            {
                base.DataRow = value;
                Key = !KeyColumn.IsNullOrEmpty() && value.Table.Columns.Contains(KeyColumn) ? value[KeyColumn] : null;
                Text = !NameColumn.IsNullOrEmpty() && value.Table.Columns.Contains(NameColumn) ? value[NameColumn].ToString() : string.Empty;
            }
        }

        public object Key { get; set; }
        public XAccordionControlElement() : base()
        {
        }
        public XAccordionControlElement(ElementStyle style, DataRow data, string keyColumnName, string nameColumnName) : base(style)
        {
            KeyColumn = keyColumnName;
            NameColumn = nameColumnName;
            DataRow = data;
        }
    }
    public class XAccordionControlElement<T> : AccordionControlElement where T : class
    {
        public virtual T DataRow { get; set; }
        public XAccordionControlElement() : base()
        {
        }
        public XAccordionControlElement(ElementStyle style) : base(style)
        {

        }
    }
}
