using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baza_wiazek_przyciskow_20240205.Source;

namespace Baza_wiazek_przyciskow_20240205
{
    public partial class Form2 : Form
    {
        string InitialDirectory;
        string filePath_DATA;
        string startPath;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Option_Click(object sender, EventArgs e)
        {
            InitialDirectory = textBox1.Text;
            filePath_DATA = textBox2.Text;
            startPath = textBox3.Text;
            this.Close();
        }
    }
}
