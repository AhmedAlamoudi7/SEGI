using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IOurCoreValueItemService
    {
        Task<List<OurCoreValueItemViewModel>> GetAll(string? GeneralSearch);
        Task<List<OurCoreValueItemViewModel>> GetModelList();
        Task<IEnumerable<OurCoreValueItemViewModel>> Detailes();
        Task<int> Create(CreateOurCoreValueItemDto dto);
        Task<int> Update(UpdateOurCoreValueItemDto dto);
        Task<UpdateOurCoreValueItemDto> Get(int id);
        Task<OurCoreValueItemViewModel> Detaile(int id);
        Task<int> Delete(int Id);
    }
}
