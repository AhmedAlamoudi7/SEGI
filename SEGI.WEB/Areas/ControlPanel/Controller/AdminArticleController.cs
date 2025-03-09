using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminArticleController : AdminBaseController
    {
        public AdminArticleController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int SectionId, PageType PageType)
        {
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.servicesService.Image(SectionId);
            var model = await _interfaceServices.servicesService.Get(SectionId, PageType);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(UpdateArticleDto model)
        {
            await _interfaceServices.servicesService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });
        }

    }
}
