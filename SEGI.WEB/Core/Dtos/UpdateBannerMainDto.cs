using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Core.Dtos
{
    public class UpdateBannerMainDto
    {
        public int Id { get; set; }
        public BannerContentType BannerContentType { get; set; }  // Image, Video, or Link
        public BannerPageType BannerPageType { get; set; }  // Image, Video, or Link
        public string? Content { get; set; } // For video or link URLs
        public IFormFile? Image { get; set; } // Stores uploaded image path

    }
}
