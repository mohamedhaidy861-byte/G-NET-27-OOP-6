using System;

namespace App_v5
{
    // ============================================================
    // MAIN
    // ============================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------------
            // a. Ticket is ABSTRACT — compiler prevents instantiation
            // -------------------------------------------------------
            // Ticket t = new Ticket("Test", 100);
            // ERROR: Cannot create instance of abstract type 'Ticket'

            // -------------------------------------------------------
            // b. Create one of each type, book all three
            // -------------------------------------------------------
            Cinema cinema = new Cinema("Galaxy Cinema");
            cinema.OpenCinema();

            StandardTicket t1 = new StandardTicket("Inception", 80, "A5");
            VIPTicket t2 = new VIPTicket("Avengers", 150, true);  // 150+50 = 200
            IMAXTicket t3 = new IMAXTicket("Dune", 100, true);  // 100+30 = 130

            t1.Book();
            t2.Book();
            t3.Book();

            // -------------------------------------------------------
            // c. Add to Cinema, print via Cinema.Reporting partial file
            // -------------------------------------------------------
            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            cinema.PrintAllTickets();   // defined in Cinema.Reporting.cs

            // -------------------------------------------------------
            // d. Polymorphism — Ticket[] calls abstract CalculateFinalPrice()
            //    Each subtype runs its OWN formula
            // -------------------------------------------------------
            Console.WriteLine("\n--- Polymorphism: Final Price per Ticket ---");
            Ticket[] allTickets = new Ticket[] { t1, t2, t3 };

            foreach (Ticket t in allTickets)
            {
                // abstract method called through base-class reference
                Console.WriteLine($"{t.GetTicketType()} => Final Price: {t.CalculateFinalPrice():F2}");
            }

            // -------------------------------------------------------
            // e. Extension method #1 — receipt (called on object naturally)
            // -------------------------------------------------------
            Console.WriteLine("\n--- Extension Method: Receipt ---");
            string receipt = t2.GenerateReceipt();   // feels like it belongs to VIPTicket
            Console.WriteLine(receipt);

            // -------------------------------------------------------
            // f. Extension method #2 — total revenue (called on array naturally)
            // -------------------------------------------------------
            Console.WriteLine("--- Extension Method: Total Revenue ---");
            decimal revenue = allTickets.TotalRevenue();   // feels like it belongs to Ticket[]
            Console.WriteLine($"Total Revenue: {revenue:F2}");

            // -------------------------------------------------------
            // g. Close the cinema
            // -------------------------------------------------------
            cinema.CloseCinema();

            Console.ReadKey();
        }
    }
}
