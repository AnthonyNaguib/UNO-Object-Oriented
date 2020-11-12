using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    class Program
    {
        //Displays some rules I have implemented
        private static void Rules()
        {
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=\n        R U L E S        \n-Must call 'UNO' before placing you're 2nd last card. Failure to do so results in a 2 card penalty.\n-Calling 'UNO' when you don't have uno also results in a 2 card penalty.\n=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
        static void Main(string[] args)
        {
            Rules();
            GameManager gameManager = new GameManager();
            Console.WriteLine("Thank you for playing my game! \n Anthony");
            Console.ReadKey();
        }
    }
}
