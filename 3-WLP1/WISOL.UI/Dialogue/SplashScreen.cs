using System;
using Wisol.Common;

namespace Wisol.MES.Dialog
{
    public partial class SplashScreen : DevExpress.XtraSplashScreen.SplashScreen
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            if ((SplashScreenCommand)cmd == SplashScreenCommand.Description)
            {
                lblMsg.Text = arg.NullString();
            }

            base.ProcessCommand(cmd, arg);
        }

        public enum SplashScreenCommand
        {
            Description = 0
        }
    }
}
