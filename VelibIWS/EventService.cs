using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibIWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "EventService" à la fois dans le code et le fichier de configuration.
    /*
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EventService : IEventService
    {
        public const int DelayMilliseconds = 10000;

        static Action<string, string, string> m_Subscribe = delegate { };
        static Action m_SubscribeFinished = delegate { };

        public void Subscribe(string city, string station, int time)
        {
            VelibIWService service = new VelibIWService();
            string nbVelib = service.GetNbVelib(station);
            m_Subscribe(nbVelib, city, station);
            m_SubscribeFinished();
        }

        public void SubscribeEvent()
        {
            IEventServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IEventServiceEvents>();
            m_Subscribe += subscriber.Subscribe;
        }

        public void SubscribeFinishedEvent()
        {
            IEventServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IEventServiceEvents>();
            m_SubscribeFinished += subscriber.SubscribeFinished;
        }
    }*/
    public class EventService : IEventService
    {
        static Action<string> m_subscribeFinished = delegate { };

        public void Subscribe(string city, string station, int time)
        {
            VelibIWService service = new VelibIWService();
            string nbVelib = service.GetNbVelib(station);
            IEventServiceEvents subscribers = OperationContext.Current.GetCallbackChannel<IEventServiceEvents>();
            m_subscribeFinished += subscribers.SubscribeFinished;
        }
    }
}
