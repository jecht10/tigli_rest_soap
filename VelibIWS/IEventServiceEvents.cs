using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibIWS
{
    /*
    interface IEventServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(string nbVelib, string city, string station);

        [OperationContract(IsOneWay = true)]
        void SubscribeFinished();
    }*/
    [ServiceContract]
    interface IEventServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(string nbVelib, string city, string station);

        [OperationContract(IsOneWay = true)]
        void SubscribeFinished(string value);
    }
}
