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
        FlightPlan[] flightsPlans = new FlightPlan[10];
        int index = 0;

        public int GetIndex()
        {
            return index;
        }
        public int AddFlightPlan(FlightPlan p)
        {
            if (index == 10)
            {
                return -1;
            }

            flightsPlans[index] = p;
            index++;
            return 0;
        }
        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= index)
            {
                return null;
            }

            return flightsPlans[i];
        }
        public bool HayVuelosEnCurso()
        {
            for (int i = 0; i < index; i++)
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
            for (int i = 0; i < index; i++)
            {
                if (!flightsPlans[i].GetFlightCompleted())
                {
                    flightsPlans[i].Mover(tiempo);
                }
            }
        }
        public void EscribeConsola()
        {
            for (int i = 0; i < index; i++)
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
            for (int i = 0; i < index; i++)
            {
                if (flightsPlans[i].EnDestino())
                {
                    continue;
                }

                for (int j = i + 1; j < index; j++)
                {
                    if (flightsPlans[j].EnDestino())
                    {
                        continue;
                    }
                    else if (flightsPlans[i].Conflicto(flightsPlans[j], distanciaSeguridad))
                    {
                        message= $"¡Conflicto detectado entre {flightsPlans[i].GetId()} y {flightsPlans[j].GetId()}!\n";
                    }
                }
            }
            return message;
        }

        public string PredecirConflictos(double distanciaSeguridad, double tiempo)
        {
            string mensaje = "";

            for (int i = 0; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    // Creamos copias de los dos aviones.
                    FlightPlan avion1 = new FlightPlan(flightsPlans[i].GetId(),flightsPlans[i].GetCurrentPosition().GetX(),flightsPlans[i].GetCurrentPosition().GetY(),flightsPlans[i].GetDestino().GetX(),flightsPlans[i].GetDestino().GetY(),flightsPlans[i].GetVelocidad());
                    FlightPlan avion2 = new FlightPlan(flightsPlans[j].GetId(),flightsPlans[j].GetCurrentPosition().GetX(),flightsPlans[j].GetCurrentPosition().GetY(),flightsPlans[j].GetDestino().GetX(),flightsPlans[j].GetDestino().GetY(),flightsPlans[j].GetVelocidad());

                    bool conflicto = false;

                    // Simulamos el movimiento de los aviones
                    for (int k = 0; k < 10000; k++)
                    {
                        if (avion1.Conflicto(avion2, distanciaSeguridad))
                        {
                            conflicto = true;
                            break;
                        }

                        // Comprobamos si ambos aviones han llegado al destino
                        if (avion1.GetCurrentPosition().Distancia(avion1.GetDestino()) == 0 && avion2.GetCurrentPosition().Distancia(avion2.GetDestino()) == 0)
                        {
                            break;
                        }

                        avion1.Mover(tiempo);
                        avion2.Mover(tiempo);
                    }

                    if (conflicto)
                    {
                        mensaje += $"Conflicto previsto entre {avion1.GetId()} y {avion2.GetId()}\n";
                    }
                }
            }

            if (mensaje == "")
            {
                mensaje = "No se han detectado conflictos futuros.";
            }

            return mensaje;
        }
        //Necesitamos un detector de conflictos pero que en vez de mensajes, devuelva un booleano
        public bool CompruebaConflicto(double distanciaSeguridad, double tiempo)
        {
            bool conflicto = false;
            for (int i = 0; i < index; i++) //Aquí no podría ser index-1 porque queremos que el último avión también se compare con los demás
            {
                for (int j = i + 1; j < index; j++)
                {
                    // Creamos copias de los dos aviones.
                    FlightPlan avion1 = new FlightPlan(flightsPlans[i].GetId(), flightsPlans[i].GetCurrentPosition().GetX(), flightsPlans[i].GetCurrentPosition().GetY(), flightsPlans[i].GetDestino().GetX(), flightsPlans[i].GetDestino().GetY(), flightsPlans[i].GetVelocidad());
                    FlightPlan avion2 = new FlightPlan(flightsPlans[j].GetId(), flightsPlans[j].GetCurrentPosition().GetX(), flightsPlans[j].GetCurrentPosition().GetY(), flightsPlans[j].GetDestino().GetX(), flightsPlans[j].GetDestino().GetY(), flightsPlans[j].GetVelocidad());

                    // Simulamos el movimiento de los aviones
                    for (int k = 0; k < 10000; k++)
                    {
                        if (avion1.Conflicto(avion2, distanciaSeguridad))
                        {
                            conflicto = true;
                            break;
                        }

                        // Comprobamos si ambos aviones han llegado al destino
                        if (avion1.GetCurrentPosition().Distancia(avion1.GetDestino()) == 0 && avion2.GetCurrentPosition().Distancia(avion2.GetDestino()) == 0)
                        {
                            break;
                        }

                        avion1.Mover(tiempo);
                        avion2.Mover(tiempo);
                    }

                }
            }
            return conflicto;
        }
        //Cambia la velocidad del segundo vuelo hasta que ya no haya conflicto
        public bool ResolverConflicto(double distanciaSeguridad, double tiempo)
        {
            FlightPlan avion = flightsPlans[1];
            double velocidadOriginal = avion.GetVelocidad();
            double velocidad = velocidadOriginal;

            bool conflicto = CompruebaConflicto(distanciaSeguridad, tiempo);

            //Reducimos un 5% de la velocidad original cada vez, sin bajar del 10%
            while (conflicto && velocidad > velocidadOriginal * 0.1)
            {
                velocidad = velocidad - velocidadOriginal * 0.05;
                avion.SetVelocidad(velocidad);
                conflicto = CompruebaConflicto(distanciaSeguridad, tiempo);
            }

            if (conflicto)
            {
                //No se ha encontrado solución: dejamos la velocidad como estaba
                avion.SetVelocidad(velocidadOriginal);
            }
            return !conflicto;
        }
        public int GetNumero()
        {
            return this.index;
        }
    }
}