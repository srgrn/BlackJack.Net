using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinClient.BlackJackWebService;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Threading;
using GameService;

namespace WinClient
{
    
    public partial class GameChooser : Form , BlackJackWebService.ServiceCallback
    {
        ServiceClient service = null;
        UserWcf me = null;
        List<GameWcf> games = new List<GameWcf>();
        private ServiceHost Host;
        private Thread serverThread = null;
        public GameChooser(UserWcf user)
        {
            InitializeComponent();
            service = new ServiceClient(new InstanceContext(this),"HttpBinding");
            me = user;
            GameWcf[] list = service.GetGames();
            if (list.Count<GameWcf>() != 0)
            {
                games.AddRange(list);
            }
            lstb_games.DataSource = games;
            lstb_games.DisplayMember = "IP";
        }

        private void btn_newGame_Click(object sender, EventArgs e)
        {
            string IP = LocalIPAddress().ToString();
            new Thread(() => service.addGame(IP, me)).Start();;
            Server s = new Server();
            serverThread = new Thread(() => s.Connect(IP));
            serverThread.Start();
            joinGame(IP);
        }

        private void joinGame(string IP)
        {
            
            GameWcf game = (from g in games
                            where g.IP == IP
                            select g).FirstOrDefault<GameWcf>();
            this.Hide();
            GameScreen gameScreen = new GameScreen(me,IP);
            gameScreen.ShowDialog();
            serverThread.Abort();
            new Thread(() => service.RemoveGameByIP(IP));
            this.Show();
            

        }
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            GameWcf g = (GameWcf)lstb_games.SelectedValue;
            joinGame(g.IP);
        }

        private void GameChooser_FormClosed(object sender, FormClosedEventArgs e)
        {
            service.logout(me);
        }

        public void updateGames(String action,GameWcf g)
        {
            if (action == "add")
            {
                games.Add(g);
            }
            else
            {
                games.Remove(g);
            }
            
            lstb_games.DisplayMember = "";
            lstb_games.DisplayMember = "IP";
  
        }
        public void loginCallback(UserWcf user)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWcf g = (GameWcf)lstb_games.SelectedValue;
            new Thread(() => service.RemoveGameByIP(g.IP));
        }
    }
}
