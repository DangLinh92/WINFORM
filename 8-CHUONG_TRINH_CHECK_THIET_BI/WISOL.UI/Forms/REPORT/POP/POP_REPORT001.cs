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
                    //imageSlider1.Images.Add(Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url));
                    Image img = Image.FromFile(@"\\10.70.21.236\Audit_Share\PI_LUAN\APP_IMAGE\UTILITY_IMAGE\" + url);

                    if (i > 0)
                    {
                        var _exifOrientation = (int)img.GetPropertyItem(0x0112).Value[0];
                        img.RotateFlip(GetOrientationToFlipType(_exifOrientation));
                    }
                    imageSlider1.Images.Add(img);
                }

                //imageSlider1.CurrentImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                //imageSlider1.Refresh();

                var _exifOrientation1 = (int)imageSlider1.CurrentImage.GetPropertyItem(0x0112).Value[0];
                imageSlider1.CurrentImage.RotateFlip(GetOrientationToFlipType(_exifOrientation1));
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

        private RotateFlipType GetOrientationToFlipType(int orientationValue)
        {
            RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;
            switch (orientationValue)
            {
                case 1:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
                case 2:
                    rotateFlipType = RotateFlipType.RotateNoneFlipX;
                    break;
                case 3:
                    rotateFlipType = RotateFlipType.Rotate180FlipNone;
                    break;
                case 4:
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    break;
                case 5:
                    rotateFlipType = RotateFlipType.Rotate90FlipX;
                    break;
                case 6:
                    rotateFlipType = RotateFlipType.Rotate90FlipNone;
                    break;
                case 7:
                    rotateFlipType = RotateFlipType.Rotate270FlipX;
                    break;
                case 8:
                    rotateFlipType = RotateFlipType.Rotate270FlipNone;
                    break;
                default:
                    rotateFlipType = RotateFlipType.RotateNoneFlipNone;
                    break;
            }
            return rotateFlipType;
        }

    }
}
