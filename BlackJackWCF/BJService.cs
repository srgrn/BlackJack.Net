using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BlackJackDB;

namespace BlackJackWCF
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.Single)]
    public class BJService : IBJService
    {
        public UserWcf login(string username,string password)
        {
            DAL dal = new DAL();
            User u = dal.GetUser(username);
            if (u != null && u.password == password)
            {
                UserWcf user = new UserWcf();
                user.ID = u.ID;
                user.Username = u.username;
                user.money = u.money;
                if(u.numOfGames != null)
                    user.numOfGames = (int)u.numOfGames; // somehow this became in? instead of int
                user.isAdmin = u.isAdmin;
                return user;
            }
            return null ;
        }
        public bool logout(UserWcf user)
        {
            RemoveGameByUser(user);
            updateUser(user);
            // other cleanups can come here
            return true;
        }
        public bool updateUser(UserWcf u)
        {
            DAL dal = new DAL();
            User dbUser = dal.GetUser(u.ID);
            if (dbUser != null)
            {
                dbUser.money = u.money;
                dbUser.numOfGames = u.numOfGames;
                dal.UpdateDB(dbUser);
                return true; // again no exception handling
            }
            return false; // should handle exception better;
        }
        public void addGame(String IP, UserWcf user)
        {
            DAL dal = new DAL();
            Game game = dal.AddGame(IP, user.Username);
            WcfServiceCallback update = OperationContext.Current.GetCallbackChannel<WcfServiceCallback>();
            update.updateGames("add",gameToWCF(game));
        }
        public bool RemoveGameByUser(UserWcf user) 
        {
            DAL dal = new DAL();
            User dbUser = dal.GetUser(user.ID);
            if (dbUser != null)
            {
                Game game = dal.GetGame(dbUser);
                if(game != null)
                {
                    dal.removeGame(game);
                    return true;
                }
            }
            return false;
        }
        public void RemoveGameByIP(String IP)
        {
            DAL dal = new DAL();
            Game game = dal.GetGame(IP);
            if(game != null)
                {
                    dal.removeGame(game);
                    WcfServiceCallback update = OperationContext.Current.GetCallbackChannel<WcfServiceCallback>();
                    update.updateGames("remove", gameToWCF(game));
                }

        }
        public GameWcf[] GetGames()
        {

            DAL dal = new DAL();
            List<GameWcf> ret = new List<GameWcf>();
            foreach (Game g in dal.GetGames())
            {
                ret.Add(gameToWCF(g));
            }
            return ret.ToArray();
        }
        public UserWcf getUser(string username)
        {
            DAL dal = new DAL();
            User user = dal.GetUser(username);
            if (user != null)
                return userToWCF(user);
            return null;
        }
        /// <summary>
        /// Creating a new user in DB 
        /// not very secure
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool addUser(UserWcf user,string password)
        {
            DAL dal = new DAL();
            dal.AddUser(user.Username, password);
            return true;
        }
        public UserWcf[] getUsers()
        {
            DAL dal = new DAL();
            List<UserWcf> ret = new List<UserWcf>();
            foreach (User u in dal.GetUsers())
            {
                ret.Add(userToWCF(u));
            }
            return ret.ToArray();
        }
        /// <summary>
        /// Convert a user entity to UserWCF 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private UserWcf userToWCF(User user)
        {
            UserWcf ret = new UserWcf();
            ret.ID = user.ID;
            ret.Username = user.username;
            ret.money = user.money;
            if(user.numOfGames != null)
                ret.numOfGames = (int)user.numOfGames;
            ret.isAdmin = user.isAdmin;
            return ret;
        }
        /// <summary>
        /// Convert a WCF user to User entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private User WCFtoUser(UserWcf user)
        {
            User ret = new User();
            ret.ID = user.ID;
            ret.username = user.Username;
            ret.money = user.money;
            if (user.numOfGames != null)
                ret.numOfGames = (int)user.numOfGames;
            ret.isAdmin = user.isAdmin;
            return ret;
        }
        /// <summary>
        /// convert a Game Entity object to a WCF Game object
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private GameWcf gameToWCF(Game game){
            GameWcf ret = new GameWcf();
            ret.ID = game.ID;
            ret.IP = game.IP;
            ret.numOfPlayers = game.NumOfUsers;
            ret.FirstUser = game.FirstUser;
            ret.SecondUser = game.SecondUser;
            return ret;
        }
        
        /// <summary>
        /// Convert a WCF Game object into a Game Entity object
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private Game WCFToGame(GameWcf game)
        {
            Game ret = new Game();
            ret.ID = game.ID;
            ret.IP = game.IP;
            ret.NumOfUsers = game.numOfPlayers;
            ret.FirstUser = game.FirstUser;
            ret.SecondUser = game.SecondUser;
            return ret;
        }

    }
}
