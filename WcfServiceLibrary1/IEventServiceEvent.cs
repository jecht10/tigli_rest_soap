using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary1
{
    interface IEventServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(int nbVelib, string city, string station);

        [OperationContract(IsOneWay = true)]
        void SubscribeFinished();
    }
}
