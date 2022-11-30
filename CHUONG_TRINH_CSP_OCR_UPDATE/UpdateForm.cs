using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace WisolUpdate
{
    public partial class UpdateForm : DevExpress.XtraEditors.XtraForm
    {
        public UpdateForm()
        {
            InitializeComponent();
            this.lblMessage.Visible = true;
            this.button2.Visible = true;
            progressBarControl1.EditValue = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Process()
            {
                StartInfo = {
                                FileName = "UTI_COST",
                                Verb = "Open",
                                Arguments = "PASS"
                            }
            }.Start();
            Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }
    }
}