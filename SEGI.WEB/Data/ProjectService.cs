using Microsoft.CodeAnalysis.Scripting;

namespace SEGI.WEB.Data
{
    public class ProjectService
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int GeneralEngineeringServicesItemId { get; set; }
        public GeneralEngineeringServicesItem GeneralEngineeringServicesItem { get; set; }
    }
}
