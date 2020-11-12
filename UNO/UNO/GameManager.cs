using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    class GameManager
    {
        //A list of all the players
        List<Player> allPlayers = new List<Player>();
        //The start of the game
        public GameManager()
        {
            //Depending on a single game or multiple game, it will enter the correct block of code
            int menuChoice = SingleGameOrMultiple();
            if (menuChoice == 1)
            {
                bool alreadyInitalised = false;
                DrawPile drawPile = new DrawPile();
                DiscardPile discardPile = new DiscardPile();
                drawPile.ShuffleCards();
                Initialise(drawPile.listOfCards, discardPile.listOfCards, allPlayers, alreadyInitalised);
                StartGame(drawPile.listOfCards, discardPile.listOfCards);
            }
            else if (menuChoice == 2)
            {
                bool alreadyInitalised = false;
                int highestScore = 0;
                int gamePoints = NumberOfPoints();
                //Will keep going around this loop whilst a player still hasn't beaten gamePoints.
                do
                {
                    DrawPile drawPile = new DrawPile();
                    DiscardPile discardPile = new DiscardPile();
                    drawPile.ShuffleCards();
                    Initialise(drawPile.listOfCards, discardPile.listOfCards, allPlayers, alreadyInitalised);
                    StartGame(drawPile.listOfCards, discardPile.listOfCards);
                    alreadyInitalised = true;

                    for (int i = 0; i < allPlayers.Count; i++)
                    {
                        if (allPlayers[i].Score > highestScore)
                        {
                            highestScore = allPlayers[i].Score;
                        }
                    }
                } while (highestScore < gamePoints);

                Console.WriteLine("Congratulations on finishing the entire game! Here are everyone's score:");
                for (int l = 0; l < allPlayers.Count; l++)
                {
                    Console.WriteLine("Player {0} has {1} points!", allPlayers[l].Name, allPlayers[l].Score);
                }
            }
        }
        /// <summary>
        /// Asks the user if they'd like to play a single game, or multiple games.
        /// </summary>
        private static int SingleGameOrMultiple()
        {
            Console.WriteLine("1. Single Game");
            Console.WriteLine("2. Multiple Games");
            int menuSelection = GetNumberFromUserInRange("Please select an option:", 1, 2);

            Console.Clear();
            return menuSelection;
        }
        /// <summary>
        /// Asks the user how many points they'd like to play until
        /// </summary>
        private static int NumberOfPoints()
        {
            int userInput = -1;
            string userInputString;
            bool pleaseBeInteger = false;

            while (!pleaseBeInteger)
            {
                Console.WriteLine("How many points would you like to play to?");
                userInputString = Console.ReadLine();
                if (int.TryParse(userInputString, out userInput))
                {
                    pleaseBeInteger = true;
                }
            }

            Console.Clear();
            return userInput;
        }
        /// <summary>
        /// Asks the user a question, and gets an -integer with a range
        /// </summary>
        private static int GetNumberFromUserInRange(string Question, int Min, int Max)
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
        /// <summary>
        /// Asks for the number of players and their names. 
        /// Then gives all the players a starting hand of 7 cards.
        /// </summary>
        private void Initialise(List<Card> drawPile, List<Card> discardPile, List<Player> players, bool alreadyInitalised)
        {
            int playerCount = -1;
            string playerCountString;
            bool pleaseBeInteger = false;
            //This if statement is used for the multigame setup, so this bit of code doesn't run everytime a game is finished.
            if (alreadyInitalised == false)
            {
                while (!pleaseBeInteger)
                {
                    do
                    {
                        Console.WriteLine("How many players would like to play? 2-10");
                        playerCountString = Console.ReadLine();

                        if (int.TryParse(playerCountString, out playerCount))
                        {
                            pleaseBeInteger = true;
                        }
                    } while (playerCount < 2 || playerCount > 10);
                }
                //Asks the player for their Name and gives them their hand
                for (int i = 0; i < playerCount; i++)
                {
                    Console.WriteLine("Player {0}, what would you like your name to be?", i + 1);
                    string name = Console.ReadLine();

                    Hand pHand = new Hand();
                    List<Card> tempHand = new List<Card>();
                    pHand.listOfCards = tempHand;
                    pHand.listOfCards = AddCard(pHand.listOfCards, drawPile, 7);

                    allPlayers.Add(new Player(name, pHand, 0));
                }
            }
            if (alreadyInitalised == true)//Since the game has already intialised, all you want to do is reset the hand.
            {
                for (int i = 0; i < playerCount; i++)
                {
                    Hand pHand = new Hand();
                    List<Card> tempHand = new List<Card>();
                    pHand.listOfCards = tempHand;
                    pHand.listOfCards = AddCard(pHand.listOfCards, drawPile, 7);

                    allPlayers[i].playerHand = pHand; 
                }
            }
            //Makes sure the start card on the discardPile is a numbered card
            int startInt = 0;
            bool discardPileStarted = false;
            while (!discardPileStarted)
            {
                if (drawPile[startInt] is NumberedCard startCard)
                {
                    discardPile.Add(drawPile[startInt]);
                    discardPile[0].HasBeenPlayed = true;
                    discardPile[0].HasBeenRead = true;
                    drawPile.Remove(drawPile[startInt]);
                    discardPileStarted = true;
                }
                else
                {
                    startInt++;
                }
            }
            Console.Clear();
        }
        /// <summary>
        /// Adds a number of cards from the deck into the hand
        /// </summary>
        private List<Card> AddCard(List<Card> hand, List<Card> deck, int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                hand.Add(deck[0]);
                deck.Remove(deck[0]);
            }
            return hand;
        }
        /// <summary>
        /// Display's what player is playing, and calls WriteContentsToConsole.
        /// </summary>
        private void PlayerTurn(List<Player> players, int j)
        {
            Console.WriteLine("Player's {0}'s turn:", players[j].Name);
            Console.Write("Hand: ");
            WriteContentsToConsole(players[j].playerHand);
        }
        /// <summary>
        /// Goes through every card in a player's hand and displays the card in the colour needed.
        /// </summary>
        private void WriteContentsToConsole(Hand hand)
        {
            for (int i = 0; i < hand.listOfCards.Count; i++)
            {
                //Makes the colour, the actual colour of the card
                Console.ForegroundColor = hand.listOfCards[i].Colour;
                switch (hand.listOfCards[i])
                {
                    case NumberedCard c:
                        Console.Write(c.Number + ", "); ;
                        break;
                    case Reverse c:
                        Console.Write("Reverse, ");
                        break;
                    case SkipTurn c:
                        Console.Write("Skip, ");
                        break;
                    case TakeTwo c:
                        Console.Write("+2, ");
                        break;
                    case TakeFour c:
                        Console.Write("+4, ");
                        break;
                    case SwapHands c:
                        Console.Write("SwapHand, ");
                        break;
                    case ChangeColour c:
                        Console.Write("ChangeColour, ");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
                //Then resets the colour
            }
            Console.WriteLine("");
            Console.WriteLine("Card Count: {0}", hand.listOfCards.Count);
        }
        /// <summary>
        /// Given a card and a string, will display them both
        /// </summary> 
        private void WriteCard(string myString, Card card)
        {
            Console.Write(myString);
            Console.ForegroundColor = card.Colour;
                switch (card)
                {
                    case NumberedCard c:
                        Console.Write(c.Number + ", "); ;
                        break;
                    case Reverse c:
                        Console.Write("Reverse, ");
                        break;
                    case SkipTurn c:
                        Console.Write("Skip, ");
                        break;
                    case TakeTwo c:
                        Console.Write("+2, ");
                        break;
                    case TakeFour c:
                        Console.Write("+4, ");
                        break;
                    case SwapHands c:
                        Console.Write("SwapHand, ");
                        break;
                    case ChangeColour c:
                        Console.Write("ChangeColour, ");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }
        /// <summary>
        /// The main method for playing the game. This includes playing cards, picking up cards, 
        /// calling UNO, and even displaying the winner at the end of that specific game.
        /// </summary>
        private void StartGame(List<Card> drawPile, List<Card> discardPile)
        {
            int currentPlayer = 0;
            bool finishGame = false;
            bool ascendingIndex = true;
            bool calledUNO = false;
            bool canPlay = false;
            ConsoleColor userColor = ConsoleColor.White;
            while (!finishGame) //Used to finish the game once a player reaches zero cards.
            {
                while (discardPile[0].HasBeenPlayed)
                {
                    string userInput = "";
                    bool validInput = false;
                    //Checks for a Reverse card
                    if (discardPile[0] is Reverse discardReverse)
                    {
                        if (discardPile[0].HasBeenRead == false)
                        {
                            if (ascendingIndex == true)
                            {
                                ascendingIndex = false;
                            }
                            else if (ascendingIndex == false)
                            {
                                ascendingIndex = true;
                            }

                            if (ascendingIndex == true)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    currentPlayer++;
                                    if (currentPlayer > allPlayers.Count - 1)
                                    {
                                        currentPlayer = 0;
                                    }
                                }
                            }
                            if (ascendingIndex == false)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    currentPlayer--;
                                    if (currentPlayer == -1)
                                    {
                                        currentPlayer = allPlayers.Count - 1;
                                    }
                                }
                            }
                            discardPile[0].HasBeenRead = true;
                        }
                    }
                    //Checks for a TakeTwo card
                    if (discardPile[0] is TakeTwo discardTakeTwo)
                    {
                        if (discardPile[0].HasBeenRead == false)
                        {
                            AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 2);
                            discardPile[0].HasBeenRead = true;
                            currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                        }
                    }
                    //Checks for a TakeFour card
                    if (discardPile[0] is TakeFour discardTakeFour)
                    {
                        if (discardPile[0].HasBeenRead == false)
                        {
                            AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 4);
                            userColor = discardTakeFour.ChangeColour();
                            discardPile[0].Colour = userColor;
                            discardPile[0].HasBeenRead = true;
                            currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                        }
                    }
                    //Checks for a ChangeColour card
                    if (discardPile[0] is ChangeColour discardChangecolour)
                    {
                        if (discardPile[0].HasBeenRead == false)
                        {
                            userColor = discardChangecolour.ChangeColour();
                            discardPile[0].Colour = userColor;
                            discardPile[0].HasBeenRead = true;
                        }
                    }
                    //Cheks for a SwapHands Card 
                    if (discardPile[0] is SwapHands discardSwapHands)
                    {
                        if (discardPile[0].HasBeenRead == false)
                        {
                            discardSwapHands.PlayerSwapHands(allPlayers, currentPlayer, ascendingIndex);
                            userColor = discardSwapHands.ChangeColour();
                            discardPile[0].Colour = userColor;
                            discardPile[0].HasBeenRead = true;
                        }
                    }
                    //Displays the player's turn and the card in play
                    PlayerTurn(allPlayers, currentPlayer);
                    WriteCard("Card in play; ", discardPile[0]);
                    while (validInput == false)
                    {
                        Console.WriteLine("Type the position of the card you'd like to play, type DRAW to draw a card, or UNO to call Uno");
                        userInput = Console.ReadLine();
                        //Makes sure the user's input is playable integer or DRAW or UNO
                        if((int.TryParse(userInput, out int testingNumb) && (testingNumb >= 1 && testingNumb <= allPlayers[currentPlayer].playerHand.listOfCards.Count)) || userInput == "DRAW" || userInput == "UNO")
                        {
                            validInput = true;
                        }
                    }
                    //Draws a Card
                    if (userInput == "DRAW")
                    {
                        //Draws a card and displays the card drew
                        AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 1);
                        int number = allPlayers[currentPlayer].playerHand.listOfCards.Count;
                        WriteCard("Card Drew: ", allPlayers[currentPlayer].playerHand.listOfCards[number - 1]);
                        Console.ReadKey();
                        //Checks if the card is playable, and if so displays the option to play it.
                        canPlay = CheckIfPlayable(discardPile, allPlayers[currentPlayer].playerHand.listOfCards, number.ToString());
                        bool pleaseBeValid = false;
                        if (canPlay == true)
                        {
                            while (!pleaseBeValid)
                            {
                                Console.WriteLine("Card is valid to play. Would you like to play it? Enter Y for Yes, N for No.");
                                userInput = Console.ReadLine();
                                if (userInput == "Y" || userInput == "y" || userInput == "yes")
                                {
                                    pleaseBeValid = true;
                                    discardPile.Insert(0, allPlayers[currentPlayer].playerHand.listOfCards[number - 1]);
                                    allPlayers[currentPlayer].playerHand.listOfCards.RemoveAt(number - 1);
                                    discardPile[0].HasBeenPlayed = true;
                                    canPlay = true;
                                }
                                else if(userInput == "N" || userInput == "n" || userInput == "no")
                                {
                                    pleaseBeValid = true;
                                }
                            }
                        }
                        //Increments for the next player
                        Console.Clear();
                        currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                    }
                    //Checks for an UNO call
                    else if (userInput == "UNO")
                    {
                        if (allPlayers[currentPlayer].playerHand.listOfCards.Count == 2) //Checks if the player actually has UNO or not
                        {
                            Console.WriteLine("Congratulations, Uno has been called!");
                            calledUNO = true;
                            bool pleaseBeValid = false;
                            while (!pleaseBeValid)
                            {
                                //Asks the user for another input, to be able to play the game.
                                Console.WriteLine("Type the position of the card you'd like to play");
                                userInput = Console.ReadLine();
                                if ((int.TryParse(userInput, out int testingNumb) && (testingNumb >= 1 && testingNumb <= allPlayers[currentPlayer].playerHand.listOfCards.Count)) || userInput == "DRAW")
                                {
                                    pleaseBeValid = true;
                                }
                            }
                            if (userInput == "DRAW")
                            {
                                //Draws a card and displays the card drew
                                AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 1);
                                int number = allPlayers[currentPlayer].playerHand.listOfCards.Count;
                                WriteCard("Card Drew: ", allPlayers[currentPlayer].playerHand.listOfCards[number - 1]);
                                Console.ReadKey();
                                //Checks if the card is playable, and if so displays the option to play it.
                                canPlay = CheckIfPlayable(discardPile, allPlayers[currentPlayer].playerHand.listOfCards, number.ToString());
                                bool validBePlease = false;
                                if (canPlay == true)
                                {
                                    while (!validBePlease)
                                    {
                                        Console.WriteLine("Card is valid to play. Would you like to play it? Enter Y for Yes, N for No.");
                                        userInput = Console.ReadLine();
                                        if (userInput == "Y" || userInput == "y" || userInput == "yes")
                                        {
                                            validBePlease = true;
                                            discardPile.Insert(0, allPlayers[currentPlayer].playerHand.listOfCards[number - 1]);
                                            allPlayers[currentPlayer].playerHand.listOfCards.RemoveAt(number - 1);
                                            discardPile[0].HasBeenPlayed = true;
                                            canPlay = true;
                                        }
                                        else if (userInput == "N" || userInput == "n" || userInput == "no")
                                        {
                                            validBePlease = true;
                                        }
                                    }
                                }
                                //Increments for the next player
                                Console.Clear();
                                currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                            }
                            else
                            {
                                bool invalidInput = false;
                                //Bool to check if the card selected is playable
                                canPlay = CheckIfPlayable(discardPile, allPlayers[currentPlayer].playerHand.listOfCards, userInput);
                                //Checks for a SkipTurn card
                                if (canPlay == true)
                                {
                                    //Plays Card
                                    if (allPlayers[currentPlayer].playerHand.listOfCards[int.Parse(userInput) - 1].IsPlayable == true)
                                    {
                                        discardPile[0].HasBeenRead = true;
                                        discardPile.Insert(0, allPlayers[currentPlayer].playerHand.listOfCards[int.Parse(userInput) - 1]);
                                        allPlayers[currentPlayer].playerHand.listOfCards.RemoveAt(int.Parse(userInput) - 1);
                                        canPlay = true;
                                        discardPile[0].HasBeenPlayed = true;
                                    }
                                }
                                else
                                {
                                    invalidInput = true;
                                    Console.WriteLine("Cannot play that card! Please try again.");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                if (invalidInput == false)
                                {
                                    //Increments for the next player
                                    Console.Clear();
                                    currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                                }
                            }
                        }
                        else //Calling UNO when you don't have UNO results in drawing 2 cards
                        {
                            Console.WriteLine("You have called UNO when you don't have UNO! Penalty: Take 2 Cards!");
                            Console.ReadKey();
                            AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 2);
                        }
                        Console.Clear();
                    }
                    else
                    {
                        bool invalidInput = false;
                        //If player's hand is 2 and he doesn't call UNO, he picks up 2
                        if (allPlayers[currentPlayer].playerHand.listOfCards.Count == 2 && calledUNO == false)
                        {
                            AddCard(allPlayers[currentPlayer].playerHand.listOfCards, drawPile, 2);
                        }
                        //Bool to check if the card selected is playable
                        canPlay = CheckIfPlayable(discardPile, allPlayers[currentPlayer].playerHand.listOfCards, userInput);
                        //Checks if canPlay is true
                        if (canPlay == true)
                        {
                            //Plays Card
                            if (allPlayers[currentPlayer].playerHand.listOfCards[int.Parse(userInput) - 1].IsPlayable == true)
                            {
                                discardPile[0].HasBeenRead = true;
                                discardPile.Insert(0, allPlayers[currentPlayer].playerHand.listOfCards[int.Parse(userInput) - 1]);
                                allPlayers[currentPlayer].playerHand.listOfCards.RemoveAt(int.Parse(userInput) - 1);
                                canPlay = true;
                                discardPile[0].HasBeenPlayed = true;
                            }
                            if (allPlayers[currentPlayer].playerHand.listOfCards.Count == 0) //If the player's hand has no more cards, it finishes the game.
                            {
                                finishGame = true;
                                discardPile[0].HasBeenPlayed = false;
                            }
                        }
                        else
                        {
                            invalidInput = true;
                            Console.WriteLine("Cannot play that card! Please try again.");
                            Console.ReadKey();
                            Console.Clear();

                        }
                        if (invalidInput == false)
                        {
                            //Increments for the next player
                            Console.Clear();
                            currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);
                        }
                    }
                    //Checks to see if card is SkipTurn AND hasn't been read
                    if (discardPile[0] is SkipTurn checkingSkipTurn2 && discardPile[0].HasBeenRead == false)
                    {
                        currentPlayer = NextPlayer(ascendingIndex, currentPlayer, allPlayers);

                        discardPile[0].HasBeenRead = true;
                        Console.WriteLine("Player has been skipped!");
                        Console.ReadKey();
                    }
                    if(drawPile.Count == 2) //If the drawPile only has two more cards, it will empty the discard pile except for the first card and then shuffle.
                    {
                        Repopulate(drawPile, discardPile);
                        Console.WriteLine("Repopulating the draw pile!");
                        Console.ReadKey();
                    }
                }
            }
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            //Iterates through every card still in player's hands and adds them all to addingScore
            for (int k = 0; k < allPlayers.Count; k++)
            {
                if (allPlayers[k].playerHand.listOfCards.Count == 0)
                {
                    Console.WriteLine("Congratulations {0}! You have won the game.", allPlayers[k].Name);
                }
            }
            int addingScore = 0;
            for(int i = 0; i < allPlayers.Count; i++)
            {
                int numberOfCards = allPlayers[i].playerHand.listOfCards.Count;
                for (int j = 0; j < numberOfCards; j++)
                {
                    addingScore = addingScore + allPlayers[i].playerHand.listOfCards[j].Score;
                }      
            }
            //Finds the player with no cards in their hand and adds addingScore to their player's score
            for(int k = 0; k < allPlayers.Count; k++)
            {
                if(allPlayers[k].playerHand.listOfCards.Count == 0)
                {
                    allPlayers[k].Score = allPlayers[k].Score + addingScore;
                }
            }
            //Iterates through every player and giving their scores
            for (int l = 0; l < allPlayers.Count; l++)
            {
                Console.WriteLine("Player {0} has {1} points!", allPlayers[l].Name, allPlayers[l].Score);
            }
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
        /// <summary>
        /// Given a hand, a deck and the user input, this will check if the card choosen is playable.
        /// </summary>
        private bool CheckIfPlayable(List<Card> discardPile, List<Card> Hand, string userInputString)
        {
            bool canPlay = false;
            int userInput = int.Parse(userInputString);
            //Checks the first card on discard pile and the card chosen to see if they're both a numbered card. If they are, it enters the CheckNumber method.
            if (discardPile[0] is NumberedCard discardNumberedCard && Hand[userInput - 1] is NumberedCard playerNumberedCard)
            {                                                         //^Player's Hand
                if (CheckNumber(discardNumberedCard, playerNumberedCard))
                {
                    Hand[userInput - 1].IsPlayable = true;
                    canPlay = true;
                }
            }
            //Checks if they're both skipturn/reverse/taketwo and returns true
            if(discardPile[0] is SkipTurn discardSkipTurn && Hand[userInput - 1] is SkipTurn playerSkipTurn)
            {
                Hand[userInput - 1].IsPlayable = true;
                canPlay = true;
            }
            if (discardPile[0] is Reverse discardReverse && Hand[userInput - 1] is Reverse playerReverse)
            {
                Hand[userInput - 1].IsPlayable = true;
                canPlay = true;
            }
            if (discardPile[0] is TakeTwo discardTakeTwo && Hand[userInput - 1] is TakeTwo playerTakeTwo)
            {
                Hand[userInput - 1].IsPlayable = true;
                canPlay = true;
            }
            //Checks if the card is a wildcard and so will return true
            if(Hand[userInput - 1] is TakeFour playerTakeFour || Hand[userInput - 1] is ChangeColour playerChangecolour || Hand[userInput - 1] is SwapHands playerSwapHands)
            {
                Hand[userInput - 1].IsPlayable = true;
                canPlay = true;
            }
            //Checks to see if the colours match and if so, will treturn true
            if (CheckColour(discardPile[0], Hand[userInput - 1]))
            {
                Hand[userInput - 1].IsPlayable = true;
                canPlay = true;
            }
            return canPlay;
        }
        /// <summary>
        /// Checks to see if two cards have the same colour.
        /// </summary>
        private bool CheckColour(Card cardOnFloor, Card cardInPlay)
        {
            bool canPlay = false;
            if (cardInPlay.Colour == cardOnFloor.Colour)
            {
                canPlay = true;
            }
            return canPlay;
        }
        /// <summary>
        /// Checks to see if two numbered cards have the same number.
        /// </summary>
        private bool CheckNumber(NumberedCard cardOnFloor, NumberedCard cardInPlay)
        {
            bool canPlay = false;
            if (cardInPlay.Number == cardOnFloor.Number)
            {
                canPlay = true;
            }
            return canPlay;
        }
        /// <summary>
        /// Increments the currentPlayer variable, depending on the bool ascendingIndex to make sure 
        /// it acts correctly with a reverse card
        /// </summary>
        private int NextPlayer(bool ascendingIndex, int currentPlayer, List<Player> allPlayers)
        {
            if (ascendingIndex == true)
            {
                currentPlayer++;
                if (currentPlayer > allPlayers.Count - 1)
                {
                    currentPlayer = 0;
                }
            }
            else if (ascendingIndex == false)
            {
                currentPlayer--;
                if (currentPlayer == -1)
                {
                    currentPlayer = allPlayers.Count - 1;
                }
            }
            return currentPlayer;
        }
        /// <summary>
        /// Goes through every card in the discardPile, except the first, and moves them to the draw pile. 
        /// Then shuffles the drawPile.
        /// </summary>
        private static void Repopulate(List<Card> drawPile, List<Card> discardPile)
        {
            for(int i = 0; i < discardPile.Count; i++)
            {
                if(i != 0)
                {
                    //Reseting all bools, and then adding it back to the drawPile
                    discardPile[i].HasBeenPlayed = false;
                    discardPile[i].HasBeenRead = false;
                    discardPile[i].IsPlayable = false;
                    drawPile.Add(discardPile[1]);
                    discardPile.Remove(discardPile[1]);
                }
            }
            //Reshuffling the drawPile
            drawPile = ShuffleCards(drawPile);
        }
        /// <summary>
        /// Shuffles the cards
        /// </summary>
        private static List<Card> ShuffleCards(List<Card> deck)
        {
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                int rng = random.Next(i + 1);
                Card hold = deck[rng];
                deck[rng] = deck[i];
                deck[i] = hold;
            }
            return deck;
        }
    }
}