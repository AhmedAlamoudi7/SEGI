using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminWhoWeAreController : AdminBaseController
    {
        public AdminWhoWeAreController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.whoWeAreService.Image();
            var model = await _interfaceServices.whoWeAreService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateWhoWeAreDto model)
        {
            await _interfaceServices.whoWeAreService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
