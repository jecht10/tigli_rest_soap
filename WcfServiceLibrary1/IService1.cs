using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    [ServiceContract(CallbackContract = typeof(IEventServiceEvents))]
    public interface IEventService
    {
        [OperationContract]
        void Subscribe(string city, string station, int time);

        [OperationContract]
        void SubscribeEvent();

        [OperationContract]
        void SubscribeFinishedEvent();
    }
}
