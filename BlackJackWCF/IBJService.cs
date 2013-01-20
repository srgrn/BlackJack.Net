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
    [ServiceContract]
    public interface IBJService
    {
        [OperationContract]
        UserWcf login(string username,string password);
        [OperationContract]
        bool logout(UserWcf user);
        [OperationContract]
        bool updateUser(UserWcf u);
        [OperationContract]
        GameWcf addGame(String IP,UserWcf user);
        [OperationContract]
        bool RemoveGameByUser(UserWcf user);
        [OperationContract]
        bool RemoveGameByIP(String IP);
        [OperationContract]
        GameWcf[] GetGames();
        [OperationContract]
        UserWcf getUser(string username);
        [OperationContract]
        bool addUser(UserWcf user,string password);
        
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
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
