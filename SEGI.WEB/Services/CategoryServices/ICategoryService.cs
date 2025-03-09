
using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services.CategoryServices
{
    public interface ICategoryService

    {
        Task<List<CategoryViewModels>> GetAll(string? GeneralSearch);
        Task<List<CategoryViewModels>> GetCategoryList();
        Task<int> Create(CreateCategoryDto dto);
        Task<int> Update(UpdateCategoryDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCategoryDto> Get(int Id);
        Task<string> CountCategoriesAsync();
        Task<CategoryViewModels> Detaile(int Id);

    }
}
