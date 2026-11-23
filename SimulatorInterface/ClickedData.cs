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
    public partial class ClickedData : Form
    {
        FlightPlan avionSeleccionado; //Propiedad para almacenar el avión seleccionado que antes se ponia en rojo
        Database db;

        // Constructor: guarda el avión seleccionado para mostrar sus datos
        public ClickedData(FlightPlan avionSel)
        {
            InitializeComponent();
            avionSeleccionado = avionSel;
            db = new Database("usuarios.db");
        }

        
        // Configura el aspecto de la tabla y rellena los datos del vuelo al cargar el formulario
        private void ClickedData_Load(object sender, EventArgs e)
        {
            fpView.ReadOnly = true;
            fpView.AllowUserToAddRows = false;
            fpView.AllowUserToDeleteRows = false;
            fpView.AllowUserToResizeRows = false;
            fpView.RowHeadersVisible = false;
            fpView.ColumnHeadersVisible = true;
            fpView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            fpView.ColumnCount = 7;
            fpView.RowCount = 1;

            fpView.Columns[0].HeaderText = "Aircraft Airline";
            fpView.Columns[1].HeaderText = "Airline Telefone";
            fpView.Columns[2].HeaderText = "Airline email";
            fpView.Columns[3].HeaderText = "Aircraft Call Sign";
            fpView.Columns[4].HeaderText = "Aircraft Speed";
            fpView.Columns[5].HeaderText = "Current Position";
            fpView.Columns[6].HeaderText = "Destination";

            // Ajusta columnas y filas al tamaño de la tabla y centra el texto
            fpView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            fpView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            fpView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fpView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Rellenamos la fila con los datos del avión seleccionado
            if (avionSeleccionado != null)
            {
                (string telefono, string email) = db.AirlineTelEmail(avionSeleccionado.GetCompany());
                fpView.Rows[0].Cells[0].Value = avionSeleccionado.GetCompany();
                fpView.Rows[0].Cells[1].Value = telefono;
                fpView.Rows[0].Cells[2].Value = email;
                fpView.Rows[0].Cells[3].Value = avionSeleccionado.GetId();
                fpView.Rows[0].Cells[4].Value = Math.Round(avionSeleccionado.GetVelocidad(), 2);
                fpView.Rows[0].Cells[5].Value = String.Format("X: {0}, Y: {1}", Math.Round(avionSeleccionado.GetCurrentPosition().GetX(), 2), Math.Round(avionSeleccionado.GetCurrentPosition().GetY(), 2));
                fpView.Rows[0].Cells[6].Value = String.Format("X: {0}, Y: {1}", Math.Round(avionSeleccionado.GetDestino().GetX(), 2), Math.Round(avionSeleccionado.GetDestino().GetY(), 2));
            }

            // Quita el sombreado azul inicial de la primera celda
            fpView.ClearSelection();
        }

        private void returnBut_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}