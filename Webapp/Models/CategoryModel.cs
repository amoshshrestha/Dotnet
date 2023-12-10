using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Webapp.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId{ get; set; }
        [Required(ErrorMessage ="Category Name is Required.")]
        [MinLength(3,ErrorMessage ="Category name must be at least 3 characters")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

    }
}
