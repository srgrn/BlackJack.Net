using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinClient.BlackJackWebService;

namespace WinClient
{
    class emptyCallback : BlackJackWebService.ServiceCallback
    {

        public void updateGames(String action,BlackJackWebService.GameWcf game)
        {
            // do nothign this is an empty callback class
        }
        public void loginCallback(UserWcf user)
        {

        }
    }
}
