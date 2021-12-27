using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT012 : PageType
    {
        public SMT012()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            txtLot.ReadOnly = true;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT012.INT_LIST"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT,
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {

                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            dtpFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();

        }

        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT012.GET_LIST",
                    new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] {  dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000", dtpToDate.DateTime.ToString("yyyyMMdd") + "235959"}
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //gvList.Columns[7].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkFlux.Checked)
            {
                if (txtLot.Text.Trim() == string.Empty)
                {
                    MsgBox.Show("MSG_ERR_022".Translation(), MsgType.Warning);
                    return;
                }
            }
            if (!chkFlux.Checked)
            {
                if (txtLotMes.Text.Trim() == string.Empty)
                {
                    MsgBox.Show("MSG_ERR_028".Translation(), MsgType.Warning);
                    return;
                }
            }

            string isFlux = chkFlux.Checked.ToString();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT012.PUT_ITEM"
                    , new string[] {
                        "A_CHECK_FLUX",
                        "A_LOT",
                        "A_LOT_MES",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        isFlux,
                        txtLot.Text.Trim().ToUpper(),
                        txtLotMes.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtLotMes.Text = string.Empty;
                    SearchPage();
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            //int indexRow = gvList.FocusedRowHandle;
            //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle && gvList.RowCount > 0)
            //{
            //    string line = gvList.GetDataRow(indexRow)["LINE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string lot_mes = gvList.GetDataRow(indexRow)["LOT_MES"].ToString().Trim();
            //    string exp_time = gvList.GetDataRow(indexRow)["HAN_SU_DUNG"].ToString().Trim();

            //    txtLotMes.Text = lot_mes;
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "NGAY_HET_HAN" )
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    DateTime exp_date = DateTime.Parse(cellValue);
                    DateTime current_date = DateTime.Now.Date;
                    double count = (current_date - exp_date).TotalDays;
                    if(count <= 7)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }
            }
        }


        private void chkFlux_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFlux.Checked)
            {
                txtLot.ReadOnly = false;
                txtLotMes.ReadOnly = true;
                txtLotMes.Text = string.Empty;
            }
            else
            {
                txtLot.ReadOnly = true;
                txtLot.Text = string.Empty;
                txtLotMes.ReadOnly = false;
            }
        }
    }
}
