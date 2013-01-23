using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BlackJackWCF;
using System.Threading;

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
        /// <summary>
        /// set up a new game server
        /// </summary>
        /// <param name="IP"></param>
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
        /// <summary>
        /// Close a game server
        /// </summary>
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
        /// <summary>
        /// Register a new client in the game server
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// remove  a client from the game server
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// send a chat message
        /// </summary>
        /// <param name="message"></param>
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
        /// <summary>
        /// Add a palyer to the game
        /// </summary>
        /// <param name="username"></param>
        /// <param name="money"></param>
        /// <param name="numOfGames"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int join(string username, int money, int numOfGames, int ID)
        {
            if (player1 == null)
            {
                player1 = new Player(ID, username, money);
                return 1;
            }
            else if (player2 == null)
            {
                player2 = new Player(ID, username, money);
                return 2;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// release a player from the game
        /// </summary>
        /// <param name="player"></param>
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
        /// <summary>
        /// Deal cards to all players
        /// </summary>
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
            sendGameMessage("GameStarted");
        }
        /// <summary>
        /// generate and set cards to a specific player
        /// </summary>
        /// <param name="id"></param>
        private void dealCards(int id)
        {
            Card card = deck.Draw();
            Card card2 = deck.Draw();
            if (id == 0)
            {
                dealer.AddCard(card);
                dealer.AddCard(card2);
            }
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    
                    callback.OnGetCard(card.CardNum, card.CardType, id);
                    
                    callback.OnGetCard(card2.CardNum, card2.CardType, id);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });
        }
        /// <summary>
        /// Give a single card to a player
        /// </summary>
        /// <param name="player"></param>
        public void hit(int player)
        {
            Card card = deck.Draw();
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (player == 0)
                {
                    dealer.AddCard(card);
                }
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    callback.OnGetCard(card.CardNum, card.CardType, player);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });
        }
        /// <summary>
        /// Get player stand order
        /// </summary>
        /// <param name="player"></param>
        public void stand(int player)
        {
            if (player == 1 && player1 != null)
            {
                player1.stand = true;
            }
            else if (player == 2 && player2 != null)
            {
                player2.stand = true;
            }
            if ((player2 == null || player2.stand || player2.bust) && (player1.stand || player1.bust))
                dealerPlay();
        }
        /// <summary>
        /// Start the Dealer play part of the game
        /// </summary>
        public void dealerPlay()
        {
            // here the dealer will play
            while (dealer.CalculateHand() < 17)
            {
                hit(0);
                
            }
            Thread.Sleep(500); // this is a very wastefull way to enter a delay
            dealer.cards.Clear();
            // since this is the end of the game reset player status to allow new game to commence properly
            if (player1 != null)
            {
                player1.bust = false;
                player1.stand = false;
            }
            if (player2 != null)
            {
                player2.stand = false;
                player2.bust = false;
            }
            // send game over message
            sendGameMessage("GameOver");


        }
        /// <summary>
        /// Reset the game and its objects
        /// </summary>
        public void resetGame()
        {
            if (gameIsNotRunning)
            {
                deck = new Deck();
                deck.Shuffle();
                dealer = new Player(-1, "Dealer", 0);
                if (player1 != null)
                    player1.resetCards();
                if (player2 != null)
                    player2.resetCards();
                gameIsNotRunning = false;

            }

        }
        /// <summary>
        /// Get player bust order
        /// </summary>
        /// <param name="player"></param>
        public void bust(int player)
        {
            if (player == 1 && player1 != null)
            {
                player1.bust = true;
            }
            else if (player == 2 && player2 != null)
            {
                player2.bust = true;
            }
            if ((player2 == null || player2.stand || player2.bust) && (player1.stand || player1.bust))
                dealerPlay();
        }
        /// <summary>
        /// Send a game message to all subscribers
        /// </summary>
        /// <param name="message"></param>
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
        public bool runningGame()
        {
            return gameIsNotRunning;
        }
    }
}
