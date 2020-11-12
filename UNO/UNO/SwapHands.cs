using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    class SwapHands:WildCard
    {
        /// <summary>
        /// Displays the number of cards all player's have and asks for a player to swap hands with
        /// </summary>
        public void PlayerSwapHands(List<Player> allPlayers, int currentPlayer, bool ascendingIndex)
        {
            if (ascendingIndex == true)
            {
                currentPlayer--;
                if (currentPlayer == -1)
                {
                    currentPlayer = allPlayers.Count - 1;
                }
            }
            else if (ascendingIndex == false)
            {
                currentPlayer++;
                if (currentPlayer > allPlayers.Count - 1)
                {
                    currentPlayer = 0;
                }
            }
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("Swap Hand Card has been activated.");
            Console.WriteLine("You have {0} cards.", allPlayers[currentPlayer].playerHand.listOfCards.Count);
            Console.WriteLine("--------------------");
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (i == currentPlayer)
                {
                    Console.WriteLine("You have {0} cards", allPlayers[currentPlayer].playerHand.listOfCards.Count);
                }
                else
                {

                    Console.WriteLine("{0} - Player {1} has {2} cards.", i, allPlayers[i].Name, allPlayers[i].playerHand.listOfCards.Count);
                }
            }
            Console.WriteLine("--------------------");
            int userInput = GetNumberFromUserInRange("Please select a player to swap hands with", 0, allPlayers.Count - 1);
            Hand holdHand = new Hand();
            holdHand = allPlayers[currentPlayer].playerHand;
            allPlayers[currentPlayer].playerHand = allPlayers[userInput].playerHand;
            allPlayers[userInput].playerHand = holdHand;
            Console.Clear();
            Console.WriteLine("Player {0} has swapped hands with Player {1}!", allPlayers[currentPlayer].Name, allPlayers[userInput].Name);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
