using System;
using System.ServiceModel;

namespace GameService
{
    //interface declarations just like the client but the callback 
    //decleration is a little different
    [ServiceContract]
    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);
        [OperationContract(IsOneWay = true)]
        void OnGetCard(int cardNum, int cardType, int playerID);
        [OperationContract(IsOneWay = true)]
        void onGameMessage(string message);

    }

    //This is a little different than the client 
    // in that we need to state the SessionMode as required or it will default to "notAllowed"
    [ServiceContract(CallbackContract = typeof(IMessageCallback), SessionMode = SessionMode.Required)]
    public interface IMessage
    {
        [OperationContract]
        void AddMessage(string message);
        [OperationContract]
        bool Subscribe();
        [OperationContract]
        bool Unsubscribe();
        [OperationContract]
        int join(string username, int money, int numOfGames, int ID);
        [OperationContract]
        void leave(int player);
        [OperationContract]
        void deal();
        [OperationContract]
        void hit(int player);
        [OperationContract]
        void stand(int player);
        [OperationContract]
        void dealerPlay();
        [OperationContract]
        void resetGame();
        [OperationContract]
        void bust(int player);


        
    }
}
