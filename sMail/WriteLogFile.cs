using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sMail
{
    public class WriteLogFile
    {
        public static bool WriteLog(string strMessage)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd")+ "_sMail.log";
                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", Application.StartupPath, fileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(strMessage);
                objStreamWriter.Close();
                objFilestream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
