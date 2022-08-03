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
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1101 : PageType
    {
        public WLP1101()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            chkPrintLabel.Checked = true;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1101.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                       base.m_ResultDB.ReturnDataSet.Tables[0]
                       );

                    base.m_BindData.BindGridLookEdit(gleCategory, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME");

                    //base.m_BindData.BindGridLookEdit(gleMaker, base.m_ResultDB.ReturnDataSet.Tables[0], "MAKER", "MAKER_NAME");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();
        }


        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_WLP1101.GET_LIST_TEMP",
                    new string[] { "A_LANG", "A_FROM_DATE", "A_TO_DATE", "A_DEPARTMENT" },
                    new string[] { Consts.USER_INFO.Language,  dtpFromDate.DateTime.ToString("yyyyMMdd") , dtpToDate.DateTime.ToString("yyyyMMdd"), Consts.DEPARTMENT}
                    ); // Manh sua doan nay , de show tong so tien ( thay doi ten thu tuc.

                //gvList.OptionsView.ShowFooter = false;
                gvList.OptionsView.ShowFooter = true;
                gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[3].DisplayFormat.FormatString = "n3";
                // Mạnh ẩn 2 cột này đi, nếu là phòng ban WLP1
                if (Consts.DEPARTMENT == "WLP1") 
                {
                    gvList.Columns[5].Visible = false;
                    gvList.Columns[6].Visible = false;
                }
                //*********************************************
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //for (int i = 9; i < gvList.Columns.Count - 2; i++)
            //{
            //    gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    gvList.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //}
            //gvList.OptionsView.ShowFooter = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gleCategory.EditValue.NullString() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_116".Translation(), MsgType.Warning);
                return;
            }
            if (txtLotQuantity.EditValue.NullString() == "0" || txtLotQuantity.EditValue.NullString().Contains("."))
            {
                MsgBox.Show("SỐ LƯỢNG ĐƠN VỊ NHẬP không hợp lệ".Translation(), MsgType.Warning);
                return;
            }
            if (txtQuantityPerStock.EditValue.NullString() == "0")
            {
                MsgBox.Show("SỐ LƯỢNG CỦA 1 ĐƠN VỊ NHẬP không hợp lệ".Translation(), MsgType.Warning);
                return;
            }
            //if (txtMakeDate.EditValue.NullString() == string.Empty)
            //{
            //    MsgBox.Show("MSG_ERR_117".Translation(), MsgType.Warning);
            //    return;
            //}
            //if (txtExpDate.EditValue.NullString() == string.Empty)
            //{
            //    MsgBox.Show("MSG_ERR_118".Translation(), MsgType.Warning);
            //    return;
            //}
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1101.PUT_ITEM"
                    , new string[] {
                        "A_CODE",
                        "A_LOT_QUANTITY",
                        "A_QUANTITY_PER_STOCK",
                        "A_MAKE_DATE",
                        "A_EXP_DATE",
                        "A_TRAN_USER",
                        "A_DEPARTMENT"
                    }
                    , new string[] {
                        gleCategory.EditValue.NullString(),
                        txtLotQuantity.EditValue.NullString(),
                        txtQuantityPerStock.EditValue.NullString(),
                        txtMakeDate.DateTime.ToString("yyyyMMdd"),
                        txtExpDate.DateTime.ToString("yyyyMMdd"),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    gleCategory.EditValue = null;
                    txtLotQuantity.Text = "0";
                    txtQuantityPerStock.Text = "0";
                    txtMakeDate.EditValue = null;
                    txtExpDate.EditValue = null;
                    SearchPage();
                    if (chkPrintLabel.Checked)
                    {
                        DataTable dtPrint = new DataTable();
                        dtPrint = base.m_ResultDB.ReturnDataSet.Tables[0];
                        UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
                        print.PrintTest(0, 0, dtPrint);
                    }
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
            //    string code = gvList.GetDataRow(indexRow)["CODE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string quantity = gvList.GetDataRow(indexRow)["SO_LUONG"].ToString().Trim();
            //    string make_date = gvList.GetDataRow(indexRow)["NGAY_SAN_XUAT"].ToString().Trim();
            //    string exp_date = gvList.GetDataRow(indexRow)["NGAY_HET_HAN"].ToString().Trim();
            //    gleCategory.EditValue = code;
            //    txtLot.Text = lot;
            //    txtQuanity.EditValue = quantity;
            //    txtMakeDate.EditValue = make_date;
            //    txtExpDate.EditValue = exp_date;
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "NGAY_HET_HAN" )
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        DateTime exp_date = DateTime.Parse(cellValue);
            //        DateTime current_date = DateTime.Now.Date;
            //        double count = (exp_date - current_date).TotalDays;
            //        if(count <= 3 && count > 0)
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //        }
            //        if(count <= 0)
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //    }
            //}
        }

        private void gleCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(gleCategory.EditValue.NullString()))
            {
                string code = gleCategory.EditValue.NullString();

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1101.GET_UNIT"
                        , new string[] {
                        "A_CODE",
                        "A_DEPARTMENT"
                        }
                        , new string[] {
                        gleCategory.EditValue.NullString(),
                        Consts.DEPARTMENT
                        }
                        );

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        string unit_stock_in = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                        string unit = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][1].ToString();
                        if ((unit_stock_in == "PCE" && unit == "PCE") || (unit_stock_in == "ROL" && unit == "ROL") || (unit_stock_in == "TIP" && unit == "TIP") || (unit_stock_in == "PAIL" && unit == "PAIL") || (unit_stock_in == "BOX" && unit == "BOX"))
                        {
                            txtQuantityPerStock.EditValue = 1;
                            txtQuantityPerStock.ReadOnly = true;
                        }
                        else
                        {
                            txtQuantityPerStock.EditValue = 0;
                            txtQuantityPerStock.ReadOnly = false;
                        }
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
        }
    }
}
