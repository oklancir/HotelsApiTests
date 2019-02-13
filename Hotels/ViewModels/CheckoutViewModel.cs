using Hotels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class CheckoutViewModel
    {
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Display(Name = "Item Id")]
        public int ItemId { get; set; }

        public Item Item { get; set; }

        [Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public double TotalAmount { get; set; }

        public double Discount { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}