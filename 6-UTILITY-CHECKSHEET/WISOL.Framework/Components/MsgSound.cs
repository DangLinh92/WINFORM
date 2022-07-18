using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;

namespace Wisol.Components
{
    public static class MsgSound
    {
        public static void Show(LabelControl control, string msg, MsgSoundType type)
        {
            control.Text = msg;
            control.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            control.Appearance.ForeColor = type == MsgSoundType.OK ? Color.Blue : Color.Red;

            using (var backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
                backgroundWorker.RunWorkerAsync(type);
            }
        }

        public static void Show(TextEdit control, string msg, MsgSoundType type)
        {
            control.Text = msg;
            control.Properties.Appearance.ForeColor = type == MsgSoundType.OK ? SystemColors.ButtonHighlight : Color.Red;

            using (var backgroundWorker = new BackgroundWorker())
            {
                backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
                backgroundWorker.RunWorkerAsync(type);
            }
        }

        static void DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (e.Argument is MsgSoundType type)
                {
                    using (var player = new SoundPlayer(type == MsgSoundType.OK ? "./OK.wav" : "./NO.wav"))
                    {
                        player.Play();
                    }
                }
            }
            catch (Exception error) { MsgBox.Show(error.Message, MsgType.Error); }
        }
    }

    public enum MsgSoundType
    {
        OK,
        NG
    }
}
