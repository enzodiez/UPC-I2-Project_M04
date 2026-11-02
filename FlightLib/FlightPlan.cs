using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        string id; // identificador
        Position initialPosition; // posicion final
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        bool flightCompleted; // indica si el vuelo ha llegado a su destino
        double velocidad;

        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.flightCompleted = false;
        }

        public string GetId()
        {
            return id;
        }
        public void SetVelocidad(double velocidad)
        // setter del atributo velocidad
        { this.velocidad = velocidad; }
        public double GetVelocidad()
        {
            return this.velocidad;
        }
        public bool GetFlightCompleted()
        {
            return this.flightCompleted;
        }
        public Position GetCurrentPosition()
        {
            return this.currentPosition;
        }
        public void SetFlightCompleted(bool flightCompleted)
        {
            this.flightCompleted = flightCompleted;
        }
        public void Mover(double tiempo)
        {
            double hipotenusa = currentPosition.Distancia(finalPosition);

            // Si ya ha llegado al destino, no hay que calcular nada más
            if (hipotenusa == 0)
            {
                return;
            }

            double distancia = (tiempo * velocidad) / 60.0;

            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            if (distancia < hipotenusa)
            {
                currentPosition.SetX(x);
                currentPosition.SetY(y);
            }
            else
            {
                // Ajustamos las coordenadas sin romper la separación de objetos
                currentPosition.SetX(finalPosition.GetX());
                currentPosition.SetY(finalPosition.GetY());
            }
        }
        public bool EnDestino()
        {
            // Comprobamos la distancia real de coordenadas, no las referencias de objeto
            return currentPosition.Distancia(finalPosition) == 0;
        }
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            // Si distanciaSeguridad es el radio del círculo de cada avión:
            // - Si Distancia < 2 * distanciaSeguridad -> Se solapan (Conflicto = true)
            // - Si Distancia >= 2 * distanciaSeguridad -> Son tangentes o están separados (Conflicto = false)
            return currentPosition.Distancia(b.currentPosition) < 2 * distanciaSeguridad;
        }
        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:f2}", velocidad);
            Console.WriteLine("Posición actual: ({0:f2},{1:f2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.EnDestino())
                Console.WriteLine("Ha llegado al destino.");
            Console.WriteLine("******************************");
        }
        public double Distance(FlightPlan plan)
        {
            return this.currentPosition.Distancia(plan.currentPosition);
        }
        public void Restart()
        {
            // Creamos una nueva instancia limpia para la posición actual
            this.currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
            this.flightCompleted = false;
        }
        public Position GetDestino()
        {
            return this.finalPosition;
        }
        //Necesitamos también un método para obtener la posición inicial del vuelo
        public Position GetOrigen()
        {
            return this.initialPosition;
        }
    }
}