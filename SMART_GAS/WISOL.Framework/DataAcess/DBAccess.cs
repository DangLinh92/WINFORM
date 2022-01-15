using PROJ_B_DLL.DataAcess;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Wisol.DataAcess
{
    public class DBAccess
    {
        private string address = string.Empty;
        private int port = 0;
        private string serverID = string.Empty;
        private string serverPW = string.Empty;
        private string clientAddress = string.Empty;
        private string userId = string.Empty;
        //private string connectionString = "Data Source = 172.22.100.150\\SQLEXPRESS;Initial Catalog = WHNP1;Persist Security Info=True;User id = sa;Password = 123456@a;Connect Timeout=90";
        //private string connectionString = "Data Source = 10.70.21.72\\SQLEXPRESS;Initial Catalog = WHNP_TEST;Persist Security Info=True;User id = sa;Password = 123456;Connect Timeout=3";
        private string connectionString = "Data Source = 10.70.10.97;Initial Catalog = SMARGAS;User Id = sa;Password = Wisol@123;Connect Timeout=3";
        private string connectionString1 = "Data Source = 10.70.10.97;Initial Catalog = WHNP1;User Id = sa;Password = Wisol@123;Connect Timeout=3";
        public DBAccess(string _connectionString)
        {
            this.connectionString = _connectionString;
        }
        public DBAccess(string _clientAddress, string _address, int _port, string _serverID, string _serverPW, string _userId)
        {
            this.address = _address;
            this.port = _port;
            this.serverID = _serverID;
            this.serverPW = _serverPW;
            this.clientAddress = _clientAddress;
            this.userId = _userId;
        }
        public DBAccess(ServerInfo server, string userId)
        {
            this.address = server.ServerIp;
            this.port = server.ServicePort;
            this.serverID = server.ServiceID;
            this.serverPW = server.ServicePassword;
            this.clientAddress = server.ClientIp;
            this.userId = userId;
        }
        public DBAccess(ServerInfo server)
        {
            this.address = server.ServerIp;
            this.port = server.ServicePort;
            this.serverID = server.ServiceID;
            this.serverPW = server.ServicePassword;
            this.clientAddress = server.ClientIp;
        }
        public DBAccess()
        {
        }

        public string ClientAddress
        {
            get
            {
                return clientAddress;
            }
            set
            {
                clientAddress = value;
            }
        }

        public string ServerID
        {
            get
            {
                return serverID;
            }
            set
            {
                serverID = value;
            }
        }
        public string ServerPW
        {
            get
            {
                return serverPW;
            }
            set
            {
                serverPW = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        public int connectionStringParam = 0;// 0 default

        public ResultDB ExcuteQuery(string query)
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            NetworkStream serverStream = null;
            BinaryFormatter bf = null;
            SetterDB setter = new SetterDB();
            setter.ClientAddress = clientAddress;
            setter.ServerID = serverID;
            setter.ServerPW = serverPW;
            setter.Query = query;
            setter.UserId = userId;
            setter.DbAccessType = DBAccessType.DB;

            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(address, port);
            serverStream = clientSocket.GetStream();



            bf = new BinaryFormatter();
            bf.Serialize(serverStream, setter);

            ResultDB result = (ResultDB)bf.Deserialize(serverStream);

            serverStream.Flush();
            clientSocket.Close();

            Clipboard.SetText(query);
            return result;
        }


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

        public ResultDB ExcuteProcWithBytes(string ProcName, string[] Parameter,string bytesParam, string[] Value,byte[] bytes, DBAccessType type = DBAccessType.DB, int rfcTableCount = 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                if (Parameter.Length != Value.Length || !(!string.IsNullOrEmpty(bytesParam) && bytes != null))
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
                return ExcuteProcWithBytes(ProcName, dic,bytesParam,bytes, type, rfcTableCount);
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

        public ResultDB ExcuteProcWithTableParam(string ProcName, string[] Parameter,string tableParam, string[] Value,DataTable table, DBAccessType type = DBAccessType.DB, int rfcTableCount = 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                if (Parameter.Length != Value.Length || !(!string.IsNullOrEmpty(tableParam) && table != null))
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
                return ExcuteProcWithTableParam(ProcName, dic,tableParam,table, type, rfcTableCount);
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


        public ResultDB ExcuteProcP(string ProcName, string[] Parameter, string[] Value, DBAccessType type = DBAccessType.DB, int rfcTableCount = 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                if (Parameter.Length != Value.Length)
                {
                    ResultDB result = new ResultDB();
                    result.ReturnInt = -1;
                    result.ReturnString = "파라메터와 값의 수가 일치하지 않습니다.";
                    return result;
                }
                for (int i = 0; i < Parameter.Length; i++)
                {
                    dic.Add(Parameter[i], Value[i]);
                }
                return ExcuteProcPR(ProcName, dic);
            }
            catch (Exception ex)
            {
                ResultDB result = new ResultDB();
                result.ReturnInt = -1;
                result.ReturnString = ex.Message;
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
                string connection = connectionString;
                if (connectionStringParam == 1)
                {
                    connection = connectionString1;
                }

                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connection);
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
                connectionStringParam = 0;
            }
        }

        public ResultDB ExcuteProcWithBytes(string ProcName, Dictionary<string, string> Dictionary,string byteParam,byte[] bytes, DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                string connection = connectionString;
                if (connectionStringParam == 1)
                {
                    connection = connectionString1;
                }

                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd = new SqlCommand(ProcName.Replace('.', '@'), con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string> item in Dictionary)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                cmd.Parameters.AddWithValue(byteParam, bytes);

                SqlParameter N_RETURN = new SqlParameter("@N_RETURN", SqlDbType.Int);
                N_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(N_RETURN);

                SqlParameter V_RETURN = new SqlParameter("@V_RETURN", SqlDbType.NVarChar, 100);
                V_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(V_RETURN);

                da = new SqlDataAdapter(cmd);
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
                connectionStringParam = 0;
            }
        }

        public ResultDB ExcuteProcWithTableParam(string ProcName, Dictionary<string, string> Dictionary,string tableParam,DataTable table, DBAccessType dbAccesstype = DBAccessType.DB, int rfcTableCount = 0)
        {
            try
            {
                string connection = connectionString;
                if (connectionStringParam == 1)
                {
                    connection = connectionString1;
                }

                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd = new SqlCommand(ProcName.Replace('.', '@'), con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string> item in Dictionary)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }

                SqlParameter sqlTableParam = cmd.Parameters.AddWithValue(tableParam, table);
                sqlTableParam.SqlDbType = SqlDbType.Structured;

                SqlParameter N_RETURN = new SqlParameter("@N_RETURN", SqlDbType.Int);
                N_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(N_RETURN);

                SqlParameter V_RETURN = new SqlParameter("@V_RETURN", SqlDbType.NVarChar, 100);
                V_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(V_RETURN);

                da = new SqlDataAdapter(cmd);
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
                connectionStringParam = 0;
            }
        }

        public ResultDB ExcuteProc(string ProcName)
        {
            try
            {
                string connection = connectionString;
                if (connectionStringParam == 1)
                {
                    connection = connectionString1;
                }

                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd = new SqlCommand(ProcName.Replace('.', '@'), con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter N_RETURN = new SqlParameter("@N_RETURN", SqlDbType.Int);
                N_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(N_RETURN);

                SqlParameter V_RETURN = new SqlParameter("@V_RETURN", SqlDbType.NVarChar, 100);
                V_RETURN.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(V_RETURN);

                da = new SqlDataAdapter(cmd);
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
                connectionStringParam = 0;
            }
        }

        public ResultDB ExcuteProcPR(string ProcName, Dictionary<string, string> Dictionary)
        {
            try
            {
                string connecPR = "Data Source = 10.70.21.72\\SQLEXPRESS;Initial Catalog = SiplaceOIS_LineI;Persist Security Info=True;User id = sa;Password = 123456;Connect Timeout=3";
                ResultDB resultDb = new ResultDB();
                DataSet dataSet = new DataSet();
                SqlConnection con = new SqlConnection(connecPR);
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
        }
    }
}

namespace PROJ_B_DLL.DataAcess
{
    public enum DBAccessType
    {
        DB = 0,
        RFC = 1
    }
}
