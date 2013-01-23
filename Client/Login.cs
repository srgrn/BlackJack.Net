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
        /// <summary>
        /// Form Constructor
        /// </summary>
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
        /// <summary>
        /// Starting Login with The wcf service using the data from the user
        /// </summary>
        private void login()
        {
            string username = txt_username.Text;
            string password = txt_password.Text;
            //new Thread( () => service.login(username,password)).Start(); // seems there is some problem with the message being lost between the threads
            btn_login.Enabled = false;
            me = service.loginWeb(username, password);
            if (me == null)
                rejected();
            else
                accepted();
            
        }
        /// <summary>
        /// the login was accepted but the server
        /// </summary>
        private void accepted()
        {
            this.Hide();
            GameChooser gameChooserWindow = new GameChooser(me);
            gameChooserWindow.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// Function for using in a delegate for the Threaded login function with the callback
        /// required since the thread of the service cannot directly update the UI thread
        /// </summary>
        /// <param name="user"></param>
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
        /// <summary>
        /// empty function since this class inherits from the Callback class
        /// </summary>
        /// <param name="action"></param>
        /// <param name="game"></param>
        public void updateGames(String action,BlackJackWebService.GameWcf game)
        {
            // do nothign this is an empty callback class
        }
        /// <summary>
        ///The login was rejected by the server        
        /// </summary>
        private void rejected()
        {
            MessageBox.Show("Username or password incorrect");
            btn_login.Enabled = true;
        }
       
    }
}
