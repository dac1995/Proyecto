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
            
            //Dictionary<string, Usuarios[]> UsuariosDia = new Dictionary<string, Usuarios[]>();
            int tam = funciones.nDatos();
            //Las llaves son 

            Dictionary< Tuple<string, string>, Usuarios[]> UsuariosDiaHora = new Dictionary<Tuple<string, string>, Usuarios[]>();
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
                funciones.EstructDia(d, ref datos, ref UsuariosDiaHora);
                
            }

            Usuarios[] Prueba = UsuariosDiaHora[Tuple.Create("V", "E1")];


            foreach (Usuarios user in Prueba)
            {
                Console.WriteLine(user.UsuarioGS);
                Console.WriteLine(user.ConduceGS);
                Console.WriteLine(user.NVecesCondGS);


            }
            ////Application.Run(new Form1());*/

        }




    }

    //Clase principal usuarios con sus getter y setter
    public class Usuarios: ICloneable
    {
        string Usuario;
        bool Conduce;
        int nVecesCond;
        int Entrada;
        int Salida;
        
        public Usuarios()
        {
            Usuario = "NoName";
        }
        public Usuarios(string usuario, int nVecesCond)
        {
            Usuario = usuario;
            this.nVecesCond = nVecesCond;
        }

        public Usuarios(string usuario, bool conduce, int nVecesCond, int entrada, int salida)
        {
            Usuario = usuario;
            Conduce = conduce;
            this.nVecesCond = nVecesCond;
            Entrada = entrada;
            Salida = salida;
        }

        public bool minNDiasCond(Usuarios otro)
        {
            if (this.nVecesCond >= otro.nVecesCond) return true;
            else return false;
        }

        public object Clone()
        {
           
            Usuarios a = new Usuarios(this.Usuario, this.Conduce, this.nVecesCond, this.Entrada, this.Salida);
            return a;
        }

        public string UsuarioGS { get => Usuario; set => Usuario = value; }
        public bool ConduceGS { get => Conduce; set => Conduce = value; }
        public int NVecesCondGS { get => nVecesCond; set => nVecesCond = value; }
        public int EntradaGS { get => Entrada; set => Entrada = value; }
        public int SalidaGS { get => Salida; set => Salida = value; }
    }

    //Funciones principales
    public class funciones
    {
        //private int Asientos = 5;

        //public int AsientosGS { get => Asientos; set => Asientos = value; }

        //public funciones(int asientos)
        //{
        //    Asientos = asientos;
        //}


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
        public static void ActualizarDatos(ref Usuarios[] oldData, Usuarios[] newData)
        {
            foreach (Usuarios newUser in newData)
            {
                foreach (Usuarios oldUser in oldData)
                {
                    if (oldUser.UsuarioGS == newUser.UsuarioGS)
                    {
                        oldUser.ConduceGS = newUser.ConduceGS;
                        oldUser.NVecesCondGS = newUser.NVecesCondGS;
                    }
                }
            }
        }

        //Estructuración de los datos por dia, se guardan los datos en un diccionario
        public static void EstructDia(string dia, ref Usuarios[] data, ref Dictionary<Tuple<string, string>, Usuarios[]> UsuariosDiaHora, int ncond=5)
        {

            List<Usuarios> UsuariosSalida = new List<Usuarios>();

            //Despues la entrada

            Usuarios[] salSol = funciones.SalidasSol(data);
            for (int i = 1; i < 6; i++)
            {
                //Coger x.length, dividir entre 5 y a partir de ahi cada 5, se mete un conductor, oon find buscamos los conductores por dia y finalmente creo una instancia de la clase para ver cual es el minimo


                Usuarios[] ex = Array.FindAll(data, element => element.EntradaGS == i);
                Usuarios min = new Usuarios();
                decimal nCondDia = 0;
                foreach (Usuarios user in ex)
                {
                    foreach(Usuarios userSalidaSol in salSol)
                    {
                        if (user.UsuarioGS == userSalidaSol.UsuarioGS)
                        {
                            user.ConduceGS = true;
                            user.NVecesCondGS++;
                            nCondDia++;
                        }
                    }
                }
                
                decimal supCondDia = (decimal)ex.Length / ncond;

                if(supCondDia % 1 != 0)
                {
                    supCondDia = Math.Truncate(supCondDia) + 1;

                }


                if (ex.Length == 1)
                {
                    ex[0].ConduceGS = true;
                    ex[0].NVecesCondGS++;
                   
                } else if (ex.Length > 1)
                {
                    
                    if (min.UsuarioGS == "NoName")
                    {
                        min = ex[0];
                    }

                    foreach(Usuarios user in ex){
                        if (user.ConduceGS == true)
                            nCondDia++;
                    }


                    while (nCondDia < supCondDia)
                    {
                        for (int j = 1; j < ex.Length; j++)
                        {

                            if (min.minNDiasCond(ex[j]))
                            {
                                min = ex[j];
                            }

                        }
                        foreach (Usuarios user in ex)
                        {
                            if (user.UsuarioGS == min.UsuarioGS)
                            {
                                user.NVecesCondGS++;
                                user.ConduceGS = true;
                            }
                        }

                        nCondDia++;
                    }
 
                }

                
                Usuarios[] entradas = ex.Select(a => (Usuarios)a.Clone()).ToArray();
                
                UsuariosDiaHora.Add(new Tuple<string,string>(dia, "E" + i), entradas);
                //Console.WriteLine(x.Length);
                //for (int j = 0; j < x.Length; j++)
                //{
                //    Console.WriteLine(x[j].UsuarioGS);

                //}
            }

            //Finalmente la salida

            for (int i = 2; i < 7; i++)
            {
                //Coger x.length, dividir entre 5 y a partir de ahi cada 5, se mete un conductor, oon find buscamos los conductores por dia y finalmente creo una instancia de la clase para ver cual es el minimo

                Usuarios[] sx = Array.FindAll(data, element => element.SalidaGS == i);
                Usuarios min = new Usuarios();
                decimal nCondDia = 0;
                decimal supCondDia = (decimal)sx.Length / ncond;
                if (supCondDia % 1 != 0)
                {
                    supCondDia = Math.Truncate(supCondDia) + 1;

                }


                if (sx.Length == 1)
                {
                    if (sx[0].ConduceGS == false)
                    {
                        sx[0].ConduceGS = true;
                        sx[0].NVecesCondGS++;
                        
                    }

                }else if (sx.Length > 1)
                {
                    Usuarios[] Conductores = Array.FindAll(sx, element => element.ConduceGS == true);
                    nCondDia = Conductores.Length;

                    //foreach(Usuarios user in Conductores)
                    //{
                    //    user.NVecesCondGS++;
                    //}

                    if (min.UsuarioGS == "NoName")
                    {
                        min = sx[0];
                    }

                    while (nCondDia < supCondDia)
                    {
                        for (int j = 1; j < sx.Length; j++)
                        {

                            if (min.minNDiasCond(sx[j]))
                            {
                                min = sx[j];
                            }

                        }
                        foreach (Usuarios user in sx)
                        {
                            if (user.UsuarioGS == min.UsuarioGS)
                            {
                                user.NVecesCondGS++;
                                user.ConduceGS = true;
                            }
                        }

                        nCondDia++;
                    }

                }


                Usuarios[] salidas = sx.Select(a => (Usuarios)a.Clone()).ToArray();
 
                UsuariosDiaHora.Add(new Tuple<string, string>(dia, "S" + i), salidas);

                foreach (Usuarios user in salidas)
                {
                    UsuariosSalida.Add(user);
                }
            }

            ;


            //Si se ha añadido algún nuevo conductor a la salida, se añadirá a la entrada
            Usuarios[] UsuariosSalidaVector = UsuariosSalida.ToArray();
            for(int i =1; i < 6; i++)
            {
                foreach(Usuarios user in UsuariosDiaHora[Tuple.Create(dia, "E"+i)])
                {
                    foreach(Usuarios userSalida in UsuariosSalidaVector)
                    {
                        if(userSalida.UsuarioGS == user.UsuarioGS && userSalida.ConduceGS != user.ConduceGS)
                        {
                            user.ConduceGS = true;
                            user.NVecesCondGS++; ;
                           

                        }
                    }
                }
            }

            //Actualizamos y reiniciamos los valores
            for(int n = 1; n< 6;n++)              
            {
                if(n<6)
                ActualizarDatos(ref data, UsuariosDiaHora[Tuple.Create(dia, "E" + n)]);

                if(n>1)
                ActualizarDatos(ref data, UsuariosDiaHora[Tuple.Create(dia, "S" + n)]);

            }

            RestaurarConducir(ref data);

        }


        // conductores que van o vuelven solos, sin usar
        public static void CondSolitarioDia(ref Usuarios[] data)
        {
            List<Usuarios> CondSol = new List<Usuarios>();

            for (int i = 1; i < 6; i++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == i);


                if (x.Length == 1)
                {
                    x[0].ConduceGS = true;
                    x[0].NVecesCondGS++;
                    CondSol.Add(x[0]);
                }

            }

            for (int j = 2; j < 7; j++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.SalidaGS == j);


                if (x.Length == 1)
                {
                    if (x[0].ConduceGS == false)
                    {
                        x[0].ConduceGS = true;
                        x[0].NVecesCondGS++;
                        CondSol.Add(x[0]);
                    }

                }

            }

            Usuarios[] usuariosSol = CondSol.ToArray();

            ActualizarDatos(ref data, usuariosSol);




        }

        public static Usuarios[] SalidasSol( Usuarios[] datos)
        {
            List<Usuarios> Sol = new List<Usuarios>();

            Usuarios[] data = datos.Select(a => (Usuarios)a.Clone()).ToArray();

            for (int j = 2; j < 7; j++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.SalidaGS == j);


                if (x.Length == 1)
                {
                    if (x[0].ConduceGS == false)
                    {
                        x[0].ConduceGS = true;
                        x[0].NVecesCondGS++;
                        Sol.Add(x[0]);
                    }

                }

            }

            return Sol.ToArray();
        }

        public static Usuarios[] EntradasSol(Usuarios[] datos)
        {
            List<Usuarios> Sol = new List<Usuarios>();

            Usuarios[] data = datos.Select(a => (Usuarios)a.Clone()).ToArray();

            for (int j = 1; j < 6; j++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == j);


                if (x.Length == 1)
                {
                    if (x[0].ConduceGS == false)
                    {
                        x[0].ConduceGS = true;
                        x[0].NVecesCondGS++;
                        Sol.Add(x[0]);
                    }

                }

            }

            return Sol.ToArray();
        }


    }
}
