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
    public class AdminProjectController : AdminBaseController
    {
        public AdminProjectController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Querys querys)
        {
            ViewData["Category"] = new SelectList(await _interfaceServices.categoryService.GetAll(null), Constant.ViewBag.id, Constant.ViewBag.Name);
            ViewData["Services"] = new SelectList(await _interfaceServices.generalEngineeringServicesItemService.GetAll(null), Constant.ViewBag.id, Constant.ViewBag.Title);

            var result = await _interfaceServices.projectService.GetAll(querys);
            return Json(new { data = result });
        }
        public async Task<IActionResult> Index(Querys querys)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                Projects = await _interfaceServices.projectService.GetAll(querys)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var script = await _interfaceServices.projectService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialProjects", script); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.projectService.Delete(id);
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
                    var entity = await _interfaceServices.projectService.Get(id);
                    if (entity != null) await _interfaceServices.projectService.Delete(entity.Id);
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
            ViewData["Services"] = new SelectList(await _interfaceServices.generalEngineeringServicesItemService.GetAll(null), Constant.ViewBag.id, Constant.ViewBag.Title);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateProjectDto dto)
        {
            try
            {
                ViewData["Category"] = new SelectList(
    await _interfaceServices.categoryService.GetCategoryList(),
    "Id",  // Corresponds to Value
    "Name" // Corresponds to Text
);
                ViewData["Services"] = new SelectList(await _interfaceServices.generalEngineeringServicesItemService.GetAll(null), Constant.ViewBag.id, Constant.ViewBag.Title);

                if (ModelState.IsValid)
                {
                    await _interfaceServices.projectService.Create(dto);
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
            ViewData[Constant.ViewBag.titleImage] = await _interfaceServices.projectService.Image(Id);

            // Retrieve the list of categories
            var categories = await _interfaceServices.categoryService.GetCategoryList();

            // Retrieve the current categories of the script
            var scriptCategories = await _interfaceServices.projectService.GetProjectCategories(Id);


            // Retrieve the list of categories
            var services = await _interfaceServices.generalEngineeringServicesItemService.GetModelList();

            // Retrieve the current categories of the script
            var projectServices = await _interfaceServices.projectService.GetProjectServices(Id);




            ViewData["Category"] = (await _interfaceServices.categoryService.GetCategoryList())
    .Select(c => new { value = c.Id, text = c.Name })
    .ToList();

            ViewData["Service"] = (await _interfaceServices.generalEngineeringServicesItemService.GetModelList())
                .Select(s => new { value = s.Id, text = s.Title })
                .ToList();
            ViewData["SelectedCategories"] = scriptCategories?.Select(c => c.CategoryId).ToList() ?? new List<int>();
            ViewData["SelectedServices"] = projectServices?.Select(c => c.ServiceId).ToList() ?? new List<int>();



            var model = await _interfaceServices.projectService.Get(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateProjectDto model)
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
                var scriptCategories = await _interfaceServices.projectService.GetProjectCategories(model.Id);
                ViewData["SelectedCategories"] = scriptCategories.Select(c => c.CategoryId).ToList();

                await _interfaceServices.projectService.Update(model);

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
                await _interfaceServices.repositoryService.TruncateTableAsync("Projects");
                return Json(new { success = true, message = "Projects table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }
    }
}
