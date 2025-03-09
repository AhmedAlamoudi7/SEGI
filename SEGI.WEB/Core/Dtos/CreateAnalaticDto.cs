using System.ComponentModel.DataAnnotations;

namespace SEGI.Core.Dtos
{
    public class CreateAnalaticDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Number { get; set; }

    }
}
