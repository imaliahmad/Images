using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<UserInterest> UserInterest { get; set; }
    }
}
