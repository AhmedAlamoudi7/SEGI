using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IOurCoreValueService
    {
        Task<IEnumerable<OurCoreValueViewModel>> Detailes();
        Task<int> Update(UpdateOurCoreValueDto dto);
        Task<UpdateOurCoreValueDto> Get();
        Task<OurCoreValueViewModel> Detaile();
        Task<string> Image();
    }
}
