using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IAnalaticService
    {
        Task<List<AnalaticViewModel>> GetAll(string? GeneralSearch);
        Task<List<AnalaticViewModel>> GetModelList();
        Task<IEnumerable<AnalaticViewModel>> Detailes();
        Task<int> Create(CreateAnalaticDto dto);
        Task<int> Update(UpdateAnalaticDto dto);
        Task<UpdateAnalaticDto> Get(int id);
        Task<AnalaticViewModel> Detaile(int id);
        Task<int> Delete(int Id);
    }
}
