using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Commons.Concretes.Helpers
{
    public static class FileHelper
    {
        public static void WriteFile(string folderPath, string fileName, string fileText)
        {
            WriteFile(folderPath, fileName, fileText, false);
        }

        public static void WriteFile(string folderPath, string fileName, string fileText, bool bOverwrite)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                throw new ArgumentNullException("strFolderPath" + " The FolderPath cannot be null or empty. ");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("strFileName" + " The FileName cannot be null or empty. ");
            }
            if (string.IsNullOrEmpty(fileText))
            {
                throw new ArgumentNullException("strFileText" + " The FileText cannot be null or empty. ");
            }

            CreateFolder(folderPath);

            fileName = Path.GetFileName(fileName);

            string filePathAndName = Path.Combine(folderPath, fileName);

            if (File.Exists(filePathAndName))
            {
                if (bOverwrite)
                {
                    File.Delete(filePathAndName);
                }
                else
                {
                    return;
                }
            }

            using (var fs = new FileStream(filePathAndName, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(fileText);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        public static string CreateFolder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                throw new ArgumentNullException("strFolderPath" + " The FolderPath cannot be null or empty. ");
            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public static string CreateFolder(string parentFolderPath, string folderName)
        {
            if (string.IsNullOrEmpty(parentFolderPath))
            {
                throw new ArgumentNullException("parentFolderPath" + " The Parent Folder Path cannot be null or empty. ");
            }
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentNullException("folderName" + " The Folder Name cannot be null or empty. ");
            }

            folderName = Path.GetDirectoryName(folderName);

            if (folderName != null)
            {
                string strFolderPath = Path.Combine(parentFolderPath, folderName);
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }

                return strFolderPath;
            }

            return string.Empty;
        }
    }
}
