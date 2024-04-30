using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baza_wiazek_przyciskow_20240205.Server;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Driver;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    internal class MongoDBData
    {
        
        public void ConnectMongoDB() 
        {
            Configurator cServer = new Configurator();

            // Ustawienie połączenia z bazą danych
            var client = new MongoClient(cServer.client);
            var database = client.GetDatabase(cServer.database);
            var collection = database.GetCollection<Update>(cServer.collection);

            // Definiowanie kryterium wyszukiwania
            var filter = Builders<Update>.Filter.Eq("author", "Szymon Wojciechowski");

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
            var collection = database.GetCollection<Update>(cServer.collection);


        }
    }
}
