using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                using (var connection = new SQLiteConnection(funciones.getConnectionString()))
                {
                    connection.Open();
                    String p = "";
                    Boolean a = false;
                    String z = "";
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                    SELECT Password, Admin, Zona
                    FROM Usuarios
                    Where Usuario = $s
                    ";
                    command.Parameters.AddWithValue("$s", user.Text);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            p = reader.GetString(0);
                            a = reader.GetBoolean(1);
                            z = reader.GetString(2);
                        }
                    }

                    if (pass.Text == p && a == true)
                    {
                        this.Hide();
                        Inicio i = new Inicio();
                        i.ShowDialog();
                    }
                    else if (pass.Text == p && z !="")
                    {
                        this.Hide();
                        Viajes v = new Viajes(z,false);
                        v.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Datos erroneos, pruebe de nuevo");
                    }
                   
                }

            }
            catch (SQLiteException sql)
            {

                MessageBox.Show(sql.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
