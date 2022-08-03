using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT003_NEW : FormType
    {
        DataTable dtCode = new DataTable();
        DataTable dtItem = new DataTable();

        public POP_REPORT003_NEW()
        {
            InitializeComponent();
        }
        public POP_REPORT003_NEW(string input)
        {
            InitializeComponent();
            try
            {

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT003_NEW.INT_LIST"
                    , new string[] { "A_PLANT" ,
                    "A_DEPARTMENT",
                    "A_TRAN_USER",
                    "A_LANG"
                    }
                    , new string[] { Consts.PLANT ,
                    "",
                    Consts.USER_INFO.Id,
                    Consts.USER_INFO.Language
                    }
                    ); 
                if (base.mResultDB.ReturnInt == 0)
                {
                    dtCode = base.mResultDB.ReturnDataSet.Tables[0].Copy();
                    dtItem = base.mResultDB.ReturnDataSet.Tables[1].Copy();

                    base.mBindData.BindGridLookEdit(gleCode, dtCode, "DEVICE_NAME", "CODE");

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
            try
            {
                string datetime = dtpDateTime.Text.ToString();
                if (string.IsNullOrEmpty(gleCode.Text))
                {
                    MsgBox.Show("Code thiết bị không được để trống", MsgType.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(gleItem.Text))
                {
                    MsgBox.Show("Item không được để trống", MsgType.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(dtpDateTime.Text))
                {
                    MsgBox.Show("Nhập ngày bảo trì.", MsgType.Warning);
                    return;
                }
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


            
                string time = dtpDateTime.DateTime.ToString("yyyy-MM-dd");
                int year = Convert.ToInt32(time.Substring(0, 4));
                int month = Convert.ToInt32(time.Substring(5, 2));
                int day = Convert.ToInt32(time.Substring(8, 2));
                string maintenance_id = gleItem.EditValue.ToString();

                base.mResultDB = base.mDBaccess.ExcuteProc("PKG_REPORT003_NEW.POP_SAVE"
                    , new string[] { "A_PLANT" ,
                        "A_DEPARTMENT",
                        "A_TRAN_USER",
                        "A_LANG",
                        "A_MAINTENANCE_ID",
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
                        maintenance_id,
                        count.ToString(),
                        memoNote.EditValue.ToString(),
                        year.ToString(),
                        month.ToString(),
                        day.ToString()
                    }
                    ); ;

                if (base.mResultDB.ReturnInt == 0)
                {
                    string incidentID = base.mResultDB.ReturnDataSet.Tables[0].Rows[0][0].ToString();
                    string add = "\\\\10.70.21.236\\Audit_Share\\PI_LUAN\\APP_IMAGE\\UTILITY_IMAGE\\" + year + "\\" + month + "\\" + day + "\\" + "fileAfter\\IncidentReportID" + incidentID;
                    System.IO.Directory.CreateDirectory(add);

                    int seed = 0;
                    if (pictureEdit1.EditValue != null)
                    {
                        seed += 1;
                        string add1 = add + "\\" + seed.ToString() + ".jpeg";
                        Bitmap b1 = new Bitmap(pictureEdit1.Image);
                        Image img1 = (Image)b1;
                        img1.Save(add1, System.Drawing.Imaging.ImageFormat.Jpeg);
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

        private void gleCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string device_name = gleCode.EditValue.ToString();
                txtDeviceName.Text = device_name;

                string code = gleCode.Text;

                if (device_name == "")
                {
                    return;
                }

                var dr = from row in dtItem.AsEnumerable()
                         where row.Field<string>("CODE") == code
                         select row;

                DataTable dt_S = dr.CopyToDataTable();
                base.mBindData.BindGridLookEdit(gleItem, dt_S, "ID", "ITEM");
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }
        }
    }
}
