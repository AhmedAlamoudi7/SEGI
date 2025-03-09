using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services.HistoryServices
{
    public interface IHistoryService
    {
        Task<List<HistoryViewModel>> GetAll(Querys querys);
        Task<List<HistoryViewModel>> GetModelList();
        Task<int> Create(CreateHistoryDto dto);
        Task<int> Update(UpdateHistoryDto dto);
        Task<int> Delete(int Id);
        Task<UpdateHistoryDto> Get(int Id);
        Task<string> CountModelAsync();
        Task<HistoryViewModel> Detaile(int Id);
        Task<string> Image(int id);
    }
}
