using Hotels.Models;
using System;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class ReservationViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Discount { get; set; }
        public int Guest { get; set; }
        public int RoomType { get; set; }
        public int Room { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}