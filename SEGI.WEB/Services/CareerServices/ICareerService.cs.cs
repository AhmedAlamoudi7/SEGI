using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.Services.CareerServices
{
    public interface ICareerService
    {
        Task<List<CareerViewModel>> GetAll(Querys querys);
        Task<List<CareerViewModel>> GetModelList();
        Task<int> Create(CreateCareerDto dto);
        Task<int> Update(UpdateCareerDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCareerDto> Get(int Id);
        Task<string> CountModelAsync();
        Task<CareerViewModel> Detaile(int Id);
        Task<List<CareerCategoryDto>> GetCareerCategories(int projectId);
        Task<string> CountCareersAsync();
        Task<string> CountCareersActiveAsync();
        Task<string> CountCareersNotActiveAsync();
        Task<List<ApplyCareerFormModelViewModel>> GetAllSubmitApplication(Querys querys);
        Task<List<ApplyCareerFormModelViewModel>> GetModelListSubmitApplication();
        Task<ApplyCareerFormModelViewModel> DetaileApplication(int Id);
        Task<int> SubmitApplication(CreateApplyCareerFormModelDto dto);
        Task<int> DeleteApplication(int Id);
        Task<int> ViewCountIncrees(int Id);

    }
}
