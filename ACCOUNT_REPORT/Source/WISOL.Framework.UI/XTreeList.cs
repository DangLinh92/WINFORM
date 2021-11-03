using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Wisol
{
    [ToolboxItem(true)]
    public class XTreeList : TreeList
    {
        private string valueMember;
        private string displayMember;

        static XTreeList()
        {

        }

        public XTreeList()
        {
        }

        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
        [DefaultValue("")]
        [DXCategory("Data")]
        public virtual string ValueMember
        {
            get
            {
                return valueMember;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                if (!(ValueMember == value))
                {
                    string oldValue = ValueMember;
                    valueMember = value;
                }
            }
        }
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
        [Category("Data")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design")]
        public virtual string DisplayMember
        {
            get
            {
                return displayMember;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                if (!(DisplayMember == value))
                {
                    displayMember = value;
                }
            }
        }

        public object GetDataRow(object rowHandle)
        {
            throw new NotImplementedException();
        }
    }
}
