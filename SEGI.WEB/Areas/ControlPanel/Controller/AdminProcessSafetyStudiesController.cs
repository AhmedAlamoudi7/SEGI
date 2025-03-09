using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminProcessSafetyStudiesController : AdminBaseController
    {
        public AdminProcessSafetyStudiesController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.processSafetyStudiesService.Image();
            var model = await _interfaceServices.processSafetyStudiesService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateProcessSafetyStudiesDto model)
        {
            await _interfaceServices.processSafetyStudiesService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
