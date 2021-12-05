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
    }
}
