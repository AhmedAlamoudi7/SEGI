using AutoMapper;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services.AboutUsServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //user
            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                   .ForMember(x => x.CreatedAt, x =>
                   x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")))
                   .ForMember(x => x.Image, x =>
                   x.MapFrom(x => x.ImageUrl))
                   .ForMember(x => x.UserType, x =>
                   x.MapFrom(x => x.UserType.ToString()));


            CreateMap<CreateApplicationUserDto, ApplicationUser>()
                .ForMember(src => src.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateApplicationUserDto, ApplicationUser>()
                .ForMember(src => src.ImageUrl, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(src => src.Image, opt => opt.Ignore());


            // Who We Are
            CreateMap<WhoWeAre, WhoWeAreViewModel>();
            CreateMap<UpdateWhoWeAreDto, WhoWeAre>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());

            // Core Value
            CreateMap<CoreValue, CoreValueViewModel>();
            CreateMap<UpdateCoreValueDto, CoreValue>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Sector
            CreateMap<Sector, SectorViewModel>();
            CreateMap<UpdateSectorDto, Sector>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Sector Item
            CreateMap<SectorItem, SectorItemViewModel>();
            CreateMap<CreateSectorItemDto, SectorItem>();
            CreateMap<UpdateSectorItemDto, SectorItem>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ForMember(src => src.Icon, opt => opt.Ignore())
                 .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore())
                 .ForMember(src => src.Icon, opt => opt.Ignore());


            // Analitic
            CreateMap<Analitic, AnalaticViewModel>();
            CreateMap<CreateAnalaticDto, Analitic>();
            CreateMap<UpdateAnalaticDto, Analitic>()
                 .ReverseMap();


            // BannerAboutUs
            CreateMap<BannerAboutUs, AboutUsBannerViewModel>();
            CreateMap<UpdateBannerAboutUssDto, BannerAboutUs>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());

            // Legacy About Us
            CreateMap<LegacyAboutUs, AboutUsLegacyViewModel>();
            CreateMap<UpdateLegacyAboutUssDto, LegacyAboutUs>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Our Vision About Us
            CreateMap<OurVisionAboutUs, AboutUsOurVisionViewModel>();
            CreateMap<UpdateOurVisionAboutUssDto, OurVisionAboutUs>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());



            // Our Mission About Us
            CreateMap<OurMissionAboutUs, AboutUsOurMissionViewModel>();
            CreateMap<UpdateOurMissionAboutUssDto, OurMissionAboutUs>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Our Core Value
            CreateMap<OurCoreValue, OurCoreValueViewModel>();
            CreateMap<UpdateOurCoreValueDto, OurCoreValue>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Our Core Value Item
            CreateMap<OurCoreValueItem, OurCoreValueItemViewModel>();
            CreateMap<CreateOurCoreValueItemDto, OurCoreValueItem>();
            CreateMap<UpdateOurCoreValueItemDto, OurCoreValueItem>()
                   .ReverseMap();

            // Our History About Us
            CreateMap<OurHistoryAboutUs, AboutUsOurHistoryViewModel>();
            CreateMap<UpdateOurHistoryAboutUssDto, OurHistoryAboutUs>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // Team
            CreateMap<Team, TeamViewModel>()
                .ForMember(x => x.TeamType, x =>
                   x.MapFrom(x => x.TeamType.ToString()));

            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                  .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore());

            CreateMap<Article, ArticleViewModel>();
            CreateMap<UpdateArticleDto, Article>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                  .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore());


            // GeneralEngineeringServicesItem
            CreateMap<GeneralEngineeringServicesItem, GeneralEngineeringServicesItemViewModel>();
            CreateMap<CreateGeneralEngineeringServicesItemDto, GeneralEngineeringServicesItem>();
            CreateMap<UpdateGeneralEngineeringServicesItemDto, GeneralEngineeringServicesItem>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                  .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore());


            // PMCServicesItem
            CreateMap<PMCServicesItem, PMCServicesItemViewModel>();
            CreateMap<CreatePMCServicesItemDto, PMCServicesItem>();
            CreateMap<UpdatePMCServicesItemDto, PMCServicesItem>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                  .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore());


            // ProcessSafetyStudies
            CreateMap<ProcessSafetyStudies, ProcessSafetyStudiesViewModel>();
            CreateMap<UpdateProcessSafetyStudiesDto, ProcessSafetyStudies>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Image, opt => opt.Ignore());


            // ProcessSafetyStudiesItem
            CreateMap<ProcessSafetyStudiesItem, ProcessSafetyStudiesItemViewModel>();
            CreateMap<CreateProcessSafetyStudiesItemDto, ProcessSafetyStudiesItem>();
            CreateMap<UpdateProcessSafetyStudiesItemDto, ProcessSafetyStudiesItem>()
                   .ReverseMap();

            // Category
            CreateMap<Category, CategoryViewModels>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                   .ReverseMap();



            // Project to ProjectViewModel
            CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.Categories, opt =>
                    opt.MapFrom(src => src.ProjectCategories.Select(pc => pc.Category.Name).ToList() ?? new List<string>()))
                .ForMember(dest => dest.Services, opt =>
                    opt.MapFrom(src => src.ProjectServices.Select(ps => ps.GeneralEngineeringServicesItem.Title).ToList() ?? new List<string>()))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.ToString("yyyy")));


            // CreateProjectDto to Project
            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new ProjectCategory { CategoryId = id })))
                .ForMember(dest => dest.ProjectServices, opt => opt.MapFrom(src =>
                    src.ServiceIds.Select(id => new ProjectService { GeneralEngineeringServicesItemId = id })));

            // UpdateProjectDto to Project and ReverseMap
            CreateMap<UpdateProjectDto, Project>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                //.ForMember(dest => dest.Image_Cover, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new ProjectCategory { CategoryId = id })))
                 .ForMember(dest => dest.ProjectServices, opt => opt.MapFrom(src =>
                    src.ServiceIds.Select(id => new ProjectService { GeneralEngineeringServicesItemId = id })))
                .ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                //.ForMember(dest => dest.Image_Cover, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src =>
                    src.ProjectCategories.Select(sc => sc.CategoryId)))
                .ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src =>
                    src.ProjectServices.Select(sc => sc.GeneralEngineeringServicesItemId)))
                .ReverseMap();




            // Career to CareerViewModel
            CreateMap<Career, CareerViewModel>()
                 .ForMember(x => x.CreatedAt, x =>
                   x.MapFrom(x => x.CreatedAt.ToString("dddd, dd MMMM yyyy")))
                .ForMember(dest => dest.Categories, opt =>
                    opt.MapFrom(src => src.CareerCategories.Select(pc => pc.Category.Name).ToList() ?? new List<string>()));



            // CreateCareerDto to Career
            CreateMap<CreateCareerDto, Career>()
                 .ForMember(dest => dest.CareerCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new CareerCategory { CategoryId = id })));

            // UpdatCareerDto to Career and ReverseMap
            CreateMap<UpdateCareerDto, Career>()
                 .ForMember(dest => dest.CareerCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new CareerCategory { CategoryId = id })))
                 .ReverseMap()
                 .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src =>
                    src.CareerCategories.Select(sc => sc.CategoryId)));


            // New to NewViewModel
            CreateMap<New, NewViewModel>()
                 .ForMember(x => x.CreatedAt, x =>
                   x.MapFrom(x => x.CreatedAt.ToString("dddd, dd MMMM yyyy")))
                .ForMember(dest => dest.Categories, opt =>
                    opt.MapFrom(src => src.NewCategories.Select(pc => pc.Category.Name).ToList() ?? new List<string>()));



            // CreateCareerDto to Career
            CreateMap<CreateNewDto, New>()
                  .ForMember(dest => dest.Image, opt => opt.Ignore())
                  .ForMember(dest => dest.Image_Cover, opt => opt.Ignore())
                 .ForMember(dest => dest.NewCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new NewCategory { CategoryId = id })));

            // UpdatNewDto to New and ReverseMap
            CreateMap<UpdateNewDto, New>()
                  .ForMember(dest => dest.Image, opt => opt.Ignore())
                  .ForMember(dest => dest.Image_Cover, opt => opt.Ignore())
                 .ForMember(dest => dest.NewCategories, opt => opt.MapFrom(src =>
                    src.CategoryIds.Select(id => new NewCategory { CategoryId = id })))
                 .ReverseMap()
                  .ForMember(dest => dest.Image, opt => opt.Ignore())
                  .ForMember(dest => dest.Image_Cover, opt => opt.Ignore())
                 .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src =>
                    src.NewCategories.Select(sc => sc.CategoryId)));


            // Contact Us
            CreateMap<ContactUs, ContactUsViewModel>();
            CreateMap<CreateContactUsDto, ContactUs>();
            CreateMap<UpdateContactUsDto, ContactUs>()
                   .ReverseMap();


            // Contact Us
            CreateMap<BannerMain, BannerMainViewModel>();
            CreateMap<UpdateBannerMainDto, BannerMain>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                  .ReverseMap()
                 .ForMember(src => src.Image, opt => opt.Ignore());


            // CreateCareerDto to Career
            CreateMap<History, HistoryViewModel>();

            CreateMap<CreateHistoryDto, History>()
               .ForMember(dest => dest.Image, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Image, opt => opt.Ignore());

            // UpdatNewDto to New and ReverseMap
            CreateMap<UpdateHistoryDto, History>()
                  .ForMember(dest => dest.Image, opt => opt.Ignore())
                 .ReverseMap()
                  .ForMember(dest => dest.Image, opt => opt.Ignore());

            // BannerAboutUs
            CreateMap<FixedItem, FixedItemViewModel>();
            CreateMap<UpdateFixedItemDto, FixedItem>()
                .ForMember(src => src.Logo_White, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Logo_White, opt => opt.Ignore())
               .ForMember(src => src.Logo_Dark, opt => opt.Ignore())
                 .ReverseMap().ForMember(src => src.Logo_Dark, opt => opt.Ignore());
            // ApplyCareerFormModel to ApplyCareerFormModels
            CreateMap<ApplyCareerFormModel, ApplyCareerFormModelViewModel>()
                 .ForMember(x => x.CreatedAt, x =>
                   x.MapFrom(x => x.CreatedAt.ToString("dddd, dd MMMM yyyy")));




            // CreateApplyCareerFormModelDto to ApplyCareerFormModel
            CreateMap<CreateApplyCareerFormModelDto, ApplyCareerFormModel>()
                    .ForMember(dest => dest.Resume, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Resume, opt => opt.Ignore());


        }
    }

}
