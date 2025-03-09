using System.ComponentModel.DataAnnotations;

namespace SEGI.Core.Dtos
{
    public class CreatePMCServicesItemDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Image { get; set; }

    }
}
