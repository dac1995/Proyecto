using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto;

namespace Proyecto
{
    public partial class Viajes : Form
    {
        Usuarios[] usuarios;

        public Viajes(Usuarios[] Data, Dictionary<string, DataTable> pairs)
        {
            
            InitializeComponent();
            this.usuarios = Data;
            dataL.DataSource = pairs["L"];
            dataM.DataSource = pairs["M"];
            dataX.DataSource = pairs["X"];
            dataJ.DataSource = pairs["J"];
            dataV.DataSource = pairs["V"];
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Viajes_Load(object sender, EventArgs e)
        {

        }

        //Actualizar datos
        private void button1_Click(object sender, EventArgs e)
        {

            funciones.ActualizarBaseDeDatos(this.usuarios);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está usted seguro?", "Seguridad", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               funciones.RestaurarBaseDeDatos();
            }
            else if (dialogResult == DialogResult.No)
            {
                //nothing
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Gestion gestion = new Gestion();
            gestion.Show();

        }
    }
}
