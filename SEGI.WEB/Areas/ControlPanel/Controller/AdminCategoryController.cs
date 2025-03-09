using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminCategoryController : AdminBaseController
    {
        public AdminCategoryController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? GeneralSearch)
        {
            var result = await _interfaceServices.categoryService.GetAll(GeneralSearch);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(string? GeneralSearch)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                Categories = await _interfaceServices.categoryService.GetAll(GeneralSearch)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var script = await _interfaceServices.categoryService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialCategory", script); // Return partial view with script data
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateCategoryDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _interfaceServices.categoryService.Create(dto);
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
        public async Task<IActionResult> Update(int id)
        {
            var result = await _interfaceServices.categoryService.Get(id);
            return View(result);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateCategoryDto dto)
        {
            try
            {
                await _interfaceServices.categoryService.Update(dto);
                return Json(new { success = true, responseText = Constant.Response.Success });
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message.ToString() });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.categoryService.Delete(id);
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
                    var entity = await _interfaceServices.categoryService.Get(id);
                    if (entity != null) await _interfaceServices.categoryService.Delete(entity.Id);
                }
                return Json(new { success = true, message = MessageResults.DeleteSuccessResultString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Truncate()
        {
            try
            {
                await _interfaceServices.repositoryService.TruncateTableAsync("Categories");
                return Json(new { success = true, message = "Categories table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

    }
}
