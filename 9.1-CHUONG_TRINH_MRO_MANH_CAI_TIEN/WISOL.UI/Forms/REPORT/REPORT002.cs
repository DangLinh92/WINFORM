using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT002 : PageType
    {

        public REPORT002()
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
            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT002.INT_LIST"
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

            DateTime x = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFrom.EditValue = x.ToString("yyyy-MM-dd");
            //dtpFrom.EditValue = x.ToString("yyyy-MM-dd 08:00:00");
            //dtpFrom.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            dtpTo.EditValue = x.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            //dtpTo.EditValue = x.AddMonths(1).ToString("yyyy-MM-dd 08:00:00");// DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //dtpTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            base.InitializePage();
        }

        public override void SearchPage()
        {
            if (Math.Floor((dtpTo.DateTime - dtpFrom.DateTime).TotalDays) > 31)
            {
                MsgBox.Show("Khoảng thời gian tối đa 31 ngày. \r\n\r\n Time range is max to 31 days.", MsgType.Warning);
                return;
            }
            base.SearchPage();
            //string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            //string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            string fromP = dtpFrom.DateTime.ToString("yyyyMMdd");
            string toP = dtpTo.DateTime.AddDays(1).ToString("yyyyMMdd");
            //string year = dtpFrom.DateTime.Year.ToString();
            //string month = dtpFrom.DateTime.Month.ToString();
            //if (month.Length == 1)
            //{
            //    month = "0" + month;
            //}

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT002.GET_LIST",
                    new string[]
                    {       "A_LANG",
                            "A_FROM",
                            "A_TO",
                            "A_DEPARTMENT"
                    },
                    new string[]
                    {       Consts.USER_INFO.Language,
                            fromP,
                            toP,
                            Consts.DEPARTMENT
                    }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {

                    DataTable dt = new DataTable();

                    dt = base.m_ResultDB.ReturnDataSet.Tables[0].Copy();
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        int so_luong = Convert.ToInt32(dt.Rows[i]["USE"].ToString()); //Convert.ToInt32(gvList.GetDataRow(i)["USE"].ToString());
                        double price_usd = Convert.ToDouble(dt.Rows[i]["PRICE_USD"].ToString());
                        string dinh_luong = dt.Rows[i]["QUANTITATIVE"].ToString();
                        string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                        string v2 = dinh_luong.Substring(dinh_luong.IndexOf(' ') + 1);
                        double value = Convert.ToDouble(v1);
                        dt.Rows[i]["CONSUME"] = (so_luong * value) + " " + v2;
                        dt.Rows[i]["TOTAL_MONEY_USD"] = so_luong * value * price_usd;
                    }

                    base.m_BindData.BindGridView(gcList, dt);

                    //base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);

                    //for (int i = 0; i < gvList.DataRowCount; i++)
                    //{
                    //    int so_luong = Convert.ToInt32(gvList.GetDataRow(i)["USE"].ToString());
                    //    double price_usd = Convert.ToDouble(gvList.GetDataRow(i)["PRICE_USD"].ToString());
                    //    string dinh_luong = gvList.GetDataRow(i)["QUANTITATIVE"].ToString();
                    //    string v1 = dinh_luong.Substring(0, dinh_luong.IndexOf(' '));
                    //    string v2 = dinh_luong.Substring(dinh_luong.IndexOf(' ') + 1);
                    //    double value = Convert.ToDouble(v1);

                    //    gvList.SetRowCellValue(i, "CONSUME", (so_luong * value) + " " + v2);
                    //    gvList.SetRowCellValue(i, "TOTAL_MONEY_USD", so_luong * value * price_usd);

                    //    gvList.Columns["TOTAL_MONEY_USD"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    //}
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }

            gvList.BeginSort();
            try
            {
                gvList.ClearSorting();
                gvList.Columns["TOTAL_MONEY_USD"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
            finally
            {
                gvList.EndSort();
            }

            gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["TOTAL_MONEY_USD"].DisplayFormat.FormatString = "n0";
            //gvList.Columns["Total_Pickup"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvList.Columns["Total_Pickup"].DisplayFormat.FormatString = "n0";
            //gvList.Columns["Total_Loss"].DisplayFormat.FormatType = FormatType.Numeric;
            //gvList.Columns["Total_Loss"].DisplayFormat.FormatString = "n0";
        }

    }
}
