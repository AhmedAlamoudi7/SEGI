
using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int SectionId { get; set; }
        public PageType PageType { get; set; }
    }
}