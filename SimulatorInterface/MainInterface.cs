using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FlightLib;

namespace SimulatorInterface
{
    public partial class MainInterface : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        double securityDistance;
        double cycleDuration;
        public MainInterface()
        {
            InitializeComponent();
        }
        private void addANewFlightPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFlightPlanInterface form = new NewFlightPlanInterface();
            form.ShowDialog();
            FlightPlan plan = form.DamePlan();
            if (plan != null)
            {
                listaPlanes.AddFlightPlan(plan);
                MessageBox.Show("Flight plan has been added.");
            }
        }
        private void addSecurityDistanceAndCycleDurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecurityCiclesInterface form = new SecurityCiclesInterface();
            form.ShowDialog();
            securityDistance = form.DameSecurityDistance();
            cycleDuration = form.DameCycleDuration();
            if (securityDistance != 0 && cycleDuration != 0)
            {
                MessageBox.Show("Security distance and cycle duration have been set.");
            }
        }
        private void startSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listaPlanes.GetIndex() == 0)
            {
                MessageBox.Show("No flight plans have been added.");
            }
            else if (securityDistance == 0 || cycleDuration == 0)
            {
                MessageBox.Show("Security distance and cycle duration have not been set.");
            }
            else
            {
                if (listaPlanes.CompruebaConflicto(securityDistance, cycleDuration))
                {
                    DialogResult respuesta = MessageBox.Show("Se ha detectado un conflicto, ¿Desea resolverlo?", "Conflicto Detectado", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        if (listaPlanes.ResolverConflicto(securityDistance, cycleDuration))
                        {
                            MessageBox.Show("Conflicto resuelto. La nueva velocidad de " + listaPlanes.GetFlightPlan(1).GetId() + " es " + listaPlanes.GetFlightPlan(1).GetVelocidad());
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido resolver el conflicto cambiando la velocidad.");
                        }
                    }
                }
                AirspaceInterface airspace = new AirspaceInterface(listaPlanes, securityDistance, cycleDuration);
                airspace.ShowDialog();
            }
        }
        private void MainInterface_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}