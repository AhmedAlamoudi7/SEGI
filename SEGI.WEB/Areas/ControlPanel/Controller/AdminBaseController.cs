using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SEGI.Core.Constants;
using SEGI.Services;

namespace SEGI.Web.Areas.ControlPanel.Controllers
{
    [Area(Constant.Area.ControlPanel)]
    [Authorize(Roles = Constant.RolesFilter.SuperAdmin)]
    public class AdminBaseController : Controller
    {
        protected readonly IInterfaceServices _interfaceServices;

        public AdminBaseController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = _interfaceServices.userService.GetUserByUserName(userName);
                ViewBag.FullName = string.Concat(user.FullName);
                ViewBag.UserImg = user.Image;
                ViewBag.UserName = user.Username;
                ViewBag.UserId = user.Id;
                ViewBag.Roles = user.UserType.ToString();
            }
        }
    }
}
