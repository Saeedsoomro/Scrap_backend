using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Domain;

namespace WebApplication1.Data
{
    public class ScrapMangementDbContext : DbContext 
    {
        public ScrapMangementDbContext(DbContextOptions<ScrapMangementDbContext> dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<UserManagement> UserManagements { get; set; }
        public DbSet<MstUserRoles> MstUserRoles { get; set; }

        public DbSet<ImageEntity> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MstUserRoles>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserManagement>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
      
            });

        }

    }
}
