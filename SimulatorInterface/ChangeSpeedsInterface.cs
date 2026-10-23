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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SimulatorInterface
{
    public partial class ChangeSpeedsInterface : Form
    {
        FlightPlanList listaPlanes;
        double securityDistance;
        double cycleDuration;
        int currentRow = -1;
        public ChangeSpeedsInterface(FlightPlanList listaPlanes, double securityDistance, double cycleDuration)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            this.securityDistance = securityDistance;
            this.cycleDuration = cycleDuration;
        }

        private void ChangeSpeedsInterface_Load(object sender, EventArgs e)
        {
            SpeedsGrid.ColumnCount = 2;
            SpeedsGrid.RowCount = this.listaPlanes.GetIndex();
            SpeedsGrid.Columns[0].HeaderText = "Flight Plan ID";
            SpeedsGrid.Columns[1].HeaderText = "Flight Plan Speed";
            SpeedsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                SpeedsGrid.Rows[i].HeaderCell.Value = $"Flight Plan {i}";
                SpeedsGrid[0, i].Value = plan.GetId();
                SpeedsGrid[1, i].Value = plan.GetVelocidad();
            }
        }

        private void SpeedsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.currentRow = e.RowIndex;
                label1.Text = $"Set new speed for flight: {SpeedsGrid[0, this.currentRow].Value}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentRow == -1)
                {
                    MessageBox.Show("First, select the flight plan whose speed you want to change.");
                }
                else if (Convert.ToDouble(textBox1.Text) <= 0)
                {
                    MessageBox.Show("Please enter a valid positive numeric value for speed.");
                }
                else
                {
                    int code = checkConflict();
                    if (code == 0)
                    {
                        FlightPlan plan = listaPlanes.GetFlightPlan(this.currentRow);
                        plan.SetVelocidad(Convert.ToDouble(textBox1.Text));
                        SpeedsGrid[1, this.currentRow].Value = plan.GetVelocidad();
                        MessageBox.Show("Speed successfully changed!");
                    }
                    else
                    {
                        MessageBox.Show("At that speed, the flight plan has conflicts!");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid numeric value for speed.");
            }
        }

        private int checkConflict()
        {
            FlightPlan planPrincipal = new FlightPlan(listaPlanes.GetFlightPlan(this.currentRow).GetId(), listaPlanes.GetFlightPlan(this.currentRow).GetCurrentPosition().GetX(), listaPlanes.GetFlightPlan(this.currentRow).GetCurrentPosition().GetY(), listaPlanes.GetFlightPlan(this.currentRow).GetDestino().GetX(), listaPlanes.GetFlightPlan(this.currentRow).GetDestino().GetY(), Convert.ToDouble(textBox1.Text));
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                if (this.currentRow != i)
                {
                    FlightPlan avion = new FlightPlan(listaPlanes.GetFlightPlan(i).GetId(), listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX(), listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY(), listaPlanes.GetFlightPlan(i).GetDestino().GetX(), listaPlanes.GetFlightPlan(i).GetDestino().GetY(), listaPlanes.GetFlightPlan(i).GetVelocidad());

                    bool conflicto = false;

                    // Simulamos el movimiento de los aviones
                    for (int k = 0; k < 10000; k++)
                    {
                        if (planPrincipal.Conflicto(avion, securityDistance))
                        {
                            conflicto = true;
                            break;
                        }

                        // Comprobamos si ambos aviones han llegado al destino
                        if (planPrincipal.GetCurrentPosition().Distancia(planPrincipal.GetDestino()) == 0 && avion.GetCurrentPosition().Distancia(avion.GetDestino()) == 0)
                        {
                            break;
                        }

                        planPrincipal.Mover(cycleDuration);
                        avion.Mover(cycleDuration);
                    }

                    if (conflicto)
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
