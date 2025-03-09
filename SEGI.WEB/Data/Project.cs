
using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Year { get; set; }
        public ICollection<ProjectCategory> ProjectCategories { get; set; }
        public ICollection<ProjectService> ProjectServices { get; set; }
        public int? ViewCount { get; set; } // Track visitor count

        public Project()
        {
            ProjectServices = new List<ProjectService>();
            ViewCount = 0;
        }
    }
}