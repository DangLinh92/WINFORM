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


namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING004 : PageType
    {
        public SETTING004()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlGroup6.Enabled = false;
            this.gcList.Enabled = false;
            //this.layoutControlGroup2.Enabled = false;
            this.layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (Consts.USER_INFO.Id.ToUpper() != "H2002001")
            {
                this.layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            this.layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            Classes.Common.SetFormIdToButton(this, "SETTING004");
        }

        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();

            //try
            //{

            //}
            //catch (Exception ex)
            //{

            //}

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtLotNo.Text.Trim() == string.Empty)
            {
                return;
            }
            ////////////////////////////////   2020-08-05 COMMENT TO PRINT ULTILITY
            //try
            //{
            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.GET_LIST"
            //        , new string[] { "A_LOT_NO", "A_TRAN_USER"
            //        }
            //        , new string[] { txtLotNo.Text.Trim(), Consts.USER_INFO.Id
            //        }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        DataTable dtPrint = new DataTable();
            //        dtPrint = base.m_ResultDB.ReturnDataSet.Tables[0];
            //        UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
            //        print.PrintTest(0, 0, dtPrint);
            //        txtLotNo.Text = string.Empty;
            //    }
            //    else
            //    {
            //        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show(ex.Message, MsgType.Error);
            //}

            ///////////////////

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.GET_DEVICE"
                    , new string[] { "A_PLANT","A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, txtLotNo.Text.Trim()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    if(base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count < 1)
                    {
                        MsgBox.Show("MSG_ERR_057".Translation(), MsgType.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }


            UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
            print.PrintUTI(0, 0, txtLotNo.Text.Trim());
            txtLotNo.Text = "";
        }

        private void btnINbinhcuuhoa_Click(object sender, EventArgs e)
        {

            if (txtBCH.Text.Trim() == string.Empty)
            {
                return;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.GET_BCH"
                    , new string[] { "A_PLANT","A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_CODE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, txtBCH.Text.Trim()
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count < 1)
                    {
                        MsgBox.Show("MSG_ERR_057".Translation(), MsgType.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
            print.PrintBCH(0, 0, txtBCH.Text.Trim());
            txtBCH.Text = "";
        }

        private void btnINbinhcuuhoa1_Click(object sender, EventArgs e)
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING004.GET_PRINT_ALL"
                    , new string[] { "A_PLANT"
                    }
                    , new string[] { Consts.PLANT
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    UserClass.PrintLabel print = new UserClass.PrintLabel(base.m_DBaccess);
                    DataTable dt = new DataTable();
                    dt = base.m_ResultDB.ReturnDataSet.Tables[0].Copy();
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        print.PrintUTI(0, 0, dt.Rows[i][0].ToString());
                    }
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Warning);
            }
        }
    }
}
