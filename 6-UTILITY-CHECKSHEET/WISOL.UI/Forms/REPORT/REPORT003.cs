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

namespace Wisol.MES.Forms.REPORT
{
    public partial class REPORT003 : PageType
    {
        DataTable dt = new DataTable("Check");
        int row_index = -1;
        string time_check = string.Empty;
        string chiphi = string.Empty;
        public REPORT003()
        {
            InitializeComponent();
        }

        public override void Form_Show()
        {
            base.Form_Show();
            this.InitializePage();
            dtpFrom.EditValue = "2020-12-01"; //DateTime.Now.AddYears(-3).ToString("yyyy-MM-dd");
            dtpTo.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            this.layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            dt.Columns.Add("index", typeof(int));
            dt.Columns.Add("val", typeof(string));
            dt.Columns.Add("name", typeof(string));
            this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT003.INT_LIST"
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
            imageSlider1.Images.Clear();
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT003.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG", "A_FROM_DATE", "A_TO_DATE"
                    }
                    , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, dtpFrom.DateTime.ToString("yyyy-MM-dd"), dtpTo.DateTime.ToString("yyyy-MM-dd")
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    //gvList.OptionsView.ShowFooter = false;
                    gvList.Columns["INCIDENTREPORTID"].Visible = false;
                    gvList.Columns["ITEM"].Width = 400;

                    gvList.Columns["CHI_PHI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CHI_PHI"].DisplayFormat.FormatString = "n0";
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


        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }


        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                row_index = e.RowHandle;
                time_check = gvList.GetRowCellDisplayText(e.RowHandle, "TIME_MAINTENANCE");
                chiphi = gvList.GetRowCellDisplayText(e.RowHandle, "CHI_PHI");
                imageSlider1.Images.Clear();
                dt.Rows.Clear();
                string ID = gvList.GetRowCellDisplayText(e.RowHandle, "INCIDENTREPORTID");
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT003.GET_PICTURE"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_ID" },
                      new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language,
                                     ID }
                    );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count; i++)
                            {
                                string url = base.m_ResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");

                                dt.Rows.Add(new Object[] { i, "false", url.Substring(url.LastIndexOf("\\") + 1) });


                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                //pictureEdit2.Image = Image.FromStream(ms);
                                //pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                                imageSlider1.Images.Add(Image.FromStream(ms));
                            }

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

                string code = gvList.GetRowCellDisplayText(e.RowHandle, "CODE");
                string name = gvList.GetRowCellDisplayText(e.RowHandle, "DEVICE_NAME");
                string item = gvList.GetRowCellDisplayText(e.RowHandle, "ITEM");
                string note = gvList.GetRowCellDisplayText(e.RowHandle, " NOTE");
                txtCode.Text = code;
                txtName.Text = name;
                txtItem.Text = item;
                txtNote.Text = note;
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

        private void imageSlider1_ImageChanged(object sender, ImageChangedEventArgs e)
        {
            if (imageSlider1.Images.Count > 0)
            {
                int index = imageSlider1.CurrentImageIndex;
                
                DataTable dt_g = new DataTable();

                dt_g = dt.Select("index = " + index).CopyToDataTable();
                //if (dt_g.Rows[0][1].ToString() == "false")
                //{
                //    DataRow[] selected = dt.Select("index = " + index);
                //    foreach (DataRow row in selected)
                //    {
                //        row["val"] = "true";
                //    }

                //    //imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                //    //imageSlider1.Refresh();
                //}
                string name = dt_g.Rows[0][2].ToString();
                name = name.Substring(0, name.IndexOf("."));
                if(name.Length > 3)
                {
                    imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    imageSlider1.Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (row_index >= 0)
            {
                string incidentId = gvList.GetRowCellDisplayText(row_index, "INCIDENTREPORTID");

                DialogResult dialogResult = MsgBox.Show("Bạn chắc chắn muốn xóa?", MsgType.Warning, DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    try
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_REPORT003.DELETE_ITEM"
                            , new string[] { "A_PLANT", "A_DEPARTMENT", "A_TRAN_USER", "A_LANG",
                                     "A_ID"
                            }
                            , new string[] { Consts.PLANT, "", Consts.USER_INFO.Id, Consts.USER_INFO.Language, incidentId
                            }
                            );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            MsgBox.Show("Xóa thành công.", MsgType.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                    SearchPage();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(row_index >= 0)
            {
                string incidentID = gvList.GetRowCellDisplayText(row_index, "INCIDENTREPORTID");
                POP.POP_REPORT003 popup = new POP.POP_REPORT003(txtCode.Text, txtName.Text, txtItem.Text, chiphi, txtNote.Text, incidentID, time_check);
                popup.ShowDialog();
                this.SearchPage();
                //if (popup.ShowDialog() == DialogResult.OK)
                //    SearchPage();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            POP.POP_REPORT003_NEW popup = new POP.POP_REPORT003_NEW("input");
            popup.ShowDialog();
        }
    }
}
