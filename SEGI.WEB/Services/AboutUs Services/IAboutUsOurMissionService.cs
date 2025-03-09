using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface IAboutUsOurMissionService
    {
        Task<IEnumerable<AboutUsOurMissionViewModel>> Detailes();
        Task<int> Update(UpdateOurMissionAboutUssDto dto);
        Task<UpdateOurMissionAboutUssDto> Get();
        Task<AboutUsOurMissionViewModel> Detaile();
        Task<string> Image();
    }
}
