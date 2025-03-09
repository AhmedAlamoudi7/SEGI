using SEGI.WEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.WEB.Core.ViewModels
{
    public class NewViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Short_Description { get; set; }
        public string Image { get; set; }
        public string Image_Cover { get; set; }
        public string CreatedAt { get; set; }
        public int? ViewCount { get; set; } // Track visitor 
        public ICollection<string> Categories { get; set; }
        public NewViewModel()
        {
            Categories = new List<string>();
        }
    }
}
