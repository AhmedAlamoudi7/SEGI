using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface IAboutUsLegacyService
    {
        Task<IEnumerable<LegacyAboutUsViewModel>> Detailes();
        Task<int> Update(UpdateLegacyAboutUssDto dto);
        Task<UpdateLegacyAboutUssDto> Get();
        Task<LegacyAboutUsViewModel> Detaile();
        Task<string> Image();
    }
}
