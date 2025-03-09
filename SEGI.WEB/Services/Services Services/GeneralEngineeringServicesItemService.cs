using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.Services_Services
{
    public class GeneralEngineeringServicesItemService : IGeneralEngineeringServicesItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public GeneralEngineeringServicesItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<GeneralEngineeringServicesItemViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.GeneralEngineeringServicesItems
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<GeneralEngineeringServicesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<GeneralEngineeringServicesItemViewModel>> GetModelList()
        {
            var model = await _db.GeneralEngineeringServicesItems.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<GeneralEngineeringServicesItemViewModel>();
            }
            var modelmapper = _mapper.Map<List<GeneralEngineeringServicesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<GeneralEngineeringServicesItemViewModel>> Detailes()
        {
            var model = _db.GeneralEngineeringServicesItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<GeneralEngineeringServicesItemViewModel>>(model);
            return dto;
        }
        public async Task<GeneralEngineeringServicesItemViewModel> Detaile(int id)
        {
            var model = await _db.GeneralEngineeringServicesItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<GeneralEngineeringServicesItemViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateGeneralEngineeringServicesItemDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<GeneralEngineeringServicesItem>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            await _db.GeneralEngineeringServicesItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateGeneralEngineeringServicesItemDto dto)
        {
            var model = await _db.GeneralEngineeringServicesItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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

            var updatedModel = _mapper.Map<UpdateGeneralEngineeringServicesItemDto, GeneralEngineeringServicesItem>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }

            _db.GeneralEngineeringServicesItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateGeneralEngineeringServicesItemDto> Get(int id)
        {
            var latestmodel = await _db.GeneralEngineeringServicesItems
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateGeneralEngineeringServicesItemDto>(latestmodel);
        }
        public async Task<string> Image(int Id)
        {

            var latestmodel = await _db.GeneralEngineeringServicesItems
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync(x => x.Id == Id);


            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Image;
        }

        public async Task<int> Delete(int Id)
        {
            var model = await _db.GeneralEngineeringServicesItems.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
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

            model.IsDelete = true;
            _db.GeneralEngineeringServicesItems.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }

        public async Task<string> CountGeneralEngineeringServicesItemsAsync()
        {
            // Build the query
            IQueryable<GeneralEngineeringServicesItem> query = _db.GeneralEngineeringServicesItems.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }


    }
}