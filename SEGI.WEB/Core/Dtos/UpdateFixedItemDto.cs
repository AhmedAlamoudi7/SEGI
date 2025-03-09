using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.Dtos
{
    public class UpdateFixedItemDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public IFormFile? Logo_White { get; set; }
        public IFormFile? Logo_Dark { get; set; }
        public string? X { get; set; }
        public string? FaceBook { get; set; }
        public string? Youtube { get; set; }
        public string? LinkedIn { get; set; }
        public string? TikTok { get; set; }
        public string? Snapchat { get; set; }
        public string? Instagram { get; set; }
        public string? Phone { get; set; }
    }
}
