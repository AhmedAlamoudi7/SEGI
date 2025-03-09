
using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}