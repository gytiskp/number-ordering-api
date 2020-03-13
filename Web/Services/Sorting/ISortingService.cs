using System.Threading.Tasks;
using Web.Models;

namespace Web.Services.Sorting
{
    public interface ISortingService
    {
        Task<SavedData> Create(SortingInput line);
        Task<SavedData> GetLatestData();

    }
}
