using PROJ_B_DLL.Objects;
using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Wisol.DataAcess
{
    public class FileAccess
    {
        private string serverIp = string.Empty;
        private int port = 8083;
        public FileAccess(string serverIp)
        {
            this.serverIp = serverIp;
        }

        public string ServerIp
        {
            get
            {
                return serverIp;
            }
            set
            {
                serverIp = value;
            }
        }

        public FileObject SetFile(string fileName, Byte[] fileContent)
        {
            FileObject fileObject = null;
            try
            {
                System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
                NetworkStream serverStream = null;
                BinaryFormatter bf = null;
                fileObject = new FileObject();
                fileObject.FileName = fileName;
                fileObject.FileContent = fileContent;

                clientSocket = new System.Net.Sockets.TcpClient();
                clientSocket.Connect(serverIp, port);
                serverStream = clientSocket.GetStream();

                bf = new BinaryFormatter();
                bf.Serialize(serverStream, fileObject);

                fileObject = (FileObject)bf.Deserialize(serverStream);

                serverStream.Flush();
                clientSocket.Close();
                return fileObject;
            }
            catch
            {
                return fileObject;
            }
        }
        public FileObject GetFile(string fileName)
        {
            FileObject fileObject = null;
            try
            {
                System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
                NetworkStream serverStream = null;
                BinaryFormatter bf = null;
                fileObject = new FileObject();
                fileObject.FileName = fileName;
                fileObject.FileContent = null;

                clientSocket = new System.Net.Sockets.TcpClient();
                clientSocket.Connect(serverIp, port);
                serverStream = clientSocket.GetStream();

                bf = new BinaryFormatter();
                bf.Serialize(serverStream, fileObject);

                fileObject = (FileObject)bf.Deserialize(serverStream);

                serverStream.Flush();
                clientSocket.Close();
                return fileObject;
            }
            catch
            {
                return fileObject;
            }
        }
    }
}
