﻿using Microsoft.CodeAnalysis.Scripting;

namespace SEGI.WEB.Data
{
    public class ProjectCategory
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
