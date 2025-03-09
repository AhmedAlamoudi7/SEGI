using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Core.ViewModels
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Year { get; set; }  // Nullable for cases where "Case" is used
        public string Case { get; set; } // New field for non-year values
        public bool IsActive { get; set; }
    }
}
