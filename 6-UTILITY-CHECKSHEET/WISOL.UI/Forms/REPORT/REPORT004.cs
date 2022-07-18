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
using System.Drawing.Imaging;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT004 : PageType
    {
        public REPORT004()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
            this.layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            dtpFrom.EditValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
        }


        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT004.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language
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

        }

        public override void SearchPage()
        {

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT004.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     dtpFrom.DateTime.ToString("yyyy-MM-dd") + " 08:00:00.000",
                                     dtpTo.DateTime.ToString("yyyy-MM-dd") + " 08:00:00.000"
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    gvList.OptionsView.ShowFooter = false;
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


            gvList.OptionsView.ShowFooter = false;
            //gvList.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[1].DisplayFormat.FormatString = "n0";
            //gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[2].DisplayFormat.FormatString = "n0";
            //gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[4].DisplayFormat.FormatString = "n0";
            //gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[5].DisplayFormat.FormatString = "n0";
            //gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[6].DisplayFormat.FormatString = "n0";
            //gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[7].DisplayFormat.FormatString = "n0";
            //gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[8].DisplayFormat.FormatString = "n0";
            //gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[9].DisplayFormat.FormatString = "n0";
            //gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[10].DisplayFormat.FormatString = "n0";
            //gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[11].DisplayFormat.FormatString = "n0";
            //gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[12].DisplayFormat.FormatString = "n0";
            //gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[13].DisplayFormat.FormatString = "n0";
            //gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[14].DisplayFormat.FormatString = "n0";
            //gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[15].DisplayFormat.FormatString = "n0";
            //gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[16].DisplayFormat.FormatString = "n0";
            //gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[17].DisplayFormat.FormatString = "n0";
            //gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[18].DisplayFormat.FormatString = "n0";


        }

        
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            
        }

        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            var gridView = sender as GridView;
            int row_index = gridView.FocusedRowHandle;

            string c_code = gvList.GetRowCellDisplayText(row_index, "DEVICE_CODE");
            string c_name = gvList.GetRowCellDisplayText(row_index, "DEVICE_NAME");
            string c_item_name = gvList.GetRowCellDisplayText(row_index, "ITEM_CHECK");
            string c_item_id = gvList.GetRowCellDisplayText(row_index, "ITEM_CHECK_ID");
            string previous_date = dtpFrom.DateTime.ToString("MM-dd").ToString();
            string current_date = dtpTo.DateTime.ToString("MM-dd").ToString();
            POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date);
            popup.ShowDialog();
        }
    }
}
