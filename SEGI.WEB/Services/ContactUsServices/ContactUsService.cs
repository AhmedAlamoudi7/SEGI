using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEGI.Core.Dtos;
using SEGI.Core.Exceptions;
using SEGI.Core.ViewModels;
using SEGI.Services.FileServices;
using SEGI.WEB.Core.Dtos;
using SEGI.WEB.Core.ViewModels;
using SEGI.WEB.Data;



namespace SEGI.Services.Services.ContactUsServicess
{
    public class ContactUsService : IContactUsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ContactUsService(
            ApplicationDbContext db,
            IMapper mapper,
            IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<List<ContactUsViewModel>> GetAll(string? GeneralSearch)
        {
            var model = await _db.ContactUss
                .Where(x => (x.Email.Contains(GeneralSearch)
            || string.IsNullOrWhiteSpace(GeneralSearch)))
            .OrderByDescending(x => x.CreatedAt).ToListAsync();
            var modelmapper = _mapper.Map<List<ContactUsViewModel>>(model);
            return modelmapper;
        }

        public async Task<List<ContactUsViewModel>> GetContactUsList()
        {
            var model = await _db.ContactUss.Where(x => !x.IsDelete).ToListAsync();
            if (model == null)
            {
                return new List<ContactUsViewModel>();
            }
            var modelmapper = _mapper.Map<List<ContactUsViewModel>>(model);
            return modelmapper;
        }
        public async Task<int> Create(CreateContactUsDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<ContactUs>(dto);
            await _db.ContactUss.AddAsync(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateContactUsDto dto)
        {
            var model = await _db.ContactUss.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedmodel = _mapper.Map<UpdateContactUsDto, ContactUs>(dto, model);
            _db.ContactUss.Update(updatedmodel);
            await _db.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _db.ContactUss.SingleOrDefaultAsync(x => x.Id == Id && !(bool)x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _db.ContactUss.Update(model);
            await _db.SaveChangesAsync();
            return model.Id;
        }
        public async Task<UpdateContactUsDto> Get(int Id)
        {
            var model = await _db.ContactUss.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<UpdateContactUsDto>(model);
        }
        public async Task<string> CountContactUsAsync()
        {
            // Build the query
            IQueryable<ContactUs> query = _db.ContactUss.AsQueryable();
            // Perform count operation in the database
            return @NumberFormatter.FormatNumber(await query.CountAsync());
        }
        public async Task<ContactUsViewModel> Detaile(int Id)
        {
            var model = await _db.ContactUss.FirstOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ContactUsViewModel>(model);
        }

    }
}
