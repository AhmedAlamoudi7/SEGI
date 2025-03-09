using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEGI.Data;
using SEGI.Core.Helpers;
using SEGI.Core.ViewModels;
using SEGI.WEB.Data;
using SEGI.Services.Users;
using SEGI.Services.RepositoryServices;
using SEGI.Services.FileServices;
using SEGI.Services.HomeServices;
using SEGI.Services.AboutUsServices;
using SEGI.WEB.Services.Services_Services;
using SEGI.Services.Services_Services;
using SEGI.Services.Services.CategoryServices;
using SEGI.Services.Services.ProjectServicess;
using SEGI.Services.Services.CareerServices;
using SEGI.Services.Services.NewServices;
using SEGI.Services.Services.ContactUsServicess;
using SEGI.Services.Services.HistoryServices;

namespace SEGI.Services
{
    public class InterfaceServices : IInterfaceServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly MailSettings _mailSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        public InterfaceServices(
           IMapper mapper,
           ApplicationDbContext db,
           IWebHostEnvironment env,
           UserManager<ApplicationUser> user_Manager,
           RoleManager<IdentityRole> role_Manager,
           SignInManager<ApplicationUser> signIn_Manager,
           IHttpClientFactory httpClientFactory,
           IConfiguration configuration,
           IOptions<MailSettings> mailSettings,
           IHttpContextAccessor httpContextAccessor,
           IMemoryCache cache
                )
        {
            _mapper = mapper;
            _db = db;
            _env = env;
            _userManager = user_Manager;
            _roleManager = role_Manager;
            _signInManager = signIn_Manager;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _mailSettings = mailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;



            signInManager = _signInManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
            userManager = _userManager;


            repositoryService = new RepositoryService(_db);
            fileService = new FileService(_env);
            userService = new UserService(_db, _mapper, _userManager, _roleManager, fileService);
            whoWeAreService = new WhoWeAreService(_db, _mapper, fileService);
            coreValueService = new CoreValueService(_db, _mapper, fileService);
            sectorService = new SectorService(_db, _mapper, fileService);
            sectorItemService = new SectorItemService(_db, _mapper, fileService);
            analaticService = new AnalaticService(_db, _mapper);
            aboutUsBannerService = new AboutUsBannerService(_db, _mapper, fileService);
            aboutUsLegacyService = new AboutUsLegacyService(_db, _mapper, fileService);
            aboutUsOurVisionService = new AboutUsOurVisionService(_db, _mapper, fileService);
            aboutUsOurMissionService = new AboutUsOurMissionService(_db, _mapper, fileService);
            ourCoreValueItemService = new OurCoreValueItemService(_db, _mapper, fileService);
            ourCoreValueService = new OurCoreValueService(_db, _mapper, fileService);
            aboutUsOurHistoryService = new AboutUsOurHistoryService(_db, _mapper, fileService);
            teamService = new TeamService(_db, _mapper, fileService);
            servicesService = new ServicesService(_db, _mapper, fileService);
            generalEngineeringServicesItemService = new GeneralEngineeringServicesItemService(_db, _mapper, fileService);
            pmcServicesItemService = new PMCServicesItemService(_db, _mapper, fileService);
            processSafetyStudiesService = new ProcessSafetyStudiesService(_db, _mapper, fileService);
            processSafetyStudiesItemService = new ProcessSafetyStudiesItemService(_db, _mapper, fileService);
            categoryService = new CategoryService(_db, _mapper, fileService);
            projectService = new SEGI.Services.Services.ProjectServicess.ProjectService(_db, _mapper, fileService);
            careerService = new CareerService(_db, _mapper, fileService);
            newService = new NewService(_db, _mapper, fileService);
            contactUsService = new ContactUsService(_db, _mapper, fileService);
            bannerMainService = new BannerMainService(_db, _mapper, fileService);
            historyService = new HistoryService(_db, _mapper, fileService);
            fixedItemService = new FixedItemService(_db, _mapper, fileService);

        }
        public IFileService fileService { get; private set; }
        public IConfiguration configuration { get; private set; }
        public IUserService userService { get; set; }
        public IWebHostEnvironment webHostEnvironment { get; private set; }
        public IRepositoryService repositoryService { get; private set; }
        public IWhoWeAreService whoWeAreService { get; private set; }
        public ICoreValueService coreValueService { get; private set; }
        public ISectorService sectorService { get; private set; }
        public ISectorItemService sectorItemService { get; private set; }
        public IAnalaticService analaticService { get; private set; }
        public IAboutUsBannerService aboutUsBannerService { get; private set; }
        public IAboutUsLegacyService aboutUsLegacyService { get; private set; }
        public IAboutUsOurVisionService aboutUsOurVisionService { get; private set; }
        public IAboutUsOurMissionService aboutUsOurMissionService { get; private set; }
        public IOurCoreValueService ourCoreValueService { get; private set; }
        public IOurCoreValueItemService ourCoreValueItemService { get; private set; }
        public IAboutUsOurHistoryService aboutUsOurHistoryService { get; private set; }
        public ITeamService teamService { get; private set; }
        public IServicesService servicesService { get; private set; }
        public IGeneralEngineeringServicesItemService generalEngineeringServicesItemService { get; private set; }
        public IPMCServicesItemService pmcServicesItemService { get; private set; }
        public IProcessSafetyStudiesService processSafetyStudiesService { get; private set; }
        public IProcessSafetyStudiesItemService processSafetyStudiesItemService { get; private set; }
        public ICategoryService categoryService { get; private set; }
        public IProjectService projectService { get; private set; }
        public ICareerService careerService { get; private set; }
        public INewService newService { get; private set; }
        public IContactUsService contactUsService { get; private set; }
        public IBannerMainService bannerMainService { get; private set; }
        public IHistoryService historyService { get; private set; }
        public IFixedItemService fixedItemService { get; private set; }

        public UserManager<ApplicationUser> userManager { get; private set; }
        public RoleManager<IdentityRole> roleManager { get; private set; }
        public SignInManager<ApplicationUser> signInManager { get; private set; }

    }
}
