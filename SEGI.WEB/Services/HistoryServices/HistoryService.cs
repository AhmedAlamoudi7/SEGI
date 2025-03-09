using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;



namespace SEGI.Services.Services.HistoryServices
{
    public class HistoryService : IHistoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public HistoryService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<HistoryViewModel>> GetAll(Querys querys)
        {
            // Ensure querys is not null
            if (querys == null)
            {
                querys = new Querys();
            }

            var query = _db.Histories
                  .AsQueryable();

            // Apply search filter if GeneralSearch is provided
            if (!string.IsNullOrWhiteSpace(querys.GeneralSearch))
            {
                query = query.Where(x => x.Year.ToString() == querys.GeneralSearch);
            }


            var model = await query.OrderByDescending(x => x.CreatedAt).ToListAsync();

            return _mapper.Map<List<HistoryViewModel>>(model);
        }
        public async Task<List<HistoryViewModel>> GetModelList()
        {
            var model = await _db.Histories
               .Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<HistoryViewModel>();
            }
            var modelmapper = _mapper.Map<List<HistoryViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateHistoryDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            // Delete the old image if a new image is provided

            var model = _mapper.Map<History>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            await _db.Histories.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateHistoryDto dto)
        {
            var model = await _db.Histories
                .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
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
            var updatedmodel = _mapper.Map<UpdateHistoryDto, History>(dto, model);
            // Update Project categories
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            _db.Histories.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.Histories.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _db.Histories.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateHistoryDto> Get(int Id)
        {
            var model = await _db.Histories
               .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateHistoryDto>(model);
        }
        public async Task<string> CountModelAsync()
        {
            // Build the query
            IQueryable<History> query = _db.Histories.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<HistoryViewModel> Detaile(int Id)
        {
            var model = await _db.Histories
               .FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
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
            return _mapper.Map<HistoryViewModel>(model);
        }

        public async Task<string> Image(int id)
        {

            var latestmodel = await _db.Histories
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .SingleOrDefaultAsync(x => x.Id == id);

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Image;
        }




    }
}
