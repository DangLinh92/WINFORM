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
using Wisol.MES.Forms.SMT.POP;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.SMT
{
    public partial class SMT010 : PageType
    {
        public SMT010()
        {
            InitializeComponent();
        }
        public override void Form_Show()
        {
            base.Form_Show();

            this.InitializePage();
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }



        public override void InitializePage()
        {

            gvList.OptionsView.ShowFooter = false;

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT010.INT_LIST"
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
                    DataTable dtTemp = base.m_ResultDB.ReturnDataSet.Tables[1].Copy();
                    gleLoai.Text = string.Empty;
                    gleLoai.Properties.NullText = string.Empty;
                    DataRow dr = dtTemp.NewRow();
                    dtTemp.Rows.InsertAt(dr, 0);
                    dtTemp.AcceptChanges();
                    GridView gvView = gleLoai.Properties.View;

                    AddColumns(gvView, dtTemp, "");

                    gleLoai.Properties.DataSource = dtTemp.Copy();
                    gleLoai.Properties.ValueMember = "CODE";
                    gleLoai.Properties.DisplayMember = "CODE";
                    gleLoai.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

                    gvView.GridControl.Refresh();
                    gleLoai.Refresh();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            dtpFromDate.EditValue = new DateTime(DateTime.Now.Year, 5, 1);
            dtpToDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd");
            base.InitializePage();
        }

        private void AddColumns(GridView gridView, DataTable dataTable, string hiddenFieldNames)
        {
            try
            {
                string[] fieldNames = hiddenFieldNames.Replace(" ", "").Split(',');

                var columnInfo = new Dictionary<String, GridColumn>();
                if (gridView.Columns.Count > 0)
                {
                    foreach (GridColumn column in gridView.Columns)
                    {
                        columnInfo.Add(column.FieldName, column);
                    }
                }

                gridView.Columns.Clear();
                gridView.GroupCount = 0;
                gridView.FormatConditions.Clear();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    DataColumn dataColumn = dataTable.Columns[i];
                    GridColumn gridColumn = gridView.Columns.AddField(dataColumn.ColumnName);

                    if (columnInfo.ContainsKey(gridColumn.FieldName) == true)
                    {
                        gridColumn.OptionsColumn.AllowEdit = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowEdit;
                        gridColumn.OptionsColumn.AllowMerge = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowMerge;
                        gridColumn.OptionsColumn.AllowSort = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowSort;
                        gridColumn.OptionsColumn.AllowGroup = (columnInfo[gridColumn.FieldName]).OptionsColumn.AllowGroup;
                        gridColumn.Fixed = (columnInfo[gridColumn.FieldName]).Fixed;
                        gridColumn.ColumnEdit = (columnInfo[gridColumn.FieldName]).ColumnEdit;
                    }
                    else
                    {
                        gridColumn.OptionsColumn.AllowEdit = false;
                        gridColumn.OptionsColumn.AllowMerge = DefaultBoolean.False;
                    }


                    gridColumn.Tag = gridColumn.Width;
                    gridColumn.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                }

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    gridView.Columns[i].Visible = true;
                    for (int j = 0; j < fieldNames.Length; j++)
                    {
                        if (gridView.Columns[i].FieldName == fieldNames[j])
                        {
                            gridView.Columns[i].Visible = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public override void SearchPage()
        {
            base.SearchPage();

            try
            {
                base.m_BindData.BindGridView(gcList,
                    "PKG_SMT010.GET_LIST",
                    new string[] { "A_FROM_DATE", "A_TO_DATE" },
                    new string[] {  dtpFromDate.DateTime.ToString("yyyyMMdd") + "000000", dtpToDate.DateTime.ToString("yyyyMMdd") + "235959"}
                    );
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            gvList.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvList.Columns[3].DisplayFormat.FormatString = "n0";
            //for (int i = 9; i < gvList.Columns.Count - 2; i++)
            //{
            //    gvList.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //    gvList.Columns[i].DisplayFormat.FormatString = "{0:##.#;;\"\"}";
            //}
            //gvList.OptionsView.ShowFooter = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if(gleLoai.EditValue.NullString() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_116".Translation(), MsgType.Warning);
                return;
            }
            if(txtLot.Text.Trim() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_022".Translation(), MsgType.Warning);
                return;
            }
            if (txtMakeDate.EditValue.NullString() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_117".Translation(), MsgType.Warning);
                return;
            }
            if (txtExpDate.EditValue.NullString() == string.Empty)
            {
                MsgBox.Show("MSG_ERR_118".Translation(), MsgType.Warning);
                return;
            }

            try
            {
                base.m_ResultDB = base.m_DBaccess.ExcuteProc("PKG_SMT010.PUT_ITEM"
                    , new string[] {
                        "A_CODE",
                        "A_LOT",
                        "A_QUANTITY",
                        "A_MAKE_DATE",
                        "A_EXP_DATE",
                        "A_TRAN_USER_ID"
                    }
                    , new string[] {
                        gleLoai.EditValue.NullString(),
                        txtLot.Text.Trim().ToUpper(),
                        txtQuanity.EditValue.NullString(),
                        txtMakeDate.DateTime.ToString("yyyyMMdd"),
                        txtExpDate.DateTime.ToString("yyyyMMdd"),
                        Consts.USER_INFO.Id
                    }
                    ); 

                if (base.m_ResultDB.ReturnInt == 0)
                {
                    MsgBox.Show(base.m_ResultDB.ReturnString.Translation(), MsgType.Information);
                    gleLoai.EditValue = null;
                    txtLot.Text = string.Empty;
                    txtQuanity.Text = "0";
                    txtMakeDate.EditValue = null;
                    txtExpDate.EditValue = null;
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


        private void gcList_DoubleClick(object sender, EventArgs e)
        {
            //int indexRow = gvList.FocusedRowHandle;
            //if (indexRow != DevExpress.XtraGrid.GridControl.InvalidRowHandle && gvList.RowCount > 0)
            //{
            //    string code = gvList.GetDataRow(indexRow)["CODE"].ToString().Trim();
            //    string lot = gvList.GetDataRow(indexRow)["LOT_NO"].ToString().Trim();
            //    string quantity = gvList.GetDataRow(indexRow)["SO_LUONG"].ToString().Trim();
            //    string make_date = gvList.GetDataRow(indexRow)["NGAY_SAN_XUAT"].ToString().Trim();
            //    string exp_date = gvList.GetDataRow(indexRow)["NGAY_HET_HAN"].ToString().Trim();
            //    gleLoai.EditValue = code;
            //    txtLot.Text = lot;
            //    txtQuanity.EditValue = quantity;
            //    txtMakeDate.EditValue = make_date;
            //    txtExpDate.EditValue = exp_date;
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void gvList_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "NGAY_HET_HAN" )
            {
                string cellValue = gvList.GetRowCellDisplayText(e.RowHandle, e.Column);
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    DateTime exp_date = DateTime.Parse(cellValue);
                    DateTime current_date = DateTime.Now.Date;
                    double count = (exp_date - current_date).TotalDays;
                    if(count <= 3 && count > 0)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                    if(count <= 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
