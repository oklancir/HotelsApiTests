using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Hotels.Models
{
    public interface IHotelsContext : IDisposable
    {
        DbSet<Guest> Guests { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<ReservationStatus> ReservationStatuses { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<RoomType> RoomTypes { get; set; }
        DbSet<ServiceProduct> ServiceProducts { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
    }
}