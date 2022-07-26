using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Tesseract;
using hqx;

namespace CSP_OCR
{
    public partial class Form1 : KryptonForm
    {
        int intCheck = 0;
        int countSave = 0;
        string Model = string.Empty;
        string lastModelLot = string.Empty;
        string status1 = "NOTCHECK";
        string status2 = "NOTCHECK";
        string status3 = "NOTCHECK";
        string pcIP = "";
        string pcName = "";
        public Form1()
        {
            InitializeComponent();
            this.Text = this.Text + " v" + Application.ProductVersion;

            pcIP = GetIp();
            pcName = GetPCInfo();

            lblInfo.Visible = false;
            lblLot.Visible = false;
            lblModel.Visible = false;
            rtbModelLot.Visible = false;
            rtbModelLot.Text = string.Empty;

            scrBrightness1.Value = 100;
            scrBrightness2.Value = 100;
            scrBrightness3.Value = 100;


            rtbModelImage1.Visible = false;
            rtbModelImage2.Visible = false;
            rtbModelImage3.Visible = false;
            lblLine1.Text = "__________________________________________________________________________________";
            lblLine2.Text = lblLine1.Text;
            lblResult.Visible = false;

            //btnCheck.Visible = false;
            //btnCheck.Visible = false;
            //btnCheck.BackColor = Color.Lavender;
            //btnCheck.ForeColor = Color.Lavender;
            //btnCheck.FlatStyle = FlatStyle.Flat;
            //btnCheck.FlatAppearance.BorderSize = 0;
            rtbModelImage1.BorderStyle = BorderStyle.None;
            rtbModelImage2.BorderStyle = BorderStyle.None;
            rtbModelImage3.BorderStyle = BorderStyle.None;
            rtbModelImage1.BackColor = Color.Lavender;
            rtbModelImage2.BackColor = Color.Lavender;
            rtbModelImage3.BackColor = Color.Lavender;

            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox5.SizeMode = PictureBoxSizeMode.AutoSize;

            //btnReCheck.Visible = false;
            //lblResult.Font = new Font(FontFamily.GenericSansSerif, 48.0F, FontStyle.Bold);

            cbbCD.Items.Add("MARKING");
            cbbCD.Items.Add("FA");
            cbbCD.Items.Add("VI");
            cbbCD.Items.Add("OQC");
            cbbCD.Items.Add("PACKING");
            cbbCD.SelectedIndex = -1;
            cbbCD.DropDownStyle = ComboBoxStyle.DropDownList;

            AdjustImage1();
            AdjustImage2();
            AdjustImage3();
        }

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblLot.Text = "";
                lblModel.Text = "";
                rtbModelLot.Text = "";
                lastModelLot = "";
                Model = "";
                string lot = txtLot.Text.Trim();
                if (string.IsNullOrWhiteSpace(lot))
                {
                    return;
                }
                string connString = "Data Source = 10.70.21.214;Initial Catalog = MESDB;User Id = mesother;Password = othermes;Connect Timeout=3";
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();

                //-------
                SqlCommand cmd = new SqlCommand("SP_GET_LOT_INFO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LOT_ID", SqlDbType.VarChar).Value = lot;
                //-------

                DataTable dataTable = new DataTable();
                dataTable.Load(cmd.ExecuteReader());
                //conn.Close();

                if (dataTable.Rows.Count < 1)
                {
                    MessageBox.Show(lot.ToUpper() + "\r\n\r\nLOT KHÔNG TỒN TẠI.\r\n\r\nLOT IS NOT EXISTS.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (dataTable.Rows[0][3].ToString() != "OK")
                {
                    MessageBox.Show(dataTable.Rows[0][2].ToString(), "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (dataTable.Rows[0][4].ToString() != Application.ProductVersion)
                {
                    MessageBox.Show("CÓ PHIÊN BẢN MỚI.\r\n\r\nĐÓNG CHƯƠNG TRÌNH RỒI MỞ LẠI ĐỂ CẬP NHẬT.\r\n\r\n" +
                        "A NEW VERSION IS AVAILABLE.\r\n\r\nPLEASE CLOSE AND RE-OPEN PROGRAM TO UPDATE.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    txtLot.Text = string.Empty;
                    rtbModelLot.Visible = true;
                    lblModel.Visible = true;
                    lblInfo.Visible = true;
                    lblLot.Visible = true;
                    Model = dataTable.Rows[0][1].ToString();
                    lblLot.Text = dataTable.Rows[0][0].ToString();
                    lblModel.Text = dataTable.Rows[0][1].ToString();
                    string ml = dataTable.Rows[0][2].ToString();

                    int numberOfCheck = Convert.ToInt32(dataTable.Rows[0]["TEMP3"].ToString());

                    int countSpace = dataTable.Rows[0][2].ToString().TrimStart().Substring(numberOfCheck).Count(Char.IsWhiteSpace);

                    // ***** Mạnh thay đổi theo yêu cầu của  Thúy Hà - CSP thuyha349@wisol.co.kr - Ngày 10/03/2022 *****************************
                    // Kiểm tra xem có phải model đang dùng là Model đặc biệt không ? (Model check 2 ký tự cuối).
                    DataTable special_Table = new DataTable();
                    SqlDataAdapter sdap = new SqlDataAdapter("Select MODEL from TB_SPECIAL_MODEL Where MODEL='" + lblModel.Text + "'", conn);
                    sdap.Fill(special_Table); // Lưu Model đặc biệt vào bẳng Special_Table.
                                              // ********************************************************************************************
                    if (special_Table.Rows.Count > 0)
                    {
                        rtbModelLot.Text = ml.Substring(ml.Length - 2 - countSpace);
                        lastModelLot = ml.Substring(ml.Length - 2 - countSpace);
                    }
                    else // Các Model còn lại check như bình thường. ************
                    {
                        rtbModelLot.Text = ml.Substring(0, numberOfCheck).Trim();
                        lastModelLot = ml.Substring(ml.Length - 2 - countSpace);
                    }
                    // ********************************************************************************
                    rtbModelLot.BackColor = Color.Lavender;
                    rtbModelLot.BorderStyle = BorderStyle.None;
                    rtbModelLot.ForeColor = Color.FromArgb(0, 120, 215);
                    rtbModelLot.Multiline = false;

                    rtbModelImage1.Text = string.Empty;
                    rtbModelImage2.Text = string.Empty;
                    rtbModelImage3.Text = string.Empty;
                    rtbModelImage1.ReadOnly = true;
                    rtbModelImage2.ReadOnly = true;
                    rtbModelImage3.ReadOnly = true;

                    pictureBox1.Image = null;
                    pictureBox2.Image = null;
                    pictureBox3.Image = null;
                    status1 = "NOTCHECK";
                    status2 = "NOTCHECK";
                    status3 = "NOTCHECK";

                    lblResult.Text = string.Empty;
                }

                conn.Close();
            }
        }

        private void btnTakePicture1_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(cbbCD.Text))
                {
                    MessageBox.Show("HÃY CHỌN CÔNG ĐOẠN TRƯỚC." +
                        "\r\n\r\nPLEASE SELECT PROCESS", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 1;
                SnippingTool.AreaSelected += OnAreaSelected1;
                SnippingTool.Snip();
                rtbModelImage1.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTakePicture2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbbCD.Text))
                {
                    MessageBox.Show("HÃY CHỌN CÔNG ĐOẠN TRƯỚC." +
                        "\r\n\r\nPLEASE SELECT PROCESS", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 2;
                SnippingTool.AreaSelected += OnAreaSelected2;
                SnippingTool.Snip();
                rtbModelImage2.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTakePicture3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbbCD.Text))
                {
                    MessageBox.Show("HÃY CHỌN CÔNG ĐOẠN TRƯỚC." +
                        "\r\n\r\nPLEASE SELECT PROCESS", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 3;
                SnippingTool.AreaSelected += OnAreaSelected3;
                SnippingTool.Snip();
                rtbModelImage3.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected1(object sender, EventArgs e)
        {
            if (intCheck != 1)
            {
                return;
            }
            var bmp = SnippingTool.Image;
            Image img = SnippingTool.Image;

            var originalbmp = new Bitmap(img); // Load the  image
            var newbmp = new Bitmap(img); // New image

            //for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            //{
            //    for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
            //    {
            //        var colorValue = originalbmp.GetPixel(row, column); // Get the color pixel
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
            //        newbmp.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue)); // Set the value to new pixel
            //    }
            //}

            for (int y = 0; (y <= (newbmp.Height - 1)); y++)
            {
                for (int x = 0; (x <= (newbmp.Width - 1)); x++)
                {
                    Color inv = newbmp.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    newbmp.SetPixel(x, y, inv);
                }
            }


            newbmp.RotateFlip(RotateFlipType.Rotate270FlipNone);


            pictureBox1.Image = newbmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture1.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture1.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture1.Visible = false;

            int wid = oPicture1.Width;
            int hei = oPicture1.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture1.Height / 2 + 10;
            }
            if (rtbModelLot.Text.Trim() == "yF")
            {
                hei = oPicture1.Height / 2 + 3;
            }
            Bitmap nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, hei), new Rectangle(0, 0, wid, hei), GraphicsUnit.Pixel);
            }

            splitPicture11.Image = nb;
            splitPicture11.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture1.Width;
            hei = oPicture1.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture1.Height / 2), new Rectangle(0, oPicture1.Height / 2, wid, oPicture1.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture12.Image = nb;
            splitPicture12.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness1.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }



        private void OnAreaSelected2(object sender, EventArgs e)
        {
            if (intCheck != 2)
            {
                return;
            }

            var bmp = SnippingTool.Image;
            Image img = SnippingTool.Image;

            var originalbmp = new Bitmap(img); // Load the  image
            var newbmp = new Bitmap(img); // New image


            //for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            //{
            //    for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
            //    {
            //        var colorValue = originalbmp.GetPixel(row, column); // Get the color pixel
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
            //        newbmp.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue)); // Set the value to new pixel
            //    }
            //}

            for (int y = 0; (y <= (newbmp.Height - 1)); y++)
            {
                for (int x = 0; (x <= (newbmp.Width - 1)); x++)
                {
                    Color inv = newbmp.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    newbmp.SetPixel(x, y, inv);
                }
            }

            newbmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

            pictureBox2.Image = newbmp;
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture2.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture2.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture2.Visible = false;

            int wid = oPicture2.Width;
            int hei = oPicture2.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture1.Height / 2 + 10;
            }
            if (rtbModelLot.Text.Trim() == "yF")
            {
                hei = oPicture1.Height / 2 + 3;
            }
            Bitmap nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, hei), new Rectangle(0, 0, wid, hei), GraphicsUnit.Pixel);
            }

            splitPicture21.Image = nb;
            splitPicture21.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture2.Width;
            hei = oPicture2.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture2.Height / 2), new Rectangle(0, oPicture2.Height / 2, wid, oPicture2.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture22.Image = nb;
            splitPicture22.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness2.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void OnAreaSelected3(object sender, EventArgs e)
        {
            if (intCheck != 3)
            {
                return;
            }

            var bmp = SnippingTool.Image;
            Image img = SnippingTool.Image;

            var originalbmp = new Bitmap(img); // Load the  image
            var newbmp = new Bitmap(img); // New image

            //for (int row = 0; row < originalbmp.Width; row++) // Indicates row number
            //{
            //    for (int column = 0; column < originalbmp.Height; column++) // Indicate column number
            //    {
            //        var colorValue = originalbmp.GetPixel(row, column); // Get the color pixel
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 3; // get the average for black and white
            //        newbmp.SetPixel(row, column, Color.FromArgb(averageValue, averageValue, averageValue)); // Set the value to new pixel
            //    }
            //}

            for (int y = 0; (y <= (newbmp.Height - 1)); y++)
            {
                for (int x = 0; (x <= (newbmp.Width - 1)); x++)
                {
                    Color inv = newbmp.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    newbmp.SetPixel(x, y, inv);
                }
            }

            newbmp.RotateFlip(RotateFlipType.Rotate270FlipNone);

            pictureBox3.Image = newbmp;
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture3.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture3.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture3.Visible = false;

            int wid = oPicture3.Width;
            int hei = oPicture3.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture1.Height / 2 + 10;
            }
            if (rtbModelLot.Text.Trim() == "yF")
            {
                hei = oPicture1.Height / 2 + 3;
            }
            Bitmap nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, hei), new Rectangle(0, 0, wid, hei), GraphicsUnit.Pixel);
            }

            splitPicture31.Image = nb;
            splitPicture31.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture3.Width;
            hei = oPicture3.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture3.Height / 2), new Rectangle(0, oPicture3.Height / 2, wid, oPicture3.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture32.Image = nb;
            splitPicture32.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness3.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void AdjustImage1()
        {
            lblBrightness1.Text = "Brightness = " + (scrBrightness1.Value / 100.0).ToString();
            if (pictureBox1 == null || pictureBox1.Image == null)
            {
                return;
            }
            pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(scrBrightness1.Value / 100.0));
        }

        private void AdjustImage2()
        {
            lblBrightness2.Text = "Brightness = " + (scrBrightness2.Value / 100.0).ToString();
            if (pictureBox2 == null || pictureBox2.Image == null)
            {
                return;
            }
            pictureBox2.Image = AdjustBrightness(oPicture2.Image, (float)(scrBrightness2.Value / 100.0));
        }

        private void AdjustImage3()
        {
            lblBrightness3.Text = "Brightness = " + (scrBrightness3.Value / 100.0).ToString();
            if (pictureBox3 == null || pictureBox3.Image == null)
            {
                return;
            }
            pictureBox3.Image = AdjustBrightness(oPicture3.Image, (float)(scrBrightness3.Value / 100.0));
        }

        private void scrBrightness1_Scroll(object sender, ScrollEventArgs e)
        {
            //lblBrightness.Text = "Brightness = " + (scrBrightness.Value / 100.0).ToString();
            //pictureBox1.Image = AdjustBrightness(pictureBox2.Image, (float)(scrBrightness.Value / 100.0));

            //AdjustImage();

            GetResult1();
        }

        private void GetResult1()
        {
            lblBrightness1.Text = "Brightness = " + (scrBrightness1.Value / 100.0).ToString();
            if (pictureBox1 == null || pictureBox1.Image == null)
            {
                return;
            }
            pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(scrBrightness1.Value / 100.0));
            rtbModelImage1.Text = "";
            string modelLot = rtbModelLot.Text;
            bool check = false;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {

                string res = "";

                using (var page = engine.Process((Bitmap)pictureBox1.Image, PageSegMode.AutoOnly))
                    res = page.GetText();


                if (res.Contains("|"))
                {
                    res = res.Replace("|", "");
                }
                if (res.Contains("i"))
                {
                    res = res.Replace("i", "");
                }
                if (res.Contains("-"))
                {
                    res = res.Replace("-", "");
                }
                if (res.Contains("l"))
                {
                    res = res.Replace("l", "L");
                }


                if (!string.IsNullOrWhiteSpace(res.Trim()))
                {
                    if (res.TrimStart().Length >= modelLot.Length)
                    {
                        string first2string = res.TrimStart().Substring(0, modelLot.Length);

                        string reststring = " ";

                        if (res.TrimStart().Length > modelLot.Length)
                        {
                            reststring = res.TrimStart().Substring(modelLot.Length);
                        }
                        rtbModelImage1.AppendText(first2string);
                        rtbModelImage1.Select(0, 2);
                        rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

                        rtbModelImage1.AppendText(reststring);
                        rtbModelImage1.Select(modelLot.Length, reststring.Length);
                        rtbModelImage1.SelectionColor = Color.Black;


                        rtbModelImage1.BackColor = Color.Lavender;

                        if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                        {
                            rtbModelImage1.Visible = true;

                            if (!radioButton2.Checked)
                            {
                                lblResult.Visible = true;
                                lblResult.Text = "OK";
                                lblResult.ForeColor = Color.Green;
                                lblResult.Location = new Point(333, 30);
                                
                            }

                            check = true;
                        }
                    }

                }


                if (check)
                {
                    status1 = "OK";
                    if (status2 == "NG" || status3 == "NG")
                    {

                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    return;
                }
                else
                {
                    status1 = "NG";
                    rtbModelImage1.Visible = true;
                    rtbModelImage1.Text = res.Trim();
                    rtbModelImage1.Select(0, res.TrimStart().Length);
                    rtbModelImage1.SelectionColor = Color.Red;

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }

            }
        }

        private Bitmap AdjustBrightness(Image image, float brightness)
        {
            //lblBrightness.Text = "Brightness = " + (scrBrightness.Value / 100.0).ToString();
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {b, 0, 0, 0, 0},
                    new float[] {0, b, 0, 0, 0},
                    new float[] {0, 0, b, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying the new ColorMatrix.
            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }

        private void btnCheck1_Click(object sender, EventArgs e)
        {
            
            bool check = false;
            if (pictureBox1 == null || pictureBox1.Image == null)
            {
                return;
            }

            if (!splashScreenManager1.IsSplashFormVisible)
            {
                splashScreenManager1.ShowWaitForm();
            }

            string modelLot = rtbModelLot.Text.Trim();


            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                //for (int i = 20; i <= 400; i++)
                for (int i = 20; i <= 800; i++)
                {
                    rtbModelImage1.Text = "";
                    string res1 = "";
                    string res2 = "";
                    //string res3 = "";


                    pictureBox4.Image = AdjustBrightness(splitPicture11.Image, (float)(i * 1.0 / 100.0));
                    //pictureBox4.Image = AdjustBrightness(splitPicture11.Image, (float)(i * 1.0 / 1000.0));

                    //using (var page = engine.Process((Bitmap)pictureBox4.Image, PageSegMode.AutoOnly))
                    //    res1 = page.GetText();

                    using (var page = engine.Process((Bitmap)pictureBox4.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox4.Image = AdjustContrast((Bitmap)splitPicture11.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox4.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();

                    //pictureBox5.Image = AdjustBrightness(splitPicture12.Image, (float)(i * 1.0 / 100.0));

                    //using (var page = engine.Process((Bitmap)pictureBox5.Image, PageSegMode.AutoOnly))
                    //    res3 = page.GetText();


                    if (res1.Contains("|"))
                    {
                        res1 = res1.Replace("|", "");
                    }
                    if (res1.Contains("i"))
                    {
                        res1 = res1.Replace("i", "");
                    }
                    if (res1.Contains("-"))
                    {
                        res1 = res1.Replace("-", "");
                    }
                    if (res1.Contains("l"))
                    {
                        res1 = res1.Replace("l", "L");
                    }

                    if(res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK"  + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "F OK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EJK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FUK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "FR" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "EK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }

                    if (res2.Contains("|"))
                    {
                        res2 = res2.Replace("|", "");
                    }
                    if (res2.Contains("i"))
                    {
                        res2 = res2.Replace("i", "");
                    }
                    if (res2.Contains("-"))
                    {
                        res2 = res2.Replace("-", "");
                    }
                    if (res2.Contains("l"))
                    {
                        res2 = res2.Replace("l", "L");
                    }
                    // 
                    if (!string.IsNullOrEmpty(res1))
                    {
                        if (res1.TrimStart().Length >= modelLot.Length)
                        {
                            if (modelLot == res1.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (modelLot == res1.TrimStart().Substring(0, modelLot.Length).Trim())
                                {
                                    check = true;
                                    status1 = "OK";
                                    rtbModelImage1.Visible = true;

                                    string first2string = res1.TrimStart().Substring(0, modelLot.Length);
                                    string secondstring = " ";
                                    string reststring = "\r\n" + lastModelLot;

                                    if (res1.TrimStart().Length > modelLot.Length)
                                    {
                                        if (res1.TrimStart().Length >= modelLot.Length + 2)
                                        {
                                            secondstring = res1.TrimStart().Substring(modelLot.Length, 2);
                                        }
                                        else
                                        {
                                            secondstring = res1.TrimStart().Substring(modelLot.Length);
                                        }
                                    }

                                    rtbModelImage1.AppendText(first2string);
                                    rtbModelImage1.Select(0, modelLot.Length);
                                    rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

                                    rtbModelImage1.AppendText(secondstring.Trim());
                                    rtbModelImage1.Select(modelLot.Length, secondstring.Trim().Length);
                                    rtbModelImage1.SelectionColor = Color.Black;

                                    rtbModelImage1.AppendText(reststring);
                                    //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                    rtbModelImage1.SelectionColor = Color.Black;

                                    if (!radioButton2.Checked)
                                    {
                                        lblResult.Visible = true;
                                        lblResult.Text = "OK";
                                        lblResult.ForeColor = Color.Green;
                                        lblResult.Location = new Point(333, 30);
                                        //
                                    }
                                    splashScreenManager1.CloseWaitForm();
                                    //MessageBox.Show(res1.ToString());
                                    break;
                                }
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(res2.TrimStart()))
                    {
                        if (res2.TrimStart().Length >= modelLot.Length)
                        {

                                if (modelLot == res2.TrimStart().Substring(0, modelLot.Length).Trim())
                                {
                                    check = true;
                                    status1 = "OK";
                                    rtbModelImage1.Visible = true;

                                    string first2string = res2.TrimStart().Substring(0, modelLot.Length);
                                    string secondstring = " ";
                                    string reststring = "\r\n" + lastModelLot;

                                    if (res2.TrimStart().Length > modelLot.Length)
                                    {
                                        if (res2.TrimStart().Length >= modelLot.Length + 2)
                                        {
                                            secondstring = res2.TrimStart().Substring(modelLot.Length, 2);
                                        }
                                        else
                                        {
                                            secondstring = res2.TrimStart().Substring(modelLot.Length);
                                        }
                                    }

                                    rtbModelImage1.AppendText(first2string);
                                    rtbModelImage1.Select(0, modelLot.Length);
                                    rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

                                    rtbModelImage1.AppendText(secondstring.Trim());
                                    rtbModelImage1.Select(modelLot.Length, secondstring.Trim().Length);
                                    rtbModelImage1.SelectionColor = Color.Black;

                                    rtbModelImage1.AppendText(reststring);
                                    rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                    rtbModelImage1.SelectionColor = Color.Black;

                                    if (!radioButton2.Checked)
                                    {
                                        lblResult.Visible = true;
                                        lblResult.Text = "OK";
                                        lblResult.ForeColor = Color.Green;
                                        lblResult.Location = new Point(333, 30);

                                    }

                                    splashScreenManager1.CloseWaitForm();
                                    //MessageBox.Show(res2.ToString());
                                    break;
                                }
                        }
                    }

                }
            }

            scrBrightness1.Value = 100;
            lblBrightness1.Text = "Brightness = 1.0";

            if (check)
            {
                status1 = "OK";
                if (status2 == "NG" || status3 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }
                return;
            }
            else
            {
                status1 = "NG";
                lblBrightness1.Text = "Brightness = 1";
                scrBrightness1.Value = 100;
                rtbModelImage1.Visible = true;
                rtbModelImage1.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage1.BackColor = Color.Lavender;
                rtbModelImage1.ForeColor = Color.Red;
                pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(scrBrightness1.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);

                btnReCheck1.Visible = true;
                btnReCheck1.PerformClick();
                btnReCheck1.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnCheck2_Click(object sender, EventArgs e)
        {
            
            bool check = false;
            if (pictureBox2 == null || pictureBox2.Image == null)
            {
                return;
            }

            if (!splashScreenManager1.IsSplashFormVisible)
            {
                splashScreenManager1.ShowWaitForm();
            }


            string modelLot = rtbModelLot.Text.Trim();


            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                for (int i = 20; i <= 400; i++)
                {
                    rtbModelImage2.Text = "";
                    string res1 = "";
                    string res2 = "";
                    //string res3 = "";


                    pictureBox6.Image = AdjustBrightness(splitPicture21.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox6.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox6.Image = AdjustContrast((Bitmap)splitPicture21.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox6.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();

                    //pictureBox7.Image = AdjustBrightness(splitPicture22.Image, (float)(i * 1.0 / 100.0));

                    //using (var page = engine.Process((Bitmap)pictureBox7.Image, PageSegMode.AutoOnly))
                    //    res3 = page.GetText();


                    if (res1.Contains("|"))
                    {
                        res1 = res1.Replace("|", "");
                    }
                    if (res1.Contains("i"))
                    {
                        res1 = res1.Replace("i", "");
                    }
                    if (res1.Contains("-"))
                    {
                        res1 = res1.Replace("-", "");
                    }
                    if (res1.Contains("l"))
                    {
                        res1 = res1.Replace("l", "L");
                    }
                     
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "F OK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EJK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FUK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "FR" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "EK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }

                    if (res2.Contains("|"))
                    {
                        res2 = res2.Replace("|", "");
                    }
                    if (res2.Contains("i"))
                    {
                        res2 = res2.Replace("i", "");
                    }
                    if (res2.Contains("-"))
                    {
                        res2 = res2.Replace("-", "");
                    }
                    if (res2.Contains("l"))
                    {
                        res2 = res2.Replace("l", "L");
                    }

                    if (!string.IsNullOrEmpty(res1))
                    {
                        if (res1.TrimStart().Length >= modelLot.Length)
                        {
                            if (modelLot == res1.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                check = true;
                                status2 = "OK";
                                rtbModelImage2.Visible = true;

                                string first2string = res1.TrimStart().Substring(0, modelLot.Length);
                                string secondstring = " ";
                                string reststring = "\r\n" + lastModelLot;

                                if (res1.TrimStart().Length > modelLot.Length)
                                {
                                    if (res1.TrimStart().Length >= modelLot.Length + 2)
                                    {
                                        secondstring = res1.TrimStart().Substring(modelLot.Length, 2);
                                    }
                                    else
                                    {
                                        secondstring = res1.TrimStart().Substring(modelLot.Length);
                                    }
                                }

                                rtbModelImage2.AppendText(first2string);
                                rtbModelImage2.Select(0, modelLot.Length);
                                rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage2.AppendText(secondstring.Trim());
                                rtbModelImage2.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage2.SelectionColor = Color.Black;

                                rtbModelImage2.AppendText(reststring);
                                rtbModelImage2.Select(modelLot.Length, reststring.Length);
                                rtbModelImage2.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    //
                                }

                                splashScreenManager1.CloseWaitForm();
                                break;
                            }

                        }
                    }
                    else if (!string.IsNullOrEmpty(res2.TrimStart()))
                    {
                        if (res2.TrimStart().Length >= modelLot.Length)
                        {
                            if (modelLot == res2.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                check = true;
                                status2 = "OK";
                                rtbModelImage2.Visible = true;

                                string first2string = res2.TrimStart().Substring(0, modelLot.Length);
                                string secondstring = " ";
                                string reststring = "\r\n" + lastModelLot;

                                if (res2.TrimStart().Length > modelLot.Length)
                                {
                                    if (res2.TrimStart().Length >= modelLot.Length + 2)
                                    {
                                        secondstring = res2.TrimStart().Substring(modelLot.Length, 2);
                                    }
                                    else
                                    {
                                        secondstring = res2.TrimStart().Substring(modelLot.Length);
                                    }
                                }

                                rtbModelImage2.AppendText(first2string);
                                rtbModelImage2.Select(0, modelLot.Length);
                                rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage2.AppendText(secondstring.Trim());
                                rtbModelImage2.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage2.SelectionColor = Color.Black;

                                rtbModelImage2.AppendText(reststring);
                                rtbModelImage2.Select(modelLot.Length, reststring.Length);
                                rtbModelImage2.SelectionColor = Color.Black;


                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }

                                splashScreenManager1.CloseWaitForm();
                                break;
                            }

                        }
                    }

                }

            }

            scrBrightness2.Value = 100;
            lblBrightness2.Text = "Brightness = 1.0";

            if (check)
            {
                status2 = "OK";
                if (status1 == "NG" || status3 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }
                return;
            }
            else
            {
                status2 = "NG";
                lblBrightness2.Text = "Brightness = 1";
                scrBrightness2.Value = 100;
                rtbModelImage2.Visible = true;
                rtbModelImage2.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage2.BackColor = Color.Lavender;
                rtbModelImage2.ForeColor = Color.Red;
                pictureBox2.Image = AdjustBrightness(oPicture2.Image, (float)(scrBrightness2.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);

                btnReCheck2.Visible = true;
                btnReCheck2.PerformClick();
                btnReCheck2.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void scrBrightness2_Scroll(object sender, ScrollEventArgs e)
        {
            GetResult2();
        }

        private void GetResult2()
        {
            lblBrightness2.Text = "Brightness = " + (scrBrightness2.Value / 100.0).ToString();
            if (pictureBox2 == null || pictureBox2.Image == null)
            {
                return;
            }
            pictureBox2.Image = AdjustBrightness(oPicture2.Image, (float)(scrBrightness2.Value / 100.0));
            rtbModelImage2.Text = "";
            string modelLot = rtbModelLot.Text;
            bool check = false;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {

                string res = "";

                using (var page = engine.Process((Bitmap)pictureBox2.Image, PageSegMode.AutoOnly))
                    res = page.GetText();


                if (res.Contains("|"))
                {
                    res = res.Replace("|", "");
                }
                if (res.Contains("i"))
                {
                    res = res.Replace("i", "");
                }
                if (res.Contains("-"))
                {
                    res = res.Replace("-", "");
                }
                if (res.Contains("l"))
                {
                    res = res.Replace("l", "L");
                }

                if (!string.IsNullOrWhiteSpace(res.Trim()))
                {
                    if (res.TrimStart().Length >= modelLot.Length)
                    {
                        string first2string = res.TrimStart().Substring(0, modelLot.Length);

                        string reststring = " ";

                        if (res.TrimStart().Length > modelLot.Length)
                        {
                            reststring = res.TrimStart().Substring(modelLot.Length);
                        }
                        rtbModelImage2.AppendText(first2string);
                        rtbModelImage2.Select(0, modelLot.Length);
                        rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

                        rtbModelImage2.AppendText(reststring);
                        rtbModelImage2.Select(modelLot.Length, reststring.Length);
                        rtbModelImage2.SelectionColor = Color.Black;


                        rtbModelImage2.BackColor = Color.Lavender;

                        if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                        {
                            rtbModelImage2.Visible = true;



                            if (!radioButton2.Checked)
                            {
                                lblResult.Visible = true;
                                lblResult.Text = "OK";
                                lblResult.ForeColor = Color.Green;
                                lblResult.Location = new Point(333, 30);
                                
                            }

                            check = true;

                        }

                    }

                }


                if (check)
                {
                    status2 = "OK";
                    if (status1 == "NG" || status3 == "NG")
                    {

                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    return;
                }
                else
                {
                    status2 = "NG";
                    rtbModelImage2.Visible = true;
                    rtbModelImage2.Text = res.Trim();
                    rtbModelImage2.Select(0, res.TrimStart().Length);
                    rtbModelImage2.SelectionColor = Color.Red;

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }

            }
        }

        private void GetResult3()
        {
            lblBrightness3.Text = "Brightness = " + (scrBrightness3.Value / 100.0).ToString();
            if (pictureBox3 == null || pictureBox3.Image == null)
            {
                return;
            }
            pictureBox3.Image = AdjustBrightness(oPicture3.Image, (float)(scrBrightness3.Value / 100.0));
            rtbModelImage3.Text = "";
            string modelLot = rtbModelLot.Text;
            bool check = false;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {

                string res = "";

                using (var page = engine.Process((Bitmap)pictureBox3.Image, PageSegMode.AutoOnly))
                    res = page.GetText();


                if (res.Contains("|"))
                {
                    res = res.Replace("|", "");
                }
                if (res.Contains("i"))
                {
                    res = res.Replace("i", "");
                }
                if (res.Contains("-"))
                {
                    res = res.Replace("-", "");
                }
                if (res.Contains("l"))
                {
                    res = res.Replace("l", "L");
                }

                if (!string.IsNullOrWhiteSpace(res))
                {
                    if (res.Length >= modelLot.Length)
                    {
                        string first2string = res.TrimStart().Substring(0, modelLot.Length);

                        string reststring = " ";

                        if (res.TrimStart().Length > modelLot.Length)
                        {
                            reststring = res.TrimStart().Substring(modelLot.Length);
                        }
                        rtbModelImage3.AppendText(first2string);
                        rtbModelImage3.Select(0, modelLot.Length);
                        rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

                        rtbModelImage3.AppendText(reststring);
                        rtbModelImage3.Select(modelLot.Length, reststring.Length);
                        rtbModelImage3.SelectionColor = Color.Black;


                        rtbModelImage3.BackColor = Color.Lavender;

                        if (modelLot == res.TrimStart().Substring(0, 2).Trim())
                        {
                            rtbModelImage3.Visible = true;

                            if (!radioButton2.Checked)
                            {
                                lblResult.Visible = true;
                                lblResult.Text = "OK";
                                lblResult.ForeColor = Color.Green;
                                lblResult.Location = new Point(333, 30);
                                
                            }

                            check = true;
                        }

                    }

                }


                if (check)
                {
                    status3 = "OK";
                    if (status1 == "NG" || status2 == "NG")
                    {

                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    return;
                }
                else
                {
                    status3 = "NG";
                    rtbModelImage3.Visible = true;
                    rtbModelImage3.Text = res.Trim();
                    rtbModelImage3.Select(0, res.TrimStart().Length);
                    rtbModelImage3.SelectionColor = Color.Red;

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }

            }
        }

        private void btnCheck3_Click(object sender, EventArgs e)
        {
            
            bool check = false;
            if (pictureBox3 == null || pictureBox3.Image == null)
            {
                return;
            }

            if (!splashScreenManager1.IsSplashFormVisible)
            {
                splashScreenManager1.ShowWaitForm();
            }


            string modelLot = rtbModelLot.Text.Trim();


            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                for (int i = 20; i <= 400; i++)
                {
                    rtbModelImage3.Text = "";
                    string res1 = "";
                    string res2 = "";
                    //string res3 = "";


                    pictureBox8.Image = AdjustBrightness(splitPicture31.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox8.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox8.Image = AdjustContrast((Bitmap)splitPicture31.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox8.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();

                    //pictureBox9.Image = AdjustBrightness(splitPicture32.Image, (float)(i * 1.0 / 100.0));

                    //using (var page = engine.Process((Bitmap)pictureBox9.Image, PageSegMode.AutoOnly))
                    //    res3 = page.GetText();


                    if (res1.Contains("|"))
                    {
                        res1 = res1.Replace("|", "");
                    }
                    if (res1.Contains("i"))
                    {
                        res1 = res1.Replace("i", "");
                    }
                    if (res1.Contains("-"))
                    {
                        res1 = res1.Replace("-", "");
                    }
                    if (res1.Contains("l"))
                    {
                        res1 = res1.Replace("l", "L");
                    }

                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "F OK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EOK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "EJK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 3 && res1.Trim().Substring(0, 3).ToUpper() == "FUK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(3);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "FR" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }
                    if (res1.Trim().Length >= 2 && res1.Trim().Substring(0, 2).ToUpper() == "EK" && rtbModelLot.Text.Trim() == "FJ")
                    {
                        res1 = "FJK" + res1.Trim().Substring(2);
                    }

                    if (res2.Contains("|"))
                    {
                        res2 = res2.Replace("|", "");
                    }
                    if (res2.Contains("i"))
                    {
                        res2 = res2.Replace("i", "");
                    }
                    if (res2.Contains("-"))
                    {
                        res2 = res2.Replace("-", "");
                    }
                    if (res2.Contains("l"))
                    {
                        res2 = res2.Replace("l", "L");
                    }

                    if (!string.IsNullOrEmpty(res1))
                    {
                        if (res1.TrimStart().Length >= modelLot.Length)
                        {
                            if (modelLot == res1.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                check = true;
                                status3 = "OK";
                                rtbModelImage3.Visible = true;

                                string first2string = res1.TrimStart().Substring(0, modelLot.Length);
                                string secondstring = " ";
                                string reststring = "\r\n" + lastModelLot;

                                if (res1.TrimStart().Length > modelLot.Length)
                                {
                                    if (res1.TrimStart().Length >= modelLot.Length + 2)
                                    {
                                        secondstring = res1.TrimStart().Substring(modelLot.Length, 2);
                                    }
                                    else
                                    {
                                        secondstring = res1.TrimStart().Substring(modelLot.Length);
                                    }
                                }

                                rtbModelImage3.AppendText(first2string);
                                rtbModelImage3.Select(0, modelLot.Length);
                                rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage3.AppendText(secondstring.Trim());
                                rtbModelImage3.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage3.SelectionColor = Color.Black;

                                rtbModelImage3.AppendText(reststring);
                                rtbModelImage3.Select(modelLot.Length, reststring.Length);
                                rtbModelImage3.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }

                                splashScreenManager1.CloseWaitForm();
                                break;
                            }

                        }
                    }
                    else if (!string.IsNullOrEmpty(res2.TrimStart()))
                    {
                        if (res2.TrimStart().Length >= modelLot.Length)
                        {
                            if (modelLot == res2.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                check = true;
                                status3 = "OK";
                                rtbModelImage3.Visible = true;

                                string first2string = res2.TrimStart().Substring(0, modelLot.Length);
                                string secondstring = " ";
                                string reststring = "\r\n" + lastModelLot;

                                if (res2.TrimStart().Length > modelLot.Length)
                                {
                                    if (res2.TrimStart().Length >= modelLot.Length + 2)
                                    {
                                        secondstring = res2.TrimStart().Substring(modelLot.Length, 2);
                                    }
                                    else
                                    {
                                        secondstring = res2.TrimStart().Substring(modelLot.Length);
                                    }
                                }

                                rtbModelImage3.AppendText(first2string);
                                rtbModelImage3.Select(0, modelLot.Length);
                                rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage3.AppendText(secondstring.Trim());
                                rtbModelImage3.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage3.SelectionColor = Color.Black;

                                rtbModelImage3.AppendText(reststring);
                                rtbModelImage3.Select(modelLot.Length, reststring.Length);
                                rtbModelImage3.SelectionColor = Color.Black;


                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }

                                splashScreenManager1.CloseWaitForm();
                                break;
                            }

                        }
                    }

                }

            }

            scrBrightness3.Value = 100;
            lblBrightness3.Text = "Brightness = 1.0";

            if (check)
            {
                status3 = "OK";
                if (status1 == "NG" || status2 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }
                return;
            }
            else
            {
                status3 = "NG";
                lblBrightness3.Text = "Brightness = 1";
                scrBrightness3.Value = 100;
                rtbModelImage3.Visible = true;
                rtbModelImage3.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage3.BackColor = Color.Lavender;
                rtbModelImage3.ForeColor = Color.Red;
                pictureBox3.Image = AdjustBrightness(oPicture3.Image, (float)(scrBrightness3.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);

                btnReCheck3.Visible = true;
                btnReCheck3.PerformClick();
                btnReCheck3.Visible = false;
            }

            splashScreenManager1.CloseWaitForm();
        }

        private void scrBrightness3_Scroll(object sender, ScrollEventArgs e)
        {
            GetResult3();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOP.Text.Trim()))
            {
                return;
            }
            txtOP.ReadOnly = true;
            //if (txtOP.Text.Trim().ToUpper() == "ADMIN")
            //{
            //    linkHIS.Visible = true;
            //}
        }

        private void txtOP_Leave(object sender, EventArgs e)
        {
            txtOP.ReadOnly = true;
            //if (txtOP.Text.Trim().ToUpper() == "ADMIN")
            //{
            //    linkHIS.Visible = true;
            //}
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtOP.ReadOnly = false;
            txtOP.Text = "";
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string create_user = txtOP.Text.Trim();
            if (string.IsNullOrWhiteSpace(create_user))
            {
                MessageBox.Show("NHẬP THÔNG TIN USER!\r\n\r\n" +
                    "PLEASE ENTER USER INFO!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOP.Focus();
                txtOP.ReadOnly = false;
                return;
            }

            string process = cbbCD.Text;
            if (process != "FA" && process != "VI" && process != "OQC" && process != "PACKING")
            {
                MessageBox.Show("HÃY NHẬP TÊN CÔNG ĐOẠN.\r\nTÊN CÔNG ĐOẠN LÀ MỘT TRONG CÁC LOẠI SAU:\r\n\r\n" +
                    "- FA\r\n" +
                    "- VI\r\n" +
                    "- OQC\r\n" +
                    "- PACKING", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string modelLot = rtbModelLot.Text;

            if (string.IsNullOrWhiteSpace(modelLot))
            {
                MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                    "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = new DataTable();
            //dt.Columns.Add("LOT_NO", typeof(string));
            //dt.Columns.Add("MODEL_LOT", typeof(string));
            dt.Columns.Add("MODEL_IMAGE", typeof(string));
            dt.Columns.Add("BRIGHTNESS", typeof(string));
            dt.Columns.Add("RESULT", typeof(string));
            dt.Columns.Add("IMAGE_BASE64", typeof(string));

            //dt.Columns.Add("CREATE_USER", typeof(string));

            string lot_no = lblLot.Text.Trim().ToUpper();

            if (pictureBox1.Image != null)
            {
                dt.Rows.Add(rtbModelImage1.Text.Trim(), scrBrightness1.Value.ToString(), status1, GetB64String(pictureBox1.Image));
            }
            if (pictureBox2.Image != null)
            {
                dt.Rows.Add(rtbModelImage2.Text.Trim(), scrBrightness2.Value.ToString(), status2, GetB64String(pictureBox2.Image));
            }
            if (pictureBox3.Image != null)
            {
                dt.Rows.Add(rtbModelImage3.Text.Trim(), scrBrightness3.Value.ToString(), status3, GetB64String(pictureBox3.Image));
            }
            if (cbbCD.Text == "VI" || cbbCD.Text == "OQC")
            {
                if (dt.Rows.Count < 3)
                {
                    MessageBox.Show("CHƯA ĐỦ SỐ LƯỢNG 3 ẢNH." +
                        "\r\n\r\nPLEASE TAKE FULL OF 3 IMAGES.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (status1 == "NOTCHECK" || status2 == "NOTCHECK" || status3 == "NOTCHECK")
                {
                    MessageBox.Show("CHƯA CHECK HẾT" +
                        "\r\n\r\nPLEASE CHECK ALL.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (cbbCD.Text == "FA" || cbbCD.Text == "PACKING")
            {

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("CHƯA ĐỦ SỐ LƯỢNG 1 ẢNH." +
                        "\r\n\r\nPLEASE TAKE 1 IMAGE.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (status1 == "NOTCHECK")
                {
                    MessageBox.Show("CHƯA THỰC HIỆN THAO TÁC CHECK" +
                        "\r\n\r\nPLEASE DO CHECK ACTION.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            //string stringXML = GetDataTableToXml(dt);

            if(!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("CHƯA XÁC NHẬN TÌNH TRẠNG COVER TAPE" +
                        "\r\n\r\nPLEASE CHECK COVER TAPE STATUS", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cover_tape_status = string.Empty;
            string note = txtNote.Text.Trim();
            if (radioButton1.Checked)
            {
                cover_tape_status = "OK";
            }
            if (radioButton2.Checked)
            {
                cover_tape_status = "NG";
            }

            try
            {

                //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                string connString = "Data Source = 10.70.21.214;Initial Catalog = MESDB;User Id = mesother;Password = othermes;Connect Timeout=3";
                SqlConnection conn = new SqlConnection(connString);

                if (cbbCD.Text == "VI" || cbbCD.Text == "OQC")
                {
                    SqlCommand cmd = new SqlCommand("SP_INSERT_RESULT", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
                    cmd.Parameters.Add("@A_MODEL", SqlDbType.VarChar).Value = Model;
                    cmd.Parameters.Add("@A_MODEL_LOT", SqlDbType.VarChar).Value = modelLot;
                    cmd.Parameters.Add("@A_MODEL_IMAGE1", SqlDbType.VarChar).Value = rtbModelImage1.Text.Trim();
                    cmd.Parameters.Add("@A_MODEL_IMAGE2", SqlDbType.VarChar).Value = rtbModelImage2.Text.Trim();
                    cmd.Parameters.Add("@A_MODEL_IMAGE3", SqlDbType.VarChar).Value = rtbModelImage3.Text.Trim();
                    cmd.Parameters.Add("@A_BRIGHTNESS1", SqlDbType.VarChar).Value = scrBrightness1.Value.ToString();
                    cmd.Parameters.Add("@A_BRIGHTNESS2", SqlDbType.VarChar).Value = scrBrightness2.Value.ToString();
                    cmd.Parameters.Add("@A_BRIGHTNESS3", SqlDbType.VarChar).Value = scrBrightness3.Value.ToString();
                    cmd.Parameters.Add("@A_RESULT1", SqlDbType.VarChar).Value = status1;
                    cmd.Parameters.Add("@A_RESULT2", SqlDbType.VarChar).Value = status2;
                    cmd.Parameters.Add("@A_RESULT3", SqlDbType.VarChar).Value = status3;
                    cmd.Parameters.Add("@A_IMAGE_BASE641", SqlDbType.VarChar).Value = GetB64String(pictureBox1.Image);
                    cmd.Parameters.Add("@A_IMAGE_BASE642", SqlDbType.VarChar).Value = GetB64String(pictureBox2.Image);
                    cmd.Parameters.Add("@A_IMAGE_BASE643", SqlDbType.VarChar).Value = GetB64String(pictureBox3.Image);
                    //cmd.Parameters.Add("@A_XML", SqlDbType.Xml).Value = stringXML;
                    cmd.Parameters.Add("@A_CREATE_USER", SqlDbType.NVarChar).Value = create_user;
                    cmd.Parameters.Add("@A_PC_IP_ADDRESS", SqlDbType.VarChar).Value = pcIP;
                    cmd.Parameters.Add("@A_PC_NAME", SqlDbType.NVarChar).Value = pcName;
                    cmd.Parameters.Add("@A_PROCESS", SqlDbType.VarChar).Value = process;
                    cmd.Parameters.Add("@A_COVER_TAPE_STATUS", SqlDbType.VarChar).Value = cover_tape_status;
                    cmd.Parameters.Add("@A_NOTE", SqlDbType.NVarChar).Value = note;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SP_INSERT_RESULT_ONE", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
                    cmd.Parameters.Add("@A_MODEL", SqlDbType.VarChar).Value = Model;
                    cmd.Parameters.Add("@A_MODEL_LOT", SqlDbType.VarChar).Value = modelLot;
                    cmd.Parameters.Add("@A_MODEL_IMAGE1", SqlDbType.VarChar).Value = rtbModelImage1.Text.Trim();
                    cmd.Parameters.Add("@A_BRIGHTNESS1", SqlDbType.VarChar).Value = scrBrightness1.Value.ToString();
                    cmd.Parameters.Add("@A_RESULT1", SqlDbType.VarChar).Value = status1;
                    cmd.Parameters.Add("@A_IMAGE_BASE641", SqlDbType.VarChar).Value = GetB64String(pictureBox1.Image);
                    //cmd.Parameters.Add("@A_XML", SqlDbType.Xml).Value = stringXML;
                    cmd.Parameters.Add("@A_CREATE_USER", SqlDbType.NVarChar).Value = create_user;
                    cmd.Parameters.Add("@A_PC_IP_ADDRESS", SqlDbType.VarChar).Value = pcIP;
                    cmd.Parameters.Add("@A_PC_NAME", SqlDbType.NVarChar).Value = pcName;
                    cmd.Parameters.Add("@A_PROCESS", SqlDbType.VarChar).Value = process;
                    cmd.Parameters.Add("@A_COVER_TAPE_STATUS", SqlDbType.VarChar).Value = cover_tape_status;
                    cmd.Parameters.Add("@A_NOTE", SqlDbType.NVarChar).Value = note;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("       SAVE OK!       ", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblLot.Text = string.Empty;
                lblModel.Text = string.Empty;
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox3.Image = null;

                rtbModelImage1.Text = string.Empty;
                rtbModelImage2.Text = string.Empty;
                rtbModelImage3.Text = string.Empty;

                status1 = "NOTCHECK";
                status2 = "NOTCHECK";
                status3 = "NOTCHECK";

                txtNote.Text = string.Empty;
                radioButton1.Checked = false;
                radioButton2.Checked = false;

                rtbModelLot.Text = string.Empty;
                lblResult.Text = string.Empty;
                Model = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetB64String(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Byte[] b = ms.ToArray();
            string b64 = Convert.ToBase64String(b);
            return b64;
        }

        private void linkHIS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }

        private string GetIp()
        {
            try
            {
                string ip = string.Empty;

                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

                for (int i = 0; i < ipHostEntry.AddressList.Length; i++)
                {
                    if (ipHostEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ip = ipHostEntry.AddressList[i].ToString();
                        break;
                    }
                }

                return ip;
            }
            catch
            {
                return string.Empty;
            }
        }


        private string GetPCInfo()
        {
            try
            {
                string name = string.Empty;

                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

                if (ipHostEntry == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ipHostEntry.HostName.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private void txtOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOP.Text.Trim()))
            {
                return;
            }

            if(e.KeyCode == Keys.Enter)
            {
                txtOP.ReadOnly = true;
                if (txtOP.Text.Trim().ToUpper() == "ADMIN")
                {
                    linkHIS.Visible = true;
                }
            }
        }


        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
        }

        private void btnRecheck1_Click(object sender, EventArgs e)
        {
            if (pictureBox1 == null || pictureBox1.Image == null)
            {
                return;
            }

            bool ch1 = false;

            string modelLot = rtbModelLot.Text;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                for (int i = 100; i <= 400; i++)
                {
                    string res = "";

                    rtbModelImage1.Text = "";

                    lblBrightness1.Text = "Brightness = " + (i*1.0 / 100.0).ToString();

                    pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox1.Image, PageSegMode.AutoOnly))
                        res = page.GetText();

                    if (res.Contains("|"))
                    {
                        res = res.Replace("|", "");
                    }
                    if (res.Contains("i"))
                    {
                        res = res.Replace("i", "");
                    }
                    if (res.Contains("-"))
                    {
                        res = res.Replace("-", "");
                    }
                    if (res.Contains("l"))
                    {
                        res = res.Replace("l", "L");
                    }

                    if (!string.IsNullOrWhiteSpace(res))
                    {
                        if (res.TrimStart().Length >= modelLot.Length)
                        {
                            string first2string = res.TrimStart().Substring(0, modelLot.Length);

                            string reststring = " ";

                            if (res.TrimStart().Length > modelLot.Length)
                            {
                                reststring = res.TrimStart().Substring(modelLot.Length);
                            }
                            rtbModelImage1.AppendText(first2string);
                            rtbModelImage1.Select(0, modelLot.Length);
                            rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage1.AppendText(reststring);
                            rtbModelImage1.Select(modelLot.Length, reststring.Length);
                            rtbModelImage1.SelectionColor = Color.Black;

                            rtbModelImage1.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            //if (lastModelLot == res.Trim().Substring(res.Trim().Length-2, 2))
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }


                                scrBrightness1.Value = i;
                                lblBrightness1.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status1 = "OK";
                                ch1 = true;
                                //MessageBox.Show(res.ToString());
                                break;
                            }
                        }

                    }
                }
            }

            // Mạnh bỏ đoạn code nay để áp dụng cho Model có yF - ngày 10/03/2022 **************
            //if (!ch1 && modelLot.Trim() == "yF")
            //{
            //    rtbModelImage1.ResetText();
            //    rtbModelImage1.AppendText("yF");
            //    rtbModelImage1.Select(0, modelLot.Length);
            //    rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

            //    rtbModelImage1.AppendText("\r\n" + lastModelLot);
            //    rtbModelImage1.Select(modelLot.Length, lastModelLot.Length);
            //    rtbModelImage1.SelectionColor = Color.Black;

            //    rtbModelImage1.Visible = true;

            //    if (!radioButton2.Checked)
            //    {
            //        lblResult.Visible = true;
            //        lblResult.Text = "OK";
            //        lblResult.ForeColor = Color.Green;
            //        lblResult.Location = new Point(333, 30);

            //    }


            //    scrBrightness1.Value = 150;
            //    lblBrightness1.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
            //    pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
            //    status1 = "OK";
            //}
            //else
            // *************************************************************************************
            if (!ch1)
            {
                rtbModelImage1.Text = "";
                rtbModelImage1.Visible = true;
                rtbModelImage1.AppendText("Not " + modelLot);
                rtbModelImage1.SelectionColor = Color.Red;

                status1 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);


                lblBrightness1.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage1.ReadOnly = false;
                }
                else
                {
                    rtbModelImage1.ReadOnly = true;
                }
            }
        }

        private void btnReCheck2_Click(object sender, EventArgs e)
        {
            if (pictureBox2 == null || pictureBox2.Image == null)
            {
                return;
            }

            bool ch2 = false;

            string modelLot = rtbModelLot.Text;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                for (int i = 100; i <= 400; i++)
                {
                    string res = "";

                    rtbModelImage2.Text = "";

                    lblBrightness2.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox2.Image = AdjustBrightness(oPicture2.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox2.Image, PageSegMode.AutoOnly))
                        res = page.GetText();

                    if (res.Contains("|"))
                    {
                        res = res.Replace("|", "");
                    }
                    if (res.Contains("i"))
                    {
                        res = res.Replace("i", "");
                    }
                    if (res.Contains("-"))
                    {
                        res = res.Replace("-", "");
                    }
                    if (res.Contains("l"))
                    {
                        res = res.Replace("l", "L");
                    }

                    if (!string.IsNullOrWhiteSpace(res.Trim()))
                    {
                        if (res.TrimStart().Length >= modelLot.Length)
                        {
                            string first2string = res.TrimStart().Substring(0, modelLot.Length);

                            string reststring = " ";

                            if (res.TrimStart().Length > modelLot.Length)
                            {
                                reststring = res.TrimStart().Substring(modelLot.Length);
                            }
                            rtbModelImage2.AppendText(first2string);
                            rtbModelImage2.Select(0, modelLot.Length);
                            rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage2.AppendText(reststring);
                            rtbModelImage2.Select(modelLot.Length, reststring.Length);
                            rtbModelImage2.SelectionColor = Color.Black;

                            rtbModelImage2.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }


                                scrBrightness2.Value = i;
                                lblBrightness2.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status2 = "OK";
                                ch2 = true;
                                break;
                            }

                        }

                    }
                }
            }
            // Mạnh bỏ đoạn code nay để áp dụng cho Model có yF - ngày 10/03/2022 **************

            //if (!ch2 && modelLot.Trim() == "yF")
            //{
            //    rtbModelImage2.ResetText();
            //    rtbModelImage2.AppendText("yF");
            //    rtbModelImage2.Select(0, modelLot.Length);
            //    rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

            //    rtbModelImage2.AppendText("\r\n" + lastModelLot);
            //    rtbModelImage2.Select(modelLot.Length, lastModelLot.Length);
            //    rtbModelImage2.SelectionColor = Color.Black;

            //    rtbModelImage2.Visible = true;

            //    if (!radioButton2.Checked)
            //    {
            //        lblResult.Visible = true;
            //        lblResult.Text = "OK";
            //        lblResult.ForeColor = Color.Green;
            //        lblResult.Location = new Point(333, 30);
            //    }


            //    scrBrightness2.Value = 150;
            //    lblBrightness2.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
            //    pictureBox2.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
            //    status2 = "OK";
            //}
            //else 
            // **************************************************************************************
            if (!ch2)
            {
                rtbModelImage2.Text = "";
                rtbModelImage2.Visible = true;
                rtbModelImage2.AppendText("Not " + modelLot);
                rtbModelImage2.SelectionColor = Color.Red;

                status2 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);

                lblBrightness2.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox2.Image = AdjustBrightness(oPicture2.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage2.ReadOnly = false;
                }
                else
                {
                    rtbModelImage2.ReadOnly = true;
                }
            }
        }

        private void btnReCheck3_Click(object sender, EventArgs e)
        {
            if (pictureBox3 == null || pictureBox3.Image == null)
            {
                return;
            }

            bool ch3 = false;

            string modelLot = rtbModelLot.Text;

            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                for (int i = 100; i <= 400; i++)
                {
                    string res = "";

                    rtbModelImage3.Text = "";

                    lblBrightness3.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox3.Image = AdjustBrightness(oPicture3.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox3.Image, PageSegMode.AutoOnly))
                        res = page.GetText();

                    if (res.Contains("|"))
                    {
                        res = res.Replace("|", "");
                    }
                    if (res.Contains("i"))
                    {
                        res = res.Replace("i", "");
                    }
                    if (res.Contains("-"))
                    {
                        res = res.Replace("-", "");
                    }
                    if (res.Contains("l"))
                    {
                        res = res.Replace("l", "L");
                    }

                    if (!string.IsNullOrWhiteSpace(res.Trim()))
                    {
                        if (res.TrimStart().Length >= modelLot.Length)
                        {
                            string first2string = res.TrimStart().Substring(0, modelLot.Length);

                            string reststring = " ";

                            if (res.TrimStart().Length > modelLot.Length)
                            {
                                reststring = res.TrimStart().Substring(modelLot.Length);
                            }
                            rtbModelImage3.AppendText(first2string);
                            rtbModelImage3.Select(0, modelLot.Length);
                            rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage3.AppendText(reststring);
                            rtbModelImage3.Select(modelLot.Length, reststring.Length);
                            rtbModelImage3.SelectionColor = Color.Black;

                            rtbModelImage3.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(333, 30);
                                    
                                }


                                scrBrightness3.Value = i;
                                lblBrightness3.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status3 = "OK";
                                ch3 = true;
                                break;
                            }

                        }

                    }
                }
            }
            // Mạnh bỏ đoạn code nay để áp dụng cho Model có yF - ngày 10/03/2022 **************

            //if (!ch3 && modelLot.Trim() == "yF")
            //{
            //    rtbModelImage3.ResetText();
            //    rtbModelImage3.AppendText("yF");
            //    rtbModelImage3.Select(0, modelLot.Length);
            //    rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

            //    rtbModelImage3.AppendText("\r\n" + lastModelLot);
            //    rtbModelImage3.Select(modelLot.Length, lastModelLot.Length);
            //    rtbModelImage3.SelectionColor = Color.Black;

            //    rtbModelImage3.Visible = true;

            //    if (!radioButton2.Checked)
            //    {
            //        lblResult.Visible = true;
            //        lblResult.Text = "OK";
            //        lblResult.ForeColor = Color.Green;
            //        lblResult.Location = new Point(333, 30);
            //        //
            //    }


            //    scrBrightness3.Value = 150;
            //    lblBrightness3.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
            //    pictureBox3.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
            //    status3 = "OK";
            //}
            //else 
            // ***********************************************************************************
            if (!ch3)
            {
                rtbModelImage3.Text = "";
                rtbModelImage3.Visible = true;
                rtbModelImage3.AppendText("Not " + modelLot);
                rtbModelImage3.SelectionColor = Color.Red;

                status3 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);


                lblBrightness3.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox3.Image = AdjustBrightness(oPicture3.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage3.ReadOnly = false;
                }
                else
                {
                    rtbModelImage3.ReadOnly = true;
                }
            }
        }

        private void cbbCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOP.Text.Trim()) && !string.IsNullOrWhiteSpace(cbbCD.Text))
            {
                MessageBox.Show("HÃY NHẬP USER VÀO TRƯỚC", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbCD.SelectedIndex = -1;
                txtOP.ReadOnly = false;
                return;
            }
            if(cbbCD.Text == "MARKING")
            {
                Form3 frm = new Form3(txtOP.Text.Trim());
                frm.ShowDialog();

                cbbCD.SelectedIndex = -1;
            }
            else if(cbbCD.Text == "FA" || cbbCD.Text == "PACKING")
            {
                btnTakePicture2.Enabled = false;
                btnTakePicture3.Enabled = false;

                pictureBox2.Image = null;
                pictureBox3.Image = null;
                rtbModelImage2.Text = string.Empty;
                rtbModelImage3.Text = string.Empty;
            }
            else
            {
                btnTakePicture2.Enabled = true;
                btnTakePicture3.Enabled = true;
            }
        }


        private void rtbModelImage1_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage1.Text.TrimStart().Length >= modelLength)
            {
                if(rtbModelImage1.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status1 = "OK";

                    rtbModelImage1.Select(0, modelLength);
                    rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage1.Select(rtbModelImage1.Text.Length, 0);
                    rtbModelImage1.SelectionColor = Color.Black;
                    if (status2 == "NG" || status3 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(333, 30);
                            
                        }
                    }
                }
                else
                {
                    status1 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);

                    rtbModelImage1.Select(0, rtbModelImage1.Text.Length);
                    rtbModelImage1.SelectionColor = Color.Red;
                    rtbModelImage1.Select(rtbModelImage1.Text.Length, 0);
                    rtbModelImage1.SelectionColor = Color.Black;
                }
            }
            else
            {
                status1 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);
                rtbModelImage1.Select(0, rtbModelImage1.Text.Length);
                rtbModelImage1.SelectionColor = Color.Red;
                rtbModelImage1.Select(rtbModelImage1.Text.Length, 0);
                rtbModelImage1.SelectionColor = Color.Black;
            }
        }

        private void rtbModelImage2_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage2.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage2.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status2 = "OK";

                    rtbModelImage2.Select(0, modelLength);
                    rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage2.Select(rtbModelImage2.Text.Length, 0);
                    rtbModelImage2.SelectionColor = Color.Black;
                    if (status1 == "NG" || status3 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(333, 30);
                            

                        }
                    }
                }
                else
                {
                    status2 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);

                    rtbModelImage2.Select(0, rtbModelImage2.Text.Length);
                    rtbModelImage2.SelectionColor = Color.Red;
                    rtbModelImage2.Select(rtbModelImage2.Text.Length, 0);
                    rtbModelImage2.SelectionColor = Color.Black;
                }
            }
            else
            {
                status2 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);
                rtbModelImage2.Select(0, rtbModelImage2.Text.Length);
                rtbModelImage2.SelectionColor = Color.Red;
                rtbModelImage2.Select(rtbModelImage2.Text.Length, 0);
                rtbModelImage2.SelectionColor = Color.Black;
            }
        }

        private void rtbModelImage3_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage3.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage3.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status3 = "OK";

                    rtbModelImage3.Select(0, modelLength);
                    rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage3.Select(rtbModelImage3.Text.Length, 0);
                    rtbModelImage3.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(333, 30);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(333, 30);
                            //
                        }
                    }
                }
                else
                {
                    status3 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);

                    rtbModelImage3.Select(0, rtbModelImage3.Text.Length);
                    rtbModelImage3.SelectionColor = Color.Red;
                    rtbModelImage3.Select(rtbModelImage3.Text.Length, 0);
                    rtbModelImage3.SelectionColor = Color.Black;
                }
            }
            else
            {
                status3 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);
                rtbModelImage3.Select(0, rtbModelImage3.Text.Length);
                rtbModelImage3.SelectionColor = Color.Red;
                rtbModelImage3.Select(rtbModelImage3.Text.Length, 0);
                rtbModelImage3.SelectionColor = Color.Black;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if(status1 == "NG" || status2 == "NG" || status3 == "NG")
                {
                    lblResult.Visible = true;
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(333, 30);
                }
                else
                {
                    if (!radioButton2.Checked)
                    {
                        lblResult.Visible = true;
                        lblResult.Text = "OK";
                        lblResult.ForeColor = Color.Green;
                        lblResult.Location = new Point(333, 30);
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                lblResult.Visible = true;
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(333, 30);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string t = "hoc C# ngay 20220308";
            string r = t.Substring(t.Length-2, 2);
        }
    }
}
