using System;
using System.Text;
using System.ServiceModel;

namespace EventsLib
{
    interface ICalcServiceEvents
    {
        [OperationContract(IsOneWay = true)]
        void Calculated(int numberOfBikes, string city, string station);

        [OperationContract(IsOneWay = true)]
        void CalculationFinished();
    }
}
