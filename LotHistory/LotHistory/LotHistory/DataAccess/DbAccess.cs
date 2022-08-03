using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotHistory.DataAccess
{
    public static class DbAccess
    {
        public static string conString = "Persist Security Info=True;User ID=whcspadmin;Password=mesadmin;Initial Catalog=MESDB;Data Source=10.70.21.214;";
        private static SqlConnection sqlCon;

        public static void SqlExecuteNonQuery(string sqlQry)
        {
            try
            {
                using (var sqlCon = new SqlConnection(conString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlQry, sqlCon))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { throw; }
        }

        public static DataTable SqlExecuteDataTable(string sqlQry)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                using (sqlCon = new SqlConnection(conString))
                {
                    if (sqlCon.State != ConnectionState.Open)
                    {
                        sqlCon.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(sqlQry, sqlCon))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlQry, sqlCon))
                        {
                            da.Fill(ds);
                            dt = ds.Tables[0];
                            sqlCon.Close();
                        }
                    }
                }
                return dt;
            }
            catch
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
                throw;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public static SqlDataReader ExecuteReader(string sql, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = conString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (parameters != null)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
