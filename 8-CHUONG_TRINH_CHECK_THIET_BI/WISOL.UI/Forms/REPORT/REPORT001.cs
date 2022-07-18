using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
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
        public static DataTable dt_detail = new DataTable();
        public DataTable vdt = new DataTable();
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

            gvList.OptionsSelection.MultiSelect = true;
            gvList.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            gvList2.OptionsSelection.MultiSelect = true;
            gvList2.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            // Phục vụ cho việc xuất ra File Excel chi tiết điểm check
            dt_detail.Columns.Add("DEVICE_CODE", typeof(string));
            dt_detail.Columns.Add("DEVICE_NAME", typeof(string));
            dt_detail.Columns.Add("SHIFT", typeof(string));
            dt_detail.Columns.Add("ITEM_CHECK", typeof(string));
            dt_detail.Columns.Add("MIN_VALUE", typeof(string));
            dt_detail.Columns.Add("MAX_VALUE", typeof(string));
            dt_detail.Columns.Add("OTHER", typeof(string));
            dt_detail.Columns.Add("REAL_VALUE", typeof(string));
            // *********************************************
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
                //gvList2.Columns.Add(
                //new DevExpress.XtraGrid.Columns.GridColumn()
                //{
                //    Caption = "Selected",
                //    ColumnEdit = new RepositoryItemCheckEdit() { },
                //    VisibleIndex = 0,
                //    UnboundType = DevExpress.Data.UnboundColumnType.Boolean
                //}
                //);
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
            string showNG = string.Empty;
            string date = dtpDate.DateTime.ToString("yyyy-MM-dd");
            string next_date = dtpDate.DateTime.AddDays(1).ToString("yyyy-MM-dd");
            c_code = string.Empty;
            c_name = string.Empty;
            c_item_name = string.Empty;
            c_item_id = string.Empty;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            vdt.Clear();
            //gvList.Columns.AddField("DEVICE_SELECT");
            
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



            gcList2.DataSource = null;
            imageSlider1.Images.Clear();
            base.SearchPage();


            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST"
                , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", 
                                 "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_STATUS", "A_SHOW_NG"},
                  new string[] { Consts.PLANT, Consts.DEPARTMENT, Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                 date, next_date, shift, status, showNG}
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
        }

        private void gvList_RowClick(object sender, RowClickEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    string deviceID = gvList.GetRowCellDisplayText(e.RowHandle, "DEVICE_ID");
            //    string status = gvList.GetRowCellDisplayText(e.RowHandle, "STATUS");
            //    string shift = gvList.GetRowCellDisplayText(e.RowHandle, "SHIFT");
            //    if (!status.Equals("Checked"))
            //    {
            //        return;
            //    }

            //    if (gvList.IsRowSelected(e.RowHandle))
            //    {
            //        //MessageBox.Show("This row was selected.");
            //        try
            //        {
            //            base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST_DETAIL"
            //            , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
            //                         "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_DEVICE_ID" },
            //              new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
            //                         g_date, g_next_date, shift, deviceID }
            //            );
            //            if (base.m_ResultDB.ReturnInt == 0)
            //            {
            //                dt_url.Reset();
            //                imageSlider1.Images.Clear();
            //                gcList2.DataSource = null;
            //                if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count < 1)
            //                {
            //                    MsgBox.Show("Máy không sản xuất hoặc đang hỏng!".Translation(), MsgType.Information);
            //                    return;
            //                }

            //                vdt.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]); // Gop 2 Datatable vao mot.

            //                //base.m_BindData.BindGridView(gcList2,
            //                //  base.m_ResultDB.ReturnDataSet.Tables[0]
            //                //  );

            //                base.m_BindData.BindGridView(gcList2,
            //                  vdt
            //                  );

            //                gvList2.Columns["DEVICE_ID"].Visible = false;
            //                gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
            //                gvList2.OptionsView.ShowFooter = false;
            //                gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
            //                gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //                gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //                gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            //                c_item_id = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["ITEM_CHECK_ID"].ToString();

            //                dt_url = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
            //                if (dt_url.Rows.Count > 0)
            //                {
            //                    for (int i = 0; i < dt_url.Rows.Count; i++)
            //                    {
            //                        string url = dt_url.Rows[i][0].ToString();
            //                        url = url.Substring(23);
            //                        url = url.Replace("/", @"\");
            //                        //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));

            //                        Image img = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);

            //                        if (i > 0)
            //                        {
            //                            var _exifOrientation = (int)img.GetPropertyItem(0x0112).Value[0];
            //                            img.RotateFlip(GetOrientationToFlipType(_exifOrientation));
            //                        }
            //                        imageSlider1.Images.Add(img);
            //                    }
            //                    //imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
            //                    //imageSlider1.Refresh();

            //                    var _exifOrientation1 = (int)imageSlider1.CurrentImage.GetPropertyItem(0x0112).Value[0];
            //                    imageSlider1.CurrentImage.RotateFlip(GetOrientationToFlipType(_exifOrientation1));
            //                    imageSlider1.Refresh();
            //                }

            //            }
            //            else
            //            {
            //                MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MsgBox.Show(ex.Message, MsgType.Error);
            //        }

            //    }
            //    else
            //    {
            //        //MessageBox.Show("This row was not selected.");
            //        string vDEVICE_ID = gvList.GetRowCellValue(e.RowHandle, "DEVICE_ID").ToString();
            //        string filterExpress = "DEVICE_ID <> " + vDEVICE_ID;
            //        if (vdt.Rows.Count <= 0) { return; }
            //        DataRow[] temp = vdt.Select(filterExpress);
            //        if (temp.Length >= 0)
            //        {
            //            if (temp.Length == 0)
            //            {
            //                vdt.Rows.Clear();
            //            }
            //            else 
            //            {
            //                vdt = temp.CopyToDataTable();
            //            }
            //            base.m_BindData.BindGridView(gcList2,
            //                vdt
            //            );

            //            gvList2.Columns["DEVICE_ID"].Visible = false;
            //            gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
            //            gvList2.OptionsView.ShowFooter = false;
            //            gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
            //            gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            //            c_item_id = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["ITEM_CHECK_ID"].ToString();
            //        }
            //    }


            //}
        }

        private RotateFlipType GetOrientationToFlipType(int orientationValue)
        {
            RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;
            switch (orientationValue)
            {
                case 1:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    rotateFlipType = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    rotateFlipType = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    rotateFlipType = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    rotateFlipType = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    rotateFlipType = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    rotateFlipType = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
            }
            return rotateFlipType;
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
            if (e.RowHandle >= 0)
            {
                // Kiểm tra xem dòng được chọn hay không được chọn ********************
                if (gvList2.IsRowSelected(e.RowHandle))
                {
                    //MessageBox.Show("This row was selected.");
                    //return;
                }
                else
                {
                    //MessageBox.Show("This row was not selected.");
                    //return;
                }
                // ********************************************************************
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
                            //if ((!string.IsNullOrWhiteSpace(min_val)) && (!string.IsNullOrWhiteSpace(max_val)))
                            //{
                            //    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            //}
                            //else
                            //{
                            //    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            //}
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
                // Tạm thời bỏ đoạn CODE bên dưới 4/1/2022 ************************

                //string previous_date = dtpDate.DateTime.AddMonths(-1).ToString("MM-dd").ToString();
                //string current_date = dtpDate.DateTime.ToString("MM-dd").ToString();
                //POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date);
                //popup.ShowDialog();

                // *****************************************************************
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
            POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date); // Mạnh sủa Method này. 4/1/2022
            popup.ShowDialog();
        }

        private void cmbShowAll_Click(object sender, EventArgs e)
        {
            if (gvList.RowCount <= 0) {
                MessageBox.Show("Don't have data to Export.");
                return;
            }
            
            DataTable dt = (DataTable)gcList.DataSource;
            string shift = dt.Rows[0][3].ToString();
            
            //dt_detail.Columns.Add("DEVICE_CODE", typeof(string));
            //dt_detail.Columns.Add("DEVICE_NAME", typeof(string));
            //dt_detail.Columns.Add("SHIFT", typeof(string));
            //dt_detail.Columns.Add("ITEM_CHECK", typeof(string));
            //dt_detail.Columns.Add("MIN_VALUE", typeof(string));
            //dt_detail.Columns.Add("MAX_VALUE", typeof(string));
            //dt_detail.Columns.Add("OTHER", typeof(string));
            //dt_detail.Columns.Add("REAL_VALUE", typeof(string));


            try
            {
                SplashScreenManager.ShowForm(this, typeof(global::Wisol.MES.FrmWaitForm), false, false);
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Duyet tung hang.....................
                    string vDeviceID = dt.Rows[i][0].ToString();

                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST_DETAIL"
                                                            , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                                                             "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_DEVICE_ID" },
                                                            new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                                            g_date, g_next_date, shift, vDeviceID }
                                                            );

                    DataTable temp_dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    //dr = dt_detail.NewRow();
                    for (int j = 0; j < temp_dt.Rows.Count; j++)
                    {
                        dr = dt_detail.NewRow();
                        dr["DEVICE_CODE"] = temp_dt.Rows[j]["DEVICE_CODE"].ToString();
                        dr["DEVICE_NAME"] = temp_dt.Rows[j]["DEVICE_NAME"].ToString();
                        dr["SHIFT"] = temp_dt.Rows[j]["SHIFT"].ToString();
                        dr["ITEM_CHECK"] = temp_dt.Rows[j]["ITEM_CHECK"].ToString();
                        dr["MIN_VALUE"] = temp_dt.Rows[j]["MIN_VALUE"].ToString();
                        dr["MAX_VALUE"] = temp_dt.Rows[j]["MAX_VALUE"].ToString();
                        dr["OTHER"] = temp_dt.Rows[j]["OTHER"].ToString();
                        dr["REAL_VALUE"] = temp_dt.Rows[j]["REAL_VALUE"].ToString();
                        dt_detail.Rows.Add(dr);
                    }
                }
                SplashScreenManager.CloseForm();
                //POP.Export_001 frmExport = new POP.Export_001();
                POP.POP_REPORT004 frmExport = new POP.POP_REPORT004();
                frmExport.Show();

                //base.m_BindData.BindGridView(gcList2,
                //                                dt_detail
                //                             );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbViewChart_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem dòng được chọn hay không được chọn ********************
            string[] temp;
            int k = 0;
            DataTable vdt = new DataTable();
            vdt.Columns.Add("DEVICE_ID", typeof(int));
            vdt.Columns.Add("DEVICE_CODE", typeof(string));
            vdt.Columns.Add("DEVICE_NAME", typeof(string));
            vdt.Columns.Add("ITEM_CHECK", typeof(string));
            vdt.Columns.Add("ITEM_CHECK_ID", typeof(int));
            vdt.Columns.Add("MIN_VALUE", typeof(float));
            vdt.Columns.Add("MAX_VALUE", typeof(float));
            vdt.Columns.Add("SHIFT", typeof(string));
            vdt.Columns.Add("unit", typeof(string));

            for (int i = 0; i < gvList2.RowCount; i++) 
            {
                if (gvList2.IsRowSelected(i))
                {
                    // ********************************************************************
                    DataRow dr = vdt.NewRow();
                    dr["DEVICE_ID"] = gvList2.GetRowCellDisplayText(i, "DEVICE_ID");
                    dr["DEVICE_CODE"] = gvList2.GetRowCellDisplayText(i, "DEVICE_CODE");
                    dr["DEVICE_NAME"] = gvList2.GetRowCellDisplayText(i, "DEVICE_NAME");
                    dr["ITEM_CHECK"] = gvList2.GetRowCellDisplayText(i, "ITEM_CHECK");
                    dr["ITEM_CHECK_ID"] = gvList2.GetRowCellDisplayText(i, "ITEM_CHECK_ID");

                    if (!string.IsNullOrWhiteSpace(gvList2.GetRowCellDisplayText(i, "MIN_VALUE")))
                    {
                        dr["MIN_VALUE"] = gvList2.GetRowCellDisplayText(i, "MIN_VALUE");
                    }
                    if (!string.IsNullOrWhiteSpace(gvList2.GetRowCellDisplayText(i, "MAX_VALUE")))
                    {
                        dr["MAX_VALUE"] = gvList2.GetRowCellDisplayText(i, "MAX_VALUE"); 
                    }

                    if (gvList.RowCount > 0) // Loai bo truong hop bam vao button SHOW_ALL_NG (se khong co cot unit)
                    {
                        if (!string.IsNullOrWhiteSpace(gvList2.GetRowCellDisplayText(i, "unit")))
                        {
                            dr["unit"] = gvList2.GetRowCellDisplayText(i, "unit");
                        }
                    }


                    //dr["MIN_VALUE"] = gvList2.GetRowCellDisplayText(i, "MIN_VALUE");
                    //dr["MAX_VALUE"] = gvList2.GetRowCellDisplayText(i, "MAX_VALUE");

                    dr["SHIFT"] = gvList2.GetRowCellDisplayText(i, "SHIFT");
                    vdt.Rows.Add(dr);
                    ////**********************************************************************
                    //string deviceID = gvList2.GetRowCellDisplayText(i, "DEVICE_ID");
                    //c_code = gvList2.GetRowCellDisplayText(i, "DEVICE_CODE");
                    //c_name = gvList2.GetRowCellDisplayText(i, "DEVICE_NAME");
                    //c_item_name = gvList2.GetRowCellDisplayText(i, "ITEM_CHECK");
                    //c_item_id = gvList2.GetRowCellDisplayText(i, "ITEM_CHECK_ID");
                    //string min_val = gvList2.GetRowCellDisplayText(i, "MIN_VALUE");
                    //string max_val = gvList2.GetRowCellDisplayText(i, "MAX_VALUE");
                    //string shift = gvList2.GetRowCellDisplayText(i, "SHIFT");
                    //k = k + 1;
                    //string previous_date = dtpDate.DateTime.AddMonths(-1).ToString("MM-dd").ToString();
                    //string current_date = dtpDate.DateTime.ToString("MM-dd").ToString();
                    //POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(c_item_id, c_code, c_name, c_item_name, previous_date, current_date, k);
                    //popup.ShowDialog();
                }

            }
            //string previous_date = dtpDate.DateTime.AddMonths(-1).ToString("MM-dd").ToString(); // Manh bo dong nay 2022-06-06
            string previous_date = dtpDate.DateTime.ToString("MM-dd");
            //string current_date = dtpDate.DateTime.ToString("MM-dd").ToString(); // Manh bo dong nay 2022-06-06
            string current_date = System.DateTime.Now.ToString("MM-dd").ToString();
            POP.POP_REPORT001_CHART popup = new POP.POP_REPORT001_CHART(vdt,previous_date, current_date);
            popup.ShowDialog();
        }

        private void gvList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
        }

        private void gvList_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {

            //MessageBox.Show("Test SelectionChanged events");
            string shift = gvList.GetRowCellDisplayText(e.ControllerRow, "SHIFT");
            string deviceID = gvList.GetRowCellDisplayText(e.ControllerRow, "DEVICE_ID");
            if (e.ControllerRow >= 0)
            {
                
                string status = gvList.GetRowCellDisplayText(e.ControllerRow, "STATUS");
                //string deviceID = gvList.GetRowCellDisplayText(e.ControllerRow, "DEVICE_ID");
                //string shift = gvList.GetRowCellDisplayText(e.ControllerRow, "SHIFT");
                if (!status.Equals("Checked"))
                {
                    return;
                }

                if (gvList.IsRowSelected(e.ControllerRow))
                {
                    //MessageBox.Show("This row was selected.");
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
                            //gcList2.DataSource = null;
                            if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count < 1)
                            {
                                MsgBox.Show("Máy không sản xuất hoặc đang hỏng!".Translation(), MsgType.Information);
                                return;
                            }

                            vdt.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]); // Gop 2 Datatable vao mot.

                            //base.m_BindData.BindGridView(gcList2,
                            //  base.m_ResultDB.ReturnDataSet.Tables[0]
                            //  );

                            base.m_BindData.BindGridView(gcList2,
                              vdt
                              );

                            gvList2.Columns["DEVICE_ID"].Visible = false;
                            gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
                            gvList2.OptionsView.ShowFooter = false;
                            gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
                            gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                            c_item_id = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["ITEM_CHECK_ID"].ToString();

                            dt_url = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                            if (dt_url.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_url.Rows.Count; i++)
                                {
                                    string url = dt_url.Rows[i][0].ToString();
                                    url = url.Substring(23);
                                    url = url.Replace("/", @"\");
                                    //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));

                                    Image img = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);

                                    if (i > 0)
                                    {
                                        var _exifOrientation = (int)img.GetPropertyItem(0x0112).Value[0];
                                        img.RotateFlip(GetOrientationToFlipType(_exifOrientation));
                                    }
                                    imageSlider1.Images.Add(img);
                                }
                                //imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                //imageSlider1.Refresh();

                                var _exifOrientation1 = (int)imageSlider1.CurrentImage.GetPropertyItem(0x0112).Value[0];
                                imageSlider1.CurrentImage.RotateFlip(GetOrientationToFlipType(_exifOrientation1));
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
                else
                {
                    //MessageBox.Show("This row was not selected.");
                    string vDEVICE_ID = gvList.GetRowCellValue(e.ControllerRow, "DEVICE_ID").ToString();
                    string filterExpress = "DEVICE_ID <> " + vDEVICE_ID;
                    if (vdt.Rows.Count <= 0) { return; }
                    DataRow[] temp = vdt.Select(filterExpress); // Loc nhung dong co deivice Id khac voi ID bo chon
                    if (temp.Length >= 0)
                    {
                        if (temp.Length == 0)
                        {
                            vdt.Rows.Clear();
                        }
                        else
                        {
                            vdt = temp.CopyToDataTable();  // Chuyen tu mang datarow to Datatable
                        }
                        base.m_BindData.BindGridView(gcList2,
                            vdt
                        );

                        gvList2.Columns["DEVICE_ID"].Visible = false;
                        gvList2.Columns["ITEM_CHECK_ID"].Visible = false;
                        gvList2.OptionsView.ShowFooter = false;
                        gvList2.Appearance.Row.Font = new Font("Tahoma", 8.75f);
                        gvList2.Columns["MIN_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["MAX_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvList2.Columns["REAL_VALUE"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                        c_item_id = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[0]["ITEM_CHECK_ID"].ToString();
                    }
                }


            }
            else
            {
                // Truong hop chon all/ hoac bo chon all.
                //MessageBox.Show("TEST");
                SplashScreenManager.ShowForm(this, typeof(global::Wisol.MES.FrmWaitForm), false, false);
                vdt.Clear();
                for (int i = 0; i< gvList.RowCount; i++) {

                    if (gvList.IsRowSelected(i))
                    {
                        shift = gvList.GetRowCellDisplayText(i, "SHIFT");
                        deviceID = gvList.GetRowCellDisplayText(i, "DEVICE_ID");
                        // Add item check sang right panel.
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT001.GET_LIST_DETAIL"
                        , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_DATE", "A_NEXT_DATE", "A_SHIFT_WORK", "A_DEVICE_ID" },
                          new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     g_date, g_next_date, shift, deviceID }
                        );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            vdt.Merge(base.m_ResultDB.ReturnDataSet.Tables[0]); // Gop 2 Datatable vao mot.
                        }
                    }
                }
                // Do data vao gvList2......
                base.m_BindData.BindGridView(gcList2,vdt);
                SplashScreenManager.CloseForm();
            }
        }
    }
}
