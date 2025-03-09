using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IWhoWeAreService
    {
        Task<IEnumerable<WhoWeAreViewModel>> Detailes();
        Task<int> Update(UpdateWhoWeAreDto dto);
        Task<UpdateWhoWeAreDto> Get();
        Task<WhoWeAreViewModel> Detaile();
        Task<string> Image();
    }
}
