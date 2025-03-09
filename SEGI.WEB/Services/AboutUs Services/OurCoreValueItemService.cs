using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Data;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.HomeServices
{
    public class OurCoreValueItemService : IOurCoreValueItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public OurCoreValueItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<OurCoreValueItemViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.OurCoreValueItems
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<OurCoreValueItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<OurCoreValueItemViewModel>> GetModelList()
        {
            var model = await _db.OurCoreValueItems.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<OurCoreValueItemViewModel>();
            }
            var modelmapper = _mapper.Map<List<OurCoreValueItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<OurCoreValueItemViewModel>> Detailes()
        {
            var model = _db.OurCoreValueItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<OurCoreValueItemViewModel>>(model);
            return dto;
        }
        public async Task<OurCoreValueItemViewModel> Detaile(int id)
        {
            var model = await _db.OurCoreValueItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<OurCoreValueItemViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateOurCoreValueItemDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<OurCoreValueItem>(dto);

            await _db.OurCoreValueItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateOurCoreValueItemDto dto)
        {
            var model = await _db.OurCoreValueItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            var updatedModel = _mapper.Map<UpdateOurCoreValueItemDto, OurCoreValueItem>(dto, model);
            _db.OurCoreValueItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateOurCoreValueItemDto> Get(int id)
        {
            var latestmodel = await _db.OurCoreValueItems
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateOurCoreValueItemDto>(latestmodel);
        }

        public async Task<int> Delete(int Id)
        {
            var model = await _db.OurCoreValueItems.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.IsDelete = true;
            _db.OurCoreValueItems.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }


    }
}