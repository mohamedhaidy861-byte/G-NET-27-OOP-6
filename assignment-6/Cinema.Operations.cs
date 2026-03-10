using System;

namespace App_v5
{
 

    public partial class Cinema
    {
        private Ticket?[] tickets = new Ticket?[20];
        public string CinemaName { get; set; }

        public Cinema(string name) => CinemaName = name;

        public void OpenCinema()
        {
            Console.WriteLine("=== Cinema Opened ===");
            Console.WriteLine("Projector ON");
        }

        public void CloseCinema()
        {
            Console.WriteLine("\nProjector OFF");
            Console.WriteLine("=== Cinema Closed ===");
        }

        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < tickets.Length; i++)
                if (tickets[i] == null) { tickets[i] = t; return true; }
            return false;
        }

        public bool BookTicket(int ticketId)
        {
            foreach (var t in tickets)
            {
                if (t != null && t.TicketId == ticketId)
                {
                    t.Book();
                    return true;
                }
            }
            return false;
        }

        // Expose tickets array to the reporting partial file
        internal Ticket?[] GetTickets() => tickets;
    }
}
