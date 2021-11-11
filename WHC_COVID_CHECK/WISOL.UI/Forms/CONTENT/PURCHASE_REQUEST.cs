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

namespace Wisol.MES.Forms.CONTENT
{
    public partial class PURCHASE_REQUEST : PageType
    {
        public PURCHASE_REQUEST()
        {
            InitializeComponent();
            this.Load += PURCHASE_REQUEST_Load;
        }

        private void PURCHASE_REQUEST_Load(object sender, EventArgs e)
        {
            GetDataInit();
            Classes.Common.SetFormIdToButton(this, "PURCHASE_REQUEST");
        }

        private void GetDataInit()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_PR.INIT_LIST", new string[] { "A_DEPARTMENT" }, new string[] { Consts.DEPARTMENT });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    // base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;
                    FormatTotalValue();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            POP.PURCHASE_REQUEST_DETAIL pop = new POP.PURCHASE_REQUEST_DETAIL();
            pop.Mode = Consts.MODE_NEW;
            pop.PRCode = string.Empty;
            pop.ShowDialog();
            GetDataInit();
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.FieldName == "VIEW")
                {
                    POP.PURCHASE_REQUEST_DETAIL pop = new POP.PURCHASE_REQUEST_DETAIL();
                    pop.Mode = Consts.MODE_VIEW;
                    pop.Status = gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString();
                    pop.PRCode = gvList.GetRowCellValue(e.RowHandle, "PR_CODE").NullString();
                    pop.MRPCode = gvList.GetRowCellValue(e.RowHandle, "MRP_CODE").NullString();
                    pop.ShowDialog();
                    GetDataInit();
                }
                else if (e.Column.FieldName == "EDIT")
                {
                    POP.PURCHASE_REQUEST_DETAIL pop = new POP.PURCHASE_REQUEST_DETAIL();
                    pop.Mode = Consts.MODE_UPDATE;
                    pop.Status = gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString();
                    pop.PRCode = gvList.GetRowCellValue(e.RowHandle, "PR_CODE").NullString();
                    pop.MRPCode = gvList.GetRowCellValue(e.RowHandle, "MRP_CODE").NullString();
                    pop.ShowDialog();
                    GetDataInit();
                }
                else if (e.Column.FieldName == "DELETE")
                {
                    DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_PR.DELETE", new string[] { "A_DEPARTMENT", "A_PR_CODE" }, new string[] { Consts.DEPARTMENT, gvList.GetRowCellValue(e.RowHandle, "PR_CODE").NullString() });
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Information);
                            GetDataInit();
                        }
                        else
                        {
                            MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                        }
                    }
                }
                else if (e.Column.FieldName == "CHART")
                {
                    splashScreenManager1.ShowWaitForm();
                    string mainID = gvList.GetRowCellValue(e.RowHandle, "PR_CODE").NullString() + "^" +
                                   gvList.GetRowCellValue(e.RowHandle, "MRP_CODE").NullString() + "^" +
                                   gvList.GetRowCellValue(e.RowHandle, "DATE_CREATE").NullString() + "^" +
                                   gvList.GetRowCellValue(e.RowHandle, "TOTAL_VALUE").NullString() + "^" +
                                   gvList.GetRowCellValue(e.RowHandle, "TOTAL_VALUE_US").NullString();
                    Consts.mainForm.NewPageFromOtherPage("PR_CHART", "Biểu đồ kế hoạch sản xuất", "W", "Y", mainID);
                    splashScreenManager1.CloseWaitForm();
                }
                else
                {
                    txtPO_ID.EditValue = gvList.GetRowCellValue(e.RowHandle, "PO_ID");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dateFromTime.EditValue.NullString()) || string.IsNullOrEmpty(dateToTime.EditValue.NullString()))
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS_PR.SEARCH_PR_BY_TIME",
                    new string[] { "A_DEPARTMENT", "A_FROM_TIME", "A_TO_TIME" },
                    new string[] { Consts.DEPARTMENT, dateFromTime.EditValue.NullString(), dateToTime.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    gcList.DataSource = base.m_ResultDB.ReturnDataSet.Tables[0];
                    gvList.OptionsView.ColumnAutoWidth = true;

                    FormatTotalValue();
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

        private void FormatTotalValue()
        {
            gvList.Columns["TOTAL_VALUE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["TOTAL_VALUE"].DisplayFormat.FormatString = "c3";
            gvList.Columns["TOTAL_VALUE_US"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["TOTAL_VALUE_US"].DisplayFormat.FormatString = "c3";
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            dateFromTime.EditValue = null;
            dateToTime.EditValue = null;
            GetDataInit();
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName == "PR_STATUS")
            {
                if (gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString() == Consts.STATUS_CANCEL)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 153, 153);
                }
                else if (gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString() == Consts.STATUS_ACCEPT)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 204);
                }
                else if (gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString() == Consts.STATUS_ORDER)
                {
                    e.Appearance.BackColor = Color.FromArgb(153, 255, 255);
                }
                else if (gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString() == Consts.STATUS_SHIPPING)
                {
                    e.Appearance.BackColor = Color.FromArgb(204, 255, 153);
                }
                else if (gvList.GetRowCellValue(e.RowHandle, "PR_STATUS").NullString() == Consts.STATUS_COMPLETE)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
            }

            if(e.Column.FieldName == "PO_ID")
            {
                e.Appearance.BackColor = Color.FromArgb(251, 238, 230);
            } 
        }

        private void btnDetailViewPO_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                string mainId = txtPO_ID.EditValue.NullString();
                Consts.mainForm.NewPageFromOtherPage("SAP_PO_PR_INFO", "Thông tin chi tiết đặt hàng với mã PO trên SAP", "W", "Y", mainId);
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
