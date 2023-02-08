using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebProj.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        // using this following attribute, we can display the name with space 
        // instead of the default one without space
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be 1 and 100")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
