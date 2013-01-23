using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BlackJackWCF;

namespace GameService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Server : IMessage
    {
        private static List<IMessageCallback> subscribers = new List<IMessageCallback>();
        public ServiceHost host = null;
        private Deck deck = null;
        private Player player1 = null;
        private Player player2 = null;
        private Player dealer = null;
        private bool gameIsNotRunning = true;

        public void Connect(String IP)
		{

            ServiceHost host = new ServiceHost(
                typeof(Server),
                //new Uri(String.Format("net.tcp://{0}:8000/GameServer", IP)));
                new Uri("net.tcp://localhost:8000")); // it doesn't seem to work when using IPs for some reason security maybe?
			
				host.AddServiceEndpoint(typeof(IMessage),
				  new NetTcpBinding(),
				  "GameServer");
				
				
				try
				{
					host.Open();
					//host.Close();
				}
				catch (Exception e)
				{

				}
			 
			
		}
        public void disconnect()
        {
            AddMessage("Going down");
            try
            {
                host.Close();
            }
            catch (Exception e)
            {

            }

        }

        public bool Subscribe()
        {
            try
            {
                //Get the hashCode of the connecting app and store it as a connection
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (!subscribers.Contains(callback))
                    subscribers.Add(callback);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Unsubscribe()
        {
            try
            {
                //remove any connection that is leaving
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (subscribers.Contains(callback))
                    subscribers.Remove(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddMessage(String message)
        {
            //Go through the list of connections and call their callback funciton
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    callback.OnMessageAdded(message, DateTime.Now);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });

        }
        public int join(string username, int money, int numOfGames, int ID)
        {
            if (player1 == null)
            {
                player1 = new Player(ID, username, money);
                return 1;
            }
            else if (player2 == null)
            {
                player2 = new Player(ID,username,money);
                return 2;
            }
            else
            {
                return -1;
            }
        }
        public void leave(int player)
        {
            if (player == 1)
            {
                player1 = null;
            }
            else if (player == 2)
            {
                player2 = null;
            }
            string leaveMsg = string.Format("Player {0} has left", player);
            sendGameMessage(leaveMsg);
        }

        public void deal()
        {
            if (player1 != null)
            {
                dealCards(1);
            }
            if (player2 != null)
            {
                dealCards(2);
            }
            dealCards(0);
        }

        private void dealCards(int id)
        {
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    Card card = deck.Draw();
                    callback.OnGetCard(card.CardNum, card.CardType,id);
                    card = deck.Draw();
                    callback.OnGetCard(card.CardNum, card.CardType,id);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });
        }

        public void hit(int player)
        {
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    Card card = deck.Draw();
                    if (player == 0)
                    {
                        dealer.AddCard(card);
                    }
                    callback.OnGetCard(card.CardNum, card.CardType, player);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });
        }

        public void stand(int player)
        {
            if (player == 1 && player1 != null)
            {
                player1.stand = true;
                if(player2 == null || player2.stand || player2.bust)
                    dealerPlay();
            }
            else if (player == 2 && player2 != null)
            {
                player2.stand = true;
                if(player1.stand || player1.bust)
                    dealerPlay();
            }
        }

        public void dealerPlay()
        {
            // here the dealer will play
            while(dealer.CalculateHand() < 17)
            {
                dealerGetCard();                
            }
                sendGameMessage("GameOver");

        }

        private void dealerGetCard()
        {
            hit(0);
        }

        public void resetGame()
        {
            if (gameIsNotRunning)
            { 
            deck = new Deck();
            deck.Shuffle();
            dealer = new Player(-1, "Dealer", 0);
            if(player1 != null)
                player1.resetCards();
            if(player2 != null)
                player2.resetCards();
            gameIsNotRunning = false;
            }
        }

        public void bust(int player)
        {
            if (player == 1 && player1 != null)
            {
                player1.bust = true;
                if (player2 == null || player2.stand || player2.bust)
                    dealerPlay();
            }
            else if (player == 2 && player2 != null)
            {
                player2.bust = true;
                if (player1.stand || player1.bust)
                    dealerPlay();
            }
        }
        private void sendGameMessage(string message)
        {
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    callback.onGameMessage(message);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });

        }
    }
}
