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
        // Konfiguracja linków.
        public string InitialDirectory {  get; set; }
        public string filePath_DATA {  get; set; }
        public string startPath {  get; set; }

        // Konfiguracja serwera.
        public string client {  get; set; }
        public string database { get; set; }
        public string collection {  get; set; }

        public Configurator() 
        {
            //InitialDirectory = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\! Specyfikacje wiązek elektrycznych";
            //filePath_DATA = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\ELCAD\\CAE7.3.2\\schematy_343\\Wojciechowski_sz\\EXCEL_VBA\\AA_Baza_wiązek_kontraktu_Vol_2\\DATA\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
            //startPath = "\\\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\Urbino G4\\";

            // InitialDirectory = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
            // filePath_DATA = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
            // startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";

             InitialDirectory = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
             filePath_DATA = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
             startPath = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";

            // Serwer
            client = "mongodb://localhost:27017";
            database = "baza_wiazek_kontraktu";
            collection = "update";
        }
    }
}


// InitialDirectory = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\! Specyfikacje wiązek elektrycznych";
// filePath_DATA = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\ELCAD\\CAE7.3.2\\schematy_343\\Wojciechowski_sz\\EXCEL_VBA\\AA_Baza_wiązek_kontraktu_Vol_2\\DATA\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "\\solaris_pl\\bolechowo\\Biuro_techniczne\\007_dokumentacja_elektryczna\\Wiązki\\Urbino G4\\";

// LEGION:
// InitialDirectory = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
// filePath_DATA = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "E:\\A_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";

// ASUS:
// InitialDirectory = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu";
// filePath_DATA = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\!NIE_OTWIERAĆ_Baza_wiązek_kontraktu_DATA.xlsx";
// startPath = "C:\\AA_SZYMON_BACKUP\\a_Instalatory\\Visual_Studio_C#\\Baza_wiazek_kontraktu\\";