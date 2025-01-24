using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Data
{
    public class AppDbContext:IdentityDbContext<ApplicaionUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<ApplicaionUser> ApplicaionUsers { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }

}
