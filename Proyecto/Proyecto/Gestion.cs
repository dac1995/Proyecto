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

            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                UPDATE Usuarios
                SET EntradaL = @EL, SalidaL = @SL, EntradaM = @EM, SalidaM = @SM, EntradaX = @EX, SalidaX = @SX, EntradaJ = @EJ, SalidaJ = @SJ, EntradaV = @EV, SalidaV = @SV, NDiasCond = @ND, Baja = @b
                WHERE Usuario = @user
                ";

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    
                    command.Parameters.AddWithValue("@user", row.Cells[0].Value);
                    command.Parameters.AddWithValue("@EL", row.Cells[1].Value);
                    command.Parameters.AddWithValue("@SL", row.Cells[2].Value);
                    command.Parameters.AddWithValue("@EM", row.Cells[3].Value);
                    command.Parameters.AddWithValue("@SM", row.Cells[4].Value);
                    command.Parameters.AddWithValue("@EX", row.Cells[5].Value);
                    command.Parameters.AddWithValue("@SX", row.Cells[6].Value);
                    command.Parameters.AddWithValue("@EJ", row.Cells[7].Value);
                    command.Parameters.AddWithValue("@SJ", row.Cells[8].Value);
                    command.Parameters.AddWithValue("@EV", row.Cells[9].Value);
                    command.Parameters.AddWithValue("@SV", row.Cells[10].Value);
                    command.Parameters.AddWithValue("@ND", row.Cells[11].Value);
                    command.Parameters.AddWithValue("@b", row.Cells[12].Value);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

            }

            dataGridView1.Update();
            dataGridView1.Refresh();
            
        }

        //Eliminar fila
        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                DELETE
                FROM Usuarios
                WHERE Usuario = @user
                ";

                foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                {
                    command.Parameters.AddWithValue("@user", row.Cells[0].Value);
                    command.ExecuteNonQuery();
                }


            }

            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        //Insertar datos
        private void button3_Click(object sender, EventArgs e)
        {
            Insertar insertar = new Insertar();
            insertar.ShowDialog();

            dataGridView1.Update();
            dataGridView1.Refresh();
        }
    }
}
