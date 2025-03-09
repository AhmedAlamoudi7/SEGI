using SEGI.WEB.Data;

namespace SEGI.WEB.Core.Dtos
{
    public class UpdateHistoryDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int? Year { get; set; }  // Nullable for cases where "Case" is used
        public string Case { get; set; } // New field for non-year values
        public bool IsActive { get; set; } // New field for non-year values

    }
}
