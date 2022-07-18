using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT003 : FormType
    {
        string incidentID = string.Empty;
        public POP_REPORT003()
        {
            InitializeComponent();
            //Init_Control();
        }

        public POP_REPORT003(string code, string name, string item, string note, string _incidentID, string time_check)
        {
            InitializeComponent();
            txtCode.Text = code;
            txtDeviceName.Text = name;
            txtItem.Text = item;
            memoNote.Text = note;
            incidentID = _incidentID;
            dtpDateTime.EditValue = time_check;
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT003.GET_PICTURE"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ID"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        _incidentID
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    if (base.mResultDB.ReturnDataSet.Tables[0].Rows.Count > 0) {
                        for (int i = 0; i < base.mResultDB.ReturnDataSet.Tables[0].Rows.Count; i++)
                        {
                            string url = string.Empty;
                            if (i == 0)
                            {
                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");
                                //pictureEdit1.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                //pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit1.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                //pictureBox1.Image = new Bitmap(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                //pictureBox1.Image = Image.FromStream(ms);
                                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                //pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                pictureEdit1.Image = Image.FromStream(ms);
                                pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit1.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                string n1 = url.Substring(url.LastIndexOf("\\") + 1);
                                n1 = n1.Substring(0, n1.IndexOf("."));
                                if (n1.Length > 3)
                                {
                                    pictureEdit1.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                            if (i == 1)
                            {
                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");
                                //pictureEdit2.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);

                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                pictureEdit2.Image = Image.FromStream(ms);
                                pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit2.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                string n2 = url.Substring(url.LastIndexOf("\\") + 1);
                                n2 = n2.Substring(0, n2.IndexOf("."));
                                if (n2.Length > 3)
                                {
                                    pictureEdit2.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                            if (i == 2)
                            {
                                //url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                //url = url.Substring(23);
                                //url = url.Replace("/", @"\");
                                //pictureEdit3.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                //pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");

                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                pictureEdit3.Image = Image.FromStream(ms);
                                pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

                                string n3 = url.Substring(url.LastIndexOf("\\") + 1);
                                n3 = n3.Substring(0, n3.IndexOf("."));
                                if (n3.Length > 3)
                                {
                                    pictureEdit3.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                            if (i == 3)
                            {
                                //url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                //url = url.Substring(23);
                                //url = url.Replace("/", @"\");
                                //pictureEdit4.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                //pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit4.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");

                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                pictureEdit4.Image = Image.FromStream(ms);
                                pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

                                string n4 = url.Substring(url.LastIndexOf("\\") + 1);
                                n4 = n4.Substring(0, n4.IndexOf("."));
                                if (n4.Length > 3)
                                {
                                    pictureEdit4.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                            if (i == 4)
                            {
                                //url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                //url = url.Substring(23);
                                //url = url.Replace("/", @"\");
                                //pictureEdit5.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                //pictureEdit5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit5.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");

                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                pictureEdit5.Image = Image.FromStream(ms);
                                pictureEdit5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

                                string n5 = url.Substring(url.LastIndexOf("\\") + 1);
                                n5 = n5.Substring(0, n5.IndexOf("."));
                                if (n5.Length > 3)
                                {
                                    pictureEdit5.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                            if (i == 5)
                            {
                                //url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                //url = url.Substring(23);
                                //url = url.Replace("/", @"\");
                                //pictureEdit6.Image = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                //pictureEdit6.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                                //pictureEdit6.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);

                                url = base.mResultDB.ReturnDataSet.Tables[0].Rows[i][0].ToString();
                                url = url.Substring(23);
                                url = url.Replace("/", @"\");

                                byte[] bytes = System.IO.File.ReadAllBytes(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                                pictureEdit6.Image = Image.FromStream(ms);
                                pictureEdit6.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

                                string n6 = url.Substring(url.LastIndexOf("\\") + 1);
                                n6 = n6.Substring(0, n6.IndexOf("."));
                                if (n6.Length > 3)
                                {
                                    pictureEdit6.Image.RotateFlip(RotateFlipType.Rotate270FlipXY);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }

        string b1 = string.Empty;
        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                b1 = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
                pictureEdit1.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btnDelete1_Click(object sender, EventArgs e)
        {
            pictureEdit1.Image = null;
        }

        private void btnDelete2_Click(object sender, EventArgs e)
        {
            pictureEdit2.Image = null;
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit2.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btndelete3_Click(object sender, EventArgs e)
        {
            pictureEdit3.Image = null;
        }

        private void btnBrowse3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit3.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btnDelete4_Click(object sender, EventArgs e)
        {
            pictureEdit4.Image = null;
        }

        private void btnBrowse4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit4.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btnDelete5_Click(object sender, EventArgs e)
        {
            pictureEdit5.Image = null;
        }

        private void btnBrowse5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit5.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btnDelete6_Click(object sender, EventArgs e)
        {
            pictureEdit6.Image = null;
        }

        private void btnBrowse6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit6.Image = Image.FromFile(openFileDialog.FileName);
                pictureEdit6.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            if(pictureEdit1.EditValue != null)
            {
                count += 1;
            }
            if (pictureEdit2.EditValue != null)
            {
                count += 1;
            }
            if (pictureEdit3.EditValue != null)
            {
                count += 1;
            }
            if (pictureEdit4.EditValue != null)
            {
                count += 1;
            }
            if (pictureEdit5.EditValue != null)
            {
                count += 1;
            }
            if (pictureEdit6.EditValue != null)
            {
                count += 1;
            }


            try
            {
                string time = dtpDateTime.DateTime.ToString("yyyy-MM-dd");
                int year = Convert.ToInt32(time.Substring(0, 4));
                int month = Convert.ToInt32(time.Substring(5, 2));
                int day = Convert.ToInt32(time.Substring(8, 2));

                string add = "\\\\10.70.21.236\\Audit_Share\\PI_LUAN\\APP_IMAGE\\UTILITY_IMAGE\\" + year + "\\" + month + "\\" + day + "\\" + "fileAfter\\IncidentReportID" + incidentID;
                System.IO.Directory.CreateDirectory(add);

                System.IO.File.Delete(add + "\\1.jpeg");
                System.IO.File.Delete(add + "\\2.jpeg");
                System.IO.File.Delete(add + "\\3.jpeg");
                System.IO.File.Delete(add + "\\4.jpeg");
                System.IO.File.Delete(add + "\\5.jpeg");
                System.IO.File.Delete(add + "\\6.jpeg");

                int seed = 0;

                if (pictureEdit1.EditValue != null)
                {
                    seed += 1;
                    string add1 = add + "\\" + seed.ToString() +  ".jpeg";
                    //sring add1 = \\10.70.21.236\AppUpdate\Luan_PI\run
                    //Image copy1 = pictureEdit1.Image;

                    //copy1.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //pictureBox1.Image.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Bitmap b1 = new Bitmap(pictureEdit1.Image);
                    Image img1 = (Image)b1;
                    img1.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //bitmap.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (pictureEdit2.EditValue != null)
                {
                    seed += 1;
                    string add2 = add + "\\" + seed.ToString() + ".jpeg";
                    Bitmap b2 = new Bitmap(pictureEdit2.Image);
                    Image img2 = (Image)b2;
                    img2.Save(add2, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (pictureEdit3.EditValue != null)
                {
                    seed += 1;
                    string add3 = add + "\\" + seed.ToString() + ".jpeg";
                    Bitmap b3 = new Bitmap(pictureEdit3.Image);
                    Image img3 = (Image)b3;
                    img3.Save(add3, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (pictureEdit4.EditValue != null)
                {
                    seed += 1;
                    string add4 = add + "\\" + seed.ToString() + ".jpeg";
                    Bitmap b4 = new Bitmap(pictureEdit4.Image);
                    Image img4 = (Image)b4;
                    img4.Save(add4, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (pictureEdit5.EditValue != null)
                {
                    seed += 1;
                    string add5 = add + "\\" + seed.ToString() + ".jpeg";
                    Bitmap b5 = new Bitmap(pictureEdit5.Image);
                    Image img5 = (Image)b5;
                    img5.Save(add5, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                if (pictureEdit6.EditValue != null)
                {
                    seed += 1;
                    string add6 = add + "\\" + seed.ToString() + ".jpeg";
                    Bitmap b6 = new Bitmap(pictureEdit6.Image);
                    Image img6 = (Image)b6;
                    img6.Save(add6, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT003.POP_SAVE"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_ID",
                        "A_COUNT",
                        "A_NOTE",
                        "A_YEAR",
                        "A_MONTH",
                        "A_DAY"
                    }
                    , new string[] { Consts.PLANT ,
                        "",
                        Consts.USER_INFO.Id,
                        Consts.USER_INFO.Language,
                        incidentID,
                        count.ToString(),
                        memoNote.EditValue.ToString(),
                        year.ToString(),
                        month.ToString(),
                        day.ToString()
                    }
                    ); ;
                if (base.mResultDB.ReturnInt == 0)
                {
                    //int year = Convert.ToInt32(time.Substring(0, 4)) + 2;
                    //int month = Convert.ToInt32(time.Substring(5, 2));
                    //int day = Convert.ToInt32(time.Substring(8, 2));

                    //if(pictureEdit1.EditValue != null)
                    //{
                    //    string add1 = "\\10.70.21.236\\Audit_Share\\PI_LUAN\\APP_IMAGE\\UTILITY_IMAGE\\" + year + "\\" + month + "\\" + day + "\\" + "fileAfter\\IncidentReportID" + incidentID + "\\1.jpeg";
                    //    Image copy1 = pictureEdit1.Image;
                    //    copy1.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //}
                    //if (pictureEdit2.EditValue != null)
                    //{
                    //    string add2 = "\\10.70.21.236\\Audit_Share\\PI_LUAN\\APP_IMAGE\\UTILITY_IMAGE\\" + year + "\\" + month + "\\" + day + "\\" + "fileAfter\\IncidentReportID" + incidentID + "\\2.jpeg";
                    //    Image copy2 = pictureEdit2.Image;
                    //    copy2.Save(add2, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //}
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Information);
                    this.Close();
                }
                else
                {
                    MsgBox.Show(base.mResultDB.ReturnString.Translation(), MsgType.Warning);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
