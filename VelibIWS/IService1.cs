using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibIWS
{

    [ServiceContract]
    public interface IVelibIWService
    {
        [OperationContract]
        List<string> GetVilles();

        [OperationContract]
        List<string> GetStations(string ville);

        [OperationContract]
        string GetNbVelib(string station);

        [OperationContract]
        List<string> GetNbVelibInStations(string stationsName);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetVilles(AsyncCallback callback, object state);
        List<string> EndGetVilles(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetStations(string ville, AsyncCallback callback, object state);
        List<string> EndGetStations(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetNbVelib(string station, AsyncCallback callback, object state);
        string EndGetNbVelib(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetNbVelibInStations(string stationsName, AsyncCallback callback, object state);
        List<string> EndGetNbVelibInStations(IAsyncResult asyncResult);
    }

    /*
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ajoutez vos opérations de service ici
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "VelibIWS.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }*/
}
