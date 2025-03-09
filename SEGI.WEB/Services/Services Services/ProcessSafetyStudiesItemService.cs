using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.HomeServices
{
    public class ProcessSafetyStudiesItemService : IProcessSafetyStudiesItemService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ProcessSafetyStudiesItemService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<ProcessSafetyStudiesItemViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.ProcessSafetyStudiesItems
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<ProcessSafetyStudiesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<ProcessSafetyStudiesItemViewModel>> GetModelList()
        {
            var model = await _db.ProcessSafetyStudiesItems.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ProcessSafetyStudiesItemViewModel>();
            }
            var modelmapper = _mapper.Map<List<ProcessSafetyStudiesItemViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<ProcessSafetyStudiesItemViewModel>> Detailes()
        {
            var model = _db.ProcessSafetyStudiesItems.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<ProcessSafetyStudiesItemViewModel>>(model);
            return dto;
        }
        public async Task<ProcessSafetyStudiesItemViewModel> Detaile(int id)
        {
            var model = await _db.ProcessSafetyStudiesItems
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<ProcessSafetyStudiesItemViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateProcessSafetyStudiesItemDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<ProcessSafetyStudiesItem>(dto);
            await _db.ProcessSafetyStudiesItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateProcessSafetyStudiesItemDto dto)
        {
            var model = await _db.ProcessSafetyStudiesItems.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);

            var updatedModel = _mapper.Map<UpdateProcessSafetyStudiesItemDto, ProcessSafetyStudiesItem>(dto, model);

            _db.ProcessSafetyStudiesItems.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateProcessSafetyStudiesItemDto> Get(int id)
        {
            var latestmodel = await _db.ProcessSafetyStudiesItems
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateProcessSafetyStudiesItemDto>(latestmodel);
        }

        public async Task<int> Delete(int Id)
        {
            var model = await _db.ProcessSafetyStudiesItems.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.IsDelete = true;
            _db.ProcessSafetyStudiesItems.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }


    }
}