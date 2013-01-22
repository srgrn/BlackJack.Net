using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameService
{
    public class Card
    {
        public int CardNum;
        public int CardType;

        public Card(int CardNum, int CardType)
        {
            this.CardNum = CardNum;
            this.CardType = CardType;
        }
    }
}
