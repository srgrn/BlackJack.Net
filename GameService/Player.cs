using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJackWCF;

namespace GameService
{
    public class Player : UserWcf
    {
        public List<Card> cards = new List<Card>();
        public int Bet = 0;
        public bool stand = false;
        public bool bust = false;

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

            internal void resetCards()
            {
                cards.Clear();
                bust = false;
                stand = false;
            }
            public int CalculateHand()
            {

                {
                    int val = 0;
                    int numAces = 0;

                    foreach (Card c in cards)
                    {
                        if (c.CardNum == 1)
                        {
                            numAces++;
                            val += 11;
                        }
                        else if (c.CardNum == 10 || c.CardNum == 11 || c.CardNum == 12)
                        {
                            val += 10;
                        }
                        else
                        {
                            val += c.CardNum;
                        }
                    }

                    while (val > 21 && numAces > 0)
                    {
                        val -= 10;
                        numAces--;
                    }

                    return val;
                }
            }
    }
}
