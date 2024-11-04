using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace WinFormsApp1.Common
{
    internal class FileName
    {
        public static void SetNetworkAdapter(string ipAddress, string subnetMask = null, string gateway = null)
        {
            IPAddress ethernetIPAddress = GetEthernetIPAddress();
            ManagementBaseObject inPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;
                if (((string[])mo["IPAddress"])[0] == ethernetIPAddress.ToString())
                {
                    inPar = mo.GetMethodParameters("EnableStatic");
                    //设置ip地址和子网掩码
                    inPar["IPAddress"] = new string[] { ipAddress };
                    inPar["SubnetMask"] = new string[] { subnetMask };
                    ManagementBaseObject put = mo.InvokeMethod("EnableStatic", inPar, null);
                    string str = put["returnvalue"].ToString();
                    bool b = (str == "0" || str == "1") ? true : false;
                    //设置网关地址
                    if (gateway != null)
                    {
                        inPar = mo.GetMethodParameters("SetGateways");
                        inPar["DefaultIPGateway"] = new string[] { gateway };
                        mo.InvokeMethod("SetGateways", inPar, null);
                    }
                    break;
                }
            }
        }

        //查找以太网ip
        private static IPAddress GetEthernetIPAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var item in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (item.Address.AddressFamily == AddressFamily.InterNetwork)
                            return item.Address;            //item.IPv4Mask获取掩码
                    }
                }
                //adapter.GetIPProperties().GatewayAddresses获取网关
            }
            throw new Exception("Ethernet not connected");
        }

    }
}
