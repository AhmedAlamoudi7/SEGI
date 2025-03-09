using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminFixedItemController : AdminBaseController
    {
        public AdminFixedItemController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewData["Image_White"] = await _interfaceServices.fixedItemService.Image_White();
            ViewData["Image_Dark"] = await _interfaceServices.fixedItemService.Image_Dark();

            var model = await _interfaceServices.fixedItemService.Get();
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateFixedItemDto model)
        {
            await _interfaceServices.fixedItemService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }


    }
}
