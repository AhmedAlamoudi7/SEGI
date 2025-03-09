using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminOurHistoryAboutUsController : AdminBaseController
    {
        public AdminOurHistoryAboutUsController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.aboutUsOurHistoryService.Image();
            var model = await _interfaceServices.aboutUsOurHistoryService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateOurHistoryAboutUssDto model)
        {
            await _interfaceServices.aboutUsOurHistoryService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
