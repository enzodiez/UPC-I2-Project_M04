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
        public FlightPlan(string id,double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = "unknown";
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.flightCompleted = false;
        }

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
        public string GetCompany()
        {
            return this.company;
        }
        public void SetFlightCompleted(bool flightCompleted)
        {
            this.flightCompleted = flightCompleted;
        }
        public void Mover(double tiempo)
        {
            if (EnDestino())
            {
                this.flightCompleted = true;
                return;
            }

            Position siguiente = PosicionDesdeEnTiempo(currentPosition, tiempo);
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
        public Position GetDestino()
        {
            return this.finalPosition;
        }
        //Necesitamos también un método para obtener la posición inicial del vuelo
        public Position GetOrigen()
        {
            return this.initialPosition;
        }

        // Tiempo en minutos desde un punto de partida cualquiera hasta el destino
        private double TiempoDesde(Position inicio)
        {
            return inicio.Distancia(finalPosition) * 60.0 / velocidad;
        }

        // Posición exacta tras t minutos partiendo desde 'inicio'
        private Position PosicionDesdeEnTiempo(Position inicio, double t)
        {
            double longitud = inicio.Distancia(finalPosition);
            double recorrido = velocidad * t / 60.0;

            if (longitud == 0 || recorrido >= longitud)
            {
                return new Position(finalPosition.GetX(), finalPosition.GetY());
            }

            double fraccion = recorrido / longitud;
            return new Position(
                inicio.GetX() + fraccion * (finalPosition.GetX() - inicio.GetX()),
                inicio.GetY() + fraccion * (finalPosition.GetY() - inicio.GetY())
            );
        }

        // Distancia mínima entre este vuelo (desde inicioA) y el vuelo b (desde inicioB) en [t0, t1]
        private double DistanciaMinima(Position inicioA, FlightPlan b, Position inicioB, double t0, double t1)
        {
            Position a0 = this.PosicionDesdeEnTiempo(inicioA, t0);
            Position b0 = b.PosicionDesdeEnTiempo(inicioB, t0);
            Position a1 = this.PosicionDesdeEnTiempo(inicioA, t1);
            Position b1 = b.PosicionDesdeEnTiempo(inicioB, t1);

            double dx0 = a0.GetX() - b0.GetX();
            double dy0 = a0.GetY() - b0.GetY();
            double dx1 = a1.GetX() - b1.GetX();
            double dy1 = a1.GetY() - b1.GetY();

            double ex = dx1 - dx0;
            double ey = dy1 - dy0;
            double e2 = ex * ex + ey * ey;

            double s = 0;
            if (e2 > 0)
            {
                s = -(dx0 * ex + dy0 * ey) / e2;
                s = Math.Max(0, Math.Min(1, s));
            }

            double mx = dx0 + s * ex;
            double my = dy0 + s * ey;
            return Math.Sqrt(mx * mx + my * my);
        }

        private bool HayConflictoEntreTrayectorias(Position inicioA, FlightPlan b, Position inicioB, double distanciaSeguridad)
        {
            double tA = this.TiempoDesde(inicioA);
            double tB = b.TiempoDesde(inicioB);

            double tPrimero = Math.Min(tA, tB);
            double tUltimo = Math.Max(tA, tB);

            double minima = Math.Min(
                this.DistanciaMinima(inicioA, b, inicioB, 0, tPrimero),
                this.DistanciaMinima(inicioA, b, inicioB, tPrimero, tUltimo)
            );

            return minima < 2 * distanciaSeguridad;
        }

        // Comprueba todo el plan de vuelo desde el ORIGEN (usado al cambiar velocidades)
        public bool HayConflictoTrayectoria(FlightPlan b, double distanciaSeguridad)
        {
            return HayConflictoEntreTrayectorias(this.initialPosition, b, b.initialPosition, distanciaSeguridad);
        }

        // Comprueba el trayecto restante desde la POSICIÓN ACTUAL (usado en predicción en curso)
        public bool HayConflictoFuturo(FlightPlan b, double distanciaSeguridad)
        {
            return HayConflictoEntreTrayectorias(this.currentPosition, b, b.currentPosition, distanciaSeguridad);
        }
    }
}