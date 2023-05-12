using SingleData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utility
{
    public static partial class File_Util
    {
        private static IOData ioData => IOData.Instance;

        /// <summary>
        /// Kiểm tra save file có hợp lệ hay không: Folder có tồn tại ko, FilePath có đang sử dụng ko
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static bool CheckValidSaveFile(string savePath)
        {
            if (savePath == "" || savePath.Length == 0)
            {
                MessageBox.Show("Đường dẫn file excel đang để trống!");
                return false;
            }
            if (!Directory.Exists(Path.GetDirectoryName(savePath)))
            {
                MessageBox.Show("Đường dẫn file excel không tồn tại!");
                return false;
            }

            try
            {
                if (IsFileInUse(savePath))
                {
                    MessageBox.Show($"File: {savePath} đang được mở. Bạn hãy đóng file và thử chạy lại.");
                    return false;
                }
            }
            catch(System.UnauthorizedAccessException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra File có đang sử dụng hay ko
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            return IsFileInUse(new FileInfo(fileName));
        }

        /// <summary>
        /// Kiểm tra File có đang sử dụng hay ko
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileInUse(FileInfo file)
        {
            bool rs = false;
            FileStream? fs = null;
            try
            {
                fs = File.OpenWrite(file.FullName);
            }
            catch (IOException)
            {
                // the file is unvailabe because it is:
                // still being written to
                // or being processed by another thread
                // or does not exist (has already been processed)
                rs = true;
            }
            finally
            {
                if (fs is not null)
                {
                    fs.Close();
                }
            }

            return rs;
        }

        public static void SaveResourceToFile(string resourceName, string filePath)
        {
            var asm = ioData.Assembly;

            Console.WriteLine(asm.GetManifestResourceNames().CombineString());
            string file = string.Format($"{asm.GetName().Name}.{resourceName}");
            var fileStream = asm.GetManifestResourceStream(file);

            SaveStreamToFile(fileStream, filePath);
        }

        /// <summary>
        /// Save steam dữ liệu vào một file path
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        public static void SaveStreamToFile(Stream stream, string filePath)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(filePath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        /// <summary>
        /// Viết content dạng string vào FilePath dạng file txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="isDeleteOldFile"></param>
        /// <returns></returns>
        public static bool WriteTxtFile(string filePath, string content, bool isDeleteOldFile = true)
        {
            try
            {
                if (!File_Util.CheckValidSaveFile(filePath)) return false;

                if (isDeleteOldFile && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    return false;
                }

                using (var sw = File.CreateText(filePath))
                {
                    sw.Write(content);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Viết content dạng string vào FilePath và mở file txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="isDeleteOldFile"></param>
        public static bool WriteTxtFileAndOpen(string filePath, string content, bool isDeleteOldFile = true)
        {
            if (WriteTxtFile(filePath, content, isDeleteOldFile))
            {
                Process.Start(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Đọc content trên file txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string? ReadTxtFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string s = "";
            using (StreamReader sr = File.OpenText(filePath))
            {
                s = sr.ReadToEnd();
            }
            return s;
        }

    }
}
