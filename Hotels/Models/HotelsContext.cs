using System.Data.Entity;

namespace Hotels.Models
{
    public class HotelsContext : DbContext
    {
        public HotelsContext() : base("name=HotelsDbContext")
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
    }
}