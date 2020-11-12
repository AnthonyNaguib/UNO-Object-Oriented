using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    abstract class WildCard:SpecialCard
    {
        /// <summary>
        /// This method asks the user to select a colour for their wildcard.
        /// </summary>
        public virtual ConsoleColor ChangeColour()
        {
            Console.Clear();
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("ChangeColour has been activated!");
            int userInput = -1;
            ConsoleColor userColour = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1 - Red");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("2 - Blue");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("3 - Green");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("4 - Yellow");
            Console.ForegroundColor = ConsoleColor.White;

            userInput = GetNumberFromUserInRange("Please choose a Colour", 1, 4);

            switch (userInput)
            {
                case 1:
                    userColour = ConsoleColor.Red;
                    break;
                case 2:
                    userColour = ConsoleColor.Blue;
                    break;
                case 3:
                    userColour = ConsoleColor.Green;
                    break;
                case 4:
                    userColour = ConsoleColor.Yellow;
                    break;
            }

            Console.ReadKey();
            Console.Clear();
            return userColour;
        }
    }
}
