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
        #region GameClient Functions

        public bool Connect(String IP)
        {
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                //new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", IP)));
                      new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", "localhost")));
            try
            {

                pipeProxy = pipeFactory.CreateChannel();

                pipeProxy.Subscribe();
                myID = pipeProxy.join(me.Username, me.money, me.numOfGames, me.ID);
                pipeProxy.resetGame();
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
        public void SendMessage(string message)
        {
            try
            {
                pipeProxy.AddMessage(message);
            }
            catch (Exception e)
            {
                // i really should use exceptyion handling maybe to some log
            }
        }
        public void OnMessageAdded(string message, DateTime timestamp)
        {
            String show = string.Format("{0} - {1}\n", timestamp.ToShortTimeString(), message);
            txt_chat.Text += show;
        }
        #endregion
        #region Form Events
        private void btn_say_Click(object sender, EventArgs e)
        {
            String msg = String.Format("{0}: {1}", me.Username, txt_input.Text);
            new Thread(() => SendMessage(msg)).Start();
            txt_input.Clear();
        }

        private void txt_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                String msg = String.Format("{0}: {1}", me.Username, txt_input.Text);
                new Thread(() => SendMessage(msg)).Start();
                txt_input.Clear();
            }
        }
        #endregion
        #region Game Related Functions

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

        private void btn_deal_Click(object sender, EventArgs e)
        {
            clearCardsImg(0);
            clearCardsImg(1);
            clearCardsImg(2);
            myCards.Clear();
            dealerCards.Clear();
            new Thread(() => pipeProxy.deal()).Start();
            btn_deal.Enabled = false;
            btn_stand.Enabled = true;
            btn_hit.Enabled = true;

        }

        private void clearCardsImg(int player)
        {
            GroupBox grp = Controls.OfType<GroupBox>().FirstOrDefault(g => g.Tag.ToString() == player.ToString());

            foreach (PictureBox pb in grp.Controls.OfType<PictureBox>())
            {
                pb.Image = null;
            }
        }



        public void OnGetCard(int cardNum, int cardType, int playerID)
        {
            Card card = new Card(cardNum, cardType);
            Thread t = null;
            if (playerID == 0)
            {
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

        public void onGameMessage(string message)
        {
            if (message == "GameOver")
            {
                calculateGame(myID);
            }
        }

        private void calculateGame(int myID)
        {
            int myHand = CalculateHand(myCards);
            int dealerHand = CalculateHand(dealerCards);
            if (busted == true || myHand < dealerHand)
            {
                // basicly the money has already been removed from my account.
            }
            else if (dealerHand > 21 || myHand > dealerHand)
            {
                me.money += bet * 2;
            }
            me.numOfGames++;
            new Thread(() => service.updateUser(me)).Start();
            btn_bet.Enabled = true;
            txt_bet.Enabled = true;
            btn_deal.Enabled = false;
            btn_hit.Enabled = false;
            btn_stand.Enabled = false;
            clearCardsImg(0);
            clearCardsImg(1);
            clearCardsImg(2);
            pipeProxy.resetGame();
        }

        private void btn_bet_Click(object sender, EventArgs e)
        {
            int amount =0;
            int.TryParse(txt_bet.Text, out amount);
            bet = amount;
            me.money -= amount;
            new Thread(() => service.updateUser(me)).Start();
            btn_deal.Enabled = true;
            btn_bet.Enabled = false;
            txt_bet.Enabled = false;

        }

        private void btn_hit_Click(object sender, EventArgs e)
        {
           new Thread(() =>  pipeProxy.hit(myID)).Start();
            
        }

        private void btn_stand_Click(object sender, EventArgs e)
        {
            new Thread(() => pipeProxy.stand(myID)).Start();
            btn_hit.Enabled = false;
            btn_stand.Enabled = false;
        }
    }
        #endregion
}
