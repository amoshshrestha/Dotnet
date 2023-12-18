using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Webapp.Models
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> categoryList {  get; set; }
    }
}
