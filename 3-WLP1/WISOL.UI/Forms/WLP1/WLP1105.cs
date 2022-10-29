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
    public partial class WLP1105 : PageType
    {
        public WLP1105()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.INT_LIST"
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
                    base.m_BindData.BindGridView(gcList2,
                        base.m_ResultDB.ReturnDataSet.Tables[1]
                        );
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            base.InitializePage();

        }

        public override void SearchPage()
        {
            gcList2.DataSource = null;
            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.GET_LIST"
                , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"},
                  new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                      base.m_ResultDB.ReturnDataSet.Tables[0]
                      );

                    //for (int i = 0; i < gvList.DataRowCount; i++)
                    //{
                    //    int so_luong = Convert.ToInt32(gvList.GetDataRow(i)["QUANTITY"].ToString());
                    //    string dinh_luong = gvList.GetDataRow(i)["QUANTITATIVE"].ToString();
                    //    double price_usd = Convert.ToDouble(gvList.GetDataRow(i)["PRICE_USD"].ToString());
                    //    string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                    //    string v2 = dinh_luong.Substring(dinh_luong.IndexOf(' ') + 1);
                    //    double value = Convert.ToDouble(v1);

                    //    gvList.SetRowCellValue(i, "STOCK", (so_luong*value) + " " + v2) ;
                    //    gvList.SetRowCellValue(i, "TOTAL_MONEY_USD", so_luong*value*price_usd);
                    //    //if (gvList.GetDataRow(i)["Judgment"].ToString().ToUpper().Trim() != "OK")
                    //    //{
                    //    //    if (String.IsNullOrWhiteSpace(gvList.GetDataRow(i)["NG_Reason"].ToString().Trim()))
                    //    //    {
                    //    //        txtStatus.Text = "NG";
                    //    //        txtStatus.BackColor = Color.FromArgb(255, 199, 206);
                    //    //        break;
                    //    //    }
                    //    //}
                    //}
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }

                gvList.Columns["QUANTITY"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                gvList.Columns["QUANTITY"].Width = 110;
                gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatString = "n0";
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

        }


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.AbsoluteIndex == 7 )
            //{
            //    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
            //    if (!string.IsNullOrWhiteSpace(cellValue))
            //    {
            //        if(cellValue.ToUpper() == "ĐANG SẢN XUẤT")
            //        {
            //            e.Appearance.BackColor = Color.Lime;
            //        }
            //        if (cellValue.ToUpper() == "CẦN HỦY")
            //        {
            //            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
            //        }
            //    }
            //}
        }

        private void gvList_RowClick(object sender, RowClickEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                string code = gvList.GetRowCellDisplayText(e.RowHandle, "CODE");

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1105.GET_LIST_DETAIL"
                    , new string[] { "A_CODE", "A_DEPARTMENT" },
                      new string[] { code, Consts.DEPARTMENT }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        base.m_BindData.BindGridView(gcList2,
                          base.m_ResultDB.ReturnDataSet.Tables[0]
                          );
                        gvList2.Columns["QUANTITY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList2.Columns["QUANTITY"].DisplayFormat.FormatString = "n0";
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
