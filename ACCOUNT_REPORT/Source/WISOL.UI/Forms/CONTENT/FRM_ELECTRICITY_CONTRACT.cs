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
    public partial class FRM_ELECTRICITY_CONTRACT : PageType
    {
        public FRM_ELECTRICITY_CONTRACT()
        {
            InitializeComponent();
            this.Load += FRM_ELECTRICITY_CONTRACT_Load;
        }

        private void FRM_ELECTRICITY_CONTRACT_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_ELECTRICITY_CONTRACT");
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_ELECTRICITY_CONTRACT.INIT", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection dataCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlBank, dataCollection[0], "ID", "BANK_NAME", "BANK_ACCOUNT");
                    m_BindData.BindGridLookEdit(stlAccount, dataCollection[0], "BANK_ACCOUNT", "BANK_ACCOUNT", "BANK_NAME,ID");
                    //m_BindData.BindGridView(gcList, dataCollection[1], false, "ID,BANK_ID");
                    gcList.DataSource = dataCollection[1];
                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["BANK_ID"].Visible = false;
                    gvList.OptionsView.ColumnAutoWidth = true;

                    gvList.Columns["BEGIN_BALANCE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["BEGIN_BALANCE"].DisplayFormat.FormatString = "n2";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (stlBank.EditValue.NullString() == "" ||
                    stlAccount.EditValue.NullString() == "" ||
                    txtBeginBalance.EditValue.NullString() == "" ||
                    dateOpen.EditValue.NullString() == "" ||
                    dateMaturity.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_ELECTRICITY_CONTRACT.PUT",
                    new string[] { "A_ID", "A_BANK_ID", "A_BANK_ACCOUNT", "A_BANK_NAME", "A_BEGIN_BALANCE", "A_OPEN_DATE", "A_MATURITY_DATE", "A_INTEREST_RATE", "A_CURRENCY", "A_NOTE" },
                    new string[] {
                        txtID.EditValue.NullString(),
                        stlBank.EditValue.NullString(),
                        stlAccount.EditValue.NullString(),
                        stlBank.Text.NullString(),
                        txtBeginBalance.EditValue.NullString(),
                        dateOpen.EditValue.NullString(),
                        dateMaturity.EditValue.NullString(),
                        txtInterestRate.EditValue.NullString(),
                        cboCurrency.Text,
                        txtNote.EditValue.NullString()});

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
                if(txtID.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_ELECTRICITY_CONTRACT.DELETE",
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
            txtID.EditValue = "";
            stlBank.EditValue = "";
            stlAccount.EditValue = "";
            txtBeginBalance.EditValue = "";
            dateOpen.EditValue = null;
            dateMaturity.EditValue = null;
            txtInterestRate.EditValue = "";
            cboCurrency.Text = "VND";
            txtNote.EditValue = "";
        }

        private void stlAccount_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.GET_BANK_FROM_ACCOUNT",
                   new string[] { "A_BANK_ACCOUNT" },
                   new string[] { stlAccount.EditValue.NullString() });

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

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "ID").NullString();
                stlBank.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ID").NullString();
                stlAccount.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ACCOUNT").NullString();
                txtBeginBalance.EditValue = gvList.GetRowCellValue(e.RowHandle, "BEGIN_BALANCE").NullString();
                dateOpen.EditValue = gvList.GetRowCellValue(e.RowHandle, "OPEN_DATE").NullString();
                dateMaturity.EditValue = gvList.GetRowCellValue(e.RowHandle, "MATURITY_DATE").NullString();
                txtInterestRate.EditValue = gvList.GetRowCellValue(e.RowHandle, "INTEREST_RATE").NullString();
                txtNote.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE").NullString();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
