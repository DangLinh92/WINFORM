using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1007 : PageType
    {
        public WLP1007()
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
            DateTime x = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFrom.EditValue = x.ToString("yyyy-MM-dd");
            dtpTo.EditValue = x.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            if (dtpFrom.DateTime.ToString("yyyyMMdd") == dtpTo.DateTime.ToString("yyyyMMdd"))
            {
                dtpFrom.EditValue = dtpFrom.DateTime.AddMonths(-1);
            }
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1007.INT_LIST"
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

            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            string todate = dtpTo.DateTime.ToString("yyyyMMdd");
            //if(dtpFrom.DateTime.ToString("yyyyMMdd") == dtpTo.DateTime.ToString("yyyyMMdd"))
            //{
            //    todate = dtpTo.DateTime.AddDays(1).ToString("yyyyMMdd");
            //}
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1007.GET_LIST",
                    new string[] { "A_PLANT", "A_LANG", "A_FROM", "A_TO", "A_DEPARTMENT" },
                    new string[] { Consts.PLANT, Consts.USER_INFO.Language,
                        dtpFrom.DateTime.ToString("yyyyMMdd"), 
                        todate,
                        Consts.DEPARTMENT}
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
            //gvList.Columns[0].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[2].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[3].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[4].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[5].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[6].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[7].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //gvList.Columns[8].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[2].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[9].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[10].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[11].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[12].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[13].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[14].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[15].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[16].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[17].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[18].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[19].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[19].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[20].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[20].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[21].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[21].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[22].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[22].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.Columns[23].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gvList.Columns[23].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //gvList.BestFitColumns();

            //gvList.Columns["MODEL"].OptionsColumn.AllowMerge = DefaultBoolean.True;
            gvList.OptionsView.ShowFooter = false;
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
