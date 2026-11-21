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
        string company; // compañia
        Position initialPosition; // posicion inicial
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        bool flightCompleted; // indica si el vuelo ha llegado a su destino
        double velocidad;

        // Constructor
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = company;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.flightCompleted = false;
        }

        // Getters
        public string GetId()
        {
            return id;
        }
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
        public string GetCompany()
        {
            return this.company;
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

        // Setters
        public void SetVelocidad(double velocidad)
        {
            this.velocidad = velocidad;
        }
        public void SetFlightCompleted(bool flightCompleted)
        {
            this.flightCompleted = flightCompleted;
        }

        // Crea una copia del vuelo situada en su punto de origen con la velocidad indicada
        public FlightPlan ClonarEnOrigen(double nuevaVelocidad)
        {
            return new FlightPlan(id, company, initialPosition.GetX(), initialPosition.GetY(), finalPosition.GetX(), finalPosition.GetY(), nuevaVelocidad);
        }

        // Calcula los minutos que le faltan al vuelo para llegar a su destino desde su posición actual
        public double TiempoRestante()
        {
            return currentPosition.Distancia(finalPosition) * 60.0 / velocidad;
        }

        // Devuelve la posición exacta del avión tras 't' minutos desde su posición actual
        public Position PosicionEnTiempo(double t)
        {
            double distanciaRestante = currentPosition.Distancia(finalPosition);
            double recorrido = velocidad * t / 60.0;

            // Si ya está en destino o en 't' minutos lo supera, se queda fijo en el destino
            if (distanciaRestante == 0 || recorrido >= distanciaRestante)
            {
                return new Position(finalPosition.GetX(), finalPosition.GetY());
            }

            double fraccion = recorrido / distanciaRestante;
            return new Position(
                currentPosition.GetX() + fraccion * (finalPosition.GetX() - currentPosition.GetX()),
                currentPosition.GetY() + fraccion * (finalPosition.GetY() - currentPosition.GetY())
            );
        }

        // Calcula la distancia mínima entre este avión y el avión 'b' dentro del intervalo de tiempo [t0, t1]
        private double DistanciaMinimaEnIntervalo(FlightPlan b, double t0, double t1)
        {
            // Posiciones relativas (vector de A respecto a B) al inicio (t0) y al final (t1) del tramo
            Position a0 = this.PosicionEnTiempo(t0);
            Position b0 = b.PosicionEnTiempo(t0);
            Position a1 = this.PosicionEnTiempo(t1);
            Position b1 = b.PosicionEnTiempo(t1);

            double dx0 = a0.GetX() - b0.GetX();
            double dy0 = a0.GetY() - b0.GetY();
            double dx1 = a1.GetX() - b1.GetX();
            double dy1 = a1.GetY() - b1.GetY();

            // Vector de desplazamiento relativo durante el intervalo
            double ex = dx1 - dx0;
            double ey = dy1 - dy0;
            double longitudCuadrado = ex * ex + ey * ey;

            // Proyección ortogonal para hallar el instante s en [0, 1] de máximo acercamiento
            double s = 0;
            if (longitudCuadrado > 0)
            {
                s = -(dx0 * ex + dy0 * ey) / longitudCuadrado;
                s = Math.Max(0, Math.Min(1, s)); // Limitamos s al intervalo [0, 1]
            }

            double distX = dx0 + s * ex;
            double distY = dy0 + s * ey;
            return Math.Sqrt(distX * distX + distY * distY);
        }

        // Predice si habrá conflicto con el vuelo 'b' en lo que queda de trayectoria (esferas tangentes permitidas)
        public bool HayConflictoFuturo(FlightPlan b, double distanciaSeguridad)
        {
            double tPrimero = Math.Min(this.TiempoRestante(), b.TiempoRestante());
            double tUltimo = Math.Max(this.TiempoRestante(), b.TiempoRestante());

            // Tramo 1 [0, tPrimero]: ambos aviones están en movimiento
            // Tramo 2 [tPrimero, tUltimo]: el primer avión ya aterrizó y el segundo sigue volando
            double distanciaMinima = Math.Min(
                this.DistanciaMinimaEnIntervalo(b, 0, tPrimero),
                this.DistanciaMinimaEnIntervalo(b, tPrimero, tUltimo)
            );

            return distanciaMinima < 2 * distanciaSeguridad;
        }

        // Avanza el avión durante el tiempo indicado
        public void Mover(double tiempo)
        {
            if (EnDestino())
            {
                this.flightCompleted = true;
                return;
            }

            Position siguiente = PosicionEnTiempo(tiempo);
            currentPosition.SetX(siguiente.GetX());
            currentPosition.SetY(siguiente.GetY());

            if (EnDestino())
            {
                this.flightCompleted = true;
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
    }
}