using System;
using System.Collections.Generic;

namespace IronNinjaDemo
{
    class SpiceHound : Ninja
    {
        // provide override for IsFull (Full at 1200 Calories)
        public override bool IsFull
        {
            get
            {
                return calorieIntake >= 1200;
            }
        }
        public override void Consume(IConsumable item)
        {
            // If NOT Full
            if(!IsFull)
            {
                // adds calorie value to SpiceHound's total calorieIntake (-5 additional calories if the consumable item is "Spicy")
                calorieIntake += item.Calories;
                if(item.IsSpicy)
                {
                    calorieIntake -= 5;
                }
                // adds the randomly selected IConsumable object to SpiceHound's ConsumptionHistory list
                ConsumptionHistory.Add(item);
                // calls the IConsumable object's GetInfo() method
                Console.WriteLine(item.GetInfo());
            }
            // If Full
            else
            {
                // issues a warning to the console that the SpiceHound is full and cannot eat anymore
                Console.WriteLine("OH NO!! Spice Houd is no longer hungry!!");
            }
        }
    }
}