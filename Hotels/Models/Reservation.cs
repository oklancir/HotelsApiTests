using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Reservation
    {
        [Display(Name = "Reservation Id")]
        public int Id { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }

        [Required, Display(Name = "Start date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required, Display(Name = "End date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [ForeignKey("Guest"), Display(Name = "Guest ")]
        public int GuestId { get; set; }

        [ForeignKey("Room"), Display(Name = "Room")]
        public int RoomId { get; set; }

        [ForeignKey("ReservationStatus"), Display(Name = "Reservation Status")]
        public int ReservationStatusId { get; set; }

        [ForeignKey("Invoices"), Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Room Room { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}