using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class Configurator
    {
        public string InitialDirectory {  get; set; }
        public string filePath_DATA {  get; set; }
        public string startPath {  get; set; }

        public Configurator() 
        {
            InitialDirectory = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
            filePath_DATA = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
            startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";
        }
    }
}
