using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class MyDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-8E0GKPI\SQLEXPRESS;Database=AspTest;Trusted_Connection=True;");
        }

        //DbSet
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Interest> Interest { get; set; }
        public DbSet<UserInterest> UserInterest { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
