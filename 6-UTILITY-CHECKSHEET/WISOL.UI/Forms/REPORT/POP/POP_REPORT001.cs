using System;
using System.Data;
using System.Drawing;
using Wisol.Components;

using Wisol.MES.Inherit;

namespace Wisol.MES.Forms.REPORT.POP
{
    public partial class POP_REPORT001 : FormType
    {

        string feeder = string.Empty;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public POP_REPORT001()
        {
            InitializeComponent();
            //Init_Control();
        }

        public POP_REPORT001(DataTable dt_url)
        {
            InitializeComponent();

            Init_Control(dt_url);
        }

        private void Init_Control(DataTable dt_url)
        {
            try
            {
                for (int i = 0; i < dt_url.Rows.Count; i++)
                {
                    string url = dt_url.Rows[i][0].ToString();
                    url = url.Substring(23);
                    url = url.Replace("/", @"\");
                    imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                }

                imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                imageSlider1.Refresh();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, MsgType.Error);
            }

            //this.Text = "abc";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (imageSlider1.Images.Count > 0)
            {
                imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                imageSlider1.Refresh();
            }
        }
    }
}
