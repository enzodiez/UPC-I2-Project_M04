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
        FlightPlanList ListaPlanes = new FlightPlanList();
        public MainInterface()
        {
            InitializeComponent();
        }

        private void addANewFlightPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFlightPlanInterface form = new NewFlightPlanInterface();
            form.ShowDialog();
            ListaPlanes.AddFlightPlan(form.DamePlan());
            MessageBox.Show("Flight plan has been added.");
        }
    }
}
