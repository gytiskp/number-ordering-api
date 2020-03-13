using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Config.Exceptions;
using Web.Utils;

namespace Web.Services.File
{
    public class FileService : IFileService
    {
        public async Task<string> SaveToFile(string text)
        {
            string fileName = GenerateFileName();
            string path = @$"{ConstantHelper.FILE_DIR}\{fileName}";

            Directory.CreateDirectory(ConstantHelper.FILE_DIR);
            await WriteToFile(path, text);

            return fileName;
        }

        private static string GenerateFileName()
        {
            Random random = new Random();

            int rng = random.Next(100, 1000);
            string time = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            return $"{time}_{rng}.{ConstantHelper.FILE_EXTENSION}";
        }

        private async Task WriteToFile(string path, string text)
        {
            using (StreamWriter writer = System.IO.File.CreateText(path))
            {
                await writer.WriteAsync(text);
            }
        }

        public async Task<Tuple<string, string>> ReadFromLastAvailableFile(Stack<FileInfo> files)
        {
            while (files?.Count > 0)
            {
                try
                {
                    FileInfo file = files.Pop();

                    string fileName = file.Name;
                    string result = await ReadFromFile(file.FullName);

                    return new Tuple<string, string>(result, fileName);
                }
                catch (IOException ex)
                {
                    // If file is being written to (being created) then move on to next most recent file
                    if (FileIsLocked(ex))
                    {
                        continue;
                    }

                    throw;
                }
            }

            // If we reached this line it means the only save file(s) is unaccessible or there are no files at all
            throw new NoDataException();
        }

        private async Task<string> ReadFromFile(string path)
        {
            using (FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
            {
                return await sr.ReadToEndAsync();
            }
        }

        private bool FileIsLocked(Exception exception)
        {
            const long ERROR_SHARING_VIOLATION = 0x20;
            const long ERROR_LOCK_VIOLATION = 0x21;

            long win32ErrorCode = exception.HResult & 0xFFFF;

            return win32ErrorCode == ERROR_SHARING_VIOLATION || win32ErrorCode == ERROR_LOCK_VIOLATION;
        }

        public Stack<FileInfo> GetFilePathsFromDirectory(string path, string searchPattern)
        {
            Directory.CreateDirectory(path);

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles(searchPattern);

            files.OrderByDescending(f => f.Name).ToArray();

            return new Stack<FileInfo>(files);
        }
    }
}
