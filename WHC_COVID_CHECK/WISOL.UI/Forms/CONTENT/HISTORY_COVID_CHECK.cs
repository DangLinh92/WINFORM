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
    public partial class HISTORY_COVID_CHECK : PageType
    {
        public HISTORY_COVID_CHECK()
        {
            InitializeComponent();
            this.Load += HISTORY_COVID_CHECK_Load;
        }

        private void HISTORY_COVID_CHECK_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(this, "HISTORY_COVID_CHECK");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK_HISTORY.SEARCH",
                    new string[] { "A_TEXT", "A_DATE_FROM", "A_DATE_TO" },
                    new string[] { txtSearch.EditValue.NullString(), dateFrom.EditValue.NullString(), dateTo.EditValue.NullString() });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    //base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                    gvList.Columns["ID"].Visible = false;
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string OK_NG = "OK";
                if (!checkOK.Checked)
                {
                    OK_NG = "NG";
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.UPDATE_ITEM",
                   new string[] { "A_ID", "A_DATE", "A_PCR_QUICK", "A_LOCATION", "A_MA_NV", "A_NAME", "A_DEPT_CODE", "A_OKE_NG_PCR", "A_OKE_NG_QUICK", "A_NOTE", "A_CA_LAM", "A_THOI_GIAN_TEST", "A_EVENT" },
                   new string[] { txtID.EditValue.NullString(), dateCheck.EditValue.NullString(),
                        "PCR", txtAddressTest.EditValue.NullString(), txtMaNV.EditValue.NullString().ToUpper(), txtName.EditValue.NullString(),
                        txtDept.EditValue.NullString(), OK_NG, "NG", txtNote.EditValue.NullString(), txtCalam.EditValue.NullString(), dateCheck.EditValue.NullString(),txtNote.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt != 0)
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                }
                else
                {
                    MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    btnSearch.PerformClick();
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
                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_NHANVIEN_COVID_CHECK.DELETE", new string[] { "A_ID" }, new string[] { txtID.EditValue.NullString() });
                    if (m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        btnSearch.PerformClick();
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Warning);
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
            txtMaNV.EditValue = "";
            txtName.EditValue = "";
            txtDept.EditValue = "";
            txtCalam.EditValue = "";
            dateCheck.EditValue = "";
            txtAddressTest.EditValue = "";
            txtNote.EditValue = "";
            checkOK.Checked = true;
        }

        private void gvList_RowCellDefaultAlignment(object sender, DevExpress.XtraGrid.Views.Base.RowCellAlignmentEventArgs e)
        {

        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "ID").NullString();
            txtMaNV.EditValue = gvList.GetRowCellValue(e.RowHandle, "CODE").NullString();
            txtName.EditValue = gvList.GetRowCellValue(e.RowHandle, "NAME").NullString();
            txtDept.EditValue = gvList.GetRowCellValue(e.RowHandle, "DEPT_CODE").NullString();
            txtCalam.EditValue = gvList.GetRowCellValue(e.RowHandle, "CA_LAM_VIEC").NullString();
            txtAddressTest.EditValue = gvList.GetRowCellValue(e.RowHandle, "DIA_DIEM_TEST").NullString();
            txtNote.EditValue = gvList.GetRowCellValue(e.RowHandle, "NOTE").NullString();
            dateCheck.EditValue = gvList.GetRowCellValue(e.RowHandle, "THOI_GIAN_TEST").NullString();
            checkOK.Checked = gvList.GetRowCellValue(e.RowHandle, "PCR_OK").NullString() == "OK" ? true : false;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }
    }
}
