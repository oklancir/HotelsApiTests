using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotels.Models
{
    public class Item
    {
        [Display(Name = "Item Id")]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        [ForeignKey("ServiceProduct")]
        public int ServiceProductId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual ServiceProduct ServiceProduct { get; set; }
    }
}