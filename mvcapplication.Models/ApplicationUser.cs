using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string? City { get; set; }
        public string? FirstLineOfAddress { get; set; }
        public string? PostCode { get; set; }

    }
}
