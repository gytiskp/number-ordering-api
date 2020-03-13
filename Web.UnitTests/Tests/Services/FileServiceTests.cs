using System.Threading.Tasks;
using Web.Config.Exceptions;
using Web.Services.File;
using Xunit;

namespace Web.Tests.Tests.Services
{
    public class FileServiceTests
    {
        [Fact]
        public async Task GetFileWhenDirEmpty_shouldReturnCustomException()
        {
            FileService service = new FileService();

            await Assert.ThrowsAsync<NoDataException>(() => service.ReadFromLastAvailableFile(null));
        }
    }
}