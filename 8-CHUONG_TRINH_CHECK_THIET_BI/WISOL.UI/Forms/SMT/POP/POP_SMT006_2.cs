using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Wisol.Common;
using Wisol.Components;
using Wisol.Objects;

using Wisol.MES.Inherit;
using Wisol.MES.Classes;
using Wisol.MES.Dialog;
using System.Text.RegularExpressions;
using DevExpress.Spreadsheet;

namespace Wisol.MES.Forms.SMT.POP
{
    public partial class POP_SMT006_2 : FormType
    {

        public POP_SMT006_2()
        {
            InitializeComponent();
        }

        public POP_SMT006_2(string x) : this()
        {
            IWorkbook workbook;
            Worksheet worksheet;
            spreadsheetControl1.Options.Behavior.Worksheet.Insert = DevExpress.XtraSpreadsheet.DocumentCapability.Disabled;

            workbook = spreadsheetControl1.Document;
            worksheet = workbook.Worksheets[0];
            worksheet.ActiveView.ShowGridlines = false;

            worksheet.MergeCells(worksheet.Range["L1:P1"]);
            worksheet.MergeCells(worksheet.Range["Q1:U1"]);
            worksheet.MergeCells(worksheet.Range["V1:Z1"]);
            worksheet.MergeCells(worksheet.Range["AA1:AE1"]);
            worksheet.MergeCells(worksheet.Range["AF1:AJ1"]);
            worksheet.MergeCells(worksheet.Range["AK1:AO1"]);
            worksheet.MergeCells(worksheet.Range["AP1:AT1"]);
            worksheet.MergeCells(worksheet.Range["AU1:AY1"]);
            worksheet.MergeCells(worksheet.Range["AZ1:BD1"]);
            worksheet.MergeCells(worksheet.Range["BF1:BJ1"]);

            CellRange range = worksheet.Range["A1:BM2"];
            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.Font.Color = Color.Black;
            rangeFormatting.Font.Bold = true;
            rangeFormatting.Fill.BackgroundColor = Color.FromArgb(180, 198, 231);
            rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range.EndUpdateFormatting(rangeFormatting);

            worksheet.Columns["D"].Width = 280;
            worksheet.Columns["J"].Width = 250;
            worksheet.Columns["K"].Width = 250;
            worksheet.Columns["L"].Width = 250;
            worksheet.Columns["M"].Width = 250;
            worksheet.Columns["N"].Width = 250;
            worksheet.Columns["O"].Width = 250;
            worksheet.Columns["P"].Width = 250;
            worksheet.Columns["Q"].Width = 250;
            worksheet.Columns["R"].Width = 250;
            worksheet.Columns["S"].Width = 250;
            worksheet.Columns["T"].Width = 250;
            worksheet.Columns["U"].Width = 250;
            worksheet.Columns["V"].Width = 250;
            worksheet.Columns["W"].Width = 250;
            worksheet.Columns["X"].Width = 250;
            worksheet.Columns["Y"].Width = 250;
            worksheet.Columns["Z"].Width = 250;
            worksheet.Columns["AA"].Width = 250;
            worksheet.Columns["AB"].Width = 250;
            worksheet.Columns["AC"].Width = 250;
            worksheet.Columns["AD"].Width = 250;
            worksheet.Columns["AE"].Width = 250;
            worksheet.Columns["AF"].Width = 250;
            worksheet.Columns["AG"].Width = 250;
            worksheet.Columns["AH"].Width = 250;
            worksheet.Columns["AI"].Width = 250;
            worksheet.Columns["AJ"].Width = 250;
            worksheet.Columns["AK"].Width = 250;
            worksheet.Columns["AL"].Width = 250;
            worksheet.Columns["AM"].Width = 250;
            worksheet.Columns["AN"].Width = 250;
            worksheet.Columns["AO"].Width = 250;
            worksheet.Columns["AP"].Width = 250;
            worksheet.Columns["AQ"].Width = 250;
            worksheet.Columns["AR"].Width = 250;
            worksheet.Columns["AS"].Width = 250;
            worksheet.Columns["AT"].Width = 250;
            worksheet.Columns["AU"].Width = 250;
            worksheet.Columns["AV"].Width = 250;
            worksheet.Columns["AW"].Width = 250;
            worksheet.Columns["AX"].Width = 250;
            worksheet.Columns["AY"].Width = 250;
            worksheet.Columns["AZ"].Width = 250;
            worksheet.Columns["BA"].Width = 250;
            worksheet.Columns["BB"].Width = 250;
            worksheet.Columns["BC"].Width = 250;
            worksheet.Columns["BD"].Width = 250;
            worksheet.Columns["BE"].Width = 250;
            worksheet.Columns["BF"].Width = 250;
            worksheet.Columns["BG"].Width = 250;
            worksheet.Columns["BH"].Width = 250;
            worksheet.Columns["BI"].Width = 250;
            worksheet.Columns["BJ"].Width = 250;
            worksheet.Columns["BK"].Width = 250;
            worksheet.Columns["BL"].Width = 250;
            worksheet.Columns["BM"].Width = 250;


            worksheet.Cells["A1"].Value = "Year";
            worksheet.Cells["A2"].Value = "Year";
            worksheet.Cells["B1"].Value = "Month";
            worksheet.Cells["B2"].Value = "Month";
            worksheet.Cells["C1"].Value = "Weekly";
            worksheet.Cells["C2"].Value = "Weekly";
            worksheet.Cells["D1"].Value = "Date";
            worksheet.Cells["D2"].Value = "Date";
            worksheet.Cells["E1"].Value = "Shift";
            worksheet.Cells["E2"].Value = "Shift";
            worksheet.Cells["F1"].Value = "Line";
            worksheet.Cells["F2"].Value = "Line";
            worksheet.Cells["G1"].Value = "Model";
            worksheet.Cells["G2"].Value = "Model";
            worksheet.Cells["H1"].Value = "Input";
            worksheet.Cells["H2"].Value = "Input";
            worksheet.Cells["I1"].Value = "Total NG";
            worksheet.Cells["I2"].Value = "Total NG";
            worksheet.Cells["J1"].Value = "Thừa thiếc";
            worksheet.Cells["J2"].Value = "Thừa thiếc";
            worksheet.Cells["K1"].Value = "Thiếu thiếc";
            worksheet.Cells["K2"].Value = "Thiếu thiếc";
            worksheet.Cells["L1"].Value = "Dị vật";
            worksheet.Cells["L2"].Value = "Dị vật Total";
            worksheet.Cells["M2"].Value = "Dị vật Chip";
            worksheet.Cells["N2"].Value = "Dị vật SAW";
            worksheet.Cells["O2"].Value = "Dị vật LNA";
            worksheet.Cells["P2"].Value = "Dị vật SW";
            worksheet.Cells["Q1"].Value = "Vãi";
            worksheet.Cells["Q2"].Value = "Vãi Total";
            worksheet.Cells["R2"].Value = "Vãi Chip";
            worksheet.Cells["S2"].Value = "Vãi SAW";
            worksheet.Cells["T2"].Value = "Vãi LNA";
            worksheet.Cells["U2"].Value = "Vãi SW";
            worksheet.Cells["V1"].Value = "Short";
            worksheet.Cells["V2"].Value = "Short Total";
            worksheet.Cells["W2"].Value = "Short Chip";
            worksheet.Cells["X2"].Value = "Short SAW";
            worksheet.Cells["Y2"].Value = "Short LNA";
            worksheet.Cells["Z2"].Value = "Short SW";
            worksheet.Cells["AA1"].Value = "Mất";
            worksheet.Cells["AA2"].Value = "Mất Total";
            worksheet.Cells["AB2"].Value = "Mất Chip";
            worksheet.Cells["AC2"].Value = "Mất SAW";
            worksheet.Cells["AD2"].Value = "Mất LNA";
            worksheet.Cells["AE2"].Value = "Mất SW";
            worksheet.Cells["AF1"].Value = "Kênh";
            worksheet.Cells["AF2"].Value = "Kênh Total";
            worksheet.Cells["AG2"].Value = "Kênh Chip";
            worksheet.Cells["AH2"].Value = "Kênh SAW";
            worksheet.Cells["AI2"].Value = "Kênh LNA";
            worksheet.Cells["AJ2"].Value = "Kênh SW";
            worksheet.Cells["AK1"].Value = "Lệch";
            worksheet.Cells["AK2"].Value = "Lệch Total";
            worksheet.Cells["AL2"].Value = "Lệch Chip";
            worksheet.Cells["AM2"].Value = "Lệch SAW";
            worksheet.Cells["AN2"].Value = "Lệch LNA";
            worksheet.Cells["AO2"].Value = "Lệch SW";
            worksheet.Cells["AP1"].Value = "Ngược";
            worksheet.Cells["AP2"].Value = "Ngược Total";
            worksheet.Cells["AQ2"].Value = "Ngược Chip";
            worksheet.Cells["AR2"].Value = "Ngược SAW";
            worksheet.Cells["AS2"].Value = "Ngược LNA";
            worksheet.Cells["AT2"].Value = "Ngược SW";
            worksheet.Cells["AU1"].Value = "Dựng";
            worksheet.Cells["AU2"].Value = "Dựng Total";
            worksheet.Cells["AV2"].Value = "Dựng Chip";
            worksheet.Cells["AW2"].Value = "Dựng SAW";
            worksheet.Cells["AX2"].Value = "Dựng LNA";
            worksheet.Cells["AY2"].Value = "Dựng SW";
            worksheet.Cells["AZ1"].Value = "Lật";
            worksheet.Cells["AZ2"].Value = "Lật Total";
            worksheet.Cells["BA2"].Value = "Lật Chip";
            worksheet.Cells["BB2"].Value = "Lật SAW";
            worksheet.Cells["BC2"].Value = "Lật LNA";
            worksheet.Cells["BD2"].Value = "Lật SW";
            worksheet.Cells["BE1"].Value = "PCB Loss";
            worksheet.Cells["BE2"].Value = "PCB Loss";
            worksheet.Cells["BF1"].Value = "Vỡ";
            worksheet.Cells["BF2"].Value = "Vỡ Total";
            worksheet.Cells["BG2"].Value = "Vỡ Chip";
            worksheet.Cells["BH2"].Value = "Vỡ SAW";
            worksheet.Cells["BI2"].Value = "Vỡ LNA";
            worksheet.Cells["BJ2"].Value = "Vỡ SW";
            worksheet.Cells["BK1"].Value = "DST";
            worksheet.Cells["BK2"].Value = "DST";
            worksheet.Cells["BL1"].Value = "Lỗi khác";
            worksheet.Cells["BL2"].Value = "Lỗi khác";
            worksheet.Cells["BM1"].Value = "Sample";
            worksheet.Cells["BM2"].Value = "Sample";

            spreadsheetControl1.Options.Behavior.Group.Group = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled;
            range = worksheet["M2:P2"];
            range.GroupColumns(true);
            range = worksheet["R2:U2"];
            range.GroupColumns(true);
            range = worksheet["W2:Z2"];
            range.GroupColumns(true);
            range = worksheet["AB2:AE2"];
            range.GroupColumns(true);
            range = worksheet["AG2:AJ2"];
            range.GroupColumns(true);
            range = worksheet["AL2:AO2"];
            range.GroupColumns(true);
            range = worksheet["AQ2:AT2"];
            range.GroupColumns(true);
            range = worksheet["AV2:AY2"];
            range.GroupColumns(true);
            range = worksheet["BA2:BD2"];
            range.GroupColumns(true);
            range = worksheet["AG2:AH2"];
            range.GroupColumns(true);
            range = worksheet["AJ2:AK2"];
            range.GroupColumns(true);
            range = worksheet["BG2:BJ2"];
            range.GroupColumns(true);

            DataTable table = new DataTable();
            try
            {
                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_SMT006_2.POP_GET_LIST",
                    new string[]
                    {
                        "A_PLANT"
                    },
                    new string[]
                    {
                        Consts.PLANT
                    }
                );
                if (base.mResultDB.ReturnInt == 0)
                {
                    table = base.mResultDB.ReturnDataSet.Tables[0].Copy();
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }

            for(int i = 0; i < table.Columns.Count; i++)
            {
                for(int j = 0; j < table.Rows.Count; j++)
                {
                    worksheet.Cells[j + 2, i].Value = (table.Rows[j][i].ToString() == "0" ? "" : table.Rows[j][i].ToString());
                }
            }

            range = worksheet.Range["A1:BM" + (table.Rows.Count +2).ToString()];
            range.SetInsideBorders(Color.Black, BorderLineStyle.Thin);
            range.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files(*.xlsx)|*.xlsx";
                saveDialog.FileName = "SMT DATA ERROR DETAIL_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    spreadsheetControl1.SaveDocument(saveDialog.FileName);
                }
            }
        }
    }
}
