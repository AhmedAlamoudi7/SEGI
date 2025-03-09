using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.AboutUsServices
{
    public class AboutUsBannerService : IAboutUsBannerService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public AboutUsBannerService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<IEnumerable<AboutUsBannerViewModel>> Detailes()
        {
            var model = _db.BannerAboutUss.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<AboutUsBannerViewModel>>(model);
            return dto;
        }
        public async Task<AboutUsBannerViewModel> Detaile()
        {
            var model = await _db.BannerAboutUss
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<AboutUsBannerViewModel>(model);
            return dto;
        }

        public async Task<int> Update(UpdateBannerAboutUssDto dto)
        {
            var model = await _db.BannerAboutUss.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateBannerAboutUssDto, BannerAboutUs>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.BannerAboutUss.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateBannerAboutUssDto> Get()
        {
            var latestmodel = await _db.BannerAboutUss
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateBannerAboutUssDto>(latestmodel);
        }
        public async Task<string> Image()
        {

            var latestmodel = await _db.BannerAboutUss
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