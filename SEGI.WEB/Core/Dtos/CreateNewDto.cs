using SEGI.WEB.Data;

namespace SEGI.WEB.Core.Dtos
{
    public class CreateNewDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Image_Cover { get; set; }
        public string Short_Description { get; set; }
        public ICollection<int> CategoryIds { get; set; }
        public CreateNewDto()
        {
            CategoryIds = new List<int>();
        }
    }
}
