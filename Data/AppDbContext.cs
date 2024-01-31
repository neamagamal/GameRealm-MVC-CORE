
using CrudOperation.Models;

namespace CrudOperation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(a =>
                {
                    a.Property(x => x.Name).IsRequired().HasMaxLength(250);
                    a.Property(x => x.Description).HasMaxLength(5000);
                    a.Property(x => x.Cover).HasMaxLength(500);
                });
            modelBuilder.Entity<Category>(a =>
            {

                a.HasData(new Category[]
            {
                new Category { Id = 1, Name = "Sports" },
                new Category { Id = 2, Name = "Cooking" },
                new Category { Id = 3, Name = "GirlMakup" },
                new Category { Id = 4, Name = "puzzle" },
                new Category { Id = 5, Name = "Dressing-up" },
                new Category { Id = 6, Name = "Film" }
            });
                a.Property(x => x.Name).HasMaxLength(300);


            });
            modelBuilder.Entity<Device>(a =>
            {
                _ = a.HasData(new Device[]
            {
                new Device { Id = 1, Name = "PlayStation", Icon = "bi bi-playstation" },
                new Device { Id = 2, Name = "xbox", Icon = "bi bi-xbox" },
                new Device { Id = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch" },
                new Device { Id = 4, Name = "PC", Icon = "bi bi-pc-display" }
            });
                a.Property(x => x.Icon).HasMaxLength(50);

            });
            modelBuilder.Entity<GameDevice>().HasKey(x => new { x.GameId, x.DeviceId });
            base.OnModelCreating(modelBuilder);
        }


    }
}
