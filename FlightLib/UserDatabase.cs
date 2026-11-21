using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FlightLib
{
    public class UserDatabase
    {
        private SQLiteConnection cnx;
        private string dataSource;
        private string fileName;

        // Constructor, guarda el nombre del archivo de la base de datos
        public UserDatabase(string fileName)
        {
            this.fileName = fileName;
            dataSource = "Data Source=" + fileName + ";Version=3;";
            cnx = null;
        }

        // Comprueba si el fichero de la base de datos existe
        public bool ExisteArchivo()
        {
            return File.Exists(fileName);
        }

        // Abre la conexion, si el archivo no existe, SQLite lo crea
        public void Open()
        {
            cnx = new SQLiteConnection(dataSource);
            cnx.Open();
        }

        // Cierra la conexion, si no se llego a abrir, no hace nada
        public void Close()
        {
            if (cnx != null)
            {
                cnx.Close();
            }
        }

        // Crea la tabla de usuarios si todavia no existe
        public void CreateTable()
        {
            string query = "CREATE TABLE IF NOT EXISTS usuarios " +
                           "(username TEXT PRIMARY KEY, password TEXT NOT NULL)";
            SQLiteCommand command = new SQLiteCommand(query, cnx);
            command.ExecuteNonQuery();
        }

        // Devuelve true si el usuario ya existe
        public bool UserExists(string username)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM usuarios WHERE username='" + username + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(query, cnx);
            int rows = adp.Fill(dt);
            return rows > 0;
        }

        // Registra un usuario nuevo. Devuelve true si se ha guardado
        public bool Register(string username, string password)
        {
            if (UserExists(username))
            {
                return false;
            }
            string query = "INSERT INTO usuarios VALUES ('" + username + "','" + password + "')";
            SQLiteCommand command = new SQLiteCommand(query, cnx);
            int rows = command.ExecuteNonQuery();
            return rows == 1;
        }

        // Devuelve true si el usuario y la contrasena son correctos
        public bool Login(string username, string password)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM usuarios WHERE username='" + username +
                           "' AND password='" + password + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(query, cnx);
            int rows = adp.Fill(dt);
            return rows == 1;
        }
    }
}