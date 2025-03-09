using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface ISectorItemService
    {
        Task<List<SectorItemViewModel>> GetAll(string? GeneralSearch);
        Task<List<SectorItemViewModel>> GetModelList();
        Task<IEnumerable<SectorItemViewModel>> Detailes();
        Task<int> Create(CreateSectorItemDto dto);
        Task<int> Update(UpdateSectorItemDto dto);
        Task<UpdateSectorItemDto> Get(int id);
        Task<SectorItemViewModel> Detaile(int id);
        Task<string> Image(int id);
        Task<string> Icon(int id);
        Task<int> Delete(int Id);
        Task<string> CountSectorItemsAsync();
    }
}
