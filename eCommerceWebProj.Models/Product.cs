using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebProj.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,10000)]
		[Display(Name = "List Price")]
		public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
		[Display(Name = "Price for 1-50")]
		public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
		[Display(Name = "Price for 50-100")]
		public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
		[Display(Name = "Price for 100+")]
		public double Price100 { get; set; }

		[ValidateNever]
		public string ImageUrl { get; set; }

        // foreign key, will automatically recognized 
        // it was able to be mapped because the 'Id' was appended
        [Required]
        [ForeignKey("Category")]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
		[ValidateNever]

        // entity framework knows this is a foreign key,
        // so it knows how to implement it for product index view page
        // but when doing API calls, we need to do extra
		public Category? Category { get; set; }

        // foreign key, will automatically recognized 
        [Required]
        [ForeignKey("CoverType")]
        [Display(Name = "Cover Type")]

		public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
		[ValidateNever]
		public Category? CoverType { get; set; }
    }
}
