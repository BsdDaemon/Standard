using System;
using System.Collections.Generic;
using System.IO;

namespace Standard.FileSystem
{
    public static class Files
    {
        private static FileStream _writer;
        private static FileStream _reader;

        public static void Write(string file, string content, bool append = false)
        {
            _writer = new FileStream(file, (append) ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            List<byte> bytes = new List<byte>();

            //Write changes
            foreach (var character in content.ToCharArray())
            {
                bytes.Add(Convert.ToByte(character));
            }

            //Write changes to disk
            _writer.Write(bytes.ToArray(), 0, bytes.Count);

            _writer.Flush();
            _writer.Close();
        }

        public static string Read(string file)
        {
            _reader = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] stringAssembly = new byte[_reader.Length];

            int remainingBuffer = (int)_reader.Length;
            int readerPointer = 0;
            while (remainingBuffer > 0)
            {
                int n = _reader.Read(stringAssembly, readerPointer, remainingBuffer);

                // Break at EOF
                if (n == 0) break;

                readerPointer += n;
                remainingBuffer -= n;
            }

            _reader.Flush();
            _reader.Close();

            return stringAssembly.ToString();
        }

        public static bool TryDelete(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            try
            {
                File.Delete(filePath);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
