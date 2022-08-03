using ComponentFactory.Krypton.Toolkit;
using DevExpress.XtraSplashScreen;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace CSP_OCR
{
    public partial class Form3 : KryptonForm
    {
        string USER;
        int intCheck = 0;
        string Model = string.Empty;
        string lastModelLot = string.Empty;
        string status1 = "NOTCHECK";
        string status2 = "NOTCHECK";
        string status3 = "NOTCHECK";
        string status4 = "NOTCHECK";
        string status5 = "NOTCHECK";
        string status6 = "NOTCHECK";
        string status7 = "NOTCHECK";
        string status8 = "NOTCHECK";
        string status9 = "NOTCHECK";
        string status10 = "NOTCHECK";
        string status11 = "NOTCHECK";
        string status12 = "NOTCHECK";
        string pcIP = "";
        string pcName = "";
        public Form3(string user)
        {
            InitializeComponent();
            USER = user;

            lblLot.Visible = false;
            lblModel.Visible = false;
            rtbModelLot.Visible = false;
            rtbModelLot.Text = string.Empty;
            lblLine1.Text = "_______________________________________________________________________________________________________________________________________________________________________________";
            lblResult.Visible = false;

            rtbModelImage1.Visible = false;
            rtbModelImage2.Visible = false;
            rtbModelImage3.Visible = false;
            rtbModelImage4.Visible = false;
            rtbModelImage5.Visible = false;
            rtbModelImage6.Visible = false;
            rtbModelImage7.Visible = false;
            rtbModelImage8.Visible = false;
            rtbModelImage9.Visible = false;
            rtbModelImage10.Visible = false;
            rtbModelImage11.Visible = false;
            rtbModelImage12.Visible = false;

            rtbModelImage1.BorderStyle = BorderStyle.None;
            rtbModelImage2.BorderStyle = BorderStyle.None;
            rtbModelImage3.BorderStyle = BorderStyle.None;
            rtbModelImage4.BorderStyle = BorderStyle.None;
            rtbModelImage5.BorderStyle = BorderStyle.None;
            rtbModelImage6.BorderStyle = BorderStyle.None;
            rtbModelImage7.BorderStyle = BorderStyle.None;
            rtbModelImage8.BorderStyle = BorderStyle.None;
            rtbModelImage9.BorderStyle = BorderStyle.None;
            rtbModelImage10.BorderStyle = BorderStyle.None;
            rtbModelImage11.BorderStyle = BorderStyle.None;
            rtbModelImage12.BorderStyle = BorderStyle.None;

            rtbModelImage1.BackColor = Color.Lavender;
            rtbModelImage2.BackColor = Color.Lavender;
            rtbModelImage3.BackColor = Color.Lavender;
            rtbModelImage4.BackColor = Color.Lavender;
            rtbModelImage5.BackColor = Color.Lavender;
            rtbModelImage6.BackColor = Color.Lavender;
            rtbModelImage7.BackColor = Color.Lavender;
            rtbModelImage8.BackColor = Color.Lavender;
            rtbModelImage9.BackColor = Color.Lavender;
            rtbModelImage10.BackColor = Color.Lavender;
            rtbModelImage11.BackColor = Color.Lavender;
            rtbModelImage12.BackColor = Color.Lavender;

            scrBrightness1.Value = 100;
            scrBrightness2.Value = 100;
            scrBrightness3.Value = 100;
            scrBrightness4.Value = 100;
            scrBrightness5.Value = 100;
            scrBrightness6.Value = 100;
            scrBrightness7.Value = 100;
            scrBrightness8.Value = 100;
            scrBrightness9.Value = 100;
            scrBrightness10.Value = 100;
            scrBrightness11.Value = 100;
            scrBrightness12.Value = 100;

            AdjustImage1();
            AdjustImage2();
            AdjustImage3();
            AdjustImage4();
            AdjustImage5();
            AdjustImage6();
            AdjustImage7();
            AdjustImage8();
            AdjustImage9();
            AdjustImage10();
            AdjustImage11();
            AdjustImage12();
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

        private void AdjustImage4()
        {
            lblBrightness4.Text = "Brightness = " + (scrBrightness4.Value / 100.0).ToString();
            if (pictureBox4 == null || pictureBox4.Image == null)
            {
                return;
            }
            pictureBox4.Image = AdjustBrightness(oPicture4.Image, (float)(scrBrightness4.Value / 100.0));
        }

        private void AdjustImage5()
        {
            lblBrightness5.Text = "Brightness = " + (scrBrightness5.Value / 100.0).ToString();
            if (pictureBox5 == null || pictureBox5.Image == null)
            {
                return;
            }
            pictureBox5.Image = AdjustBrightness(oPicture5.Image, (float)(scrBrightness5.Value / 100.0));
        }

        private void AdjustImage6()
        {
            lblBrightness6.Text = "Brightness = " + (scrBrightness6.Value / 100.0).ToString();
            if (pictureBox6 == null || pictureBox6.Image == null)
            {
                return;
            }
            pictureBox6.Image = AdjustBrightness(oPicture6.Image, (float)(scrBrightness6.Value / 100.0));
        }

        private void AdjustImage7()
        {
            lblBrightness7.Text = "Brightness = " + (scrBrightness7.Value / 100.0).ToString();
            if (pictureBox7 == null || pictureBox7.Image == null)
            {
                return;
            }
            pictureBox7.Image = AdjustBrightness(oPicture7.Image, (float)(scrBrightness7.Value / 100.0));
        }

        private void AdjustImage8()
        {
            lblBrightness8.Text = "Brightness = " + (scrBrightness8.Value / 100.0).ToString();
            if (pictureBox8 == null || pictureBox8.Image == null)
            {
                return;
            }
            pictureBox8.Image = AdjustBrightness(oPicture8.Image, (float)(scrBrightness8.Value / 100.0));
        }

        private void AdjustImage9()
        {
            lblBrightness9.Text = "Brightness = " + (scrBrightness9.Value / 100.0).ToString();
            if (pictureBox9 == null || pictureBox9.Image == null)
            {
                return;
            }
            pictureBox9.Image = AdjustBrightness(oPicture9.Image, (float)(scrBrightness9.Value / 100.0));
        }

        private void AdjustImage10()
        {
            lblBrightness10.Text = "Brightness = " + (scrBrightness10.Value / 100.0).ToString();
            if (pictureBox10 == null || pictureBox10.Image == null)
            {
                return;
            }
            pictureBox10.Image = AdjustBrightness(oPicture10.Image, (float)(scrBrightness10.Value / 100.0));
        }

        private void AdjustImage11()
        {
            lblBrightness11.Text = "Brightness = " + (scrBrightness11.Value / 100.0).ToString();
            if (pictureBox110 == null || pictureBox110.Image == null)
            {
                return;
            }
            pictureBox110.Image = AdjustBrightness(oPicture11.Image, (float)(scrBrightness11.Value / 100.0));
        }

        private void AdjustImage12()
        {
            lblBrightness12.Text = "Brightness = " + (scrBrightness12.Value / 100.0).ToString();
            if (pictureBox12 == null || pictureBox12.Image == null)
            {
                return;
            }
            pictureBox12.Image = AdjustBrightness(oPicture12.Image, (float)(scrBrightness12.Value / 100.0));
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
                conn.Close();

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

                    //rtbModelLot.Text = ml.Substring(0, numberOfCheck).Trim();
                    //lastModelLot = ml.Substring(ml.Length - 2 - countSpace);


                    rtbModelLot.BackColor = Color.Lavender;
                    rtbModelLot.BorderStyle = BorderStyle.None;
                    rtbModelLot.ForeColor = Color.FromArgb(0, 120, 215);
                    rtbModelLot.Multiline = false;

                    rtbModelImage1.Text = string.Empty;
                    //rtbModelImage2.Text = string.Empty;
                    //rtbModelImage3.Text = string.Empty;
                    rtbModelImage1.ReadOnly = true;
                    //rtbModelImage2.ReadOnly = true;
                    //rtbModelImage3.ReadOnly = true;

                    pictureBox1.Image = null;
                    //pictureBox2.Image = null;
                    //pictureBox3.Image = null;
                    status1 = "NOTCHECK";
                    //status2 = "NOTCHECK";
                    //status3 = "NOTCHECK";

                    lblResult.Text = string.Empty;
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture1_Click(object sender, EventArgs e)
        {
            try
            {

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
            catch (Exception ex)
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
                for (int i = 20; i <= 400; i++)
                {
                    rtbModelImage1.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox11.Image = AdjustBrightness(splitPicture11.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox11.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox11.Image = AdjustContrast((Bitmap)splitPicture11.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox11.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                    lblResult.Location = new Point(900, 29);
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
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                if (status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG" 
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
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
                lblResult.Location = new Point(900, 29);

                btnReCheck1.Visible = true;
                btnReCheck1.PerformClick();
                btnReCheck1.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck1_Click(object sender, EventArgs e)
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

                    lblBrightness1.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

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
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness1.Value = i;
                                lblBrightness1.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status1 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage1.ResetText();
                rtbModelImage1.AppendText("yF");
                rtbModelImage1.Select(0, modelLot.Length);
                rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage1.AppendText("\r\n" + lastModelLot);
                rtbModelImage1.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage1.SelectionColor = Color.Black;

                rtbModelImage1.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness1.Value = 150;
                lblBrightness1.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox1.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status1 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage1.Text = "";
                rtbModelImage1.Visible = true;
                rtbModelImage1.AppendText("Not " + modelLot);
                rtbModelImage1.SelectionColor = Color.Red;

                status1 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


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

        private void rtbModelImage1_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage1.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage1.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status1 = "OK";

                    rtbModelImage1.Select(0, modelLength);
                    rtbModelImage1.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage1.Select(rtbModelImage1.Text.Length, 0);
                    rtbModelImage1.SelectionColor = Color.Black;
                    if (status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status1 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

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
                lblResult.Location = new Point(900, 29);
                rtbModelImage1.Select(0, rtbModelImage1.Text.Length);
                rtbModelImage1.SelectionColor = Color.Red;
                rtbModelImage1.Select(rtbModelImage1.Text.Length, 0);
                rtbModelImage1.SelectionColor = Color.Black;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture2_Click(object sender, EventArgs e)
        {
            try
            {

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                hei = oPicture2.Height / 2 + 10;
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


                    pictureBox21.Image = AdjustBrightness(splitPicture21.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox21.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox21.Image = AdjustContrast((Bitmap)splitPicture21.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox21.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage2.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                    lblResult.Location = new Point(900, 29);
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
                if (status1 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
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
                lblResult.Location = new Point(900, 29);

                btnReCheck2.Visible = true;
                btnReCheck2.PerformClick();
                btnReCheck2.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck2_Click(object sender, EventArgs e)
        {
            if (pictureBox2 == null || pictureBox2.Image == null)
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
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness2.Value = i;
                                lblBrightness2.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status2 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage2.ResetText();
                rtbModelImage2.AppendText("yF");
                rtbModelImage2.Select(0, modelLot.Length);
                rtbModelImage2.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage2.AppendText("\r\n" + lastModelLot);
                rtbModelImage2.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage2.SelectionColor = Color.Black;

                rtbModelImage2.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness2.Value = 150;
                lblBrightness2.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox2.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status2 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage2.Text = "";
                rtbModelImage2.Visible = true;
                rtbModelImage2.AppendText("Not " + modelLot);
                rtbModelImage2.SelectionColor = Color.Red;

                status2 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


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
                    if (status1 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status2 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

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
                lblResult.Location = new Point(900, 29);
                rtbModelImage2.Select(0, rtbModelImage2.Text.Length);
                rtbModelImage2.SelectionColor = Color.Red;
                rtbModelImage2.Select(rtbModelImage2.Text.Length, 0);
                rtbModelImage2.SelectionColor = Color.Black;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture3_Click(object sender, EventArgs e)
        {
            try
            {

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                hei = oPicture3.Height / 2 + 10;
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


                    pictureBox31.Image = AdjustBrightness(splitPicture31.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox31.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox31.Image = AdjustContrast((Bitmap)splitPicture31.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox31.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage3.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status3 = "OK";
                if (status1 == "NG" || status2 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
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
                lblResult.Location = new Point(900, 29);

                btnReCheck3.Visible = true;
                btnReCheck3.PerformClick();
                btnReCheck3.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck3_Click(object sender, EventArgs e)
        {

            if (pictureBox3 == null || pictureBox3.Image == null)
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
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness3.Value = i;
                                lblBrightness3.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status3 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage3.ResetText();
                rtbModelImage3.AppendText("yF");
                rtbModelImage3.Select(0, modelLot.Length);
                rtbModelImage3.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage3.AppendText("\r\n" + lastModelLot);
                rtbModelImage3.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage3.SelectionColor = Color.Black;

                rtbModelImage3.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness3.Value = 150;
                lblBrightness3.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox3.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status3 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage3.Text = "";
                rtbModelImage3.Visible = true;
                rtbModelImage3.AppendText("Not " + modelLot);
                rtbModelImage3.SelectionColor = Color.Red;

                status3 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


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
                    if (status1 == "NG" || status2 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status3 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

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
                lblResult.Location = new Point(900, 29);
                rtbModelImage3.Select(0, rtbModelImage3.Text.Length);
                rtbModelImage3.SelectionColor = Color.Red;
                rtbModelImage3.Select(rtbModelImage3.Text.Length, 0);
                rtbModelImage3.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture4_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 4;
                SnippingTool.AreaSelected += OnAreaSelected4;
                SnippingTool.Snip();
                rtbModelImage4.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected4(object sender, EventArgs e)
        {
            if (intCheck != 4)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox4.Image = newbmp;
            pictureBox4.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture4.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture4.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture4.Visible = false;

            int wid = oPicture4.Width;
            int hei = oPicture4.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture4.Height / 2 + 10;
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

            splitPicture41.Image = nb;
            splitPicture41.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture4.Width;
            hei = oPicture4.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture4.Height / 2), new Rectangle(0, oPicture4.Height / 2, wid, oPicture4.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture42.Image = nb;
            splitPicture42.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness4.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck4_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox4 == null || pictureBox4.Image == null)
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
                    rtbModelImage4.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox41.Image = AdjustBrightness(splitPicture41.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox41.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox41.Image = AdjustContrast((Bitmap)splitPicture41.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox41.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status4 = "OK";
                                rtbModelImage4.Visible = true;

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

                                rtbModelImage4.AppendText(first2string);
                                rtbModelImage4.Select(0, modelLot.Length);
                                rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage4.AppendText(secondstring.Trim());
                                rtbModelImage4.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage4.SelectionColor = Color.Black;

                                rtbModelImage4.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage4.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status4 = "OK";
                                rtbModelImage4.Visible = true;

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

                                rtbModelImage4.AppendText(first2string);
                                rtbModelImage4.Select(0, modelLot.Length);
                                rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage4.AppendText(secondstring.Trim());
                                rtbModelImage4.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage4.SelectionColor = Color.Black;

                                rtbModelImage4.AppendText(reststring);
                                rtbModelImage4.Select(modelLot.Length, reststring.Length);
                                rtbModelImage4.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status4 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status4 = "NG";
                lblBrightness4.Text = "Brightness = 1";
                scrBrightness4.Value = 100;
                rtbModelImage4.Visible = true;
                rtbModelImage4.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage4.BackColor = Color.Lavender;
                rtbModelImage4.ForeColor = Color.Red;
                pictureBox4.Image = AdjustBrightness(oPicture4.Image, (float)(scrBrightness4.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck4.Visible = true;
                btnReCheck4.PerformClick();
                btnReCheck4.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck4_Click(object sender, EventArgs e)
        {

            if (pictureBox4 == null || pictureBox4.Image == null)
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

                    rtbModelImage4.Text = "";

                    lblBrightness4.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox4.Image = AdjustBrightness(oPicture4.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox4.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage4.AppendText(first2string);
                            rtbModelImage4.Select(0, modelLot.Length);
                            rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage4.AppendText(reststring);
                            rtbModelImage4.Select(modelLot.Length, reststring.Length);
                            rtbModelImage4.SelectionColor = Color.Black;

                            rtbModelImage4.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness4.Value = i;
                                lblBrightness4.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status4 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage4.ResetText();
                rtbModelImage4.AppendText("yF");
                rtbModelImage4.Select(0, modelLot.Length);
                rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage4.AppendText("\r\n" + lastModelLot);
                rtbModelImage4.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage4.SelectionColor = Color.Black;

                rtbModelImage4.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness4.Value = 150;
                lblBrightness4.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox4.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status4 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage4.Text = "";
                rtbModelImage4.Visible = true;
                rtbModelImage4.AppendText("Not " + modelLot);
                rtbModelImage4.SelectionColor = Color.Red;

                status4 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness4.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox4.Image = AdjustBrightness(oPicture4.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage4.ReadOnly = false;
                }
                else
                {
                    rtbModelImage4.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage4_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage4.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage4.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status4 = "OK";

                    rtbModelImage4.Select(0, modelLength);
                    rtbModelImage4.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage4.Select(rtbModelImage4.Text.Length, 0);
                    rtbModelImage4.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status5 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status4 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage4.Select(0, rtbModelImage4.Text.Length);
                    rtbModelImage4.SelectionColor = Color.Red;
                    rtbModelImage4.Select(rtbModelImage4.Text.Length, 0);
                    rtbModelImage4.SelectionColor = Color.Black;
                }
            }
            else
            {
                status4 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage4.Select(0, rtbModelImage4.Text.Length);
                rtbModelImage4.SelectionColor = Color.Red;
                rtbModelImage4.Select(rtbModelImage4.Text.Length, 0);
                rtbModelImage4.SelectionColor = Color.Black;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture5_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 5;
                SnippingTool.AreaSelected += OnAreaSelected5;
                SnippingTool.Snip();
                rtbModelImage5.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected5(object sender, EventArgs e)
        {
            if (intCheck != 5)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox5.Image = newbmp;
            pictureBox5.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture5.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture5.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture5.Visible = false;

            int wid = oPicture5.Width;
            int hei = oPicture5.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture5.Height / 2 + 10;
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

            splitPicture51.Image = nb;
            splitPicture51.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture5.Width;
            hei = oPicture5.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture5.Height / 2), new Rectangle(0, oPicture5.Height / 2, wid, oPicture5.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture52.Image = nb;
            splitPicture52.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness5.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck5_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox5 == null || pictureBox5.Image == null)
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
                    rtbModelImage5.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox51.Image = AdjustBrightness(splitPicture51.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox51.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox51.Image = AdjustContrast((Bitmap)splitPicture51.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox51.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status5 = "OK";
                                rtbModelImage5.Visible = true;

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

                                rtbModelImage5.AppendText(first2string);
                                rtbModelImage5.Select(0, modelLot.Length);
                                rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage5.AppendText(secondstring.Trim());
                                rtbModelImage5.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage5.SelectionColor = Color.Black;

                                rtbModelImage5.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage5.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status5 = "OK";
                                rtbModelImage5.Visible = true;

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

                                rtbModelImage5.AppendText(first2string);
                                rtbModelImage5.Select(0, modelLot.Length);
                                rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage5.AppendText(secondstring.Trim());
                                rtbModelImage5.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage5.SelectionColor = Color.Black;

                                rtbModelImage5.AppendText(reststring);
                                rtbModelImage5.Select(modelLot.Length, reststring.Length);
                                rtbModelImage5.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status5 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status5 = "NG";
                lblBrightness5.Text = "Brightness = 1";
                scrBrightness5.Value = 100;
                rtbModelImage5.Visible = true;
                rtbModelImage5.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage5.BackColor = Color.Lavender;
                rtbModelImage5.ForeColor = Color.Red;
                pictureBox5.Image = AdjustBrightness(oPicture5.Image, (float)(scrBrightness5.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck5.Visible = true;
                btnReCheck5.PerformClick();
                btnReCheck5.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck5_Click(object sender, EventArgs e)
        {

            if (pictureBox5 == null || pictureBox5.Image == null)
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

                    rtbModelImage5.Text = "";

                    lblBrightness5.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox5.Image = AdjustBrightness(oPicture5.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox5.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage5.AppendText(first2string);
                            rtbModelImage5.Select(0, modelLot.Length);
                            rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage5.AppendText(reststring);
                            rtbModelImage5.Select(modelLot.Length, reststring.Length);
                            rtbModelImage5.SelectionColor = Color.Black;

                            rtbModelImage5.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness5.Value = i;
                                lblBrightness5.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status5 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage5.ResetText();
                rtbModelImage5.AppendText("yF");
                rtbModelImage5.Select(0, modelLot.Length);
                rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage5.AppendText("\r\n" + lastModelLot);
                rtbModelImage5.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage5.SelectionColor = Color.Black;

                rtbModelImage5.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness5.Value = 150;
                lblBrightness5.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox5.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status5 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage5.Text = "";
                rtbModelImage5.Visible = true;
                rtbModelImage5.AppendText("Not " + modelLot);
                rtbModelImage5.SelectionColor = Color.Red;

                status5 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness5.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox5.Image = AdjustBrightness(oPicture5.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage5.ReadOnly = false;
                }
                else
                {
                    rtbModelImage5.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage5_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage5.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage5.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status5 = "OK";

                    rtbModelImage5.Select(0, modelLength);
                    rtbModelImage5.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage5.Select(rtbModelImage5.Text.Length, 0);
                    rtbModelImage5.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status6 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status5 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage5.Select(0, rtbModelImage5.Text.Length);
                    rtbModelImage5.SelectionColor = Color.Red;
                    rtbModelImage5.Select(rtbModelImage5.Text.Length, 0);
                    rtbModelImage5.SelectionColor = Color.Black;
                }
            }
            else
            {
                status5 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage5.Select(0, rtbModelImage5.Text.Length);
                rtbModelImage5.SelectionColor = Color.Red;
                rtbModelImage5.Select(rtbModelImage5.Text.Length, 0);
                rtbModelImage5.SelectionColor = Color.Black;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture6_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 6;
                SnippingTool.AreaSelected += OnAreaSelected6;
                SnippingTool.Snip();
                rtbModelImage6.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected6(object sender, EventArgs e)
        {
            if (intCheck != 6)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox6.Image = newbmp;
            pictureBox6.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture6.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture6.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture6.Visible = false;

            int wid = oPicture6.Width;
            int hei = oPicture6.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture6.Height / 2 + 10;
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

            splitPicture61.Image = nb;
            splitPicture61.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture6.Width;
            hei = oPicture6.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture6.Height / 2), new Rectangle(0, oPicture6.Height / 2, wid, oPicture6.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture62.Image = nb;
            splitPicture62.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness6.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck6_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox6 == null || pictureBox6.Image == null)
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
                    rtbModelImage6.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox61.Image = AdjustBrightness(splitPicture61.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox61.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox61.Image = AdjustContrast((Bitmap)splitPicture61.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox61.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status6 = "OK";
                                rtbModelImage6.Visible = true;

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

                                rtbModelImage6.AppendText(first2string);
                                rtbModelImage6.Select(0, modelLot.Length);
                                rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage6.AppendText(secondstring.Trim());
                                rtbModelImage6.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage6.SelectionColor = Color.Black;

                                rtbModelImage6.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage6.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status6 = "OK";
                                rtbModelImage6.Visible = true;

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

                                rtbModelImage6.AppendText(first2string);
                                rtbModelImage6.Select(0, modelLot.Length);
                                rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage6.AppendText(secondstring.Trim());
                                rtbModelImage6.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage6.SelectionColor = Color.Black;

                                rtbModelImage6.AppendText(reststring);
                                rtbModelImage6.Select(modelLot.Length, reststring.Length);
                                rtbModelImage6.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status6 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status6 = "NG";
                lblBrightness6.Text = "Brightness = 1";
                scrBrightness6.Value = 100;
                rtbModelImage6.Visible = true;
                rtbModelImage6.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage6.BackColor = Color.Lavender;
                rtbModelImage6.ForeColor = Color.Red;
                pictureBox6.Image = AdjustBrightness(oPicture6.Image, (float)(scrBrightness6.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck6.Visible = true;
                btnReCheck6.PerformClick();
                btnReCheck6.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck6_Click(object sender, EventArgs e)
        {

            if (pictureBox6 == null || pictureBox6.Image == null)
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

                    rtbModelImage6.Text = "";

                    lblBrightness6.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox6.Image = AdjustBrightness(oPicture6.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox6.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage6.AppendText(first2string);
                            rtbModelImage6.Select(0, modelLot.Length);
                            rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage6.AppendText(reststring);
                            rtbModelImage6.Select(modelLot.Length, reststring.Length);
                            rtbModelImage6.SelectionColor = Color.Black;

                            rtbModelImage6.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness6.Value = i;
                                lblBrightness6.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status6 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage6.ResetText();
                rtbModelImage6.AppendText("yF");
                rtbModelImage6.Select(0, modelLot.Length);
                rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage6.AppendText("\r\n" + lastModelLot);
                rtbModelImage6.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage6.SelectionColor = Color.Black;

                rtbModelImage6.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness6.Value = 150;
                lblBrightness6.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox6.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status6 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage6.Text = "";
                rtbModelImage6.Visible = true;
                rtbModelImage6.AppendText("Not " + modelLot);
                rtbModelImage6.SelectionColor = Color.Red;

                status6 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness6.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox6.Image = AdjustBrightness(oPicture6.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage6.ReadOnly = false;
                }
                else
                {
                    rtbModelImage6.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage6_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage6.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage6.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status6 = "OK";

                    rtbModelImage6.Select(0, modelLength);
                    rtbModelImage6.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage6.Select(rtbModelImage6.Text.Length, 0);
                    rtbModelImage6.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status6 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage6.Select(0, rtbModelImage6.Text.Length);
                    rtbModelImage6.SelectionColor = Color.Red;
                    rtbModelImage6.Select(rtbModelImage6.Text.Length, 0);
                    rtbModelImage6.SelectionColor = Color.Black;
                }
            }
            else
            {
                status6 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage6.Select(0, rtbModelImage6.Text.Length);
                rtbModelImage6.SelectionColor = Color.Red;
                rtbModelImage6.Select(rtbModelImage6.Text.Length, 0);
                rtbModelImage6.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture7_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 7;
                SnippingTool.AreaSelected += OnAreaSelected7;
                SnippingTool.Snip();
                rtbModelImage7.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected7(object sender, EventArgs e)
        {
            if (intCheck != 7)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox7.Image = newbmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture7.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture7.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture7.Visible = false;

            int wid = oPicture7.Width;
            int hei = oPicture7.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture7.Height / 2 + 10;
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

            splitPicture71.Image = nb;
            splitPicture71.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture7.Width;
            hei = oPicture7.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture7.Height / 2), new Rectangle(0, oPicture7.Height / 2, wid, oPicture7.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture72.Image = nb;
            splitPicture72.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness7.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck7_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox7 == null || pictureBox7.Image == null)
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
                    rtbModelImage7.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox71.Image = AdjustBrightness(splitPicture71.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox71.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox71.Image = AdjustContrast((Bitmap)splitPicture71.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox71.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status7 = "OK";
                                rtbModelImage7.Visible = true;

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

                                rtbModelImage7.AppendText(first2string);
                                rtbModelImage7.Select(0, modelLot.Length);
                                rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage7.AppendText(secondstring.Trim());
                                rtbModelImage7.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage7.SelectionColor = Color.Black;

                                rtbModelImage7.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage7.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status7 = "OK";
                                rtbModelImage7.Visible = true;

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

                                rtbModelImage7.AppendText(first2string);
                                rtbModelImage7.Select(0, modelLot.Length);
                                rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage7.AppendText(secondstring.Trim());
                                rtbModelImage7.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage7.SelectionColor = Color.Black;

                                rtbModelImage7.AppendText(reststring);
                                rtbModelImage7.Select(modelLot.Length, reststring.Length);
                                rtbModelImage7.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status7 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status7 = "NG";
                lblBrightness7.Text = "Brightness = 1";
                scrBrightness7.Value = 100;
                rtbModelImage7.Visible = true;
                rtbModelImage7.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage7.BackColor = Color.Lavender;
                rtbModelImage7.ForeColor = Color.Red;
                pictureBox7.Image = AdjustBrightness(oPicture7.Image, (float)(scrBrightness7.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck7.Visible = true;
                btnReCheck7.PerformClick();
                btnReCheck7.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck7_Click(object sender, EventArgs e)
        {

            if (pictureBox7 == null || pictureBox7.Image == null)
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

                    rtbModelImage7.Text = "";

                    lblBrightness7.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox7.Image = AdjustBrightness(oPicture7.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox7.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage7.AppendText(first2string);
                            rtbModelImage7.Select(0, modelLot.Length);
                            rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage7.AppendText(reststring);
                            rtbModelImage7.Select(modelLot.Length, reststring.Length);
                            rtbModelImage7.SelectionColor = Color.Black;

                            rtbModelImage7.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness7.Value = i;
                                lblBrightness7.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status7 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage7.ResetText();
                rtbModelImage7.AppendText("yF");
                rtbModelImage7.Select(0, modelLot.Length);
                rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage7.AppendText("\r\n" + lastModelLot);
                rtbModelImage7.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage7.SelectionColor = Color.Black;

                rtbModelImage7.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness7.Value = 150;
                lblBrightness7.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox7.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status7 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage7.Text = "";
                rtbModelImage7.Visible = true;
                rtbModelImage7.AppendText("Not " + modelLot);
                rtbModelImage7.SelectionColor = Color.Red;

                status7 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness7.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox7.Image = AdjustBrightness(oPicture7.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage7.ReadOnly = false;
                }
                else
                {
                    rtbModelImage7.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage7_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage7.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage7.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status7 = "OK";

                    rtbModelImage7.Select(0, modelLength);
                    rtbModelImage7.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage7.Select(rtbModelImage7.Text.Length, 0);
                    rtbModelImage7.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status7 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage7.Select(0, rtbModelImage7.Text.Length);
                    rtbModelImage7.SelectionColor = Color.Red;
                    rtbModelImage7.Select(rtbModelImage7.Text.Length, 0);
                    rtbModelImage7.SelectionColor = Color.Black;
                }
            }
            else
            {
                status7 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage7.Select(0, rtbModelImage7.Text.Length);
                rtbModelImage7.SelectionColor = Color.Red;
                rtbModelImage7.Select(rtbModelImage7.Text.Length, 0);
                rtbModelImage7.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture8_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 8;
                SnippingTool.AreaSelected += OnAreaSelected8;
                SnippingTool.Snip();
                rtbModelImage8.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected8(object sender, EventArgs e)
        {
            if (intCheck != 8)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox8.Image = newbmp;
            pictureBox8.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture8.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture8.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture8.Visible = false;

            int wid = oPicture8.Width;
            int hei = oPicture8.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture8.Height / 2 + 10;
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

            splitPicture81.Image = nb;
            splitPicture81.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture8.Width;
            hei = oPicture8.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture8.Height / 2), new Rectangle(0, oPicture8.Height / 2, wid, oPicture8.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture82.Image = nb;
            splitPicture82.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness8.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck8_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox8 == null || pictureBox8.Image == null)
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
                    rtbModelImage8.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox81.Image = AdjustBrightness(splitPicture81.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox81.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox81.Image = AdjustContrast((Bitmap)splitPicture81.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox81.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status8 = "OK";
                                rtbModelImage8.Visible = true;

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

                                rtbModelImage8.AppendText(first2string);
                                rtbModelImage8.Select(0, modelLot.Length);
                                rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage8.AppendText(secondstring.Trim());
                                rtbModelImage8.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage8.SelectionColor = Color.Black;

                                rtbModelImage8.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage8.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status8 = "OK";
                                rtbModelImage8.Visible = true;

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

                                rtbModelImage8.AppendText(first2string);
                                rtbModelImage8.Select(0, modelLot.Length);
                                rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage8.AppendText(secondstring.Trim());
                                rtbModelImage8.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage8.SelectionColor = Color.Black;

                                rtbModelImage8.AppendText(reststring);
                                rtbModelImage8.Select(modelLot.Length, reststring.Length);
                                rtbModelImage8.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status8 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status8 = "NG";
                lblBrightness8.Text = "Brightness = 1";
                scrBrightness8.Value = 100;
                rtbModelImage8.Visible = true;
                rtbModelImage8.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage8.BackColor = Color.Lavender;
                rtbModelImage8.ForeColor = Color.Red;
                pictureBox8.Image = AdjustBrightness(oPicture8.Image, (float)(scrBrightness8.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck8.Visible = true;
                btnReCheck8.PerformClick();
                btnReCheck8.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck8_Click(object sender, EventArgs e)
        {

            if (pictureBox8 == null || pictureBox8.Image == null)
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

                    rtbModelImage8.Text = "";

                    lblBrightness8.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox8.Image = AdjustBrightness(oPicture8.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox8.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage8.AppendText(first2string);
                            rtbModelImage8.Select(0, modelLot.Length);
                            rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage8.AppendText(reststring);
                            rtbModelImage8.Select(modelLot.Length, reststring.Length);
                            rtbModelImage8.SelectionColor = Color.Black;

                            rtbModelImage8.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness8.Value = i;
                                lblBrightness8.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status8 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage8.ResetText();
                rtbModelImage8.AppendText("yF");
                rtbModelImage8.Select(0, modelLot.Length);
                rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage8.AppendText("\r\n" + lastModelLot);
                rtbModelImage8.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage8.SelectionColor = Color.Black;

                rtbModelImage8.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness8.Value = 150;
                lblBrightness8.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox8.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status8 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage8.Text = "";
                rtbModelImage8.Visible = true;
                rtbModelImage8.AppendText("Not " + modelLot);
                rtbModelImage8.SelectionColor = Color.Red;

                status8 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness8.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox8.Image = AdjustBrightness(oPicture8.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage8.ReadOnly = false;
                }
                else
                {
                    rtbModelImage8.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage8_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage8.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage8.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status8 = "OK";

                    rtbModelImage8.Select(0, modelLength);
                    rtbModelImage8.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage8.Select(rtbModelImage8.Text.Length, 0);
                    rtbModelImage8.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status8 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage8.Select(0, rtbModelImage8.Text.Length);
                    rtbModelImage8.SelectionColor = Color.Red;
                    rtbModelImage8.Select(rtbModelImage8.Text.Length, 0);
                    rtbModelImage8.SelectionColor = Color.Black;
                }
            }
            else
            {
                status8 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage8.Select(0, rtbModelImage8.Text.Length);
                rtbModelImage8.SelectionColor = Color.Red;
                rtbModelImage8.Select(rtbModelImage8.Text.Length, 0);
                rtbModelImage8.SelectionColor = Color.Black;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture9_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 9;
                SnippingTool.AreaSelected += OnAreaSelected9;
                SnippingTool.Snip();
                rtbModelImage9.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected9(object sender, EventArgs e)
        {
            if (intCheck != 9)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox9.Image = newbmp;
            pictureBox9.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture9.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture9.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture9.Visible = false;

            int wid = oPicture9.Width;
            int hei = oPicture9.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture9.Height / 2 + 10;
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

            splitPicture91.Image = nb;
            splitPicture91.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture9.Width;
            hei = oPicture9.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture9.Height / 2), new Rectangle(0, oPicture9.Height / 2, wid, oPicture9.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture92.Image = nb;
            splitPicture92.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness9.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck9_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox9 == null || pictureBox9.Image == null)
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
                    rtbModelImage9.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox91.Image = AdjustBrightness(splitPicture91.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox91.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox91.Image = AdjustContrast((Bitmap)splitPicture91.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox91.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status9 = "OK";
                                rtbModelImage9.Visible = true;

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

                                rtbModelImage9.AppendText(first2string);
                                rtbModelImage9.Select(0, modelLot.Length);
                                rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage9.AppendText(secondstring.Trim());
                                rtbModelImage9.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage9.SelectionColor = Color.Black;

                                rtbModelImage9.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage9.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status9 = "OK";
                                rtbModelImage9.Visible = true;

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

                                rtbModelImage9.AppendText(first2string);
                                rtbModelImage9.Select(0, modelLot.Length);
                                rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage9.AppendText(secondstring.Trim());
                                rtbModelImage9.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage9.SelectionColor = Color.Black;

                                rtbModelImage9.AppendText(reststring);
                                rtbModelImage9.Select(modelLot.Length, reststring.Length);
                                rtbModelImage9.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status9 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status9 = "NG";
                lblBrightness9.Text = "Brightness = 1";
                scrBrightness9.Value = 100;
                rtbModelImage9.Visible = true;
                rtbModelImage9.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage9.BackColor = Color.Lavender;
                rtbModelImage9.ForeColor = Color.Red;
                pictureBox9.Image = AdjustBrightness(oPicture9.Image, (float)(scrBrightness9.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck9.Visible = true;
                btnReCheck9.PerformClick();
                btnReCheck9.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck9_Click(object sender, EventArgs e)
        {

            if (pictureBox9 == null || pictureBox9.Image == null)
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

                    rtbModelImage9.Text = "";

                    lblBrightness9.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox9.Image = AdjustBrightness(oPicture9.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox9.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage9.AppendText(first2string);
                            rtbModelImage9.Select(0, modelLot.Length);
                            rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage9.AppendText(reststring);
                            rtbModelImage9.Select(modelLot.Length, reststring.Length);
                            rtbModelImage9.SelectionColor = Color.Black;

                            rtbModelImage9.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness9.Value = i;
                                lblBrightness9.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status9 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage9.ResetText();
                rtbModelImage9.AppendText("yF");
                rtbModelImage9.Select(0, modelLot.Length);
                rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage9.AppendText("\r\n" + lastModelLot);
                rtbModelImage9.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage9.SelectionColor = Color.Black;

                rtbModelImage9.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness9.Value = 150;
                lblBrightness9.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox9.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status9 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage9.Text = "";
                rtbModelImage9.Visible = true;
                rtbModelImage9.AppendText("Not " + modelLot);
                rtbModelImage9.SelectionColor = Color.Red;

                status9 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness9.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox9.Image = AdjustBrightness(oPicture9.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage9.ReadOnly = false;
                }
                else
                {
                    rtbModelImage9.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage9_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage9.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage9.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status9 = "OK";

                    rtbModelImage9.Select(0, modelLength);
                    rtbModelImage9.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage9.Select(rtbModelImage9.Text.Length, 0);
                    rtbModelImage9.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status9 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage9.Select(0, rtbModelImage9.Text.Length);
                    rtbModelImage9.SelectionColor = Color.Red;
                    rtbModelImage9.Select(rtbModelImage9.Text.Length, 0);
                    rtbModelImage9.SelectionColor = Color.Black;
                }
            }
            else
            {
                status9 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage9.Select(0, rtbModelImage9.Text.Length);
                rtbModelImage9.SelectionColor = Color.Red;
                rtbModelImage9.Select(rtbModelImage9.Text.Length, 0);
                rtbModelImage9.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void btnTakePicture10_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 10;
                SnippingTool.AreaSelected += OnAreaSelected10;
                SnippingTool.Snip();
                rtbModelImage10.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected10(object sender, EventArgs e)
        {
            if (intCheck != 10)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox10.Image = newbmp;
            pictureBox10.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture10.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture10.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture10.Visible = false;

            int wid = oPicture10.Width;
            int hei = oPicture10.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture10.Height / 2 + 10;
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

            splitPicture101.Image = nb;
            splitPicture101.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture10.Width;
            hei = oPicture10.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture10.Height / 2), new Rectangle(0, oPicture10.Height / 2, wid, oPicture10.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture102.Image = nb;
            splitPicture102.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness10.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck10_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox10 == null || pictureBox10.Image == null)
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
                    rtbModelImage10.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox101.Image = AdjustBrightness(splitPicture101.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox101.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox101.Image = AdjustContrast((Bitmap)splitPicture101.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox101.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status10 = "OK";
                                rtbModelImage10.Visible = true;

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

                                rtbModelImage10.AppendText(first2string);
                                rtbModelImage10.Select(0, modelLot.Length);
                                rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage10.AppendText(secondstring.Trim());
                                rtbModelImage10.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage10.SelectionColor = Color.Black;

                                rtbModelImage10.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage10.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status10 = "OK";
                                rtbModelImage10.Visible = true;

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

                                rtbModelImage10.AppendText(first2string);
                                rtbModelImage10.Select(0, modelLot.Length);
                                rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage10.AppendText(secondstring.Trim());
                                rtbModelImage10.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage10.SelectionColor = Color.Black;

                                rtbModelImage10.AppendText(reststring);
                                rtbModelImage10.Select(modelLot.Length, reststring.Length);
                                rtbModelImage10.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status10 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status10 = "NG";
                lblBrightness10.Text = "Brightness = 1";
                scrBrightness10.Value = 100;
                rtbModelImage10.Visible = true;
                rtbModelImage10.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage10.BackColor = Color.Lavender;
                rtbModelImage10.ForeColor = Color.Red;
                pictureBox10.Image = AdjustBrightness(oPicture10.Image, (float)(scrBrightness10.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck10.Visible = true;
                btnReCheck10.PerformClick();
                btnReCheck10.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck10_Click(object sender, EventArgs e)
        {

            if (pictureBox10 == null || pictureBox10.Image == null)
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

                    rtbModelImage10.Text = "";

                    lblBrightness10.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox10.Image = AdjustBrightness(oPicture10.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox10.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage10.AppendText(first2string);
                            rtbModelImage10.Select(0, modelLot.Length);
                            rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage10.AppendText(reststring);
                            rtbModelImage10.Select(modelLot.Length, reststring.Length);
                            rtbModelImage10.SelectionColor = Color.Black;

                            rtbModelImage10.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness10.Value = i;
                                lblBrightness10.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status10 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage10.ResetText();
                rtbModelImage10.AppendText("yF");
                rtbModelImage10.Select(0, modelLot.Length);
                rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage10.AppendText("\r\n" + lastModelLot);
                rtbModelImage10.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage10.SelectionColor = Color.Black;

                rtbModelImage10.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness10.Value = 150;
                lblBrightness10.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox10.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status10 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage10.Text = "";
                rtbModelImage10.Visible = true;
                rtbModelImage10.AppendText("Not " + modelLot);
                rtbModelImage10.SelectionColor = Color.Red;

                status10 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness10.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox10.Image = AdjustBrightness(oPicture10.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage10.ReadOnly = false;
                }
                else
                {
                    rtbModelImage10.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage10_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage10.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage10.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status10 = "OK";

                    rtbModelImage10.Select(0, modelLength);
                    rtbModelImage10.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage10.Select(rtbModelImage10.Text.Length, 0);
                    rtbModelImage10.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status11 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status10 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage10.Select(0, rtbModelImage10.Text.Length);
                    rtbModelImage10.SelectionColor = Color.Red;
                    rtbModelImage10.Select(rtbModelImage10.Text.Length, 0);
                    rtbModelImage10.SelectionColor = Color.Black;
                }
            }
            else
            {
                status10 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage10.Select(0, rtbModelImage10.Text.Length);
                rtbModelImage10.SelectionColor = Color.Red;
                rtbModelImage10.Select(rtbModelImage10.Text.Length, 0);
                rtbModelImage10.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture11_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 11;
                SnippingTool.AreaSelected += OnAreaSelected11;
                SnippingTool.Snip();
                rtbModelImage11.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected11(object sender, EventArgs e)
        {
            if (intCheck != 11)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox110.Image = newbmp;
            pictureBox110.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture11.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture11.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture11.Visible = false;

            int wid = oPicture11.Width;
            int hei = oPicture11.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture11.Height / 2 + 10;
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

            splitPicture111.Image = nb;
            splitPicture111.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture11.Width;
            hei = oPicture11.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture11.Height / 2), new Rectangle(0, oPicture11.Height / 2, wid, oPicture11.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture112.Image = nb;
            splitPicture112.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness11.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck11_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox110 == null || pictureBox110.Image == null)
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
                    rtbModelImage11.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox111.Image = AdjustBrightness(splitPicture111.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox111.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox111.Image = AdjustContrast((Bitmap)splitPicture111.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox111.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status11 = "OK";
                                rtbModelImage11.Visible = true;

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

                                rtbModelImage11.AppendText(first2string);
                                rtbModelImage11.Select(0, modelLot.Length);
                                rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage11.AppendText(secondstring.Trim());
                                rtbModelImage11.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage11.SelectionColor = Color.Black;

                                rtbModelImage11.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage11.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status11 = "OK";
                                rtbModelImage11.Visible = true;

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

                                rtbModelImage11.AppendText(first2string);
                                rtbModelImage11.Select(0, modelLot.Length);
                                rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage11.AppendText(secondstring.Trim());
                                rtbModelImage11.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage11.SelectionColor = Color.Black;

                                rtbModelImage11.AppendText(reststring);
                                rtbModelImage11.Select(modelLot.Length, reststring.Length);
                                rtbModelImage11.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status11 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status12 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status11 = "NG";
                lblBrightness11.Text = "Brightness = 1";
                scrBrightness11.Value = 100;
                rtbModelImage11.Visible = true;
                rtbModelImage11.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage11.BackColor = Color.Lavender;
                rtbModelImage11.ForeColor = Color.Red;
                pictureBox110.Image = AdjustBrightness(oPicture11.Image, (float)(scrBrightness11.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck11.Visible = true;
                btnReCheck11.PerformClick();
                btnReCheck11.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck11_Click(object sender, EventArgs e)
        {

            if (pictureBox110 == null || pictureBox110.Image == null)
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

                    rtbModelImage11.Text = "";

                    lblBrightness11.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox110.Image = AdjustBrightness(oPicture11.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox110.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage11.AppendText(first2string);
                            rtbModelImage11.Select(0, modelLot.Length);
                            rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage11.AppendText(reststring);
                            rtbModelImage11.Select(modelLot.Length, reststring.Length);
                            rtbModelImage11.SelectionColor = Color.Black;

                            rtbModelImage11.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness11.Value = i;
                                lblBrightness11.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status11 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage11.ResetText();
                rtbModelImage11.AppendText("yF");
                rtbModelImage11.Select(0, modelLot.Length);
                rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage11.AppendText("\r\n" + lastModelLot);
                rtbModelImage11.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage11.SelectionColor = Color.Black;

                rtbModelImage11.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness11.Value = 150;
                lblBrightness11.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox11.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status11 = "OK";
            }
            else if (!ch1)
            {
                rtbModelImage11.Text = "";
                rtbModelImage11.Visible = true;
                rtbModelImage11.AppendText("Not " + modelLot);
                rtbModelImage11.SelectionColor = Color.Red;

                status11 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness11.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox110.Image = AdjustBrightness(oPicture11.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage11.ReadOnly = false;
                }
                else
                {
                    rtbModelImage11.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage11_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage11.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage11.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status11 = "OK";

                    rtbModelImage11.Select(0, modelLength);
                    rtbModelImage11.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage11.Select(rtbModelImage11.Text.Length, 0);
                    rtbModelImage11.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status12 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status11 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage11.Select(0, rtbModelImage11.Text.Length);
                    rtbModelImage11.SelectionColor = Color.Red;
                    rtbModelImage11.Select(rtbModelImage11.Text.Length, 0);
                    rtbModelImage11.SelectionColor = Color.Black;
                }
            }
            else
            {
                status11 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage11.Select(0, rtbModelImage11.Text.Length);
                rtbModelImage11.SelectionColor = Color.Red;
                rtbModelImage11.Select(rtbModelImage11.Text.Length, 0);
                rtbModelImage11.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnTakePicture12_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(rtbModelLot.Text))
                {
                    MessageBox.Show("CHƯA CÓ THÔNG TIN MARKING CỦA LOT_ID.\r\n\r\nHÃY NHẬP LOT_ID TRƯỚC." +
                        "\r\n\r\nPLEASE ENTER LOT_ID FIRST.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                intCheck = 12;
                SnippingTool.AreaSelected += OnAreaSelected12;
                SnippingTool.Snip();
                rtbModelImage12.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnAreaSelected12(object sender, EventArgs e)
        {
            if (intCheck != 12)
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
            //        var averageValue = ((int)colorValue.R + (int)colorValue.B + (int)colorValue.G) / 4; // get the average for black and white
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


            pictureBox12.Image = newbmp;
            pictureBox12.SizeMode = PictureBoxSizeMode.AutoSize;

            oPicture12.Image = newbmp;// hqx.HqxSharp.Scale2(newbmp);
            oPicture12.SizeMode = PictureBoxSizeMode.AutoSize;
            oPicture12.Visible = false;

            int wid = oPicture12.Width;
            int hei = oPicture12.Height / 2 + 20;
            if (rtbModelLot.Text.Trim().ToUpper() == "FJ")
            {
                hei = oPicture12.Height / 2 + 10;
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

            splitPicture121.Image = nb;
            splitPicture121.SizeMode = PictureBoxSizeMode.AutoSize;


            wid = oPicture12.Width;
            hei = oPicture12.Height / 2;
            nb = new Bitmap(wid, hei);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(newbmp, new Rectangle(0, 0, wid, oPicture12.Height / 2), new Rectangle(0, oPicture12.Height / 2, wid, oPicture12.Height / 2), GraphicsUnit.Pixel);
            }

            splitPicture122.Image = nb;
            splitPicture122.SizeMode = PictureBoxSizeMode.AutoSize;


            lblBrightness12.Visible = true;

            //AdjustImage1();
            //btnCheck1.PerformClick();
            return;
        }

        private void btnCheck12_Click(object sender, EventArgs e)
        {
            bool check = false;
            if (pictureBox12 == null || pictureBox12.Image == null)
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
                    rtbModelImage12.Text = "";
                    string res1 = "";
                    string res2 = "";


                    pictureBox121.Image = AdjustBrightness(splitPicture121.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox121.Image, PageSegMode.AutoOnly))
                        res1 = page.GetText();


                    pictureBox121.Image = AdjustContrast((Bitmap)splitPicture121.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox121.Image, PageSegMode.AutoOnly))
                        res2 = page.GetText();


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
                                status12 = "OK";
                                rtbModelImage12.Visible = true;

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

                                rtbModelImage12.AppendText(first2string);
                                rtbModelImage12.Select(0, modelLot.Length);
                                rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage12.AppendText(secondstring.Trim());
                                rtbModelImage12.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage12.SelectionColor = Color.Black;

                                rtbModelImage12.AppendText(reststring);
                                //rtbModelImage1.Select(modelLot.Length, reststring.Length);
                                rtbModelImage12.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
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
                                status12 = "OK";
                                rtbModelImage12.Visible = true;

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

                                rtbModelImage12.AppendText(first2string);
                                rtbModelImage12.Select(0, modelLot.Length);
                                rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);

                                rtbModelImage12.AppendText(secondstring.Trim());
                                rtbModelImage12.Select(modelLot.Length, secondstring.Trim().Length);
                                rtbModelImage12.SelectionColor = Color.Black;

                                rtbModelImage12.AppendText(reststring);
                                rtbModelImage12.Select(modelLot.Length, reststring.Length);
                                rtbModelImage12.SelectionColor = Color.Black;

                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }

                                splashScreenManager1.CloseWaitForm();
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
                status12 = "OK";
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG")
                {
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                return;
            }
            else
            {
                status12 = "NG";
                lblBrightness12.Text = "Brightness = 1";
                scrBrightness12.Value = 100;
                rtbModelImage12.Visible = true;
                rtbModelImage12.Text = "Not\r\n" + "\'" + modelLot + "\'";
                rtbModelImage12.BackColor = Color.Lavender;
                rtbModelImage12.ForeColor = Color.Red;
                pictureBox12.Image = AdjustBrightness(oPicture12.Image, (float)(scrBrightness12.Value / 100.0));
                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);

                btnReCheck12.Visible = true;
                btnReCheck12.PerformClick();
                btnReCheck12.Visible = false;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnReCheck12_Click(object sender, EventArgs e)
        {

            if (pictureBox12 == null || pictureBox12.Image == null)
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

                    rtbModelImage12.Text = "";

                    lblBrightness12.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                    pictureBox12.Image = AdjustBrightness(oPicture12.Image, (float)(i * 1.0 / 100.0));

                    using (var page = engine.Process((Bitmap)pictureBox12.Image, PageSegMode.AutoOnly))
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
                            rtbModelImage12.AppendText(first2string);
                            rtbModelImage12.Select(0, modelLot.Length);
                            rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);

                            rtbModelImage12.AppendText(reststring);
                            rtbModelImage12.Select(modelLot.Length, reststring.Length);
                            rtbModelImage12.SelectionColor = Color.Black;

                            rtbModelImage12.Visible = true;
                            if (modelLot == res.TrimStart().Substring(0, modelLot.Length).Trim())
                            {
                                if (!radioButton2.Checked)
                                {
                                    lblResult.Visible = true;
                                    lblResult.Text = "OK";
                                    lblResult.ForeColor = Color.Green;
                                    lblResult.Location = new Point(900, 29);
                                }


                                scrBrightness12.Value = i;
                                lblBrightness12.Text = "Brightness = " + (i * 1.0 / 100.0).ToString();

                                status12 = "OK";
                                ch1 = true;
                                break;
                            }
                        }

                    }
                }
            }

            if (!ch1 && modelLot.Trim() == "yF")
            {
                rtbModelImage12.ResetText();
                rtbModelImage12.AppendText("yF");
                rtbModelImage12.Select(0, modelLot.Length);
                rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);

                rtbModelImage12.AppendText("\r\n" + lastModelLot);
                rtbModelImage12.Select(modelLot.Length, lastModelLot.Length);
                rtbModelImage12.SelectionColor = Color.Black;

                rtbModelImage12.Visible = true;

                if (!radioButton2.Checked)
                {
                    lblResult.Visible = true;
                    lblResult.Text = "OK";
                    lblResult.ForeColor = Color.Green;
                    lblResult.Location = new Point(900, 29);
                }


                scrBrightness12.Value = 150;
                lblBrightness12.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();
                pictureBox12.Image = AdjustBrightness(oPicture1.Image, (float)(150 * 1.0 / 100.0));
                status12 = "OK";
            }
            else if(!ch1)
            {
                rtbModelImage12.Text = "";
                rtbModelImage12.Visible = true;
                rtbModelImage12.AppendText("Not " + modelLot);
                rtbModelImage12.SelectionColor = Color.Red;

                status12 = "NG";
                lblResult.Text = "NG";
                lblResult.Visible = true;
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);


                lblBrightness12.Text = "Brightness = " + (150 * 1.0 / 100.0).ToString();

                pictureBox12.Image = AdjustBrightness(oPicture12.Image, (float)(150 * 1.0 / 100.0));

                if (rtbModelLot.Text == "yF" || rtbModelLot.Text == "RJ")
                {
                    rtbModelImage12.ReadOnly = false;
                }
                else
                {
                    rtbModelImage12.ReadOnly = true;
                }
            }
        }

        private void rtbModelImage12_TextChanged(object sender, EventArgs e)
        {
            int modelLength = rtbModelLot.Text.Length;
            if (rtbModelImage12.Text.TrimStart().Length >= modelLength)
            {
                if (rtbModelImage12.Text.TrimStart().Substring(0, modelLength).Trim() == rtbModelLot.Text)
                {
                    status12 = "OK";

                    rtbModelImage12.Select(0, modelLength);
                    rtbModelImage12.SelectionColor = Color.FromArgb(0, 120, 215);
                    rtbModelImage12.Select(rtbModelImage12.Text.Length, 0);
                    rtbModelImage12.SelectionColor = Color.Black;
                    if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG"
                    || status6 == "NG" || status7 == "NG" || status8 == "NG" || status9 == "NG" || status10 == "NG" || status11 == "NG")
                    {
                        lblResult.Text = "NG";
                        lblResult.ForeColor = Color.Red;
                        lblResult.Location = new Point(900, 29);
                    }
                    else
                    {
                        if (!radioButton2.Checked)
                        {
                            lblResult.Visible = true;
                            lblResult.Text = "OK";
                            lblResult.ForeColor = Color.Green;
                            lblResult.Location = new Point(900, 29);
                        }
                    }
                }
                else
                {
                    status12 = "NG";

                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);

                    rtbModelImage12.Select(0, rtbModelImage12.Text.Length);
                    rtbModelImage12.SelectionColor = Color.Red;
                    rtbModelImage12.Select(rtbModelImage12.Text.Length, 0);
                    rtbModelImage12.SelectionColor = Color.Black;
                }
            }
            else
            {
                status12 = "NG";

                lblResult.Text = "NG";
                lblResult.ForeColor = Color.Red;
                lblResult.Location = new Point(900, 29);
                rtbModelImage12.Select(0, rtbModelImage12.Text.Length);
                rtbModelImage12.SelectionColor = Color.Red;
                rtbModelImage12.Select(rtbModelImage12.Text.Length, 0);
                rtbModelImage12.SelectionColor = Color.Black;
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (status1 == "NG" || status2 == "NG" || status3 == "NG" || status4 == "NG" || status5 == "NG" || status6 == "NG" || status7 == "NG" || status8 == "NG" 
                    || status9 == "NG" || status10 == "NG" || status11 == "NG" || status12 == "NG")
                {
                    lblResult.Visible = true;
                    lblResult.Text = "NG";
                    lblResult.ForeColor = Color.Red;
                    lblResult.Location = new Point(900, 29);
                }
                else
                {
                    if (!radioButton2.Checked)
                    {
                        lblResult.Visible = true;
                        lblResult.Text = "OK";
                        lblResult.ForeColor = Color.Green;
                        lblResult.Location = new Point(900, 29);
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
                lblResult.Location = new Point(900, 29);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string create_user = USER;

            string process = "MARKING";


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
            if (pictureBox4.Image != null)
            {
                dt.Rows.Add(rtbModelImage4.Text.Trim(), scrBrightness4.Value.ToString(), status4, GetB64String(pictureBox4.Image));
            }
            if (pictureBox5.Image != null)
            {
                dt.Rows.Add(rtbModelImage5.Text.Trim(), scrBrightness5.Value.ToString(), status5, GetB64String(pictureBox5.Image));
            }
            if (pictureBox6.Image != null)
            {
                dt.Rows.Add(rtbModelImage6.Text.Trim(), scrBrightness6.Value.ToString(), status6, GetB64String(pictureBox6.Image));
            }
            if (pictureBox7.Image != null)
            {
                dt.Rows.Add(rtbModelImage7.Text.Trim(), scrBrightness7.Value.ToString(), status7, GetB64String(pictureBox7.Image));
            }
            if (pictureBox8.Image != null)
            {
                dt.Rows.Add(rtbModelImage8.Text.Trim(), scrBrightness8.Value.ToString(), status8, GetB64String(pictureBox8.Image));
            }
            // Mạnh bỏ check 4 picture cuối. *******************
            //if (pictureBox9.Image != null)
            //{
            //    dt.Rows.Add(rtbModelImage9.Text.Trim(), scrBrightness9.Value.ToString(), status9, GetB64String(pictureBox9.Image));
            //}
            //if (pictureBox10.Image != null)
            //{
            //    dt.Rows.Add(rtbModelImage10.Text.Trim(), scrBrightness10.Value.ToString(), status10, GetB64String(pictureBox10.Image));
            //}
            //if (pictureBox110.Image != null)
            //{
            //    dt.Rows.Add(rtbModelImage11.Text.Trim(), scrBrightness11.Value.ToString(), status11, GetB64String(pictureBox110.Image));
            //}
            //if (pictureBox12.Image != null)
            //{
            //    dt.Rows.Add(rtbModelImage12.Text.Trim(), scrBrightness12.Value.ToString(), status12, GetB64String(pictureBox12.Image));
            //}
            // ************************************************


            //if (dt.Rows.Count < 12)
            //{
            //    MessageBox.Show("CHƯA ĐỦ SỐ LƯỢNG 12 ẢNH." +
            //        "\r\n\r\nPLEASE TAKE FULL OF 12 IMAGES.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (dt.Rows.Count < 8)
            {
                MessageBox.Show("CHƯA ĐỦ SỐ LƯỢNG 12 ẢNH." +
                    "\r\n\r\nPLEASE TAKE FULL OF 12 IMAGES.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //if (status1 == "NOTCHECK" || status2 == "NOTCHECK" || status3 == "NOTCHECK" || status4 == "NOTCHECK" || status5 == "NOTCHECK" || status6 == "NOTCHECK" || status7 == "NOTCHECK"
            //     || status8 == "NOTCHECK" || status9 == "NOTCHECK" || status10 == "NOTCHECK" || status11 == "NOTCHECK" || status12 == "NOTCHECK")
            //{
            //    MessageBox.Show("CHƯA CHECK HẾT" +
            //        "\r\n\r\nPLEASE CHECK ALL.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (status1 == "NOTCHECK" || status2 == "NOTCHECK" || status3 == "NOTCHECK" || status4 == "NOTCHECK" || status5 == "NOTCHECK" || status6 == "NOTCHECK" || status7 == "NOTCHECK"
                || status8 == "NOTCHECK")
            {
                MessageBox.Show("CHƯA CHECK HẾT" +
                    "\r\n\r\nPLEASE CHECK ALL.", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!radioButton1.Checked && !radioButton2.Checked)
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

            pcIP = GetPCInfo();
            pcName = GetIp();

            try
            {

                //string connString = "Data Source = 10.70.10.97;Initial Catalog = MESDB;User Id = sa;Password = Wisol@123;Connect Timeout=3";
                string connString = "Data Source = 10.70.21.214;Initial Catalog = MESDB;User Id = mesother;Password = othermes;Connect Timeout=3";
                SqlConnection conn = new SqlConnection(connString);


                SqlCommand cmd = new SqlCommand("SP_INSERT_RESULT_08", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@A_LOT_NO", SqlDbType.VarChar).Value = lot_no;
                cmd.Parameters.Add("@A_MODEL", SqlDbType.VarChar).Value = Model;
                cmd.Parameters.Add("@A_MODEL_LOT", SqlDbType.VarChar).Value = modelLot;
                cmd.Parameters.Add("@A_MODEL_IMAGE1", SqlDbType.VarChar).Value = rtbModelImage1.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE2", SqlDbType.VarChar).Value = rtbModelImage2.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE3", SqlDbType.VarChar).Value = rtbModelImage3.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE4", SqlDbType.VarChar).Value = rtbModelImage4.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE5", SqlDbType.VarChar).Value = rtbModelImage5.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE6", SqlDbType.VarChar).Value = rtbModelImage6.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE7", SqlDbType.VarChar).Value = rtbModelImage7.Text.Trim();
                cmd.Parameters.Add("@A_MODEL_IMAGE8", SqlDbType.VarChar).Value = rtbModelImage8.Text.Trim();
                //cmd.Parameters.Add("@A_MODEL_IMAGE9", SqlDbType.VarChar).Value = rtbModelImage9.Text.Trim();
                //cmd.Parameters.Add("@A_MODEL_IMAGE10", SqlDbType.VarChar).Value = rtbModelImage10.Text.Trim();
                //cmd.Parameters.Add("@A_MODEL_IMAGE11", SqlDbType.VarChar).Value = rtbModelImage11.Text.Trim();
                //cmd.Parameters.Add("@A_MODEL_IMAGE12", SqlDbType.VarChar).Value = rtbModelImage12.Text.Trim();

                cmd.Parameters.Add("@A_BRIGHTNESS1", SqlDbType.VarChar).Value = scrBrightness1.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS2", SqlDbType.VarChar).Value = scrBrightness2.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS3", SqlDbType.VarChar).Value = scrBrightness3.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS4", SqlDbType.VarChar).Value = scrBrightness4.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS5", SqlDbType.VarChar).Value = scrBrightness5.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS6", SqlDbType.VarChar).Value = scrBrightness6.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS7", SqlDbType.VarChar).Value = scrBrightness7.Value.ToString();
                cmd.Parameters.Add("@A_BRIGHTNESS8", SqlDbType.VarChar).Value = scrBrightness8.Value.ToString();
                //cmd.Parameters.Add("@A_BRIGHTNESS9", SqlDbType.VarChar).Value = scrBrightness9.Value.ToString();
                //cmd.Parameters.Add("@A_BRIGHTNESS10", SqlDbType.VarChar).Value = scrBrightness10.Value.ToString();
                //cmd.Parameters.Add("@A_BRIGHTNESS11", SqlDbType.VarChar).Value = scrBrightness11.Value.ToString();
                //cmd.Parameters.Add("@A_BRIGHTNESS12", SqlDbType.VarChar).Value = scrBrightness12.Value.ToString();

                cmd.Parameters.Add("@A_RESULT1", SqlDbType.VarChar).Value = status1;
                cmd.Parameters.Add("@A_RESULT2", SqlDbType.VarChar).Value = status2;
                cmd.Parameters.Add("@A_RESULT3", SqlDbType.VarChar).Value = status3;
                cmd.Parameters.Add("@A_RESULT4", SqlDbType.VarChar).Value = status4;
                cmd.Parameters.Add("@A_RESULT5", SqlDbType.VarChar).Value = status5;
                cmd.Parameters.Add("@A_RESULT6", SqlDbType.VarChar).Value = status6;
                cmd.Parameters.Add("@A_RESULT7", SqlDbType.VarChar).Value = status7;
                cmd.Parameters.Add("@A_RESULT8", SqlDbType.VarChar).Value = status8;
                //cmd.Parameters.Add("@A_RESULT9", SqlDbType.VarChar).Value = status9;
                //cmd.Parameters.Add("@A_RESULT10", SqlDbType.VarChar).Value = status10;
                //cmd.Parameters.Add("@A_RESULT11", SqlDbType.VarChar).Value = status11;
                //cmd.Parameters.Add("@A_RESULT12", SqlDbType.VarChar).Value = status12;


                cmd.Parameters.Add("@A_IMAGE_BASE641", SqlDbType.VarChar).Value = GetB64String(pictureBox1.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE642", SqlDbType.VarChar).Value = GetB64String(pictureBox2.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE643", SqlDbType.VarChar).Value = GetB64String(pictureBox3.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE644", SqlDbType.VarChar).Value = GetB64String(pictureBox4.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE645", SqlDbType.VarChar).Value = GetB64String(pictureBox5.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE646", SqlDbType.VarChar).Value = GetB64String(pictureBox6.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE647", SqlDbType.VarChar).Value = GetB64String(pictureBox7.Image);
                cmd.Parameters.Add("@A_IMAGE_BASE648", SqlDbType.VarChar).Value = GetB64String(pictureBox8.Image);
                //cmd.Parameters.Add("@A_IMAGE_BASE649", SqlDbType.VarChar).Value = GetB64String(pictureBox9.Image);
                //cmd.Parameters.Add("@A_IMAGE_BASE6410", SqlDbType.VarChar).Value = GetB64String(pictureBox10.Image);
                //cmd.Parameters.Add("@A_IMAGE_BASE6411", SqlDbType.VarChar).Value = GetB64String(pictureBox110.Image);
                //cmd.Parameters.Add("@A_IMAGE_BASE6412", SqlDbType.VarChar).Value = GetB64String(pictureBox12.Image);

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



                MessageBox.Show("       SAVE OK!       ", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblLot.Text = string.Empty;
                lblModel.Text = string.Empty;
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                pictureBox5.Image = null;
                pictureBox6.Image = null;
                pictureBox7.Image = null;
                pictureBox8.Image = null;
                //pictureBox9.Image = null;
                //pictureBox10.Image = null;
                //pictureBox110.Image = null;
                //pictureBox12.Image = null;

                rtbModelImage1.Text = string.Empty;
                rtbModelImage2.Text = string.Empty;
                rtbModelImage3.Text = string.Empty;
                rtbModelImage4.Text = string.Empty;
                rtbModelImage5.Text = string.Empty;
                rtbModelImage6.Text = string.Empty;
                rtbModelImage7.Text = string.Empty;
                rtbModelImage8.Text = string.Empty;
                //rtbModelImage9.Text = string.Empty;
                //rtbModelImage10.Text = string.Empty;
                //rtbModelImage11.Text = string.Empty;
                //rtbModelImage12.Text = string.Empty;

                status1 = "NOTCHECK";
                status2 = "NOTCHECK";
                status3 = "NOTCHECK";
                status4 = "NOTCHECK";
                status5 = "NOTCHECK";
                status6 = "NOTCHECK";
                status7 = "NOTCHECK";
                status8 = "NOTCHECK";
                //status9 = "NOTCHECK";
                //status10 = "NOTCHECK";
                //status11 = "NOTCHECK";
                //status12 = "NOTCHECK";

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
    }
}
