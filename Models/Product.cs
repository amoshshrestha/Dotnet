
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Product Name is Required")]
        [MinLength(3,ErrorMessage ="Product Name should be at least 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Product Description is Required")]
        [MinLength(20,ErrorMessage ="Product Description must be at least 20 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product Price is Required")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$",ErrorMessage ="Invalid Value for price")]
        public double Price { get; set; }
        public int CategoryID {  get; set; }
        [ForeignKey("CategoryID")]
        [ValidateNever]
        public CategoryModel Category { get; set; }
        [Required(ErrorMessage ="Count in stock is required")]
        public int Stock {  get; set; }
        [ValidateNever]
        public string? ImageUrl {  get; set; }
    }
}
