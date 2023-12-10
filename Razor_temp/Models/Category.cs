using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Razor_temp.Models
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
