using PROJ_B_DLL.DataAcess;
using PROJ_B_DLL.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Wisol.DataAcess
{
    public static class Extensions // CS1106  
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
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
        private string connectionString = "Data Source = 10.70.10.97;Initial Catalog = MRO;User Id = sa;Password = Wisol@123;Connect Timeout=10";
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
        // *************************************************************
        // Manh tao them de copy vao ke hoach tuan *********************
        public int ExcuteProcNoneQuery(string ProName) {
            int i = 0;
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd = new SqlCommand(ProName.Replace('.', '@'), con);
                cmd.CommandType = CommandType.StoredProcedure;

                i = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return i;
            }
            catch (Exception e) {
                MessageBox.Show("Lỗi trong khi kết nối máy chủ.");
                return 0;
            }
            
        }
        public void ExecuteNoneQuery(string Sql) {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e ) {
                MessageBox.Show("Lỗi trong khi kết nối máy chủ.");
            }
        }

        public DataTable ExecuteQuery(string Sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(Sql, con);
                SqlDataAdapter dap = new SqlDataAdapter(Sql, con);
                dap.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return null;
            }
        }
        public DataTable THEO_DOI_NGUYEN_LIEU_WLP1(DataTable dt) 
        {
            DataTable my_return = new DataTable();
            DataTable tempTable = new DataTable();
            tempTable.Columns.Add("CODE", typeof(string));
            tempTable.Columns.Add("LUONG_CHUA_NHAP", typeof(double));
            tempTable.Columns.Add("LUONG_DUNG_MOT_NGAY", typeof(double));
            tempTable.Columns.Add("LUONG_DUNG_MOT_THANG", typeof(double));
            tempTable.Columns.Add("TON_CAN_CO", typeof(double));
            tempTable.Columns.Add("CHENH_LECH", typeof(double));
            tempTable.Columns.Add("SO_LUONG_DAT_HANG", typeof(double));
            tempTable.Columns.Add("SO_TIEN_DAT_HANG", typeof(double));
            //DataRow dr = tempTable.NewRow();
            //dt.Columns.Add();
            double luong_chua_nhap;
            double luong_dung_mot_ngay;
            double luong_dung_mot_thang;
            double ton_can_co;
            double chenh_lech;
            double so_luong_dat_hang;
            double so_tien_dat_hang;
            double ton_hien_tai;
           
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                string vCODE = dt.Rows[i]["CODE"].ToString();
                
                DataRow dr = tempTable.NewRow();

                DataTable vtable = ExecuteQuery("select QUANTITY from EWIPCHEMTEMP where CODE ='" + vCODE + "' and DEPARTMENT ='WLP1'");
                if (vtable.Rows.Count > 0)
                {
                    luong_chua_nhap = Convert.ToInt32(vtable.Rows[0][0].ToString());
                }
                else {
                    luong_chua_nhap = 0;
                }
                ton_hien_tai = Convert.ToInt32(dt.Rows[i]["QUANTITY"].ToString());
                
                switch (vCODE)
                {
                    
                    case "M0001":
                        luong_dung_mot_ngay = Convert.ToDouble(dt.Rows[i]["SO_NHAN_LUC"].ToString()) * Convert.ToDouble(dt.Rows[i]["SO_LAN_GIAO_CA"].ToString()) * Convert.ToDouble(dt.Rows[i]["TI_LE_SU_DUNG"].ToString()) / 100;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        //DataRow dr = tempTable.NewRow();
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0002":
                        luong_dung_mot_ngay = Convert.ToDouble(dt.Rows[i]["SO_NHAN_LUC"].ToString()) * Convert.ToDouble(dt.Rows[i]["SO_LAN_GIAO_CA"].ToString()) * Convert.ToDouble(dt.Rows[i]["TI_LE_SU_DUNG"].ToString()) / 100;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        //DataRow dr = tempTable.NewRow();
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;

                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0003":
                        luong_dung_mot_ngay = Convert.ToDouble(dt.Rows[i]["SO_NHAN_LUC"].ToString()) * Convert.ToDouble(dt.Rows[i]["SO_LAN_GIAO_CA"].ToString()) * Convert.ToDouble(dt.Rows[i]["TI_LE_SU_DUNG"].ToString()) / 100;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;

                        tempTable.Rows.InsertAt(dr, i);
                        break;
                    case "M0004":
                        luong_dung_mot_ngay = 1.5;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;

                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0005":
                        luong_dung_mot_ngay = 1.5;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0006":
                        luong_dung_mot_ngay = 0.5;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0007":
                        luong_dung_mot_ngay = 0.4;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0008":
                        luong_dung_mot_ngay = 0.4;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0009":
                        luong_dung_mot_ngay = 1.0;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0010":
                        luong_dung_mot_ngay = 0.14;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);
                        break;
                    case "M0011":
                        luong_dung_mot_ngay = 0.14;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0012":

                        break;
                    case "M0013":
                        luong_dung_mot_ngay = 1.5;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_thang * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_hien_tai + luong_chua_nhap - (ton_can_co - luong_dung_mot_thang) - chenh_lech;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0014":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 5;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co* Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0015":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 5;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0016":
                        luong_dung_mot_ngay = 0.1;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co* Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0017":
                        luong_dung_mot_ngay = 0.14;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0018":
                        luong_dung_mot_ngay = 0.06;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0019":
                        luong_dung_mot_ngay = 0.06;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        so_luong_dat_hang = Convert.ToDouble(dt.Rows[i]["MOQ"].ToString())* Convert.ToDouble(dt.Rows[i]["SO_LUONG_THIET_BI"].ToString());
                        ton_can_co = so_luong_dat_hang + luong_dung_mot_thang;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0020":
                        luong_dung_mot_ngay = 0.07;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0021":
                        luong_dung_mot_ngay = 0.11;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0022":
                        luong_dung_mot_ngay = 0.11;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0023":
                        luong_dung_mot_ngay = 0.11;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0024":
                        luong_dung_mot_ngay = 0.11;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0025":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0026":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0027":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0028":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0029":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0030":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0031":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0032":
                        luong_dung_mot_ngay = 0.36;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0033":
                        luong_dung_mot_ngay = 0.1;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0034":
                        luong_dung_mot_ngay = 0.04;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0035":
                        luong_dung_mot_ngay = 0.07;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0036":
                        luong_dung_mot_ngay = 0.1;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0037":
                        luong_dung_mot_ngay = 0.03;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0038":
                        luong_dung_mot_ngay = 0.07;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0039":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0040":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0041":
                        luong_dung_mot_ngay = 0.07;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = 18;
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0042":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 10;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0043":
                        luong_dung_mot_ngay = 0.14;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0044":
                        luong_dung_mot_ngay = 1.43;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0045":
                        luong_dung_mot_ngay = 1.07;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0046":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 15;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0047":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 10;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0048":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 15;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0049":
                        luong_dung_mot_ngay = 0.14;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0050":
                        luong_dung_mot_ngay = 0.11;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0051":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 1;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0052":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 1;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0053":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 1;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0054":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0055":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0056":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0057":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 1;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0058":
                        luong_dung_mot_ngay = 10.0;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0059":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 8;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        //so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_luong_dat_hang = 0;// Tam thoi chua tinh 
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0060":
                        luong_dung_mot_ngay = 0.1;
                        luong_dung_mot_thang = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString());
                        ton_can_co = luong_dung_mot_ngay * Convert.ToDouble(dt.Rows[i]["SO_NGAY_LAM_VIEC"].ToString()) * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString()) * Convert.ToDouble(dt.Rows[i]["DU_PHONG"].ToString()) - (Convert.ToDouble(dt.Rows[i]["LEAD_TIME"].ToString()) * luong_dung_mot_ngay);
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        //so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_luong_dat_hang = 0;//  Tam thoi chua tinh
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0061":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 3;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0062":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 3;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0063":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 5;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0064":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 10;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0065":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 10;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0066":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0067":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 3;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0068":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 3;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0069":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 30;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0070":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 2;
                        chenh_lech = ton_hien_tai + luong_chua_nhap - ton_can_co;
                        so_luong_dat_hang = ton_can_co * Convert.ToDouble(dt.Rows[i]["MOQ"].ToString());
                        so_tien_dat_hang = so_luong_dat_hang * Convert.ToDouble(dt.Rows[i][4].ToString());
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0071":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 0;
                        chenh_lech = 0;
                        so_luong_dat_hang = 0;
                        so_tien_dat_hang = 0;
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                    case "M0072":
                        luong_dung_mot_ngay = 0;
                        luong_dung_mot_thang = 0;
                        ton_can_co = 0;
                        chenh_lech = 0;
                        so_luong_dat_hang = 0;
                        so_tien_dat_hang = 0;
                        //tempTable.Rows.Add([{ }],[{ luong_chua_nhap, luong_dung_mot_ngay,luong_dung_mot_thang},ton_can_co,chenh_lech,so_luong_dat_hang,so_tien_dat_hang }]);
                        dr["CODE"] = vCODE;
                        dr["LUONG_CHUA_NHAP"] = luong_chua_nhap;
                        dr["LUONG_DUNG_MOT_NGAY"] = luong_dung_mot_ngay;
                        dr["LUONG_DUNG_MOT_THANG"] = luong_dung_mot_thang;
                        dr["TON_CAN_CO"] = ton_can_co;
                        dr["CHENH_LECH"] = chenh_lech;
                        dr["SO_LUONG_DAT_HANG"] = so_luong_dat_hang;
                        dr["SO_TIEN_DAT_HANG"] = so_tien_dat_hang;
                        tempTable.Rows.InsertAt(dr, i);

                        break;
                }
            }

            // Combine 2 table (dt + tempTable)
            var NewDt = (
                         from dt2 in tempTable.AsEnumerable()
                         from dt1 in dt.AsEnumerable()
                         where dt2["CODE"] == dt1["CODE"]
                         select new
                         {
                             CODE = dt1["CODE"],
                             NAME = dt1["NAME"],
                             MOQ = dt1["MOQ"],
                             LEAD_TIME = dt1["LEAD_TIME"],
                             DU_PHONG = dt1["DU_PHONG"],
                             TON_CAN_CO = dt2["TON_CAN_CO"],
                             UNIT_PRICE = dt1[4],

                             SO_LUONG_HIEN_TAI = dt1["QUANTITY"],
                             CAN_USE_FOR = "",

                             SO_TIEN_HIEN_TAI_usd = dt1["TOTAL_MONEY_USD"],



                             LUONG_CHUA_NHAP = dt2["LUONG_CHUA_NHAP"],
                             LUONG_DUNG_MOT_NGAY = dt2["LUONG_DUNG_MOT_NGAY"],
                             LUONG_DUNG_MOT_THANG = dt2["LUONG_DUNG_MOT_THANG"],
                             CHENH_LECH = dt2["CHENH_LECH"],
                             SO_LUONG_DAT_HANG = dt2["SO_LUONG_DAT_HANG"],
                             SO_TIEN_DAT_HANG_usd = dt2["SO_TIEN_DAT_HANG"],

                         }).ToList();

            //var NewDt = (
            // from dt1 in tempTable.AsEnumerable()
            // from dt2 in dt.AsEnumerable()
            // where dt2["CODE"] == dt1["CODE"]
            // select new
            // {
            //     CODE = dt2["CODE"],
            //     NAME = dt2["NAME"],
            //     MOQ = dt2["MOQ"],
            //     LEAD_TIME = dt2["LEAD_TIME"],
            //     DU_PHONG = dt2["DU_PHONG"],
            //     TON_CAN_CO = dt1["TON_CAN_CO"],
            //     UNIT_PRICE = dt2[4],

            //     SO_LUONG_HIEN_TAI = dt2["QUANTITY"],
            //     CAN_USE_FOR = "",

            //     SO_TIEN_HIEN_TAI_usd = dt2["TOTAL_MONEY_USD"],



            //     LUONG_CHUA_NHAP = dt1["LUONG_CHUA_NHAP"],
            //     LUONG_DUNG_MOT_NGAY = dt1["LUONG_DUNG_MOT_NGAY"],
            //     LUONG_DUNG_MOT_THANG = dt1["LUONG_DUNG_MOT_THANG"],
            //     CHENH_LECH = dt1["CHENH_LECH"],
            //     SO_LUONG_DAT_HANG = dt1["SO_LUONG_DAT_HANG"],
            //     SO_TIEN_DAT_HANG_usd = dt1["SO_TIEN_DAT_HANG"],

            // }).ToList();


            my_return = Extensions.ToDataTable(NewDt);
            // Tính xem tồn kho có thể được dùng trong bao lâu nưa?
            for (int i = 0; i < my_return.Rows.Count; i++) {
                if ((my_return.Rows[i]["LUONG_DUNG_MOT_THANG"].ToString() != "") && (my_return.Rows[i]["LUONG_DUNG_MOT_THANG"].ToString() != "0"))
                {
                    double temp = 0;
                    temp = Math.Round(Convert.ToDouble(my_return.Rows[i]["SO_LUONG_HIEN_TAI"].ToString()) / Convert.ToDouble(my_return.Rows[i]["LUONG_DUNG_MOT_THANG"].ToString()),1);
                    temp = temp * 4;
                    my_return.Rows[i]["CAN_USE_FOR"] = "~" + temp + " W";
                }
                else
                {
                    my_return.Rows[i]["CAN_USE_FOR"] = "";
                }
            }
            return my_return;

        }

        // ************* Copy from Datatable to Table **************
        public void CopyDataTableToTable(DataTable dt)
        {
            string sql = "";
            this.ExecuteNoneQuery("delete from EWIPDINH_MUC_NGUYEN_LIEU_WLP1");
            for (int i = 0; i < dt.Rows.Count;i++) 
            {
                sql= "Insert into EWIPDINH_MUC_NGUYEN_LIEU_WLP1 values ('" + dt.Rows[i]["CODE"].ToString() + "','" + dt.Rows[i]["NAME"].ToString() + "'," + Convert.ToDouble(dt.Rows[i]["LUONG_DUNG_MOT_THANG"].ToString()) + "," + Convert.ToDouble(dt.Rows[i]["SO_LUONG_HIEN_TAI"].ToString()) + ",'" + dt.Rows[i]["CAN_USE_FOR"].ToString() + "')";
                this.ExecuteNoneQuery(sql);
            }
        }
        //**************************************************************
        public void Update_LICH_SU_TON_KHO(string CODE, string DEPARTMENT,DataTable dt, ref DataTable mydt)
        {
            
            try
            {
                //ton_cuoi_ky = ton_dau_ky + nhap_trong_thang - xuat_trong_thang;
                //string sql = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_cuoi_ky + ",'" + thang + "','" + nam + "')";
                //ton_dau_ky = ton_cuoi_ky;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO where CODE ='" + CODE + "' and DEPARTMENT ='" + DEPARTMENT +"'");
                mydt.Columns.Add("CODE", typeof(string));
                mydt.Columns.Add("DEPARTMENT", typeof(string));
                mydt.Columns.Add("THANG", typeof(string));
                mydt.Columns.Add("NAM", typeof(string));
                mydt.Columns.Add("SO_LUONG_IN_OUT", typeof(int));
                int ton_kho_cuoi_ky;
                int ton_kho_dau_ky = 0;
                int sl_nhap, sl_xuat = 0;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO");
                ExecuteNoneQuery("delete from LICH_SU_TON_KHO_NEW");
                string insert_command;
                if (dt.Rows.Count == 1)
                {
                    ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) - 0;
                    mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[0]["THANG"].ToString(), dt.Rows[0]["NAM"].ToString(), ton_kho_cuoi_ky });
                    insert_command = "Insert into LICH_SU_TON_KHO_NEW values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[0]["THANG"].ToString() + "','" + dt.Rows[0]["NAM"].ToString() + "')";
                    ExecuteNoneQuery(insert_command);
                    ton_kho_dau_ky = ton_kho_cuoi_ky;
                }
                else
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        if ((i == 0) && dt.Rows[i]["THANG"].ToString() != dt.Rows[i + 1]["THANG"].ToString())
                        {
                            ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) - 0;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_NEW values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            continue;
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_NEW values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            break;
                        }

                        if ((dt.Rows[i]["THANG"].ToString() == dt.Rows[i + 1]["THANG"].ToString()) && (dt.Rows[i]["NAM"].ToString() == dt.Rows[i + 1]["NAM"].ToString()))
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i + 1]["SO_LUONG_IN_OUT"].ToString()) - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_NEW values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            i = i + 1;
                            continue;
                        }
                        else
                        {
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + 0 - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_NEW values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                        }

                    }
                }



                //ExecuteNoneQuery(sql);
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        // ******************* Combine column in 2 table Function ******
        //public static DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection props =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}
        //***************************************************** ****

        public void Update_LICH_SU_TON_KHO_ALL(string CODE, string DEPARTMENT, DataTable dt, ref DataTable mydt)
        {

            try
            {
                //ton_cuoi_ky = ton_dau_ky + nhap_trong_thang - xuat_trong_thang;
                //string sql = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_cuoi_ky + ",'" + thang + "','" + nam + "')";
                //ton_dau_ky = ton_cuoi_ky;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO where CODE ='" + CODE + "' and DEPARTMENT ='" + DEPARTMENT +"'");
                mydt.Columns.Add("CODE", typeof(string));
                mydt.Columns.Add("DEPARTMENT", typeof(string));
                mydt.Columns.Add("THANG", typeof(string));
                mydt.Columns.Add("NAM", typeof(string));
                mydt.Columns.Add("SO_LUONG_IN_OUT", typeof(int));
                int ton_kho_cuoi_ky;
                int ton_kho_dau_ky = 0;
                int sl_nhap, sl_xuat = 0;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO");
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO_NEW");
                string insert_command;
                if (dt.Rows.Count == 1)
                {
                    ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) - 0;
                    mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[0]["THANG"].ToString(), dt.Rows[0]["NAM"].ToString(), ton_kho_cuoi_ky });
                    insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[0]["THANG"].ToString() + "','" + dt.Rows[0]["NAM"].ToString() + "')";
                    ExecuteNoneQuery(insert_command);
                    ton_kho_dau_ky = ton_kho_cuoi_ky;
                }
                else
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        if ((i == 0) && dt.Rows[i]["THANG"].ToString() != dt.Rows[i + 1]["THANG"].ToString())
                        {
                            ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) - 0;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            continue;
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            break;
                        }

                        if ((dt.Rows[i]["THANG"].ToString() == dt.Rows[i + 1]["THANG"].ToString()) && (dt.Rows[i]["NAM"].ToString() == dt.Rows[i + 1]["NAM"].ToString()))
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i + 1]["SO_LUONG_IN_OUT"].ToString()) - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            i = i + 1;
                            continue;
                        }
                        else
                        {
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + 0 - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                        }
                    }
                }
                // Thử insert thêm một dòng cuối cùng cho những code mà không có (tháng - năm) là tháng -năm hiện tại. ****************************
                string vsql = "select right(('0'+T1.THANG + '-' + T1.NAM),7) as 'TN',SO_LUONG_TON from LICH_SU_TON_KHO_ALL T1 where T1.CODE ='" + CODE + "' and T1.DEPARTMENT ='" + DEPARTMENT + "' order by TN desc";
                DataTable vdt = ExecuteQuery(vsql);
                string vtoday = string.Format("{0:MM-yyyy}", System.DateTime.Now);
                if (vdt.Rows[0][0].ToString() != vtoday)
                {
                    double vton_kho = Convert.ToDouble(vdt.Rows[0][1].ToString());
                    int vstart = Convert.ToInt32(vdt.Rows[0][0].ToString().Substring(0,2));

                    for (int j = vstart +1; j <= System.DateTime.Now.Month; j++)
                    {
                        vsql = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + vton_kho + ",0,0,'" + j.ToString() + "','" + System.DateTime.Now.Year + "')";
                        ExecuteNoneQuery(vsql);
                    }

                }
                // *********************************************************************************************************************************
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        public void Update_LICH_SU_TON_KHO_ALL_OLD(string CODE, string DEPARTMENT, DataTable dt, ref DataTable mydt)
        {

            try
            {
                //ton_cuoi_ky = ton_dau_ky + nhap_trong_thang - xuat_trong_thang;
                //string sql = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_cuoi_ky + ",'" + thang + "','" + nam + "')";
                //ton_dau_ky = ton_cuoi_ky;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO where CODE ='" + CODE + "' and DEPARTMENT ='" + DEPARTMENT +"'");
                mydt.Columns.Add("CODE", typeof(string));
                mydt.Columns.Add("DEPARTMENT", typeof(string));
                mydt.Columns.Add("THANG", typeof(string));
                mydt.Columns.Add("NAM", typeof(string));
                mydt.Columns.Add("SO_LUONG_IN_OUT", typeof(int));
                int ton_kho_cuoi_ky;
                int ton_kho_dau_ky = 0;
                int sl_nhap, sl_xuat = 0;
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO");
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO_ALL");
                //ExecuteNoneQuery("delete from LICH_SU_TON_KHO_ALL where DEPARTMENT ='" + DEPARTMENT + "' and CODE ='" + CODE + "'");

                string insert_command;

                if (dt.Rows.Count == 1)
                {
                    ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) - 0;
                    mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[0]["THANG"].ToString(), dt.Rows[0]["NAM"].ToString(), ton_kho_cuoi_ky });
                    insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[0]["THANG"].ToString() + "','" + dt.Rows[0]["NAM"].ToString() + "')";
                    ExecuteNoneQuery(insert_command);
                    ton_kho_dau_ky = ton_kho_cuoi_ky;
                }
                else 
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        if ((i == 0) && dt.Rows[i]["THANG"].ToString() != dt.Rows[i + 1]["THANG"].ToString())
                        {
                            ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) - 0;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString()) + ",0,'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            continue;
                        }
                        if (i == dt.Rows.Count - 1)
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            break;
                        }

                        if ((dt.Rows[i]["THANG"].ToString() == dt.Rows[i + 1]["THANG"].ToString()) && (dt.Rows[i]["NAM"].ToString() == dt.Rows[i + 1]["NAM"].ToString()))
                        {
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + Convert.ToInt32(dt.Rows[i + 1]["SO_LUONG_IN_OUT"].ToString()) - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            i = i + 1;
                            continue;
                        }
                        else
                        {
                            //ton_kho_cuoi_ky = ton_kho_dau_ky + 0 - Convert.ToInt32(dt.Rows[i]["SO_LUONG_IN_OUT"].ToString());
                            sl_nhap = Get_sl_nhap(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            sl_xuat = Get_sl_xuat(CODE, DEPARTMENT, dt.Rows[i]["NAM"].ToString(), dt.Rows[i]["THANG"].ToString());
                            ton_kho_cuoi_ky = ton_kho_dau_ky + sl_nhap - sl_xuat;
                            if (ton_kho_cuoi_ky < 0) { ton_kho_cuoi_ky = 0; }
                            mydt.Rows.Add(new object[] { CODE, DEPARTMENT, dt.Rows[i]["THANG"].ToString(), dt.Rows[i]["NAM"].ToString(), ton_kho_cuoi_ky });
                            //insert_command = "Insert into LICH_SU_TON_KHO values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            insert_command = "Insert into LICH_SU_TON_KHO_ALL values('" + CODE + "','" + DEPARTMENT + "','-'," + ton_kho_cuoi_ky + "," + sl_nhap + "," + sl_xuat + ",'" + dt.Rows[i]["THANG"].ToString() + "','" + dt.Rows[i]["NAM"].ToString() + "')";
                            ExecuteNoneQuery(insert_command);
                            ton_kho_dau_ky = ton_kho_cuoi_ky;
                        }

                    }

                }


                //ExecuteNoneQuery(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public int Get_sl_nhap(string CODE, string DEPARTMENT,string nam, string thang) {
            int temp = 0;
            DataTable dt = new DataTable();
            thang = "0" + thang;
            thang = thang.Substring(thang.Length - 2, 2);
            string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,sum(QUANTITY) as 'SO_LUONG_IN' FROM[MRO].[dbo].[EWIPSTOCKIN_NEW]  where DEPARTMENT = '" + DEPARTMENT + "' AND CODE = '" + CODE +"' and CREATE_TIME like '" + nam + thang+ "%' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)) order by THANG";
            dt = ExecuteQuery(sql);
            if (dt.Rows.Count>0)
            {
                temp = Convert.ToInt32(dt.Rows[0]["SO_LUONG_IN"].ToString());
            }
            else
            {
                temp = 0;
            }
            
            return temp;
        }

        public int Get_sl_xuat(string CODE, string DEPARTMENT, string nam, string thang)
        {
            int temp = 0;
            DataTable dt = new DataTable();
            thang = "0" + thang;
            thang = thang.Substring(thang.Length - 2, 2);
            string sql = "SELECT  CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME))as THANG,sum(QUANTITY) as 'SO_LUONG_OUT' FROM[MRO].[dbo].[EWIPSTOCKOUT_NEW]  where DEPARTMENT = '" + DEPARTMENT + "' AND CODE = '" + CODE + "' and CREATE_TIME like '" + nam + thang + "%' group by CODE, MONTH(DBO.CHAR_TO_DATE(CREATE_TIME)) order by THANG";
            dt = ExecuteQuery(sql);
            if (dt.Rows.Count>0)
            {
                temp = Convert.ToInt32(dt.Rows[0]["SO_LUONG_OUT"].ToString());
            }
            else
            {
                temp = 0;
            }
            return temp;
        }


        public DataTable Get_ton_kho_hoa_chat(DataTable vdt) {
            try
            {
                //DataTable vdt = new DataTable();
                DataTable vdt_temp = new DataTable();
                //string sql = "SELECT * FROM (select left(t1.CREATE_TIME,6) AS 'TN',sum(t1.QUANTITY) AS 'QUANTITY_IN' from [EWIPSTOCKHISTORY] t1 where t1.ACTION='STOCK-IN' and t1.CREATE_TIME <='" + to_date + "080000' and t1.CREATE_TIME >='20210101080000' group by left(t1.CREATE_TIME,6)) A \n";
                //sql = sql + "  FULL JOIN \n";
                //sql = sql + "  (select left(t2.CREATE_TIME,6) AS 'TN',sum(t2.QUANTITY) AS 'QUANTITY_OUT' from [EWIPSTOCKHISTORY] t2 where t2.ACTION='STOCK-OUT' and t2.CREATE_TIME <='" + to_date + "080000' AND t2.CREATE_TIME >='20210101080000' group by left(t2.CREATE_TIME,6)) B  \n";
                //sql = sql + "  ON A.TN =B.TN  ORDER BY A.TN,B.TN \n";

                //string sql = "SELECT * FROM (select left(t1.CREATE_TIME,6) AS 'TN',sum(t1.QUANTITY) AS 'QUANTITY_IN' from [EWIPSTOCKHISTORY] t1 where t1.ACTION='STOCK-IN' and t1.CREATE_TIME <='" + to_date + "080000' and t1.CREATE_TIME >='20210101080000' group by left(t1.CREATE_TIME,6)) A  FULL JOIN (select left(t2.CREATE_TIME,6) AS 'TN',sum(t2.QUANTITY) AS 'QUANTITY_OUT' from [EWIPSTOCKHISTORY] t2 where t2.ACTION='STOCK-OUT' and t2.CREATE_TIME <='" + to_date + "080000' AND t2.CREATE_TIME >='20210101080000' group by left(t2.CREATE_TIME,6)) B  ON A.TN =B.TN  ORDER BY A.TN,B.TN";

               // vdt = this.ExecuteQuery(sql);

                if (vdt.Rows.Count > 0) 
                {
                    //DataRow dr = new DataRow();
                    //DataColumn dc = new DataColumn("TN", typeof(string));
                    vdt_temp.Columns.Add("TN", typeof(string));
                    vdt_temp.Columns.Add("QUANTITY", typeof(Int32));
                    int ton_dau_ky = 0;
                    int ton_cuoi_ky = 0;
                    for (int i = 0; i < vdt.Rows.Count; i++) 
                    {
                        string vTN;
                        int vQUANTITY=0;
                        vTN = vdt.Rows[i]["TN"].ToString();
                        vQUANTITY= ton_dau_ky + Convert.ToInt32(vdt.Rows[i]["QUANTITY_IN"].ToString()) - Convert.ToInt32(vdt.Rows[i]["QUANTITY_OUT"].ToString());
                        vdt_temp.Rows.Add(vTN,vQUANTITY);
                        ton_cuoi_ky = ton_dau_ky + vQUANTITY;
                        ton_dau_ky = ton_cuoi_ky;
                    }
                }
                return vdt_temp;
            }
            catch (Exception e) 
            {
                return null;
            }
        }
        // *******************************************************************
        public ResultDB ExcuteProc(string ProcName)
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

        public ResultDB ExcuteProcPR(string ProcName, Dictionary<string, string> Dictionary)
        {
            try
            {
                string connecPR = "Data Source = 10.70.21.72\\SQLEXPRESS;Initial Catalog = SiplaceOIS_LineI;Persist Security Info=True;User id = sa;Password = 123456;Connect Timeout=10";
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
