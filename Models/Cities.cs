using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Cities
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<AppUser> AppUsers { get; set; }
    }
}
