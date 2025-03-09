using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.ViewModels;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    public class AdminUserController : AdminBaseController
    {
        public AdminUserController(IInterfaceServices interfaceServices) : base(interfaceServices)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(Querys querys)
        {
            var result = await _interfaceServices.userService.GetAll(querys);
            return Json(new { data = result });
        }

        public async Task<IActionResult> Index(Querys querys)
        {
            var interfacesViewModel = new InterfaceViewModel()
            {
                Users = await _interfaceServices.userService.GetAll(querys)
            };

            return View(interfacesViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateApplicationUserDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dto.EmailConfirmed = true;
                    await _interfaceServices.userService.Create(dto);
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
        public async Task<IActionResult> Update(string id)
        {
            var result = await _interfaceServices.userService.GetUpdate(id);
            return View(result);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateApplicationUserDto dto)
        {
            try
            {
                await _interfaceServices.userService.Update(dto);
                return Json(new { success = true, responseText = Constant.Response.Success });
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message.ToString() });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _interfaceServices.userService.Delete(id);
            return Json(new { success = true, message = MessageResults.DeleteSuccessResultString() });
        }

        [HttpPost]
        public async Task<JsonResult> ChangeActive(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "Invalid user ID." });
            }

            try
            {
                await _interfaceServices.userService.ChangeUserActive(id);
                return Json(new { success = true, message = MessageResults.ChangeActiveSuccessResultString() });
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                // _logger.LogError(ex, "Error changing user active status for ID: {Id}", id);
                return Json(new { success = false, message = "An error occurred while changing the user status." });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            string UserId = ViewBag.UserId;
            await _interfaceServices.signInManager.SignOutAsync();

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Redirect(Constant.Links.Login);
            }
        }



        public async Task<IActionResult> Details(string id)
        {
            if (id == null) throw new InvalidDataException();

            var model = await _interfaceServices.userService.DetailesForUser(id); // Or your actual data fetching logic
            return PartialView("_DetailsPartialUser", model); // Return partial view with script data
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<string> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    // Delete each record based on its ID
                    var entity = await _interfaceServices.userService.Get(id);
                    if (entity != null) await _interfaceServices.userService.Delete(entity.Id);
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
                await _interfaceServices.repositoryService.TruncateTableAsync("AspNetUsers");
                return Json(new { success = true, message = "Users table has been truncated successfully." });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{ex.Message}" });
            }
        }

        // Get the view for resetting a user's password
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            var user = await _interfaceServices.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ResetPasswordUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName
            };
            return View(model);
        }

        // Handle password reset form submission
        [HttpPost]
        public async Task<JsonResult> ResetPassword(ResetPasswordUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _interfaceServices.userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return Json(new { success = false, message = "Not Found" });
                }

                // Reset the password directly
                var resetToken = await _interfaceServices.userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _interfaceServices.userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

                if (result.Succeeded)
                {
                    // You can redirect to a success page or display a message
                    return Json(new { success = true, message = MessageResults.ChangeChangeUserPasswordResultString() });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        return Json(new { success = false, message = error.Description });
                    }
                }
            }
            return Json(new { success = true, message = MessageResults.ChangeChangeUserPasswordResultString() });

        }
    }
}