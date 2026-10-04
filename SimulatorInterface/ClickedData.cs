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
        public FlightPlan AvionSeleccionado { get;set; } //Propiedad para almacenar el avión seleccionado que antes se ponia en rojo
        public ClickedData()
        {
            InitializeComponent();
        }

        private void ClickedData_Load(object sender, EventArgs e)
        {
            fpView.RowCount = 2;
            fpView.ColumnCount = 4;
            fpView.Rows[0].Cells[0].Value = "Aircraft Call Sign";
            fpView.Rows[0].Cells[1].Value = "Aircraft Speed";
            fpView.Rows[0].Cells[2].Value = "Current Position";
            fpView.Rows[0].Cells[3].Value = "Destination"; //Aquí podriamos añadir distancia con otros planes de vuelo
            //rellenamos la segunda fila con los datos del avion seleccionado:
            if (AvionSeleccionado != null)
            {
                fpView.Rows[1].Cells[0].Value = AvionSeleccionado.GetId();
                fpView.Rows[1].Cells[1].Value = AvionSeleccionado.GetVelocidad();
                fpView.Rows[1].Cells[2].Value = String.Format("X: {0},Y: {1}", Math.Round(AvionSeleccionado.GetCurrentPosition().GetX(), 2), Math.Round(AvionSeleccionado.GetCurrentPosition().GetY(), 2)); //redondeamos a 2 Con,2 en vez de F2
                fpView.Rows[1].Cells[3].Value = String.Format("X: {0},Y: {1}", Math.Round(AvionSeleccionado.GetDestino().GetX(), 2), Math.Round(AvionSeleccionado.GetDestino().GetY(), 2));
            }
        }

        private void returnBut_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
