using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        public WeddingContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Wedding { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public void CreateWedding(NewWedding wedding, int userId)
        {
            wedding.UserId = userId;
            Add(new Wedding(wedding));
            SaveChanges();
        }
    }

}