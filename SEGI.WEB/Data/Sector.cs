
namespace SEGI.WEB.Data
{
    public class Sector : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<SectorItem> SectorItems { get; set; }
    }
}
