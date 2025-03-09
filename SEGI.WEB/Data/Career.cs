
using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class Career : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public ICollection<CareerCategory> CareerCategories { get; set; }
        public ICollection<ApplyCareerFormModel> ApplyCareerFormModels { get; set; }
        public int? ViewCount { get; set; } // Track visitor count

        public Career()
        {
            CareerCategories = new List<CareerCategory>();
            ApplyCareerFormModels = new List<ApplyCareerFormModel>();
            ViewCount = 0;
        }
    }
}