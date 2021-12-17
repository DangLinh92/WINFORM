using DevExpress.Data;
using DevExpress.XtraGrid;
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
    public partial class FRM_BANK_STATEMENT : PageType
    {
        public FRM_BANK_STATEMENT()
        {
            InitializeComponent();
            this.Load += FRM_BANK_STATEMENT_Load;
        }

        private void FRM_BANK_STATEMENT_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_BANK_STATEMENT");
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.INIT", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection dataCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridLookEdit(stlBank, dataCollection[0], "ID", "BANK_NAME", "BANK_ACCOUNT");
                    m_BindData.BindGridLookEdit(stlBankAccount, dataCollection[0], "BANK_ACCOUNT", "BANK_ACCOUNT", "BANK_NAME,ID");
                    m_BindData.BindGridLookEdit(stlRemarkType, dataCollection[1], "ID", "REMARK_NAME_VN", "REMARK_NAME_KR");
                }
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
                string fileName = "BANK_STATEMENT.xlsx";

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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            POP.IMPORT_EXCEL pop = new POP.IMPORT_EXCEL();
            pop.ImportType = Consts.IMPORT_TYPE_BANK_STATEMENT;
            pop.ShowDialog();
            btnSearch.PerformClick();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.GET_BY_TIME",
                    new string[] { "A_DATE_FROM", "A_DATE_TO" },
                    new string[] { dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    //base.m_BindData.BindGridView(gcList, data);
                    gcList.DataSource = data;

                    gvList.Columns["DEBIT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["DEBIT"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["CREDIT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CREDIT"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["BALANCE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["BALANCE"].DisplayFormat.FormatString = "n2";

                    gvList.Columns["ID"].Visible = false;

                    gvList.OptionsView.ColumnAutoWidth = true;

                    GridColumnSummaryItem debitTotal = new GridColumnSummaryItem();
                    debitTotal.SummaryType = SummaryItemType.Sum;
                    debitTotal.DisplayFormat = "Debit: {0:#.###}";

                    GridColumnSummaryItem creditTotal = new GridColumnSummaryItem();
                    creditTotal.SummaryType = SummaryItemType.Sum;
                    creditTotal.DisplayFormat = "Credit: {0:#.###}";
                    if(gvList.Columns["DEBIT"].Summary.Count == 0)
                    {
                        gvList.Columns["DEBIT"].Summary.Add(debitTotal);
                    }

                    if (gvList.Columns["CREDIT"].Summary.Count == 0)
                    {
                        gvList.Columns["CREDIT"].Summary.Add(creditTotal);
                    }
                        
                    gvList.OptionsView.ShowFooter = true;
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.GET_BY_ID",
                   new string[] { "A_ID" },
                   new string[] { gvList.GetRowCellValue(e.RowHandle, "ID").NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    txtID.EditValue = data.Rows[0]["ID"];
                    stlBank.EditValue = data.Rows[0]["BANK_ID"];
                    stlBankAccount.EditValue = data.Rows[0]["BANK_ACCOUNT"];
                    dateTransaction.EditValue = data.Rows[0]["TRANSACTION_DATE"];
                    txtDebit.EditValue = data.Rows[0]["DEBIT"];
                    txtCredit.EditValue = data.Rows[0]["CREDIT"];
                    txtBalance.EditValue = data.Rows[0]["BALANCE"];
                    cboCurrency.Text = data.Rows[0]["CURRENCY"].NullString();
                    txtRemark.EditValue = data.Rows[0]["REMARK"].NullString();
                    stlRemarkType.EditValue = data.Rows[0]["REMARK_TYPE"].NullString();
                    txtTransactionType.EditValue = data.Rows[0]["TRANSACTION_TYPE"].NullString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (stlBankAccount.EditValue.NullString() == "" ||
                    stlBank.EditValue.NullString() == "" ||
                    dateTransaction.EditValue.NullString() == "" ||
                    txtTransactionType.EditValue.NullString() == "" ||
                    txtDebit.EditValue.NullString() == "" ||
                    txtCredit.EditValue.NullString() == "" ||
                    txtBalance.EditValue.NullString() == "" ||
                    cboCurrency.Text == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.PUT",
                  new string[] { "A_ID", "A_BANK_ACCOUNT", "A_BANK_ID", "A_BANK_NAME", "A_TRANSACTION_DATE", "A_TRANSACTION_TYPE", "A_DEBIT", "A_CREDIT", "A_BALANCE", "A_REMARK", "A_REMARK_TYPE", "A_CURRENCY", "A_USER" },
                  new string[]
                  {
                      txtID.EditValue.NullString(),
                      stlBankAccount.EditValue.NullString(),
                      stlBank.EditValue.NullString(),
                      stlBank.Text.NullString(),
                      dateTransaction.EditValue.NullString(),
                      txtTransactionType.EditValue.NullString(),
                      txtDebit.EditValue.NullString(),
                      txtCredit.EditValue.NullString(),
                      txtBalance.EditValue.NullString(),
                      txtRemark.EditValue.NullString(),
                      stlRemarkType.EditValue.NullString(),
                      cboCurrency.Text.NullString(),
                      Consts.USER_INFO.Id
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
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK_STATEMENT.DELETE",
                                            new string[] { "A_ID" },
                                            new string[]
                                            {
                                                 txtID.EditValue.NullString()
                                            });
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                    btnSearch.PerformClick();
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
            stlBankAccount.EditValue = "";
            stlRemarkType.EditValue = "";
            txtBalance.EditValue = 0;
            txtCredit.EditValue = 0;
            txtDebit.EditValue = 0;
            txtRemark.EditValue = "";
            txtTransactionType.EditValue = "";
            dateTransaction.EditValue = null;
            cboCurrency.Text = "";
        }

        private void gvList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (gvList.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(gvList.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(gvList.PaintAppearance.Row);
            e.HighPriority = true;
        }
    }
}
