using System.IO.Ports;

namespace Wisol
{
    public class Barcode
    {
        private string COMport = string.Empty;
        SerialPort serialPort = null;
        public Barcode(string _COMport)
        {
            COMport = _COMport;
            serialPort = new SerialPort(COMport, 9600, Parity.None, 8, StopBits.One);
        }

        public void PrintLabel(string data)
        {
            serialPort.Open();
            serialPort.Write(data);
            serialPort.Close();
        }
    }
}
