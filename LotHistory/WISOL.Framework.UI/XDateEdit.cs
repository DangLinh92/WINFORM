using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout.Utils;
using System.ComponentModel;
using System.Drawing;

namespace Wisol
{
    [UserRepositoryItem("RegisterXDateEdit")]
    public class RepositoryItemXDateEdit : RepositoryItemDateEdit
    {
        static RepositoryItemXDateEdit()
        {
            RegisterXDateEdit();
        }

        public const string CustomEditName = "XDateEdit";

        public RepositoryItemXDateEdit()
        {
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterXDateEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(XDateEdit), typeof(RepositoryItemXDateEdit), typeof(XDateEditViewInfo), new XDateEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemXDateEdit source = item as RepositoryItemXDateEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class XDateEdit : DateEdit
    {
        static XDateEdit()
        {
            RepositoryItemXDateEdit.RegisterXDateEdit();
        }

        public XDateEdit()
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemXDateEdit Properties => base.Properties as RepositoryItemXDateEdit;

        public override string EditorTypeName => RepositoryItemXDateEdit.CustomEditName;

        public LayoutVisibility Visibility { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new XDateEditPopupForm(this);
        }
    }

    public class XDateEditViewInfo : DateEditViewInfo
    {
        public XDateEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class XDateEditPainter : ButtonEditPainter
    {
        public XDateEditPainter()
        {
        }
    }

    public class XDateEditPopupForm : PopupDateEditForm
    {
        public XDateEditPopupForm(XDateEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}
