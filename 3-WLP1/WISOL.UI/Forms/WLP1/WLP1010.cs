using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;


namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1010 : PageType
    {
        public WLP1010()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            //this.layoutControlGroup6.Enabled = false;
            //this.gcList.Enabled = false;
            //this.layoutControlGroup2.Enabled = false;
            dtpFromDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");

            txtCode.Enabled = false;
            txtQtyMove.Enabled = false;
            txtLotNo.Enabled = true;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1010.GET_LIST"
                    , new string[] { "A_FROM_DATE", "A_TO_DATE"
                    }
                    , new string[] { dtpFromDate.DateTime.ToString("yyyyMMdd"), dtpToDate.DateTime.ToString("yyyyMMdd")
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                      base.m_ResultDB.ReturnDataSet.Tables[0]
                      );
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


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chCode.Checked)
            {
                if (txtCode.Text.Trim() == string.Empty || txtQtyMove.Text.NullString() == "")
                {
                    return;
                }

                try
                {
                    base.m_ResultDB = 
                        base.m_DBaccess.ExcuteProc("PKG_WLP1010.PUT_ITEM_BY_CODE"
                        , new string[] { "A_CODE", "A_QTY","A_USER"}
                        , new string[] { txtCode.Text.Trim(),txtQtyMove.Text.NullString(), Consts.USER_INFO.Id });

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        if (chePrintLabel.Checked)
                        {
                            DataTable dtPrint = new DataTable();
                            dtPrint = base.m_ResultDB.ReturnDataSet.Tables[0];
                            UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
                            print.PrintTest(0, 0, dtPrint);
                        }

                        this.SearchPage();
                        txtCode.Text = string.Empty;
                        txtQtyMove.Text = "";
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
            else
            {
                if (txtLotNo.Text.Trim() == string.Empty)
                {
                    return;
                }
                //////////////////////////////   2020-08-05 COMMENT TO PRINT ULTILITY
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1010.PUT_ITEM"
                        , new string[] { "A_LOT_NO", "A_TRAN_USER"
                        }
                        , new string[] { txtLotNo.Text.Trim(), Consts.USER_INFO.Id});

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        if (chePrintLabel.Checked)
                        {
                            DataTable dtPrint = new DataTable();
                            dtPrint = base.m_ResultDB.ReturnDataSet.Tables[0];
                            UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
                            print.PrintTest(0, 0, dtPrint);
                        }

                        this.SearchPage();
                        txtLotNo.Text = string.Empty;
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

        private void chCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chCode.Checked)
            {
                txtCode.Enabled = true;
                txtQtyMove.Enabled = true;
                txtLotNo.Enabled = false;
            }
            else
            {
                txtCode.Enabled = false;
                txtQtyMove.Enabled = false;
                txtLotNo.Enabled = true;
            }
        }
    }
}
