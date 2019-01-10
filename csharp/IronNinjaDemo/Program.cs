using System;

namespace IronNinjaDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Buffet buffet = new Buffet();
            SweetTooth st = new SweetTooth();
            SpiceHound sh = new SpiceHound();

            while(!st.IsFull)
            {
                st.Consume(buffet.Serve());
            }
            while(!sh.IsFull)
            {
                sh.Consume(buffet.Serve());
            }
            if(sh.ConsumptionHistory.Count > st.ConsumptionHistory.Count)
            {
                Console.WriteLine($"Spice Hound has {sh.ConsumptionHistory.Count} items.");
            }
            else
            {
                Console.WriteLine($"Sweet Tooth has {st.ConsumptionHistory.Count} items.");
            }
        }
    }
}
