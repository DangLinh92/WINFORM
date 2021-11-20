using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_RECEIVED_PAYMENT_DETAIL : PageType
    {
        public FRM_RECEIVED_PAYMENT_DETAIL()
        {
            InitializeComponent();
            this.Load += FRM_RECEIVED_PAYMENT_DETAIL_Load;
        }

        private void FRM_RECEIVED_PAYMENT_DETAIL_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_RECEIVED_PAYMENT_DETAIL");
            dateFrom.EditValue = DateTime.Now;
            dateTo.EditValue = DateTime.Now;
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_RECEIVE_PAYMENT_DETAIL.INIT",
                    new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] { dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection dataCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlBank, dataCollection[0], "ID", "BANK_NAME", "BANK_ACCOUNT");
                    m_BindData.BindGridLookEdit(stlBank_Account, dataCollection[0], "BANK_ACCOUNT", "BANK_ACCOUNT", "ID,BANK_NAME");

                    gcList.DataSource = dataCollection[1];
                    gvList.Columns["ID"].Visible = false;
                    gvList.Columns["BANK_ID"].Visible = false;
                    gvList.Columns["BANK_ACCOUNT"].Visible = false;
                    gvList.Columns["BANK_ACCOUNT"].Visible = false;
                    gvList.Columns["ITEM_TYPE"].Visible = false;
                    gvList.Columns["USER_UPDATE"].Visible = false;
                    gvList.Columns["SYS_DATE"].Visible = false;

                    gvList.OptionsView.ColumnAutoWidth = true;

                    gvList.Columns["CASH"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CASH"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["DEPOSIT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["DEPOSIT"].DisplayFormat.FormatString = "n2";
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
                    stlBank_Account.EditValue.NullString() == "" ||
                    dateRevPay.EditValue.NullString() == "" ||
                    cboCurrency.Text.NullString() == "" ||
                    txtCash.EditValue.NullString() == "" ||
                    txtDeposit.EditValue.NullString() == "" ||
                    cboItemType.Text.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_RECEIVE_PAYMENT_DETAIL.PUT",
                   new string[] { "A_ID", "A_BANK_ID", "A_BANK_NAME","A_BANK_ACCOUNT", "A_TRANS_DATE", "A_DETAIL", "A_CURRENCY", "A_CASH", "A_DEPOSIT", "A_VENDOR", "A_USER", "A_ITEM_TYPE", "A_ITEM" },
                   new string[]
                   {
                         txtID.EditValue.NullString(),
                         stlBank.EditValue.NullString(),
                         stlBank.Text.NullString(),
                         stlBank_Account.EditValue.NullString(),
                         dateRevPay.EditValue.NullString(),
                         txtDetail.EditValue.NullString(),
                         cboCurrency.Text.NullString(),
                         txtCash.EditValue.NullString(),
                         txtDeposit.EditValue.NullString(),
                         txtVendor.EditValue.NullString(),
                         Consts.USER_INFO.Id,
                         cboItemType.Text.NullString(),
                         txtItem.EditValue.NullString()
                   });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    btnSearch.PerformClick();
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
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_RECEIVE_PAYMENT_DETAIL.DELETE",
                                  new string[] { "A_ID" },
                                  new string[] { txtID.EditValue.NullString() });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        btnSearch.PerformClick();
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
            dateRevPay.EditValue = null;
            txtDetail.EditValue = "";
            cboCurrency.Text = "";
            txtCash.EditValue = "";
            txtDeposit.EditValue = "";
            txtVendor.EditValue = "";
            cboItemType.Text = "";
            txtItem.EditValue = "";
            stlBank_Account.EditValue = "";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                POP.IMPORT_EXCEL pop = new POP.IMPORT_EXCEL();
                pop.ImportType = Consts.IMPORT_TYPE_RECEIVE_PAYMENT;
                pop.ShowDialog();
                btnSearch.PerformClick();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "RECEIVE_PAYMENT.xlsx";

                string url = Application.StartupPath + @"\\" + fileName;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (WebClient web1 = new WebClient())
                        web1.DownloadFile(url, saveFileDialog.FileName);

                    MsgBox.Show("MSG_COM_001".Translation(), MsgType.Information);

                    //open file 
                    Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "ID").NullString();
                stlBank.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ID").NullString();
                dateRevPay.EditValue = gvList.GetRowCellValue(e.RowHandle, "TRANS_DATE").NullString();
                txtDetail.EditValue = gvList.GetRowCellValue(e.RowHandle, "DETAIL").NullString();
                cboCurrency.Text = gvList.GetRowCellValue(e.RowHandle, "CURRENCY").NullString();
                txtCash.EditValue = gvList.GetRowCellValue(e.RowHandle, "CASH").NullString();
                txtDeposit.EditValue = gvList.GetRowCellValue(e.RowHandle, "DEPOSIT").NullString();
                txtVendor.EditValue = gvList.GetRowCellValue(e.RowHandle, "VENDOR_ID").NullString();
                cboItemType.Text = gvList.GetRowCellValue(e.RowHandle, "ITEM_TYPE").NullString();
                txtItem.EditValue = gvList.GetRowCellValue(e.RowHandle, "ITEM").NullString();
                stlBank_Account.EditValue = gvList.GetRowCellValue(e.RowHandle, "BANK_ACCOUNT").NullString();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void stlBank_Account_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.GET_BANK_FROM_ACCOUNT",
                   new string[] { "A_BANK_ACCOUNT" },
                   new string[] { stlBank_Account.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    if (data.Rows.Count > 0)
                    {
                        stlBank.EditValue = data.Rows[0]["ID"].NullString();
                    }
                    else
                    {
                        stlBank.EditValue = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
