using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class UserInterest
    {
        [Key]
        public int UserInterestId { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public virtual AppUser AppUser { get; set; }


        [ForeignKey("Interest")]
        public int InterestId { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
