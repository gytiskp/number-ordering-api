using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.Models;
using Web.Services.File;
using Web.Services.Sorting;
using Xunit;

namespace Web.Tests.Tests.Services
{
    public class OrderingServiceTests
    {

        [Theory]
        [InlineData("1", "1")]
        [InlineData("7 4 9 2 1 18 5 91", "1 2 4 5 7 9 18 91")]
        public async Task SortAndSaveNumbers_shouldReturnSortedtring(string input, string expected)
        {
            const string fileName = "file01.txt";

            var mockFileService = new Mock<IFileService>();

            mockFileService
                .Setup(f => f.SaveToFile(It.IsAny<string>()))
                .ReturnsAsync(fileName);

            var sortingService = new SortingService(mockFileService.Object);

            SavedData result = await sortingService.Create(new SortingInput { Line = input });

            Assert.Equal(expected, result.OrderedNumbers);
            Assert.Equal(fileName, result.FileName);
        }

        [Fact]
        public async Task GetLatestData()
        {
            string numbers = "1 5 6";
            string path = "file01.txt";

            var mockFileService = new Mock<IFileService>();

            mockFileService
                .Setup(f => f.ReadFromLastAvailableFile(It.IsAny<Stack<FileInfo>>()))
                .ReturnsAsync(new Tuple<string, string>(numbers, path));

            var sortingService = new SortingService(mockFileService.Object);

            SavedData result = await sortingService.GetLatestData();

            Assert.Equal(numbers, result.OrderedNumbers);
            Assert.Equal(path, result.FileName);
        }

    }

}
