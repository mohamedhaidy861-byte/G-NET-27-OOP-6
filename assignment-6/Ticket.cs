using System;

namespace App_v5
{
    public abstract class Ticket : IPrintable, IBookable, ICloneable
    {
        private static int ticketCounter = 0;
        private decimal _price;

        public int TicketId { get; protected set; }
        public string MovieName { get; set; } = string.Empty;

        public decimal Price
        {
            get => _price;
            set { if (value > 0) _price = value; }
        }

        public abstract decimal CalculateFinalPrice();
        public virtual string GetTicketType() => "Ticket";

        public bool IsBooked { get; private set; }

        protected Ticket(string movieName, decimal price)
        {
            ticketCounter++;
            TicketId = ticketCounter;
            MovieName = movieName;
            Price = price;
            IsBooked = false;
        }

        protected Ticket() { }

        protected void AssignNewId()
        {
            ticketCounter++;
            TicketId = ticketCounter;
        }

        protected void CopyBaseTo(Ticket target)
        {
            target.AssignNewId();
            target.MovieName = this.MovieName;
            target.Price = this.Price;
            target.IsBooked = false;
        }

        public static int GetTotalTickets() => ticketCounter;

        public void Book()
        {
            if (IsBooked)
                throw new InvalidOperationException($"Ticket #{TicketId} is already booked.");
            IsBooked = true;
        }

        public void Cancel()
        {
            if (!IsBooked)
                throw new InvalidOperationException($"Ticket #{TicketId} is not booked.");
            IsBooked = false;
        }

        public abstract void PrintTicket();
        public abstract object Clone();
    }
}