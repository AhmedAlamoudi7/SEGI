using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Constants;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Data;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;

namespace SEGI.Services.AboutUsServices
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public TeamService(ApplicationDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<TeamViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.Teams
                .Where(x => (x.FullName.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<TeamViewModel>>(model);
            return modelmapper;
        }
        public async Task<List<TeamViewModel>> GetModelList()
        {
            var model = await _db.Teams.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<TeamViewModel>();
            }
            var modelmapper = _mapper.Map<List<TeamViewModel>>(model);
            return modelmapper;
        }
        public async Task<IEnumerable<TeamViewModel>> Detailes()
        {
            var model = _db.Teams.OrderByDescending(x => x.Id).ToList().Take(1);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var dto = _mapper.Map<IEnumerable<TeamViewModel>>(model);
            return dto;
        }
        public async Task<TeamViewModel> Detaile(int id)
        {
            var model = await _db.Teams
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<TeamViewModel>(model);
            return dto;
        }
        public async Task<int> Create(CreateTeamDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Team>(dto);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            await _db.Teams.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateTeamDto dto)
        {
            var model = await _db.Teams.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
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
            var updatedModel = _mapper.Map<UpdateTeamDto, Team>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, "Files/Images");
            }
            _db.Teams.Update(updatedModel);
            await _db.SaveChangesAsync();
            return updatedModel.Id;
        }
        public async Task<UpdateTeamDto> Get(int id)
        {
            var latestmodel = await _db.Teams
                .Where(x => !x.IsDelete && x.Id == id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateTeamDto>(latestmodel);
        }

        public async Task<int> Delete(int Id)
        {
            var model = await _db.Teams.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
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
            _db.Teams.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<string> Image(int Id)
        {

            var latestmodel = await _db.Teams
              .Where(x => !x.IsDelete)
              .OrderByDescending(x => x.Id)
              .FirstOrDefaultAsync(x => x.Id == Id);

            if (latestmodel == null)
            {
                throw new EntityNotFoundException();
            }

            return latestmodel.Image;
        }

    }
}