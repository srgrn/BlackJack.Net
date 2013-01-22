using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameService;
using System.ServiceModel;
using System.Threading;
using WinClient.BlackJackWebService;

namespace WinClient
{
    public partial class GameScreen : Form, IMessageCallback, IDisposable
    {
        private UserWcf me;
        IMessage pipeProxy = null;
        private Client client = null;
        #region constructors 

        public GameScreen()
        {
            InitializeComponent();
        }

        public GameScreen(BlackJackWebService.UserWcf me,String IP)
        {
            InitializeComponent();
            this.me = me;
            Connect(IP);
        }
        #endregion
        #region GameClient Functions
        
        public bool Connect(String IP)
        {
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                      //new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", IP)));
                      new EndpointAddress(String.Format("net.tcp://{0}:8000/GameServer", "localhost")));
            try
            {

                pipeProxy = pipeFactory.CreateChannel();

                pipeProxy.Subscribe();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public void Close()
        {
            pipeProxy.Unsubscribe();
        }
        public void SendMessage(string message)
        {
            // this is a simple trick to show the username, i simply add it to the message so the message mechanism is clean
            String msg = String.Format("{0} : {1}", me.Username, message);
            try
            {
                pipeProxy.AddMessage(msg);
            }
            catch (Exception e)
            {
                // i really should use exceptyion handling maybe to some log
            }
        }
        public void OnMessageAdded(string message, DateTime timestamp)
        {
            txt_chat.Text += string.Format("{0} - {1} \n",timestamp.ToShortTimeString(), message);
        }
        public void Dispose()
        {
            pipeProxy.Unsubscribe();
        }
        #endregion
        #region Form Events
        private void btn_say_Click(object sender, EventArgs e)
        {
            new Thread(() => SendMessage(String.Format("{0}: {1}", me.Username, txt_input.Text))).Start();
            txt_input.Clear();
        }

        private void txt_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                new Thread(() => SendMessage(String.Format("{0}: {1}", me.Username, txt_input.Text))).Start();
                txt_input.Clear();
            }
        }
        #endregion
        
        
    }
}
