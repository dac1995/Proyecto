using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] Dias = new string[] { "L", "M", "X", "J", "V" };
            int tam = funciones.nDatos();
            Usuarios[] datos = new Usuarios[tam];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            datos = funciones.CargarDatos(tam);
            Console.WriteLine(datos.Length);
            Console.WriteLine(datos[0].UsuarioGS) ;

            //Application.Run(new Form1());

        }




    }

    public class Usuarios
    {
        string Usuario;
        bool Conduce;
        int NDiasCond;
        int Entrada;
        int Salida;

        public Usuarios(string usuario, int nDiasCond)
        {
            Usuario = usuario;
            NDiasCond = nDiasCond;
        }

        public string UsuarioGS { get => Usuario; set => Usuario = value; }
        public bool ConduceGS { get => Conduce; set => Conduce = value; }
        public int NDiasCondGS { get => NDiasCond; set => NDiasCond = value; }
        public int EntradaGS { get => Entrada; set => Entrada = value; }
        public int SalidaGS { get => Salida; set => Salida = value; }
    }

    
    public class funciones
    {

        public static string getConnectionString()
        {
            string relativePath = @"Proyecto\\Datos.db";
            var parentdir = Path.GetDirectoryName(Application.StartupPath);
            string myString = parentdir.Remove(parentdir.Length - 31, 31);
            string absolutePath = Path.Combine(myString, relativePath);
            string connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", absolutePath);

            return connectionString;
        }

        public static int nDatos()
        {
            int x = -1;

            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT count(*)
                FROM Usuarios
                ";

                x = int.Parse(command.ExecuteScalar().ToString());

                
            }

            return x;
        }
        public static Usuarios[] CargarDatos(int x)
        {
            Usuarios[] datos = new Usuarios[x];
            int i = 0;
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Usuario
                FROM Usuarios
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);


                        datos[i] = new Usuarios(name, 0);
                        i++;
                    }
                }
            }
            return datos;
        }

        public static void CargarDia(string dia, ref Usuarios[] data)
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();

                for (int i = 0; i < data.Length; i++)
                {
                    CargarDiaUsuario(dia,ref data[i],command);
                }



            }
        }


        public static void CargarDiaUsuario(string dia, ref Usuarios user, SQLiteCommand com)
        {

            string line = "Entrada"+dia+", Salida"+dia;

            com.CommandText =
               @"
                SELECT " + line + @"
                FROM Usuarios
                WHERE Usuario = $id
                ";
            com.Parameters.AddWithValue("$id", user.UsuarioGS);

            using (var reader = com.ExecuteReader())
            {
                while (reader.Read())
                {
                    var ent = reader.GetInt32(0);
                    var sal = reader.GetInt32(1);


                    user.EntradaGS = ent;
                    user.SalidaGS = sal;


                }
            }
        }
    }
   

}
