using AutoMapper;
using Humanizer;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;



namespace SEGI.Services.Services.NewServices
{
    public class NewService : INewService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public NewService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<NewViewModel>> GetAll(Querys querys)
        {
            // Ensure querys is not null
            if (querys == null)
            {
                querys = new Querys();
            }

            var query = _db.News
                .Include(x => x.NewCategories)
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
                query = query.Where(x => x.NewCategories.Any(sc => sc.CategoryId == querys.categoryId));
            }

            var model = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<List<NewViewModel>>(model);
        }

        public async Task<List<NewViewModel>> GetModelList()
        {
            var model = await _db.News
                .Include(x => x.NewCategories)
               .ThenInclude(sc => sc.Category)
               .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<NewViewModel>();
            }
            var modelmapper = _mapper.Map<List<NewViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateNewDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            // Delete the old image if a new image is provided

            var model = _mapper.Map<New>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            if (dto.Image_Cover != null)
            {
                model.Image_Cover = await _fileService.SaveFile(dto.Image_Cover, FolderNames.ImagesFolder);
            }
            await _db.News.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateNewDto dto)
        {
            var model = await _db.News
                .Include(x => x.NewCategories)
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
            if (!string.IsNullOrEmpty(model.Image_Cover) && dto.Image_Cover != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Image_Cover);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            var updatedmodel = _mapper.Map<UpdateNewDto, New>(dto, model);
            // Update Project categories
            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                // Detach tracked entities to avoid duplicate tracking
                foreach (var entry in _db.ChangeTracker.Entries<NewCategory>())
                {
                    if (entry.Entity.NewId == model.Id)
                    {
                        entry.State = EntityState.Detached;
                    }
                }

                // Remove existing ScriptCategories
                var existingCategories = await _db.NewCategories
                    .Where(sc => sc.NewId == model.Id)
                    .ToListAsync();
                _db.NewCategories.RemoveRange(existingCategories);

                // Add new ScriptCategories
                var newCategories = dto.CategoryIds.Select(categoryId => new NewCategory
                {
                    NewId = model.Id,
                    CategoryId = categoryId
                });

                await _db.NewCategories.AddRangeAsync(newCategories);
            }
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            if (dto.Image_Cover != null)
            {
                model.Image_Cover = await _fileService.SaveFile(dto.Image_Cover, FolderNames.ImagesFolder);
            }
            _db.News.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.News.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            if (!string.IsNullOrEmpty(model.Image) && model.Image != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Image);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            if (!string.IsNullOrEmpty(model.Image_Cover) && model.Image_Cover != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Image_Cover);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            _db.News.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateNewDto> Get(int Id)
        {
            var model = await _db.News
                 .Include(x => x.NewCategories)
               .ThenInclude(sc => sc.Category)
               .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateNewDto>(model);
        }
        public async Task<string> CountModelAsync()
        {
            // Build the query
            IQueryable<New> query = _db.News.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<NewViewModel> Detaile(int Id)
        {
            var model = await _db.News
                 .Include(x => x.NewCategories)
               .ThenInclude(sc => sc.Category)
               .FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<NewViewModel>(model);
        }
        // Method to get categories associated with a script
        public async Task<List<NewCategoriesDto>> GetNewCategories(int projectId)
        {
            return await _db.NewCategories
                .Where(sc => sc.NewId == projectId)
                .Select(sc => new NewCategoriesDto
                {
                    CategoryId = sc.CategoryId,
                    CategoryName = sc.Category.Name
                })
                .ToListAsync();
        }
        public async Task<string> CountNewsAsync()
        {
            // Build the query
            IQueryable<New> query = _db.News.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<int> ViewCountIncrees(int Id)
        {
            var model = await _db.News.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.ViewCount++;
            _db.News.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
    }
}
