using System;

namespace App_v5
{
    /*

PART 01

----------------------------------------------------------------
Q1: What is abstraction in OOP? How is it different from encapsulation?
----------------------------------------------------------------

ABSTRACTION:
Abstraction means hiding the complexity of HOW something works
and showing only WHAT it does. The user interacts with a simplified
interface without needing to know the internal logic.

ENCAPSULATION:
Encapsulation means hiding the internal DATA of an object and
protecting it from direct access, by wrapping it inside a class
with controlled access through properties or methods.

KEY DIFFERENCE:
Abstraction hides complexity of BEHAVIOR.
Encapsulation hides internal DATA.

REAL-WORLD EXAMPLE (Car):
- Abstraction  : You press the gas pedal to accelerate. You don't
              know how the fuel injection, pistons, or engine
              timing work — that complexity is hidden from you.
- Encapsulation: The engine's fuel level and oil pressure are stored
              internally. You can't directly change them — you
              interact through a dashboard (the interface), which
              controls what you can see and modify.

----------------------------------------------------------------
Q2: Abstract Class vs Interface — 4+ Differences
----------------------------------------------------------------

DIFFERENCE 1 - Implementation:
Abstract Class : Can have concrete (fully implemented) methods.
Interface      : Cannot have implementation (except default
                methods in C# 8+, which are rarely used).

DIFFERENCE 2 - Fields / State:
Abstract Class : Can have fields and instance variables.
Interface      : Cannot have fields or state.

DIFFERENCE 3 - Constructors:
Abstract Class : Can have constructors.
Interface      : Cannot have constructors.

DIFFERENCE 4 - Inheritance:
Abstract Class : A class can inherit only ONE abstract class.
Interface      : A class can implement MULTIPLE interfaces.

DIFFERENCE 5 - Access Modifiers:
Abstract Class : Members can be public, protected, or private.
Interface      : All members are public by default.

DIFFERENCE 6 - Purpose:
Abstract Class : Represents a BASE TYPE with shared behavior.
Interface      : Represents a CONTRACT / CAPABILITY.

WHEN TO CHOOSE ABSTRACT CLASS:
When your classes share common logic or state.
Example: All Tickets share Book() and Cancel() logic,
so Ticket is an abstract class to avoid code duplication.

WHEN TO CHOOSE INTERFACE:
When unrelated classes need to guarantee the same behavior.
Example: IPrintable can apply to a Ticket, an Invoice, or a Report.
They share no common base but all must be printable.

----------------------------------------------------------------
Q3a: Can you write => Appliance a = new Appliance("LG"); ?
----------------------------------------------------------------

NO. Appliance is an abstract class and abstract classes CANNOT
be instantiated directly. The compiler will throw:
"Cannot create an instance of the abstract type 'Appliance'"

The reason is that PowerConsumption() is abstract — it has no
body inside Appliance, so creating an object of it would leave
that method undefined. You must use a concrete subclass like
WashingMachine or Toaster.

----------------------------------------------------------------
Q3b: Difference between PowerConsumption(), Status(), and Label()
----------------------------------------------------------------

PowerConsumption() => ABSTRACT
Every appliance consumes a completely different amount of power.
There is no shared or default value that makes sense — each
subclass MUST provide its own number.

Status() => VIRTUAL
Most appliances default to "Standby", so a default implementation
makes sense. But some (like WashingMachine) have a meaningful
override. Subclasses CAN change it but don't have to.

Label() => CONCRETE
The label format is the same for all appliances — it just calls
the other two methods. No subclass needs to change this, so it
is a regular concrete method.

----------------------------------------------------------------
Q3c: What does Status() return on a Toaster object?
----------------------------------------------------------------

It returns "Standby".

Because Toaster does NOT override Status(). Since Status() is
virtual, it has a default body in Appliance that returns "Standby".
When a subclass doesn't override a virtual method, the base
class version is used automatically.

----------------------------------------------------------------
Q4a: What is a partial class? Why split Calculator into two files?
----------------------------------------------------------------

A partial class is a single class whose definition is SPLIT ACROSS
MULTIPLE FILES using the 'partial' keyword. The compiler merges
all parts into one class at compile time.

A developer splits it to improve organization — one file handles
the core logic (Add, LastResult), while a separate file handles
cross-cutting concerns like logging. This keeps each file focused
and easier to maintain, especially in large projects or when
auto-generated code is involved.

----------------------------------------------------------------
Q4b: What is a partial method? What if the implementation is deleted?
----------------------------------------------------------------

A partial method is a method that is DECLARED in one partial file
and IMPLEMENTED in another. The declaration acts as a placeholder.

If the implementation in Calculator.Logging.cs is deleted —
THE CODE WILL STILL COMPILE. This is by design: if a partial method
has no implementation, the compiler simply removes the call to it
entirely. OnCalculated(LastResult) disappears at compile time as
if it was never written. No error, no warning.

----------------------------------------------------------------
Q4c: What is an extension method? Three rules for writing one?
----------------------------------------------------------------

An extension method lets you ADD NEW METHODS to an existing type
WITHOUT modifying its source code. It feels like the method belongs
to the type when you call it.

THREE RULES:
Rule 1: It must be inside a STATIC CLASS.
Rule 2: The method itself must be STATIC.
Rule 3: The first parameter must have the THIS keyword followed
     by the type being extended (e.g., this double value).

----------------------------------------------------------------
Q4d: What will this code print?
  Calculator calc = new Calculator();
  double result = calc.Add(19.5, 0.5);
  Console.WriteLine(result.ToCurrency());
----------------------------------------------------------------

OUTPUT:
Log: result = 20
$20.00

WHY:
- Add(19.5, 0.5) calculates 20, stores it in LastResult,
then calls OnCalculated(20).
- OnCalculated is implemented in the logging file,
so it prints => Log: result = 20
- result is 20.0 (a double), and ToCurrency() formats
it as => $20.00


*/

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
