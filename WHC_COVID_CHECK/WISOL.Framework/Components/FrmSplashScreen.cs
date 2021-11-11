using DevExpress.XtraSplashScreen;
using System;
using Wisol.Common;

namespace Wisol.Components
{
    public partial class FrmSplashScreen : SplashScreen
    {
        private string projectName = string.Empty;
        private string imagePath = string.Empty;
        public FrmSplashScreen()
        {
            InitializeComponent();
            this.labelControl3.Text = "드림텍 MES";
        }
        public FrmSplashScreen(string _projectName, string _imagePath)
        {
            InitializeComponent();
            this.imagePath = _imagePath;
            this.projectName = _projectName;
            pictureEdit2.LoadAsync(this.imagePath);
            labelControl3.Text = this.projectName;
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
