using SEGI.WEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.WEB.Core.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ViewCount { get; set; } // Track visitor 

        public ICollection<string> Categories { get; set; }
        public ICollection<string> Services { get; set; }
        public ProjectViewModel()
        {
            Categories = new List<string>();
            Services = new List<string>();
        }
    }
}
