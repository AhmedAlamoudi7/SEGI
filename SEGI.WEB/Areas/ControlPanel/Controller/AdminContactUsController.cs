using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminContactUsController : AdminBaseController
    {
        public AdminContactUsController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? GeneralSearch)
        {
            var result = await _interfaceServices.contactUsService.GetAll(GeneralSearch);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(string? GeneralSearch)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                ContactUs = await _interfaceServices.contactUsService.GetAll(GeneralSearch)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var script = await _interfaceServices.contactUsService.Detaile(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialContactUs", script); // Return partial view with script data
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateContactUsDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _interfaceServices.contactUsService.Create(dto);
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
            var result = await _interfaceServices.contactUsService.Get(id);
            return View(result);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateContactUsDto dto)
        {
            try
            {
                await _interfaceServices.contactUsService.Update(dto);
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
            await _interfaceServices.contactUsService.Delete(id);
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
                    var entity = await _interfaceServices.contactUsService.Get(id);
                    if (entity != null) await _interfaceServices.contactUsService.Delete(entity.Id);
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
                await _interfaceServices.repositoryService.TruncateTableAsync("ContactUss");
                return Json(new { success = true, message = "ContactUs table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

    }
}
