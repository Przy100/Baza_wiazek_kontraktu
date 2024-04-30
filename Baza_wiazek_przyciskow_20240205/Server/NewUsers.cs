using Baza_wiazek_przyciskow_20240205.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            mongoDBData.SendDataToMongoDB(UserId, Version);
        }
    }
}
