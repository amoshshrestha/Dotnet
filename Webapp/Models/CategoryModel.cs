using System.ComponentModel.DataAnnotations;

namespace Webapp.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId{ get; set; }
        [Required]
        public string CategoryName { get; set; }

    }
}
