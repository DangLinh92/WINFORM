using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WisolUpdate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string current = Directory.GetCurrentDirectory();
                string source = File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + "UpdateInfo.txt");
                FileInfo remoteFile = new FileInfo(source + "\\UTI_COST.exe");
                DateTime remoteLastModified = remoteFile.LastWriteTime;
                if (remoteLastModified.Year >= 2020)
                {
                    DateTime localLastModified = (new FileInfo(Directory.GetCurrentDirectory() + "\\" + "UTI_COST.exe")).LastWriteTime;
                    if (remoteLastModified != localLastModified)
                    {
                        DirectoryInfo diSource = new DirectoryInfo(source);
                        DirectoryInfo diTarget = new DirectoryInfo(Directory.GetCurrentDirectory());

                        CopyAll(diSource, diTarget);

                        UpdateForm form = new UpdateForm();
                        form.ShowDialog();
                    }
                    else
                    {
                        new Process()
                        {
                            StartInfo = {
                                FileName = "UTI_COST",
                                Verb = "Open",
                                Arguments = "PASS"
                            }
                        }.Start();
                        Application.ExitThread();
                        Environment.Exit(0);
                        Application.Exit();
                    }
                }
                else
                {
                    new Process()
                    {
                        StartInfo = {
                                FileName = "UTI_COST",
                                Verb = "Open",
                                Arguments = "PASS"
                            }
                    }.Start();
                    Application.ExitThread();
                    Environment.Exit(0);
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                new Process()
                {
                    StartInfo = {
                                FileName = "UTI_COST",
                                Verb = "Open",
                                WorkingDirectory = Environment.CurrentDirectory,
                                Arguments = "PASS"
                            }
                }.Start();
                Application.ExitThread();
                Environment.Exit(0);
                Application.Exit();

            }
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
