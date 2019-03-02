using System.Threading.Tasks;
using RoadStatus.DTO;

namespace RoadStatus.Services
{
    public interface ITflService
    {
        Task<Result<TflApiPresentationEntitiesRoadCorridor>> GetRoadAsync(string roadName);
    }
}