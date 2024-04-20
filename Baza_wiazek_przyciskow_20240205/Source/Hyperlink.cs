using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baza_wiazek_przyciskow_20240205.Source
{
    public class Hyperlink
    {
        /// <summary>
        /// Funkcja, która zmienia nazwę hiperłącza wyswietlanego w okienku.
        /// </summary>
        /// <param name="LINK">Ścieżka dostępu.</param>
        /// <param name="NAME">Nazwa wiązki.</param>
        /// <param name="BTE">Numer wiązki.</param>
        public void CreateHyperlink(string[] LINK, string[] NAME, string[] BTE) 
        {
            var form1 = new Form1();
            form1.LinkLabelText = NAME[19] + " " + BTE[19];
            
        }
    }
}
