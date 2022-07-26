﻿using DevExpress.Utils;
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
    public partial class WLP1103 : PageType
    {
        public WLP1103()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1103.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT
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
            dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");

            //if(Consts.DEPARTMENT == "CSP" || Consts.DEPARTMENT == "LFEM")
            //{
            //    layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //else
            //{
            //    layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
            base.InitializePage();
        }


        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_WLP1103.GET_LIST_TEMP",
                    new string[] { "A_FROM_DATE", "A_TO_DATE", "A_DEPARTMENT", "A_LANG" },
                    new string[] {  dtpFromDate.DateTime.ToString("yyyyMMdd") , dtpToDate.DateTime.ToString("yyyyMMdd"), Consts.DEPARTMENT, Consts.USER_INFO.Language}
                    );
                //gvList.OptionsView.ShowFooter = false;
                gvList.OptionsView.ShowFooter = true;
                gvList.Columns[" QUANTITY "].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns[" QUANTITY "].DisplayFormat.FormatString = "n3";
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
            if(txtLotNo.EditValue.NullString() == string.Empty)
            {
                return;
            }

            if(Consts.DEPARTMENT == "CSP" && spinQuantity.EditValue.ToString() == "0")
            {
                MsgBox.Show("Hãy nhập số lượng xuất kho", MsgType.Warning);
                return;
            }
            if (Consts.DEPARTMENT == "WLP2" && spinQuantity.EditValue.ToString() == "0")
            {
                MsgBox.Show("Hãy nhập số lượng xuất kho", MsgType.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtNguoiThaoTac.EditValue.NullString()))
            {
                MsgBox.Show("Hãy nhập người thao tác xuất kho", MsgType.Warning);
                return;
            }

            try
            {
                if (Consts.DEPARTMENT == "WLP2")
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1103.PUT_ITEM"
                        , new string[] {
                        "A_LOT_NO","A_TRAN_USER", "A_DEPARTMENT", "A_NGUOI_THAO_TAC", "A_QUANTITY"  
                        }
                        , new string[] {
                        txtLotNo.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT,
                        txtNguoiThaoTac.EditValue.NullString(),
                        spinQuantity.EditValue.ToString()
                        }
                        );
                }
                else if (Consts.DEPARTMENT == "CSP" || Consts.DEPARTMENT == "LFEM" || Consts.DEPARTMENT == "WLP1")
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1103.PUT_ITEM_QUANTITY"
                        , new string[] {
                        "A_LOT_NO","A_TRAN_USER", "A_DEPARTMENT", "A_NGUOI_THAO_TAC", "A_QUANTITY"
                        }
                        , new string[] {
                        txtLotNo.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT,
                        txtNguoiThaoTac.EditValue.NullString(),
                        spinQuantity.EditValue.ToString()
                        }
                        );
                }


                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtLotNo.EditValue = null;
                    spinQuantity.EditValue = "0";
                    spinQuantity.ReadOnly = false;
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

        private void txtLotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Consts.DEPARTMENT == "CSP" || Consts.DEPARTMENT == "WLP1")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtLotNo.Text.Trim() == string.Empty)
                    {
                        return;
                    }
                    else
                    {

                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1103.GET_LOT_INFO"
                            , new string[] {
                        "A_LOT_NO","A_TRAN_USER", "A_DEPARTMENT"
                                }
                                , new string[] {
                        txtLotNo.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                            }
                            );


                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            string unit_stock_in = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["UNIT_STOCK_IN"].ToString();
                            string unit = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["UNIT"].ToString();

                            if (unit_stock_in == "PAIL")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else if (unit_stock_in == "PCE" && unit == "PCE")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else if (unit_stock_in == "ROL" && unit == "ROL")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else if (unit_stock_in == "BOX" && unit == "BOX")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else
                            {
                                spinQuantity.EditValue = "0";
                                spinQuantity.ReadOnly = false;
                            }
                        }
                        else
                        {
                            MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                        }
                    }
                }
            }

            if (Consts.DEPARTMENT == "WLP2")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtLotNo.Text.Trim() == string.Empty)
                    {
                        return;
                    }
                    else
                    {

                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1103.GET_LOT_INFO"
                            , new string[] {
                        "A_LOT_NO","A_TRAN_USER", "A_DEPARTMENT"
                                }
                                , new string[] {
                        txtLotNo.Text.Trim().ToUpper(),
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                            }
                            );


                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            string unit_stock_in = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["UNIT_STOCK_IN"].ToString();
                            string unit = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["UNIT"].ToString();
                            string quantity = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["QUANTITY"].ToString();

                            if(unit_stock_in == "BOX" && unit == "PCE")
                            {
                                spinQuantity.EditValue = "0";
                                spinQuantity.ReadOnly = false;
                            }
                            else if (unit_stock_in == "PCE" && unit == "PCE")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else if (unit_stock_in == "ROL" && unit == "ROL")
                            {
                                spinQuantity.EditValue = "1";
                                spinQuantity.ReadOnly = true;
                            }
                            else if(unit == "M")
                            {
                                spinQuantity.EditValue = quantity;
                                spinQuantity.ReadOnly = true;
                            }
                            else
                            {
                                spinQuantity.EditValue = "0";
                                spinQuantity.ReadOnly = false;
                            }
                        }
                        else
                        {
                            MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                        }
                    }
                }
            }
        }
    }
}
