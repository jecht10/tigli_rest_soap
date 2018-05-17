using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibIWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IEventService" à la fois dans le code et le fichier de configuration.
    /*
    [ServiceContract(CallbackContract = typeof(IEventServiceEvents))]
    public interface IEventService
    {
        [OperationContract]
        void Subscribe(string city, string station, int time);

        [OperationContract]
        void SubscribeEvent();

        [OperationContract]
        void SubscribeFinishedEvent();
    }*/
    [ServiceContract(CallbackContract = typeof(IEventServiceEvents))]
    public interface IEventService
    {
        [OperationContract]
        void Subscribe(string city, string station, int time);
    }
}
