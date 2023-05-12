using System;
using System.Net.NetworkInformation;

namespace Utility
{
    public static class MacAddressUtil
    {
        public static string GetMacAddress()
        {
            try
            {
                string macAddresses = "";

                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }

                return macAddresses;
            }
            catch
            {
                throw;
            }
        }
    }
}