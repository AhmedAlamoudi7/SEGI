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
    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ServicesService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<ArticleViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.Articles
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<ArticleViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<ArticleViewModel>> GetModelList()
        {
            var model = await _db.Articles.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ArticleViewModel>();
            }
            var modelmapper = _mapper.Map<List<ArticleViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<ArticleViewModel>> Detailes()
        {
            var model = _db.Articles.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<ArticleViewModel>>(model);
            return dto;
        }
        public async Task<ArticleViewModel> Detaile(int id)
        {
            var model = await _db.Articles
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<ArticleViewModel>(model);
            return dto;
        }
        public async Task<ArticleViewModel> DetaileBySection(int SectionId)
        {
            var model = await _db.Articles
                .Where(x => x.SectionId == SectionId)
                .FirstOrDefaultAsync();
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<ArticleViewModel>(model);
            return dto;
        }
        public async Task<int> Update(UpdateArticleDto dto)
        {
            var model = await _db.Articles.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateArticleDto, Article>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.Articles.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateArticleDto> Get(int SectionId, PageType PageType)
        {
            var latestmodel = await _db.Articles
                .Where(x => !x.IsDelete && x.SectionId == SectionId && x.PageType == PageType)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateArticleDto>(latestmodel);
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.Articles.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
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
            _db.Articles.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<string> Image(int SectionId)
        {
            var latestmodel = await _db.Articles
              .Where(x => !x.IsDelete && x.SectionId == SectionId)
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
