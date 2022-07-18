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
    public partial class SMT013 : PageType
    {
        public SMT013()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT013.INT_LIST"
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
                    "PKG_SMT013.GET_LIST",
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


        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string hsd = string.Empty;
            string tgh = string.Empty;
             
            if (e.Column.AbsoluteIndex == 7 )
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if(cellValue.ToUpper() == "ĐANG SẢN XUẤT")
                    {
                        e.Appearance.BackColor = Color.Lime;
                    }
                    if (cellValue.ToUpper() == "CẦN HỦY")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                    //DateTime exp_date = DateTime.Parse(cellValue);
                    //DateTime current_date = DateTime.Now.Date;
                    //double count = (current_date - exp_date).TotalDays;
                    //if(count <= 7)
                    //{
                    //    e.Appearance.BackColor = Color.Yellow;
                    //}
                }
            }
            //if (e.RowHandle >= 0)
            //{
                //if (e.Column.AbsoluteIndex == 5)
                //{
                //    hsd = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                //}
                if (e.Column.AbsoluteIndex == 6)
                {
                    hsd = gvList.GetRowCellDisplayText(e.RowHandle, gvList.Columns[5]);
                    tgh = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                    if (!string.IsNullOrWhiteSpace(tgh))
                    {
                        DateTime d1 = DateTime.Parse(hsd);
                        DateTime d2 = DateTime.Parse(tgh);

                        if ((d2 - d1).TotalMinutes > 0 && (d2 - d1).TotalMinutes < 60)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                        }
                        if ((d2 - d1).TotalMinutes > 60)
                        {
                            e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                        }
                    }
                }
            //}
        }

    }
}
