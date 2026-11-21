using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorInterface
{
    public partial class RegisterForm : Form
    {
        private UserDatabase db;
        public RegisterForm()
        {
            InitializeComponent();
            db = new UserDatabase("usuarios.db");
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string user = textBoxUser.Text.Trim();
            string pass = textBoxPassword.Text;
            string pass2 = textBoxPassword2.Text;

            // Comprobamos que no haya campos vacios
            if (user == "" || pass == "" || pass2 == "")
            {
                MessageBox.Show("Rellena todos los campos.");
                return;
            }

            // Rechazamos comillas simples para no romper la consulta SQL
            if (user.Contains("'") || pass.Contains("'"))
            {
                MessageBox.Show("No se permiten comillas simples (').");
                return;
            }

            // Las dos contrasenas deben coincidir
            if (pass != pass2)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                db.Open();
                bool saved = db.Register(user, pass);
                db.Close();

                if (saved)
                {
                    MessageBox.Show("Usuario registrado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ese nombre de usuario ya existe.");
                }
            }
            catch (Exception ex)
            {
                db.Close();
                MessageBox.Show("Error con la base de datos: " + ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
