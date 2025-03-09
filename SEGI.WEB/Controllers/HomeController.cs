using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using SEGI.Core.Constants;
using SEGI.Core.ViewModels;
using SEGI.Services;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;
using Slugify;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SEGI.WEB.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IInterfaceServices _interfaceServices;

        public HomeController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }


        public async Task<IActionResult> Index()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                WhoWeAre = await _interfaceServices.whoWeAreService.Detaile(),
                CoreValue = await _interfaceServices.coreValueService.Detaile(),
                Sector = await _interfaceServices.sectorService.Detaile(),
                SectorItems = await _interfaceServices.sectorItemService.GetModelList(),
                GeneralEngineeringServicesItems = await _interfaceServices.generalEngineeringServicesItemService.GetModelList(),
                Analatics = await _interfaceServices.analaticService.GetModelList(),
                News = await _interfaceServices.newService.GetModelList(),
                BannerMain = await _interfaceServices.bannerMainService.DetaileHome(),
            };
            return View(interfacevm);
        }
        public async Task<IActionResult> AboutUs()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                WhoWeAre = await _interfaceServices.whoWeAreService.Detaile(),
                BannerAboutUs = await _interfaceServices.aboutUsBannerService.Detaile(),
                LegacyAboutUs = await _interfaceServices.aboutUsLegacyService.Detaile(),
                OurVision = await _interfaceServices.aboutUsOurVisionService.Detaile(),
                OurMission = await _interfaceServices.aboutUsOurMissionService.Detaile(),
                OurCoreValue = await _interfaceServices.ourCoreValueService.Detaile(),
                OurCoreValueItems = await _interfaceServices.ourCoreValueItemService.GetModelList(),
                Teams = await _interfaceServices.teamService.GetModelList(),
                AboutUsOurHistory = await _interfaceServices.aboutUsOurHistoryService.Detaile(),
            };
            return View(interfacevm);
        }
        public async Task<IActionResult> Services()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                GeneralEngineeringServicesItems = await _interfaceServices.generalEngineeringServicesItemService.GetModelList(),
                Article = await _interfaceServices.servicesService.GetModelList(),
                PMCServicesItems = await _interfaceServices.pmcServicesItemService.GetModelList(),
                ProcessSafetyStudiesItems = await _interfaceServices.processSafetyStudiesItemService.GetModelList(),
                ProcessSafetyStudies = await _interfaceServices.processSafetyStudiesService.Detaile(),
            };
            return View(interfacevm);
        }
        public async Task<IActionResult> News(Querys querys, int pageNumber = 1, int pageSize = 12)
        {

            var allNews = await _interfaceServices.newService.GetAll(querys) ?? new List<NewViewModel>(); // Ensure it's never null

            // Paginate the results
            var paginatedProjects = allNews
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Categories = await _interfaceServices.categoryService.GetCategoryList(),
                News = paginatedProjects
            };

            // Pass pagination details to the view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(allNews.Count / (double)pageSize);

            return View(interfacevm);
        }
        public async Task<IActionResult> Projects(Querys querys, int pageNumber = 1, int pageSize = 12)
        {
            var allProjects = await _interfaceServices.projectService.GetAll(querys) ?? new List<ProjectViewModel>(); // Ensure it's never null
            querys ??= new Querys(); // Ensure it's not null

            // Paginate the results
            var paginatedProjects = allProjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                Projects = paginatedProjects,
                Categories = await _interfaceServices.categoryService.GetCategoryList(),
                GeneralEngineeringServicesItems = await _interfaceServices.generalEngineeringServicesItemService.GetModelList(),
                Querys = querys // Pass the query object back to the view

            };

            // Pass pagination details to the view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(allProjects.Count / (double)pageSize);

            return View(interfacevm);
        }
        public async Task<IActionResult> Projects_Details(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var visitedKey = $"Projects_Visited_{id}";
            if (HttpContext.Session.GetString(visitedKey) == null)
            {
                await _interfaceServices.projectService.ViewCountIncrees(id);
                HttpContext.Session.SetString(visitedKey, "true");
            }
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                Projects = await _interfaceServices.projectService.GetAll(null),
                Categories = await _interfaceServices.categoryService.GetCategoryList(),
                GeneralEngineeringServicesItems = await _interfaceServices.generalEngineeringServicesItemService.GetModelList(),
                Project = await _interfaceServices.projectService.Detaile(id),
            };
            return View(interfacevm);

        }
        //[Route("news/{id}/{slug}")]
        public async Task<IActionResult> News_Details(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid News ID.");

            var newsDetails = await _interfaceServices.newService.Detaile(id);
            if (newsDetails == null)
                return NotFound("News item not found.");

            var visitedKey = $"News_Visited_{id}";
            if (HttpContext.Session.GetString(visitedKey) == null)
            {
                await _interfaceServices.newService.ViewCountIncrees(id);
                HttpContext.Session.SetString(visitedKey, "true");
            }

            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Categories = await _interfaceServices.categoryService.GetCategoryList(),
                New = newsDetails,
                News = await _interfaceServices.newService.GetModelList(),
            };

            ViewData["Title"] = newsDetails.Title; // SEO-friendly Page Title

            return View(interfacevm);
        }
        public async Task<IActionResult> Careers_Details(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            ViewBag.domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var visitedKey = $"Careers_Visited_{id}";
            if (HttpContext.Session.GetString(visitedKey) == null)
            {
                await _interfaceServices.careerService.ViewCountIncrees(id);
                HttpContext.Session.SetString(visitedKey, "true");
            }
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Career = await _interfaceServices.careerService.Detaile(id),
                Careers = await _interfaceServices.careerService.GetModelList(),

            };
            return View(interfacevm);

        }
        public async Task<IActionResult> Careers(Querys querys)
        {
            querys ??= new Querys(); // Ensure it's not null

            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                Careers = await _interfaceServices.careerService.GetAll(querys),
                Categories = await _interfaceServices.categoryService.GetCategoryList(),
                Querys = querys // Pass the query object back to the view

            };
            return View(interfacevm);
        }
        public async Task<IActionResult> Sectors()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Sector = await _interfaceServices.sectorService.Detaile(),
                SectorItems = await _interfaceServices.sectorItemService.GetModelList()
            };
            return View(interfacevm);
        }
        public async Task<IActionResult> Sustinabilty()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                BannerMain = await _interfaceServices.bannerMainService.DetaileSustainability(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
            };
            return View(interfacevm);
        }
        public async Task<IActionResult> ContactUs()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
            };
            return View(interfacevm);
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(CreateContactUsDto dto)
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
            };

            if (ModelState.IsValid)
            {
                var result = await _interfaceServices.contactUsService.Create(dto);
                if (result != null && result > 0)
                {
                    TempData["SuccessMessage"] = "Your message has been sent successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try again!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input. Please check your data!";
            }

            return View(interfacevm);
        }
        public async Task<IActionResult> ProjectApplyForm(int id)
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                Career = await _interfaceServices.careerService.Detaile(id),
            };
            return View(interfacevm);
        }
        [HttpPost]
        public async Task<IActionResult> ProjectApplyForm(CreateApplyCareerFormModelDto dto)
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                Career = await _interfaceServices.careerService.Detaile(dto.CareerId),
                CreateApplyCareerFormModel = dto
            };

            if (ModelState.IsValid)
            {
                var result = await _interfaceServices.careerService.SubmitApplication(dto);
                if (result != null && result > 0)
                {
                    TempData["SuccessMessage"] = "Your application has been submitted successfully! Our team will review it and contact you soon. Thank you for choosing us!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try again!";
                    return View(interfacevm);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input. Please check your data!";
                return View(interfacevm);

            }

            return View(interfacevm);
        }
        public async Task<IActionResult> History()
        {
            var interfacevm = new InterfaceViewModel
            {
                FixedItem = await _interfaceServices.fixedItemService.Detaile(),
                Article = await _interfaceServices.servicesService.GetModelList() ?? new List<ArticleViewModel>(), // Ensure it's never null
                History = await _interfaceServices.historyService.GetModelList(),
            };
            return View(interfacevm);
        }
    }
}
