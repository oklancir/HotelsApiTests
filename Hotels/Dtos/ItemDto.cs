using System.ComponentModel.DataAnnotations;

namespace Hotels.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int ServiceProductId { get; set; }
    }
}