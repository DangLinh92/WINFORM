using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_CANCEL_STOCKOUT : FormType { 
        public POP_CANCEL_STOCKOUT()
        {
            InitializeComponent();
        }
        public POP_CANCEL_STOCKOUT(string ID) : this()
        {
            try
            {
                DateTime date = DateTime.Now;
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.INIT_STOCKIN", new string[] { }, new string[] { });
                Console.WriteLine(base.mResultDB.ReturnInt);
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(sltMaterial, base.mResultDB.ReturnDataSet.Tables[0], "CODE", "NAME");
                    sltMaterial.Select();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        private void sltMaterial_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(sltMaterial.EditValue.NullString()))
            {
                e.Cancel = true;
                sltMaterial.Focus();
                dxErrorProvider1.SetError(sltMaterial, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.ClearErrors();
            }
        }
    }
}