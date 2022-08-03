using System.Net.Sockets;

namespace Wisol.Objects
{
    public class TCPSocket
    {
        private TcpListener listener = null;
        private TcpClient client = null;

        public TcpListener Listener
        {
            get
            {
                return listener;
            }
            set
            {
                listener = value;
            }
        }
        public TcpClient Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }
    }
}
