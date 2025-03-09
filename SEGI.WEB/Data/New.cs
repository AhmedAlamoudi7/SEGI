

namespace SEGI.WEB.Data
{
    public class New : BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Image_Cover { get; set; }
        public string Description { get; set; }
        public string Short_Description { get; set; }

        public int? ViewCount { get; set; } = 0; // Track visitor count
        public ICollection<NewCategory> NewCategories { get; set; }

        public New()
        {
            NewCategories = new List<NewCategory>();
            ViewCount = 0;
        }
    }
}