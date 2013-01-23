using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using GameService;

namespace GameService
{
    public class Client : IMessageCallback, IDisposable
    {
        IMessage pipeProxy = null;
        public bool Connect(String IP)
        {
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                      new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", IP)));
            try
            {

                pipeProxy = pipeFactory.CreateChannel();

                pipeProxy.Subscribe();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void Close()
        {
            pipeProxy.Unsubscribe();
        }



        public string SendMessage(string message)
        {
            try
            {
                pipeProxy.AddMessage(message);
                return "sent >>>>  " + message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void OnMessageAdded(string message, DateTime timestamp)
        {
            
        }

        //We need to tell the server that we are leaving
        public void Dispose()
        {
            pipeProxy.Unsubscribe();
        }




        public void OnGetCard(int cardNum, int cardType, int playerID)
        {
            throw new NotImplementedException();
        }

        public void onGameMessage(string message)
        {
            // should show game message
        }
    }

}
