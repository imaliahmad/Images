using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Please enter your name.")]
        public string Name { get; set; }

       
        public string Description { get; set; }
        public int CreatedBy { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
