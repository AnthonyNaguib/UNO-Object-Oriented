using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    abstract class Card
    {
        // All Cards have a colour, 3 bools and a score. 
        public ConsoleColor Colour { get; set; }
        public bool IsPlayable { get; set; }
        public bool HasBeenPlayed { get; set; }
        public bool HasBeenRead { get; set; }
        public int Score { get; set; }
        /// <summary>
        /// GetNumberFromUserInRange method is here, so I can access it from all classes than inherit from Card.
        /// </summary>
        protected static int GetNumberFromUserInRange(string Question, int Min, int Max)
        {
            int result = -1;
            string resultString;
            bool pleaseBeInteger = false;

            while (!pleaseBeInteger)
            {
                do
                {
                    Console.WriteLine(Question);
                    Console.WriteLine("Enter a number between " + Min + " and " + Max + " inclusive.");
                    resultString = Console.ReadLine();

                    if (int.TryParse(resultString, out result))
                    {
                        pleaseBeInteger = true;
                    }
                } while (result < Min || result > Max);
            }

            return result;
        }
    }
}
