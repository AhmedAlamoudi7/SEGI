using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services_Services
{
    public interface IPMCServicesItemService
    {
        Task<List<PMCServicesItemViewModel>> GetAll(string? GeneralSearch);
        Task<List<PMCServicesItemViewModel>> GetModelList();
        Task<IEnumerable<PMCServicesItemViewModel>> Detailes();
        Task<int> Create(CreatePMCServicesItemDto dto);
        Task<int> Update(UpdatePMCServicesItemDto dto);
        Task<UpdatePMCServicesItemDto> Get(int id);
        Task<PMCServicesItemViewModel> Detaile(int id);
        Task<string> Image(int Id);
        Task<int> Delete(int Id);
    }
}
