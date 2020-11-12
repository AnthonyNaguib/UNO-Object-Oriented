using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    class DrawPile:Deck
    {
        //List of Colours so I only use the Colours needed for the game
        private List<ConsoleColor> CardColours = new List<ConsoleColor> { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.White };
        //List of Integers so I only use the integers in the game
        private List<int> CardNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //Random to use for the shuffle
        Random random = new Random();
        public DrawPile()
        {
            for (int i = 0; i < CardColours.Count; i++)
            {
                ConsoleColor currentCardColour = CardColours[i];
                if (currentCardColour != ConsoleColor.White)
                {
                    for (int j = 0; j < CardNumber.Count; j++)
                    {
                        //Adds Two of each numbered card except for 0 where it only adds one
                        int currentCardNumber = CardNumber[j];
                        if (currentCardNumber == 0)
                        {
                            listOfCards.Add(new NumberedCard() { Colour = currentCardColour, Number = currentCardNumber, IsPlayable = false, HasBeenPlayed = false, Score = 0, HasBeenRead = false });
                        }
                        else
                        {
                            listOfCards.Add(new NumberedCard() { Colour = currentCardColour, Number = currentCardNumber, IsPlayable = false, HasBeenPlayed = false, Score = j, HasBeenRead = false });
                            listOfCards.Add(new NumberedCard() { Colour = currentCardColour, Number = currentCardNumber, IsPlayable = false, HasBeenPlayed = false, Score = j, HasBeenRead = false });
                        }
                    }
                    //Adds Two of each SkipTurn, Reverse and TakeTwo cards
                    listOfCards.Add(new SkipTurn() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                    listOfCards.Add(new SkipTurn() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                    listOfCards.Add(new Reverse() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                    listOfCards.Add(new Reverse() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                    listOfCards.Add(new TakeTwo() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                    listOfCards.Add(new TakeTwo() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 20, HasBeenRead = false });
                }
                else
                {
                    for (int k = 0; k < 4; k++)
                    {
                        //Adds four of each WildCards: TakeFour, SwapHands and ChangeColour
                        listOfCards.Add(new TakeFour() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 50, HasBeenRead = false });
                        listOfCards.Add(new SwapHands() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 50, HasBeenRead = false });
                        listOfCards.Add(new ChangeColour() { Colour = currentCardColour, IsPlayable = false, HasBeenPlayed = false, Score = 50, HasBeenRead = false });
                    }
                }
            }
        }
        public void ShuffleCards()
        {
            for(int i = 0; i < listOfCards.Count; i++)
            { 
                int rng = random.Next(i + 1);
                Card hold = listOfCards[rng];
                listOfCards[rng] = listOfCards[i];
                listOfCards[i] = hold;
            }
        }
    }
}

