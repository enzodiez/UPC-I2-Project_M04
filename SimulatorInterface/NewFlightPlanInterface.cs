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

namespace SimulatorInterface
{
    public partial class NewFlightPlanInterface : Form
    {
        FlightPlan flightPlan;
        public NewFlightPlanInterface()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void IX_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewFlightPlanInterface_Load(object sender, EventArgs e)
        {

        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(IX.Text) < 0 || Convert.ToDouble(IY.Text) < 0 || Convert.ToDouble(FX.Text) < 0 || Convert.ToDouble(FY.Text) < 0 || Convert.ToDouble(Speed.Text) < 0)
                {
                    MessageBox.Show("The values cannot be negative.");
                }
                else if (CallSign.Text == "")
                {
                    MessageBox.Show("The call sign cannot be empty.");
                }
                else
                {
                    flightPlan = new FlightPlan(CallSign.Text, Convert.ToDouble(IX.Text), Convert.ToDouble(IY.Text), Convert.ToDouble(FX.Text), Convert.ToDouble(FY.Text), Convert.ToDouble(Speed.Text));
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Erro de formato.");
            }
        }
        public FlightPlan DamePlan()
        {
            return flightPlan;
        }
    }
}
