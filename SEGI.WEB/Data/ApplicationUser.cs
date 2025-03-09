using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using SEGI.Core.Constants;

namespace SEGI.WEB.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public int? Code { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        //public ICollection<UserScript> UserScripts { get; set; }
        public bool? Active { get; set; }

        public ApplicationUser()
        {
            IsDelete = false;
            CreatedAt = DateTime.Now;
            Active = true;
        }
    }
}
