using System;
using System.Windows.Forms;

namespace Wisol.MES.Dialog
{
    public partial class DialogueWarning : Form
    {
        public DialogueWarning(string message)
        {
            InitializeComponent();
            this.lblMessage.Text = message;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
