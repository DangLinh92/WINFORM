using DevExpress.XtraWaitForm;
using System;

namespace Wisol.Components
{
    public partial class FrmWaitForm : WaitForm
    {
        public FrmWaitForm()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }


        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }


        public enum WaitFormCommand
        {
        }
    }
}