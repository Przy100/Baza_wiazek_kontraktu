using Baza_wiazek_przyciskow_20240205.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Baza_wiazek_przyciskow_20240205.Server
{
    public class NewUsers
    {
        public Guid UserId { get; set; }
        public string Version { get; set; }

        public NewUsers()
        {
            // Generowanie nowego GUID dla każdego użytkownika
            UserId = Guid.NewGuid();
            // Ustawienie wersji programu.
            Configurator configurator = new Configurator();
            Version = configurator.Version;

            MongoDBData mongoDBData = new MongoDBData();
            mongoDBData.SendDataToMongoDB(UserId, Version, GetIPAddress());
        }
        public string GetIPAddress()
        {
            string ip;
            try
            {
                // Pobranie nazwy hosta komputera lokalnego.
                string hostName = Dns.GetHostName();
                Console.WriteLine("Host name: " + hostName);

                // Pobranie informacji IP dla tego hosta.
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                foreach (IPAddress address in addresses)
                {
                    // Sprawdzenie, czy adres jest adresem IPv4.
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine("IPv4 Address: " + address);
                       return ip =  address.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return "";
            }
            return "";  
        }
    }
}
