using SEGI.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.HomeServices
{
    public interface IProcessSafetyStudiesService
    {
        Task<IEnumerable<ProcessSafetyStudiesViewModel>> Detailes();
        Task<int> Update(UpdateProcessSafetyStudiesDto dto);
        Task<UpdateProcessSafetyStudiesDto> Get();
        Task<ProcessSafetyStudiesViewModel> Detaile();
        Task<string> Image();
    }
}
