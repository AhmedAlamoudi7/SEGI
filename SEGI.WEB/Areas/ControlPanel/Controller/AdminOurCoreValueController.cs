using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminOurCoreValueController : AdminBaseController
    {
        public AdminOurCoreValueController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.ourCoreValueService.Image();
            var model = await _interfaceServices.ourCoreValueService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateOurCoreValueDto model)
        {
            await _interfaceServices.ourCoreValueService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
