using SEGI.WEB.Data;

namespace SEGI.WEB.Core.Dtos
{
    public class CreateContactUsDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
