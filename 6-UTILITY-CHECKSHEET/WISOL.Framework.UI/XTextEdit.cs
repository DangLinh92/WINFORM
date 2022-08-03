using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Wisol
{
    [UserRepositoryItem("RegisterXTextEdit")]
    public class RepositoryItemXTextEdit : RepositoryItemTextEdit
    {
        static RepositoryItemXTextEdit()
        {
            RegisterXTextEdit();
        }

        public const string CustomEditName = "XTextEdit";

        public RepositoryItemXTextEdit()
        {
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterXTextEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(XTextEdit), typeof(RepositoryItemXTextEdit), typeof(XTextEditViewInfo), new XTextEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                var source = item as RepositoryItemXTextEdit;
                if (source == null)
                {
                    return;
                }
                //
            }
            finally
            {
                EndUpdate();
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
    }

    [ToolboxItem(true)]
    public class XTextEdit : TextEdit
    {
        private Timer m_NullValidation_Timer;

        static XTextEdit()
        {
            RepositoryItemXTextEdit.RegisterXTextEdit();
        }

        public XTextEdit()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_NullValidation_Timer = new System.Windows.Forms.Timer();
            this.m_NullValidation_Timer.Interval = 500;
            this.m_NullValidation_Timer.Tick += (sender, e) => { SetBackColor(); };
        }

        private void SetTimer()
        {
            BackColor = NullValidation ? NullBackColor : DefaultBackColor;
        }

        private void SetBackColor()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { SetBackColor(); }));
            }
            else
            {
                if (!DesignMode)
                {
                    if (EditValue == null)
                    {
                        BackColor = DefaultBackColor;
                    }
                    else
                    {
                        BackColor = BackColor == NullBackColor ? DefaultBackColor : NullBackColor;
                    }
                }
            }
        }

        private new Color DefaultBackColor => (ViewInfo == null ? Color.Empty : ViewInfo.DefaultAppearance.BackColor);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemXTextEdit Properties => base.Properties as RepositoryItemXTextEdit;

        public override string EditorTypeName => RepositoryItemXTextEdit.CustomEditName;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DXCategory(nameof(XTextEdit))]
        public Color NullBackColor { get; set; } = Color.Lime;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DXCategory(nameof(XTextEdit))]
        public bool NullValidation
        {
            get { return m_NullValidation_Timer.Enabled; }
            set
            {
                m_NullValidation_Timer.Enabled = value;
                SetTimer();
            }
        }

        public string Value
        {
            get
            {
                if (base.EditValue != null)
                {
                    return base.EditValue.ToString();
                }
                return null;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            SetTimer();
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            SetTimer();
        }
    }

    public class XTextEditViewInfo : TextEditViewInfo
    {
        public XTextEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class XTextEditPainter : TextEditPainter
    {
        public XTextEditPainter()
        {
        }
    }
}
