using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorInterface
{
    public partial class AirspaceInterface : Form
    {
        FlightPlanList listaPlanes;
        double securityDistance;
        double cycleDuration;
        PictureBox[] aircraftsPics = new PictureBox[10];
        public AirspaceInterface(FlightPlanList listaPlanes, double securityDistance, double cycleDuration)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            this.securityDistance = securityDistance;
            this.cycleDuration = cycleDuration;
        }
        private void Airspace_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                aircraftsPics[i] = new PictureBox();
                aircraftsPics[i].Image = Image.FromFile("plane.png");
                aircraftsPics[i].Size = new Size(20, 20);
                aircraftsPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                // Se hace un cast a int para que no haya problemas con la ubicación de los aviones en el panel
                // Esto significa que se trucan los valores de las coordenadas X e Y a enteros.
                aircraftsPics[i].Location = new Point((int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX(), (int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY());
                panel1.Controls.Add(aircraftsPics[i]);
                aircraftsPics[i].BringToFront();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                aircraftsPics[i].Location = new Point((int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX(), (int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY());
            }
        }
    }
}