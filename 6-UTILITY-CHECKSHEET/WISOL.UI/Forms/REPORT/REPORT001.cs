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

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT001 : PageType
    {
        string g_date = string.Empty;
        string g_next_date = string.Empty;
        DataTable dt_url = new DataTable();
        string c_code = string.Empty;
        string c_name = string.Empty;
        string c_item_name = string.Empty;
        string c_item_id = string.Empty;
        public REPORT001()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            RadioGroupItem item1 = new RadioGroupItem();
            item1.Description = "DAY".Translation();
            RadioGroupItem item2 = new RadioGroupItem();
            item2.Description = "NIGHT".Translation();

            radioGroup1.Properties.Items.Add(item1);
            radioGroup1.Properties.Items.Add(item2);
            radioGroup1.SelectedIndex = 0;

            DateTime moment = DateTime.Now;

            if(moment.Hour >= 9)
            {
                dtpDate.EditValue = moment.ToString("yyyy-MM-dd");
            }
            else
            {
                dtpDate.EditValue = moment.AddDays(-1).ToString("yyyy-MM-dd");
            }
            chkChecked.Checked = true;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.INT_LIST"
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
            string shift = string.Empty;
            string status = string.Empty;
            string machineStop = string.Empty;
            string showNG = string.Empty;
            string date = dtpDate.DateTime.ToString("yyyy-MM-dd");
            string next_date = dtpDate.DateTime.AddDays(1).ToString("yyyy-MM-dd");
            c_code = string.Empty;
            c_name = string.Empty;
            c_item_name = string.Empty;
            c_item_id = string.Empty;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (chkChecked.Checked == false && chkUnchecked.Checked == false && chkNG.Checked == false)
            {
                MsgBox.Show("Hãy chọn STATUS.\r\nPlease choose STATUS.", MsgType.Warning);
                return;
            }

            if (radioGroup1.SelectedIndex == 0)
            {
                shift = "DAY";
            }
            if (radioGroup1.SelectedIndex == 1)
            {
                shift = "NIGHT";
            }

            if (chkChecked.Checked)
            {
                status = "CHECKED";
            }
            if (chkUnchecked.Checked)
            {
                status = "UNCHECKED";
            }
            if(chkChecked.Checked && chkUnchecked.Checked)
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
            if (chkMachineStop.Checked)
            {
                machineStop = "1";
            }else
            {
                machineStop = "0";
            }


            gcList2.DataSource = null;
            imageSlider1.Images.Clear();
            base.SearchPage();


            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST"
                , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", 
                                 "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_STATUS", "A_SHOW_NG", "A_MACHINESTOP"},
                  new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                 date, next_date, shift, status, showNG, machineStop}
                );
                if (base.m_ResultDB.ReturnInt == 0) 
                {
                    if(showNG == "FALSE")
                    {
                        g_date = dtpDate.DateTime.ToString("yyyy-MM-dd");
                        g_next_date = dtpDate.DateTime.AddDays(1).ToString("yyyy-MM-dd");

                        base.m_BindData.BindGridView(gcList,
                          base.m_ResultDB.ReturnDataSet.Tables[0]
                          );
                    }
                    else
                    {
                        g_date = dtpDate.DateTime.ToString("yyyy-MM-dd");
                        g_next_date = dtpDate.DateTime.AddDays(1).ToString("yyyy-MM-dd");
                        gcList.DataSource = null;

                        base.m_BindData.BindGridView(gcList2,
                          base.m_ResultDB.ReturnDataSet.Tables[0]
                          );

                        //gvList2.Columns["DEVICE_ID"].Visible = false;
                        gvList2.Columns["DEVICE_ID"].Visible = false;
                        gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
                        gvList2.OptionsView.ShowFooter = false;
                        gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
                        gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }

                    int c = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString());
                    int u = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][1].ToString());
                    int ng = Convert.ToInt32(base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][2].ToString());
                    lblReport.Text = "AMOUNT: " + (c + u) + " / CHECKED: " + c + " / UNCHECKED: " + u + " / ALL NG ITEM: " +  ng;
                    lblReport.Font = new Font("Tahoma", 8.75f, FontStyle.Bold);
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

            //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\2020\8\16\checkDevice\HistoryID2422\WU-2001-1_0.png"));
            //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\2020\8\16\checkDevice\HistoryID2421\WU-2001-3_0.png"));
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
            if (e.RowHandle >= 0)
            {
                string deviceID = gvList.GetRowCellDisplayText(e.RowHandle, "DEVICE_ID");
                string status = gvList.GetRowCellDisplayText(e.RowHandle, "STATUS");
                string shift = gvList.GetRowCellDisplayText(e.RowHandle, "SHIFT");
                if (!status.Equals("Checked"))
                {
                    return;
                }

                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST_DETAIL"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_DEVICE_ID" },
                      new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     g_date, g_next_date, shift, deviceID }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        dt_url.Reset();
                        imageSlider1.Images.Clear();
                        base.m_BindData.BindGridView(gcList2,
                          base.m_ResultDB.ReturnDataSet.Tables[0]
                          );

                        gvList2.Columns["DEVICE_ID"].Visible = false;
                        gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
                        gvList2.OptionsView.ShowFooter = false;
                        gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
                        gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                        dt_url = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                        if (dt_url.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt_url.Rows.Count; i++)
                            {
                                string url = dt_url.Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");
                                imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                            }
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
                    MsgBox.Show(ex.Message, MsgType.Error);
                }
            }
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

        double min_value = 0;
        double max_value = 0;
        double real_value = 0;

        private void gvList2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if(e.RowHandle < 0)
            {
                return;
            }
            bool temp = false;

            if (!chkNG.Checked)
            {
                if (e.Column.FieldName == "REAL_VALUE")
                {
                    min_value = 0;
                    max_value = 0;
                    if (!string.IsNullOrWhiteSpace(gvList2.GetDataRow(e.RowHandle)["MIN_VALUE"].NullString()))
                    {
                        min_value = Double.Parse(gvList2.GetDataRow(e.RowHandle)["MIN_VALUE"].NullString());
                        temp = true;
                    }
                    if (!string.IsNullOrWhiteSpace(gvList2.GetDataRow(e.RowHandle)["MAX_VALUE"].NullString()))
                    {
                        max_value = Double.Parse(gvList2.GetDataRow(e.RowHandle)["MAX_VALUE"].NullString());
                        temp = true;
                    }

                    if (!string.IsNullOrWhiteSpace(gvList2.GetDataRow(e.RowHandle)["REAL_VALUE"].NullString()))
                    {
                        if (temp)
                        {
                            real_value = Double.Parse(gvList2.GetDataRow(e.RowHandle)["REAL_VALUE"].NullString());
                            //if (real_value >= min_value && real_value <= max_value)
                            //{

                            //}
                            //else
                            //{
                            //    e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                            //    e.Appearance.ForeColor = Color.Red;
                            //}
                            if(real_value < min_value && min_value != 0)
                            {
                                e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                                e.Appearance.ForeColor = Color.Red;
                            }
                            if (real_value > max_value && max_value != 0)
                            {
                                e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                                e.Appearance.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            if (gvList2.GetDataRow(e.RowHandle)["REAL_VALUE"].NullString().ToUpper().Equals("NG"))
                            {
                                e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                                e.Appearance.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            else
            {
                if (e.Column.FieldName == "REAL_VALUE")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 199, 206);
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void imageSlider1_Click(object sender, EventArgs e)
        {
            //MsgBox.Show("abc", MsgType.Information);

            if (imageSlider1.Images.Count > 0)
            {
                POP.POP_REPORT001 popup = new POP.POP_REPORT001(dt_url);
                popup.ShowDialog();
            }
        }

        private void gvList2_RowClick(object sender, RowClickEventArgs e)
        {
            if(e.RowHandle >= 0)
            {
                string deviceID = gvList2.GetRowCellDisplayText(e.RowHandle, "DEVICE_ID");
                c_code = gvList2.GetRowCellDisplayText(e.RowHandle, "DEVICE_CODE");
                c_name = gvList2.GetRowCellDisplayText(e.RowHandle, "DEVICE_NAME");
                c_item_name = gvList2.GetRowCellDisplayText(e.RowHandle, "ITEM_CHECK");
                c_item_id = gvList2.GetRowCellDisplayText(e.RowHandle, "ITEM_CHECK_ID");
                string min_val = gvList2.GetRowCellDisplayText(e.RowHandle, "MIN_VALUE");
                string max_val = gvList2.GetRowCellDisplayText(e.RowHandle, "MAX_VALUE");
                string shift = gvList2.GetRowCellDisplayText(e.RowHandle, "SHIFT");

                if (chkNG.Checked)
                {

                    try
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST_DETAIL"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_DEVICE_ID" },
                          new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     g_date, g_next_date, shift, deviceID }
                        );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            dt_url.Reset();
                            imageSlider1.Images.Clear();

                            dt_url = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                            if (dt_url.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_url.Rows.Count; i++)
                                {
                                    string url = dt_url.Rows[i][0].ToString();
                                    url = url.Substring(23);
                                    url = url.Replace("/", @"\");
                                    imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                                }
                                imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                imageSlider1.Refresh();

                            }
                            if ((!string.IsNullOrWhiteSpace(min_val)) && (!string.IsNullOrWhiteSpace(max_val)))
                            {
                                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            }
                            else
                            {
                                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
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
                    if ((!string.IsNullOrWhiteSpace(min_val)) && (!string.IsNullOrWhiteSpace(max_val)))
                    {
                        string previous_date = dtpDate.DateTime.AddMonths(-1).ToString("MM-dd").ToString();
                        string current_date = dtpDate.DateTime.ToString("MM-dd").ToString();
                        POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name,previous_date, current_date);
                        popup.ShowDialog();
                    }
                }
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(c_item_id))
            {
                return;
            }
            string previous_date = dtpDate.DateTime.AddMonths(-1).ToString("MM-dd").ToString();
            string current_date = dtpDate.DateTime.ToString("MM-dd").ToString();
            POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date);
            popup.ShowDialog();
        }
    }
}
