using Hotels.Models;

namespace Hotels.ViewModels
{
    public class ReservationCheckoutModel
    {
        public Reservation Reservation { get; set; }

        public decimal Total { get; set; }
    }
}