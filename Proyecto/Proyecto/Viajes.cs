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
        //Dictionary<string, DataTable> pairs;
        String zona;
        Boolean a;

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
        public Viajes(String zona, Boolean a)
        {
            
            InitializeComponent();
            DialogResult dialogResult = MessageBox.Show("¿Desea guardar los datos en un registro?", "Log", MessageBoxButtons.YesNo);
            this.zona = zona;
            this.a = a;

            if(a == true)
            {
                button4.Hide();
            }
            else
            {
                button1.Hide();
                button2.Hide();
                button3.Hide();
            }

            string[] Dias = new string[] { "L", "M", "X", "J", "V" };
            this.Text = this.Text + " " + zona;
            int tam = funciones.nDatos(zona);
            //Las llaves son una tupla con dos string, una definiendo el dia y otra la hora de entra o salida

            Dictionary<Tuple<string, string>, Usuarios[]> UsuariosDiaHora = new Dictionary<Tuple<string, string>, Usuarios[]>();
            Usuarios[] datos = new Usuarios[tam];
            Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);
            datos = funciones.CargarDatos(tam,zona);
            //Console.WriteLine(datos.Length);
            //Console.WriteLine(datos[1].UsuarioGS);
            //Console.WriteLine(datos[1].EntradaGS);

            foreach (string d in Dias)
            {
                funciones.CargarDia(d, ref datos);
                funciones.EstructDia(d, ref datos, ref UsuariosDiaHora);

            }

            Dictionary<string, DataTable> pairs = new Dictionary<string, DataTable>();
            foreach (string d in Dias)
            {

                DataTable tabla = new DataTable(d);
                DataColumn col1 = new DataColumn("Hora");
                DataColumn col2 = new DataColumn("Conduce");
                DataColumn col3 = new DataColumn("No Conduce");
                tabla.Columns.Add(col1);
                tabla.Columns.Add(col2);
                tabla.Columns.Add(col3);

                for (int i = 1; i < 7; i++)
                {
                    Usuarios[] hort = UsuariosDiaHora[Tuple.Create(d, "E" + i)];
                    DataRow row = tabla.NewRow();

                    row["Hora"] = "Entrada " + i + "ª hora";

                    Usuarios[] horaCon = Array.FindAll(hort, element => element.ConduceGS == true);
                    foreach (Usuarios user in horaCon)
                    {

                        row["Conduce"] = row["Conduce"] + user.UsuarioGS + "; ";
                    }


                    Usuarios[] horaNo = Array.FindAll(hort, element => element.ConduceGS == false);
                    foreach (Usuarios user in horaNo)
                    {

                        row["No Conduce"] = row["No Conduce"] + user.UsuarioGS + "; ";
                    }

                    tabla.Rows.Add(row);

                }

                for (int i = 1; i < 7; i++)
                {
                    Usuarios[] hort = UsuariosDiaHora[Tuple.Create(d, "S" + i)];
                    DataRow row = tabla.NewRow();

                    row["Hora"] = "Salida " + i + "ª hora";

                    Usuarios[] horaCon = Array.FindAll(hort, element => element.ConduceGS == true);
                    foreach (Usuarios user in horaCon)
                    {

                        row["Conduce"] = row["Conduce"] + user.UsuarioGS + "; ";
                    }


                    Usuarios[] horaNo = Array.FindAll(hort, element => element.ConduceGS == false);
                    foreach (Usuarios user in horaNo)
                    {

                        row["No Conduce"] = row["No Conduce"] + user.UsuarioGS + "; ";
                    }

                    tabla.Rows.Add(row);
                }


                pairs.Add(d, tabla);

                
                if (dialogResult == DialogResult.Yes)
                {

                    string log = funciones.getLogString() + DateTime.Now.ToString("yyyy-M-dd_HH_mm_ss") + d + zona + ".xml";
                    tabla.WriteXml(log);


                }
                else if (dialogResult == DialogResult.No)
                {
                    //nothing
                }
               

            }

            this.usuarios = datos;
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

            DialogResult dialogResult = MessageBox.Show("¿Está usted seguro?", "Seguridad", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                funciones.ActualizarBaseDeDatos(this.usuarios);
                this.Close();
                Viajes viajes = new Viajes(this.zona, this.a);
                viajes.Show();

                

            }
            else if (dialogResult == DialogResult.No)
            {
                //nothing
            }

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está usted seguro?", "Seguridad", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                funciones.RestaurarBaseDeDatos();
                
                Viajes viajes = new Viajes(this.zona, this.a);
                viajes.Show();

               

            }
            else if (dialogResult == DialogResult.No)
            {
                //nothing
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Gestion gestion = new Gestion(this.zona);
            gestion.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            login l = new login();
            l.ShowDialog();
        }
    }
}
