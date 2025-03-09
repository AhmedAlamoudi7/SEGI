using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface IAboutUsOurVisionService
    {
        Task<IEnumerable<AboutUsOurVisionViewModel>> Detailes();
        Task<int> Update(UpdateOurVisionAboutUssDto dto);
        Task<UpdateOurVisionAboutUssDto> Get();
        Task<AboutUsOurVisionViewModel> Detaile();
        Task<string> Image();
    }
}
