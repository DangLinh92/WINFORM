using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace Wisol.Common
{
    public class Converter
    {
        public static T ParseValue<T>(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    if (typeof(T) == typeof(String))
                    {
                        value = String.Empty;
                    }
                    else
                    {
                        value = default(T);
                    }

                    return (T)value;
                }

                if (typeof(T) == typeof(Int16) ||
                    typeof(T) == typeof(Int32) ||
                    typeof(T) == typeof(Int64) ||
                    typeof(T) == typeof(UInt16) ||
                    typeof(T) == typeof(UInt32) ||
                    typeof(T) == typeof(UInt64) ||
                    typeof(T) == typeof(Single) ||
                    typeof(T) == typeof(Double) ||
                    typeof(T) == typeof(Decimal))
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    if (value.NullString() == string.Empty)
                    {
                        value = 0;
                    }
                    else
                    {
                        value = (T)converter.ConvertFromString(value.ToString().Replace(",", ""));
                    }
                }
                else if (typeof(T) == typeof(String))
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    value = (T)converter.ConvertFromString(value.ToString());
                }
                else if (typeof(T) == typeof(Boolean))
                {
                    if (value.ToString() == "" || value.ToString() == "0" || value.ToString().ToUpper() == "FALSE")
                        value = false;
                    else
                        value = true;
                }
                else if (typeof(T) == typeof(IPAddress))
                {
                    value = IPAddress.Parse(value.ToString());
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    if (value.ToString().Length == 8)
                    {
                        value = DateTime.Parse(
                            String.Format("{0}-{1}-{2}", value.ToString().Substring(0, 4),
                            value.ToString().Substring(4, 2), value.ToString().Substring(6, 2)));
                    }
                    else if (value.ToString().Length == 14)
                    {
                        value = DateTime.ParseExact(value.NullString(), "yyyyMMddHHmmss", null);
                    }
                    else
                    {
                        value = DateTime.Parse(value.ToString());
                    }
                }
                else if (typeof(T) == typeof(TimeSpan))
                {
                    value = TimeSpan.Parse(value.ToString());
                }
                else
                {
                    value = default(T);
                    return (T)value;
                }

                return (T)value;
            }
            catch
            {
                value = default(T);
                return (T)value;
            }
        }

        public static string GetDataTableToXml(DataTable dt)
        {
            string sXML = "";
            DataTable dtNew = dt.Copy();
            dtNew.TableName = "Table";

            using (MemoryStream ms = new MemoryStream())
            {
                dtNew.WriteXml(ms, XmlWriteMode.IgnoreSchema);
                ms.Flush();
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    sXML = sr.ReadToEnd();
                    sr.Close();
                }
                ms.Close();
            }
            sXML = Regex.Replace(sXML
                                , @">(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?<"
                                , @">${year}${month}${date}<"
                                , RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            return sXML;
        }

        public static string GetDataTableToXml(DataTable dt, string exceptColumn)
        {
            string sXML = "";
            string[] exceptList = exceptColumn.Split(',');
            for (int i = 0; i < exceptColumn.Length; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName == exceptList[i].Trim())
                    {
                        dt.Columns.Remove(dt.Columns[j]);
                    }
                }
            }
            DataTable dtNew = dt.Copy();
            dtNew.TableName = "Table";

            using (MemoryStream ms = new MemoryStream())
            {
                dtNew.WriteXml(ms, XmlWriteMode.IgnoreSchema);
                ms.Flush();
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    sXML = sr.ReadToEnd();
                    sr.Close();
                }
                ms.Close();
            }
            sXML = Regex.Replace(sXML
                                , @">(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?<"
                                , @">${year}${month}${date}<"
                                , RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            return sXML;
        }

        public static string ConvertDataTableToXML(DataTable dt)
        {
            return ConvertDataTableToXML(dt, "");
        }

        public static string ConvertDataTableToXML(DataTable dt, string dateFormat)
        {
            return ConvertDataTableToXML(dt, dateFormat, "");
        }

        public static string ConvertDataTableToXML(DataTable dt, string dateFormat, string columns)
        {
            List<string> columnList = columns.Split(',').Select(p => p.Trim()).ToList();

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlNode elementNode = doc.CreateNode(XmlNodeType.Element, "DocumentElement", null);
            doc.AppendChild(elementNode);

            foreach (DataRow dr in dt.Rows)
            {
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "Table", null);

                foreach (DataColumn dc in dt.Columns)
                {
                    if (columns.Trim().Length > 0)
                    {
                        if (!columnList.Contains(dc.ColumnName))
                            continue;
                    }

                    XmlNode columnNode = doc.CreateNode(XmlNodeType.Element, dc.ColumnName, null);

                    Type type = dc.DataType.GetType();
                    string a = type.FullName;

                    if (dateFormat != "")
                    {
                        DateTime dtResult = DateTime.Now;

                        if (DateTime.TryParse(dr[dc.ColumnName].NullString(), out dtResult))
                        {
                            columnNode.InnerText = dtResult.ToString(dateFormat);
                        }
                        else
                        {
                            columnNode.InnerText = dr[dc.ColumnName].NullString();
                        }
                    }
                    else
                    {
                        columnNode.InnerText = dr[dc.ColumnName].NullString();
                    }

                    tableNode.AppendChild(columnNode);
                }

                elementNode.AppendChild(tableNode);
            }

            doc.AppendChild(elementNode);

            return doc.OuterXml;
        }
        public DataTable GetChangedData(GridControl Grid)
        {
            DataTable dt = new DataTable();

            if (Grid.DataSource == null)
                return dt;

            if (Grid.Views.Count > 0)
            {
                if (Grid.DefaultView is GridView)
                {
                    (Grid.DefaultView as GridView).FocusedRowHandle = -1;
                }
            }

            dt = (Grid.DataSource as DataTable).GetChanges();


            if (dt == null)
                return new DataTable();

            return dt;
        }

        public static void SetEmptyColumnVisibleFlase(GridView gv)
        {
            try
            {
                bool emptyFlag = true;
                if (gv.Columns.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    emptyFlag = true;
                    for (int j = 0; j < gv.RowCount; j++)
                    {
                        if (gv.GetDataRow(j)[i].NullString().Trim() != string.Empty)
                        {
                            emptyFlag = false;
                            break;
                        }
                    }
                    if (emptyFlag == true)
                    {
                        gv.Columns[i].Visible = false;
                    }
                }
            }
            catch
            {
            }
        }

        public static DataSet GetDataSetFromExcelFile(string fileName)
        {
            string connString = string.Empty;

            if (System.IO.Path.GetExtension(fileName).ToUpper() == ".XLSX")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName +
                                                ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"";
            }
            else
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName +
                                                ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";
            }

            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();

            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

            string strSheet = "";
            foreach (DataRow dr in schemaTable.Rows)
            {
                if (dr["Table_Name"].ToString().IndexOf('$') > -1)
                {
                    strSheet = dr["Table_Name"].ToString();
                    break;
                }
            }

            conn.Close();

            if (strSheet == "")
                return null;

            string strQuery;

            strQuery = "select * from [" + strSheet + "]";

            try
            {
                OleDbCommand cmd = new OleDbCommand(strQuery, conn);
                OleDbDataAdapter Adapter = new OleDbDataAdapter();
                Adapter.SelectCommand = cmd;
                DataSet dsExcel = new DataSet();

                Adapter.Fill(dsExcel);

                return dsExcel;
            }
            catch
            {
                return null;
            }
        }
    }
}
