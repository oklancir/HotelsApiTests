using Hotels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class ReservationFormViewModel
    {
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Display(Name = "Guest")]
        public int GuestId { get; set; }

        [Display(Name = "Reservation Status")]
        public int ReservationStatusId { get; set; }

        public int Discount { get; set; }

        public IEnumerable<ReservationStatus> ReservationStatuses { get; set; }
        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}