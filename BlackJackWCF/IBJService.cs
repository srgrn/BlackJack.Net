using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BlackJackDB;

namespace BlackJackWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBJService" in both code and config file together.
    [ServiceContract(
        Name = "Service",
        SessionMode = SessionMode.Required,
        CallbackContract = typeof(WcfServiceCallback))]
    public interface IBJService
    {
        [OperationContract]
        void login(string username,string password);
        [OperationContract]
        UserWcf loginWeb(string username, string password);
        [OperationContract]
        bool logout(UserWcf user);
        [OperationContract]
        bool updateUser(UserWcf u);
        [OperationContract]
        void addGame(String IP,UserWcf user);
        [OperationContract]
        bool RemoveGameByUser(UserWcf user);
        [OperationContract]
        void RemoveGameByIP(String IP);
        [OperationContract]
        GameWcf[] GetGames();
        [OperationContract]
        UserWcf getUser(string username);
        [OperationContract]
        bool addUser(UserWcf user,string password);
        [OperationContract]
        UserWcf[] getUsers();
        
    }

    /// <summary>
    /// Represantation of a user entity in WCF
    /// </summary>
    [DataContract]
    public class UserWcf
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Username {get; set; }
        [DataMember]
        public int money { get; set; }
        [DataMember]
        public int numOfGames {get; set; }
        [DataMember]
        public bool isAdmin { get; set; }
    }
    /// <summary>
    ///     /// Represantation of a game entity in WCF
    /// </summary>
    [DataContract]
    public class GameWcf
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public int numOfPlayers { get; set; }
        [DataMember]
        public string FirstUser { get; set; }
        [DataMember]
        public string SecondUser { get; set; }
    }
}
