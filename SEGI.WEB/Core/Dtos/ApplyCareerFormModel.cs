using SEGI.Services.FileServices;
using SEGI.WEB.Data;
using System.ComponentModel.DataAnnotations;

namespace SEGI.WEB.Core.Dtos
{
    public class CreateApplyCareerFormModelDto
    {

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string PositionAppliedFor { get; set; }

        [Required]
        [StringLength(500)]
        public string CoverLetter { get; set; }

        [Required]
        public string ExperienceLevel { get; set; } // Example: Entry, Mid, Senior

        [Required]
        public string Industry { get; set; } // Example: Power & Energy, Hydrocarbon, Water, Infrastructure

        [Required]
        public string Education { get; set; } // Example: Bachelor's in Engineering, Master's in Project Management, etc.

        [Required]
        public string Nationality { get; set; }

        [Required]
        public bool CurrentlyInSaudiArabia { get; set; }

        [Required]
        public bool HasValidIqamaOrWorkPermit { get; set; }

        public string LinkedInProfile { get; set; }

        public IFormFile Resume { get; set; } // For file upload

        public int CareerId { get; set; }

    }
}
