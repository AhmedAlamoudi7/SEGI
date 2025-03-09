namespace SEGI.WEB.Core.Dtos
{
    public class UpdateCareerDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public ICollection<int> CategoryIds { get; set; }
        public UpdateCareerDto()
        {
            CategoryIds = new List<int>();

        }
    }
}
