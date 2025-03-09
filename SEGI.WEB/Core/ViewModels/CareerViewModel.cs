using SEGI.WEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.WEB.Core.ViewModels
{
    public class CareerViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }
        public int? ViewCount { get; set; } // Track visitor 
        public int? ApplicationAppliedCount { get; set; } // Track visitor 
        public ICollection<string> Categories { get; set; }
        public CareerViewModel()
        {
            Categories = new List<string>();
        }
    }
}
