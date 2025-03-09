using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.HomeServices
{
    public class CoreValueService : ICoreValueService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CoreValueService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<IEnumerable<CoreValueViewModel>> Detailes()
        {
            var model = _db.CoreValues.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<CoreValueViewModel>>(model);
            return dto;
        }
        public async Task<CoreValueViewModel> Detaile()
        {
            var model = await _db.CoreValues
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<CoreValueViewModel>(model);
            return dto;
        }
        public async Task<int> Update(UpdateCoreValueDto dto)
        {
            var model = await _db.CoreValues.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateCoreValueDto, CoreValue>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.CoreValues.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateCoreValueDto> Get()
        {
            var latestmodel = await _db.CoreValues
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateCoreValueDto>(latestmodel);
        }
        public async Task<string> Image()
        {

            var latestmodel = await _db.CoreValues
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Image;
        }
    }
}