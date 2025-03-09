using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace SEGI.Services.Services.CareerServices
{
    public class CareerService : ICareerService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CareerService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<CareerViewModel>> GetAll(Querys querys)
        {
            // Ensure querys is not null
            if (querys == null)
            {
                querys = new Querys();
            }

            var query = _db.Careers
                .Include(x => x.CareerCategories)
                .ThenInclude(sc => sc.Category)
                .AsQueryable();

            // Apply search filter if GeneralSearch is provided
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.Title.Contains(querys.GeneralSearch));
            }

            // Apply category filter if categoryId is provided
            if (querys.categoryId.HasValue)  // Ensure categoryId is not null
            {
                query = query.Where(x => x.CareerCategories.Any(sc => sc.CategoryId == querys.categoryId));
            }

            var model = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
            // Step 1: Map the model
            var modelmapper = _mapper.Map<List<CareerViewModel>>(model);

            // Step 2: Get all CareerIds from the model
            var careerIds = modelmapper.Select(i => i.Id).ToList();

            // Step 3: Fetch application counts in one query
            var appliedCounts = await _db.ApplyCareerFormModels
                .Where(a => careerIds.Contains(a.CareerId))
                .GroupBy(a => a.CareerId)
                .Select(g => new { CareerId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.CareerId, g => g.Count);

            // Step 4: Assign counts efficiently
            foreach (var item in modelmapper)
            {
                item.ApplicationAppliedCount = appliedCounts.TryGetValue(item.Id, out var count) ? count : 0;
            }
            return modelmapper;
        }
        public async Task<List<CareerViewModel>> GetModelList()
        {
            var model = await _db.Careers
                .Include(x => x.CareerCategories)
               .ThenInclude(sc => sc.Category)
               .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<CareerViewModel>();
            }
            var modelmapper = _mapper.Map<List<CareerViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateCareerDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            // Delete the old image if a new image is provided

            var model = _mapper.Map<Career>(dto);
            await _db.Careers.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateCareerDto dto)
        {
            var model = await _db.Careers
                .Include(x => x.CareerCategories)
               .ThenInclude(sc => sc.Category)
               .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedmodel = _mapper.Map<UpdateCareerDto, Career>(dto, model);
            // Update Project categories
            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                // Detach tracked entities to avoid duplicate tracking
                foreach (var entry in _db.ChangeTracker.Entries<CareerCategory>())
                {
                    if (entry.Entity.CareerId == model.Id)
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Remove existing ScriptCategories
                var existingCategories = await _db.CareerCategories
                    .Where(sc => sc.CareerId == model.Id)
                    .ToListAsync();
                _db.CareerCategories.RemoveRange(existingCategories);

                // Add new ScriptCategories
                var newCategories = dto.CategoryIds.Select(categoryId => new CareerCategory
                {
                    CareerId = model.Id,
                    CategoryId = categoryId
                });

                await _db.CareerCategories.AddRangeAsync(newCategories);
            }

            _db.Careers.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.Careers.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _db.Careers.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateCareerDto> Get(int Id)
        {
            var model = await _db.Careers
                 .Include(x => x.CareerCategories)
               .ThenInclude(sc => sc.Category)
               .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateCareerDto>(model);
        }
        public async Task<string> CountModelAsync()
        {
            // Build the query
            IQueryable<Career> query = _db.Careers.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<CareerViewModel> Detaile(int Id)
        {
            var model = await _db.Careers
                 .Include(x => x.CareerCategories)
               .ThenInclude(sc => sc.Category)
               .FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<CareerViewModel>(model);
        }
        // Method to get categories associated with a script
        public async Task<List<CareerCategoryDto>> GetCareerCategories(int projectId)
        {
            return await _db.CareerCategories
                .Where(sc => sc.CareerId == projectId)
                .Select(sc => new CareerCategoryDto
                {
                    CategoryId = sc.CategoryId,
                    CategoryName = sc.Category.Name
                })
                .ToListAsync();
        }
        public async Task<string> CountCareersAsync()
        {
            // Build the query
            IQueryable<Career> query = _db.Careers.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<string> CountCareersActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Careers.CountAsync(x => x.IsActive));

        }
        public async Task<string> CountCareersNotActiveAsync()
        {
            // Handle nullable Active
            return @NumberFormatter.FormatNumber(await _db.Careers.CountAsync(x => !x.IsActive));

        }
        public async Task<List<ApplyCareerFormModelViewModel>> GetAllSubmitApplication(Querys querys)
        {
            // Ensure querys is not null
            if (querys == null)
            {
                querys = new Querys();
            }

            var query = _db.ApplyCareerFormModels
                .Include(x => x.Career)
                 .AsQueryable();

            // Apply search filter if GeneralSearch is provided
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.PositionAppliedFor.Contains(querys.GeneralSearch));
            }
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.FullName.Contains(querys.GeneralSearch));
            }
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.Industry.Contains(querys.GeneralSearch));
            }
            var model = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<List<ApplyCareerFormModelViewModel>>(model);
        }
        public async Task<List<ApplyCareerFormModelViewModel>> GetModelListSubmitApplication()
        {
            var model = await _db.ApplyCareerFormModels
                .Include(x => x.Career)
               .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ApplyCareerFormModelViewModel>();
            }
            var modelmapper = _mapper.Map<List<ApplyCareerFormModelViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> SubmitApplication(CreateApplyCareerFormModelDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            // Delete the old image if a new image is provided

            var model = _mapper.Map<ApplyCareerFormModel>(dto);
            if (dto.Resume != null)
            {
                model.Resume = await _fileService.SaveFile(dto.Resume, FolderNames.ImagesFolder);
            }
            await _db.ApplyCareerFormModels.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<ApplyCareerFormModelViewModel> DetaileApplication(int Id)
        {
            var model = await _db.ApplyCareerFormModels
                 .Include(x => x.Career)
               .FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ApplyCareerFormModelViewModel>(model);
        }
        public async Task<int> DeleteApplication(int Id)
        {
            var model = await _db.ApplyCareerFormModels.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            if (!string.IsNullOrEmpty(model.Resume) && model.Resume != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Resume);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            model.IsDelete = true;
            _db.ApplyCareerFormModels.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> ViewCountIncrees(int Id)
        {
            var model = await _db.Careers.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.ViewCount++;
            _db.Careers.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }

    }
}
