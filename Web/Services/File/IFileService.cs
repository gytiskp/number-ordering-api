using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Web.Services.File
{
    public interface IFileService
    {
        Task<string> SaveToFile(string text);
        Task<Tuple<string, string>> ReadFromLastAvailableFile(Stack<FileInfo> files);
        Stack<FileInfo> GetFilePathsFromDirectory(string path, string searchPattern);
    }
}
