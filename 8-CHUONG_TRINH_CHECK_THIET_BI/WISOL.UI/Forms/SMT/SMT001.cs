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
    public partial class SMT001 : PageType
    {
        public SMT001()
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT001.INT_LIST"
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

            if (Consts.USER_INFO.Id.ToUpper() == "H2002001")
            {
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            base.InitializePage();
        }

        public override void SearchPage()
        {
            base.SearchPage();
            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT001.GET_LIST",
                    new string[] { "A_PLANT", "A_YEAR_MONTH", "A_USER" },
                    new string[] { Consts.PLANT, dtpYearMonth.DateTime.ToString("yyyyMM"), Consts.USER_INFO.Id }
                    );
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

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT001.PUT_ITEM"
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
                        dtpYearMonth.DateTime.ToString("yyyyMM"),
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
                string Year = dtpYearMonth.DateTime.Year.ToString();
                string Month = dtpYearMonth.DateTime.Month.ToString();
                if(Month.Length == 1)
                {
                    Month = "0" + Month;
                }
                if (!GetExcelFileName(ref fileName)) return;
                var pop = new POP_SMT001(Year, Month, fileName);
                if (pop.ShowDialog() == DialogResult.OK)
                    SearchPage();
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }

        private void btnUpNewMaterial_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = new DataTable();
                
            //    for (int i = 0; i < 7; i++)
            //    {
            //        using (SqlConnection conn = new SqlConnection(Program.connectionString))
            //        {
            //            StringBuilder strSqlString = new StringBuilder();

            //            strSqlString.AppendFormat("select t1.PART_NUMBER,  t1.DTTIME  from \n");
            //            strSqlString.AppendFormat(" ( \n");
            //            strSqlString.AppendFormat(" SELECT convert(varchar(6), DTTIME, 112) AS DTTIME, \n");
            //            strSqlString.AppendFormat(" right(PARTNUMBER.strPartNumber, charindex('\\', reverse(PARTNUMBER.strPartNumber)) - 1) as PART_NUMBER \n");
            //            strSqlString.AppendFormat(" FROM \n");
            //            strSqlString.AppendFormat(" [PCCLIENT" + i + "].SiplaceOIS.dbo.PARTNUMBER \n");
            //            strSqlString.AppendFormat(" inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.Compposition on PARTNUMBER.lPartNumber = Compposition.lPartNumber \n");
            //            strSqlString.AppendFormat(" inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.compDetail  on compDetail.lIdPosition = Compposition.lIdPosition \n");
            //            strSqlString.AppendFormat(" inner join [PCCLIENT" + i + "].SiplaceOIS.dbo.compBlock  on compBlock.lIdBlock = compDetail.lIdBlock \n");
            //            strSqlString.AppendFormat(" WHERE \n");
            //            //strSqlString.AppendFormat(" compBlock.dttime >= DATEADD(month, DATEDIFF(month, 0, getdate()), 0)");
            //            strSqlString.AppendFormat(" compBlock.dttime >= convert(datetime, dateadd(day,-3, cast(getdate() as date)))");
            //            //strSqlString.AppendFormat(" and YEAR(compBlock.dttime) = YEAR(GETDATE())");
            //            //strSqlString.AppendFormat(" and MONTH(compBlock.dttime) = MONTH(GETDATE())");
            //            strSqlString.AppendFormat(" group by PARTNUMBER.strPartNumber, DTTIME \n");
            //            strSqlString.AppendFormat(" ) t1 \n");
            //            strSqlString.AppendFormat(" group by t1.PART_NUMBER, t1.DTTIME \n");
            //            new SqlDataAdapter(strSqlString.ToString(), conn).Fill(dt);
            //        }
            //    }

            //    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT001.PUT_NEW_ITEM",
            //            new string[]
            //            {
            //            "A_PLANT",
            //            "A_XML",
            //            "A_TRAN_USER_ID"
            //            },
            //            new string[]
            //            {
            //            Consts.PLANT,
            //            Converter.GetDataTableToXml(dt),
            //            Consts.USER_INFO.Id
            //            }
            //        );
            //    if (base.m_ResultDB.ReturnInt == 0)
            //    {
            //        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
            //        SearchPage();
            //    }
            //    else
            //    {
            //        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Warning);
            //    }
            //}
            //catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }
}
