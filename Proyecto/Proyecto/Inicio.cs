using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();

            DataSet dataSet = funciones.fillDropDown();

            comboBox1.DataSource = dataSet.Tables[0];
            comboBox1.ValueMember = "Nombre";
            comboBox1.DisplayMember = "Nombre";
        }

        private void search_Click(object sender, EventArgs e)
        {
            Viajes v = new Viajes(this.comboBox1.SelectedValue.ToString(), true);
            v.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gestion g = new Gestion();
            g.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login l = new login();
            l.ShowDialog();
        }
    }
}
