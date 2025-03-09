
using SEGI.Core.Dtos;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;

namespace SEGI.Services.Services.ContactUsServicess
{
    public interface IContactUsService

    {
        Task<List<ContactUsViewModel>> GetAll(string? GeneralSearch);
        Task<List<ContactUsViewModel>> GetContactUsList();
        Task<int> Create(CreateContactUsDto dto);
        Task<int> Update(UpdateContactUsDto dto);
        Task<int> Delete(int Id);
        Task<UpdateContactUsDto> Get(int Id);
        Task<string> CountContactUsAsync();
        Task<ContactUsViewModel> Detaile(int Id);

    }
}
