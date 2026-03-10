using System;

namespace App_v5
{
    // ============================================================
    // INTERFACES
    // ============================================================

    /// <summary>
    /// Standard contract for any printable object.
    /// </summary>
    public interface IPrintable
    {
        void PrintTicket();
    }

    /// <summary>
    /// Standard contract for booking and cancellation.
    /// </summary>
    public interface IBookable
    {
        bool IsBooked { get; }
        void Book();
        void Cancel();
    }
}
