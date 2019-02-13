using System;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int GuestId { get; set; }

        [Required]
        public int RoomId { get; set; }

        public int ReservationStatusId { get; set; }

        public int InvoiceId { get; set; }
    }
}