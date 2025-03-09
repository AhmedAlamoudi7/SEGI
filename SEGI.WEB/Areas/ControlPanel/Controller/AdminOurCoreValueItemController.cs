using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminOurCoreValueItemController : AdminBaseController
    {
        public AdminOurCoreValueItemController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? GeneralSearch)
        {
            var result = await _interfaceServices.ourCoreValueItemService.GetAll(GeneralSearch);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(string? GeneralSearch)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                OurCoreValueItems = await _interfaceServices.ourCoreValueItemService.GetAll(GeneralSearch)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var script = await _interfaceServices.ourCoreValueItemService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialOurCoreValueItems", script); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.ourCoreValueItemService.Delete(id);
            return Json(new { success = true, message = MessageResults.DeleteSuccessResultString() });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    // Delete each record based on its ID
                    var entity = await _interfaceServices.ourCoreValueItemService.Get(id);
                    if (entity != null) await _interfaceServices.ourCoreValueItemService.Delete(entity.Id);
                }
                return Json(new { success = true, message = MessageResults.DeleteSuccessResultString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateOurCoreValueItemDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _interfaceServices.ourCoreValueItemService.Create(dto);
                    return Json(new
                    {
                        success = true,
                        responseText = Constant.Response.Success
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message.ToString() });
            }
            return Json(new { success = false, responseText = Constant.Response.Error });
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {

            var model = await _interfaceServices.ourCoreValueItemService.Get(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UpdateOurCoreValueItemDto model)
        {
            await _interfaceServices.ourCoreValueItemService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }
        [HttpPost]
        public async Task<IActionResult> Truncate()
        {
            try
            {
                await _interfaceServices.repositoryService.TruncateTableAsync("OurCoreValueItems");
                return Json(new { success = true, message = "OurCoreValueItem table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

    }
}
