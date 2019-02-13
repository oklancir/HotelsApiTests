using System.ComponentModel.DataAnnotations;

namespace Hotels.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public double TotalAmount { get; set; }

        [Required]
        public bool IsPaid { get; set; } = false;

        [Required]
        public int ReservationId { get; set; }

        public int ItemId { get; set; }
    }
}