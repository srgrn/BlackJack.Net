using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace GameService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Server : IMessage
    {
        private static List<IMessageCallback> subscribers = new List<IMessageCallback>();
        public ServiceHost host = null;


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
                Console.WriteLine(e.Message);
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
                    Console.WriteLine("Calling OnMessageAdded on callback ({0}).", callback.GetHashCode());
                    callback.OnMessageAdded(message, DateTime.Now);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });

        }
    }
}
