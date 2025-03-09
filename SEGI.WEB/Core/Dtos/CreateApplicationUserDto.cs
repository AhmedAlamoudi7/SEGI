using Microsoft.AspNetCore.Http;
using SEGI.Core.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataType = System.ComponentModel.DataAnnotations.DataType;


namespace SEGI.Core.Dtos
{
    public class CreateApplicationUserDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]

        //[RegularExpression(Message.RegularExpEmail)]
        public string Email { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public UserType UserType { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Message.DescriptionConfirmPassword)]
        [Compare(Message.Password, ErrorMessage = Message.ErrorMessage.PassAndConfirmPassNotSame)]
        public string ConfirmPassword { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
