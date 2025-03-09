using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface ICoreValueService
    {
        Task<IEnumerable<CoreValueViewModel>> Detailes();
        Task<int> Update(UpdateCoreValueDto dto);
        Task<UpdateCoreValueDto> Get();
        Task<CoreValueViewModel> Detaile();
        Task<string> Image();
    }
}
