using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.Enums;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.WEB.Services.Services_Services
{
    public class FixedItemService : IFixedItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public FixedItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<IEnumerable<FixedItemViewModel>> Detailes()
        {
            var model = _db.FixedItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<FixedItemViewModel>>(model);
            return dto;
        }
        public async Task<FixedItemViewModel> Detaile()
        {
            var model = await _db.FixedItems
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<FixedItemViewModel>(model);
            return dto;
        }
        public async Task<int> Update(UpdateFixedItemDto dto)
        {
            var model = await _db.FixedItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            // Delete the old image if a new image is provided
            if (!string.IsNullOrEmpty(model.Logo_White) && dto.Logo_White != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Logo_White);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            if (!string.IsNullOrEmpty(model.Logo_Dark) && dto.Logo_Dark != null)
            {
                // Get the full path of the existing image
                var oldImagePath = Path.Combine("wwwroot/Files/Images", model.Logo_Dark);

                // Check if the file exists and delete it
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            var updatedModel = _mapper.Map<UpdateFixedItemDto, FixedItem>(dto, model);
            if (dto.Logo_Dark != null)
            {
                model.Logo_Dark = await _fileService.SaveFile(dto.Logo_Dark, "Files/Images");
            }
            if (dto.Logo_White != null)
            {
                model.Logo_White = await _fileService.SaveFile(dto.Logo_White, "Files/Images");
            }
            _db.FixedItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateFixedItemDto> Get()
        {
            var latestmodel = await _db.FixedItems
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateFixedItemDto>(latestmodel);
        }
        public async Task<string> Image_White()
        {

            var latestmodel = await _db.FixedItems
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Logo_White;
        }
        public async Task<string> Image_Dark()
        {

            var latestmodel = await _db.FixedItems
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Logo_Dark;
        }
    }
}
