using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        List<FlightPlan> flightsPlans = new List<FlightPlan>();
        public int GetIndex()
        {
            return flightsPlans.Count;
        }
        public void AddFlightPlan(FlightPlan p)
        {
            flightsPlans.Add(p);
        }
        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= flightsPlans.Count)
            {
                return null;
            }

            return flightsPlans[i];
        }
        public bool HayVuelosEnCurso()
        {
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (!flightsPlans[i].GetFlightCompleted())
                {
                    return true;
                }
            }
            return false;
        }
        public void Mover(double tiempo)
        {
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (!flightsPlans[i].GetFlightCompleted())
                {
                    flightsPlans[i].Mover(tiempo);
                    if (flightsPlans[i].EnDestino())
                    {
                        flightsPlans[i].SetFlightCompleted(true);
                    }
                }
            }
        }
        public void EscribeConsola()
        {
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                // Procesa solo los vuelos que no se han archivado previamente
                if (!flightsPlans[i].GetFlightCompleted())
                {
                    flightsPlans[i].EscribeConsola();

                    // Tras imprimir los datos del turno actual, si ya está en destino, se marca completado
                    if (flightsPlans[i].EnDestino())
                    {
                        flightsPlans[i].SetFlightCompleted(true);
                    }
                }
            }
        }
        public string ComprobarConflictos(double distanciaSeguridad)
        {
            string message = "";
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (flightsPlans[i].EnDestino())
                {
                    continue;
                }

                for (int j = i + 1; j < flightsPlans.Count; j++)
                {
                    if (flightsPlans[j].EnDestino())
                    {
                        continue;
                    }
                    else if (flightsPlans[i].Conflicto(flightsPlans[j], distanciaSeguridad))
                    {
                        message += $"¡Conflicto detectado entre {flightsPlans[i].GetCompany()} {flightsPlans[i].GetId()} y {flightsPlans[j].GetCompany()} {flightsPlans[j].GetId()}!\n";
                    }
                }
            }
            return message;
        }

        public string PredecirConflictos(double distanciaSeguridad)
        {
            string mensaje = "";

            for (int i = 0; i < flightsPlans.Count; i++)
            {
                for (int j = i + 1; j < flightsPlans.Count; j++)
                {
                    if (flightsPlans[i].HayConflictoFuturo(flightsPlans[j], distanciaSeguridad))
                    {
                        mensaje += $"Conflicto previsto entre {flightsPlans[i].GetCompany()} {flightsPlans[i].GetId()} y {flightsPlans[j].GetCompany()} {flightsPlans[j].GetId()}\n";
                    }
                }
            }

            if (mensaje == "")
            {
                mensaje = "No se han detectado conflictos futuros.";
            }

            return mensaje;
        }

        public bool CompruebaConflicto(double distanciaSeguridad)
        {
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                for (int j = i + 1; j < flightsPlans.Count; j++)
                {
                    if (flightsPlans[i].HayConflictoFuturo(flightsPlans[j], distanciaSeguridad))
                    {
                        return true; // Salida inmediata al encontrar el primer conflicto
                    }
                }
            }
            return false;
        }

        //Cambia la velocidad del segundo vuelo hasta que ya no haya conflicto
        public bool ResolverConflicto(double distanciaSeguridad)
        {
            FlightPlan avion = flightsPlans[1];
            double velocidadOriginal = avion.GetVelocidad();
            double velocidad = velocidadOriginal;

            bool conflicto = CompruebaConflicto(distanciaSeguridad);

            //Reducimos un 5% de la velocidad original cada vez, sin bajar del 10%
            while (conflicto && velocidad > velocidadOriginal * 0.1)
            {
                velocidad = velocidad - velocidadOriginal * 0.05;
                avion.SetVelocidad(velocidad);
                conflicto = CompruebaConflicto(distanciaSeguridad);
            }

            if (conflicto)
            {
                //No se ha encontrado solución: dejamos la velocidad como estaba
                avion.SetVelocidad(velocidadOriginal);
            }
            return !conflicto;
        }
        public void ResetAll()
        {
            for(int i=0;i< flightsPlans.Count; i++)
            {
                this.GetFlightPlan(i).Restart(); //reiniciamos la posicion actual de todos los vuelos
            }//no necesitamos llamar a flightplans. (usamos this porque estamos dentro de la clase)
        }
        public bool HayConflictoConNuevaVelocidad(int indiceAvion, double nuevaVelocidad, double distanciaSeguridad)
        {
            FlightPlan original = flightsPlans[indiceAvion];
            FlightPlan planSimulado = new FlightPlan(
                original.GetId(),
                original.GetCompany(),
                original.GetOrigen().GetX(),
                original.GetOrigen().GetY(),
                original.GetDestino().GetX(),
                original.GetDestino().GetY(),
                nuevaVelocidad
            );

            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (i != indiceAvion)
                {
                    if (planSimulado.HayConflictoTrayectoria(flightsPlans[i], distanciaSeguridad))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}