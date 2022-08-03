using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Wisol.MES.Classes
{
    class csNetDrive
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        [DllImport("mpr.dll", CharSet = CharSet.Auto)]
        public static extern int WNetUseConnection(
            IntPtr hwndOwner,
            [MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
            string lpPassword,
            string lpUserID,
            uint dwFlags,
            StringBuilder lpAccessName,
            ref int lpBufferSize,
            out uint lpResult);

        public int setRemoteConnection(string strRemoteConnectString, string strRemoteUserID, string strRemotePWD, string strLocalName)
        {
            int capacity = 64;
            uint resultFlags = 0;
            uint flags = 0;

            try
            {
                if ((strRemoteConnectString != "" || strRemoteConnectString != string.Empty) &&
                   (strRemoteUserID != "" || strRemoteUserID != string.Empty) &&
                   (strRemotePWD != "" || strRemotePWD != string.Empty))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(capacity);
                    NETRESOURCE ns = new NETRESOURCE();
                    ns.dwType = 1;
                    ns.lpLocalName = strLocalName;                     
                    ns.lpRemoteName = strRemoteConnectString;
                    ns.lpProvider = null;

                    int result = WNetUseConnection(IntPtr.Zero, ref ns, strRemotePWD, strRemoteUserID, flags,
                                        sb, ref capacity, out resultFlags);

                    return result;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        [DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
        public static extern int WNetCancelConnection2(string lpName, int dwFlags, int fForce);

        public int CencelRemoteServer(string strRemoteConnectString)
        {
            int result;

            try
            {
                result = WNetCancelConnection2(strRemoteConnectString, 1, 1);
            }
            catch
            {
                result = 0;
            }

            return result;
        }
    }
}
