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
using Wisol.MES.Classes;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class FRM_BANK : PageType
    {
        public FRM_BANK()
        {
            InitializeComponent();
            this.Load += FRM_BANK_Load;
        }

        private void FRM_BANK_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "FRM_BANK");
            InitData();
        }

        private void InitData()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.INIT", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = base.m_ResultDB.ReturnDataSet.Tables[0];
                    //base.m_BindData.BindGridView(gcList, data);
                    gcList.DataSource = data;

                    gvList.Columns["OPEN_BALANCE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["OPEN_BALANCE"].DisplayFormat.FormatString = "n2";

                    gvList.OptionsView.ColumnAutoWidth = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (gvList.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(gvList.PaintAppearance.SelectedRow);
            else
                e.Appearance.Assign(gvList.PaintAppearance.Row);
            e.HighPriority = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBankId.EditValue.NullString() == "" ||
                    txtBankName.EditValue.NullString() == "" ||
                    txtBankAccount.EditValue.NullString() == "" ||
                    txtBeginMoney.EditValue.NullString() == "" ||
                    cboCurrency.Text.NullString() == "" ||
                    dateInit.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.PUT",
                                           new string[] { "A_BANK_ID", "A_BANK_NAME", "A_BANK_ACCOUNT", "A_BANK_BRANCH", "A_OPEN_BALANCE", "A_CURRENCY", "A_DATE", "A_USER" },
                                           new string[]
                                           {
                                                txtBankId.EditValue.NullString(),
                                                txtBankName.EditValue.NullString(),
                                                txtBankAccount.EditValue.NullString(),
                                                txtBanch.EditValue.NullString(),
                                                txtBeginMoney.EditValue.NullString(),
                                                cboCurrency.Text.NullString(),
                                                dateInit.EditValue.NullString(),
                                                Consts.USER_INFO.Id
                                           });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    InitData();
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
                if (txtBankId.EditValue.NullString() == "" ||
                   txtBankAccount.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.DELETE",
                                        new string[] { "A_BANK_ID", "A_BANK_ACCOUNT", "A_USER" },
                                        new string[]
                                        {
                                                txtBankId.EditValue.NullString(),
                                                txtBankAccount.EditValue.NullString(),
                                                Consts.USER_INFO.Id
                                        });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        InitData();
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
            txtBankId.EditValue = "";
            txtBankName.EditValue = "";
            txtBankAccount.EditValue = "";
            txtBanch.EditValue = "";
            txtBeginMoney.EditValue = 0;
            cboCurrency.EditValue = "";
            dateInit.EditValue = null;
        }

        private void gvList_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_BANK.GET",
                                        new string[] { "A_BANK_ID", "A_BANK_ACCOUNT" },
                                        new string[]
                                        {
                                                gvList.GetRowCellValue(e.RowHandle,"SWIFT CODE").NullString(),
                                                gvList.GetRowCellValue(e.RowHandle,"BANK_ACCOUNT").NullString()
                                        });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable data = m_ResultDB.ReturnDataSet.Tables[0];

                    txtBankId.EditValue = data.Rows[0]["ID"].NullString();
                    txtBankName.EditValue = data.Rows[0]["BANK_NAME"].NullString();
                    txtBankAccount.EditValue = data.Rows[0]["BANK_ACCOUNT"].NullString();
                    txtBanch.EditValue = data.Rows[0]["BANK_BRANCH"].NullString();
                    txtBeginMoney.EditValue = data.Rows[0]["OPEN_BALANCE"].NullString();
                    cboCurrency.Text = data.Rows[0]["CURRENCY"].NullString();
                    dateInit.EditValue = data.Rows[0]["DATE"].NullString();
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
    }
}
