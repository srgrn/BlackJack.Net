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
                user.numOfGames = u.numOfGames;
                return user;
            }
            return null ;
        }
        public bool logout(UserWcf user)
        {
            RemoveGame(user);
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
            return false; // should handle exptions better;
        }
        GameWcf addGame(String IP, UserWcf user)
        {
            DAL dal = new DAL();
            Game game = dal.AddGame(IP, user.Username);

        }
        bool RemoveGame(UserWcf user) 
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
        public bool RemoveGame(String IP)
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
        private GameWcf gameToWCF(Game game){
            GameWcf ret = new GameWcf();
            ret.ID = game.ID;
            ret.IP = game.IP;
            ret.numOfPlayers = game.NumOfUsers;
            ret.FirstUser = game.FirstUser;
            ret.SecondUser = game.SecondUser;
        }
    }
}
