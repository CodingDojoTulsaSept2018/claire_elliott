using System;
using System.Collections.Generic;

namespace IronNinjaDemo
{
    class SweetTooth : Ninja
    {
        public override bool IsFull
        {
            get
            {
                return calorieIntake >= 1500;
            }
        }
        public override void Consume(IConsumable item)
        {
            // If NOT Full
            if(!IsFull)
            {
                // adds calorie value to SweetTooth's total calorieIntake (+10 additional calories if the consumable item is "Sweet")
                calorieIntake += item.Calories;
                if(item.IsSweet)
                {
                    calorieIntake += 10;
                }
                // adds the randomly selected IConsumable object to SweetTooth's ConsumptionHistory list
                ConsumptionHistory.Add(item);
            // calls the IConsumable object's GetInfo() method
            Console.WriteLine(item.GetInfo());
            }
            // If Full
            else
            {
                // issues a warning to the console that the SweetTooth is full and cannot eat anymore
                Console.WriteLine("Oh no! Sweet Tooth is full!");
            }
        }
    }
}





