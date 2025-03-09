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
    public class AdminApplicationCareerController : AdminBaseController
    {
        public AdminApplicationCareerController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Querys querys)
        {
            var result = await _interfaceServices.careerService.GetAllSubmitApplication(querys);
            return Json(new { data = result });
        }
        public async Task<IActionResult> Index(Querys querys)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                ApplyCareerFormModels = await _interfaceServices.careerService.GetAllSubmitApplication(querys)
            };
            return View(interfacesViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) throw new InvalidDataException();

            var model = await _interfaceServices.careerService.DetaileApplication(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialCareerApplication", model); // Return partial view with script data
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.careerService.DeleteApplication(id);
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
                    var entity = await _interfaceServices.careerService.Get(id);
                    if (entity != null) await _interfaceServices.careerService.Delete(entity.Id);
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
                await _interfaceServices.repositoryService.TruncateTableAsync("ApplyCareerFormModels");
                return Json(new { success = true, message = "ApplyCareerFormModels table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }
    }
}
