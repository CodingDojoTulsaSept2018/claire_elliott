using System;
using System.Collections.Generic;

namespace IronNinjaDemo
{
class Buffet
    {
        public List<IConsumable> Menu;
        public Buffet()
        {
            Menu = new List<IConsumable>()
            {
                new Food("Pizza", 300, true, false),
                new Drink("Coffee", 70, false, false),
                new Food("Asparagus", 80, false, false),
                new Food("Burrito", 400, true, false),
                new Food("Crab Legs", 100, false, false),
                new Drink("Pepsi", 250, false, true)
            };
        }
        public IConsumable Serve()
        {
            Random rand = new Random();
            return Menu[rand.Next(Menu.Count)];
        }

    }
}
