using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Invoice
    {
        [Display(Name = "Invoice Id")]
        public int Id { get; set; }

        [Required, Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [Display(Name = "Payment status")]
        public bool IsPaid { get; set; } = false;

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        [ForeignKey("Items")]
        public int ItemId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}