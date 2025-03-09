using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.WEB.Data
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProjectCategory> ProjectCategories { get; set; }
        public ICollection<CareerCategory> CareerCategories { get; set; }
        public ICollection<NewCategory> NewCategories { get; set; }

        public Category()
        {
            ProjectCategories = new List<ProjectCategory>();
            CareerCategories = new List<CareerCategory>();
            NewCategories = new List<NewCategory>();

        }

    }
}
