using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Core.Dtos
{
    public class UpdateArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int SectionId { get; set; }
        public PageType PageType { get; set; }

    }
}
