using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;
using System.IO;
using System.Globalization;

namespace SimulatorInterface
{
    public partial class NewFlightPlanInterface : Form
    {
        FlightPlan flightPlan;
        public FlightPlanList listacargadosfile = new FlightPlanList(); //la hacemos publica para usarla luego!
        int contadortotal = 0;
        int contadorfile = 0;

        public NewFlightPlanInterface()
        {
            InitializeComponent();
        }
        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(IX.Text) < 0 || Convert.ToDouble(IY.Text) < 0 || Convert.ToDouble(FX.Text) < 0 || Convert.ToDouble(FY.Text) < 0 || Convert.ToDouble(Speed.Text) <= 0)
                {
                    MessageBox.Show("The values cannot be negative.");
                }
                else if (CallSign.Text == "")
                {
                    MessageBox.Show("The call sign cannot be empty.");
                }
                else
                {
                    flightPlan = new FlightPlan(CallSign.Text, airline.Text, Convert.ToDouble(IX.Text), Convert.ToDouble(IY.Text), Convert.ToDouble(FX.Text), Convert.ToDouble(FY.Text), Convert.ToDouble(Speed.Text));
                    this.Close();
                    contadortotal += 1;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Format error.");
            }
        }
        public FlightPlan DamePlan()
        {
            return flightPlan;
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
                progressBarLoad.Visible = true;
                progressBarLoad.Maximum = lineas.Length;
                progressBarLoad.Minimum = 0;
                progressBarLoad.Value = 0;
                contadorfile = 0;

                try
                {
                    foreach (string linea in lineas)
                    {
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');
                            if (datos.Length < 7) throw new FormatException();

                            string id = datos[0].Trim();
                            string company = string.IsNullOrWhiteSpace(datos[1]) ? "unknown" : datos[1].Trim();
                            double Xi = double.Parse(datos[2], CultureInfo.InvariantCulture);
                            double Yi = double.Parse(datos[3], CultureInfo.InvariantCulture);
                            double Xf = double.Parse(datos[4], CultureInfo.InvariantCulture);
                            double Yf = double.Parse(datos[5], CultureInfo.InvariantCulture);
                            double vel = double.Parse(datos[6], CultureInfo.InvariantCulture);

                            if (Xi < 0 || Yi < 0 || Xf < 0 || Yf < 0 || vel <= 0 || id == "")
                            {
                                throw new FormatException();
                            }

                            listacargadosfile.AddFlightPlan(new FlightPlan(id, company, Xi, Yi, Xf, Yf, vel));
                            contadorfile++;
                            contadortotal++;
                        }
                        progressBarLoad.Value++;
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(250);
                    }
                    MessageBox.Show($"You successfully loaded {contadorfile} flights from the file you selected! You now have: {contadortotal} flights in total");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Error de formato o valores inválidos en el fichero.");
                }
                finally
                {
                    progressBarLoad.Visible = false;
                }
            }
        }
    }
}
