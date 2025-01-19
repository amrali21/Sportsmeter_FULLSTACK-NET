using CRUD_Design;
using CRUD_Design.Models.DBModel;
using CRUD_Design.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sportsmeter_frontend.Model.Services
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //public DbSet<User> Users{ get; set; }
        public DbSet<RunInfo> RunInfos { get; set; }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
