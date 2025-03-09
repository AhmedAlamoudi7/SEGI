using SEGI.Core.Dtos;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.WEB.Services.Services_Services
{
    public interface IFixedItemService
    {
        Task<IEnumerable<FixedItemViewModel>> Detailes();
        Task<FixedItemViewModel> Detaile();
        Task<int> Update(UpdateFixedItemDto dto);
        Task<UpdateFixedItemDto> Get();
        Task<string> Image_Dark();
        Task<string> Image_White();

    }
}
