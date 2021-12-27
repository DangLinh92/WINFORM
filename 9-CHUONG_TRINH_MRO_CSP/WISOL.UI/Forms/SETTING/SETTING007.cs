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
    public partial class SETTING007 : PageType
    {
        public SETTING007()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.INT_LIST"
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.GET_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT", "A_USER"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT, Consts.USER_INFO.Id
                    }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                    if (dt.Rows.Count > 0) {
                        dt.Columns["NEXT_1"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[0][0].ToString();
                        dt.Columns["NEXT_2"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[1][0].ToString();
                        dt.Columns["NEXT_3"].ColumnName = base.m_ResultDB.ReturnDataSet.Tables[1].Rows[2][0].ToString();
                        dt.AcceptChanges();

                        base.m_BindData.BindGridView(gcList,
                            //base.m_ResultDB.ReturnDataSet.Tables[0]
                            dt
                            );
                        // gvList.OptionsView.ShowFooter = false;
                        gvList.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns[1].DisplayFormat.FormatString = "n0";
                        gvList.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns[2].DisplayFormat.FormatString = "n0";
                        gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gvList.Columns[3].DisplayFormat.FormatString = "n0";
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

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.PUT_ITEM"
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
                var pop = new POP_SETTING007(fileName);
                if (pop.ShowDialog() == DialogResult.OK)
                    SearchPage();
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

       
    }
}
