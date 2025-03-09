
using SEGI.WEB.Core.Enums;

namespace SEGI.WEB.Data
{
    public class Team : BaseEntity
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public TeamType TeamType { get; set; }
    }
}
