using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class BuyViewModel
    {
        public int ReservationId { get; set; }

        public int ServiceProductId { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
    }
}