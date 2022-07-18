using System.Windows.Forms;

namespace Wisol.Components
{
    public class MsgBox
    {
        public static void Show(string _msg)
        {
            MsgType1 msgType1 = new MsgType1(_msg);

            msgType1.ShowDialog();
        }

        public static DialogResult Show(string _msg, MsgType msgType)
        {
            MsgType1 msgType1 = new MsgType1(_msg, msgType);

            msgType1.ShowDialog();

            return DialogResult.OK;
        }

        public static DialogResult Show(string _msg, MsgType msgType, DialogType dlgType)
        {
            MsgType1 msgType1 = null;
            MsgType2 msgType2 = null;
            switch (dlgType)
            {
                case DialogType.OK:
                    msgType1 = new MsgType1(_msg, msgType);
                    msgType1.ShowDialog();
                    return DialogResult.OK;
                case DialogType.OkCancel:
                    msgType2 = new MsgType2(_msg, msgType);
                    msgType2.ShowDialog();
                    return msgType2.DialogResult;
            }
            return DialogResult.OK;
        }
    }
    public enum MsgType
    {
        Information,
        Warning,
        Error
    }
    public enum DialogType
    {
        OkCancel,
        OK
    }
}
