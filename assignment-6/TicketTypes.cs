using System;

namespace App_v5
{
    // ============================================================
    // STANDARD TICKET
    // ============================================================

    public class StandardTicket : Ticket
    {
        public string Seat { get; set; } = string.Empty; 

        public StandardTicket(string movieName, decimal price, string seat)
            : base(movieName, price)
        {
            Seat = seat;
        }

        private StandardTicket() : base() { }

        // Overrides abstract method — Standard: price * 1.14 tax
        public override decimal CalculateFinalPrice() => Math.Round(Price * 1.14m, 2);

        // Overrides virtual method
        public override string GetTicketType() => "StandardTicket";

        // [Ticket #1] Inception | Standard | Seat: A5 | Price: 80 | Final: 91.20 | Booked: Yes
        public override void PrintTicket()
        {
            Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {Seat} | " +
                $"Price: {Price} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }

        public override object Clone()
        {
            var clone = new StandardTicket();
            CopyBaseTo(clone);
            clone.Seat = this.Seat;
            return clone;
        }
    }

    // ============================================================
    // VIP TICKET
    // ============================================================

    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; } = 50;

        // price + 50 service fee baked in
        public VIPTicket(string movieName, decimal price, bool lounge)
            : base(movieName, price + 50)
        {
            LoungeAccess = lounge;
        }

        private VIPTicket() : base() { }

        // Overrides abstract method — VIP: (price + fee) * 1.14 + extra 5% luxury tax
        public override decimal CalculateFinalPrice() => Math.Round(Price * 1.14m * 1.05m, 2);

        public override string GetTicketType() => "VIPTicket";

        // [Ticket #2] Avengers | VIP | Lounge: Yes | Fee: 50 | Price: 200 | Final: 285.00 | Booked: Yes
        public override void PrintTicket()
        {
            Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | VIP | " +
                $"Lounge: {(LoungeAccess ? "Yes" : "No")} | Fee: {ServiceFee} | " +
                $"Price: {Price} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }

        public override object Clone()
        {
            var clone = new VIPTicket();
            CopyBaseTo(clone);
            clone.LoungeAccess = this.LoungeAccess;
            return clone;
        }
    }

    // ============================================================
    // IMAX TICKET
    // ============================================================

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        // 3D surcharge baked in
        public IMAXTicket(string movieName, decimal price, bool is3D)
            : base(movieName, price + (is3D ? 30 : 0))
        {
            Is3D = is3D;
        }

        private IMAXTicket() : base() { }

        // Overrides abstract method — IMAX: price * 1.14 tax
        public override decimal CalculateFinalPrice() => Math.Round(Price * 1.14m, 2);

        public override string GetTicketType() => "IMAXTicket";

        // [Ticket #3] Dune | IMAX | 3D: Yes | Price: 130 | Final: 148.20 | Booked: Yes
        public override void PrintTicket()
        {
            Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | IMAX | " +
                $"3D: {(Is3D ? "Yes" : "No")} | " +
                $"Price: {Price} | Final: {CalculateFinalPrice():F2} | Booked: {(IsBooked ? "Yes" : "No")}");
        }

        public override object Clone()
        {
            var clone = new IMAXTicket();
            CopyBaseTo(clone);
            clone.Is3D = this.Is3D;
            return clone;
        }
    }
}
