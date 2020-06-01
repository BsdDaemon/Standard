using System.IO;

namespace Standard.Helper
{
    public class FilePath
    {
        private string _fullPath;
        private char _separator = Path.PathSeparator;

        public string Full => _fullPath;
        public string PathOnly => Path.GetDirectoryName(_fullPath);
        public string FileOnly => Path.GetFileName(_fullPath);
        public string FileNameOnly => Path.GetFileNameWithoutExtension(_fullPath);
        public string FileTypeOnly => Path.GetExtension(_fullPath);

        public FilePath(string fullPath)
        {
            _fullPath = fullPath;
        }

        public void DirectorySeparator(char separator = '\\')
        {
            _fullPath = _fullPath.Replace(_separator, separator);
            _separator = separator;
        }
    }
}
