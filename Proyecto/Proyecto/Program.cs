using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var connection = new SQLiteConnection("Data Source=C:\\Users\\estudiante\\Documents\\GitHub\\Proyecto\\Datos.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT Usuario
                FROM Usuarios
                WHERE Usuario = $id
                ";
                command.Parameters.AddWithValue("$id", "J");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }
            }
            Application.Run(new Form1());
            
        }


    }

    class Usuarios
    {
        string Usuario;
        bool Conduce;
        int NDiasCond;
        int Entrada;
        int Salida;
    }

}
