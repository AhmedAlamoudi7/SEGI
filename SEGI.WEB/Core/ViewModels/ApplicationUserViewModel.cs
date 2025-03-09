using SEGI.Core.Constants;
using System.Text.Json.Serialization;

namespace SEGI.Core.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedAt { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string UserType { get; set; }
        public bool Active { get; set; }


    }
}
