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
    public partial class Insertar : Form
    {
        public Insertar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                Insert into Usuarios (Usuario, EntradaL, SalidaL, EntradaM, SalidaM, EntradaX, SalidaX, EntradaJ, SalidaJ, EntradaV, SalidaV)
                Values (@user, @EL, @SL, @EM, @SM, @EX, @SX, @EJ, @SJ, @EV, @SV)
                
                ";

               

                    command.Parameters.AddWithValue("@user", user.Text);
                    command.Parameters.AddWithValue("@EL", EL.Value);
                    command.Parameters.AddWithValue("@SL", SL.Value);
                    command.Parameters.AddWithValue("@EM", EM.Value);
                    command.Parameters.AddWithValue("@SM", SM.Value);
                    command.Parameters.AddWithValue("@EX", EX.Value);
                    command.Parameters.AddWithValue("@SX", SX.Value);
                    command.Parameters.AddWithValue("@EJ", EJ.Value);
                    command.Parameters.AddWithValue("@SJ", SJ.Value);
                    command.Parameters.AddWithValue("@EV", EV.Value);
                    command.Parameters.AddWithValue("@SV", SV.Value);
    

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                

            }

            this.Close();
            Gestion gestion = new Gestion();
            gestion.ShowDialog();
        }
    }
}
