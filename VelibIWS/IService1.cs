﻿using System;
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
    }

    [DataContract]
    public class CompositeVille
    {
        string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value;  }
        }
    }

    [DataContract]
    public class CompositeStation
    {
        string name;
        int nbVelib;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int NbVelib
        {
            get { return nbVelib; }
            set { nbVelib = value; }
        }
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