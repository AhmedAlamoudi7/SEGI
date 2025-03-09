using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.WEB.Services.Services_Services
{
    public interface IServicesService
    {
        Task<List<ArticleViewModel>> GetAll(string? GeneralSearch);
        Task<List<ArticleViewModel>> GetModelList();
        Task<IEnumerable<ArticleViewModel>> Detailes();
        Task<ArticleViewModel> Detaile(int id);
        Task<ArticleViewModel> DetaileBySection(int SectionId);
        Task<int> Update(UpdateArticleDto dto);
        Task<UpdateArticleDto> Get(int SectionId, PageType PageType);
        Task<int> Delete(int Id);
        Task<string> Image(int SectionId);

    }
}
