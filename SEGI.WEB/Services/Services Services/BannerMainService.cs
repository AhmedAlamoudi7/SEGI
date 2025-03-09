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
    public class BannerMainService : IBannerMainService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public BannerMainService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<BannerMainViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.BannerMains
                .Where(x => (x.Content.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<BannerMainViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<BannerMainViewModel>> GetModelList()
        {
            var model = await _db.BannerMains.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<BannerMainViewModel>();
            }
            var modelmapper = _mapper.Map<List<BannerMainViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<BannerMainViewModel>> Detailes()
        {
            var model = _db.BannerMains.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<BannerMainViewModel>>(model);
            return dto;
        }
        public async Task<BannerMainViewModel> Detaile(int id)
        {
            var model = await _db.BannerMains
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<BannerMainViewModel>(model);
            return dto;
        }
        public async Task<BannerMainViewModel> Detaile()
        {
            var model = await _db.BannerMains
                            .OrderByDescending(x => x.Id)
                            .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<BannerMainViewModel>(model);
            return dto;
        }
        public async Task<BannerMainViewModel> DetaileHome()
        {
            var model = await _db.BannerMains
                            .Where(x => x.BannerPageType == BannerPageType.Home)
                            .OrderByDescending(x => x.CreatedAt)
                            .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<BannerMainViewModel>(model);
            return dto;
        }
        public async Task<BannerMainViewModel> DetaileSustainability()
        {
            var model = await _db.BannerMains
                            .Where(x => x.BannerPageType == BannerPageType.Sustinabilty)
                            .OrderByDescending(x => x.CreatedAt)
                            .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<BannerMainViewModel>(model);
            return dto;
        }
        //public async Task<BannerMainViewModel> DetaileBySection(int SectionId)
        //{
        //    var model = await _db.BannerMains
        //        .Where(x => x.SectionId == SectionId)
        //        .FirstOrDefaultAsync();
        //    if (model == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }

        //    var dto = _mapper.Map<ArticleViewModel>(model);
        //    return dto;
        //}
        public async Task<int> Update(UpdateBannerMainDto dto)
        {
            var model = await _db.BannerMains.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateBannerMainDto, BannerMain>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.BannerMains.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateBannerMainDto> Get(int Id, BannerPageType BannerPageType)
        {
            var latestmodel = await _db.BannerMains
                .Where(x => !x.IsDelete && x.Id == Id && x.BannerPageType == BannerPageType)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateBannerMainDto>(latestmodel);
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.BannerMains.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
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
            _db.BannerMains.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<string> Image(int Id)
        {
            var latestmodel = await _db.BannerMains
              .Where(x => !x.IsDelete && x.Id == Id)
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
