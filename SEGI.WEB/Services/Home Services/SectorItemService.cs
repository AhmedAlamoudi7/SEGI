using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.HomeServices
{
    public class SectorItemService : ISectorItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public SectorItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<SectorItemViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.SectorItems
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<SectorItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<SectorItemViewModel>> GetModelList()
        {
            var model = await _db.SectorItems.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<SectorItemViewModel>();
            }
            var modelmapper = _mapper.Map<List<SectorItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<SectorItemViewModel>> Detailes()
        {
            var model = _db.SectorItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<SectorItemViewModel>>(model);
            return dto;
        }
        public async Task<SectorItemViewModel> Detaile(int id)
        {
            var model = await _db.SectorItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<SectorItemViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateSectorItemDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<SectorItem>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            if (dto.Icon != null)
            {
                model.Icon = await _fileService.SaveFile(dto.Icon, FolderNames.ImagesFolder);
            }
            await _db.SectorItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateSectorItemDto dto)
        {
            var model = await _db.SectorItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            // Delete the old image if a new image is provided
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
            // Delete the old image if a new image is provided
            if (!string.IsNullOrEmpty(model.Icon) && dto.Icon != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Icon);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            var updatedModel = _mapper.Map<UpdateSectorItemDto, SectorItem>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            if (dto.Icon != null)
            {
                model.Icon = await _fileService.SaveFile(dto.Icon, "Files/Images");
            }
            _db.SectorItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateSectorItemDto> Get(int id)
        {
            var latestmodel = await _db.SectorItems
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateSectorItemDto>(latestmodel);
        }
        public async Task<string> Image(int Id)
        {

            var latestmodel = await _db.SectorItems
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync(x => x.Id == Id);

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Image;
        }
        public async Task<string> Icon(int Id)
        {

            var latestmodel = await _db.SectorItems
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync(x => x.Id == Id);

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Icon;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.SectorItems.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            // Delete the old image if a new image is provided
            if (!string.IsNullOrEmpty(model.Image))
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Image);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            // Delete the old image if a new image is provided
            if (!string.IsNullOrEmpty(model.Icon))
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Icon);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            model.IsDelete = true;
            _db.SectorItems.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<string> CountSectorItemsAsync()
        {
            // Build the query
            IQueryable<SectorItem> query = _db.SectorItems.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
    }
}