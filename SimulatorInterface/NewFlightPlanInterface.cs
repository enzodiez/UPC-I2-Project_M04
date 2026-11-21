using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorInterface
{
    public partial class NewFlightPlanInterface : Form
    {
        int contadorfile = 0;
        FlightPlanList listaPlanes;
        bool simulacionCargada = false;
        double distanciaCargada;
        double cicloCargado;

        public bool EsSimulacionCargada()
        {
            return simulacionCargada;
        }
        public double DameDistanciaCargada()
        {
            return distanciaCargada;
        }
        public double DameCicloCargado()
        {
            return cicloCargado;
        }
        public NewFlightPlanInterface(FlightPlanList listaPlanes)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
        }
        //Método para cargar simulaciones:
        private void CargarSimulacion(string[] lineas)
        {
            try
            {
                // Leer los datos generales
                string[] config = lineas[0].Split(';');

                if (config.Length != 3 || config[0] != "SIMULACION")
                    throw new FormatException("Formato incorrecto.");

                double distancia = Convert.ToDouble(config[1]);
                double duracion = Convert.ToDouble(config[2]);

                if (distancia <= 0 || duracion <= 0)
                    throw new FormatException("Parámetros incorrectos.");

                FlightPlanList nuevos = new FlightPlanList();

                // Leer los aviones
                for (int i=1; i<lineas.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lineas[i]))
                        continue;

                    string[] datos = lineas[i].Split(';');

                    if (datos.Length != 10)
                        throw new FormatException("Datos de vuelo incorrectos.");

                    string id = datos[0].Trim();
                    string company = datos[1].Trim();

                    double xi = Convert.ToDouble(datos[2]);
                    double yi = Convert.ToDouble(datos[3]);
                    double xf = Convert.ToDouble(datos[4]);
                    double yf = Convert.ToDouble(datos[5]);
                    double velocidad = Convert.ToDouble(datos[6]);

                    double xa = Convert.ToDouble(datos[7]);
                    double ya = Convert.ToDouble(datos[8]);

                    bool completado = Convert.ToBoolean(datos[9]);

                    if (id == "" || company == "" || velocidad <= 0 ||
                        nuevos.ExisteVuelo(id, company))
                        throw new FormatException("Datos de vuelo no válidos.");

                    FlightPlan avion = new FlightPlan(id, company, xi, yi, xf, yf, velocidad);
                    avion.GetCurrentPosition().SetX(xa);
                    avion.GetCurrentPosition().SetY(ya);
                    avion.SetFlightCompleted(completado);
                    nuevos.AddFlightPlan(avion);
                }

                if (nuevos.GetIndex() == 0)
                    throw new FormatException("No hay vuelos guardados.");

                // Sustituir la simulación anterior
                listaPlanes.SustituirPlanes(nuevos);

                distanciaCargada = distancia;
                cicloCargado = duracion;
                simulacionCargada = true;

                MessageBox.Show("Simulation loaded successfully.");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading simulation.");
            }
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(IX.Text) < 0 || Convert.ToDouble(IY.Text) < 0 || Convert.ToDouble(FX.Text) < 0 || Convert.ToDouble(FY.Text) < 0 || Convert.ToDouble(Speed.Text) <= 0)
                {
                    MessageBox.Show("The values cannot be negative (and speed must be greater than 0).");
                }
                else if (CallSign.Text.Trim() == "" || airline.Text.Trim() == "")
                {
                    MessageBox.Show("The call sign nor the airline cannot be empty.");
                }
                else if (listaPlanes.ExisteVuelo(CallSign.Text, airline.Text))
                {
                    MessageBox.Show("This flight plan already exists.");
                }
                else
                {
                    FlightPlan flightPlan = new FlightPlan(CallSign.Text.Trim(), airline.Text.Trim(), Convert.ToDouble(IX.Text), Convert.ToDouble(IY.Text), Convert.ToDouble(FX.Text), Convert.ToDouble(FY.Text), Convert.ToDouble(Speed.Text));
                    listaPlanes.AddFlightPlan(flightPlan);
                    MessageBox.Show($"You successfully loaded this flight plan! You now have: {listaPlanes.GetIndex()} flights in total");

                    // Limpiamos los campos para poder introducir otro vuelo cómodamente
                    CallSign.Clear();
                    airline.Clear();
                    IX.Clear();
                    IY.Clear();
                    FX.Clear();
                    FY.Clear();
                    Speed.Clear();
                    altitude.Clear();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Format error.");
            }
        }

        private void fileselectorBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Select a flight plan file";
            openfile.Filter = "Text files (*.txt)|*.txt";

            if (openfile.ShowDialog() == DialogResult.OK)
            {
                routeBox.Text = openfile.FileName;
                string[] lineas = File.ReadAllLines(openfile.FileName);
                if (lineas.Length > 0 && lineas[0].StartsWith("SIMULACION"))
                {
                    CargarSimulacion(lineas);
                    return;
                }
                progressBarLoad.Visible = true;
                progressBarLoad.Maximum = lineas.Length;
                progressBarLoad.Minimum = 0;
                progressBarLoad.Value = 0;
                contadorfile = 0;
                int errores = 0;

                foreach (string linea in lineas)
                {
                    progressBarLoad.Value++;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(250);

                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        try
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length != 7) continue;

                            string id = datos[0].Trim();
                            string company = datos[1].Trim();
                            double Xi = Convert.ToDouble(datos[2].Trim());
                            double Yi = Convert.ToDouble(datos[3].Trim());
                            double Xf = Convert.ToDouble(datos[4].Trim());
                            double Yf = Convert.ToDouble(datos[5].Trim());
                            double vel = Convert.ToDouble(datos[6].Trim());

                            if (Xi < 0 || Yi < 0 || Xf < 0 || Yf < 0 || vel <= 0 || id == "" || company == "" || listaPlanes.ExisteVuelo(id, company))
                            {
                                errores++;
                            }

                            listaPlanes.AddFlightPlan(new FlightPlan(id, company, Xi, Yi, Xf, Yf, vel));
                            contadorfile++;
                        }
                        catch (FormatException)
                        {
                            // Si una línea tiene texto inválido en un número, la saltamos y seguimos con la siguiente
                            errores++;
                        }
                    }
                }
                progressBarLoad.Visible = false;
                if (errores > 0)
                {
                    MessageBox.Show($"Se han omitido {errores} líneas por tener errores de formato, datos inválidos o vuelos duplicados.");
                }
                MessageBox.Show($"You successfully loaded {contadorfile} flights from the file you selected! You now have: {listaPlanes.GetIndex()} flights in total");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}