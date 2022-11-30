using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CSP_OCR
{
    public partial class Form2 : KryptonForm
    {
        public Form2()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            //dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);


            //DataTable dt = new DataTable();
            //dt.Columns.Add("LOT_ID", typeof(string));

            //dt.Rows.Add("CNM056FE100000");
            //dt.Rows.Add("CNM048FE200007");

            //dataGridView1.DataSource = dt;

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            string connString = "Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            //-------
            SqlCommand cmd = new SqlCommand("SP_GET_LOT_BY_MODEL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@A_TIME1", SqlDbType.VarChar).Value = dateTimePicker1.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add("@A_TIME2", SqlDbType.VarChar).Value = dateTimePicker2.Value.ToString("yyyyMMdd");
            cmd.Parameters.Add("@A_MODEL", SqlDbType.VarChar).Value = txtModel.Text.Trim();
            //-------

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());
            conn.Close();

            if(dataTable.Rows.Count < 1)
            {
                if (string.IsNullOrWhiteSpace(txtModel.Text.Trim()))
                {
                    MessageBox.Show("KHÔNG CÓ DỮ LIỆU.\r\n\r\nNO DATA FOUND", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("MODEL " + txtModel.Text.Trim() + "\r\n\r\nKHÔNG CÓ DỮ LIỆU.\r\n\r\nNO DATA FOUND", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            lblLot.Text = string.Empty;
            lblModelLot.Text = string.Empty;
            lblTime.Text = string.Empty;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;

            rtbModelImage1.Text = string.Empty;
            rtbModelImage2.Text = string.Empty;
            rtbModelImage3.Text = string.Empty;

            label5.Visible = false;
            label6.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            txtNote.Visible = false;
            lblResult.Visible = false;

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["PC_IP_ADDRESS"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLotId.Text.Trim()))
            {
                return;
            }

            GetLot(txtLotId.Text.Trim());
        }

        private void GetDetail(string lot_no)  //private void GetDetail(string lot_no, string ip_address) 
        {
            //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            string connString = "Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            //-------
            SqlCommand cmd = new SqlCommand("SP_GET_LOT_DETAIL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
            //cmd.Parameters.Add("@A_IP_ADDRESS", SqlDbType.VarChar).Value = ip_address; MẠNH BỎ DÒNG NÀY
            //-------

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());
            conn.Close();

            if(dataTable.Rows.Count < 1)
            {
                MessageBox.Show(lot_no + "\r\n\r\nKHÔNG CÓ THÔNG TIN LOT.\r\n\r\nNO INFO ABOUT LOT", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblLot.Visible = true;
            lblModelLot.Visible = true;

            lblLot.Text = dataTable.Rows[0][2].ToString() + " - ";
            lblModelLot.Text = dataTable.Rows[0][3].ToString();

            lblTime.Visible = true;
            lblTime.Text = dataTable.Rows[0][1].ToString() + "  |  " + dataTable.Rows[0]["PC_NAME"].ToString() + "  |  " + dataTable.Rows[0]["PROCESS"].ToString();

            lblResult.Text = dataTable.Rows[0]["LOT_STATUS"].ToString();
            lblResult.Visible = true;

            if(lblResult.Text == "OK")
            {
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.ForeColor = Color.Red;
            }


            label5.Visible = true;
            label6.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            txtNote.Visible = true;
            if(dataTable.Rows[0]["COVER_TAPE_STATUS"].ToString() == "OK")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if(dataTable.Rows[0]["COVER_TAPE_STATUS"].ToString() == "NG")
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
            else
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
            }

            txtNote.Text = dataTable.Rows[0]["NOTE"].ToString();

            rtbModelImage1.Text = string.Empty;
            rtbModelImage2.Text = string.Empty;
            rtbModelImage3.Text = string.Empty;

            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;

            byte[] b1 = Convert.FromBase64String(dataTable.Rows[0]["IMAGE_BASE64"].ToString());
            using(MemoryStream ms = new MemoryStream(b1))
            {
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }

            string model1 = dataTable.Rows[0]["MARKING_IMAGE"].ToString();
            string first2string = model1.Substring(0, 3);
            //string reststring = model1.Substring(2);

            if (dataTable.Rows[0]["RESULT"].ToString() == "OK")
            {
                rtbModelImage1.AppendText(first2string);
                rtbModelImage1.Select(0, 3);
                rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);
                //rtbModelImage1.AppendText(reststring);
                //rtbModelImage1.Select(2, reststring.Length);
                //rtbModelImage1.SelectionColor = Color.Black;
            }
            else
            {
                rtbModelImage1.AppendText(model1);
                rtbModelImage1.Select(0, model1.Length);
                rtbModelImage1.SelectionColor = Color.Red;
            }

            rtbModelImage1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage1.BorderStyle = BorderStyle.None;
            rtbModelImage2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage2.BorderStyle = BorderStyle.None;
            rtbModelImage3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage3.BorderStyle = BorderStyle.None;
            rtbModelImage1.Visible = true;
            rtbModelImage2.Visible = true;
            rtbModelImage3.Visible = true;


            if (dataTable.Rows.Count > 1)
            {
                byte[] b2 = Convert.FromBase64String(dataTable.Rows[1]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b2))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox2.Image = image;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b3 = Convert.FromBase64String(dataTable.Rows[2]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b3))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox3.Image = image;
                    pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                string model2 = dataTable.Rows[1]["MARKING_IMAGE"].ToString();
                string model3 = dataTable.Rows[2]["MARKING_IMAGE"].ToString();

                first2string = model2.Substring(0, 3);
                //reststring = model2.Substring(2);
                if (dataTable.Rows[1]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage2.AppendText(first2string);
                    rtbModelImage2.Select(0, 3);
                    rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);
                    //rtbModelImage2.AppendText(reststring);
                    //rtbModelImage2.Select(2, reststring.Length);
                    //rtbModelImage2.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage2.AppendText(model2);
                    rtbModelImage2.Select(0, model2.Length);
                    rtbModelImage2.SelectionColor = Color.Red;
                }


                first2string = model3.Substring(0, 3);
                //reststring = model3.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage3.AppendText(first2string);
                    rtbModelImage3.Select(0, 3);
                    rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);
                    //rtbModelImage3.AppendText(reststring);
                    //rtbModelImage3.Select(2, reststring.Length);
                    //rtbModelImage3.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage3.AppendText(model3);
                    rtbModelImage3.Select(0, model3.Length);
                    rtbModelImage3.SelectionColor = Color.Red;
                }
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                rtbModelImage2.Visible = false;
                rtbModelImage3.Visible = false;
            }

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string lot_no = dataGridView1.Rows[e.RowIndex].Cells["LOT_ID"].Value.ToString();
            //string ip_address = dataGridView1.Rows[e.RowIndex].Cells["PC_IP_ADDRESS"].Value.ToString(); MẠNH BỎ DÒNG CODE NÀY

            if (string.IsNullOrWhiteSpace(lot_no))
            {
                return;
            }

            //GetDetail(lot_no, ip_address); MẠNH BỎ DÒNG NÀY
            GetDetail(lot_no);
        }

        private void txtLotId_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLotId.Text.Trim()))
            {
                return;
            }

            string lot_no = txtLotId.Text.Trim();

            if (e.KeyCode == Keys.Enter)
            {
                txtLotId.Text = string.Empty;
                GetLot(lot_no);
            }

        }

        private void GetLot(string lot_no)
        {
            //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            string connString = "Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            //-------
            SqlCommand cmd = new SqlCommand("SP_GET_LOT", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
            //-------

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());
            conn.Close();

            if (dataTable.Rows.Count < 1)
            {
                if (string.IsNullOrWhiteSpace(txtModel.Text.Trim()))
                {
                    MessageBox.Show("KHÔNG CÓ DỮ LIỆU.\r\n\r\nNO DATA FOUND", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("MODEL " + txtModel.Text.Trim() + "\r\n\r\nKHÔNG CÓ DỮ LIỆU.\r\n\r\nNO DATA FOUND", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            lblLot.Text = string.Empty;
            lblModelLot.Text = string.Empty;
            lblTime.Text = string.Empty;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;

            rtbModelImage1.Text = string.Empty;
            rtbModelImage2.Text = string.Empty;
            rtbModelImage3.Text = string.Empty;

            txtLotId.Text = string.Empty;

            label5.Visible = false;
            label6.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            txtNote.Visible = false;
            lblResult.Visible = false;


            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridView1.Columns["PC_IP_ADDRESS"].Visible = false; MẠNH BỎ DÒNG CODE NÀY
        }
    }
}
