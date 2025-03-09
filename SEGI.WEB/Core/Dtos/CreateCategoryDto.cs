using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SEGI.Core.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

    }
}
