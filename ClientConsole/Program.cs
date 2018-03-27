using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            
            VelibSOAP.VelibIWServiceClient client = new VelibSOAP.VelibIWServiceClient();

            Console.WriteLine("Velib IWS Console version");
            Use();

            string input = "";

            while (true)
            {
                Console.Write(">");

                input = Console.ReadLine();
                string[] a = input.Split(' ');
                if (a.Length < 1)
                {
                    continue;
                }

                if (a[0] == "quit")
                {
                    break;
                }
                else if (a[0] == "villes")
                {
                    Console.WriteLine();
                    string[] villes = client.GetVilles();
                    foreach (string ville in villes)
                    {
                        Console.WriteLine(ville);
                    }
                }
                else if (a[0] == "stations")
                {
                    string[] villes = client.GetVilles();
                    if (a.Length < 2 || !contains(villes, a[1]))
                    {
                        Console.WriteLine("nombre d\'arguments invalide ou ville inexistante.");
                        continue;
                    }

                    string val = a[1];
                    string[] stations = client.GetStations(val);
                    if (stations.Length == 0)
                        Console.WriteLine("MERDE");
                    foreach (string station in stations)
                    {
                        Console.WriteLine(station);
                    }
                }
                else if (a[0] == "velib")
                {
                    if (a.Length < 2)
                    {
                        Console.WriteLine("Nombre d\'arguments invalide.");
                        continue;
                    }

                    string val = a[1];
                    if (a.Length > 2)
                    {
                        for (int i = 2; i < a.Length; i++)
                            val += " " + a[i];
                    }
                    val = val.ToLower();
                    string[] res = client.GetNbVelibInStations(val);
                    if(res.Length == 0)
                    {
                        Console.WriteLine("Aucun résultat.");
                        continue;
                    }

                    for(int i = 0; i < res.Length - 1; i += 2)
                    {
                        Console.WriteLine("Station: " + res[i]);
                        Console.WriteLine("Nb velib : " + res[i+1]);
                    }
                }
                else if ("help".Contains(a[0]))
                {
                    Use();
                }
                else
                {
                    Console.WriteLine("Commande inconnue.");
                    continue;
                }
            }
        }
    
        static private void Use()
        {
            Console.WriteLine("Commandes");
            Console.WriteLine("villes - liste des villes disponibles");
            Console.WriteLine("stations <ville> - liste des stations de la ville");
            Console.WriteLine("velib <station> - nb de velib sur la station");
            Console.WriteLine("quit - quitter l'application");
        }

        static private bool contains(string[] list, string val)
        {
            val = val.ToLower();
            foreach (string item in list)
            {
                if (item.ToLower().Contains(val))
                {
                    return true;
                }
            }
            return false;
        }
    }


}
