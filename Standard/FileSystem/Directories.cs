using System;
using System.IO;
using System.Linq;
using Standard.Helper;

namespace Standard.FileSystem
{
    public static class Directories
    {
        public static bool TryCreate(string filePath)
        {
            if (filePath == null) return false;
            if (Directory.Exists(filePath)) return false;

            try
            {
                Directory.CreateDirectory(filePath);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool TryDelete(string filePath)
        {
            if (filePath == null) return false;
            if (Directory.Exists(filePath)) return false;

            try
            {
                Directory.Delete(filePath, true);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static int CountFiles(string filePath, bool searchSubDirectories = false, string filter = "*")
        {
            if (!Directory.Exists(filePath)) return 0;

            if (searchSubDirectories)
            {
                return Directory.GetFiles(filePath, filter, SearchOption.AllDirectories).Length;
            }
            return Directory.GetFiles(filePath, filter, SearchOption.TopDirectoryOnly).Length;
        }

        public static FilePath[] ListFiles(string filePath, string filter = "*")
        {
            if (!Directory.Exists(filePath)) return new FilePath[] { };
            DirectoryInfo directive = new DirectoryInfo(filePath);
            FileInfo[] files = directive.GetFiles(filter);

            return files.Select(file => new FilePath(file.FullName)).ToArray();
        }

    }
}
