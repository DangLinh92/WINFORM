using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WHC_FinaceService
{
    public class DBAccess
    {
        private string connectionString = "Data Source = 10.70.10.97;Initial Catalog = WHC_FINANCE_REPORT;User Id = sa;Password = Wisol@123;Connect Timeout=3";

        public ResultDB ExcuteProc(string ProcName, string[] Parameter, string[] Value, DBAccessType type = DBAccessType.DB, int rfcTableCount = 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                if (Parameter.Length != Value.Length)
                {
                    ResultDB result = new ResultDB();
                    result.ReturnInt = -1;
                    result.ReturnString = "Thông số và số lượng giá trị không khớp.";
                    //result.ReturnString = "파라메터와 값의 수가 일치하지 않습니다.";
                    return result;
                }
                for (int i = 0; i < Parameter.Length; i++)
                {
                    dic.Add(Parameter[i], Value[i]);
                }
                return ExcuteProc(ProcName, dic, type, rfcTableCount);
            }
            catch (Exception ex)
            {
                ResultDB result = new ResultDB();
                result.ReturnInt = -1;
                result.ReturnString = ex.Message;
                Console.WriteLine(result);
                return result;
            }
            finally
            {
            }
        }

        public ResultDB ExcuteProc(string ProcName, Dictionary<string, string> Dictionary, DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd = new SqlCommand(ProcName.Replace('.', '@'), con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string> item in Dictionary)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                SqlParameter N_RETURN = new SqlParameter("@N_RETURN", SqlDbType.Int);
                N_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(N_RETURN);

                SqlParameter V_RETURN = new SqlParameter("@V_RETURN", SqlDbType.NVarChar, 100);
                V_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(V_RETURN);

                da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 60;
                da.Fill(ds);
                con.Close();
                resultDb.ReturnDataSet = ds;
                resultDb.ReturnInt = (int)N_RETURN.Value;
                resultDb.ReturnString = (string)V_RETURN.Value;
                return resultDb;
            }
            catch (SocketException)
            {
                ResultDB result = new ResultDB();
                result.ReturnInt = -1;
                result.ReturnString = "서버 연결이 불가능합니다. Không kết nối được đến máy chủ.";
                return result;
            }
            finally
            {
            }
        }
    }

    public enum DBAccessType
    {
        DB = 0,
        RFC = 1
    }

    [Serializable]
    public class ResultDB
    {
        private int returnInt = 0;
        private string returnString = string.Empty;
        private DataSet returnDataSet = new DataSet();

        public int ReturnInt
        {
            get
            {
                return returnInt;
            }
            set
            {
                returnInt = value;
            }
        }
        public string ReturnString
        {
            get
            {
                return returnString;
            }
            set
            {
                returnString = value;
            }
        }
        public DataSet ReturnDataSet
        {
            get
            {
                return returnDataSet;
            }
            set
            {
                returnDataSet = value.Copy();
            }
        }
    }
}
