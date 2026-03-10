using System;

namespace App_v5
{
    public static class TicketExtensions
    {
        public static string GenerateReceipt(this Ticket ticket)
        {
            return
                $"========== RECEIPT ==========\n" +
                $"  Movie   : {ticket.MovieName}\n" +
                $"  Type    : {ticket.GetTicketType()}\n" +
                $"  Price   : {ticket.Price}\n" +
                $"  Final   : {ticket.CalculateFinalPrice():F2}\n" +
                $"  Status  : {(ticket.IsBooked ? "Booked" : "Not Booked")}\n" +
                $"==============================";
        }

        public static decimal TotalRevenue(this Ticket[] tickets)
        {
            decimal total = 0;
            foreach (var t in tickets)
                if (t != null) total += t.CalculateFinalPrice();
            return Math.Round(total, 2);
        }
    }
}