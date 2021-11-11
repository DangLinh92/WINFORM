using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using Wisol.Components;

namespace Wisol
{
    [ToolboxItem(true)]
    public partial class XSimpleButton : SimpleButton
    {
        private ButtonTypes m_ButtonType = ButtonTypes.None;
        [Description("Image Button")]
        [Bindable(true)]
        [Category("Wisol")]
        [DefaultValue(ButtonTypes.None)]
        public ButtonTypes ButtonType
        {
            get
            {
                return m_ButtonType;
            }
            set
            {
                m_ButtonType = value;

                switch (value)
                {
                    case ButtonTypes.None:
                        Image = null;
                        Text = string.Empty;
                        break;
                    case ButtonTypes.Add:
                        Image = global::Wisol.Properties.Resources.add_32x32;
                        Text = "ADD";
                        break;
                    case ButtonTypes.Backword:
                        Image = global::Wisol.Properties.Resources.backward_32x32;
                        Text = "BACKWORD";
                        break;
                    case ButtonTypes.Cancel:
                        Image = global::Wisol.Properties.Resources.cancel_32x32;
                        Text = "CANCEL";
                        break;
                    case ButtonTypes.Export:
                        Image = global::Wisol.Properties.Resources.export_32x32;
                        Text = "EXPORT";
                        break;
                    case ButtonTypes.Find:
                        Image = global::Wisol.Properties.Resources.find_32x32;
                        Text = "FIND";
                        break;
                    case ButtonTypes.Forword:
                        Image = global::Wisol.Properties.Resources.forward_32x32;
                        Text = "FORWORD";
                        break;
                    case ButtonTypes.Init:
                        Image = global::Wisol.Properties.Resources.new_32x32;
                        Text = "INIT";
                        break;
                    case ButtonTypes.Ok:
                        Image = global::Wisol.Properties.Resources.apply_32x32;
                        Text = "OK";
                        break;
                    case ButtonTypes.Preview:
                        Image = global::Wisol.Properties.Resources.preview_32x32;
                        Text = "PREVIEW";
                        break;
                    case ButtonTypes.Print:
                        Image = global::Wisol.Properties.Resources.print_32x32;
                        Text = "PRINT";
                        break;
                    case ButtonTypes.Save:
                        Image = global::Wisol.Properties.Resources.save_32x32;
                        Text = "SAVE";
                        break;
                    case ButtonTypes.Setting:
                        Image = global::Wisol.Properties.Resources.properties_32x32;
                        Text = "SETTING";
                        break;
                    case ButtonTypes.User:
                        Image = global::Wisol.Properties.Resources.customer_32x32;
                        Text = "USER";
                        break;
                    case ButtonTypes.View:
                        Image = global::Wisol.Properties.Resources.zoom_32x32;
                        Text = "VIEW";
                        break;
                }
            }
        }

        public string FormId { get; set; }
        public bool isFormType { get; set; }

        protected override void OnClick(EventArgs e)
        {
            if (isFormType)
            {
                base.OnClick(e);
            }
            else
            {
                bool active = CommonRoleControl.GetActiveWithRole(FormId, this.Name);

                if (active)
                {
                    base.OnClick(e);
                }
                else
                {
                    MsgBox.Show("NOT HAVE PERMISSION TO ACCESS!!!", MsgType.Warning);
                }
            }
        }

        public XSimpleButton()
        {
            InitializeComponent();

            Size = new System.Drawing.Size(129, 30);
            MaximumSize = new System.Drawing.Size(129, 30);
            MinimumSize = new System.Drawing.Size(129, 30);
        }

        public XSimpleButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Size = new System.Drawing.Size(129, 30);
            MaximumSize = new System.Drawing.Size(129, 30);
            MinimumSize = new System.Drawing.Size(129, 30);
        }
    }

    public enum ButtonTypes
    {
        None,
        Add,
        Backword,
        Cancel,
        Export,
        Find,
        Forword,
        Init,
        Ok,
        Preview,
        Print,
        Save,
        Setting,
        User,
        View
    }
}
