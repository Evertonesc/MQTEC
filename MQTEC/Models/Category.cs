using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MQTEC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
