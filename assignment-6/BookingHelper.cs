using System;

namespace App_v5
{
  

    public static class BookingHelper
    {
        private static int counter = 0;

        public static string GenerateBookingReference()
        {
            counter++;
            return $"BK-{counter}";
        }

        public static decimal CalcGroupDiscount(int numberOfTickets, decimal pricePerTicket)
        {
            decimal total = numberOfTickets * pricePerTicket;
            if (numberOfTickets >= 5) total *= 0.9m;
            return total;
        }
    }
}
