using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJackWeb.BJServiceRef;


namespace BlackJackWeb
{
    class emptyCallback : BJServiceRef.ServiceCallback
    {
        public void updateGames(String action,GameWcf game)
        {
            // do nithing since this is an empty callback class
        }
    }
}
