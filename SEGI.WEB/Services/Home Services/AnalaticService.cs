using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Services.FileServices;
using SEGI.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.HomeServices
{
    public class AnalaticService : IAnalaticService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public AnalaticService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<AnalaticViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.Analitics
                .Where(x => (x.Title.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<AnalaticViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<AnalaticViewModel>> GetModelList()
        {
            var model = await _db.Analitics.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<AnalaticViewModel>();
            }
            var modelmapper = _mapper.Map<List<AnalaticViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<AnalaticViewModel>> Detailes()
        {
            var model = _db.Analitics.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<AnalaticViewModel>>(model);
            return dto;
        }
        public async Task<AnalaticViewModel> Detaile(int id)
        {
            var model = await _db.Analitics
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<AnalaticViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateAnalaticDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Analitic>(dto);

            await _db.Analitics.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateAnalaticDto dto)
        {
            var model = await _db.Analitics.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            // Delete the old image if a new image is provided
            var updatedModel = _mapper.Map<UpdateAnalaticDto, Analitic>(dto, model);
            _db.Analitics.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateAnalaticDto> Get(int id)
        {
            var latestmodel = await _db.Analitics
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateAnalaticDto>(latestmodel);
        }


        public async Task<int> Delete(int Id)
        {
            var model = await _db.Analitics.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.IsDelete = true;
            _db.Analitics.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }


    }
}