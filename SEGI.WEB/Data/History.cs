
namespace SEGI.WEB.Data
{
    public class History : BaseEntity
    {
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Year { get; set; }  // Nullable for cases where "Case" is used
        public string Case { get; set; } // New field for non-year values

    }
}
