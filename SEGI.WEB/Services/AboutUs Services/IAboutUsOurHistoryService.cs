using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface IAboutUsOurHistoryService
    {
        Task<IEnumerable<AboutUsOurHistoryViewModel>> Detailes();
        Task<int> Update(UpdateOurHistoryAboutUssDto dto);
        Task<UpdateOurHistoryAboutUssDto> Get();
        Task<AboutUsOurHistoryViewModel> Detaile();
        Task<string> Image();
    }
}
