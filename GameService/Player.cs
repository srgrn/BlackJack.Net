using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJackWCF;

namespace GameService
{
    class Player : UserWcf
    {
        public List<Card> cards = new List<Card>();
        public int Bet = 0;
        public bool stand = false;

            public Player(int ID, string Username,int money)
            {
                this.ID = ID;
                this.Username = Username;
                this.money = money;
            }
            public Player(UserWcf u)
            {
                this.ID = u.ID;
                this.Username = u.Username;
                this.money = u.money;
            }
            public void AddCard(Card card)
            {
                cards.Add(card);
            }
    }
}
