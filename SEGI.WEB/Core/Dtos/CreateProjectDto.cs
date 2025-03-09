using SEGI.WEB.Data;

namespace SEGI.WEB.Core.Dtos
{
    public class CreateProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Year { get; set; }
        public ICollection<int> CategoryIds { get; set; }
        public ICollection<int> ServiceIds { get; set; }
        public CreateProjectDto()
        {
            CategoryIds = new List<int>();
            ServiceIds = new List<int>();

        }
    }
}
