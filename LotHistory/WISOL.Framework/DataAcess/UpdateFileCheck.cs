using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Wisol.DataAcess
{
    public static class UpdateFileCheck
    {
        public static bool CheckFile(string address, int port)
        {
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(address, port);
                NetworkStream ns = client.GetStream();

                buffer = BitConverter.GetBytes('C');
                ns.Write(buffer, 0, buffer.Length);

                buffer = new byte[bufferSize];
                ns.Read(buffer, 0, buffer.Length);
                int fileCount = BitConverter.ToInt32(buffer, 0);
                for (int i = 0; i < fileCount; i++)
                {
                    buffer = new byte[bufferSize];
                    ns.Read(buffer, 0, buffer.Length);
                    string[] fileInfo = Encoding.Default.GetString(buffer, 0, buffer.Length).Trim().Split('/');
                    string fileName = fileInfo[0];
                    string fileLastWriteTime = fileInfo[1].Replace("\0", "");

                    FileInfo finfo = new FileInfo("./" + fileName);
                    if (finfo.Exists == false)
                    {
                        return true;
                    }
                    else if (finfo.LastWriteTime.ToString("yyyyMMddHHmmss") != fileLastWriteTime)
                    {
                        return true;
                    }
                    buffer = BitConverter.GetBytes('N');
                    ns.Write(buffer, 0, buffer.Length);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
