using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminLegacyAboutUsController : AdminBaseController
    {
        public AdminLegacyAboutUsController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.aboutUsLegacyService.Image();
            var model = await _interfaceServices.aboutUsLegacyService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateLegacyAboutUssDto model)
        {
            await _interfaceServices.aboutUsLegacyService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
