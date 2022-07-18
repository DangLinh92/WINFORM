using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;
using DevExpress.XtraCharts;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Calendar;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Spreadsheet;
using System.Globalization;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Views.Grid;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT006 : PageType
    {
        public REPORT006()
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
            //try
            //{
            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT006.INT_LIST"
            //        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
            //        }
            //        , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
            //        }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        base.m_BindData.BindGridView(gcList,
            //            base.m_ResultDB.ReturnDataSet.Tables[0]
            //            );

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}
            this.SearchPage();
            base.InitializePage();
        }

        public override void SearchPage()
        {
            DataTable dt = new DataTable();
            base.SearchPage();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT006.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i]["LAST_MAINTENANCE_TIME"].ToString()))
                        {
                            int day = 0;
                            DateTime dtTime = DateTime.Parse(dt.Rows[i]["LAST_MAINTENANCE_TIME"].ToString());
                            if (!string.IsNullOrEmpty(dt.Rows[i]["MAINTENANCE_TIME_DAY"].ToString()))
                            {
                                day = Convert.ToInt32(dt.Rows[i]["MAINTENANCE_TIME_DAY"].ToString());
                                dtTime = dtTime.AddDays(day);
                                dt.Rows[i].SetField("NEXT_MAINTENANCE", dtTime.ToString("yyyy-MM-dd"));
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[i]["MAINTENANCE_TIME_HOURS"].ToString()))
                            {
                                int hour = Convert.ToInt32(dt.Rows[i]["MAINTENANCE_TIME_HOURS"].ToString());
                                day = Convert.ToInt32(Math.Floor(hour*1.0/24));
                                dtTime = dtTime.AddDays(day);
                                dt.Rows[i].SetField("NEXT_MAINTENANCE", dtTime.ToString("yyyy-MM-dd"));
                            }
                        }
                        else
                        {
                            DateTime dtTimeTemp = DateTime.Today.AddDays(-1200);
                            int day2 = 0;
                            dt.Rows[i].SetField("LAST_MAINTENANCE_TIME", dtTimeTemp.ToString("yyyy-MM-dd"));
                            if (!string.IsNullOrEmpty(dt.Rows[i]["MAINTENANCE_TIME_DAY"].ToString()))
                            {
                                day2 = Convert.ToInt32(dt.Rows[i]["MAINTENANCE_TIME_DAY"].ToString());
                                dtTimeTemp = dtTimeTemp.AddDays(day2);
                                dt.Rows[i].SetField("NEXT_MAINTENANCE", dtTimeTemp.ToString("yyyy-MM-dd"));
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["MAINTENANCE_TIME_HOURS"].ToString()))
                            {
                                int hour2 = Convert.ToInt32(dt.Rows[i]["MAINTENANCE_TIME_HOURS"].ToString());
                                day2 = Convert.ToInt32(Math.Floor(hour2 * 1.0 / 24));
                                dtTimeTemp = dtTimeTemp.AddDays(day2);
                                dt.Rows[i].SetField("NEXT_MAINTENANCE", dtTimeTemp.ToString("yyyy-MM-dd"));
                            }
                        }
                    }

                    base.m_BindData.BindGridView(gcList,
                        dt
                        );
                    gvList.OptionsView.ShowFooter = false;

                    gvList.BeginSort();
                    try
                    {
                        gvList.ClearSorting();
                        gvList.Columns["NEXT_MAINTENANCE"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                    }
                    finally
                    {
                        gvList.EndSort();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            //var gridView = sender as GridView;
            //int row_index = gridView.FocusedRowHandle;

            //string deviceId = gvList.GetRowCellDisplayText(row_index, "DEVICE_ID");

            //POP.POP_REPORT006 popup = new POP.POP_REPORT006(deviceId);
            //popup.ShowDialog();
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "NEXT_MAINTENANCE")
                {
                    string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                    if (String.Compare(cellValue, DateTime.Today.ToString("yyyy-MM-dd")) <= 0)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
        }
    }
}
