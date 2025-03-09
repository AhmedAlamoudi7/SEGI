using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Helpers;
using SEGI.Core.ViewModels;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Core.ViewModels
{
    public class InterfaceViewModel
    {
        public IEnumerable<ApplicationUserViewModel> Users { get; set; }
        public IEnumerable<SectorItemViewModel> Sectors { get; set; }
        public IEnumerable<OurCoreValueItemViewModel> OurCoreValueItems { get; set; }
        public IEnumerable<AnalaticViewModel> Analatics { get; set; }
        public IEnumerable<TeamViewModel> Teams { get; set; }
        public IEnumerable<GeneralEngineeringServicesItemViewModel> GeneralEngineeringServicesItems { get; set; }
        public IEnumerable<PMCServicesItemViewModel> PMCServicesItems { get; set; }
        public IEnumerable<ProcessSafetyStudiesItemViewModel> ProcessSafetyStudiesItems { get; set; }
        public IEnumerable<CategoryViewModels> Categories { get; set; }
        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public IEnumerable<CareerViewModel> Careers { get; set; }
        public IEnumerable<NewViewModel> News { get; set; }
        public IEnumerable<ContactUsViewModel> ContactUs { get; set; }
        public IEnumerable<SectorItemViewModel> SectorItems { get; set; }
        public IEnumerable<ArticleViewModel> Article { get; set; }
        public IEnumerable<HistoryViewModel> History { get; set; }
        public IEnumerable<ApplyCareerFormModelViewModel> ApplyCareerFormModels { get; set; }

        public ProcessSafetyStudiesViewModel ProcessSafetyStudies { get; set; }
        public WhoWeAreViewModel WhoWeAre { get; set; }
        public FixedItemViewModel FixedItem { get; set; }

        public CoreValueViewModel CoreValue { get; set; }
        public SectorViewModel Sector { get; set; }
        public AboutUsBannerViewModel BannerAboutUs { get; set; }
        public BannerMainViewModel BannerMain { get; set; }
        public LegacyAboutUsViewModel LegacyAboutUs { get; set; }
        public AboutUsOurMissionViewModel OurMission { get; set; }
        public AboutUsOurVisionViewModel OurVision { get; set; }
        public OurCoreValueViewModel OurCoreValue { get; set; }
        public AboutUsOurHistoryViewModel AboutUsOurHistory { get; set; }
        public ProjectViewModel Project { get; set; }
        public NewViewModel New { get; set; }
        public CareerViewModel Career { get; set; }
        public Querys Querys { get; set; }

        public UpdateApplicationUserDto UpdateApplicationUserDto { get; set; }
        public CreateContactUsDto CreateContactUsDto { get; set; }
        public CreateApplyCareerFormModelDto CreateApplyCareerFormModel { get; set; }

        public ChangePasswordDto ChangePasswordDto { get; set; }


        public string CountUsers { get; set; }
        public string CountUsersActive { get; set; }
        public string CountUsersNotActive { get; set; }

        public string CountCareers { get; set; }
        public string CountCareersActive { get; set; }
        public string CountCareersNotActive { get; set; }

        public string CountNews { get; set; }
        public string CountProjects { get; set; }


        public string CountSectors { get; set; }
        public string CountServices { get; set; }

    }
}
