namespace SEGI.WEB.Core.ViewModels
{
    public class ApplyCareerFormModelViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PositionAppliedFor { get; set; }
        public string CoverLetter { get; set; }
        public string ExperienceLevel { get; set; } // Example: Entry, Mid, Senior
        public string Industry { get; set; } // Example: Power & Energy, Hydrocarbon, Water, Infrastructur
        public string Education { get; set; } // Example: Bachelor's in Engineering, Master's in Project Management, etc.
        public string Nationality { get; set; }
        public bool CurrentlyInSaudiArabia { get; set; }
        public bool HasValidIqamaOrWorkPermit { get; set; }
        public string LinkedInProfile { get; set; }
        public string Resume { get; set; } // For file upload
        public string CreatedAt { get; set; }
        public CareerViewModel Career { get; set; }
    }
}
