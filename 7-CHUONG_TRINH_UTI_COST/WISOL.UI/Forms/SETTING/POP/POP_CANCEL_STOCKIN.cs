using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_CANCEL_STOCKIN : FormType
    {
        private string id = string.Empty;
        public POP_CANCEL_STOCKIN()
        {
            InitializeComponent();
        }
        public POP_CANCEL_STOCKIN(string ID) : this()
        {
            try
            {
                DateTime date = DateTime.Now;
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.INIT_CANCEL_STOCK_IN", new string[] { }, new string[] { });
                if (base.mResultDB.ReturnInt == 0)
                {
                    base.mBindData.BindGridLookEdit(sltMaterial, base.mResultDB.ReturnDataSet.Tables[0], "ID", "CODE");
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

        private void sltMaterial_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit lookUpEdit = sender as GridLookUpEdit;
            DataRowView selectedDataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

            id = sltMaterial.EditValue.NullString();
            Console.WriteLine(id);
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.DETAIL_ITEM_STOCKIN", new string[] {
                "A_ID"}, new string[] { id });
                Console.WriteLine(base.mResultDB.ReturnInt);
                if (base.mResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0].Copy();

                    txtCode.EditValue = dt.Rows[0]["CODE"].ToString();
                    txtName.EditValue = dt.Rows[0]["NAME"].ToString();
                    txtCost.EditValue = string.Format("{0:#,##0}", double.Parse(dt.Rows[0]["COST"].ToString()));
                    txtUnit.EditValue = dt.Rows[0]["UNIT"].ToString();
                    txtQuantityStockin.EditValue = dt.Rows[0]["QUANTITY"].ToString();
                    txtValidDate.EditValue = dt.Rows[0]["VALID_DATE"].ToString();   

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (id.Trim() == string.Empty)
            {
                return;
            }
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.mResultDB = base.mDBaccess.ExcuteProc("[PKG_SETTING007.PUT_CANCEL_STOCKIN]"
                        , new string[] { "A_ID", "A_TRAN_USER"
                        }
                        , new string[] { id, Consts.USER_INFO.Id
                        }
                        );
                    if (base.mResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Information);
                        this.Close();
                    }
                    else
                    {
                        MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
            }
        }
    }
}