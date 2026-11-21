using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorInterface
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Primero mostramos el login
            LoginForm login = new LoginForm();
            Application.Run(login);

            // Solo abrimos el simulador si el login ha sido correcto
            if (login.IsLoginCorrect())
            {
                Application.Run(new MainInterface());
            }
        }
    }
}
