using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Data;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING.POP
{
    public partial class POP_STOCKIN : FormType
    {
        private string costId = string.Empty;

        public POP_STOCKIN()
        {
            InitializeComponent();
        }

        public POP_STOCKIN(string ID) : this()
        {
            try
            {
                DateTime date = DateTime.Now;
                txtDateImport.EditValue = date.ToString("yyyy-MM-dd");

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

        private void txtDateImport_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDateImport.EditValue.NullString()))
            {
                e.Cancel = true;
                txtDateImport.Focus();
                dxErrorProvider2.SetError(txtDateImport, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider2.ClearErrors();
            }
        }

        private void txtQuantityImport_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantityImport.EditValue.NullString()))
            {
                e.Cancel = true;
                txtQuantityImport.Focus();
                dxErrorProvider3.SetError(txtQuantityImport, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider3.ClearErrors();
            }
        }

        private void txtCostNew_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCostNew.EditValue.NullString()))
            {
                e.Cancel = true;
                txtCostNew.Focus();
                dxErrorProvider3.SetError(txtCostNew, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider3.ClearErrors();
            }
        }

        private void sltMaterial_EditValueChanged(object sender, EventArgs e)
        {

            GridLookUpEdit lookUpEdit = sender as GridLookUpEdit;
            DataRowView selectedDataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

            string code = sltMaterial.EditValue.NullString();
            costId = selectedDataRow["COST_ID"].ToString();
            try
            {
                
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.GET_ITEM_DETAIL", new string[] {
                "A_CODE", "A_COSTID" }, new string[] { code, costId });

                if (base.mResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.mResultDB.ReturnDataSet.Tables[0].Copy();

                    txtCode.EditValue = dt.Rows[0]["CODE"].ToString();
                    txtName.EditValue = dt.Rows[0]["NAME"].ToString();
                    txtCost.EditValue = string.Format("{0:#,##0}", double.Parse(dt.Rows[0]["COST"].ToString()));
                    txtCostNew.EditValue = dt.Rows[0]["COST"].ToString();
                    txtUnit.EditValue = dt.Rows[0]["UNIT"].ToString();
                    txtOnHand.EditValue = dt.Rows[0]["QUANTITY"].ToString();

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(sltMaterial.EditValue.NullString()) || string.IsNullOrEmpty(txtCostNew.EditValue.NullString()) || string.IsNullOrEmpty(txtQuantityImport.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.STOCK_IN", new string[] {
                        "A_CODE", "A_COSTID", "A_COST", "A_VALID_DATE", "A_QUANTITY", "A_TRAN_USER_ID"
                    }, new string[] {
                        sltMaterial.EditValue.NullString(),
                        costId,
                        txtCostNew.EditValue.NullString(),
                        txtDateImport.DateTime.ToString("yyyyMMdd"),
                        txtQuantityImport.EditValue.NullString(),
                        Consts.USER_INFO.Id,
                    });
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