using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminCoreValueController : AdminBaseController
    {
        public AdminCoreValueController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.coreValueService.Image();
            var model = await _interfaceServices.coreValueService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateCoreValueDto model)
        {
            await _interfaceServices.coreValueService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
