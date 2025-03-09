namespace SEGI.WEB.Data
{
    public class GeneralEngineeringServicesItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<ProjectService> ProjectServices { get; set; }

        public GeneralEngineeringServicesItem()
        {
            ProjectServices = new List<ProjectService>();
        }
    }
}
