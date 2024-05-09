using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baza_wiazek_przyciskow_20240205.Server;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class MongoDBData
    {
        
        public void ConnectMongoDB() 
        {
            Configurator cServer = new Configurator();

            // Ustawienie połączenia z bazą danych
            var client = new MongoClient(cServer.client);
            var database = client.GetDatabase(cServer.database);
            var collection = database.GetCollection<UpdateDB>(cServer.collection);

            // Definiowanie kryterium wyszukiwania
            var filter = Builders<UpdateDB>.Filter.Eq("author", "Szymon Wojciechowski");

            // Odczytywanie dokumentu
            var update = collection.Find(filter).FirstOrDefault();

            if (update != null)
            {
                MessageBox.Show("Version: " + update.Version);
            }
            else
            {
                MessageBox.Show("No document matches the provided query.");
            }
        }
        public void CheckVersionMongoDB() 
        {
            Configurator cServer = new Configurator();

            // Ustawienie połączenia z bazą danych
            var client = new MongoClient(cServer.client);
            var database = client.GetDatabase(cServer.database);
            var collection = database.GetCollection<UpdateDB>(cServer.collection);

            // Wersja docelowa, poniżej której szukamy użytkowników
            var targetVersion = new Version("2.0");

            // Wyszukiwanie użytkowników z niższą wersją programu
            var users = collection.Find(FilterDefinition<UpdateDB>.Empty).ToList();
            var usersWithOlderVersions = users.Where(user => new Version(user.Version) < targetVersion).ToList();

            foreach (var user in usersWithOlderVersions)
            {
                MessageBox.Show($"Name: {user.Author}, Program Version: {user.Version}");
            }
        }
        public void SendDataToMongoDB(string userVersion, string ip) 
        {
            Configurator cServer = new Configurator();

            // Ustawienie połączenia z bazą danych
            var client = new MongoClient(cServer.client);
            var database = client.GetDatabase(cServer.database);
            var collection = database.GetCollection<UsersDB>(cServer.collection);


            var newUser = new UsersDB()
            {
                //User = userName,
                Version = userVersion,
                IP = ip,
            };

            try 
            {
                collection.InsertOne(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting user: " + ex.Message);
            }
        }
        public bool CheckIPExist(string ip)
        {
            Configurator cServer = new Configurator();

            // Ustawienie połączenia z bazą danych
            var client = new MongoClient(cServer.client);
            var database = client.GetDatabase(cServer.database);
            var collection = database.GetCollection<UsersDB>(cServer.collection);

            // Pobierz podane ip z bazy
            var ipFromServer = Builders<UsersDB>.Filter.Eq("ip", ip);

            // Podaj liczbę znalezionych pozycji
            var count = collection.CountDocuments(ipFromServer);

            return count > 0;
        }
    }
}
