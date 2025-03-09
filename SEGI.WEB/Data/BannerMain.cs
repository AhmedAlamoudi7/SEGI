using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class BannerMain : BaseEntity
    {
        public BannerContentType BannerContentType { get; set; }  // Image, Video, or Link
        public BannerPageType BannerPageType { get; set; }  // Image, Video, or Link
        public string? Content { get; set; } // For video or link URLs
        public string? Image { get; set; } // Stores uploaded image path
    }
}
