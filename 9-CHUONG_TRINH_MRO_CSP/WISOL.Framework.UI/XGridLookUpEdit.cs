using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Wisol
{
    [UserRepositoryItem("RegisterXGridLookUpEdit")]
    public class RepositoryItemXGridLookUpEdit : RepositoryItemGridLookUpEdit
    {
        static RepositoryItemXGridLookUpEdit()
        {
            RegisterXGridLookUpEdit();
        }

        public const string CustomEditName = "XGridLookUpEdit";

        public RepositoryItemXGridLookUpEdit()
        {
            InitializeComponent();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (!IsDesignMode)
            {
                View.Columns.Clear();
                View.Columns.Add(new GridColumn()
                {
                    FieldName = ValueMember,
                    Caption = ValueMember,
                    VisibleIndex = 1,
                });
                View.Columns.Add(new GridColumn()
                {
                    FieldName = DisplayMember,
                    Caption = DisplayMember,
                    VisibleIndex = 2,
                });
            }
        }

        protected override ColumnView CreateViewInstance()
        {
            var columnView = base.CreateViewInstance();
            return columnView;
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterXGridLookUpEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(XGridLookUpEdit), typeof(RepositoryItemXGridLookUpEdit), typeof(XGridLookUpEditViewInfo), new XGridLookUpEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemXGridLookUpEdit source = item as RepositoryItemXGridLookUpEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        private void RepositoryItemXGridLookUpEdit_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            var selectedRows = View.GetSelectedRows();
            var stringBuilder = new StringBuilder();
            foreach (var index in selectedRows)
            {
                if (View.GetRow(index) is System.Data.DataRowView rowView)
                {
                    if (stringBuilder.ToString().Length > 0) { stringBuilder.Append(", "); }
                    stringBuilder.Append(rowView[ValueMember]);
                }
            }
            e.DisplayText = stringBuilder.ToString();
        }
    }

    [ToolboxItem(true)]
    public class XGridLookUpEdit : GridLookUpEdit
    {
        static XGridLookUpEdit()
        {
            RepositoryItemXGridLookUpEdit.RegisterXGridLookUpEdit();
        }

        public XGridLookUpEdit()
        {
            InitializeComponent();
        }

        private void InitializeComponent() { }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemXGridLookUpEdit Properties => base.Properties as RepositoryItemXGridLookUpEdit;

        public override string EditorTypeName => RepositoryItemXGridLookUpEdit.CustomEditName;

        protected override PopupBaseForm CreatePopupForm()
        {
            return new XGridLookUpEditPopupForm(this);
        }
    }

    public class XGridLookUpEditViewInfo : GridLookUpEditBaseViewInfo
    {
        public XGridLookUpEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class XGridLookUpEditPainter : ButtonEditPainter
    {
        public XGridLookUpEditPainter()
        {
        }
    }

    public class XGridLookUpEditPopupForm : PopupGridLookUpEditForm
    {
        public XGridLookUpEditPopupForm(XGridLookUpEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}
