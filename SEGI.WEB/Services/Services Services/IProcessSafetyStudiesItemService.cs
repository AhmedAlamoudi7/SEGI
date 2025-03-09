using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IProcessSafetyStudiesItemService
    {
        Task<List<ProcessSafetyStudiesItemViewModel>> GetAll(string? GeneralSearch);
        Task<List<ProcessSafetyStudiesItemViewModel>> GetModelList();
        Task<IEnumerable<ProcessSafetyStudiesItemViewModel>> Detailes();
        Task<int> Create(CreateProcessSafetyStudiesItemDto dto);
        Task<int> Update(UpdateProcessSafetyStudiesItemDto dto);
        Task<UpdateProcessSafetyStudiesItemDto> Get(int id);
        Task<ProcessSafetyStudiesItemViewModel> Detaile(int id);
        Task<int> Delete(int Id);
    }
}
