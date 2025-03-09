using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.AboutUsServices
{
    public class AboutUsLegacyService : IAboutUsLegacyService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public AboutUsLegacyService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<IEnumerable<LegacyAboutUsViewModel>> Detailes()
        {
            var model = _db.LegacyAboutUss.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<LegacyAboutUsViewModel>>(model);
            return dto;
        }
        public async Task<LegacyAboutUsViewModel> Detaile()
        {
            var model = await _db.LegacyAboutUss.Select(x=> new LegacyAboutUsViewModel() { 
            Id = x.Id,
            Description = x.Description,
            Image = x.Image,
            Title = x.Title
            }).OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            //var dto = _mapper.Map<LegacyAboutUsViewModel>(model);
            return model;
        }

        public async Task<int> Update(UpdateLegacyAboutUssDto dto)
        {
            var model = await _db.LegacyAboutUss.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateLegacyAboutUssDto, LegacyAboutUs>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.LegacyAboutUss.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateLegacyAboutUssDto> Get()
        {
            var latestmodel = await _db.LegacyAboutUss
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateLegacyAboutUssDto>(latestmodel);
        }
        public async Task<string> Image()
        {

            var latestmodel = await _db.LegacyAboutUss
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