
namespace SEGI.WEB.Data
{
    public class OurCoreValue : BaseEntity
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public ICollection<OurCoreValueItem> OurCoreValueItems { get; set; }
    }
}
