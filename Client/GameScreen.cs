using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameService;
using System.ServiceModel;
using System.Threading;
using WinClient.BlackJackWebService;

namespace WinClient
{
    public partial class GameScreen : Form, IMessageCallback, IDisposable
    {
        private UserWcf me;
        private int myID = -1;
        private IMessage pipeProxy = null;
        private List<Card> myCards = null;
        private List<Card> dealerCards = null;
        private int bet = 0;
        private ServiceClient service = null;
        bool busted = false;
        #region constructors

        public GameScreen()
        {
            InitializeComponent();
        }

        public GameScreen(UserWcf me, String IP)
        {
            InitializeComponent();
            this.me = me;
            Connect(IP);
            myCards = new List<Card>();
            dealerCards = new List<Card>();
            service = new ServiceClient(new InstanceContext(new emptyCallback()), "HttpBinding");
        }
        #endregion
        /// <summary>
        /// Connect to game server
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool Connect(String IP)
        {

            // Create a pipeFactory
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                //new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", IP)));
                      new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", "localhost")));
            try
            {

                // Creating the communication channel
                pipeProxy = pipeFactory.CreateChannel();
                // register for events 
                pipeProxy.Subscribe();
                // join the game 
                myID = pipeProxy.join(me.Username, me.money, me.numOfGames, me.ID);
                if (pipeProxy.runningGame())
                {
                    pipeProxy.resetGame();
                }
                return true;
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        
        public void Disconnect()
        {
            pipeProxy.Unsubscribe();
        }
        /// <summary>
        /// Send a Chat messege
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            try
            {
                // put the message on the channel
                pipeProxy.AddMessage(message);
            }
            catch (Exception e)
            {
                // i really should use exceptyion handling maybe to some log
            }
        }
        /// <summary>
        /// Callback for the chat message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timestamp"></param>
        public void OnMessageAdded(string message, DateTime timestamp)
        {
            String show = string.Format("{0} - {1}\n", timestamp.ToShortTimeString(), message);
            txt_chat.Text += show;
        }
        /// <summary>
        /// send chat message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_say_Click(object sender, EventArgs e)
        {
            String msg = String.Format("{0}: {1}", me.Username, txt_input.Text);
            new Thread(() => SendMessage(msg)).Start();
            txt_input.Clear();
        }
        /// <summary>
        /// send chat message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                String msg = String.Format("{0}: {1}", me.Username, txt_input.Text);
                new Thread(() => SendMessage(msg)).Start();
                txt_input.Clear();
            }
        }

        /// <summary>
        /// Load the card image for a given card in a given player group
        /// </summary>
        /// <param name="card"></param>
        /// <param name="player"></param>
        private void loadCard(Card card, int player)
        {
            GroupBox grp = Controls.OfType<GroupBox>().FirstOrDefault(g => g.Tag.ToString() == player.ToString());
            PictureBox pb = grp.Controls.OfType<PictureBox>().First(p => p.Image == null);
            string cardPath = "";
            if (card.CardType == 1)
                cardPath += "cl";
            else if (card.CardType == 2)
                cardPath += "di";
            else if (card.CardType == 3)
                cardPath += "he";
            else if (card.CardType == 4)
                cardPath += "sp";
            cardPath += card.CardNum.ToString() + ".gif";
            if (card.Down)
            {
                cardPath = "Back.png";
            }
            String imagePath = string.Format("{0}\\{1}", ".\\images",cardPath);
            pb.Image = new Bitmap(imagePath);

        }
        /// <summary>
        /// handle deal button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deal_Click(object sender, EventArgs e)
        {
            pipeProxy.resetGame();
            clearCardsImg(0);
            clearCardsImg(1);
            clearCardsImg(2);
            myCards.Clear();
            dealerCards.Clear();            
            new Thread(() => pipeProxy.deal()).Start();
        }
        /// <summary>
        /// Clear the card images for given player
        /// </summary>
        /// <param name="player">0 for dealer</param>
        private void clearCardsImg(int player)
        {
            GroupBox grp = Controls.OfType<GroupBox>().FirstOrDefault(g => g.Tag.ToString() == player.ToString());

            foreach (PictureBox pb in grp.Controls.OfType<PictureBox>())
            {
                pb.Image = null;
            }
        }


        /// <summary>
        /// callback for getting a new card from server 
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="cardType"></param>
        /// <param name="playerID"></param>
        public void OnGetCard(int cardNum, int cardType, int playerID)
        {
            Card card = new Card(cardNum, cardType);
            Thread t = null;
            if (playerID == 0)
            {
                // the dealer has his own card list to handle 
                dealerCards.Add(card);
                if (dealerCards.Count == 2)
                { 
                    // need to reload cards later
                    card.Down = true; 
                }
            }else if (playerID == myID)
            {
                myCards.Add(card);
                if (CalculateHand(myCards)> 21)
                {
                    // loading the cards images before going bust
                    t = new Thread(() =>  pipeProxy.bust(myID));

                }
            }
            loadCard(card ,playerID);
            if (t != null)
            {
                t.Start();
                btn_stand.Enabled = false;
                btn_hit.Enabled = false;
                MessageBox.Show("You are busted");
                busted = true;
            }
        }

        private int CalculateHand(List<Card> cards)
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
                    else if (c.CardNum == 11|| c.CardNum == 12 || c.CardNum == 13)
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
        /// <summary>
        /// Callback for hte GameMessage allows for server notification regarding the game
        /// </summary>
        /// <param name="message"></param>
        public void onGameMessage(string message)
        {
            if (message == "GameOver")
            {
                calculateGame(myID);
            }
            if (message == "GameStarted")
            {
                btn_deal.Enabled = false;
                btn_stand.Enabled = true;
                btn_hit.Enabled = true;
            }

        }
        /// <summary>
        /// Calculate who won in the game
        /// </summary>
        /// <param name="myID"></param>
        private void calculateGame(int myID)
        {
            if (myID != -1)
            {
                int myHand = CalculateHand(myCards);
                int dealerHand = CalculateHand(dealerCards);
                myCards.Clear();
                dealerCards.Clear();
                // if the player won
                if (dealerHand > 21 || (myHand > dealerHand && busted== false))
                {
                    String msg = String.Format("You Won: Player Hand = {0} , Dealer Hand = {1}", myHand, dealerHand);
                    MessageBox.Show(msg);
                    me.money += bet * 2;
                }
                else // if (busted == true || myHand < dealerHand) // assuming if i haven't won i have lost
                {
                    String msg = String.Format("You lost: Player Hand = {0} , Dealer Hand = {1}", myHand, dealerHand);
                    MessageBox.Show(msg);
                }
                me.numOfGames++;
                // update user details in DB
                new Thread(() => service.updateUser(me)).Start();
            }
            // Set Gui back to game start
            btn_bet.Enabled = true;
            txt_bet.Enabled = true;
            btn_deal.Enabled = false;
            btn_hit.Enabled = false;
            btn_stand.Enabled = false;
            busted = false;
            clearCardsImg(0);
            clearCardsImg(1);
            clearCardsImg(2);
        }
        /// <summary>
        /// Handle bet button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_bet_Click(object sender, EventArgs e)
        {
            int amount =0;
            int.TryParse(txt_bet.Text, out amount);
            bet = amount;
            me.money -= amount;
            busted = false;
            new Thread(() => service.updateUser(me)).Start();
            if (myID == 1)
            {
                btn_deal.Enabled = true;
            }
            btn_bet.Enabled = false;
            txt_bet.Enabled = false;

        }
        /// <summary>
        /// handle hit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_hit_Click(object sender, EventArgs e)
        {
           new Thread(() =>  pipeProxy.hit(myID)).Start();
            
        }
        /// <summary>
        /// Hnadle stand button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stand_Click(object sender, EventArgs e)
        {
            new Thread(() => pipeProxy.stand(myID)).Start();
            btn_hit.Enabled = false;
            btn_stand.Enabled = false;
        }

        
    }
       
}
