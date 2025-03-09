using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.WEB.Services.Services_Services
{
    public interface IBannerMainService
    {
        Task<List<BannerMainViewModel>> GetAll(string? GeneralSearch);
        Task<List<BannerMainViewModel>> GetModelList();
        Task<IEnumerable<BannerMainViewModel>> Detailes();
        Task<BannerMainViewModel> Detaile(int id);
        Task<BannerMainViewModel> Detaile();
        Task<BannerMainViewModel> DetaileSustainability();
        Task<BannerMainViewModel> DetaileHome();
        Task<int> Update(UpdateBannerMainDto dto);
        Task<UpdateBannerMainDto> Get(int Id, BannerPageType BannerPageType);
        Task<int> Delete(int Id);
        Task<string> Image(int Id);

    }
}
