using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto;

namespace Proyecto
{

    public partial class Gestion : Form
    {
        public Gestion()
        {

            DataGridView dView = new DataGridView();

            InitializeComponent();

            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT *
                FROM Usuarios               
                ";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command.CommandText,connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];


            }
        }

        private void Gestion_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            dataGridView1.Update();
            dataGridView1.Refresh();
            

        }
    }
}
