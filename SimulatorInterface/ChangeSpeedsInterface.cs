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
    public partial class ChangeSpeedsInterface : Form
    {
        FlightPlanList listaPlanes;
        double securityDistance;
        int currentRow = -1;
        bool reiniciar = false;
        public ChangeSpeedsInterface(FlightPlanList listaPlanes, double securityDistance)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            this.securityDistance = securityDistance;
        }

        private void ChangeSpeedsInterface_Load(object sender, EventArgs e)
        {
            // Deshabilita la edición de celdas pare permite hacer click y seleccionar
            SpeedsGrid.ReadOnly = true;

            // Hace que al hacer click en cualquier casilla se selecciona la fila entera
            SpeedsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            SpeedsGrid.ColumnCount = 3;
            SpeedsGrid.RowCount = this.listaPlanes.GetIndex();
            SpeedsGrid.Columns[0].HeaderText = "Flight Plan Airline";
            SpeedsGrid.Columns[1].HeaderText = "Flight Plan ID";
            SpeedsGrid.Columns[2].HeaderText = "Flight Plan Speed";
            SpeedsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SpeedsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                SpeedsGrid.Rows[i].HeaderCell.Value = $"Flight Plan {i}";
                SpeedsGrid[0, i].Value = plan.GetCompany();
                SpeedsGrid[1, i].Value = plan.GetId();
                SpeedsGrid[2, i].Value = plan.GetVelocidad();
            }
        }

        private void SpeedsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.currentRow = e.RowIndex;
                label1.Text = $"Set new speed for flight: {SpeedsGrid[0, this.currentRow].Value} {SpeedsGrid[1, this.currentRow].Value}";
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
                    double nuevaVelocidad = Convert.ToDouble(textBox1.Text);

                    if (!listaPlanes.HayConflictoConNuevaVelocidad(this.currentRow, nuevaVelocidad, securityDistance))
                    {
                        FlightPlan plan = listaPlanes.GetFlightPlan(this.currentRow);
                        plan.SetVelocidad(nuevaVelocidad);
                        SpeedsGrid[2, this.currentRow].Value = plan.GetVelocidad();
                        MessageBox.Show("Speed successfully changed!");
                        reiniciar = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool Instruction()
        { return reiniciar; }
    }
}
