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
        Database db;
        public FlightInformationInterface(FlightPlanList listaPlanes)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            db = new Database("usuarios.db");
        }
        private void FlightInformationInterface_Load(object sender, EventArgs e)
        {
            flightsGrid.ReadOnly = true;
            flightsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            flightsGrid.ColumnCount = 11;
            flightsGrid.RowCount = this.listaPlanes.GetIndex();
            flightsGrid.Columns[0].HeaderText = "Aircraft Airline";
            flightsGrid.Columns[1].HeaderText = "Airline Telefone";
            flightsGrid.Columns[2].HeaderText = "Airline email";
            flightsGrid.Columns[3].HeaderText = "Flight Plan ID";
            flightsGrid.Columns[4].HeaderText = "Speed";
            flightsGrid.Columns[5].HeaderText = "Initial position X";
            flightsGrid.Columns[6].HeaderText = "Initial position Y";
            flightsGrid.Columns[7].HeaderText = "Current position X";
            flightsGrid.Columns[8].HeaderText = "Current position Y";
            flightsGrid.Columns[9].HeaderText = "Final position X";
            flightsGrid.Columns[10].HeaderText = "Final position Y";
            flightsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                (string telefono, string email) = db.AirlineTelEmail(plan.GetCompany());
                flightsGrid.Rows[i].HeaderCell.Value = $"Flight Plan {i}";
                flightsGrid[0, i].Value = plan.GetCompany();
                flightsGrid[1, i].Value = telefono;
                flightsGrid[2, i].Value = email;
                flightsGrid[3, i].Value = plan.GetId();
                flightsGrid[4, i].Value = Math.Round(plan.GetVelocidad(), 2);
                flightsGrid[5, i].Value = plan.GetOrigen().GetX();
                flightsGrid[6, i].Value = plan.GetOrigen().GetY();
                flightsGrid[7, i].Value = Math.Round(plan.GetCurrentPosition().GetX(), 2);
                flightsGrid[8, i].Value = Math.Round(plan.GetCurrentPosition().GetY(), 2);
                flightsGrid[9, i].Value = plan.GetDestino().GetX();
                flightsGrid[10, i].Value = plan.GetDestino().GetY();
            }
        }

        // Muestra las distancias entre el vuelo de la fila pulsada y todos los demás
        private void flightsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (listaPlanes.GetIndex() < 2)
                {
                    MessageBox.Show("No hay otros vuelos en el espacio aéreo para calcular distancias.");
                    return;
                }

                FlightPlan seleccionado = listaPlanes.GetFlightPlan(e.RowIndex);
                string texto = $"Distancias desde {seleccionado.GetCompany()} {seleccionado.GetId()}:\n\n";

                for (int i = 0; i < listaPlanes.GetIndex(); i++)
                {
                    if (i != e.RowIndex)
                    {
                        FlightPlan otro = listaPlanes.GetFlightPlan(i);
                        double dist = Math.Round(seleccionado.Distance(otro), 2);
                        texto += $"- {otro.GetCompany()} {otro.GetId()}: {dist}\n";
                    }
                }

                MessageBox.Show(texto, "Distancias con el resto de vuelos");
            }
        }
    }
}
