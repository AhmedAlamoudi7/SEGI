using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SEGI.WEB.Data;
using SEGI.Core.ViewModels;
using SEGI.Core.Exceptions;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Helpers;
using SEGI.WEB.Data;
using SEGI.Services.FileServices;


namespace SEGI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFileService _fileService;

        public UserService(ApplicationDbContext db,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IFileService fileService
             )
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _fileService = fileService;

        }
        public async Task<List<ApplicationUserViewModel>> GetAll(Querys querys)
        {
            var model = await _db.Users
                         .Where(x => (x.FullName.Contains(querys.GeneralSearch)
                    || string.IsNullOrWhiteSpace(querys.GeneralSearch)))
                         .OrderByDescending(x => x.CreatedAt)
                         .ToListAsync();

            if (model == null)
            {
                return new List<ApplicationUserViewModel>();
            }
            var modelmapper = _mapper.Map<List<ApplicationUserViewModel>>(model);
            return modelmapper;
        }
        public async Task<ApplicationUserViewModel> Detailes(string? id)
        {
            var user = await _db.Users
                .Where(x => x.UserType == UserType.SuperAdmin)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ApplicationUserViewModel>(user);
        }
        public async Task<List<ApplicationUserViewModel>> GetModelList()
        {
            var model = await _db.Users
                 .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ApplicationUserViewModel>();
            }
            var modelmapper = _mapper.Map<List<ApplicationUserViewModel>>(model);
            return modelmapper;
        }
        public async Task<IdentityResult> Create(CreateApplicationUserDto dto)
        {

            var emailOrPhoneIsExist = _db.Users
                .Any(x => (x.Email == dto.Email));
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = _mapper.Map<ApplicationUser>(dto);
            if (dto.Image != null)
            {
                user.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            user.UserName = dto.Email;

            //// Encrypt sensitive data
            //user.FullName = _encryptionService.Encrypt(dto.FullName);
            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (dto.UserType != null)
                {
                    //await _userManager.AddToRolesAsync(user, dto.UserType.ToString());
                    await _userManager.AddToRoleAsync(user, dto.UserType.ToString());
                }
                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //await _emailService.Send(user.Email, "New Account !", $"Username is : {user.Email} and Password is { password }");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> Update(UpdateApplicationUserDto dto)
        {

            var emailOrPhoneIsExist = _db.Users
                .Any(x => (x.Email == dto.Email ||
                 x.Id == dto.Id) &&
                x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            try
            {
                var user = _db.Users.Find(dto.Id);
                var currentUserRole = user.UserType.ToString();

                // Delete the old image if a new image is provided
                if (!string.IsNullOrEmpty(user.ImageUrl) && dto.Image != null)
                {
                    // Get the full path of the existing image
                    var oldImagePath = Path.Combine("wwwroot/Files/Images", user.ImageUrl);

                    // Check if the file exists and delete it
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                var updatedUser = _mapper.Map<UpdateApplicationUserDto, ApplicationUser>(dto, user);
                if (dto.Image != null)
                {
                    updatedUser.ImageUrl = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
                }
                await _userManager.UpdateAsync(updatedUser);
                if (!currentUserRole.Equals(dto.UserType.ToString()))
                {
                    await _userManager.RemoveFromRoleAsync(user, currentUserRole);
                    await _userManager.AddToRoleAsync(user, user.UserType.ToString());
                }
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            // Delete the old image if a new image is provided
            if (!string.IsNullOrEmpty(user.ImageUrl))
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", user.ImageUrl);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return null;
        }


        public async Task<ApplicationUserViewModel> Get(string Id)
        {
            var user = await _db.Users
                 .SingleOrDefaultAsync(x =>
                x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            var modelmapper = _mapper.Map<ApplicationUserViewModel>(user);
            return modelmapper;
        }
        public async Task<UpdateApplicationUserDto> GetUpdate(string Id)
        {
            var user = await _db.Users
                 .SingleOrDefaultAsync(x =>
                x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            var modelmapper = _mapper.Map<UpdateApplicationUserDto>(user);
            return modelmapper;
        }
        public ApplicationUserViewModel GetUserByUserName(string UserName)
        {
            var user = _db.Users
                .SingleOrDefault(x =>
                (x.UserName == UserName || x.Email == UserName));
            if (user == null)
            {
                return null;
            }
            var mapper = _mapper.Map<ApplicationUserViewModel>(user);
            return mapper;
        }
        public async Task<bool> EmailConfirmed(string Id, int code)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            if (user.Code == code)
            {
                user.EmailConfirmed = true;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<SignInResult> Login(string UserName, string Password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => (x.UserName == UserName || x.Email == UserName));

            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await _userManager.CheckPasswordAsync(user, Password);

            if (!result)
            {
                return SignInResult.Failed;
            }

            return SignInResult.Success;
        }
        public async Task<string> UserImage(string id)
        {
            var model = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return model.ImageUrl;
        }
        public ApplicationUser FindByEmail(string Email)
        {
            return _db.Users.SingleOrDefault(x => x.Email == Email);
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            if (user == null) { throw new InvalidDateException(); }
            var existingUser = _userManager.UpdateAsync(user);
            if (existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            return IdentityResult.Success;
        }


        public async Task<bool> ChangeUserActive(string UserId)
        {
            if (UserId == null)
            {
                throw new InvalidDateException(); // Handle user not found appropriately
            }
            // Fetch the user from the database
            var user = _db.Users.Find(UserId);
            // Check if user exists and if their balance is sufficient
            if (user == null)
            {
                throw new EntityNotFoundException(); // Handle user not found appropriately
            }
            user.Active = !user.Active;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return (bool)user.Active; // Return user active
        }

        public async Task<ApplicationUserViewModel> DetailesForUser(string? id)
        {
            var user = await _db.Users
                .FindAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ApplicationUserViewModel>(user);
        }



        public async Task<bool> UpdateUserPasswordAsync(ApplicationUser user, string newPassword)
        {
            // Create the PasswordHasher instance
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            // Generate the new password hash
            user.PasswordHash = passwordHasher.HashPassword(user, newPassword);

            // Update the user in the database
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
        public async Task<string> CountUsersAsync()
        {
            // Build the query
            IQueryable<ApplicationUser> query = _db.Users.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<string> CountUsersActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Users.CountAsync(x => x.Active.HasValue && x.Active.Value));

        }
        public async Task<string> CountUsersNotActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Users.CountAsync(x => x.Active.HasValue && !x.Active.Value));

        }


    }

}
