using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace BlackJackWCF
{
    public interface WcfServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void updateGames(String action,GameWcf game);
    }
}
