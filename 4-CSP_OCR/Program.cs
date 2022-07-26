using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSP_OCR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                string input = args[0].ToString();
                if (!input.Equals("PASS"))
                {
                    MessageBox.Show("Please Run Updater Program First!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.ExitThread();
                    Environment.Exit(0);
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("Please Run Updater Program First!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.ExitThread();
                Environment.Exit(0);
                Application.Exit();
            }

            Application.Run(new Form1());
        }
    }
}
