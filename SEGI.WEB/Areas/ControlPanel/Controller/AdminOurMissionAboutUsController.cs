using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminOurMissionAboutUsController : AdminBaseController
    {
        public AdminOurMissionAboutUsController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.aboutUsOurMissionService.Image();
            var model = await _interfaceServices.aboutUsOurMissionService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateOurMissionAboutUssDto model)
        {
            await _interfaceServices.aboutUsOurMissionService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
