using SEGI.WEB.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SEGI.Core.Dtos
{
    public class UpdateTeamDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Position { get; set; }
        public IFormFile Image { get; set; }
        public TeamType TeamType { get; set; }

    }
}
