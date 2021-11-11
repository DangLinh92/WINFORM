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
    public partial class POP_STOCKOUT : FormType
    {

        private string costId = string.Empty;

        public POP_STOCKOUT()
        {
            InitializeComponent();
        }

        public POP_STOCKOUT(string ID) : this()
        {
            try
            {
                DateTime date = DateTime.Now;
                txtDateExport.EditValue = date.ToString("yyyy-MM-dd");
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.INIT_EXPORTSTOCK"
                    , new string[] {
                    }
                    , new string[] {
                    }
                    );
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
                    txtUnit.EditValue = dt.Rows[0]["UNIT"].ToString();
                    txtOnHand.EditValue = dt.Rows[0]["QUANTITY"].ToString();

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(sltMaterial.EditValue.NullString()) || string.IsNullOrEmpty(txtQuantityExport.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }
                if ( Convert.ToInt32(txtQuantityExport.EditValue.NullString()) <= Convert.ToInt32(txtOnHand.EditValue.NullString()))
                {
                    base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SETTING007.STOCK_OUT", new string[] {
                        "A_CODE", "A_COSTID", "A_VALID_DATE", "A_QUANTITY", "A_CAUSE", "A_TRAN_USER_ID"
                    }, new string[] {
                        sltMaterial.EditValue.NullString(),
                        costId,
                        txtDateExport.DateTime.ToString("yyyyMMdd"),
                        txtQuantityExport.EditValue.NullString(),
                        txtCause.EditValue.NullString(),
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

        private void txtDateExport_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDateExport.EditValue.NullString()))
            {
                e.Cancel = true;
                txtDateExport.Focus();
                dxErrorProvider2.SetError(txtDateExport, "Not be left blank!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider2.ClearErrors();
            }
        }

        private void txtQuantityExport_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantityExport.EditValue.NullString()))
            {
                e.Cancel = true;
                txtQuantityExport.Focus();
                dxErrorProvider3.SetError(txtQuantityExport, "Not be left blank!");
            }
            else if (txtQuantityExport.EditValue.ToInt() > txtOnHand.EditValue.ToInt())
            {
                e.Cancel = true;
                txtQuantityExport.Focus();
                dxErrorProvider3.SetError(txtQuantityExport, "Out of stock!");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider3.ClearErrors();
            }


        }
    }
}