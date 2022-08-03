using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Wisol.Components
{
    public partial class FrmSystemAlarm : Form
    {
        private string startProgram = string.Empty;

        public FrmSystemAlarm()
        {
            InitializeComponent();
        }

        public FrmSystemAlarm(Form _parentForm, string _Msg, string _title, string _startProgram)
        {
            InitializeComponent();
            this.Text = _title;
            lblMsg.Text = _Msg;
            this.startProgram = _startProgram;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ApplicationContext ac = new ApplicationContext();

            string runApplication = System.IO.Path.Combine(Application.StartupPath, startProgram);
            Process.Start(runApplication);
            Process.GetCurrentProcess().Kill();

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            ApplicationContext ac = new ApplicationContext();

            string runApplication = System.IO.Path.Combine(Application.StartupPath, startProgram);
            Process.Start(runApplication);
            Process.GetCurrentProcess().Kill();

        }

        private void FrmSystemAlarm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationContext ac = new ApplicationContext();

            string runApplication = System.IO.Path.Combine(Application.StartupPath, startProgram);
            Process.Start(runApplication);
            Process.GetCurrentProcess().Kill();
        }
    }
}
