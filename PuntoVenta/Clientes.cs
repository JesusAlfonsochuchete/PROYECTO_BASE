using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class Clientes : Form
    {
        public Clientes(string username)
        {
            InitializeComponent();
            // Usa el parámetro 'username' si necesitas mostrarlo en la pantalla
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           TIENDA tIENDA = new TIENDA();
            tIENDA.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clientes1 Apartado = new Clientes1();
            Apartado.Show();
        }
    }
}
