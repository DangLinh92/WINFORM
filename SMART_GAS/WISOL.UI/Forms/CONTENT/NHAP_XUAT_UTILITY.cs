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
    public partial class NHAP_XUAT_UTILITY : PageType
    {
        public NHAP_XUAT_UTILITY()
        {
            InitializeComponent();
            Classes.Common.SetFormIdToButton(this, "NHAP_XUAT_KHO");
            
            this.Load += NHAP_XUAT_UTILITY_Load;
        }

        private void NHAP_XUAT_UTILITY_Load(object sender, EventArgs e)
        {
            InitData();
            btnReload.PerformClick();
        }

        private void InitData()
        {
            try
            {
                dateSearch.EditValue = "";

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@INIT_UTILITY", new string[] { }, new string[] { });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection tableCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridView(gcList, tableCollection[0]);
                    gvListSum.OptionsView.ColumnAutoWidth = true;
                    if (gvList.Columns.Count > 0)
                        gvList.Columns["Id"].Visible = false;
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
                if (txtKg.EditValue.NullString() == "" || cboInOut.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@PUT_INOUT_UTILITY",
                    new string[] { "A_ID", "A_QTY_EA", "A_QTY_KG", "A_DATE", "A_IN_OUT", "A_USER", "A_PRICE" },
                    new string[]
                    {
                        txtID.EditValue.NullString(),
                        txtQuantity.EditValue.NullString(),
                        txtKg.EditValue.NullString(),
                        dateMonth.EditValue.NullString(),
                        cboInOut.EditValue.NullString(),
                        txtUser.EditValue.NullString(),
                        txtAmount.EditValue.NullString()
                    });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    InitData();
                    btnClear.PerformClick();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.EditValue = "";
            txtKg.EditValue = "";
            txtQuantity.EditValue = "";
            txtUser.EditValue = "";
            txtAmount.EditValue = "";
            txtPriceEnd.EditValue = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    if (txtID.EditValue.NullString() == "")
                    {
                        MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                        return;
                    }

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@DELETE_INOUT_UTILITY",
                        new string[] { "A_ID" },
                        new string[]
                        {
                        txtID.EditValue.NullString()
                        });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                        InitData();
                        btnClear.PerformClick();
                    }
                    else
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                string time = "";
               
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@SUM_INOUT_UTILITY", new string[] { "A_MONTH" }, new string[] { time });
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection tableCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridView(gcListSum, tableCollection[0]);
                    gvListSum.OptionsView.ColumnAutoWidth = true;
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
                if (e.RowHandle < 0)
                    return;

                txtID.EditValue = gvList.GetRowCellValue(e.RowHandle, "Id");
                txtQuantity.EditValue = gvList.GetRowCellValue(e.RowHandle, "QTY_EA");
                txtKg.EditValue = gvList.GetRowCellValue(e.RowHandle, "QTY_KG");
                txtAmount.EditValue = gvList.GetRowCellValue(e.RowHandle, "PRICE");
                dateMonth.EditValue = gvList.GetRowCellValue(e.RowHandle, "TIME_PUT");
                cboInOut.EditValue = gvList.GetRowCellValue(e.RowHandle, "IN_OUT_REMAIN");

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnSaveEnd_Click(object sender, EventArgs e)
        {
            try
            {
                if(dateMonthEnd.EditValue.NullString() == "" || cboInOutEnd.EditValue.NullString() == "" || txtPriceEnd.EditValue.NullString() == "")
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@PUT_INOUT_UTILITY_END", 
                    new string[] { "A_DATE", "A_IN_OUT", "A_PRICE" },
                    new string[] { dateMonthEnd.EditValue.NullString(),cboInOutEnd.EditValue.NullString(), txtPriceEnd.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    InitData();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_BUSINESS@SEARCH_UTILITY", 
                    new string[] { "A_DATE" }, 
                    new string[] { dateSearch.EditValue.NullString() });

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTableCollection tableCollection = base.m_ResultDB.ReturnDataSet.Tables;
                    m_BindData.BindGridView(gcList, tableCollection[0]);
                    gvListSum.OptionsView.ColumnAutoWidth = true;
                    if (gvList.Columns.Count > 0)
                        gvList.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        /// <summary>
        /// Reload data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xSimpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            InitData();
            splashScreenManager1.CloseWaitForm();
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Consts.mainForm.NewPageFromOtherPage("CHART_UTILITY", "Biểu đồ", "W", "Y", null);
            splashScreenManager1.CloseWaitForm();
        }
    }
}
