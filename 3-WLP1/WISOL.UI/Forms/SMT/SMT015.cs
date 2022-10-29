using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT015 : PageType
    {
        public SMT015()
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
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT015.INT_LIST"
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
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT015.GET_LIST",
                    new string[] { "A_PLANT", "A_YEAR_MONTH" },
                    new string[] { Consts.PLANT, dtpYearMonth.DateTime.ToString("yyyyMM") }
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[4].DisplayFormat.FormatString = "n0";
            gvList.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[5].DisplayFormat.FormatString = "n0";
            gvList.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[6].DisplayFormat.FormatString = "n0";
            gvList.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[7].DisplayFormat.FormatString = "n0";
            gvList.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[8].DisplayFormat.FormatString = "n0";
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtLine.EditValue = string.Empty;
                txtPoint.EditValue = string.Empty;
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
                string line = txtLine.Text.Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(line))
                {
                    MsgBox.Show("MSG_ERR_023".Translation(), MsgType.Warning);
                    return;
                }
                if (line != "C" && line != "D" && line != "E" && line != "F" && line != "G" && line != "H" && line != "I")
                {
                    MsgBox.Show("MSG_ERR_027".Translation(), MsgType.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtModel.Text.Trim().ToUpper()))
                {
                    MsgBox.Show("MSG_ERR_037".Translation(), MsgType.Warning);
                    return;
                }

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT015.PUT_ITEM"
                    , new string[] { "A_LINE",
                        "A_MODEL",
                        "A_POINT",
                        "A_BLOCK",
                        "A_CYCLE_TIME",
                        "A_DAY_CAPA",
                        "A_MONTH_CAPA",
                        "A_YEAR_MONTH"
                    }
                    , new string[] { 
                        txtLine.EditValue.NullString().ToUpper(),
                        txtModel.EditValue.NullString().ToUpper(),
                        txtPoint.EditValue.NullString(),
                        txtBlock.EditValue.NullString(),
                        txtCycleTime.EditValue.NullString(),
                        txtDayCapa.EditValue.NullString(),
                        txtMonthCapa.EditValue.NullString(),
                        dtpYearMonth.DateTime.ToString("yyyyMM")
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    txtLine.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtPoint.Text = string.Empty;
                    txtBlock.Text = string.Empty;
                    txtCycleTime.Text = string.Empty;
                    txtDayCapa.Text = string.Empty;
                    txtMonthCapa.Text = string.Empty;
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


        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    txtLine.EditValue = gvList.GetDataRow(e.RowHandle)["LINE"].NullString();
                    txtModel.EditValue = gvList.GetDataRow(e.RowHandle)["MODEL"].NullString();
                    txtPoint.EditValue = float.Parse(gvList.GetDataRow(e.RowHandle)["POINT"].NullString()).ToString();
                    txtBlock.EditValue = float.Parse(gvList.GetDataRow(e.RowHandle)["BLOCK"].NullString()).ToString();
                    txtCycleTime.EditValue = float.Parse(gvList.GetDataRow(e.RowHandle)["CYCLE_TIME"].NullString()).ToString();
                    txtDayCapa.EditValue = float.Parse(gvList.GetDataRow(e.RowHandle)["DAY_CAPA"].NullString()).ToString();
                    txtMonthCapa.EditValue = float.Parse(gvList.GetDataRow(e.RowHandle)["MONTH_CAPA"].NullString()).ToString();

                    //if (string.IsNullOrWhiteSpace(gvList.GetDataRow(e.RowHandle)["PRICE"].NullString()))
                    //{
                    //    txtPoint.EditValue = string.Empty;
                    //}
                    //else
                    //{
                    //    float price = float.Parse(gvList.GetDataRow(e.RowHandle)["PRICE"].NullString());
                    //    txtPoint.EditValue = price.ToString("#,##0.00");
                    //}
                    //txtPoint.Focus();
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
                string Year = dtpYearMonth.DateTime.Year.ToString();
                string Month = dtpYearMonth.DateTime.Month.ToString();
                if(Month.Length == 1)
                {
                    Month = "0" + Month;
                }
                if (!GetExcelFileName(ref fileName)) return;
                var pop = new POP_SMT015(Year, Month, fileName);
                if (pop.ShowDialog() == DialogResult.OK)
                    SearchPage();
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
