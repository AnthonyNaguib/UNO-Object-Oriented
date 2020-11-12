using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    abstract class Deck
    {
        //All Decks will have a list of cards, and so, the property is in this class.
        public List<Card> listOfCards { get; set; }
        public Deck()
        {
            List<Card> temp = new List<Card>();
            listOfCards = temp;
        }
    }
}
