using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorViewModel>> Detailes();
        Task<int> Update(UpdateSectorDto dto);
        Task<UpdateSectorDto> Get();
        Task<SectorViewModel> Detaile();
        Task<string> Image();
    }
}
