using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLib;

namespace SimulatorConsole
{
    public class Simulator
    {
        // Se define como constante para que no pueda ser modificada accidentalmente
        private const double DistanciaSeguridad = 10.0;
        static void Main(string[] args)
        {
            try
            {
                FlightPlanList plansList = new FlightPlanList();

                // Creamos dos planes de vuelo
                Console.WriteLine("**********Plan de vuelo A**********");
                FlightPlan plan_a = CreateNewPlan();

                Console.WriteLine("\n**********Plan de vuelo B**********");
                FlightPlan plan_b = CreateNewPlan();

                plansList.AddFlightPlan(plan_a);
                plansList.AddFlightPlan(plan_b);

                for (int i = 0; i < 1000 && plansList.HayVuelosEnCurso(); i++)
                {
                    plansList.Mover(10);
                    plansList.EscribeConsola();
                    plansList.ComprobarConflictos(DistanciaSeguridad);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static FlightPlan CreateNewPlan()
        {
            // Este método está aquí hasta que creemos una clase para la interfaz en la que se implemente esta función.
            try
            {
                Console.WriteLine("Escribe el identificador");
                string identificador = Console.ReadLine(); ;

                Console.WriteLine("Escribe la velocidad");
                double velocidad = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                string[] trozos = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double ix = Convert.ToDouble(trozos[0]);
                double iy = Convert.ToDouble(trozos[1]);

                Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                trozos = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double fx = Convert.ToDouble(trozos[0]);
                double fy = Convert.ToDouble(trozos[1]);

                return new FlightPlan(identificador, ix, iy, fx, fy, velocidad);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}