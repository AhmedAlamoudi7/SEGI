using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.ViewModels;
using SEGI.Services;


namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    [Authorize(Roles = Constant.RolesFilter.SuperAdminAndAdmin)]
    public class AdminHomeController : AdminBaseController
    {
        public AdminHomeController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        public async Task<IActionResult> Index()
        {
            var interfacevm = new InterfaceViewModel
            {
                CountUsers = await _interfaceServices.userService.CountUsersAsync(),
                CountUsersActive = await _interfaceServices.userService.CountUsersActiveAsync(),
                CountUsersNotActive = await _interfaceServices.userService.CountUsersNotActiveAsync(),
                CountCareers = await _interfaceServices.careerService.CountCareersAsync(),
                CountCareersActive = await _interfaceServices.careerService.CountCareersActiveAsync(),
                CountCareersNotActive = await _interfaceServices.careerService.CountCareersNotActiveAsync(),

                CountProjects = await _interfaceServices.projectService.CountProjectsAsync(),
                CountServices = await _interfaceServices.generalEngineeringServicesItemService.CountGeneralEngineeringServicesItemsAsync(),
                CountSectors = await _interfaceServices.sectorItemService.CountSectorItemsAsync(),
                CountNews = await _interfaceServices.newService.CountNewsAsync(),
                ContactUs = await _interfaceServices.contactUsService.GetAll(null),
                ApplyCareerFormModels = await _interfaceServices.careerService.GetAllSubmitApplication(null),


            };

            return View(interfacevm);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}