using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;



namespace SEGI.Services.Services.ProjectServicess
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ProjectService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<ProjectViewModel>> GetAll(Querys querys)
        {
            // Ensure querys is not null
            if (querys == null)
            {
                querys = new Querys();
            }

            var query = _db.Projects
              .Include(x => x.ProjectServices)
               .Include(x => x.ProjectCategories)
               .ThenInclude(sc => sc.Category)
               .Include(x => x.ProjectServices).ThenInclude(x => x.GeneralEngineeringServicesItem)

                 .AsQueryable();

            // Apply search filter if GeneralSearch is provided
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.Title.Contains(querys.GeneralSearch));
            }

            // Apply category filter if categoryId is provided
            if (querys.categoryId.HasValue)  // Ensure categoryId is not null
            {
                query = query.Where(x => x.ProjectCategories.Any(sc => sc.CategoryId == querys.categoryId));
            }
            if (querys.serviceId.HasValue)  // Ensure categoryId is not null
            {
                query = query.Where(x => x.ProjectServices.Any(sc => sc.GeneralEngineeringServicesItemId == querys.serviceId));
            }
            var model = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<List<ProjectViewModel>>(model);
        }
        public async Task<List<ProjectViewModel>> GetModelList()
        {
            var model = await _db.Projects
                .Include(x => x.ProjectServices)
                .Include(x => x.ProjectCategories)
               .ThenInclude(sc => sc.Category)
               .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ProjectViewModel>();
            }
            var modelmapper = _mapper.Map<List<ProjectViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateProjectDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            // Delete the old image if a new image is provided

            var model = _mapper.Map<Project>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            await _db.Projects.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateProjectDto dto)
        {
            var model = await _db.Projects
                .Include(x => x.ProjectServices)
                .Include(x => x.ProjectCategories)
               .ThenInclude(sc => sc.Category)
               .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            if (!string.IsNullOrEmpty(model.Image) && dto.Image != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Image);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            var updatedmodel = _mapper.Map<UpdateProjectDto, Project>(dto, model);
            // Update Project categories
            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                // Detach tracked entities to avoid duplicate tracking
                foreach (var entry in _db.ChangeTracker.Entries<ProjectCategory>())
                {
                    if (entry.Entity.ProjectId == model.Id)
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Remove existing ScriptCategories
                var existingCategories = await _db.ProjectCategories
                    .Where(sc => sc.ProjectId == model.Id)
                    .ToListAsync();
                _db.ProjectCategories.RemoveRange(existingCategories);

                // Add new ScriptCategories
                var newCategories = dto.CategoryIds.Select(categoryId => new ProjectCategory
                {
                    ProjectId = model.Id,
                    CategoryId = categoryId
                });

                await _db.ProjectCategories.AddRangeAsync(newCategories);
            }
            // Update Project Services
            if (dto.ServiceIds != null && dto.ServiceIds.Any())
            {
                // Detach tracked entities to avoid duplicate tracking
                foreach (var entry in _db.ChangeTracker.Entries<SEGI.WEB.Data.ProjectService>())
                {
                    if (entry.Entity.ProjectId == model.Id)
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Remove existing ProjectServices
                var existingCategories = await _db.ProjectServices
                    .Where(sc => sc.ProjectId == model.Id)
                    .ToListAsync();
                _db.ProjectServices.RemoveRange(existingCategories);

                // Add new ProjectServices
                var newCategories = dto.ServiceIds.Select(categoryId => new SEGI.WEB.Data.ProjectService
                {
                    ProjectId = model.Id,
                    GeneralEngineeringServicesItemId = categoryId
                });

                await _db.ProjectServices.AddRangeAsync(newCategories);
            }
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.Projects.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.Projects.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _db.Projects.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateProjectDto> Get(int Id)
        {
            var model = await _db.Projects
                .Include(x => x.ProjectServices)
                .Include(x => x.ProjectCategories)
               .ThenInclude(sc => sc.Category)
               .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateProjectDto>(model);
        }
        public async Task<string> CountModelAsync()
        {
            // Build the query
            IQueryable<Project> query = _db.Projects.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<ProjectViewModel> Detaile(int Id)
        {
            var model = await _db.Projects
                 .Include(x => x.ProjectServices)
                .Include(x => x.ProjectCategories)
               .ThenInclude(sc => sc.Category)
               .Include(sc => sc.ProjectServices).ThenInclude(x => x.GeneralEngineeringServicesItem)

               .FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ProjectViewModel>(model);
        }
        // Method to get categories associated with a script
        public async Task<List<ProjectCategoryDto>> GetProjectCategories(int projectId)
        {
            return await _db.ProjectCategories
                .Where(sc => sc.ProjectId == projectId)
                .Select(sc => new ProjectCategoryDto
                {
                    CategoryId = sc.CategoryId,
                    CategoryName = sc.Category.Name
                })
                .ToListAsync();
        }
        public async Task<List<ProjectServicesDto>> GetProjectServices(int projectId)
        {
            return await _db.ProjectServices
                .Where(sc => sc.ProjectId == projectId)
                .Select(sc => new ProjectServicesDto
                {
                    ServiceId = sc.GeneralEngineeringServicesItemId,
                    ServiceTitle = sc.GeneralEngineeringServicesItem.Title
                })
                .ToListAsync();
        }
        public async Task<string> Image(int id)
        {
            var latestmodel = await _db.Projects
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id == id)
              .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }
            return latestmodel.Image;
        }

        public async Task<string> CountProjectsAsync()
        {
            // Build the query
            IQueryable<Project> query = _db.Projects.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<string> CountProjectActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Projects.CountAsync(x => x.IsActive));

        }
        public async Task<string> CountProjectNotActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Projects.CountAsync(x => !x.IsActive));

        }

        public async Task<int> ViewCountIncrees(int Id)
        {
            var model = await _db.Projects.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.ViewCount++;
            _db.Projects.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
    }
}
