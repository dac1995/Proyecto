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
        Boolean pass;
        public Insertar(Boolean pass)
        {
            this.pass = pass;
            InitializeComponent();
            DataSet dataSet = funciones.fillDropDown();
            if(this.pass == false)
            {
                passwordbox.Hide();
                label13.Hide();
            }
            comboBox1.DataSource = dataSet.Tables[0];
            comboBox1.ValueMember = "Nombre";
            comboBox1.DisplayMember = "Nombre";

            EL.ReadOnly = true;
            SL.ReadOnly = true;
            EM.ReadOnly = true;
            SM.ReadOnly = true;
            EX.ReadOnly = true;
            SX.ReadOnly = true;
            EJ.ReadOnly = true;
            SJ.ReadOnly = true;
            EV.ReadOnly = true;
            SV.ReadOnly = true;

            EL.BackColor = Color.White;
            SL.BackColor = Color.White;
            EM.BackColor = Color.White;
            SM.BackColor = Color.White;
            EX.BackColor = Color.White;
            SX.BackColor = Color.White;
            EJ.BackColor = Color.White;
            SJ.BackColor = Color.White;
            EV.BackColor = Color.White;
            SV.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(SL.Value >= EL.Value && SM.Value >= EM.Value && SX.Value >= EX.Value && SJ.Value >= EJ.Value && SV.Value >= EV.Value)
                {

                
                using (var connection = new SQLiteConnection(funciones.getConnectionString()))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                    Insert into Usuarios (Usuario, EntradaL, SalidaL, EntradaM, SalidaM, EntradaX, SalidaX, EntradaJ, SalidaJ, EntradaV, SalidaV, Zona, Password)
                    Values (@user, @EL, @SL, @EM, @SM, @EX, @SX, @EJ, @SJ, @EV, @SV, @z, @p)";



                    command.Parameters.AddWithValue("@user", user.Text);
                        if(pass == true)
                        {
                            command.Parameters.AddWithValue("@p", passwordbox.Text);

                        }
                        else
                        {
                            command.Parameters.AddWithValue("@p", "password");
                        }
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
                    command.Parameters.AddWithValue("@z", this.comboBox1.SelectedValue.ToString());


                    command.ExecuteNonQuery();
                    command.Parameters.Clear();


                }

                this.Close();
                Gestion gestion = new Gestion();
                gestion.Show();
                } else{
                    MessageBox.Show("Ha introducido valores imposibles o incorrectos");
                }

            }
            catch (SQLiteException sql)
            {

               MessageBox.Show(sql.ToString());
            }

        }
    }
}
