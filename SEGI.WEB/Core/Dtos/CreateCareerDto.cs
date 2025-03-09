using SEGI.WEB.Data;

namespace SEGI.WEB.Core.Dtos
{
    public class CreateCareerDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public ICollection<int> CategoryIds { get; set; }
        public CreateCareerDto()
        {
            CategoryIds = new List<int>();

        }
    }
}
