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

namespace SimulatorInterface
{
    public partial class NewFlightPlanInterface : Form
    {
        FlightPlan flightPlan;
        public FlightPlanList listacargadosfile= new FlightPlanList(); //la hacemos publica para usarla luego!
        int contadornormal = 0;
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
                    contadornormal += 1;
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

                foreach(string linea in lineas)
                {
                    if (linea != "")
                    {
                        string[] datos = linea.Split(',');
                        string id = datos[0];
                        string company = datos[1];
                        double Xi = Convert.ToDouble(datos[2]);
                        double Yi = Convert.ToDouble(datos[3]);
                        double Xf = Convert.ToDouble(datos[4]);
                        double Yf = Convert.ToDouble(datos[5]);
                        double vel = Convert.ToDouble(datos[6]); //Creamos un constructor nuevo para no tener que modificar todo!
                        listacargadosfile.AddFlightPlan(new FlightPlan(id, company, Xi, Yi, Xf, Yf, vel));
                        contadorfile++;
                        contadortotal++;
                    }
                }
                progressBarLoad.Value++;
                Application.DoEvents(); //Para hacer que la barra se dibuje con el tiempo.
                System.Threading.Thread.Sleep(250);//mini pausa de 500 ms para cada avion (queda más molón)

                progressBarLoad.Visible = false;
                MessageBox.Show($"You successfully loaded {contadorfile} flights from the file you selected! You now have: {contadortotal} flights in total");
            }
        }
    }
}
