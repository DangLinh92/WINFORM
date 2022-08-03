using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wisol.Common;
using Wisol.Components;
using Wisol.MES.Classes;
using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.CONTENT
{
    public partial class frmViewLot : PageType
    {
        public frmViewLot()
        {
            InitializeComponent();
            Classes.Common.SetFormIdToButton(this, "frmViewLot");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                if (radioGroup1.SelectedIndex == 0)
                {
                    if (string.IsNullOrEmpty(txtLotID.Text.NullString()) && string.IsNullOrEmpty(txtFileUrl.Text))
                    {
                        MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                        return;
                    }
                }
                else if (radioGroup1.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(txtMarkingNo.Text.NullString()) && string.IsNullOrEmpty(txtFileUrl.Text))
                    {
                        MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                        return;
                    }
                }
                else if (radioGroup1.SelectedIndex == 2)
                {
                    if (string.IsNullOrEmpty(txtReelId.Text.NullString()) && string.IsNullOrEmpty(txtFileUrl.Text))
                    {
                        MsgBox.Show("MSG_ERR_044".Translation(), MsgType.Warning);
                        return;
                    }
                }

                string formatString = "yyyyMMddHHmmss";

                DataTable Data = new DataTable();
                Data.Columns.Add("MARKING_TIME");
                Data.Columns.Add("LOT_ID");
                Data.Columns.Add("MARKING_NO");
                Data.Columns.Add("MATERIAL_NAME");
                Data.Columns.Add("ISMECA");
                Data.Columns.Add("START_END_LINE");
                Data.Columns.Add("END_END_LINE");
                Data.Columns.Add("REMARK");

                DataTable Data1 = new DataTable();
                Data1.Columns.Add("LOT_ID");
                Data1.Columns.Add("REEL_ID");
                Data1.Columns.Add("QTY");
                Data1.Columns.Add("FA_JUDGE");
                Data1.Columns.Add("CUSTOMER_NAME");
                Data1.Columns.Add("SHIP_REEL");
                Data1.Columns.Add("SHIP_TIME");

                DataTable Data2 = new DataTable();
                Data2.Columns.Add("LOT_ID");
                Data2.Columns.Add("REEL_ID");
                Data2.Columns.Add("WAFER_ID");
                Data2.Columns.Add("START_FB");
                Data2.Columns.Add("END_FB");
                Data2.Columns.Add("QUIPMENT_FB");
                Data2.Columns.Add("START_BB");
                Data2.Columns.Add("END_BB");
                Data2.Columns.Add("QUIPMENT_BB");



                DataTable DataSum = new DataTable();
                DataSum.Columns.Add("MARKING_TIME");
                DataSum.Columns.Add("LOT_ID");
                DataSum.Columns.Add("MARKING_NO");
                DataSum.Columns.Add("MATERIAL_NAME");
                DataSum.Columns.Add("ISMECA");
                DataSum.Columns.Add("START_END_LINE");
                DataSum.Columns.Add("END_END_LINE");
                DataSum.Columns.Add("REMARK");
                DataSum.Columns.Add("REEL_ID");
                DataSum.Columns.Add("PKG_LOT");
                DataSum.Columns.Add("QTY");
                DataSum.Columns.Add("FA_JUDGE");
                DataSum.Columns.Add("CUSTOMER_NAME");
                DataSum.Columns.Add("SHIP_REEL");
                DataSum.Columns.Add("WAFER_ID");
                DataSum.Columns.Add("REWORK_LOT");
                DataSum.Columns.Add("START_FB");
                DataSum.Columns.Add("END_FB");
                DataSum.Columns.Add("QUIPMENT_FB");

                DataSum.Columns.Add("START_BB");
                DataSum.Columns.Add("END_BB");
                DataSum.Columns.Add("QUIPMENT_BB");

                DataSum.Columns.Add("SHIP_TIME");

                List<string> lstLotCondition = new List<string>();
                if (string.IsNullOrEmpty(txtFileUrl.Text))
                {
                    if (!string.IsNullOrEmpty(txtLotID.Text.NullString()))
                    {
                        lstLotCondition.Add(txtLotID.Text.NullString());
                    }
                    else if (!string.IsNullOrEmpty(txtMarkingNo.Text.NullString()))
                    {
                        lstLotCondition.Add(txtMarkingNo.Text.NullString());
                    }
                    else if (!string.IsNullOrEmpty(txtReelId.Text.NullString()))
                    {
                        lstLotCondition.Add(txtReelId.Text.NullString());
                    }
                }
                else if (!string.IsNullOrEmpty(txtFileUrl.Text.NullString()))
                {
                    if (!File.Exists(txtFileUrl.Text.NullString()))
                    {
                        MsgBox.Show("File Không Tồn Tại!", MsgType.Error);
                        return;
                    }
                    string filePath = txtFileUrl.Text.NullString();
                    string excelcon;
                    if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    {
                        excelcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'";
                    }
                    else
                    {
                        excelcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
                    }
                    OleDbConnection conexcel = new OleDbConnection(excelcon);

                    try
                    {
                        conexcel.Open();
                        DataTable dtExcel = conexcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        string sheetName = "DATA";

                        foreach (DataRow drSheet in dtExcel.Rows)
                        {
                            if (drSheet["TABLE_NAME"].ToString().Contains("$"))
                            {
                                sheetName = drSheet["TABLE_NAME"].ToString();
                                break;
                            }
                        }

                        OleDbCommand cmdexcel1 = new OleDbCommand();
                        cmdexcel1.Connection = conexcel;
                        cmdexcel1.CommandText = "select * from[" + sheetName + "]";

                        DataTable DataLotId = new DataTable();
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmdexcel1;
                        da.Fill(DataLotId);
                        conexcel.Close();
                        DataLotId.Rows.RemoveAt(0);

                        foreach (DataRow item in DataLotId.Rows)
                        {
                            if (!lstLotCondition.Contains(item[0].NullString()) && item[0].NullString() != "")
                            {
                                lstLotCondition.Add(item[0].NullString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, MsgType.Error);
                    }
                }

                Dictionary<string, List<string>> relationDicOrigin = new Dictionary<string, List<string>>();
                // search by Lot Id
                if (radioGroup1.SelectedIndex == 0 || radioGroup1.SelectedIndex == 1 || radioGroup1.SelectedIndex == 2)
                {
                    if (lstLotCondition.Count == 0)
                        return;

                    m_DBaccess.connectionStringParam = 1;// connect to mesdb
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        // 1. Get Lot Info from LotId
                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("LOT", lstLotCondition));
                    }
                    else if (radioGroup1.SelectedIndex == 1)
                    {
                        // 1. Get Lot Info from LotId
                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("MARKING_NO", lstLotCondition));
                    }
                    else if (radioGroup1.SelectedIndex == 2)
                    {
                        // 1. Get Lot Info from reel id
                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("REEL_ID", lstLotCondition));
                        DataTable dtaLotId = base.m_ResultDB.ReturnDataSet.Tables[0];
                        List<string> lotIds = new List<string>();

                        foreach (DataRow item in dtaLotId.Rows)
                        {
                            if (!lotIds.Contains(item["LOT_ID"].NullString()) && item["LOT_ID"].NullString() != "")
                            {
                                lotIds.Add(item["LOT_ID"].NullString());
                            }

                            if (relationDicOrigin.ContainsKey(item["LOT_ID"].NullString()) && !relationDicOrigin[item["LOT_ID"].NullString()].Contains(item["RELATION_LOT_ID"].NullString()))
                            {
                                relationDicOrigin[item["LOT_ID"].NullString()].Add(item["RELATION_LOT_ID"].NullString());
                            }
                            else
                            {
                                relationDicOrigin.Add(item["LOT_ID"].NullString(), new List<string> { item["RELATION_LOT_ID"].NullString() });
                            }
                        }

                        if (lotIds.Count == 0)
                        {
                            MsgBox.Show("Not found lot Id by reel id", MsgType.Warning);
                            return;
                        }

                        m_DBaccess.connectionStringParam = 1;
                        // 1. Get Lot Info from LotId
                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("LOT", lotIds));
                    }

                    if (base.m_ResultDB.ReturnInt == 0)
                    {
                        DataTable dtaLot = base.m_ResultDB.ReturnDataSet.Tables[0];
                        List<string> lotIds = new List<string>();
                        if (dtaLot.Rows.Count > 0)
                        {
                            DataRow newRow;
                            foreach (DataRow row in dtaLot.Rows)
                            {
                                newRow = Data.NewRow();

                                if (!lotIds.Contains(row["LOT_ID"].NullString()))
                                {
                                    lotIds.Add(row["LOT_ID"].NullString());
                                }

                                newRow["LOT_ID"] = row["LOT_ID"];
                                newRow["MARKING_NO"] = row["MARKING_NO"];
                                Data.Rows.Add(newRow);
                            }
                        }

                        Dictionary<string, List<string>> relationDic = new Dictionary<string, List<string>>();
                        List<string> relationLotId = new List<string>();

                        // 2. Get Lot history by LotId
                        m_DBaccess.connectionStringParam = 1;// connect to mesdb
                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("WIP", lotIds));
                        if (base.m_ResultDB.ReturnInt == 0)
                        {
                            DataTable dtData = base.m_ResultDB.ReturnDataSet.Tables[0];
                            if (dtData.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtData.Rows)
                                {
                                    if (relationDicOrigin.Count > 0)
                                    {
                                        if (!relationDicOrigin.ContainsKey(row["LOT_ID"].NullString()))
                                        {
                                            continue;
                                        }
                                    }

                                    foreach (DataRow row1 in Data.Rows)
                                    {
                                        if (row["LOT_ID"].NullString() == row1["LOT_ID"].NullString())
                                        {
                                            // marking time
                                            if (row1["MARKING_TIME"].NullString() == "" && row["Operation"].NullString() == "OC290")
                                            {
                                                if (row["Tx Time"].NullString() != "")
                                                {
                                                    row1["MARKING_TIME"] = DateTime.ParseExact(row["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                }
                                            }

                                            // Material Name
                                            row1["MATERIAL_NAME"] = row["Material Name"];

                                            // ISMECA
                                            row1["ISMECA"] = row["Equipment Name"];

                                            // START_END_LINE
                                            if (row1["START_END_LINE"].NullString() == "" && row["Operation"].NullString() == "OC360")
                                            {
                                                if (row["Tx Time"].NullString() != "")
                                                {
                                                    row1["START_END_LINE"] = DateTime.ParseExact(row["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                }
                                            }

                                            // END_END_LINE
                                            if (row["Operation"].NullString() == "OC360")
                                            {
                                                if (row["Tx Time"].NullString() != "")
                                                {
                                                    row1["END_END_LINE"] = DateTime.ParseExact(row["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                }
                                            }

                                            // REMARK
                                            row1["REMARK"] = row["Comment"];
                                        }
                                    }

                                    if (row["Relation Lot"].NullString() != "" && (row["Operation"].NullString() == "OC360" || row["Operation"].NullString() == "OC156"))
                                    {
                                        //if (relationDicOrigin.Count <= 0)
                                        //{
                                        if (relationDic.ContainsKey(row["LOT_ID"].NullString()))
                                        {
                                            if (!relationDic[row["LOT_ID"].NullString()].Contains(row["Relation Lot"].NullString()))
                                            {
                                                relationDic[row["LOT_ID"].NullString()].Add(row["Relation Lot"].NullString());
                                            }
                                        }
                                        else
                                        {
                                            relationDic.Add(row["LOT_ID"].NullString(), new List<string>() { row["Relation Lot"].NullString() });
                                        }
                                        //}
                                        //else
                                        //{
                                        //    if (relationDic.ContainsKey(row["LOT_ID"].NullString()) && relationDicOrigin.ContainsKey(row["LOT_ID"].NullString()))
                                        //    {
                                        //        if (!relationDic[row["LOT_ID"].NullString()].Contains(row["Relation Lot"].NullString()) && relationDicOrigin[row["LOT_ID"].NullString()].Contains(row["Relation Lot"].NullString()))
                                        //        {
                                        //            relationDic[row["LOT_ID"].NullString()].Add(row["Relation Lot"].NullString());
                                        //        }
                                        //    }
                                        //    else if (!relationDic.ContainsKey(row["LOT_ID"].NullString()) && relationDicOrigin.ContainsKey(row["LOT_ID"].NullString()))
                                        //    {
                                        //        if (relationDicOrigin[row["LOT_ID"].NullString()].Contains(row["Relation Lot"].NullString()))
                                        //        {
                                        //            relationDic.Add(row["LOT_ID"].NullString(), new List<string>() { row["Relation Lot"].NullString() });
                                        //        }
                                        //    }
                                        //}
                                    }
                                }

                                foreach (var dic in relationDic)
                                {
                                    // 3. Get Lot Info from Relation id
                                    m_DBaccess.connectionStringParam = 1;// connect to mesdb
                                    base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("LOT", dic.Value));

                                    if (base.m_ResultDB.ReturnInt == 0)
                                    {
                                        DataTable dtaLot1 = base.m_ResultDB.ReturnDataSet.Tables[0];

                                        if (dtaLot1.Rows.Count > 0)
                                        {
                                            DataRow newRow;
                                            foreach (DataRow row in dtaLot1.Rows)
                                            {
                                                newRow = Data1.NewRow();
                                                newRow["LOT_ID"] = dic.Key;
                                                newRow["REEL_ID"] = row["LOT_ID"];
                                                newRow["QTY"] = row["QTY1"];
                                                Data1.Rows.Add(newRow);
                                            }
                                        }

                                        // 4. Get Lot history by Relation Lot
                                        m_DBaccess.connectionStringParam = 1;// connect to mesdb
                                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("WIP", dic.Value));
                                        DataTable dtData1 = base.m_ResultDB.ReturnDataSet.Tables[0];

                                        string txEdc = "";
                                        string txHold = "";
                                        string txEnd = "";

                                        foreach (DataRow row in Data1.Rows)
                                        {
                                            txEdc = "";
                                            txHold = "";
                                            txEnd = "";

                                            foreach (DataRow item in dtData1.Rows)
                                            {
                                                if (row["REEL_ID"].NullString() == item["LOT_ID"].NullString())
                                                {
                                                    if (item["Tx Code"].NullString() == "TX_EDC")
                                                    {
                                                        txEdc = "TX_EDC";
                                                    }
                                                    else
                                                    if (item["Tx Code"].NullString() == "TX_HOLD")
                                                    {
                                                        txHold = "TX_HOLD";
                                                    }
                                                    else
                                                    if (item["Tx Code"].NullString() == "TX_END")
                                                    {
                                                        txEnd = "TX_END";
                                                    }

                                                    if (!row["REEL_ID"].NullString().StartsWith("CNM")) // PKG LOT 
                                                    {
                                                        if (item["Tx Code"].NullString() == "TX_END" && item["Operation"].NullString() == "OC150")
                                                        {
                                                            row["QTY"] = item["Chip Qty"];
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (double.TryParse(row["QTY"].NullString(), out _) && double.Parse(row["QTY"].NullString()) == 0)
                                                        {
                                                            if (item["Tx Code"].NullString() == "TX_END" && item["Operation"].NullString() == "OC360")
                                                            {
                                                                row["QTY"] = item["Chip Qty"];
                                                            }
                                                        }
                                                    }

                                                    if (item["Tx Code"].NullString() == "TX_SHIP")
                                                    {
                                                        row["SHIP_TIME"] = item["Tx Time"];
                                                    }
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(txEdc))
                                            {
                                                if (!string.IsNullOrEmpty(txHold))
                                                {
                                                    row["FA_JUDGE"] = "FALSE";
                                                }
                                                else
                                                if (!string.IsNullOrEmpty(txEnd))
                                                {
                                                    row["FA_JUDGE"] = "PASS";
                                                }
                                            }
                                        }

                                        DataRow newRow2;
                                        // Data2.Columns.Add("LOT_ID");
                                        // Data2.Columns.Add("REEL_ID");
                                        // Data2.Columns.Add("WAFER_ID");
                                        // Data2.Columns.Add("START_FB");
                                        // Data2.Columns.Add("END_FB");
                                        List<string> lstWaferId = new List<string>();
                                        foreach (DataRow item in dtData1.Rows)
                                        {
                                            newRow2 = Data2.NewRow();

                                            if (item["Relation Lot"].NullString() != "" && !item["Relation Lot"].NullString().StartsWith("CNM"))// assy lot cnm
                                            {
                                                newRow2["LOT_ID"] = dic.Key;
                                                newRow2["REEL_ID"] = item["LOT_ID"];
                                                newRow2["WAFER_ID"] = item["Relation Lot"];
                                                lstWaferId.Add(item["Relation Lot"].NullString());
                                                Data2.Rows.Add(newRow2);
                                            }
                                        }

                                        // 5. Get customer and ship reel
                                        m_DBaccess.connectionStringParam = 1;// connect to mesdb
                                        base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("DATA", dic.Value));

                                        if (base.m_ResultDB.ReturnInt == 0)
                                        {
                                            DataTable dtData2 = base.m_ResultDB.ReturnDataSet.Tables[0];

                                            foreach (DataRow row in Data1.Rows)
                                            {
                                                foreach (DataRow item in dtData2.Rows)
                                                {
                                                    if (row["REEL_ID"].NullString() == item["Lot ID"].NullString())
                                                    {
                                                        row["CUSTOMER_NAME"] = item["Customer Name"];
                                                        row["SHIP_REEL"] = item["Ship Reel"];
                                                    }
                                                }
                                            }
                                        }

                                        if (lstWaferId.Count > 0)
                                        {
                                            // Get wafer, start FB, End FB

                                            m_DBaccess.connectionStringParam = 1;// connect to mesdb
                                            base.m_ResultDB = base.m_DBaccess.SqlExecuteDataTable(MakeSqlString("WIP", lstWaferId));
                                            DataTable dtData3 = base.m_ResultDB.ReturnDataSet.Tables[0];

                                            foreach (DataRow row in Data2.Rows)
                                            {
                                                foreach (DataRow item in dtData3.Rows)
                                                {
                                                    if (row["WAFER_ID"].NullString() == item["LOT_ID"].NullString())
                                                    {
                                                        // Start FB
                                                        if (row["START_FB"].NullString() == "" && item["Operation"].NullString() == "OC150")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                row["START_FB"] = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }

                                                        if (item["Operation"].NullString() == "OC150")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                row["END_FB"] = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }

                                                        if (row["START_BB"].NullString() == "" && item["Operation"].NullString() == "OC100")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                row["START_BB"] = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }

                                                        if (item["Operation"].NullString() == "OC100")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                row["END_BB"] = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }

                                                        if (item["Operation"].NullString() == "OC150")
                                                        {
                                                            row["QUIPMENT_FB"] = item["Equipment"].NullString();
                                                        }

                                                        if (item["Operation"].NullString() == "OC100")
                                                        {
                                                            row["QUIPMENT_BB"] = item["Equipment"].NullString();
                                                        }
                                                    }
                                                }
                                            }

                                            string enfb = "";

                                            foreach (DataRow row in Data2.Rows)
                                            {
                                                enfb = "";
                                                foreach (DataRow item in dtData3.Rows)
                                                {
                                                    if (row["WAFER_ID"].NullString() == item["LOT_ID"].NullString())
                                                    {
                                                        // Start FB
                                                        if (row["START_FB"].NullString() == "" && item["Operation"].NullString() == "OC140")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                row["START_FB"] = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }

                                                        if (item["Operation"].NullString() == "OC140")
                                                        {
                                                            if (item["Tx Time"].NullString() != "")
                                                            {
                                                                enfb = DateTime.ParseExact(item["Tx Time"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                        }
                                                    }
                                                }

                                                if (row["END_FB"].NullString() == "")
                                                {
                                                    row["END_FB"] = enfb;
                                                }
                                            }
                                        }
                                    }
                                }

                                // cho data2 -> data sum
                                DataRow rowsum;
                                foreach (DataRow row2 in Data2.Rows)
                                {
                                    rowsum = DataSum.NewRow();
                                    rowsum["LOT_ID"] = row2["LOT_ID"];
                                    rowsum["REEL_ID"] = row2["REEL_ID"];
                                    rowsum["WAFER_ID"] = row2["WAFER_ID"];
                                    rowsum["START_FB"] = row2["START_FB"];
                                    rowsum["END_FB"] = row2["END_FB"];
                                    rowsum["QUIPMENT_FB"] = row2["QUIPMENT_FB"];

                                    rowsum["START_BB"] = row2["START_BB"];
                                    rowsum["END_BB"] = row2["END_BB"];
                                    rowsum["QUIPMENT_BB"] = row2["QUIPMENT_BB"];

                                    DataSum.Rows.Add(rowsum);
                                }

                                // cho data1 -> data sum
                                foreach (DataRow row1 in Data1.Rows)
                                {
                                    foreach (DataRow item in DataSum.Rows)
                                    {
                                        if (row1["LOT_ID"].NullString() == item["LOT_ID"].NullString() && row1["REEL_ID"].NullString() == item["REEL_ID"].NullString())
                                        {
                                            item["QTY"] = row1["QTY"];
                                            item["FA_JUDGE"] = row1["FA_JUDGE"];

                                            if (row1["FA_JUDGE"].NullString() == "FALSE")
                                            {
                                                item["REWORK_LOT"] = item["WAFER_ID"];
                                                item["WAFER_ID"] = "";
                                            }

                                            item["CUSTOMER_NAME"] = row1["CUSTOMER_NAME"];
                                            item["SHIP_REEL"] = row1["SHIP_REEL"];

                                            if (DateTime.TryParseExact(row1["SHIP_TIME"].NullString(), formatString, null, System.Globalization.DateTimeStyles.None, out _))
                                            {
                                                item["SHIP_TIME"] = DateTime.ParseExact(row1["SHIP_TIME"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                            }
                                        }
                                    }
                                }

                                // cho data1 ma k co trong data sum vao data sum
                                bool exist = false;
                                foreach (DataRow row1 in Data1.Rows)
                                {
                                    exist = false;
                                    foreach (DataRow item in DataSum.Rows)
                                    {
                                        if (row1["LOT_ID"].NullString() == item["LOT_ID"].NullString() && row1["REEL_ID"].NullString() == item["REEL_ID"].NullString())
                                        {
                                            exist = true;
                                            break;
                                        }
                                    }

                                    if (!exist)
                                    {
                                        rowsum = DataSum.NewRow();
                                        rowsum["LOT_ID"] = row1["LOT_ID"];

                                        rowsum["REEL_ID"] = row1["REEL_ID"];

                                        rowsum["QTY"] = row1["QTY"];
                                        rowsum["FA_JUDGE"] = row1["FA_JUDGE"];
                                        rowsum["CUSTOMER_NAME"] = row1["CUSTOMER_NAME"];
                                        rowsum["SHIP_REEL"] = row1["SHIP_REEL"];
                                        if (DateTime.TryParseExact(row1["SHIP_TIME"].NullString(), formatString, null, System.Globalization.DateTimeStyles.None, out _))
                                        {
                                            rowsum["SHIP_TIME"] = DateTime.ParseExact(row1["SHIP_TIME"].NullString(), formatString, null).ToString("yyyy-MM-dd HH:mm:ss");
                                        }
                                        DataSum.Rows.Add(rowsum);
                                    }
                                }

                                // cho data vao datasum
                                foreach (DataRow rsum in DataSum.Rows)
                                {
                                    foreach (DataRow row in Data.Rows)
                                    {
                                        if (rsum["LOT_ID"].NullString() == row["LOT_ID"].NullString())
                                        {
                                            rsum["MARKING_TIME"] = row["MARKING_TIME"];
                                            rsum["MARKING_NO"] = row["MARKING_NO"];
                                            rsum["MATERIAL_NAME"] = row["MATERIAL_NAME"];
                                            rsum["ISMECA"] = row["ISMECA"];
                                            rsum["START_END_LINE"] = row["START_END_LINE"];
                                            rsum["END_END_LINE"] = row["END_END_LINE"];
                                            rsum["REMARK"] = row["REMARK"];
                                            break;
                                        }
                                    }
                                }

                                foreach (DataRow row1 in Data.Rows)
                                {
                                    exist = false;
                                    foreach (DataRow rsum in DataSum.Rows)
                                    {
                                        if (rsum["LOT_ID"].NullString() == row1["LOT_ID"].NullString())
                                        {
                                            exist = true;
                                            break;
                                        }
                                    }

                                    if (!exist)
                                    {
                                        rowsum = DataSum.NewRow();
                                        rowsum["MARKING_TIME"] = row1["MARKING_TIME"];
                                        rowsum["LOT_ID"] = row1["LOT_ID"];
                                        rowsum["MARKING_NO"] = row1["MARKING_NO"];
                                        rowsum["MATERIAL_NAME"] = row1["MATERIAL_NAME"];
                                        rowsum["ISMECA"] = row1["ISMECA"];
                                        rowsum["START_END_LINE"] = row1["START_END_LINE"];
                                        rowsum["END_END_LINE"] = row1["END_END_LINE"];
                                        rowsum["REMARK"] = row1["REMARK"];
                                        DataSum.Rows.Add(rowsum);
                                    }
                                }

                                foreach (DataRow row in DataSum.Rows)
                                {
                                    if (!row["REEL_ID"].NullString().StartsWith("CNM"))
                                    {
                                        row["PKG_LOT"] = row["REEL_ID"];
                                        row["REEL_ID"] = "";
                                    }
                                }

                                // m_BindData.BindGridView(gcList, DataSum);
                                gcList.DataSource = DataSum;
                                gvList.ClearSorting();
                                gvList.Columns["LOT_ID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                                gvList.OptionsView.ColumnAutoWidth = true;
                            }
                        }
                    }
                    else
                    {
                        MsgBox.Show(m_ResultDB.ReturnString.Translation(), MsgType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel Files (.xls*)|*.xls*|All Files (*.*)|*.*";
            dlg.Multiselect = false;
            DialogResult dlgResult = dlg.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                txtFileUrl.Text = dlg.FileName;
                txtLotID.Text = string.Empty;
                txtMarkingNo.Text = string.Empty;
                txtReelId.Text = string.Empty;
            }
        }

        private string MakeSqlString(string Step, List<string> lotIds)
        {
            StringBuilder strSqlString = new StringBuilder();

            try
            {
                if (Step == "LOT")
                {
                    strSqlString.AppendFormat(" SELECT A.LOT_ID, A.LOT_UDF4 AS FA_ID, A.LOT_UDF2 AS MARKING_NO, A.MATERIAL_ID, A.CUSTOMER_LOT_ID, A.LOT_STATUS  \n");
                    strSqlString.AppendFormat("        , A.CREATION_QTY1, A.QTY1, A.DELETE_FLAG, W.WAFER_ID \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.RELATION_FLAG = 'F' THEN B.RELATION_LOT_ID ELSE A.RELATION_LOT_ID END 'MOTHER_ID' \n");
                    strSqlString.AppendFormat("        , (SELECT VALUE1 FROM NB_CODE_DATA (NOLOCK) WHERE TABLE_ID = 'LOT_CATEGORY' AND SITE_ID = A.SITE_ID AND PK1 = A.LOT_CATEGORY) 'LCATEGORY_NAME' \n");
                    strSqlString.AppendFormat("        , (SELECT VALUE1 FROM NB_CODE_DATA (NOLOCK) WHERE TABLE_ID = 'LOT_TYPE' AND SITE_ID = A.SITE_ID AND PK1 = A.LOT_TYPE) 'LTYPE_NAME' \n");
                    strSqlString.AppendFormat("        , (SELECT VALUE1 FROM NB_CODE_DATA (NOLOCK) WHERE TABLE_ID = 'MAT_VENDOR' AND SITE_ID = A.SITE_ID AND PK1 = A.LOT_UDF6) 'LVENDOR' \n");
                    strSqlString.AppendFormat("        , A.OPERATION_ID, O.OPERATION_SHORT_NAME, O.UNIT_1 \n");
                    strSqlString.AppendFormat("        , ISNULL(H.LOSS_CHIP, 0) AS LOSS_CHIP \n");
                    strSqlString.AppendFormat("        , A.LOT_UDF1 \n");
                    strSqlString.AppendFormat("   FROM NM_LOTS A (NOLOCK)   \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_LOT_HISTORY B (NOLOCK) ON B.LOT_ID = A.LOT_ID AND B.HISTORY_SEQ = 1 \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_OPERATIONS O (NOLOCK) ON A.SITE_ID = O.SITE_ID AND A.OPERATION_ID = O.OPERATION_ID \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_MATERIALS M (NOLOCK) ON A.MATERIAL_ID = M.MATERIAL_ID AND M.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN (SELECT A.LOT_ID \n");
                    strSqlString.AppendFormat("                                , SUM(L.CHANGE_QTY) 'LOSS_CHIP' \n");
                    strSqlString.AppendFormat("        					FROM NM_LOTS A (NOLOCK) \n");
                    strSqlString.AppendFormat("        						INNER JOIN NM_LOT_QUANTITY_CHANGES L (NOLOCK) ON L.LOT_ID = A.LOT_ID AND L.HISTORY_DELETE_FLAG <> 'Y' \n");
                    strSqlString.AppendFormat("        						INNER JOIN NM_OPERATIONS O (NOLOCK) ON L.SITE_ID = O.SITE_ID AND L.OPERATION_ID = O.OPERATION_ID \n");
                    strSqlString.AppendFormat("        					GROUP BY a.LOT_ID ) H \n");
                    strSqlString.AppendFormat("                     ON H.LOT_ID = A.LOT_ID \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN WS_FABMES2TJMES W (NOLOCK) ON A.LOT_ID = W.LOT_ID  \n");

                    if (lotIds.Count > 0)
                        strSqlString.AppendFormat("  WHERE A.LOT_ID = '{0}' \n", lotIds[0]);

                    if (lotIds.Count > 1)
                    {
                        foreach (var item in lotIds)
                        {
                            strSqlString.AppendFormat(" OR A.LOT_ID = '{0}' \n", item);
                        }
                    }

                }
                else if (Step == "MARKING_NO") // search by marking No
                {
                    string strMarkingNo = " (";

                    foreach (var item in lotIds)
                    {
                        strMarkingNo += "'" + item + "',";
                    }

                    strMarkingNo = strMarkingNo.Substring(0, strMarkingNo.Length - 1) + ") ";

                    strSqlString.AppendFormat("SELECT H.TX_DTTM 'Marking Time' \n");
                    strSqlString.AppendFormat("       , L.LOT_ID 'LOT_ID' \n");
                    strSqlString.AppendFormat("       , H.LOT_DESCRIPTION 'Lot Description' \n");
                    strSqlString.AppendFormat("       , L.LOT_UDF2 'MARKING_NO' \n");
                    strSqlString.AppendFormat("       , LT.VALUE1 'Lot Type' \n");
                    strSqlString.AppendFormat("       , LC.VALUE1 'Lot Category' \n");
                    strSqlString.AppendFormat("       , L.OPERATION_ID 'Operation' \n");
                    strSqlString.AppendFormat("       , O.OPERATION_SHORT_NAME 'Operation Name' \n");
                    strSqlString.AppendFormat("       , H.TX_USER_ID + ' : ' + H.TX_USER_NAME 'Worker' \n");
                    strSqlString.AppendFormat("       , L.MATERIAL_ID 'Material ID' \n");
                    strSqlString.AppendFormat("       , M.MATERIAL_NAME 'Material Name' \n");
                    strSqlString.AppendFormat("       , H.EQUIPMENT_ID 'Equipment ID' \n");
                    strSqlString.AppendFormat("       , E.EQUIPMENT_NAME 'Equipment Name' \n");
                    strSqlString.AppendFormat("       , ISNULL((SELECT 'Y' FROM NM_LOTS X (NOLOCK) WHERE X.TX_CODE = 'TX_SHIP' AND X.LOT_ID = L.LOT_ID),'N') 'Shipping_Flag' \n");
                    strSqlString.AppendFormat("  FROM NM_LOT_SUMMARY_NEW L (NOLOCK) \n");
                    strSqlString.AppendFormat("       INNER JOIN (SELECT LOT_ID, MIN(END_HISTORY_SEQ) AS END_HISTORY_SEQ \n");
                    strSqlString.AppendFormat("                     FROM NM_LOT_SUMMARY_NEW (NOLOCK) \n");
                    strSqlString.AppendFormat("                    WHERE LOT_UDF2 IN ");
                    strSqlString.AppendFormat(strMarkingNo + "     AND LOT_ID LIKE '%0' GROUP BY LOT_ID) X ON \n");
                    strSqlString.AppendFormat("                  L.LOT_ID = X.LOT_ID AND L.END_HISTORY_SEQ = X.END_HISTORY_SEQ \n");
                    strSqlString.AppendFormat("       INNER JOIN NM_LOT_HISTORY H (NOLOCK) ON L.LOT_ID = H.LOT_ID AND L.START_HISTORY_SEQ = H.HISTORY_SEQ \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA LT (NOLOCK) ON L.LOT_TYPE = LT.PK1 AND LT.TABLE_ID = 'LOT_TYPE' AND LT.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA LC (NOLOCK) ON L.LOT_CATEGORY = LC.PK1 AND LC.TABLE_ID = 'LOT_CATEGORY' AND LC.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("       INNER JOIN NM_OPERATIONS O (NOLOCK) ON L.SITE_ID = O.SITE_ID AND L.OPERATION_ID = O.OPERATION_ID \n");
                    strSqlString.AppendFormat("       INNER JOIN NM_MATERIALS M (NOLOCK) ON L.SITE_ID = M.SITE_ID AND L.MATERIAL_ID = M.MATERIAL_ID \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NM_EQUIPMENT E (NOLOCK) ON H.SITE_ID = E.SITE_ID AND H.EQUIPMENT_ID = E.EQUIPMENT_ID \n");
                    strSqlString.AppendFormat(" WHERE L.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("       AND L.LOT_UDF2 IN  \n");
                    strSqlString.AppendFormat(strMarkingNo + "\n");
                    strSqlString.AppendFormat(" ORDER BY H.TX_DTTM \n");
                }
                else if (Step == "REEL_ID") // search lot by reel Id
                {
                    string strReelId = " (";

                    foreach (var item in lotIds)
                    {
                        strReelId += "'" + item + "',";
                    }

                    strReelId = strReelId.Substring(0, strReelId.Length - 1) + ") \n";

                    strSqlString.AppendFormat(" SELECT DISTINCT * FROM ( \n");
                    strSqlString.AppendFormat(" SELECT A.LOT_ID,A.RELATION_LOT_ID \n");
                    strSqlString.AppendFormat("        , CASE WHEN (A.TX_CODE = 'TX_END' AND O2.PUSH_PULL_FLAG = 'N') OR A.TX_CODE = 'TX_REWORK' THEN O2.OPERATION_ID ELSE O.OPERATION_ID  END 'Operation' \n");
                    strSqlString.AppendFormat("  FROM NM_LOT_HISTORY A (NOLOCK) \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_OPERATIONS O (NOLOCK) ON A.SITE_ID = O.SITE_ID AND A.OPERATION_ID = O.OPERATION_ID \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NM_OPERATIONS O2 (NOLOCK) ON A.SITE_ID = O.SITE_ID AND A.PREV_OPERATION_ID = O2.OPERATION_ID \n");
                    strSqlString.AppendFormat("  WHERE 1=1  \n");
                    strSqlString.AppendFormat("    AND A.HISTORY_DELETE_FLAG <> 'Y' \n");
                    strSqlString.AppendFormat("    AND A.RELATION_LOT_ID IN " + strReelId);
                    strSqlString.AppendFormat("    ) SUB ");
                    strSqlString.AppendFormat("  WHERE SUB.Operation = 'OC360' OR SUB.Operation = 'OC156' ");
                }
                else if (Step == "WIP")
                {
                    strSqlString.AppendFormat(" SELECT A.LOT_ID,CASE \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_CREATE) + "' THEN 0 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_START) + "' THEN 1 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_END) + "' THEN 2 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_MOVE) + "' THEN 3 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_LOSS) + "' THEN 4 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_BONUS) + "' THEN 5 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_SPLIT) + "' THEN 6 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_COMBINE) + "' THEN 7 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_MERGE) + "' THEN 8 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_HOLD) + "' THEN 9 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_RELEASE) + "' THEN 10 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_ADAPT) + "' OR A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_ADAPT_UDF) + "' THEN 11 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_REWORK) + "' THEN 12 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_EDC) + "' THEN 13 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_SHIP) + "' THEN 14 \n");
                    strSqlString.AppendFormat("             WHEN A.TX_CODE = '" + Transaction.GetTxCode(Transaction.TxCode.TX_TERMINATED) + "' THEN 15 \n");
                    strSqlString.AppendFormat("             ELSE -1 \n");
                    strSqlString.AppendFormat("        END '_' \n");
                    strSqlString.AppendFormat("        , A.HISTORY_SEQ 'Seq' \n");
                    strSqlString.AppendFormat("        , A.TX_DTTM 'Tx Time' \n");// *
                    strSqlString.AppendFormat("        , CASE WHEN (A.TX_CODE = 'TX_END' AND O2.PUSH_PULL_FLAG = 'N') OR A.TX_CODE = 'TX_REWORK' THEN O2.OPERATION_ID ELSE O.OPERATION_ID  END 'Operation' \n");
                    strSqlString.AppendFormat("        , CASE WHEN (A.TX_CODE = 'TX_END' AND O2.PUSH_PULL_FLAG = 'N') OR A.TX_CODE = 'TX_REWORK' THEN O2.OPERATION_SHORT_NAME ELSE O.OPERATION_SHORT_NAME  END 'Operation Name' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.TX_CODE = 'TX_MERGE' OR A.TX_CODE = 'TX_SPLIT' OR A.TX_CODE = 'TX_COMBINE' THEN A.RELATION_LOT_ID ELSE '' END 'Relation Lot' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.TX_CODE = 'TX_MERGE' OR A.TX_CODE = 'TX_SPLIT' OR A.TX_CODE = 'TX_COMBINE' THEN A.RELATION_MATERIAL_ID ELSE '' END 'Relation Material' \n");
                    strSqlString.AppendFormat("        , EF.LOT_EXTRA_FIELD1 'Product Order' \n");
                    strSqlString.AppendFormat("        , H.RELATION_LOT_ID 'Assy Lot' \n");
                    strSqlString.AppendFormat("        , MC.VALUE1 'Material Category' \n");
                    strSqlString.AppendFormat("        , MS.VALUE1 'Material Series' \n");
                    strSqlString.AppendFormat("        , MM.VALUE1 'Material Model'  \n");
                    strSqlString.AppendFormat("        , A.MATERIAL_ID 'Material Id' \n");
                    strSqlString.AppendFormat("        , M.MATERIAL_NAME 'Material Name' \n");
                    strSqlString.AppendFormat("        , A.ROUTE_ID 'Route' \n");
                    strSqlString.AppendFormat("        , R.ROUTE_NAME 'Route Name' \n");
                    strSqlString.AppendFormat("        , A.EQUIPMENT_ID 'Equipment' \n");
                    strSqlString.AppendFormat("        , E.EQUIPMENT_NAME 'Equipment Name' \n");
                    strSqlString.AppendFormat("        , A.QTY1 'Chip Qty' \n");
                    strSqlString.AppendFormat("        , O.UNIT_1 'Unit' \n");
                    strSqlString.AppendFormat("        , A.PREV_OPERATION_ID 'Old Operation' \n");
                    strSqlString.AppendFormat("        , A.PREV_QTY1 'Old Qty' \n");
                    strSqlString.AppendFormat("        , LT.VALUE1 'Lot Type' \n");
                    strSqlString.AppendFormat("        , LC.VALUE1 'Lot Category' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.TX_USER_NAME = '' THEN A.TX_USER_ID ELSE A.TX_USER_NAME END 'User Name' \n");
                    strSqlString.AppendFormat("        , A.LOT_UDF2 'Marking No' \n");
                    strSqlString.AppendFormat("        , A.HOLD_FLAG 'Hold' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.HOLD_FLAG = 'Y' THEN A.HOLD_CODE ELSE ''  END 'Hold Code' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.HOLD_FLAG = 'Y' THEN A.HOLD_NAME ELSE ''  END 'Hold Name' \n");
                    strSqlString.AppendFormat("        , CASE WHEN A.TX_CODE = 'TX_REWORK' THEN A.REWORK_FLAG ELSE ''  END 'Rework' \n");
                    strSqlString.AppendFormat("        , A.REWORK_CODE 'Rework Code' \n");
                    strSqlString.AppendFormat("        , A.REWORK_COUNT 'Rework Count' \n");
                    strSqlString.AppendFormat("        , A.TX_COMMENT 'Comment' \n");
                    strSqlString.AppendFormat("        , A.MOVEIN_DTTM 'Move In Time' \n");
                    strSqlString.AppendFormat("        , A.TX_CODE 'Tx Code' \n");
                    strSqlString.AppendFormat("        , A.CUSTOMER_LOT_ID 'Customer Lot' \n");
                    strSqlString.AppendFormat("        , A.DELETE_FLAG 'Delete Flag' \n");
                    strSqlString.AppendFormat("   FROM NM_LOT_HISTORY A (NOLOCK) \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_OPERATIONS O (NOLOCK) ON A.SITE_ID = O.SITE_ID AND A.OPERATION_ID = O.OPERATION_ID \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NM_OPERATIONS O2 (NOLOCK) ON A.SITE_ID = O.SITE_ID AND A.PREV_OPERATION_ID = O2.OPERATION_ID \n");
                    strSqlString.AppendFormat("        INNER JOIN NM_MATERIALS M (NOLOCK) ON A.MATERIAL_ID = M.MATERIAL_ID AND M.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN WM_LOT_HISTORY_EXTRA_FIELD EF (NOLOCK) ON EF.LOT_ID = A.LOT_ID AND EF.HISTORY_SEQ = A.HISTORY_SEQ \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NM_EQUIPMENT E (NOLOCK) ON A.SITE_ID = E.SITE_ID AND A.EQUIPMENT_ID = E.EQUIPMENT_ID \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NB_CODE_DATA MC (NOLOCK) ON M.SITE_ID = MC.SITE_ID AND M.MATERIAL_CATEGORY = MC.PK1 AND MC.TABLE_ID = 'MAT_CATEGORY' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NB_CODE_DATA MS (NOLOCK) ON M.SITE_ID = MS.SITE_ID AND M.MATERIAL_GROUP1 = MS.PK1 AND MS.TABLE_ID = 'MAT_SERIES' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NB_CODE_DATA MM (NOLOCK) ON M.SITE_ID = MM.SITE_ID AND M.MATERIAL_GROUP2 = MM.PK1 AND MM.TABLE_ID = 'MODEL_TYPE' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NM_ROUTES R (NOLOCK) ON A.SITE_ID = R.SITE_ID AND A.ROUTE_ID = R.ROUTE_ID \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NB_CODE_DATA LT (NOLOCK) ON A.SITE_ID = LT.SITE_ID AND A.LOT_TYPE = LT.PK1 AND LT.TABLE_ID = 'LOT_TYPE' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN NB_CODE_DATA LC (NOLOCK) ON A.SITE_ID = LC.SITE_ID AND A.LOT_CATEGORY = LC.PK1 AND LC.TABLE_ID = 'LOT_CATEGORY' \n");
                    strSqlString.AppendFormat("        LEFT OUTER JOIN dbo.NM_LOT_HISTORY H (NOLOCK) ON H.SITE_ID = A.SITE_ID AND H.OPERATION_ID = '{0}' AND H.LOT_ID = A.RELATION_LOT_ID AND H.TX_CODE = '{1}' AND A.TX_CODE = '{2}' \n", OIComnConstants.FLIP_BONDING_INSPECTION_OPERATION, OIComnConstants.TX_MERGE, OIComnConstants.TX_COMBINE);
                    strSqlString.AppendFormat("  WHERE 1=1  \n");
                    strSqlString.AppendFormat("    AND A.HISTORY_DELETE_FLAG <> 'Y' \n");

                    if (lotIds.Count > 0)
                        strSqlString.AppendFormat("    AND (A.LOT_ID = '{0}' \n", lotIds[0].Trim());


                    if (lotIds.Count > 1)
                    {
                        foreach (var item in lotIds)
                        {
                            strSqlString.AppendFormat("  OR A.LOT_ID = '{0}' \n", item.NullString());
                        }
                    }
                    if (lotIds.Count > 0)
                        strSqlString.AppendFormat(" )  \n");

                    strSqlString.AppendFormat("  ORDER BY A.LOT_ID ASC ,A.HISTORY_SEQ ASC \n");
                }
                else if (Step == "DATA")
                {
                    strSqlString.AppendFormat("SELECT L.TX_DTTM 'Work Date' \n");
                    strSqlString.AppendFormat("       , L.LOT_ID 'Lot ID' \n");
                    strSqlString.AppendFormat("       , L.LOT_UDF4 'FA ID' \n");
                    strSqlString.AppendFormat("       , LT.VALUE1 'Lot Type' \n");
                    strSqlString.AppendFormat("       , L.LOT_UDF2 'Marking No' \n");
                    strSqlString.AppendFormat("       , L.MATERIAL_ID 'Material' \n");
                    strSqlString.AppendFormat("       , MG.VALUE1 'Material Group' \n");
                    strSqlString.AppendFormat("       , MG2.VALUE1 'Material Group 2' \n");
                    strSqlString.AppendFormat("       , L.QTY1 'Qty' \n");
                    strSqlString.AppendFormat("       , L.SITE_ID 'Site' \n");
                    strSqlString.AppendFormat("       , R.VENDOR_ID 'Vendor ID' \n");
                    strSqlString.AppendFormat("       , R.CUSTOMER_ID 'Customer ID' \n");
                    strSqlString.AppendFormat("       , C.CUSTOMER_NAME 'Customer Name' \n");
                    strSqlString.AppendFormat("       , R.SHIP_UDF1 'Customer ID2' \n");
                    strSqlString.AppendFormat("       , C2.CUSTOMER_NAME 'Customer Name2' \n");
                    strSqlString.AppendFormat("       , R.CUSTOMER_MATID 'Customer Material' \n");
                    strSqlString.AppendFormat("       , R.CUSTOMER_MATCLASS 'Customer Material Class' \n");
                    strSqlString.AppendFormat("       , R.CUSTOMER_FIXED 'Customer Fixed' \n");
                    strSqlString.AppendFormat("       , R.SHIP_BOXNID 'Ship Box' \n");
                    strSqlString.AppendFormat("       , R.SHIP_REEL 'Ship Reel' \n");
                    strSqlString.AppendFormat("       , R.SHIP_CARTON 'Ship Carton' \n");
                    strSqlString.AppendFormat("       , R.SHIP_BOXNO1 'Ship Box No 1' \n");
                    strSqlString.AppendFormat("       , R.SHIP_BOXNO2 'Ship Box No 2' \n");
                    strSqlString.AppendFormat("       , R.SHIP_BOXNO3 'Ship Box No 3' \n");
                    strSqlString.AppendFormat("       , R.SHIP_BOXNO4 'Ship Box No 4' \n");
                    strSqlString.AppendFormat("       , R.SHIP_POTYPE + ' : ' + SP.VALUE1 'Ship PO Type' \n");
                    strSqlString.AppendFormat("       , R.SUM_QTY1 'Total Qty' \n");
                    strSqlString.AppendFormat("       , L.TX_COMMENT 'Comment' \n");
                    strSqlString.AppendFormat("       , LH.TX_DTTM  'LB DATE' \n");
                    strSqlString.AppendFormat("  FROM NM_LOTS L (NOLOCK) \n");
                    strSqlString.AppendFormat("       INNER JOIN WS_REEL_SHIPPING R (NOLOCK) ON L.LOT_ID = R.LOT_ID \n");
                    strSqlString.AppendFormat("       INNER JOIN WS_CUSTOMERS C (NOLOCK) ON R.CUSTOMER_ID = C.CUSTOMER_ID \n");
                    strSqlString.AppendFormat("       LEFT JOIN WS_CUSTOMERS C2 (NOLOCK) ON R.SHIP_UDF1 = C2.CUSTOMER_ID \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NM_MATERIALS M (NOLOCK) ON L.MATERIAL_ID = M.MATERIAL_ID AND M.SITE_ID = 'WHCSP' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA LT (NOLOCK) ON L.LOT_TYPE = LT.PK1 AND LT.SITE_ID = 'WHCSP' AND LT.TABLE_ID = 'LOT_TYPE' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA MG (NOLOCK) ON M.MATERIAL_GROUP1 = MG.PK1 AND MG.SITE_ID = 'WHCSP' AND MG.TABLE_ID = 'MAT_GROUP1' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA MG2 (NOLOCK) ON M.MATERIAL_GROUP2 = MG2.PK1 AND MG2.SITE_ID = 'WHCSP' AND MG2.TABLE_ID = 'MAT_GROUP2' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN NB_CODE_DATA SP (NOLOCK) ON R.SHIP_POTYPE = SP.PK1 AND SP.SITE_ID = 'WHCSP' AND SP.TABLE_ID = 'SHIP_POTYPE' \n");
                    strSqlString.AppendFormat("       LEFT OUTER JOIN (SELECT TOP(1) * FROM NM_LOT_HISTORY WHERE OPERATION_ID = 'OC360' AND TX_CODE = 'TX_START' AND HISTORY_DELETE_FLAG <> 'Y' AND LOT_UDF6 = 'Y') AS LH ON LH.LOT_ID = L.LOT_UDF3 \n");
                    strSqlString.AppendFormat(" WHERE L.TX_CODE = 'TX_SHIP' \n");
                    // strSqlString.AppendFormat("       AND L.TX_DTTM BETWEEN '{0}' AND '{1}' \n", fromDate, toDate);

                    if (lotIds.Count > 0)
                        strSqlString.AppendFormat("       AND ( L.LOT_ID = '{0}' \n", lotIds[0]);

                    if (lotIds.Count > 1)
                    {
                        foreach (var item in lotIds)
                        {
                            strSqlString.AppendFormat("  OR L.LOT_ID = '{0}' \n", item.NullString());
                        }
                    }
                    if (lotIds.Count > 0)
                        strSqlString.AppendFormat(" )  \n");

                    strSqlString.AppendFormat("       AND L.SITE_ID = 'CUSTOMER' \n");
                    strSqlString.AppendFormat(" ORDER BY L.TX_DTTM, L.LOT_UDF4, L.LOT_ID \n");
                }
                else
                {
                    strSqlString.Length = 0;
                }

                return strSqlString.ToString();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
                return "";
            }
        }

        private void gvList_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "MARKING_TIME" || e.Column.FieldName == "LOT_ID" || e.Column.FieldName == "MARKING_NO" ||
                e.Column.FieldName == "MATERIAL_NAME" || e.Column.FieldName == "ISMECA" || e.Column.FieldName == "START_END_LINE" ||
                e.Column.FieldName == "END_END_LINE" || e.Column.FieldName == "REEL_ID" || e.Column.FieldName == "REMARK" || e.Column.FieldName == "PKG_LOT" || e.Column.FieldName == "QTY")
            {
                string v1 = gvList.GetRowCellValue(e.RowHandle1, e.Column).NullString();
                string v2 = gvList.GetRowCellValue(e.RowHandle2, e.Column).NullString();
                e.Merge = v1 == v2;
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
                e.Merge = false;
            }
        }

        private void btnGetFileTemp_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "LotID-Data.xlsx";

                string url = Application.StartupPath + @"\\" + fileName;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (WebClient web1 = new WebClient())
                        web1.DownloadFile(url, saveFileDialog.FileName);
                    MsgBox.Show("Save File success!".Translation(), MsgType.Information);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0) // lot Id
            {
                txtLotID.Enabled = true;
                txtMarkingNo.Enabled = false;
                txtReelId.Enabled = false;

                txtMarkingNo.Text = "";
                txtReelId.Text = "";
            }
            else if (radioGroup1.SelectedIndex == 1) // marking No
            {
                txtLotID.Enabled = false;
                txtMarkingNo.Enabled = true;
                txtReelId.Enabled = false;

                txtLotID.Text = "";
                txtReelId.Text = "";
            }
            else
            {
                txtLotID.Enabled = false;
                txtMarkingNo.Enabled = false;
                txtReelId.Enabled = true;

                txtLotID.Text = "";
                txtMarkingNo.Text = "";
            }
        }
    }
}
