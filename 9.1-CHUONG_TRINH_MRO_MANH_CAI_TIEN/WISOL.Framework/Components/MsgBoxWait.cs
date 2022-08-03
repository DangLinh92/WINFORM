using DevExpress.XtraSplashScreen;
using System.Windows.Forms;

namespace Wisol.Components
{
    public class MsgBoxWait
    {
        public static SplashScreenManager splashForm = null;
        public static void Show(Form parentForm)
        {
            splashForm = new SplashScreenManager(parentForm, typeof(FrmWaitForm), false, false);

            splashForm.ShowWaitForm();
        }

        public static void Show(UserControl parentControl)
        {
            splashForm = new SplashScreenManager(parentControl.FindForm(), typeof(FrmWaitForm), false, false);

            splashForm.ShowWaitForm();
        }

        public static void Close()
        {
            splashForm.CloseWaitForm();
        }
    }
}
