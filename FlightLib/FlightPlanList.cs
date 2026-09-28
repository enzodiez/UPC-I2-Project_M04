using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ComprobarConflictos(double distanciaSeguridad)
        {
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
                        Console.WriteLine($"¡Conflicto detectado entre {flightsPlans[i].GetId()} y {flightsPlans[j].GetId()}!");
                    }
                }
            }
        }
    }
}