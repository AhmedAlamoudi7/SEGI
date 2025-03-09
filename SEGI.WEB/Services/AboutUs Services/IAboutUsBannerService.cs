using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface IAboutUsBannerService
    {
        Task<IEnumerable<AboutUsBannerViewModel>> Detailes();
        Task<int> Update(UpdateBannerAboutUssDto dto);
        Task<UpdateBannerAboutUssDto> Get();
        Task<AboutUsBannerViewModel> Detaile();
        Task<string> Image();
    }
}
