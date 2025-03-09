using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services_Services
{
    public interface IGeneralEngineeringServicesItemService
    {
        Task<List<GeneralEngineeringServicesItemViewModel>> GetAll(string? GeneralSearch);
        Task<List<GeneralEngineeringServicesItemViewModel>> GetModelList();
        Task<IEnumerable<GeneralEngineeringServicesItemViewModel>> Detailes();
        Task<int> Create(CreateGeneralEngineeringServicesItemDto dto);
        Task<int> Update(UpdateGeneralEngineeringServicesItemDto dto);
        Task<UpdateGeneralEngineeringServicesItemDto> Get(int id);
        Task<GeneralEngineeringServicesItemViewModel> Detaile(int id);
        Task<string> Image(int id);
        Task<int> Delete(int Id);
        Task<string> CountGeneralEngineeringServicesItemsAsync();

    }
}
