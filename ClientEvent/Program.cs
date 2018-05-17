using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez la ville (exemple = Besancon) :");
            string city = Console.ReadLine();

            Console.WriteLine("Station (exemple = 11 - JEAN CORNET) : ");
            string station = Console.ReadLine();

            VelibServiceCallbackSink objsink = new VelibServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);
            
            EventLibService.CalcServiceClient objClient = new EventLibService.CalcServiceClient(iCntxt);

            objClient.SubscribeCalculatedEvent();
            objClient.SubscribeCalculationFinishedEvent();
            objClient.Calculate(city, station, 0);

            Console.WriteLine("Appuyez sur une touche pour terminer le programme ...");
            Console.ReadKey();
        }
    }
}
