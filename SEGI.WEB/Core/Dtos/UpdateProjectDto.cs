namespace SEGI.WEB.Core.Dtos
{
    public class UpdateProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Year { get; set; }
        public ICollection<int> CategoryIds { get; set; }
        public ICollection<int> ServiceIds { get; set; }
        public UpdateProjectDto()
        {
            CategoryIds = new List<int>();
            ServiceIds = new List<int>();

        }
    }
}
