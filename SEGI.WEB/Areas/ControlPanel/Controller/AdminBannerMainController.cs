using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminBannerMainController : AdminBaseController
    {
        public AdminBannerMainController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id, BannerPageType BannerPageType)
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.servicesService.Image(Id);
            var model = await _interfaceServices.bannerMainService.Get(Id, BannerPageType);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateBannerMainDto model)
        {
            await _interfaceServices.bannerMainService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });
        }

    }
}
