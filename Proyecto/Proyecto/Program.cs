using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aron.Weiler;

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
            
            //Dictionary<string, Usuarios[]> UsuariosDia = new Dictionary<string, Usuarios[]>();
            int tam = funciones.nDatos();
            //Las llaves son 
            MultiKeyDictionary<string, string, Usuarios[]> UsuariosDiaHora = new MultiKeyDictionary<string, String, Usuarios[]>();
            Usuarios[] datos = new Usuarios[tam];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            datos = funciones.CargarDatos(tam);
            //Console.WriteLine(datos.Length);
            //Console.WriteLine(datos[1].UsuarioGS);
            //Console.WriteLine(datos[1].EntradaGS);

            foreach (string d in Dias)
            {
                funciones.CargarDia(d, ref datos);
                Usuarios[] x = Array.FindAll(datos, element => element.EntradaGS == 4);
                Console.WriteLine(x.Length);
                for (int i = 0;i<x.Length; i++)
                {
                    Console.WriteLine(x[i].UsuarioGS);

                }
               // Console.WriteLine(datos[1].UsuarioGS);
                //Console.WriteLine(datos[1].EntradaGS);

            }
            //Application.Run(new Form1());

        }




    }

    //Clase principal usuarios con sus getter y setter
    public class Usuarios
    {
        string Usuario;
        bool Conduce;
        int NDiasCond;
        int Entrada;
        int Salida;

        public Usuarios()
        {
            Usuario = "NoName";
        }
        public Usuarios(string usuario, int nDiasCond)
        {
            Usuario = usuario;
            NDiasCond = nDiasCond;
        }

        public bool minNDiasCond(Usuarios otro)
        {
            if (this.NDiasCond >= otro.NDiasCond) return true;
            else return false;
        }
        public string UsuarioGS { get => Usuario; set => Usuario = value; }
        public bool ConduceGS { get => Conduce; set => Conduce = value; }
        public int NDiasCondGS { get => NDiasCond; set => NDiasCond = value; }
        public int EntradaGS { get => Entrada; set => Entrada = value; }
        public int SalidaGS { get => Salida; set => Salida = value; }
    }

    //Funciones principales
    public class funciones
    {
        //Funcion para coger el String de conexion, dando igual donde este localizado el proyecto (Teniendo que estar la base de datos en su respectivo sitio en el proyecto)
        public static string getConnectionString()
        {
            string relativePath = @"Proyecto\\Datos.db";
            var parentdir = Path.GetDirectoryName(Application.StartupPath);
            string myString = parentdir.Remove(parentdir.Length - 31, 31);
            string absolutePath = Path.Combine(myString, relativePath);
            string connectionString = string.Format("Data Source={0};Version=3;Pooling=True;Max Pool Size=100;", absolutePath);

            return connectionString;
        }

        //Devuelve el numero de datos que hay en la base de datos
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

        //Devuelve el array de usuarios 
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

        //Carga la hora del dia elegido en sus usuarios
        public static void CargarDia(string dia, ref Usuarios[] data)
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();

                for (int i = 0; i < data.Length; i++)
                {
                    CargarDiaUsuario(dia, ref data[i], command);
                }



            }
        }

        public static void CargarDiaUsuario(string dia, ref Usuarios user, SQLiteCommand com)
        {

            string line = "Entrada" + dia + ", Salida" + dia;

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
                    user.ConduceGS = false;


                }
            }
        }
        //Restaura el booleano a false sobre si conduce un dia o no de todos los usuarios
        public static void RestaurarConducir(ref Usuarios[] data)
        {
            foreach (Usuarios user in data)
            {
                user.ConduceGS = false;
            }
        }

        //Actualiza los datos antiguos con los nuevos
        public static void ActualizarDatos(ref Usuarios[] oldData, ref Usuarios[] newData)
        {
            foreach (Usuarios newUser in newData)
            {
                foreach (Usuarios oldUser in oldData)
                {
                    if (oldUser.UsuarioGS == newUser.UsuarioGS)
                    {
                        oldUser.ConduceGS = newUser.ConduceGS;
                        oldUser.NDiasCondGS = newUser.NDiasCondGS;
                    }
                }
            }
        }

        //Estructuración de los datos por dia, se guardan los datos en un diccionario
        public static void EstructDia(string dia, ref Usuarios[] data, ref MultiKeyDictionary<string, string, Usuarios[]> UsuariosDiaHora)
        {

            //Primero los conductores que van solos

            CondSolitarioDia(ref data);

            //Despues la entrada
            for (int i = 1; i < 6; i++)
            {
                //Coger x.length, dividir entre 5 y a partir de ahi cada 5, se mete un conductor, oon find buscamos los conductores por dia y finalmente creo una instancia de la clase para ver cual es el minimo

                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == i);
                Usuarios min = new Usuarios();
                double nCondDia = 0;
                double supCondDia = x.Length / 5;
                supCondDia = Math.Truncate(supCondDia) + 1;
                Usuarios[] ConductoresSol = Array.FindAll(x, element => element.ConduceGS == true);

                if (x.Length > 1)
                {
                    nCondDia = ConductoresSol.Length;
                    if (min.UsuarioGS == "NoName")
                    {
                        min = x[0];
                    }

                    while (nCondDia < supCondDia)
                    {
                        for (int j = 1; j < x.Length; j++)
                        {

                            if (min.minNDiasCond(x[i]))
                            {
                                min = x[i];
                            }

                        }
                        foreach(Usuarios user in x)
                        {
                            if(user.UsuarioGS == min.UsuarioGS)
                            {
                                user.NDiasCondGS++;
                                user.ConduceGS = true;
                            }
                        }

                        nCondDia++;
                    }
 
                }



                UsuariosDiaHora.Add(dia, "E" + i, x);
                //Console.WriteLine(x.Length);
                //for (int j = 0; j < x.Length; j++)
                //{
                //    Console.WriteLine(x[j].UsuarioGS);

                //}
            }

            //Finalmente la salida


            //Restauramos y actualizamos los valores

        }


        //Preparacion de los datos de los conductores que van o vuelven solos
        public static void CondSolitarioDia(ref Usuarios[] data)
        {
            List<Usuarios> CondSol = new List<Usuarios>();

            for (int i = 1; i < 6; i++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == i);


                if (x.Length == 1)
                {
                    x[0].ConduceGS = true;
                    x[0].NDiasCondGS++;
                    CondSol.Add(x[0]);
                }

            }

            for (int j = 2; j < 7; j++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == j);


                if (x.Length == 1)
                {
                    if (x[0].ConduceGS == false)
                    {
                        x[0].ConduceGS = true;
                        x[0].NDiasCondGS++;
                        CondSol.Add(x[0]);
                    }

                }

            }

            Usuarios[] usuariosSol = CondSol.ToArray();

            ActualizarDatos(ref data, ref usuariosSol);



        }



    }
}
