using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SEGI.Services.AboutUsServices;
using SEGI.Services.FileServices;
using SEGI.Services.HomeServices;
using SEGI.Services.RepositoryServices;
using SEGI.Services.Services.CareerServices;
using SEGI.Services.Services.CategoryServices;
using SEGI.Services.Services.ContactUsServicess;
using SEGI.Services.Services.HistoryServices;
using SEGI.Services.Services.NewServices;
using SEGI.Services.Services.ProjectServicess;
using SEGI.Services.Services_Services;
using SEGI.Services.Users;
using SEGI.WEB.Data;
using SEGI.WEB.Services.Services_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Services
{
    public interface IInterfaceServices
    {
        IWebHostEnvironment webHostEnvironment { get; }
        IConfiguration configuration { get; }
        IUserService userService { get; }
        IFileService fileService { get; }
        IRepositoryService repositoryService { get; }
        IWhoWeAreService whoWeAreService { get; }
        ICoreValueService coreValueService { get; }
        ISectorService sectorService { get; }
        ISectorItemService sectorItemService { get; }
        IAnalaticService analaticService { get; }
        IAboutUsBannerService aboutUsBannerService { get; }
        IAboutUsLegacyService aboutUsLegacyService { get; }
        IAboutUsOurVisionService aboutUsOurVisionService { get; }
        IAboutUsOurMissionService aboutUsOurMissionService { get; }
        IOurCoreValueService ourCoreValueService { get; }
        IOurCoreValueItemService ourCoreValueItemService { get; }
        IAboutUsOurHistoryService aboutUsOurHistoryService { get; }
        ITeamService teamService { get; }
        IServicesService servicesService { get; }
        IGeneralEngineeringServicesItemService generalEngineeringServicesItemService { get; }
        IPMCServicesItemService pmcServicesItemService { get; }
        IProcessSafetyStudiesService processSafetyStudiesService { get; }
        IProcessSafetyStudiesItemService processSafetyStudiesItemService { get; }
        ICategoryService categoryService { get; }
        IProjectService projectService { get; }
        ICareerService careerService { get; }
        INewService newService { get; }
        IContactUsService contactUsService { get; }
        IBannerMainService bannerMainService { get; }
        IHistoryService historyService { get; }
        IFixedItemService fixedItemService { get; }

        UserManager<ApplicationUser> userManager { get; }
        RoleManager<IdentityRole> roleManager { get; }
        SignInManager<ApplicationUser> signInManager { get; }
    }
}
