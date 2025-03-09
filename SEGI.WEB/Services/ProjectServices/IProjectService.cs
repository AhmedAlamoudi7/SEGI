using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services.ProjectServicess
{
    public interface IProjectService
    {
        Task<List<ProjectViewModel>> GetAll(Querys querys);
        Task<List<ProjectViewModel>> GetModelList();
        Task<int> Create(CreateProjectDto dto);
        Task<int> Update(UpdateProjectDto dto);
        Task<int> Delete(int Id);
        Task<UpdateProjectDto> Get(int Id);
        Task<string> CountModelAsync();
        Task<ProjectViewModel> Detaile(int Id);
        Task<List<ProjectCategoryDto>> GetProjectCategories(int projectId);
        Task<List<ProjectServicesDto>> GetProjectServices(int projectId);
        Task<string> Image(int id);
        Task<string> CountProjectsAsync();
        Task<string> CountProjectActiveAsync();
        Task<string> CountProjectNotActiveAsync();
        Task<int> ViewCountIncrees(int Id);
    }
}
