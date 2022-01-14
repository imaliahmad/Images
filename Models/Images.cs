using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Images
    {
        [Key]
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; } //png, jpg, jpeg, bmp
        public string FileType { get; set; } //image/ pdf/ excel
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
