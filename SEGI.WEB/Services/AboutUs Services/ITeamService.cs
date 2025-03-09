using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.AboutUsServices
{
    public interface ITeamService
    {
        Task<List<TeamViewModel>> GetAll(string? GeneralSearch);
        Task<List<TeamViewModel>> GetModelList();
        Task<IEnumerable<TeamViewModel>> Detailes();
        Task<int> Create(CreateTeamDto dto);
        Task<int> Update(UpdateTeamDto dto);
        Task<UpdateTeamDto> Get(int id);
        Task<TeamViewModel> Detaile(int id);
        Task<int> Delete(int Id);
        Task<string> Image(int Id);

    }
}
