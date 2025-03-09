using System.ComponentModel.DataAnnotations;

namespace SEGI.Core.Dtos
{
    public class UpdateAnalaticDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Number { get; set; }

    }
}
