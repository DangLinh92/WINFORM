using DevExpress.Export;
using DevExpress.Printing.ExportHelpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SETTING
{
    public partial class SETTING007 : PageType
    {
        private string b64 = string.Empty;
        private bool onRemove = false;
        private string itemImage = string.Empty;
        private string costId = string.Empty;
        public SETTING007()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();

            Classes.Common.SetFormIdToButton(this, "SETTING007");
        }
        public override void InitializePage()
        {
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                dtOpenDate.EditValue = firstDayOfMonth.ToString("yyyy-MM-dd");
                dtCloseDate.EditValue = lastDayOfMonth.ToString("yyyy-MM-dd");

                dtImportDate.EditValue = date.ToString("yyyy-MM-dd");

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.INT_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_OPEN_DATE", "A_CLOSE_DATE" }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT, dtOpenDate.DateTime.ToString("yyyyMMdd"), dtCloseDate.DateTime.ToString("yyyyMMdd") }
                    );
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    Init_Control(true);
                    base.m_BindData.BindGridLookEdit(sltLocation, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME_LOCATION");
                    gvList.Columns["CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["NAME_MATERIAL"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["UNIT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["COST_ID"].Visible = false;

                    gvList.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["COST"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["OPENING_STOCK"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["OPENING_STOCK"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["STOCK_IN"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["STOCK_IN"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["STOCK_OUT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["STOCK_OUT"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["CLOSING_STOCK"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CLOSING_STOCK"].DisplayFormat.FormatString = "n0";
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

                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.GET_LIST"
                    , new string[] { "A_PLANT", "A_DEPARTMENT", "A_OPEN_DATE", "A_CLOSE_DATE"
                    }
                    , new string[] { Consts.PLANT, Consts.DEPARTMENT, dtOpenDate.DateTime.ToString("yyyyMMdd"), dtCloseDate.DateTime.ToString("yyyyMMdd")
                    }
                    ); ;
                if (base.m_ResultDB.ReturnInt == 0)
                {
                    base.m_BindData.BindGridView(gcList,
                        base.m_ResultDB.ReturnDataSet.Tables[0]
                        );
                    Init_Control(true);
                    base.m_BindData.BindGridLookEdit(sltLocation, base.m_ResultDB.ReturnDataSet.Tables[1], "CODE", "NAME_LOCATION");
                    gvList.Columns["CODE"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["NAME_MATERIAL"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["UNIT"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvList.Columns["COST_ID"].Visible = false;

                    gvList.Columns["COST"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["COST"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["OPENING_STOCK"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["OPENING_STOCK"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["STOCK_IN"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["STOCK_IN"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["STOCK_OUT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["STOCK_OUT"].DisplayFormat.FormatString = "n0";
                    gvList.Columns["CLOSING_STOCK"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvList.Columns["CLOSING_STOCK"].DisplayFormat.FormatString = "n0";
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
                txtCode.EditValue = string.Empty;
                txtName.EditValue = string.Empty;
                txtCost.EditValue = string.Empty;
                txtDesc.EditValue = string.Empty;
                txtUnit.EditValue = string.Empty;
                txtQuantity.EditValue = string.Empty;
                sltLocation.EditValue = string.Empty;
                picImage.Image = null;
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
                if (string.IsNullOrEmpty(txtName.EditValue.NullString()) || string.IsNullOrEmpty(txtCost.EditValue.NullString()) || string.IsNullOrEmpty(txtUnit.EditValue.NullString()) || string.IsNullOrEmpty(txtQuantity.EditValue.NullString())
                   )
                {
                    MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                    return;
                }
                if (onRemove || itemImage == string.Empty)
                {
                    b64 = string.Empty;
                }
                else b64 = itemImage;
                string importDate = string.Empty;
                if (!string.IsNullOrWhiteSpace(dtImportDate.EditValue.NullString()))
                {
                    importDate = dtImportDate.DateTime.ToString("yyyyMMdd");
                }
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.PUT_ITEM"
                    , new string[] { "A_CODE",
                        "A_COSTID",
                        "A_NAME",
                        "A_COST",
                        "A_UNIT",
                        "A_DESC",
                        "A_QUANTITY",
                        "A_IMAGE",
                        "A_VALID_DATE",
                        "A_LOCATION",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        txtCode.EditValue.NullString(),
                        costId,
                        txtName.EditValue.NullString(),
                        txtCost.EditValue.NullString(),
                        txtUnit.EditValue.NullString().ToUpper(),
                        txtDesc.EditValue.NullString(),
                        txtQuantity.EditValue.NullString(),
                        b64,
                        importDate,
                        sltLocation.EditValue.NullString(),
                        Consts.USER_INFO.Id,
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.EditValue = string.Empty;
            txtName.EditValue = string.Empty;
            costId = string.Empty;
            txtCost.EditValue = string.Empty;
            txtDesc.EditValue = string.Empty;
            txtUnit.EditValue = string.Empty;
            txtQuantity.EditValue = string.Empty;
            sltLocation.EditValue = string.Empty;
            picImage.Image = null;
            onRemove = false;
            itemImage = string.Empty;
        }

        private void btnDestroy_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                return;
            }

            string id = txtCode.Text.Trim();
            DialogResult dialogResult = MsgBox.Show("MSG_COM_015".Translation(), MsgType.Warning, DialogType.OkCancel);
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.DELETE_ITEM"
                        , new string[] { "A_CODE", "A_COSTID", "A_TRAN_USER"
                        }
                        , new string[] { id, costId, Consts.USER_INFO.Id
                        }
                        );
                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
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
                SearchPage();
            }
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            onRemove = false;
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            openFileDialog.Title = "Save an Image File";
            openFileDialog.ShowDialog();
            b64 = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
            itemImage = b64;
            picImage.Image = Image.FromFile(openFileDialog.FileName);
        }

        private void btnClearImg_Click(object sender, EventArgs e)
        {
            b64 = string.Empty;
            picImage.Image = null;
            onRemove = true;
            itemImage = string.Empty;
        }

        private void gvList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;
                else
                {
                    try
                    {
                        costId = gvList.GetDataRow(e.RowHandle)["COST_ID"].NullString();
                        base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SETTING007.GET_ITEM_DETAIL"
                            , new string[] { "A_CODE", "A_COSTID"
                            }
                            , new string[] { gvList.GetDataRow(e.RowHandle)["CODE"].NullString(), costId
                            }
                            );
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            DataTable dt = base.m_ResultDB.ReturnDataSet.Tables[0];
                            txtCode.EditValue = dt.Rows[0]["CODE"].NullString();
                            txtName.EditValue = dt.Rows[0]["NAME"].NullString();
                            txtCost.EditValue = dt.Rows[0]["COST"].NullString();
                            txtUnit.EditValue = dt.Rows[0]["UNIT"].NullString();
                            txtDesc.EditValue = dt.Rows[0]["DESC"].NullString();
                            txtQuantity.EditValue = dt.Rows[0]["QUANTITY"].NullString();
                            sltLocation.EditValue = dt.Rows[0]["LOCATION"].NullString();

                            itemImage = dt.Rows[0]["IMAGE"].NullString();

                            picImage.Image = null;
                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["IMAGE"].NullString()))
                            {
                                byte[] imagebytes = Convert.FromBase64String(dt.Rows[0]["IMAGE"].NullString());
                                using (var ms = new MemoryStream(imagebytes, 0, imagebytes.Length))
                                {
                                    picImage.Image = Image.FromStream(ms, true);
                                }
                                picImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                picImage.Size = picImage.Image.Size;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void btnStockIn_Click_1(object sender, EventArgs e)
        {
            POP.POP_STOCKIN popup = new POP.POP_STOCKIN(ID: null);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void btnStockout_Click(object sender, EventArgs e)
        {
            POP.POP_STOCKOUT popup = new POP.POP_STOCKOUT(ID: null);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void gvList_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if (
                    (e.Column.FieldName == "CODE") && (e.Column.FieldName == "NAME") && (e.Column.FieldName == "UNIT")
                    )
                {
                    int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
                    int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

                    e.Merge = (value1 == value2);
                    e.Handled = true;

                    return;
                }
            }
            catch
            {

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure that the data-aware export mode is enabled.
                DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware;
                // Create a new object defining how a document is exported to the XLSX format.
                XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                // Subscribe to the CustomizeSheetHeader event. 
                options.CustomizeSheetHeader += options_CustomizeSheetHeader;
                // Export the grid data to the XLSX format.
                string file = "VatTu" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                gcList.ExportToXlsx(file, options);
                // Open the created document.
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
        private void options_CustomizeSheetHeader(DevExpress.Export.ContextEventArgs e)
        {
            DateTime date = DateTime.Now;

            // Create a new row.
            CellObject row = new CellObject();
            // Specify row values.
            row.Value = "BẢNG TỔNG HỢP NHẬP XUẤT TỒN KHO";
            // Specify row formatting.
            XlFormattingObject rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 20, Name = "Times New Roman" };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Center, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Add an empty row to the output document.
            e.ExportContext.AddRow();
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(9, 0)));

            CellObject row1 = new CellObject();
            row1.Value = "Từ ngày " + dtOpenDate.DateTime.ToString("dd") + " Tháng " + dtOpenDate.DateTime.ToString("MM") + " đến ngày " + dtCloseDate.DateTime.ToString("dd") + " tháng " + dtOpenDate.DateTime.ToString("MM") + " năm " + dtOpenDate.DateTime.ToString("yyyy");
            XlFormattingObject rowFormatting1 = new XlFormattingObject();
            rowFormatting1.Font = new XlCellFont { Bold = true, Size = 12, Name = "Times New Roman", Italic = true };
            rowFormatting1.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Center, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row1.Formatting = rowFormatting1;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row1 });
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 2), new DevExpress.Export.Xl.XlCellPosition(9, 2)));


        }

        private void dtCloseDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dtOpenDate.DateTime >= dtCloseDate.DateTime)
            {
                e.Cancel = true;
                dtCloseDate.Focus();
                dxErrorProvider1.SetError(dtCloseDate, "Date is invalid");
            }
            else
            {
                e.Cancel = false;
                dxErrorProvider1.ClearErrors();
            }

        }

        private void btnListDetail_Click(object sender, EventArgs e)
        {
            POP.POP_LISTDETAIL popup = new POP.POP_LISTDETAIL(ID: null);
            popup.ShowDialog();
            //this.SearchPage();
        }

        private void btnCancelStockIn_Click(object sender, EventArgs e)
        {
            POP.POP_CANCEL_STOCKIN popup = new POP.POP_CANCEL_STOCKIN(ID: null);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void btnCancelStockOut_Click(object sender, EventArgs e)
        {
            POP.POP_CANCEL_STOCKOUT popup = new POP.POP_CANCEL_STOCKOUT(ID: null);
            popup.ShowDialog();
            this.SearchPage();
        }

        private void picImage_EditValueChanged(object sender, EventArgs e)
        {
            System.Drawing.Image returnImage = null;
            if (Clipboard.ContainsImage())
            {
                onRemove = false;
                returnImage = Clipboard.GetImage();
                byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(returnImage, typeof(byte[]));
                itemImage = Convert.ToBase64String(bytes);
            }
                
        }
    }
}
