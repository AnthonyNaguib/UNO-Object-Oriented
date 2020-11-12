using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    class Player
    {
        //Every player will have a Name, a Hand and a score to use for multigames.
        public string Name { get; set; }
        public Hand playerHand { get; set; }
        public int Score { get; set; }

        public Player(string _name, Hand _playerhand, int _score)
        {
            this.Name = _name;
            this.playerHand = _playerhand;
            this.Score = _score;
        }
    }
}
