using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace SimulatorInterface
{
    public partial class LoginForm : Form
    {
        private UserDatabase db;
        private bool loginCorrect;   // true si el usuario ha entrado bien
        public LoginForm()
        {
            InitializeComponent();
            db = new UserDatabase("usuarios.db");
            loginCorrect = false;

            // Creamos la tabla de usuarios si todavia no existe
            try
            {
                if (!db.ExisteArchivo())
                {
                    MessageBox.Show("Aviso: No se encontró el archivo de base de datos 'usuarios.db'. Se creará uno nuevo.");
                }
                db.Open();
                db.CreateTable();
                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                MessageBox.Show("No se pudo abrir la base de datos: " + ex.Message);
            }
        }
        // Para que Program.cs sepa si el login ha sido correcto
        public bool IsLoginCorrect()
        {
            return loginCorrect;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string user = textBoxUser.Text.Trim();
            string pass = textBoxPassword.Text;

            // Comprobamos que no haya campos vacios
            if (user == "" || pass == "")
            {
                MessageBox.Show("Escribe usuario y contraseña.");
                return;
            }

            // Rechazamos comillas simples y dobles para no romper la consulta SQL
            if (user.Contains("'") || pass.Contains("'") || user.Contains('"') || pass.Contains('"'))
            {
                MessageBox.Show("No se permiten comillas simples (') ni dobles (\").");
                return;
            }

            try
            {
                if (!db.ExisteArchivo())
                {
                    MessageBox.Show("Error: No se encuentra el archivo de la base de datos 'usuarios.db'.");
                    return;
                }
                db.Open();
                bool correct = db.Login(user, pass);
                db.Close();

                if (correct)
                {
                    loginCorrect = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                    textBoxPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                db.Close();
                MessageBox.Show("Error con la base de datos: " + ex.Message);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
        }
    }
}
