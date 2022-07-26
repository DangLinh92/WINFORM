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
    public partial class REPORT020 : PageType
    {

        public REPORT020()
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
            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT020.INT_LIST"
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
            dtpFrom.EditValue = x.ToString("yyyy-MM-dd 08:00:00");
            dtpFrom.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

            dtpTo.EditValue = x.AddMonths(1).ToString("yyyy-MM-dd 08:00:00");// DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dtpTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";

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
            string fromP = dtpFrom.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            string toP = dtpTo.DateTime.ToString("yyyy-MM-dd HH:mm:ss.000");
            string year = dtpFrom.DateTime.Year.ToString();
            string month = dtpFrom.DateTime.Month.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT020.GET_LIST",
                    new string[]
                    {
                            "A_FROM",
                            "A_TO",
                            "A_YEAR",
                            "A_MONTH"
                    },
                    new string[]
                    {
                            fromP,
                            toP,
                            year,
                            month
                    }
                );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList, base.m_ResultDB.ReturnDataSet.Tables[0]);
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }

            gvList.Columns["Total_Pickup"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["Total_Pickup"].DisplayFormat.FormatString = "n0";
            gvList.Columns["Total_Loss"].DisplayFormat.FormatType = FormatType.Numeric;
            gvList.Columns["Total_Loss"].DisplayFormat.FormatString = "n0";
        }

    }
}
