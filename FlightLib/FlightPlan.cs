using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Atributos

        string id; // identificador
        Position initialPosition;
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        bool flightCompleted; // indica si el vuelo ha llegado a su destino
        double velocidad;

        // Constructores
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.flightCompleted = false;
        }

        // Metodos

        public string GetId()
        {
            return id;
        }
        public void SetVelocidad(double velocidad)
        // setter del atributo velocidad
        { this.velocidad = velocidad; }
        public bool GetFlightCompleted()
        {
            return this.flightCompleted;
        }
        public void SetFlightCompleted(bool flightCompleted)
        {
            this.flightCompleted = flightCompleted;
        }

        public void Mover(double tiempo)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = currentPosition.Distancia(finalPosition);

            if (hipotenusa == 0)
            {
                currentPosition = finalPosition;
                return;
            }

            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        public bool EnDestino()
        {
            return currentPosition == finalPosition;
        }

        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            return currentPosition.Distancia(b.currentPosition) < distanciaSeguridad;
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

        public void Restart()
        {
            currentPosition.SetX(initialPosition.GetX());
            currentPosition.SetY(initialPosition.GetY());
        }
    }
}