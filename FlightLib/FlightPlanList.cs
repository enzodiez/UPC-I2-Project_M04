using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] flightsPlans = new FlightPlan[10];
        int index = 0;

//Creo una clase para deshacer el paso y uso un stack.
        private class EstadoVuelo
        {
            public double X;
            public double Y;
            public double Velocidad;
            public bool Completado;
        }

        private Stack<EstadoVuelo[]> historial = new Stack<EstadoVuelo[]>();
        //Método para guardar el estado del vuelo:
        private void GuardarEstado()
        {
            EstadoVuelo[] estado = new EstadoVuelo[index];
            for (int i = 0; i < index; i++)
            {
                FlightPlan avion = flightsPlans[i];

                estado[i] = new EstadoVuelo();
                estado[i].X = avion.GetCurrentPosition().GetX();
                estado[i].Y = avion.GetCurrentPosition().GetY();
                estado[i].Velocidad = avion.GetVelocidad();
                estado[i].Completado = avion.GetFlightCompleted();
            }
            historial.Push(estado);
        }

        // Métodos para deshacer un paso y retirarlo del stack, así puedo deshacer tantos como quiera.
        public bool PuedeDeshacer()
        {
            return historial.Count > 0;
        }

        public bool DeshacerUltimoPaso()
        {
            if (!PuedeDeshacer())
                return false;
            EstadoVuelo[] estado = historial.Pop();
            for (int i = 0; i < estado.Length; i++)
            {
                FlightPlan avion = flightsPlans[i];
                avion.GetCurrentPosition().SetX(estado[i].X);
                avion.GetCurrentPosition().SetY(estado[i].Y);
                avion.SetVelocidad(estado[i].Velocidad);
                avion.SetFlightCompleted(estado[i].Completado);
            }
            return true;
        }
        //Guardar archivo con los FP.

        public void GuardarEnArchivo(string ruta, double securityDistance, double cycleDuration)
        {
            //El using nos permite cerrar el archivo automaticamente.
            using (StreamWriter writer = new StreamWriter(ruta))
            {
                writer.WriteLine("SIMULACION_V2");

                // Parámetros. Lo de culture es por cuestiones de decimales: . o ,. El . se convierte siempre en el separador decimal.
                writer.WriteLine(securityDistance.ToString(CultureInfo.InvariantCulture));
                writer.WriteLine(cycleDuration.ToString(CultureInfo.InvariantCulture));

                // Número de aviones
                writer.WriteLine(index);

                // Información
                for (int i = 0; i < index; i++)
                {
                    FlightPlan avion = flightsPlans[i];
                    //Uri.EscapeDataString nos permite unir caracteres de texto para que no interfieran con el formato escogido. Separación por ";". Si no se usa, cada elemento será una línea del archivo de salida, que es peor visualmente.
                    string linea = string.Join(";",Uri.EscapeDataString(avion.GetAirline()),avion.GetId().ToString(),avion.GetOrigen().GetX().ToString(CultureInfo.InvariantCulture),avion.GetOrigen().GetY().ToString(CultureInfo.InvariantCulture),avion.GetCurrentPosition().GetX().ToString(CultureInfo.InvariantCulture),avion.GetCurrentPosition().GetY().ToString(CultureInfo.InvariantCulture),avion.GetDestino().GetX().ToString(CultureInfo.InvariantCulture),avion.GetDestino().GetY().ToString(CultureInfo.InvariantCulture),avion.GetVelocidad().ToString(CultureInfo.InvariantCulture),avion.GetFlightCompleted().ToString());
                    writer.WriteLine(linea);
                }
            }
        }
        // Cargar archivos:


        public static FlightPlanList CargarDesdeArchivo(string ruta, out double securityDistance, out double cycleDuration)
        {
            using (StreamReader reader = new StreamReader(ruta))
            {
                string formato = reader.ReadLine();
                //Genera error si el formato no es el correcto según el código que le hemos asignado en el método anterior.
                if (formato != "SIMULACION_V2")
                    throw new InvalidDataException("Formato de archivo incorrecto.");

                //.Parse() nos convierte en valor en el formato correspondiente. Así no tenemos que poner el convert correspondiente en cada caso.
                double distancia = double.Parse(reader.ReadLine(),CultureInfo.InvariantCulture);

                double duracion = double.Parse(reader.ReadLine(),CultureInfo.InvariantCulture);

                int numeroAviones = int.Parse(reader.ReadLine());

                if (numeroAviones < 1 || numeroAviones > 10 || distancia <= 0 || duracion <= 0)
                {
                    throw new InvalidDataException("Datos de simulación incorrectos.");
                }

                FlightPlanList nuevaLista = new FlightPlanList();

                for (int i = 0; i < numeroAviones; i++)
                {
                    string linea = reader.ReadLine();

                    if (linea == null)
                        throw new InvalidDataException("Archivo incompleto.");

                    string[] datos = linea.Split(';');

                    if (datos.Length != 10)
                        throw new InvalidDataException("Datos de vuelo incorrectos.");

                    string airline = Uri.UnescapeDataString(datos[0]);
                    int id = int.Parse(datos[1]);
                    double xi = double.Parse(datos[2], CultureInfo.InvariantCulture);
                    double yi = double.Parse(datos[3], CultureInfo.InvariantCulture);
                    double xa = double.Parse(datos[4], CultureInfo.InvariantCulture);
                    double ya = double.Parse(datos[5], CultureInfo.InvariantCulture);
                    double xf = double.Parse(datos[6], CultureInfo.InvariantCulture);
                    double yf = double.Parse(datos[7], CultureInfo.InvariantCulture);
                    double velocidad = double.Parse(datos[8], CultureInfo.InvariantCulture);
                    bool completado = bool.Parse(datos[9]);

                    //Crear el avión con su origen y destino
                    FlightPlan avion = new FlightPlan(
                        airline, id, xi, yi, xf, yf, velocidad);

                    //Restaurar
                    avion.GetCurrentPosition().SetX(xa);
                    avion.GetCurrentPosition().SetY(ya);
                    avion.SetFlightCompleted(completado);
                    nuevaLista.AddFlightPlan(avion);
                }

                securityDistance = distancia;
                cycleDuration = duracion;

                return nuevaLista;
            }
        }



        //Versión 1:
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
            historial.Clear();
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
            GuardarEstado();
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
                        message= $"¡Conflicto detectado entre {flightsPlans[i].GetId()} de {flightsPlans[i].GetAirline()} y {flightsPlans[j].GetId()} de {flightsPlans[j].GetAirline()}!\n";
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
                    FlightPlan avion1 = new FlightPlan(flightsPlans[i].GetAirline(), flightsPlans[i].GetId(),flightsPlans[i].GetCurrentPosition().GetX(),flightsPlans[i].GetCurrentPosition().GetY(),flightsPlans[i].GetDestino().GetX(),flightsPlans[i].GetDestino().GetY(),flightsPlans[i].GetVelocidad());
                    FlightPlan avion2 = new FlightPlan(flightsPlans[j].GetAirline(), flightsPlans[j].GetId(),flightsPlans[j].GetCurrentPosition().GetX(),flightsPlans[j].GetCurrentPosition().GetY(),flightsPlans[j].GetDestino().GetX(),flightsPlans[j].GetDestino().GetY(),flightsPlans[j].GetVelocidad());

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
                        mensaje += $"Conflicto previsto entre {avion1.GetAirline()} {avion1.GetId()} y {avion2.GetAirline()} {avion2.GetId()}\n";
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
                    FlightPlan avion1 = new FlightPlan(flightsPlans[i].GetAirline(), flightsPlans[i].GetId(), flightsPlans[i].GetCurrentPosition().GetX(), flightsPlans[i].GetCurrentPosition().GetY(), flightsPlans[i].GetDestino().GetX(), flightsPlans[i].GetDestino().GetY(), flightsPlans[i].GetVelocidad());
                    FlightPlan avion2 = new FlightPlan(flightsPlans[j].GetAirline(), flightsPlans[j].GetId(), flightsPlans[j].GetCurrentPosition().GetX(), flightsPlans[j].GetCurrentPosition().GetY(), flightsPlans[j].GetDestino().GetX(), flightsPlans[j].GetDestino().GetY(), flightsPlans[j].GetVelocidad());

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
        public void ResetAll()
        {
            historial.Clear();
            for (int i=0;i< index; i++)
            {
                this.GetFlightPlan(i).Restart(); //reiniciamos la posicion actual de todos los vuelos
            }//no necesitamos llamar a flightplans. (usamos this porque estamos dentro de la clase)
        }
    }
}