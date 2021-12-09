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
        public Viajes()
        {
            
            InitializeComponent();

            string[] Dias = new string[] { "L", "M", "X", "J", "V" };

            int tam = funciones.nDatos();
            //Las llaves son una tupla con dos string, una definiendo el dia y otra la hora de entra o salida

            Dictionary<Tuple<string, string>, Usuarios[]> UsuariosDiaHora = new Dictionary<Tuple<string, string>, Usuarios[]>();
            Usuarios[] datos = new Usuarios[tam];
            Application.EnableVisualStyles();
           // Application.SetCompatibleTextRenderingDefault(false);
            datos = funciones.CargarDatos(tam);
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

                for (int i = 1; i < 6; i++)
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

                for (int i = 2; i <= 6; i++)
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
                //string log = funciones.getLogString() + DateTime.Now.ToString("yyyy-M-dd HH_mm_ss") + d + ".xml";
                //tabla.WriteXml(log);


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

            funciones.ActualizarBaseDeDatos(this.usuarios);
            this.Hide();
            Viajes viajes = new Viajes();
            viajes.ShowDialog();

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Está usted seguro?", "Seguridad", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               funciones.RestaurarBaseDeDatos();
                this.Hide();
                Viajes viajes = new Viajes();
                viajes.ShowDialog();

                this.Close();

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
