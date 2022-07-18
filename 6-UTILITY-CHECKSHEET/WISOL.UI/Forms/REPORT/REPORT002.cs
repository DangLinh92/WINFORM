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
            string pdate  = dtpMonth.DateTime.ToString("yyyy-MM-01");
            string ndate = dtpMonth.DateTime.AddMonths(1).ToString("yyyy-MM-01");
            chkChecked.Checked = true;

            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT002.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_PDATE", "A_NDATE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, pdate, ndate
                    }
                    );
            if (base.m_ResultDB.ReturnInt == 0)
            {
                base.m_BindData.BindGridView(gcList,
                    base.m_ResultDB.ReturnDataSet.Tables[0]
                    );
            }

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns["ID"].Visible = false;
            int c = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
            int u = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString());
            int ng = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[3].Rows[0][0].ToString());
            lblReport.Text = "AMOUNT: " + c + " / CHECKED: " + u + " / UNCHECKED: " + (c-u) + " / ALL NG ITEM: " + ng;
            lblReport.Font = new Font("Tahoma", 8.75f, FontStyle.Bold);

            base.InitializePage();
        }

        public override void SearchPage()
        {
            string shift = string.Empty;
            string status = string.Empty;
            string showNG = string.Empty;
            string pdate = dtpMonth.DateTime.ToString("yyyy-MM-01");
            string ndate = dtpMonth.DateTime.AddMonths(1).ToString("yyyy-MM-01");

            imageSlider1.Images.Clear();

            if (chkChecked.Checked == false && chkUnchecked.Checked == false && chkNG.Checked == false)
            {
                MsgBox.Show("Hãy chọn STATUS.\r\nPlease choose STATUS.", MsgType.Warning);
                return;
            }

            if (chkChecked.Checked)
            {
                status = "CHECKED";
            }
            if (chkUnchecked.Checked)
            {
                status = "UNCHECKED";
            }
            if (chkChecked.Checked && chkUnchecked.Checked)
            {
                status = "ALL";
            }
            if (chkNG.Checked)
            {
                showNG = "TRUE";
            }
            else
            {
                showNG = "FALSE";
            }
            base.SearchPage();

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT002.GET_LIST"
               , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                 "A_PDATE", "A_NDATE", "A_STATUS", "A_SHOW_NG"},
                 new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                 pdate, ndate, status, showNG}
               );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    gvList.OptionsView.ShowFooter = false;
                    gvList.Columns["ID"].Visible = false;
                    int c = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                    int u = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[2].Rows[0][0].ToString());
                    int ng = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[3].Rows[0][0].ToString());
                    lblReport.Text = "AMOUNT: " + c + " / CHECKED: " + u + " / UNCHECKED: " + (c - u) + " / ALL NG ITEM: " + ng;
                    lblReport.Font = new Font("Tahoma", 8.75f, FontStyle.Bold);
                }
                else
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }

        }

        private void chkNG_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNG.Checked)
            {
                chkChecked.Enabled = false;
                chkUnchecked.Enabled = false;
            }
            else
            {
                chkChecked.Enabled = true;
                chkUnchecked.Enabled = true;
            }
        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "STATUS")
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    if (cellValue.ToUpper() == "NG")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    }
                }
            }
        }

        private void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                imageSlider1.Images.Clear();
                string ID = gvList.GetRowCellDisplayText(e.RowHandle, "ID");
                string time_check = gvList.GetRowCellDisplayText(e.RowHandle, "TIME_CHECK");
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT002.GET_PICTURE"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_ID", "A_TIME_CHECK" },
                      new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     ID, time_check }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                        {
                            string url = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                            url = url.Substring(23);
                            url = url.Replace("/", @"\");
                            imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                            imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                            imageSlider1.Refresh();
                        }
                    }
                    else
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
                    }

                }
                catch (Exception ex)
                {
                    //MsgBox.Show(ex.Message, MsgType.Error);
                }
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (imageSlider1.Images.Count > 0)
            {
                imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                imageSlider1.Refresh();
            }
        }
    }
}
