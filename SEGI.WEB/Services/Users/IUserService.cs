using Microsoft.AspNetCore.Identity;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Helpers;
using SEGI.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.Users
{
    public interface IUserService
    {
        Task<List<ApplicationUserViewModel>> GetAll(Querys querys);
        Task<ApplicationUserViewModel> Detailes(string? id);
        Task<IdentityResult> Create(CreateApplicationUserDto dto);
        Task<IdentityResult> Update(UpdateApplicationUserDto dto);
        Task<string> Delete(string Id);
        Task<ApplicationUserViewModel> Get(string Id);
        Task<bool> EmailConfirmed(string Id, int code);
        Task<SignInResult> Login(string UserName, string Password);
        ApplicationUserViewModel GetUserByUserName(string UserName);
        Task<string> UserImage(string id);
        Task<UpdateApplicationUserDto> GetUpdate(string Id);
        ApplicationUser FindByEmail(string Email);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<List<ApplicationUserViewModel>> GetModelList();
        Task<bool> ChangeUserActive(string UserId);
        Task<ApplicationUserViewModel> DetailesForUser(string? id);
        Task<bool> UpdateUserPasswordAsync(ApplicationUser user, string newPassword);
        Task<string> CountUsersAsync();
        Task<string> CountUsersNotActiveAsync();
        Task<string> CountUsersActiveAsync();

    }
}
