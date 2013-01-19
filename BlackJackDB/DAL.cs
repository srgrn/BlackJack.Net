using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace BlackJackDB
{
    public class DAL
    {
        /// <summary>
        /// Get all Games from DB
        /// </summary>
        /// <returns></returns>
        public List<Game> GetGames()
        {
            List<Game> ret = new List<Game>();
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ret = (from g in ctx.Game select g).ToList<Game>();
            };
            return ret;
        }
        /// <summary>
        /// Get User by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(String username)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities()) {
                var ret =
                         (from p in ctx.User
                         where p.username == username
                          orderby p.ID descending
                          select p);
                if (ret.Count<User>() != 0)
                    return ret.First<User>();
            }
            return null;
        }
        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public User GetUser(int ID)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var ret =
                         (from p in ctx.User
                          where p.ID == ID
                          select p);
                if (ret.Count<User>() != 0)
                    return ret.First<User>();
            }
            return null;
        }
        /// <summary>
        /// Get Game by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Game GetGame(int ID)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var ret =
                         (from p in ctx.Game
                         where p.ID == ID
                         select p);
                if (ret.Count<Game>() != 0)
                    return ret.First<Game>();
            }
            return null;
        }
        /// <summary>
        /// Get game by Server IP
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public Game GetGame(string IP)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var ret =
                         from p in ctx.Game
                         where p.IP == IP
                         orderby p.ID descending
                         select p;
                if (ret.Count<Game>() != 0)
                    return ret.First<Game>();
            }
            return null;
        }
        /// <summary>
        /// Get Game by the user that opened it
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Game GetGame(User user)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var ret =
                         from p in ctx.Game
                         where p.FirstUser == user.username
                         orderby p.ID descending
                         select p;
                if (ret.Count<Game>() != 0)
                    return ret.First<Game>();
            }
            return null;
        }
        /// <summary>
        /// Get Games  with only one user in them
        /// </summary>
        /// <returns></returns>
        public List<Game> GetEmptyGames()
        {
            List<Game> ret = new List<Game>();
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ret = (
                    from g in ctx.Game 
                    where g.SecondUser == null
                    select g
                    ).ToList<Game>();
            };
            return ret;
        }
        /// <summary>
        /// Get all Users from DB
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            List<User> ret = new List<User>();
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ret = (from u in ctx.User select u).ToList<User>();
                
            };
            return ret;
        }
        /// <summary>
        /// Add new Game using user object
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="u"></param>
        public Game AddGame(string IP, User u)
        {
            return AddGame(IP, u.username);
        }
        /// <summary>
        /// Add New Game with User ID
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="ID"></param>
        public Game AddGame(string IP, int ID)
        {
            return AddGame(IP, GetUser(ID).username);
        }
        /// <summary>
        /// Add new Game Basic
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="FirstUser"></param>
        public Game AddGame(string IP, string FirstUser)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var maxid = 1;
                if (ctx.Game.Count() > 0)
                {
                    maxid = ctx.Game.Max(u => u.ID);
                    maxid += 1;
                }

                Game newgame = Game.CreateGame(maxid, IP, FirstUser,1,null); // not very cuncurrent and should not be used but the DB is limiting
                ctx.AddToGame(newgame);
                ctx.SaveChanges();
                return newgame;
            }
        }
        /// <summary>
        /// Add a user to the DB
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="money" value="1000">Starting Money</param>
        public User AddUser(string username, string password, int money=1000)
        {

            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                var maxid = 1;
                if (ctx.User.Count() > 0)
                {
                    maxid = ctx.User.Max(u => u.ID);
                    maxid += 1;
                }
                
                User newuser = User.CreateUser(maxid,username,password,money); // not very cuncurrent and should not be used but the DB is limiting
                ctx.AddToUser(newuser);
                ctx.SaveChanges();
                return newuser;
            }
        }
        /// <summary>
        /// Supposed to  recreate the DB according to the model
        /// </summary>
        public void createDB() //doesn't work in mssql compact
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ctx.CreateDatabase();
            }
        }
        /// <summary>
        /// supposed to remove the DB
        /// </summary>
        public void deleteDB() // isn't relevant since createDB doesn't work
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ctx.DeleteDatabase();
            }
        }
        /// <summary>
        /// Remove all Users from DB used for testing
        /// </summary>
        public void deleteAllUsers()
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ctx.SaveChanges();
                foreach (User u in GetUsers())
                {
                    ctx.Attach(u); // to prevent from running the get all query again
                    ctx.User.DeleteObject(u);
                }
                ctx.SaveChanges();
            };
        }
        /// <summary>
        /// Remove all games from DB used for Testing
        /// </summary>
        public void deleteAllGames()
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                foreach (Game g in GetGames())  
                {
                    ctx.Game.DeleteObject(g);
                }
                ctx.SaveChanges();
            }
        }
        /// <summary>
        /// Remove a game from the DB
        /// </summary>
        /// <param name="game">Game to Remove</param>
        public void removeGame(Game game)
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                ctx.Game.Attach(game);
                ctx.DeleteObject(game);
            }
        }
        /// <summary>
        /// Updates the Database with the state of a entity object 
        /// </summary>
        /// <param name="entity">The entity to update</param>
        public void UpdateDB(EntityObject entity) //amazingly enough this is required to allow changing objects in the detached way my dal is working since im closing the context all the time
        {
            using (BlackJackDataEntities ctx = new BlackJackDataEntities())
            {
                if (entity.EntityState == System.Data.EntityState.Modified)
                {
                    object original = null;
                    if (ctx.TryGetObjectByKey(entity.EntityKey, out original))
                    {
                        ctx.ApplyCurrentValues(entity.EntityKey.EntitySetName, entity);
                    }
                    ctx.SaveChanges();
                }

            }
        }
    }
}
