using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinClient.BlackJackWebService;
using System.ServiceModel;
using System.Threading;

namespace WinClient
{
    public partial class Login : Form, BlackJackWebService.ServiceCallback
    {
        ServiceClient service = null;
        UserWcf me = null;
        public delegate void LoginThreadAction();
        public LoginThreadAction rejectDelegate;
        public LoginThreadAction continueDelegate;
        public Login()
        {
            InitializeComponent();
            service = new ServiceClient(new InstanceContext(this), "HttpBinding");
            continueDelegate = new LoginThreadAction(accepted);
            rejectDelegate = new LoginThreadAction(rejected);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            string username = txt_username.Text;
            string password = txt_password.Text;
            new Thread( () => service.login(username,password)).Start();
            btn_login.Enabled = false;
            
        }
        private void accepted()
        {
            this.Hide();
            GameChooser gameChooserWindow = new GameChooser(me);
            gameChooserWindow.ShowDialog();
            this.Close();
        }
        public void loginCallback(UserWcf user)
        {
            me = user;
            if (me == null)
            {
                this.Invoke(rejectDelegate);
            }
            else
            {
                this.Invoke(continueDelegate);
            }
        }
        public void updateGames(String action,BlackJackWebService.GameWcf game)
        {
            // do nothign this is an empty callback class
        }
        private void rejected()
        {
            MessageBox.Show("Username or password incorrect");
            btn_login.Enabled = true;
        }
       
    }
}
