using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminOurVisionAboutUsController : AdminBaseController
    {
        public AdminOurVisionAboutUsController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.aboutUsOurVisionService.Image();
            var model = await _interfaceServices.aboutUsOurVisionService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateOurVisionAboutUssDto model)
        {
            await _interfaceServices.aboutUsOurVisionService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
