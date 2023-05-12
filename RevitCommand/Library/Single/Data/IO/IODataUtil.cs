using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class IODataUtil
    {
        public static string GetDeviceId(this IOData iOData)
        {
            var sb = new StringBuilder();

            var mos = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (var mo in mos.Get())
            {
                try
                {
                    sb.Append(mo["ProcessorId"].ToString().Substring(0, 4));
                }
                catch
                {
                    throw new Exception("Processor");
                }
                break;
            }

            mos = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (var mo in mos.Get())
            {
                try
                {
                    sb.Append(mo["Model"].ToString().Substring(0, 4));
                }
                catch
                {
                    throw new Exception("HDD Model");
                }
                break;
            }

            mos = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (var mo in mos.Get())
            {
                try
                {
                    sb.Append(mo["Version"].ToString().Substring(0, 4));
                }
                catch
                {
                    throw new Exception("BIOS");
                }
                break;
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            var hashedBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);

            var device_Id = Convert.ToBase64String(hashedBytes);
            return device_Id;
        }

        public static void Dispose()
        {
            IOData.Instance = null;
        }
    }
}
