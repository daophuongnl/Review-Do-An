using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Utility;

namespace SingleData
{
    public class IOData
    {
        private static IOData? instance;
        public static IOData Instance
        {
            get => instance ??= new IOData();
            set=>instance = value;
        }

        private Assembly? assembly;
        public Assembly Assembly
        {
            get => assembly ??= Assembly.GetExecutingAssembly();
            set
            {
                assembly = value;

                AssemblyFilePath = null;
                AssemblyDirectoryPath = null;
            }
        }

        private string? assemblyFilePath;
        public string AssemblyFilePath
        {
            get => assemblyFilePath ??= Assembly.Location;
            set => assemblyFilePath = value;
        }

        private string? assemblyDirectoryPath;
        public string AssemblyDirectoryPath
        {
            get => assemblyDirectoryPath ??= Path.GetDirectoryName(AssemblyFilePath);
            set => assemblyDirectoryPath = value;
        }

        private string? iconDirectoryPath;
        public string IconDirectoryPath
        {
            get => iconDirectoryPath ??= Path.Combine(AssemblyDirectoryPath, "Icon");
            set => iconDirectoryPath = value;
        }

        private string? desktopDirectoryPath;
        public string DesktopDirectoryPath
        {
            get => desktopDirectoryPath ??= Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            set => desktopDirectoryPath = value;
        }

        private string? epplusPath;
        public string EpplusPath => epplusPath ??= Path.Combine(AssemblyDirectoryPath, "EPPlus.dll");

        private string tempDirectoryPath = @"D:\Tai\Temp";
        public string TempDirectoryPath
        {
            get => tempDirectoryPath;
            set =>tempDirectoryPath = value;
        }

        private string? tempFilePath;
        public string TempFilePath
        {
            get => tempFilePath ??= Path.Combine(TempDirectoryPath, "test.txt");
            set => tempFilePath = value;
        }

        private string? errorFilePath;
        public string ErrorFilePath
        {
            get => errorFilePath ??= Path.Combine(AssemblyDirectoryPath, "error.txt");
            set => errorFilePath = value;
        }

        private string? deviceId;
        public string DeviceId => deviceId ??= this.GetDeviceId();
    }
}
