using MySql.Data.MySqlClient;

namespace proyecto_cafeteria
{
    internal class Modulo

    {
        public static string cadena_conexion = "server=localhost; database=coffee_corner; user=root; password=tics3;";
        public static MySqlConnection conexion = new MySqlConnection(cadena_conexion);
    }
}

