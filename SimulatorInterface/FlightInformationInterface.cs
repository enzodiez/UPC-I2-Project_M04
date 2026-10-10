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
    public partial class FlightInformationInterface : Form
    {
        FlightPlanList listaPlanes;
        int f1 = -1, f2 = -1;
        public FlightInformationInterface(FlightPlanList listaPlanes)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
        }
        public int GetSelectedRow1()
        { return f1; }
        public int GetSelectedRow2()
        { return f2; }
        private void FlightInformationInterface_Load(object sender, EventArgs e)
        {
            flightsGrid.ColumnCount = 8;
            flightsGrid.RowCount = this.listaPlanes.GetIndex();
            flightsGrid.Columns[0].HeaderText = "Flight Plan ID";
            flightsGrid.Columns[1].HeaderText = "Speed";
            flightsGrid.Columns[2].HeaderText = "Initial position X";
            flightsGrid.Columns[3].HeaderText = "Initial position Y";
            flightsGrid.Columns[4].HeaderText = "Current position X";
            flightsGrid.Columns[5].HeaderText = "Current position Y";
            flightsGrid.Columns[6].HeaderText = "Final position X";
            flightsGrid.Columns[7].HeaderText = "Final position Y";
            flightsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                flightsGrid.Rows[i].HeaderCell.Value = $"Flight Plan {i}";
                flightsGrid[0, i].Value = plan.GetId();
                flightsGrid[1, i].Value = plan.GetVelocidad();
                flightsGrid[2, i].Value = plan.GetOrigen().GetX();
                flightsGrid[3, i].Value = plan.GetOrigen().GetY();
                flightsGrid[4, i].Value = plan.GetCurrentPosition().GetX();
                flightsGrid[5, i].Value = plan.GetCurrentPosition().GetY();
                flightsGrid[6, i].Value = plan.GetDestino().GetX();
                flightsGrid[7, i].Value = plan.GetDestino().GetY();
            }
        }
        private void flightsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (f1 == -1)
            {
                f1 = e.RowIndex;
                if (f1 != -1)
                {
                    MessageBox.Show("Flight 1 selected.");
                }
            }
            else if (f2 == -1)
            {
                f2 = e.RowIndex;
                if (f2 != -1)
                {
                    MessageBox.Show("Flight 2 selected.");
                    Close();
                }
            }
        }
    }
}
