using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }

        [Required, DefaultValue("Pending"), Display(Name = "Status")]
        public string Name { get; set; }
    }
}