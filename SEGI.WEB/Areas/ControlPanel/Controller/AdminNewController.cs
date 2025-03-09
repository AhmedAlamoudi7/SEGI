using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminNewController : AdminBaseController
    {
        public AdminNewController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Querys querys)
        {
            ViewData["Category"] = new SelectList(await _interfaceServices.categoryService.GetAll(null), Constant.ViewBag.id, Constant.ViewBag.Name);

            var result = await _interfaceServices.newService.GetAll(querys);
            return Json(new { data = result });
        }
        public async Task<IActionResult> Index(Querys querys)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                News = await _interfaceServices.newService.GetAll(querys)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var model = await _interfaceServices.newService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialNews", model); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.newService.Delete(id);
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
                    var entity = await _interfaceServices.newService.Get(id);
                    if (entity != null) await _interfaceServices.newService.Delete(entity.Id);
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
            ViewData["Category"] = new SelectList(
    await _interfaceServices.categoryService.GetCategoryList(),
    "Id",  // Corresponds to Value
    "Name" // Corresponds to Text
);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateNewDto dto)
        {
            try
            {
                ViewData["Category"] = new SelectList(
    await _interfaceServices.categoryService.GetCategoryList(),
    "Id",  // Corresponds to Value
    "Name" // Corresponds to Text
);

                if (ModelState.IsValid)
                {
                    await _interfaceServices.newService.Create(dto);
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

            // Retrieve the list of categories
            var categories = await _interfaceServices.categoryService.GetCategoryList();

            // Retrieve the current categories of the script
            var careerCategories = await _interfaceServices.newService.GetNewCategories(Id);




            ViewData["Category"] = (await _interfaceServices.categoryService.GetCategoryList())
    .Select(c => new { value = c.Id, text = c.Name })
    .ToList();


            ViewData["SelectedCategories"] = careerCategories?.Select(c => c.CategoryId).ToList() ?? new List<int>();



            var model = await _interfaceServices.newService.Get(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateNewDto model)
        {
            try
            {
                ViewData["Category"] = new SelectList(
                await _interfaceServices.categoryService.GetCategoryList(),
                "Id",  // Corresponds to Value
                "Name" // Corresponds to Text
            );
                // Retrieve the list of categories
                var categories = await _interfaceServices.categoryService.GetCategoryList();

                // Retrieve the current categories of the script
                var careerCategories = await _interfaceServices.newService.GetNewCategories(model.Id);
                ViewData["SelectedCategories"] = careerCategories.Select(c => c.CategoryId).ToList();

                await _interfaceServices.newService.Update(model);

                return Json(new { success = true, responseText = Constant.Response.Success });
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message.ToString() });
            }

        }
        [HttpPost]
        public async Task<IActionResult> Truncate()
        {
            try
            {
                await _interfaceServices.repositoryService.TruncateTableAsync("News");
                return Json(new { success = true, message = "News table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }
    }
}
