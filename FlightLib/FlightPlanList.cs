using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightLib
{
    public class FlightPlanList
    {
        List<FlightPlan> flightsPlans = new List<FlightPlan>();
        Stack<List<FlightPlan>> historial = new Stack<List<FlightPlan>>();

        //Método para guardar estado:

        private void GuardarEstado()
        {
            List<FlightPlan> estado = new List<FlightPlan>();
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                FlightPlan avion = flightsPlans[i];
                FlightPlan copia = new FlightPlan(avion.GetId(),avion.GetCompany(),avion.GetOrigen().GetX(),avion.GetOrigen().GetY(),avion.GetDestino().GetX(),avion.GetDestino().GetY(),avion.GetVelocidad());

                copia.GetCurrentPosition().SetX(avion.GetCurrentPosition().GetX());
                copia.GetCurrentPosition().SetY(avion.GetCurrentPosition().GetY());
                copia.SetFlightCompleted(avion.GetFlightCompleted());
                estado.Add(copia);
            }
            historial.Push(estado);
        }

        //Método para deshacer:
        public bool PuedeDeshacer() //Comprobar.
        {
            return historial.Count > 0;
        }

        public bool DeshacerUltimoPaso()
        {
            if (!PuedeDeshacer())
                return false;
            flightsPlans = historial.Pop();
            return true;
        }


        //Guardar en fichero:

        public void GuardarEnArchivo(string ruta, double securityDistance, double cycleDuration)
        {
            StreamWriter writer = new StreamWriter(ruta);
            // Datos generales
            writer.WriteLine("SIMULACION;" + securityDistance + ";" + cycleDuration);

            // Guardamos todos
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                FlightPlan avion = flightsPlans[i];
                writer.WriteLine(avion.GetId() + ";" +avion.GetCompany() + ";" +avion.GetOrigen().GetX() + ";" +avion.GetOrigen().GetY() + ";" +avion.GetDestino().GetX() + ";" +avion.GetDestino().GetY() + ";" +avion.GetVelocidad() + ";" +avion.GetCurrentPosition().GetX() + ";" +avion.GetCurrentPosition().GetY() + ";" +avion.GetFlightCompleted());
            }
            writer.Close();
        }
        //Al cargar simulaciones guardadas, necesitamos un método que sustituya los aviones.

        public void SustituirPlanes(FlightPlanList nuevaLista)
        {
            flightsPlans.Clear();
            for (int i = 0; i < nuevaLista.GetIndex(); i++)
            {
                flightsPlans.Add(nuevaLista.GetFlightPlan(i));
            }
            historial.Clear();
        }


        //Versión 1:
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
            if (!HayVuelosEnCurso())
                return;
            GuardarEstado();
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
                if (flightsPlans[i].EnDestino()) continue;

                for (int j = i + 1; j < flightsPlans.Count; j++)
                {
                    if (flightsPlans[j].EnDestino()) continue;

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
                if (flightsPlans[i].EnDestino()) continue;

                for (int j = i + 1; j < flightsPlans.Count; j++)
                {
                    if (flightsPlans[j].EnDestino()) continue;

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
            // Por si acaso comprobamos que hayan mínimo dos planes de vuelo para evitar errores de índice (nunca debería suceder)
            if (flightsPlans.Count < 2) return false;

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

        // Comprueba si asignar 'nuevaVelocidad' al avión indicado provoca conflictos desde el inicio de la simulación
        public bool HayConflictoConNuevaVelocidad(int indiceAvion, double nuevaVelocidad, double distanciaSeguridad)
        {
            FlightPlan planModificado = flightsPlans[indiceAvion].ClonarEnOrigen(nuevaVelocidad);

            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (i != indiceAvion)
                {
                    FlightPlan otroPlan = flightsPlans[i].ClonarEnOrigen(flightsPlans[i].GetVelocidad());
                    if (planModificado.HayConflictoFuturo(otroPlan, distanciaSeguridad))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Comprueba si ya existe un plan de vuelo con la misma aerolínea e identificador
        public bool ExisteVuelo(string id, string company)
        {
            for (int i = 0; i < flightsPlans.Count; i++)
            {
                if (flightsPlans[i].GetId().ToUpper() == id.Trim().ToUpper() &&
                    flightsPlans[i].GetCompany().ToUpper() == company.Trim().ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
    }
}