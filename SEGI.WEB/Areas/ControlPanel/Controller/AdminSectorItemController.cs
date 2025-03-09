using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminSectorItemController : AdminBaseController
    {
        public AdminSectorItemController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? GeneralSearch)
        {
            var result = await _interfaceServices.sectorItemService.GetAll(GeneralSearch);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(string? GeneralSearch)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                Sectors = await _interfaceServices.sectorItemService.GetAll(GeneralSearch)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var script = await _interfaceServices.sectorItemService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialSectorItems", script); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.sectorItemService.Delete(id);
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
                    var entity = await _interfaceServices.sectorItemService.Get(id);
                    if (entity != null) await _interfaceServices.sectorItemService.Delete(entity.Id);
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
        public async Task<JsonResult> Create(CreateSectorItemDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _interfaceServices.sectorItemService.Create(dto);
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
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.sectorItemService.Image(Id);
            ViewData[Constant.ViewBag.titleIcon] = await _interfaceServices.sectorItemService.Icon(Id);

            var model = await _interfaceServices.sectorItemService.Get(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UpdateSectorItemDto model)
        {
            await _interfaceServices.sectorItemService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }
        [HttpPost]
        public async Task<IActionResult> Truncate()
        {
            try
            {
                await _interfaceServices.repositoryService.TruncateTableAsync("SectorItems");
                return Json(new { success = true, message = "SectorItems table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

    }
}
