using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebProj.Models
{
    [Keyless]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set;  }

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }
        public int? CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        //[ValidateNever]
        //public Company Company { get; set; }
    }
}
