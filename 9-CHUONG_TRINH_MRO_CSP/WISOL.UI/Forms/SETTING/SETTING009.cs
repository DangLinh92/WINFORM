using System;
using System.Data;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SETTING.POP;
//using Wisol.MES.Forms.SETTING.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING009 : PageType
    {
        DataTable dt = new DataTable(); // dt WAFER, luu du lieu e hoach WAFER.
        DataTable dt_CHIP = new DataTable();
        DataTable dt1 = new DataTable();
        public static string plan_type;
        public string Get_Plan_Type()
        {
            
            return plan_type;
        }

        public SETTING009()
        {
            InitializeComponent();
            
        }
        public override void Form_Show()
        {
            cmbTypePlan.Properties.Items.Clear();
            if (Consts.DEPARTMENT == "WLP2") 
            {
                cmbTypePlan.Properties.Items.Add("WAFER PLAN");
                cmbTypePlan.Properties.Items.Add("CHIP PLAN");
            }
            if (Consts.DEPARTMENT == "LFEM")
            {
                cmbTypePlan.Properties.Items.Add("SAN XUAT");
                cmbTypePlan.Properties.Items.Add("SHIPPING");
            }

            base.Form_Show();
            this.InitializePage();
            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            cmbTypePlan.SelectedIndex = 0;
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.INT_LIST"
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

                    Init_Control(true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }


            base.InitializePage();
            this.SearchPage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                // Copy du lieu tu bang KE HOACH THANG vao bang KE HOACH TUAN.
                //base.m_ResultDB = base.m_DBaccess.ExcuteProc("Thu tuc copy du lieu vao bang KE HOACH TUAN");
                // Kiem tra xem phong ban nao, de goi procedure tuong ung
                if (Consts.DEPARTMENT == "CSP") {
                    int i = base.m_DBaccess.ExcuteProcNoneQuery("PKG_SETTING009.COPY_TO_KEHOACH_TUAN");
                }
                if (Consts.DEPARTMENT == "WLP2")
                {
                    int i = base.m_DBaccess.ExcuteProcNoneQuery("PKG_SETTING009.COPY_TO_KEHOACH_TUAN_WLP2");
                }
                if (Consts.DEPARTMENT == "LFEM")
                {
                    int i = base.m_DBaccess.ExcuteProcNoneQuery("PKG_SETTING009.COPY_TO_KEHOACH_TUAN_LFEM");
                }

                //int i= base.m_DBaccess.ExcuteProcNoneQuery("PKG_SETTING009.COPY_TO_KEHOACH_TUAN");

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.GET_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    //dt1= base.m_ResultDB.ReturnDataSet.Tables[1];
                    //dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    if (base.m_ResultDB.ReturnDataSet.Tables[1].Rows.Count > 0) { dt1 = base.m_ResultDB.ReturnDataSet.Tables[1]; }
                    if (base.m_ResultDB.ReturnDataSet.Tables[0].Rows.Count > 0)
                    {
                        dt = base.m_ResultDB.ReturnDataSet.Tables[0]; // Biến dùng chung cho các phòng ban.
                    }
                    
                    if ((Consts.DEPARTMENT == "WLP2"))
                    {
                        dt_CHIP = base.m_ResultDB.ReturnDataSet.Tables[2];
                        dt_CHIP.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt_CHIP.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt_CHIP.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt_CHIP.AcceptChanges();

                        tablePanel1.Visible = true; // Hien thi lua chon loai ke hoach (WAFER hay CHIP)
                        cmbTypePlan.Visible = true;
                    }
                    if ((Consts.DEPARTMENT == "LFEM"))
                    {
                        dt_CHIP = base.m_ResultDB.ReturnDataSet.Tables[2]; // Mượn tạm biến dt_CHIP để không phải khai báo thêm.
                        dt_CHIP.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                        dt_CHIP.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                        dt_CHIP.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                        dt_CHIP.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";
                        dt_CHIP.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt_CHIP.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt_CHIP.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt_CHIP.AcceptChanges();
                        tablePanel1.Visible = true; // Hien thi lua chon loai ke hoach/SHIPPING
                        cmbTypePlan.Visible = true;
                    }
                    else { tablePanel1.Visible = false; }


                    // Manh revise
                    dt.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5,2) + "-W1";
                    dt.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W2";
                    dt.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W3";
                    dt.Columns["NEXT_4"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString().Substring(5, 2) + "-W4";
                    dt.Columns["NEXT_5"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W1";
                    dt.Columns["NEXT_6"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W2";
                    dt.Columns["NEXT_7"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W3";
                    dt.Columns["NEXT_8"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString().Substring(5, 2) + "-W4";
                    dt.Columns["NEXT_9"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W1";
                    dt.Columns["NEXT_10"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W2";
                    dt.Columns["NEXT_11"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W3";
                    dt.Columns["NEXT_12"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString().Substring(5, 2) + "-W4";
                    dt.Columns["Column1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                    dt.Columns["Column2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                    dt.Columns["Column3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                    dt.AcceptChanges();

                    base.m_BindData.BindGridView(gcList,dt);
                    
                    for (int k = 1; k < gvList.Columns.Count; k++) {
                        // Định dạng dạng số trong Grid view
                        gvList.Columns[k].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns[k].DisplayFormat.FormatString = "n2";
                    }

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtCommCode.EditValue = string.Empty;
                txtPrice.EditValue = string.Empty;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCommCode.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_110".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtType.EditValue.NullString()) == true)
                {
                    MsgBox.Show("MSG_ERR_113".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING009.PUT_ITEM"
                    , new string[] { "A_PLANT",
                        "A_COMMCODE",
                        "A_TYPE",
                        "A_PRICE",
                        "A_YEAR_MONTH",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] { Consts.PLANT,
                        txtCommCode.EditValue.NullString().ToUpper(),
                        txtType.EditValue.NullString().ToUpper().Replace(" ", string.Empty),
                        txtPrice.EditValue.NullString().ToUpper(),
                        Consts.USER_INFO.Id
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtCommCode.Text = string.Empty;
                    txtType.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    SearchPage();
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

        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                Init_Control(false);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }




        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    txtCommCode.EditValue = gvList.GetDataRow(e.RowHandle)["COMMCODE"].NullString();
                    txtType.EditValue = gvList.GetDataRow(e.RowHandle)["TYPE"].NullString();
                    if (string.IsNullOrWhiteSpace(gvList.GetDataRow(e.RowHandle)["PRICE"].NullString()))
                    {
                        txtPrice.EditValue = string.Empty;
                    }
                    else
                    {
                        float price = float.Parse(gvList.GetDataRow(e.RowHandle)["PRICE"].NullString());
                        txtPrice.EditValue = price.ToString("#,##0.00");
                    }
                    txtPrice.Focus();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = string.Empty;
                if (!GetExcelFileName(ref fileName)) return;
                //var pop = new POP_SETTING009(fileName);
                var pop = new POP_SETTING007(fileName);
                if (pop.ShowDialog() == DialogResult.OK)
                    SearchPage();
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void cmbTypePlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTypePlan.SelectedIndex == 0)
            {
                // Do du lieu vao gcList tu datatable
                base.m_BindData.BindGridView(gcList, dt); // dt luu ke hoach san xuat WAFER và kế hoạch sản xuất của các phòng ban (biến chung)

                for (int k = 1; k < gvList.Columns.Count; k++)
                {
                    // Định dạng dạng số trong Grid view
                    gvList.Columns[k].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns[k].DisplayFormat.FormatString = "n2";
                }
            }
            else 
            {
                // Do du lieu vao gcList tu datatable
                base.m_BindData.BindGridView(gcList, dt_CHIP);// dt_CHIP luu ke hoach san xuat CHIP WLP2 và kế hoạch SHIPPING của LFEM

                for (int k = 1; k < gvList.Columns.Count; k++)
                {
                    // Định dạng dạng số trong Grid view
                    gvList.Columns[k].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns[k].DisplayFormat.FormatString = "n2";
                }
            }
            plan_type = cmbTypePlan.Text;
        }

    }
}
