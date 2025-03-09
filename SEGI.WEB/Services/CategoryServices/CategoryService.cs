using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;



namespace SEGI.Services.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CategoryService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<CategoryViewModels>> GetAll(string? GeneralSearch)
        {
            var model = await _db.Categories
                .Where(x => (x.Name.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<CategoryViewModels>>(model);
            return modelmapper;
        }

        public async Task<List<CategoryViewModels>> GetCategoryList()
        {
            var model = await _db.Categories.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<CategoryViewModels>();
            }
            var modelmapper = _mapper.Map<List<CategoryViewModels>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateCategoryDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Category>(dto);
            await _db.Categories.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var model = await _db.Categories.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedmodel = _mapper.Map<UpdateCategoryDto, Category>(dto, model);
            _db.Categories.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.Categories.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _db.Categories.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var model = await _db.Categories.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateCategoryDto>(model);
        }
        public async Task<string> CountCategoriesAsync()
        {
            // Build the query
            IQueryable<Category> query = _db.Categories.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<CategoryViewModels> Detaile(int Id)
        {
            var model = await _db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<CategoryViewModels>(model);
        }

    }
}
