using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using SEGI.Core.Constants;

namespace SEGI.Core.Dtos
{
    public class UpdateApplicationUserDto
    {
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        //[RegularExpression(Message.RegularExpEmail)]
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public UserType UserType { get; set; }
        public string PhoneNumber { get; set; }

    }
}
