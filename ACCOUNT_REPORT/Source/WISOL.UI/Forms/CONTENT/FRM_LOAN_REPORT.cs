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

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_LOAN_REPORT : PageType
    {
        public FRM_LOAN_REPORT()
        {
            InitializeComponent();
            this.Load += FRM_LOAN_REPORT_Load;
        }

        private void FRM_LOAN_REPORT_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_LOAN_REPORT");
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_LOAN_REPORT.INIT", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection dataCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlBank, dataCollection[0], "ID", "BANK_NAME", "BANK_ACCOUNT");
                    m_BindData.BindGridLookEdit(stlBankAccount, dataCollection[0], "BANK_ACCOUNT", "BANK_ACCOUNT", "BANK_NAME,ID");
                    gcList.DataSource = dataCollection[1];
                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["BANK_ID"].Visible = false;
                    gvList.OptionsView.ColumnAutoWidth = true;

                    gvList.Columns["LIMITED"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["LIMITED"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["LOAN_AMOUNT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["LOAN_AMOUNT"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["REMAIN_LIMITED"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["REMAIN_LIMITED"].DisplayFormat.FormatString = "n2";
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void stlBankAccount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.GET_BANK_FROM_ACCOUNT",
                   new string[] { "A_BANK_ACCOUNT" },
                   new string[] { stlBankAccount.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    if (data.Rows.Count > 0)
                    {
                        stlBank.EditValue = data.Rows[0]["ID"].NullString();
                    }
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
                if (stlBank.EditValue.NullString() == "" ||
                    stlBankAccount.EditValue.NullString() == "" ||
                    dateOpen.EditValue.NullString() == "" ||
                    dateDue.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_LOAN_REPORT.PUT",
                  new string[] { "A_ID", "A_BANK_ID", "A_BANK_NAME", "A_BANK_ACCOUNT", "A_OPEN_DATE", "A_DUE_DATE", "A_LIMITED", "A_LOAN_AMOUNT", "A_REMAIN_LIMITED", "A_NOTE1", "A_NOTE2", "A_CURRENCY", "A_USER_UPDATE" },
                  new string[]
                  {
                      txtID.EditValue.NullString(),
                      stlBank.EditValue.NullString(),
                      stlBank.Text.NullString(),
                      stlBankAccount.EditValue.NullString(),
                      dateOpen.EditValue.NullString(),
                      dateDue.EditValue.NullString(),
                      txtLimited.EditValue.NullString(),
                      txtLoanAmount.EditValue.NullString(),
                      txtRemainLimited.EditValue.NullString(),
                      txtNote1.EditValue.NullString(),
                      txtNote2.EditValue.NullString(),
                      cboCurrency.Text.NullString(),
                      Consts.USER_INFO.Id
                  });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    InitData();
                    btnClear.PerformClick();
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_LOAN_REPORT.DELETE",
                  new string[] { "A_ID" },
                  new string[] { txtID.EditValue.NullString() });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        InitData();
                        btnClear.PerformClick();
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.EditValue = "";
                stlBank.EditValue = "";
                stlBank.Text = "";
                stlBankAccount.EditValue = "";
                dateOpen.EditValue = null;
                dateDue.EditValue = null;
                txtLimited.EditValue = "";
                txtLoanAmount.EditValue = "";
                txtRemainLimited.EditValue = "";
                txtNote1.EditValue = "";
                txtNote2.EditValue = "";
                cboCurrency.Text = "USD";
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "ID").NullString();
                stlBank.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ID").NullString();
                stlBankAccount.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ACCOUNT").NullString();
                dateOpen.EditValue = gvList.GetRowCellValue(e.RowHandle, "OPEN_DATE").NullString();
                dateDue.EditValue = gvList.GetRowCellValue(e.RowHandle, "DUE_DATE").NullString();
                txtLimited.EditValue = gvList.GetRowCellValue(e.RowHandle, "LIMITED").NullString();
                txtLoanAmount.EditValue = gvList.GetRowCellValue(e.RowHandle, "LOAN_AMOUNT").NullString();
                txtRemainLimited.EditValue = gvList.GetRowCellValue(e.RowHandle, "REMAIN_LIMITED").NullString();
                cboCurrency.Text = gvList.GetRowCellValue(e.RowHandle, "CURRENCY").NullString();
                txtNote1.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE1").NullString();
                txtNote2.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE2").NullString();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
