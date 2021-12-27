using System;
using Wisol.Common;
using Wisol.MES.Inherit;

namespace Wisol.MES.Dialog
{
    public partial class FrmPassword : FormType
    {
        public string password;
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            password = txtPassword.EditValue.NullString();
            this.Close();
        }
    }
}
