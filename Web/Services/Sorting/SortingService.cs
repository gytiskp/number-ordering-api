using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.Models;
using Web.Services.File;
using Web.Utils;
using Web.Utils.Sorting;
using Web.Utils.Sorting.Algorithms;

namespace Web.Services.Sorting
{
    public class SortingService : ISortingService
    {
        private readonly IFileService _fileService;

        public SortingService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<SavedData> Create(SortingInput input)
        {
            long[] array = input.GetNumbers();

            ArraySorter sorter = new ArraySorter(new MergeSort());
            sorter.Sort(array);

            string orderedNumbers = StringHelper.Concat(array);

            string fileName = await _fileService.SaveToFile(orderedNumbers);

            return new SavedData { OrderedNumbers = orderedNumbers, FileName = fileName };
        }

        public async Task<SavedData> GetLatestData()
        {
            Stack<FileInfo> paths = _fileService.GetFilePathsFromDirectory(ConstantHelper.FILE_DIR, $"*.{ConstantHelper.FILE_EXTENSION}");

            Tuple<string, string> result = await _fileService.ReadFromLastAvailableFile(paths);

            return new SavedData { OrderedNumbers = result.Item1, FileName = result.Item2 };
        }

    }
}
