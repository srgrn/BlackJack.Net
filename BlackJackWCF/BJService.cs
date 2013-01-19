using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BlackJackDB;

namespace BlackJackWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
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
                user.numOfGames = (int)u.numOfGames; // somehow this became in? instead of int
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
        public GameWcf addGame(String IP, UserWcf user)
        {
            DAL dal = new DAL();
            Game game = dal.AddGame(IP, user.Username);
            return gameToWCF(game);

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
        public bool RemoveGameByIP(String IP)
        {
            DAL dal = new DAL();
            Game game = dal.GetGame(IP);
            if(game != null)
                {
                    dal.removeGame(game);
                    return true;
                }
            return false;
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
