using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT.POP
{
    public partial class PURCHASE_REQUEST_DETAIL : FormType
    {
        public string Mode { get; set; }
        public string PRCode { get; set; }
        public string Status { get; set; }
        public string MRPCode { get; set; }

        public DataTable DataItem { get; set; }

        public PURCHASE_REQUEST_DETAIL()
        {
            InitializeComponent();
            CreateDataItem();
        }

        private void btnCreatePRCode_Click(object sender, EventArgs e)
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_BUSINESS_PR.CREATE_PR_CODE",
                 new string[] { "A_DEPARTMENT" },
                 new string[] { Consts.DEPARTMENT });

                if (mResultDB.ReturnInt == 0)
                {
                    txtPR_Code.EditValue = mResultDB.ReturnDataSet.Tables[0].Rows[0]["CODE"].NullString();
                }
                else
                {
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void PURCHASE_REQUEST_DETAIL_Load(object sender, EventArgs e)
        {
            Classes.Common.SetFormIdToButton(null, "PURCHASE_REQUEST_DETAIL", this);

            Init();

            btnSave.Enabled = false;
            txtPR_Code.Enabled = false;
            groupControl1.Text += " [" + Mode + "]";

            if (Mode == Consts.MODE_NEW)
            {
                stlStatus.EditValue = Consts.STATUS_NEW;
                btnCreatePRCode.Enabled = true;
                togetVerify.EditValue = false;
                dateCreate.EditValue = DateTime.Now;
            }
            else
            {
                stlStatus.EditValue = Status;
                txtPR_Code.EditValue = PRCode;
                stlMRP_Code.EditValue = MRPCode;
                btnCreatePRCode.Enabled = false;
                togetVerify.EditValue = true;

                if (Mode == Consts.MODE_VIEW)
                {
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    stlMRP_Code.Enabled = false;
                }
                else if (Mode == Consts.MODE_UPDATE)
                {
                    togetVerify.EditValue = false;
                    stlMRP_Code.Enabled = false;
                }
            }
        }
        private void Init()
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_BUSINESS_PR.INIT",
                 new string[] { "A_DEPARTMENT" },
                 new string[] { Consts.DEPARTMENT });

                if (mResultDB.ReturnInt == 0)
                {
                    mBindData.BindGridLookEdit(stlStatus, mResultDB.ReturnDataSet.Tables[0], "CODE", "NAME");
                    mBindData.BindGridLookEdit(stlMRP_Code, mResultDB.ReturnDataSet.Tables[1], "MRP_CODE", "TITLE");

                    repositoryItemGridLookUpEdit1.DataSource = mResultDB.ReturnDataSet.Tables[2];
                    repositoryItemGridLookUpEdit1.DisplayMember = "NAME";
                    repositoryItemGridLookUpEdit1.ValueMember = "CODE";
                    repositoryItemGridLookUpEdit1.BeforePopup += RepositoryItemGridLookUpEdit1_BeforePopup; ;
                    gvList.Columns["UNIT"].ColumnEdit = repositoryItemGridLookUpEdit1;

                    repositoryItemGridLookUpEdit2.DataSource = mResultDB.ReturnDataSet.Tables[3];
                    repositoryItemGridLookUpEdit2.DisplayMember = "NAME";
                    repositoryItemGridLookUpEdit2.ValueMember = "CODE";
                    gvList.Columns["VENDOR_ID"].ColumnEdit = repositoryItemGridLookUpEdit2;
                }
                else
                {
                    MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void RepositoryItemGridLookUpEdit1_BeforePopup(object sender, EventArgs e)
        {
        }

        private void GetData()
        {
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_BUSINESS_PR.GET_MRP_LIST",
                new string[] { "A_DEPARTMENT", "A_MRP_CODE", "A_PR_CODE" },
                new string[] { Consts.DEPARTMENT, stlMRP_Code.EditValue.NullString(), txtPR_Code.EditValue.NullString() });

                if (mResultDB.ReturnInt == 0)
                {
                    DataTableCollection datas = mResultDB.ReturnDataSet.Tables;
                    //mBindData.BindGridView(gcList, mResultDB.ReturnDataSet.Tables[0]);
                    gcList.DataSource = datas[0];
                    InputDataFromGrid(datas[0]);
                    FormatGrid();

                    if (datas[1].Rows.Count > 0 && Mode != Consts.MODE_NEW)
                    {
                        txtTotalMoney.EditValue = datas[1].Rows[0]["TOTAL_VALUE"].NullString();
                        txtTotalMoney_US.EditValue = datas[1].Rows[0]["TOTAL_VALUE_US"].NullString();
                        txtPurposeBuy.EditValue = datas[1].Rows[0]["PURPOSE_OF_BUYING"].NullString();
                        dateDelivery.EditValue = datas[1].Rows[0]["DATE_NEED_FINISH"].NullString();
                        dateCreate.EditValue = datas[1].Rows[0]["DATE_CREATE"].NullString();

                        txtTotalMoney.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        txtTotalMoney.Properties.DisplayFormat.FormatString = "c2";
                        txtTotalMoney_US.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        txtTotalMoney_US.Properties.DisplayFormat.FormatString = "c2";
                    }
                }
                else
                {
                    mBindData.BindGridView(gcList, new DataTable());
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void InputDataFromGrid(DataTable data)
        {
            DataItem.Rows.Clear();

            DataRow newRow;
            foreach (DataRow row in data.Rows)
            {
                newRow = DataItem.NewRow();
                newRow["MRP_CODE"] = stlMRP_Code.EditValue.NullString();
                newRow["SPAREPART_CODE"] = row["SPAREPART_CODE"];
                newRow["DEPT_CODE"] = Consts.DEPARTMENT;
                newRow["QUANTITY_NEED_BUY"] = row["QUANTITY_NEED_BUY"];
                newRow["UNIT"] = row["UNIT"];
                newRow["EXPECTED_PRICE_VN"] = row["EXPECTED_PRICE_VN"];
                newRow["PRICE_VN"] = row["PRICE_VN"];
                newRow["TOTAL_MONEY_VN"] = row["TOTAL_MONEY_VN"];
                newRow["VENDOR_ID"] = row["VENDOR_ID"];
                newRow["PR_CODE"] = txtPR_Code.EditValue.NullString();
                newRow["DATE_NEED_FINISH"] = row["DATE_NEED_FINISH"];
                newRow["USER_UPDATE"] = Consts.USER_INFO.Id;
                newRow["DATE_UPDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
                newRow["DATE_CREATE"] = row["DATE_CREATE"];
                newRow["STATUS"] = stlStatus.EditValue.NullString();
                newRow["EXPECTED_PRICE_US"] = row["EXPECTED_PRICE_US"];
                newRow["PRICE_US"] = row["PRICE_US"];
                newRow["TOTAL_MONEY_US"] = row["TOTAL_MONEY_US"];
                newRow["DATE_END_ACTUAL"] = row["DATE_END_ACTUAL"];
                newRow["STT_MRP"] = row["STT_MRP"];

                DataItem.Rows.Add(newRow);
            }
        }

        private void FormatGrid(bool EditAble = true)
        {
            gvList.OptionsView.ColumnAutoWidth = true;
            gvList.OptionsBehavior.Editable = EditAble;
            gvList.Columns["GL_ACCOUNT"].OptionsColumn.AllowEdit = false;
            gvList.Columns["COST_CTR"].OptionsColumn.AllowEdit = false;
            gvList.Columns["NAME_VI"].OptionsColumn.AllowEdit = false;
            gvList.Columns["NAME_KR"].OptionsColumn.AllowEdit = false;
            gvList.Columns["IMAGE64"].OptionsColumn.AllowEdit = false;
            gvList.Columns["DELETE"].OptionsColumn.AllowEdit = false;
            gvList.Columns["SPAREPART_CODE"].OptionsColumn.AllowEdit = false;
            gvList.Columns["RATE"].OptionsColumn.AllowEdit = false;

            gvList.Columns["EXPECTED_PRICE_VN"].OptionsColumn.AllowEdit = false;
            gvList.Columns["EXPECTED_PRICE_US"].OptionsColumn.AllowEdit = false;
            gvList.Columns["PRICE_VN"].OptionsColumn.AllowEdit = false;
            gvList.Columns["PRICE_US"].OptionsColumn.AllowEdit = false;
            gvList.Columns["TOTAL_MONEY_VN"].OptionsColumn.AllowEdit = false;
            gvList.Columns["TOTAL_MONEY_US"].OptionsColumn.AllowEdit = false;

            gvList.Columns["STT_MRP"].Visible = false;
            gvList.Columns["DATE_END_ACTUAL"].Visible = false;
            gvList.Columns["DATE_CREATE"].Visible = false;

            gvList.Columns["EXPECTED_PRICE_VN"].SummaryItem.DisplayFormat = "{0:n3}";
            gvList.Columns["EXPECTED_PRICE_US"].SummaryItem.DisplayFormat = "{0:n3}";
            gvList.Columns["PRICE_US"].SummaryItem.DisplayFormat = "{0:n3}";
            gvList.Columns["TOTAL_MONEY_VN"].SummaryItem.DisplayFormat = "{0:n3}";
            gvList.Columns["TOTAL_MONEY_US"].SummaryItem.DisplayFormat = "{0:n3}";
            gvList.Columns["PRICE_VN"].SummaryItem.DisplayFormat = "{0:n3}";

            gvList.Columns["QUANTITY_NEED_BUY"].SummaryItem.FieldName = "QUANTITY_NEED_BUY";
            gvList.Columns["QUANTITY_NEED_BUY"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gvList.Columns["QUANTITY_NEED_BUY"].SummaryItem.DisplayFormat = "{0:n3}";
        }

        private void stlMRP_Code_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void gvList_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "IMAGE64" && e.IsGetData)
            {
                string imgBase64 = ((DataRowView)e.Row)["IMAGE"].NullString();
                e.Value = Wisol.MES.Classes.Common.GetImage(imgBase64);
            }
        }

        private void CreateDataItem()
        {
            DataItem = new DataTable();
            DataItem.Columns.Add("MRP_CODE");
            DataItem.Columns.Add("SPAREPART_CODE");
            DataItem.Columns.Add("DEPT_CODE");
            DataItem.Columns.Add("QUANTITY_NEED_BUY");
            DataItem.Columns.Add("UNIT");
            DataItem.Columns.Add("EXPECTED_PRICE_VN");
            DataItem.Columns.Add("PRICE_VN");
            DataItem.Columns.Add("TOTAL_MONEY_VN");
            DataItem.Columns.Add("VENDOR_ID");
            DataItem.Columns.Add("PR_CODE");
            DataItem.Columns.Add("DATE_NEED_FINISH");
            DataItem.Columns.Add("USER_UPDATE");
            DataItem.Columns.Add("DATE_UPDATE");
            DataItem.Columns.Add("DATE_CREATE");
            DataItem.Columns.Add("STATUS");
            DataItem.Columns.Add("EXPECTED_PRICE_US");
            DataItem.Columns.Add("PRICE_US");
            DataItem.Columns.Add("TOTAL_MONEY_US");
            DataItem.Columns.Add("DATE_END_ACTUAL");
            DataItem.Columns.Add("STT_MRP");
            DataItem.Columns.Add("IS_DELETE");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dateCreate.EditValue.NullString()) ||
                    string.IsNullOrEmpty(txtPR_Code.EditValue.NullString()) ||
                    string.IsNullOrEmpty(stlStatus.EditValue.NullString()) ||
                    string.IsNullOrEmpty(stlMRP_Code.EditValue.NullString()) ||
                    string.IsNullOrEmpty(dateDelivery.EditValue.NullString()) || DataItem.Rows.Count == DeleteItem.Count)
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }

                int count = 0;
                if (DataItem.Rows.Count > 0)
                {
                    foreach (DataRow row in DataItem.Rows)
                    {
                        if (DeleteItem.Contains(row["STT_MRP"].NullString()))
                        {
                            count++;
                            row["IS_DELETE"] = "1";
                        }
                        else
                        {
                            row["IS_DELETE"] = "0";
                        }

                        row["PR_CODE"] = txtPR_Code.EditValue.NullString();
                        row["STATUS"] = stlStatus.EditValue.NullString();
                        row["MRP_CODE"] = stlMRP_Code.EditValue.NullString();
                        row["DATE_UPDATE"] = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    DataItem.AcceptChanges();
                }

                if (DataItem.Rows.Count == count)
                {
                    MsgBox.Show("HÃY CHỌN THIẾT BỊ ĐỂ TẠO MUA HÀNG", MsgType.Warning);
                    return;
                }

                DialogResult dialogResult = MsgBox.Show("CONFIRM_UPDATE".Translation(), MsgType.Information, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    base.mResultDB = base.mDBaccess.ExcuteProcWithTableParam("PKG_BUSINESS_PR.PUT_DETAIL",
                                                  new string[]
                                                  {                 "A_DATE",
                                                                     "A_PR_CODE",
                                                                     "A_STATUS",
                                                                     "A_MRP_CODE",
                                                                     "A_DATE_NEED_FINISH",
                                                                     "A_TOTAL_VALUE_VN",
                                                                     "A_TOTAL_VALUE_USD",
                                                                     "A_PURPOSE_OF_BUY",
                                                                     "A_DEPARTMENT",
                                                                     "A_USER"
                                                  },
                                                  "A_DATA",
                                                  new string[]
                                                  {
                                                                     dateCreate.EditValue.NullString(),
                                                                     txtPR_Code.EditValue.NullString(),
                                                                     stlStatus.EditValue.NullString(),
                                                                     stlMRP_Code.EditValue.NullString(),
                                                                     dateDelivery.EditValue.NullString(),
                                                                     txtTotalMoney.EditValue.NullString(),
                                                                     txtTotalMoney_US.EditValue.NullString(),
                                                                     txtPurposeBuy.EditValue.NullString(),
                                                                     Consts.DEPARTMENT,
                                                                     Consts.USER_INFO.Id
                                                  },
                                                  DataItem);
                    if (mResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Information);
                        this.Close();
                    }
                    else
                    {
                        MsgBox.Show(mResultDB.ReturnString.Translation(), MsgType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void gvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string stt = gvList.GetRowCellValue(e.RowHandle, gvList.Columns["STT_MRP"]).NullString();
            string code = gvList.GetRowCellValue(e.RowHandle, gvList.Columns["SPAREPART_CODE"]).NullString();
            var row = DataItem.Select().FirstOrDefault(x => x["STT_MRP"].NullString() == stt && x["SPAREPART_CODE"].NullString() == code);
            if (row == null) return;

            if (e.Column.FieldName == "QUANTITY_NEED_BUY")
            {
                row["QUANTITY_NEED_BUY"] = e.Value.NullString();

                string priceEx = gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_VN").NullString();
                string price = gvList.GetRowCellValue(e.RowHandle, "PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "PRICE_VN").NullString();
                string priceNew = price == "0" ? priceEx : price;

                string priceExUS = gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_US").NullString();
                string priceUS = gvList.GetRowCellValue(e.RowHandle, "PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "PRICE_US").NullString();
                string priceNewUS = priceUS == "0" ? priceExUS : priceUS;

                SetValueForGridViewPrice(e.Value.NullString(), priceNew, "TOTAL_MONEY_VN", e.RowHandle);
                SetValueForGridViewPrice(e.Value.NullString(), priceNewUS, "TOTAL_MONEY_US", e.RowHandle);
            }
            else if (e.Column.FieldName == "UNIT")
            {
                float rate = Classes.Common.ConvertUnit(row["UNIT"].NullString(), e.Value.NullString(), code);
                row["UNIT"] = e.Value.NullString();
                row["PRICE_VN"] = double.Parse(row["PRICE_VN"].NullString()) / rate;
                row["PRICE_US"] = double.Parse(row["PRICE_US"].NullString()) / rate;

                gvList.SetRowCellValue(e.RowHandle, "PRICE_VN", row["PRICE_VN"]);
                gvList.SetRowCellValue(e.RowHandle, "EXPECTED_PRICE_VN", row["PRICE_VN"]);

                gvList.SetRowCellValue(e.RowHandle, "PRICE_US", row["PRICE_US"]);
                gvList.SetRowCellValue(e.RowHandle, "EXPECTED_PRICE_US", row["PRICE_US"]);
            }
            else if (e.Column.FieldName == "EXPECTED_PRICE_VN")
            {
                row["EXPECTED_PRICE_VN"] = e.Value.NullString();

                string priceEx = e.Value.NullString() == string.Empty ? "0" : e.Value.NullString();
                string price = gvList.GetRowCellValue(e.RowHandle, "PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "PRICE_VN").NullString();
                string priceNew = price == "0" ? priceEx : price;
                string quantity = gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString();

                SetValueForGridViewPrice(quantity, priceNew, "TOTAL_MONEY_VN", e.RowHandle);
            }
            else if (e.Column.FieldName == "PRICE_VN")
            {
                row["PRICE_VN"] = e.Value.NullString();

                string priceEx = gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_VN").NullString();
                string price = e.Value.NullString() == string.Empty ? "0" : e.Value.NullString();
                string priceNew = price == "0" ? priceEx : price;

                string quantity = gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString();
                SetValueForGridViewPrice(quantity, priceNew, "TOTAL_MONEY_VN", e.RowHandle);
            }
            else if (e.Column.FieldName == "EXPECTED_PRICE_US")
            {
                row["EXPECTED_PRICE_US"] = e.Value.NullString();

                string priceExUS = e.Value.NullString() == string.Empty ? "0" : e.Value.NullString(); ;
                string priceUS = gvList.GetRowCellValue(e.RowHandle, "PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "PRICE_US").NullString();
                string priceNewUS = priceUS == "0" ? priceExUS : priceUS;
                string quantity = gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString();

                SetValueForGridViewPrice(quantity, priceNewUS, "TOTAL_MONEY_US", e.RowHandle);
            }
            else if (e.Column.FieldName == "PRICE_US")
            {
                row["PRICE_US"] = e.Value.NullString();

                string priceExUS = gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "EXPECTED_PRICE_US").NullString();
                string priceUS = e.Value.NullString() == string.Empty ? "0" : e.Value.NullString();
                string priceNewUS = priceUS == "0" ? priceExUS : priceUS;
                string quantity = gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(e.RowHandle, "QUANTITY_NEED_BUY").NullString();

                SetValueForGridViewPrice(quantity, priceNewUS, "TOTAL_MONEY_US", e.RowHandle);
            }
            else if (e.Column.FieldName == "VENDOR_ID")
            {
                row["VENDOR_ID"] = e.Value.NullString();
            }
            else if (e.Column.FieldName == "DATE_NEED_FINISH")
            {
                row["DATE_NEED_FINISH"] = e.Value.NullString();
            }
        }

        private void SetValueForGridViewPrice(string quantiy, string price, string columnName, int rowHandle)
        {
            if (quantiy == string.Empty || !float.TryParse(quantiy, out _)) quantiy = "0";
            if (price == string.Empty || !float.TryParse(price, out _)) price = "0";

            gvList.SetRowCellValue(rowHandle, columnName, float.Parse(quantiy) * float.Parse(price));
        }

        private void toggleSwitch1_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = (bool)togetVerify.EditValue;

                FormatGrid(!(bool)togetVerify.EditValue);

                double total_VN = 0;
                double total_US = 0;
                string expectVN;
                string expectUS;
                string priceVN;
                string priceUS;
                string quantity;
                DateTime maxDateTime = new DateTime();
                string dateNeedFinish;
                for (int i = 0; i < gvList.RowCount; i++)
                {
                    expectVN = (gvList.GetRowCellValue(i, "EXPECTED_PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(i, "EXPECTED_PRICE_VN").NullString());
                    expectUS = gvList.GetRowCellValue(i, "EXPECTED_PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(i, "EXPECTED_PRICE_US").NullString();
                    priceVN = gvList.GetRowCellValue(i, "PRICE_VN").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(i, "PRICE_VN").NullString();
                    priceUS = gvList.GetRowCellValue(i, "PRICE_US").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(i, "PRICE_US").NullString();
                    quantity = gvList.GetRowCellValue(i, "QUANTITY_NEED_BUY").NullString() == string.Empty ? "0" : gvList.GetRowCellValue(i, "QUANTITY_NEED_BUY").NullString();

                    string stt = gvList.GetRowCellValue(i, "STT_MRP").NullString();
                    if (!DeleteItem.Contains(stt))
                    {
                        total_VN += ((priceVN == "0" ? double.Parse(expectVN) : double.Parse(priceVN)) * double.Parse(quantity));
                        total_US += ((priceUS == "0" ? double.Parse(expectUS) : double.Parse(priceUS)) * double.Parse(quantity));

                        dateNeedFinish = gvList.GetRowCellValue(i, "DATE_NEED_FINISH").NullString();

                        if (DateTime.TryParse(dateNeedFinish, out DateTime outDate))
                        {
                            if (maxDateTime <= outDate) maxDateTime = outDate;
                        }
                    }
                }

                txtTotalMoney.EditValue = total_VN;
                txtTotalMoney_US.EditValue = total_US;
                dateDelivery.EditValue = maxDateTime;

                txtTotalMoney.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtTotalMoney.Properties.DisplayFormat.FormatString = "c2";
                txtTotalMoney_US.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtTotalMoney_US.Properties.DisplayFormat.FormatString = "c2";
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private List<string> DeleteItem = new List<string>();
        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName == "DELETE" && !(bool)togetVerify.EditValue)
            {
                string stt = gvList.GetRowCellValue(e.RowHandle, "STT_MRP").NullString();
                if (DeleteItem.Contains(stt))
                {
                    DeleteItem.Remove(stt);
                }
                else
                {
                    DeleteItem.Add(stt);
                }
            }
        }

        private void gvList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string stt = gvList.GetRowCellValue(e.RowHandle, "STT_MRP").NullString();
            if (DeleteItem.Contains(stt))
            {
                e.Appearance.BackColor = Color.FromArgb(192, 192, 192);
            }
            else
            {
                if (e.Column.FieldName == "TOTAL_MONEY_VN" || e.Column.FieldName == "TOTAL_MONEY_US")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 229, 204);
                }
                else
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 204);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MsgBox.Show("NHẤN OK ĐỂ TIẾP TỤC", MsgType.Warning, Components.DialogType.OkCancel);
                if (dialogResult == DialogResult.OK)
                {
                    stlMRP_Code.EditValue = null;
                    txtPR_Code.EditValue = null;
                    stlStatus.EditValue = null;
                    txtTotalMoney.EditValue = null;
                    txtTotalMoney_US.EditValue = null;
                    txtPurposeBuy.EditValue = null;
                    togetVerify.EditValue = false;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private int rowHandleUnit;
        private void gvList_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "UNIT")
            {
                rowHandleUnit = e.RowHandle;
            }
        }
    }
}
