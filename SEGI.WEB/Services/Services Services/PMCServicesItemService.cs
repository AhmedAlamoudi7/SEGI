using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.Services_Services
{
    public class PMCServicesItemService : IPMCServicesItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public PMCServicesItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<PMCServicesItemViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.PMCServicesItems
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<PMCServicesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<PMCServicesItemViewModel>> GetModelList()
        {
            var model = await _db.PMCServicesItems.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<PMCServicesItemViewModel>();
            }
            var modelmapper = _mapper.Map<List<PMCServicesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<PMCServicesItemViewModel>> Detailes()
        {
            var model = _db.PMCServicesItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<PMCServicesItemViewModel>>(model);
            return dto;
        }
        public async Task<PMCServicesItemViewModel> Detaile(int id)
        {
            var model = await _db.PMCServicesItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<PMCServicesItemViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreatePMCServicesItemDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<PMCServicesItem>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }

            await _db.PMCServicesItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdatePMCServicesItemDto dto)
        {
            var model = await _db.PMCServicesItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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

            var updatedModel = _mapper.Map<UpdatePMCServicesItemDto, PMCServicesItem>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }

            _db.PMCServicesItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdatePMCServicesItemDto> Get(int id)
        {
            var latestmodel = await _db.PMCServicesItems
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdatePMCServicesItemDto>(latestmodel);
        }
        public async Task<string> Image(int Id)
        {

            var latestmodel = await _db.PMCServicesItems
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
            var model = await _db.PMCServicesItems.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
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
            _db.PMCServicesItems.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }


    }
}