using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services.NewServices
{
    public interface INewService
    {
        Task<List<NewViewModel>> GetAll(Querys querys);
        Task<List<NewViewModel>> GetModelList();
        Task<int> Create(CreateNewDto dto);
        Task<int> Update(UpdateNewDto dto);
        Task<int> Delete(int Id);
        Task<UpdateNewDto> Get(int Id);
        Task<string> CountModelAsync();
        Task<NewViewModel> Detaile(int Id);
        Task<List<NewCategoriesDto>> GetNewCategories(int projectId);
        Task<string> CountNewsAsync();
        Task<int> ViewCountIncrees(int Id);
    }
}
