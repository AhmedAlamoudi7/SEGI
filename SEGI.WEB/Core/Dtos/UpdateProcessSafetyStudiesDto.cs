using System.ComponentModel.DataAnnotations;

namespace SEGI.Core.Dtos
{
    public class UpdateProcessSafetyStudiesDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
