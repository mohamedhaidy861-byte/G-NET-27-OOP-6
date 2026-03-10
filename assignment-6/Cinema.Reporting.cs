using System;

namespace App_v5
{
    public partial class Cinema
    {
        public void PrintAllTickets()
        {
            Console.WriteLine("\n--- All Tickets (from Cinema.Reporting) ---");
            foreach (var t in GetTickets())
                if (t != null) ((IPrintable)t).PrintTicket();
        }

        public void PrintStatistics()
        {
            int count = 0;
            decimal total = 0;

            foreach (var t in GetTickets())
            {
                if (t != null)
                {
                    count++;
                    total += t.CalculateFinalPrice();
                }
            }

            Console.WriteLine($"\n--- Cinema Statistics ---");
            Console.WriteLine($"Total Tickets : {count}");
            Console.WriteLine($"Total Revenue : {total:F2}");
        }
    }
}