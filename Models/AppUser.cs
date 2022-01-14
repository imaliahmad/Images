using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    [Table("AppUser")]
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        [NotMapped]
        public SelectList CityList { get; set; }

        [NotMapped]
        public SelectList GenderList { get; set; }

        [NotMapped]
        public List<SelectListItem> InterestList { get; set; }



        [ForeignKey("Cities")]
        public int? CityId { get; set; }
        public virtual Cities Cities { get; set; }


        [ForeignKey("Gender")]
        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        [ForeignKey("Images")]
        public int? ImageId { get; set; }
        public virtual Images Images { get; set; }


        public virtual IEnumerable<UserInterest> UserInterest { get; set; }
    }
}
