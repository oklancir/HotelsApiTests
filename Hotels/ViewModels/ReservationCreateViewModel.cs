using Hotels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class ReservationCreateViewModel
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Discount { get; set; }

        [Required]
        public int Guest { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }
    }
}