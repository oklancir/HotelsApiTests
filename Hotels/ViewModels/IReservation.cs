using System;

namespace Hotels.ViewModels
{
    interface IReservation
    {
        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        int Guest { get; set; }

        int Discount { get; set; }
    }
}
