using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminHistoryController : AdminBaseController
    {
        public AdminHistoryController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Querys querys)
        {
            var result = await _interfaceServices.historyService.GetAll(querys);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(Querys querys)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                History = await _interfaceServices.historyService.GetAll(querys)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var model = await _interfaceServices.historyService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialHistory", model); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.historyService.Delete(id);
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
                    var entity = await _interfaceServices.historyService.Get(id);
                    if (entity != null) await _interfaceServices.historyService.Delete(entity.Id);
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
        public async Task<JsonResult> Create(CreateHistoryDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _interfaceServices.historyService.Create(dto);
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
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.historyService.Image(Id);
            var model = await _interfaceServices.historyService.Get(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UpdateHistoryDto model)
        {
            await _interfaceServices.historyService.Update(model);
            return Json(new { success = true, responseText = Constant.Response.Success });

        }
        [HttpPost]
        public async Task<IActionResult> Truncate()
        {
            try
            {
                await _interfaceServices.repositoryService.TruncateTableAsync("Histories");
                return Json(new { success = true, message = "Histories table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

    }
}
