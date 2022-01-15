using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.ComponentModel;
using System.Drawing;

namespace Wisol
{
    [UserRepositoryItem("RegisterXSearchLookUpEdit")]
    public class RepositoryItemXSearchLookUpEdit : RepositoryItemSearchLookUpEdit
    {
        static RepositoryItemXSearchLookUpEdit()
        {
            RegisterXSearchLookUpEdit();
        }

        public const string CustomEditName = "XSearchLookUpEdit";

        public RepositoryItemXSearchLookUpEdit()
        {
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterXSearchLookUpEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(XSearchLookUpEdit), typeof(RepositoryItemXSearchLookUpEdit), typeof(XSearchLookUpEditViewInfo), new XSearchLookUpEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemXSearchLookUpEdit source = item as RepositoryItemXSearchLookUpEdit;
                if (source == null) return;
                //
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class XSearchLookUpEdit : SearchLookUpEdit
    {
        static XSearchLookUpEdit()
        {
            RepositoryItemXSearchLookUpEdit.RegisterXSearchLookUpEdit();
        }

        public XSearchLookUpEdit()
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemXSearchLookUpEdit Properties => base.Properties as RepositoryItemXSearchLookUpEdit;

        public override string EditorTypeName => RepositoryItemXSearchLookUpEdit.CustomEditName;

        protected override PopupBaseForm CreatePopupForm()
        {
            return new XSearchLookUpEditPopupForm(this);
        }
    }

    public class XSearchLookUpEditViewInfo : SearchLookUpEditBaseViewInfo
    {
        public XSearchLookUpEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class XSearchLookUpEditPainter : ButtonEditPainter
    {
        public XSearchLookUpEditPainter()
        {
        }
    }

    public class XSearchLookUpEditPopupForm : PopupSearchLookUpEditForm
    {
        public XSearchLookUpEditPopupForm(XSearchLookUpEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}
