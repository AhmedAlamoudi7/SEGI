using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminSectorController : AdminBaseController
    {
        public AdminSectorController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.sectorService.Image();
            var model = await _interfaceServices.sectorService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateSectorDto model)
        {
            await _interfaceServices.sectorService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
