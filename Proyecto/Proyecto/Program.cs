using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;


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
            //string[] Dias = new string[] { "L", "M", "X", "J", "V" };
            
            //int tam = funciones.nDatos();
            ////Las llaves son una tupla con dos string, una definiendo el dia y otra la hora de entra o salida

            //Dictionary< Tuple<string, string>, Usuarios[]> UsuariosDiaHora = new Dictionary<Tuple<string, string>, Usuarios[]>();
            //Usuarios[] datos = new Usuarios[tam];
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //datos = funciones.CargarDatos(tam);
            ////Console.WriteLine(datos.Length);
            ////Console.WriteLine(datos[1].UsuarioGS);
            ////Console.WriteLine(datos[1].EntradaGS);

            //foreach (string d in Dias)
            //{
            //    funciones.CargarDia(d, ref datos);
            //    funciones.EstructDia(d, ref datos, ref UsuariosDiaHora);
                
            //}

            //Dictionary<string, DataTable> pairs = new Dictionary<string, DataTable>();
            //foreach(string d in Dias)
            //{
                
            //    DataTable tabla = new DataTable(d);
            //    DataColumn col1 = new DataColumn("Hora");
            //    DataColumn col2 = new DataColumn("Conduce");
            //    DataColumn col3 = new DataColumn("No Conduce");
            //    tabla.Columns.Add(col1);
            //    tabla.Columns.Add(col2);
            //    tabla.Columns.Add(col3);

            //    for(int i = 1; i < 6; i++){
            //        Usuarios[] hort = UsuariosDiaHora[Tuple.Create(d,"E"+i)];
            //        DataRow row = tabla.NewRow();

            //        row["Hora"] = "Entrada "+i+"ª hora";
                    
            //        Usuarios[] horaCon = Array.FindAll(hort, element => element.ConduceGS == true);
            //        foreach(Usuarios user in horaCon){

            //            row["Conduce"] = row["Conduce"] + user.UsuarioGS+"; ";
            //        }


            //        Usuarios[] horaNo = Array.FindAll(hort, element => element.ConduceGS == false);
            //        foreach (Usuarios user in horaNo)
            //        {

            //            row["No Conduce"] = row["No Conduce"] + user.UsuarioGS + "; ";
            //        }

            //        tabla.Rows.Add(row);

            //    }

            //    for (int i = 2; i <= 6; i++)
            //    {
            //        Usuarios[] hort = UsuariosDiaHora[Tuple.Create(d, "S" + i)];
            //        DataRow row = tabla.NewRow();

            //        row["Hora"] = "Salida " + i + "ª hora";

            //        Usuarios[] horaCon = Array.FindAll(hort, element => element.ConduceGS == true);
            //        foreach (Usuarios user in horaCon)
            //        {

            //            row["Conduce"] = row["Conduce"] + user.UsuarioGS + "; ";
            //        }


            //        Usuarios[] horaNo = Array.FindAll(hort, element => element.ConduceGS == false);
            //        foreach (Usuarios user in horaNo)
            //        {

            //            row["No Conduce"] = row["No Conduce"] + user.UsuarioGS + "; ";
            //        }

            //        tabla.Rows.Add(row);
            //    }


            //    pairs.Add(d, tabla);
            //    //string log = funciones.getLogString() + DateTime.Now.ToString("yyyy-M-dd HH_mm_ss") + d + ".xml";
            //    //tabla.WriteXml(log);


            //}
            

            //Pruebas

            //Usuarios[] Prueba = UsuariosDiaHora[Tuple.Create("V", "E1")];


            //foreach (Usuarios user in Prueba)
            //{
            //    Console.WriteLine(user.UsuarioGS);
            //    Console.WriteLine(user.ConduceGS);
            //    Console.WriteLine(user.NVecesCondGS);


            //}
            //Application.Run(new Viajes(datos, pairs));
            Application.Run(new Start());

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
        public static string getLogString()
        {


            string relativePath = @"Proyecto\log\";
            var parentdir = Path.GetDirectoryName(Application.StartupPath);
            string myString = parentdir.Remove(parentdir.Length - 31, 31);
            string absolutePath = Path.Combine(myString, relativePath);

            return absolutePath;
        }

        //Devuelve el numero de datos que hay en la base de datos
        public static int nDatos(String zona)
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
                Where Baja = false AND Zona = $z;
                ";
                command.Parameters.AddWithValue("$z", zona);
                x = int.Parse(command.ExecuteScalar().ToString());


            }

            return x;
        }

        //Devuelve el array de usuarios 
        public static Usuarios[] CargarDatos(int x, String zona)
        {
            Usuarios[] datos = new Usuarios[x];
            int i = 0;
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Usuario, NDiasCond
                FROM Usuarios
                Where Baja = false AND Zona = $z;
                ";
                command.Parameters.AddWithValue("$z", zona);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var n = reader.GetInt32(1);


                        datos[i] = new Usuarios(name, n);
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
        public static void EstructDia(string dia, ref Usuarios[] data, ref Dictionary<Tuple<string, string>, Usuarios[]> UsuariosDiaHora, int ncond = 5)
        {

            List<Usuarios> UsuariosSalida = new List<Usuarios>();

            //la entrada, primero conseguimos un vector con todos los datos clonados que sean los usuarios que se van solos

            Usuarios[] salSol = funciones.SalidasSol(data);
            for (int i = 1; i < 7; i++)
            {
                //Coger x.length, dividir entre el numero de plazas y a partir de ahi cada x, se mete un conductor, 
                //con find buscamos los conductores por dia y finalmente creo una instancia de la clase para ver cual es el minimo
                //gracias a la funcion de la clase min, podre saber cual es el minimo


                Usuarios[] ex = Array.FindAll(data, element => element.EntradaGS == i);
                Usuarios min = new Usuarios();
                decimal nCondDia = 0;
                //buscamos en el vector clonado quien se va solo, para tenerlo en cuenta
                //Dejando así un hueco, para más adelante
                foreach (Usuarios user in ex)
                {
                    foreach (Usuarios userSalidaSol in salSol)
                    {
                        if (user.UsuarioGS == userSalidaSol.UsuarioGS)
                        {
                            user.ConduceGS = true;
                            user.NVecesCondGS++;
                            nCondDia++;
                        }
                    }
                }
                //numero de conductores supuesto
                decimal supCondDia = (decimal)ex.Length / ncond;

                if (supCondDia % 1 != 0)
                {
                    supCondDia = Math.Truncate(supCondDia) + 1;

                }

                //Si solo hay un usuario, siempre será conductor
                if (ex.Length == 1)
                {
                    ex[0].ConduceGS = true;
                    ex[0].NVecesCondGS++;

                } //si hay más de uno se descubrirá quien ha conducido menos, y ellos será los posibles conductores
                else if (ex.Length > 1)
                {

                    if (min.UsuarioGS == "NoName")
                    {
                        min = ex[0];
                    }

                    foreach (Usuarios user in ex)
                    {
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

                //Creamos el vector en el diccionario clonando para evitar errores
                Usuarios[] entradas = ex.Select(a => (Usuarios)a.Clone()).ToArray();

                UsuariosDiaHora.Add(new Tuple<string, string>(dia, "E" + i), entradas);

            }

            //Finalmente la salida

            for (int i = 1; i < 7; i++)
            {
                //Será hacer la misma ejecución de antes, teniendo en cuenta quien ha conducido de antes
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

                }
                else if (sx.Length > 1)
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
            //Rellenando así el hueco que hemos dejado anteriormente
            Usuarios[] UsuariosSalidaVector = UsuariosSalida.ToArray();
            for (int i = 1; i < 7; i++)
            {
                foreach (Usuarios user in UsuariosDiaHora[Tuple.Create(dia, "E" + i)])
                {
                    foreach (Usuarios userSalida in UsuariosSalidaVector)
                    {
                        if (userSalida.UsuarioGS == user.UsuarioGS && userSalida.ConduceGS != user.ConduceGS)
                        {
                            user.ConduceGS = true;
                            user.NVecesCondGS++; ;


                        }
                    }
                }
            }

            //Actualizamos y reiniciamos los valores
            for (int n = 1; n < 7; n++)
            {
                //if (n < 6)
                    ActualizarDatos(ref data, UsuariosDiaHora[Tuple.Create(dia, "E" + n)]);

                //f (n > 1)
                    ActualizarDatos(ref data, UsuariosDiaHora[Tuple.Create(dia, "S" + n)]);

            }

            RestaurarConducir(ref data);

        }


        // conductores que van o vuelven solos, sin usar
        public static void CondSolitarioDia(ref Usuarios[] data)
        {
            List<Usuarios> CondSol = new List<Usuarios>();

            for (int i = 1; i < 7; i++)
            {
                Usuarios[] x = Array.FindAll(data, element => element.EntradaGS == i);

                if (x.Length == 1)
                {
                    x[0].ConduceGS = true;
                    x[0].NVecesCondGS++;
                    CondSol.Add(x[0]);
                }
            }

            for (int j = 1; j < 7; j++)
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
        //Los usuarios que conducen de vuelta solos, se obtienen antes para una mejor representación de los datos.
        public static Usuarios[] SalidasSol(Usuarios[] datos)
        {
            List<Usuarios> Sol = new List<Usuarios>();

            Usuarios[] data = datos.Select(a => (Usuarios)a.Clone()).ToArray();

            for (int j = 1; j < 7; j++)
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

            for (int j = 1; j < 7; j++)
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

        public static void ActualizarBaseDeDatos(Usuarios[] users)
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                foreach (Usuarios user in users)
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                    Update Usuarios
                    Set NDiasCond = $dc
                    WHERE Usuario = $id
                     ";

                    command.Parameters.AddWithValue("$dc", user.NVecesCondGS);
                    command.Parameters.AddWithValue("$id", user.UsuarioGS);


                    command.ExecuteNonQuery();


                }

            }
        }


        public static void RestaurarBaseDeDatos()
        {
            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                Update Usuarios
                Set NDiasCond = 0;
                ";

                command.ExecuteNonQuery();
            }

        }

        public static DataSet fillDropDown()
        {
            DataSet dataSet = new DataSet();

            using (var connection = new SQLiteConnection(funciones.getConnectionString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Nombre
                FROM Zonas;
                
                ";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command.CommandText, connection);
                adapter.Fill(dataSet);
                

            }

            return dataSet;
        }

    }



}
