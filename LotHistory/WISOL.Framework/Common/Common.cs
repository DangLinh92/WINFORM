using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wisol.Common
{
    public static class Common
    {

        public static string NullString(this object result)
        {
            if (result == null)
                return string.Empty;
            else
                return result.ToString().Trim();
        }

        public static string IfNullIsZero(this object result)
        {
            if (result == null)
                return "0";
            else
                return result.ToString().Trim() =="" ? "0" : result.ToString().Trim();
        }

        public static Control[] GetAllControls(Control containerControl)
        {
            List<Control> allControls = new List<Control>();
            Queue<Control.ControlCollection> queue = new Queue<Control.ControlCollection>();
            queue.Enqueue(containerControl.Controls);

            Task task = new Task(() =>
            {
                while (queue.Count > 0)
                {
                    Control.ControlCollection controls = (Control.ControlCollection)queue.Dequeue();
                    if (controls == null || controls.Count == 0) continue;

                    foreach (Control control in controls)
                    {
                        allControls.Add(control);
                        queue.Enqueue(control.Controls);
                    }
                }
            });

            task.Start();
            task.Wait();

            return allControls.ToArray();
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
                var adapter = new OleDbDataAdapter { SelectCommand = new OleDbCommand(strQuery, conn) };
                adapter.Fill(new DataSet());

                return new DataSet();
            }
            catch
            {
                return null;
            }
        }


    }
}
