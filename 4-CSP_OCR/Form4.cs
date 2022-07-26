using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSP_OCR
{
    public partial class Form4 : KryptonForm
    {
        public Form4(string lot_no, string ip_address)
        {
            InitializeComponent();

            lblLine1.Text = "_______________________________________________________________________________________________________________________________________________________________________________";

            //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
            string connString = "Data Source = 10.70.21.214;Initial Catalog = MESDB;User Id = mesother;Password = othermes;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            //-------
            SqlCommand cmd = new SqlCommand("SP_GET_LOT_DETAIL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
            cmd.Parameters.Add("@A_IP_ADDRESS", SqlDbType.VarChar).Value = ip_address;
            //-------

            DataTable dataTable = new DataTable();
            dataTable.Load(cmd.ExecuteReader());
            conn.Close();

            if (dataTable.Rows.Count < 1)
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

            if (lblResult.Text == "OK")
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
            if (dataTable.Rows[0]["COVER_TAPE_STATUS"].ToString() == "OK")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if (dataTable.Rows[0]["COVER_TAPE_STATUS"].ToString() == "NG")
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
            using (MemoryStream ms = new MemoryStream(b1))
            {
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }

            string model1 = dataTable.Rows[0]["MARKING_IMAGE"].ToString();
            string first2string = model1.Substring(0, 2);
            string reststring = model1.Substring(2);

            if (dataTable.Rows[0]["RESULT"].ToString() == "OK")
            {
                rtbModelImage1.AppendText(first2string);
                rtbModelImage1.Select(0, 2);
                rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);
                rtbModelImage1.AppendText(reststring);
                rtbModelImage1.Select(2, reststring.Length);
                rtbModelImage1.SelectionColor = Color.Black;
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
            rtbModelImage4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage4.BorderStyle = BorderStyle.None;
            rtbModelImage5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage5.BorderStyle = BorderStyle.None;
            rtbModelImage6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage6.BorderStyle = BorderStyle.None;
            rtbModelImage7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage7.BorderStyle = BorderStyle.None;
            rtbModelImage8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage8.BorderStyle = BorderStyle.None;
            rtbModelImage9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage9.BorderStyle = BorderStyle.None;
            rtbModelImage10.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage10.BorderStyle = BorderStyle.None;
            rtbModelImage11.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage11.BorderStyle = BorderStyle.None;
            rtbModelImage12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            rtbModelImage12.BorderStyle = BorderStyle.None;
            rtbModelImage1.Visible = true;
            rtbModelImage2.Visible = true;
            rtbModelImage3.Visible = true;
            rtbModelImage4.Visible = true;
            rtbModelImage5.Visible = true;
            rtbModelImage6.Visible = true;
            rtbModelImage7.Visible = true;
            rtbModelImage8.Visible = true;
            rtbModelImage9.Visible = true;
            rtbModelImage10.Visible = true;
            rtbModelImage11.Visible = true;
            rtbModelImage12.Visible = true;


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

                byte[] b4 = Convert.FromBase64String(dataTable.Rows[3]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b4))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox4.Image = image;
                    pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b5 = Convert.FromBase64String(dataTable.Rows[4]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b5))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox5.Image = image;
                    pictureBox5.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b6 = Convert.FromBase64String(dataTable.Rows[5]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b6))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox6.Image = image;
                    pictureBox6.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b7 = Convert.FromBase64String(dataTable.Rows[6]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b7))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox7.Image = image;
                    pictureBox7.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b8 = Convert.FromBase64String(dataTable.Rows[7]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b8))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox8.Image = image;
                    pictureBox8.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b9 = Convert.FromBase64String(dataTable.Rows[8]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b9))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox9.Image = image;
                    pictureBox9.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b10 = Convert.FromBase64String(dataTable.Rows[9]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b10))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox10.Image = image;
                    pictureBox10.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b11 = Convert.FromBase64String(dataTable.Rows[10]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b11))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox11.Image = image;
                    pictureBox11.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                byte[] b12 = Convert.FromBase64String(dataTable.Rows[11]["IMAGE_BASE64"].ToString());
                using (MemoryStream ms = new MemoryStream(b12))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox12.Image = image;
                    pictureBox12.SizeMode = PictureBoxSizeMode.AutoSize;
                }

                string model2 = dataTable.Rows[1]["MARKING_IMAGE"].ToString();
                string model3 = dataTable.Rows[2]["MARKING_IMAGE"].ToString();
                string model4 = dataTable.Rows[3]["MARKING_IMAGE"].ToString();
                string model5 = dataTable.Rows[4]["MARKING_IMAGE"].ToString();
                string model6 = dataTable.Rows[5]["MARKING_IMAGE"].ToString();
                string model7 = dataTable.Rows[6]["MARKING_IMAGE"].ToString();
                string model8 = dataTable.Rows[7]["MARKING_IMAGE"].ToString();
                string model9 = dataTable.Rows[8]["MARKING_IMAGE"].ToString();
                string model10 = dataTable.Rows[9]["MARKING_IMAGE"].ToString();
                string model11 = dataTable.Rows[10]["MARKING_IMAGE"].ToString();
                string model12 = dataTable.Rows[11]["MARKING_IMAGE"].ToString();

                first2string = model2.Substring(0, 2);
                reststring = model2.Substring(2);
                if (dataTable.Rows[1]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage2.AppendText(first2string);
                    rtbModelImage2.Select(0, 2);
                    rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage2.AppendText(reststring);
                    rtbModelImage2.Select(2, reststring.Length);
                    rtbModelImage2.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage2.AppendText(model2);
                    rtbModelImage2.Select(0, model2.Length);
                    rtbModelImage2.SelectionColor = Color.Red;
                }


                first2string = model3.Substring(0, 2);
                reststring = model3.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage3.AppendText(first2string);
                    rtbModelImage3.Select(0, 2);
                    rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage3.AppendText(reststring);
                    rtbModelImage3.Select(2, reststring.Length);
                    rtbModelImage3.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage3.AppendText(model3);
                    rtbModelImage3.Select(0, model3.Length);
                    rtbModelImage3.SelectionColor = Color.Red;
                }


                first2string = model4.Substring(0, 2);
                reststring = model4.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage4.AppendText(first2string);
                    rtbModelImage4.Select(0, 2);
                    rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage4.AppendText(reststring);
                    rtbModelImage4.Select(2, reststring.Length);
                    rtbModelImage4.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage4.AppendText(model4);
                    rtbModelImage4.Select(0, model4.Length);
                    rtbModelImage4.SelectionColor = Color.Red;
                }

                first2string = model5.Substring(0, 2);
                reststring = model5.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage5.AppendText(first2string);
                    rtbModelImage5.Select(0, 2);
                    rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage5.AppendText(reststring);
                    rtbModelImage5.Select(2, reststring.Length);
                    rtbModelImage5.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage5.AppendText(model5);
                    rtbModelImage5.Select(0, model5.Length);
                    rtbModelImage5.SelectionColor = Color.Red;
                }

                first2string = model6.Substring(0, 2);
                reststring = model6.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage6.AppendText(first2string);
                    rtbModelImage6.Select(0, 2);
                    rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage6.AppendText(reststring);
                    rtbModelImage6.Select(2, reststring.Length);
                    rtbModelImage6.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage6.AppendText(model6);
                    rtbModelImage6.Select(0, model6.Length);
                    rtbModelImage6.SelectionColor = Color.Red;
                }

                first2string = model7.Substring(0, 2);
                reststring = model7.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage7.AppendText(first2string);
                    rtbModelImage7.Select(0, 2);
                    rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage7.AppendText(reststring);
                    rtbModelImage7.Select(2, reststring.Length);
                    rtbModelImage7.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage7.AppendText(model7);
                    rtbModelImage7.Select(0, model7.Length);
                    rtbModelImage7.SelectionColor = Color.Red;
                }

                first2string = model8.Substring(0, 2);
                reststring = model8.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage8.AppendText(first2string);
                    rtbModelImage8.Select(0, 2);
                    rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage8.AppendText(reststring);
                    rtbModelImage8.Select(2, reststring.Length);
                    rtbModelImage8.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage8.AppendText(model8);
                    rtbModelImage8.Select(0, model8.Length);
                    rtbModelImage8.SelectionColor = Color.Red;
                }

                first2string = model9.Substring(0, 2);
                reststring = model9.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage9.AppendText(first2string);
                    rtbModelImage9.Select(0, 2);
                    rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage9.AppendText(reststring);
                    rtbModelImage9.Select(2, reststring.Length);
                    rtbModelImage9.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage9.AppendText(model9);
                    rtbModelImage9.Select(0, model9.Length);
                    rtbModelImage9.SelectionColor = Color.Red;
                }

                first2string = model10.Substring(0, 2);
                reststring = model10.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage10.AppendText(first2string);
                    rtbModelImage10.Select(0, 2);
                    rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage10.AppendText(reststring);
                    rtbModelImage10.Select(2, reststring.Length);
                    rtbModelImage10.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage10.AppendText(model10);
                    rtbModelImage10.Select(0, model10.Length);
                    rtbModelImage10.SelectionColor = Color.Red;
                }

                first2string = model11.Substring(0, 2);
                reststring = model11.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage11.AppendText(first2string);
                    rtbModelImage11.Select(0, 2);
                    rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage11.AppendText(reststring);
                    rtbModelImage11.Select(2, reststring.Length);
                    rtbModelImage11.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage11.AppendText(model11);
                    rtbModelImage11.Select(0, model11.Length);
                    rtbModelImage11.SelectionColor = Color.Red;
                }

                first2string = model12.Substring(0, 2);
                reststring = model12.Substring(2);
                if (dataTable.Rows[2]["RESULT"].ToString() == "OK")
                {
                    rtbModelImage12.AppendText(first2string);
                    rtbModelImage12.Select(0, 2);
                    rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage12.AppendText(reststring);
                    rtbModelImage12.Select(2, reststring.Length);
                    rtbModelImage12.SelectionColor = Color.Black;
                }
                else
                {
                    rtbModelImage12.AppendText(model12);
                    rtbModelImage12.Select(0, model12.Length);
                    rtbModelImage12.SelectionColor = Color.Red;
                }
        }
    }
}
