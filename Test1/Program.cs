using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJackDB;
namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL conn = new DAL();
            
            Console.WriteLine("starting tests:");
            testConn(conn);
            conn.AddUser("john2", "pass", 1000);
            conn.AddUser("john", "pass", 1000);
            testConn(conn);
            Game g = conn.GetGame("153");
            User u = conn.GetUser("john");
            u.money = 5000;
            Console.WriteLine("lucky users is  ID:" + u.ID + " username:" + u.username + " password:" + u.password + " money:" + u.money);
            conn.UpdateDB(u);
            testConn(conn);
            Console.WriteLine("delete tests");
            
            //conn.deleteAllUsers();
            //conn.deleteAllGames();
            Console.WriteLine("end");
        }

        private static void testConn(DAL conn)
        {
            foreach (Game item in conn.GetGames())
            {
                Console.WriteLine("ID:" + item.ID + " IP:" + item.IP + " FirstUser:" + item.FirstUser);
            }
            foreach (User item in conn.GetUsers())
            {
                Console.WriteLine("ID:" + item.ID + " username:" + item.username + " password:" + item.password + " money:" + item.money);
            }
        }
    }
}
