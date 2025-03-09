using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Core.ViewModels
{
    public class BannerMainViewModel
    {
        public int Id { get; set; }
        public string BannerContentType { get; set; }  // Image, Video, or Link
        public string BannerPageType { get; set; }  // Image, Video, or Link
        public string? Content { get; set; } // For video or link URLs
        public string? Image { get; set; } // Stores uploaded image path
    }
}
