using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameService
{
    public class Deck
    {
        // Deck Cards
        protected List<Card> cards = new List<Card>();

        // Get a card
        public Card this[int position]
        {
            get { return (Card)cards[position]; }
        }

        public Deck()
        {
            openDeck();
            Shuffle();
        }

        // Get a new Card
        public Card Draw()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int i = 0; i < cards.Count; i++)
            {
                int index1 = i;
                int index2 = random.Next(cards.Count);
                SwapCard(index1, index2);
            }
        }

        private void SwapCard(int index1, int index2)
        {
            Card card = cards[index1];
            cards[index1] = cards[index2];
            cards[index2] = card;
        }
        private void openDeck()
        {
            for (int cardNum = 1; cardNum <= 13; cardNum++)
            {
                for (int cardType = 1; cardType <= 4; cardType++)
                {
                    cards.Add(new Card(cardNum, cardType));
                }
            }
        }
    }
}
