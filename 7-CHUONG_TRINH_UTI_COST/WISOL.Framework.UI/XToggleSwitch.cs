using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.ComponentModel;
using System.Drawing;

namespace Wisol
{
    [UserRepositoryItem("RegisterXToggleSwitch")]
    public class RepositoryItemXToggleSwitch : RepositoryItemToggleSwitch
    {
        static RepositoryItemXToggleSwitch()
        {
            RegisterXToggleSwitch();
        }

        public const string CustomEditName = "XToggleSwitch";

        public RepositoryItemXToggleSwitch()
        {
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterXToggleSwitch()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(XToggleSwitch), typeof(RepositoryItemXToggleSwitch), typeof(XToggleSwitchViewInfo), new XToggleSwitchPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemXToggleSwitch source = item as RepositoryItemXToggleSwitch;
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
    public class XToggleSwitch : ToggleSwitch
    {
        static XToggleSwitch()
        {
            RepositoryItemXToggleSwitch.RegisterXToggleSwitch();
        }

        public XToggleSwitch()
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemXToggleSwitch Properties => base.Properties as RepositoryItemXToggleSwitch;

        public override string EditorTypeName => RepositoryItemXToggleSwitch.CustomEditName;
    }

    public class XToggleSwitchViewInfo : ToggleSwitchViewInfo
    {
        public XToggleSwitchViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class XToggleSwitchPainter : ToggleSwitchPainter
    {
        public XToggleSwitchPainter()
        {
        }
    }
}
