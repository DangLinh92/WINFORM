using System;
using System.Data;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.WLP1
{
    public partial class WLP1009 : PageType
    {
        public WLP1009()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            gvList.OptionsView.ShowFooter = false;
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatString = "n0";
        }



        public override void InitializePage()
        {
            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1009.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT
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
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1009.INT_LIST"
                    , new string[] { "A_PLANT", "A_LANG", "A_DEPARTMENT"
                    }
                    , new string[] { Consts.PLANT, Consts.USER_INFO.Language, Consts.DEPARTMENT
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
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns["LUONG_CHUA_NHAP"].DisplayFormat.FormatString = "n0";
            gvList.OptionsView.ShowFooter = false;
        }



        private void Init_Control(bool condFlag)
        {
            try
            {
                txtCode.EditValue = string.Empty;
                txtChemicalName.EditValue = string.Empty;
                txtDate1.EditValue = string.Empty;
                txtDate2.EditValue = string.Empty;
                txtDate3.EditValue = string.Empty;
                txtDate4.EditValue = string.Empty;
                txtDate5.EditValue = string.Empty;
                txtQuantity1.EditValue = string.Empty;
                txtQuantity2.EditValue = string.Empty;
                txtQuantity3.EditValue = string.Empty;
                txtQuantity4.EditValue = string.Empty;
                txtQuantity5.EditValue = string.Empty;
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
                DataTable dt = new DataTable();
                dt.Columns.Add("DATE_EXPECTED", typeof(string));
                dt.Columns.Add("QUANTITY", typeof(string));
                if(!string.IsNullOrWhiteSpace(txtDate1.EditValue.NullString()) && txtQuantity1.EditValue.NullString() != "0")
                {
                    dt.Rows.Add(new object[] { txtDate1.DateTime.ToString("yyyyMMdd"), txtQuantity1.EditValue.NullString() });
                }
                if (!string.IsNullOrWhiteSpace(txtDate2.EditValue.NullString()) && txtQuantity2.EditValue.NullString() != "0")
                {
                    dt.Rows.Add(new object[] { txtDate2.DateTime.ToString("yyyyMMdd"), txtQuantity2.EditValue.NullString() });
                }
                if (!string.IsNullOrWhiteSpace(txtDate3.EditValue.NullString()) && txtQuantity3.EditValue.NullString() != "0")
                {
                    dt.Rows.Add(new object[] { txtDate3.DateTime.ToString("yyyyMMdd"), txtQuantity3.EditValue.NullString() });
                }
                if (!string.IsNullOrWhiteSpace(txtDate4.EditValue.NullString()) && txtQuantity4.EditValue.NullString() != "0")
                {
                    dt.Rows.Add(new object[] { txtDate4.DateTime.ToString("yyyyMMdd"), txtQuantity4.EditValue.NullString() });
                }
                if (!string.IsNullOrWhiteSpace(txtDate5.EditValue.NullString()) && txtQuantity5.EditValue.NullString() != "0")
                {
                    dt.Rows.Add(new object[] { txtDate5.DateTime.ToString("yyyyMMdd"), txtQuantity5.EditValue.NullString() });
                }

                string XML = Converter.GetDataTableToXml(dt);

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1009.PUT_ITEM"
                    , new string[] { "A_CODE",
                        "A_XML",
                        "A_TRAN_USER",
                        "A_DEPARTMENT"
                    }
                    , new string[] { txtCode.Text.Trim(),
                        XML,
                        Consts.USER_INFO.Id,
                        Consts.DEPARTMENT
                    }
                    );

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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
                    this.Init_Control(true);
                    string code = gvList.GetDataRow(e.RowHandle)["CODE"].NullString();
                    txtCode.EditValue = code;
                    txtChemicalName.EditValue = gvList.GetDataRow(e.RowHandle)["NAME"].NullString();
                    string quantity = gvList.GetDataRow(e.RowHandle)["LUONG_CHUA_NHAP"].NullString();
                    if (!string.IsNullOrWhiteSpace(quantity))
                    {
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_WLP1009.GET_DETAIL"
                        , new string[] { "A_CODE", "A_DEPARTMENT"
                        }
                        , new string[] { code, Consts.DEPARTMENT
                        }
                        );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                            int count = dt.Rows.Count;
                            if(count == 1)
                            {
                                txtDate1.EditValue = dt.Rows[0][0].ToString();
                                txtQuantity1.EditValue = dt.Rows[0][1].ToString();
                            }
                            else if (count == 2)
                            {
                                txtDate1.EditValue = dt.Rows[0][0].ToString();
                                txtQuantity1.EditValue = dt.Rows[0][1].ToString();
                                txtDate2.EditValue = dt.Rows[1][0].ToString();
                                txtQuantity2.EditValue = dt.Rows[1][1].ToString();
                            }
                            else if (count == 3)
                            {
                                txtDate1.EditValue = dt.Rows[0][0].ToString();
                                txtQuantity1.EditValue = dt.Rows[0][1].ToString();
                                txtDate2.EditValue = dt.Rows[1][0].ToString();
                                txtQuantity2.EditValue = dt.Rows[1][1].ToString();
                                txtDate3.EditValue = dt.Rows[2][0].ToString();
                                txtQuantity3.EditValue = dt.Rows[2][1].ToString();
                            }
                            else if (count == 4)
                            {
                                txtDate1.EditValue = dt.Rows[0][0].ToString();
                                txtQuantity1.EditValue = dt.Rows[0][1].ToString();
                                txtDate2.EditValue = dt.Rows[1][0].ToString();
                                txtQuantity2.EditValue = dt.Rows[1][1].ToString();
                                txtDate3.EditValue = dt.Rows[2][0].ToString();
                                txtQuantity3.EditValue = dt.Rows[2][1].ToString();
                                txtDate4.EditValue = dt.Rows[3][0].ToString();
                                txtQuantity4.EditValue = dt.Rows[3][1].ToString();
                            }
                            else if (count == 5)
                            {
                                txtDate1.EditValue = dt.Rows[0][0].ToString();
                                txtQuantity1.EditValue = dt.Rows[0][1].ToString();
                                txtDate2.EditValue = dt.Rows[1][0].ToString();
                                txtQuantity2.EditValue = dt.Rows[1][1].ToString();
                                txtDate3.EditValue = dt.Rows[2][0].ToString();
                                txtQuantity3.EditValue = dt.Rows[2][1].ToString();
                                txtDate4.EditValue = dt.Rows[3][0].ToString();
                                txtQuantity4.EditValue = dt.Rows[3][1].ToString();
                                txtDate5.EditValue = dt.Rows[4][0].ToString();
                                txtQuantity5.EditValue = dt.Rows[4][1].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
