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
        bool reiniciar = false;
        public ChangeSpeedsInterface(FlightPlanList listaPlanes, double securityDistance, double cycleDuration)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            this.securityDistance = securityDistance;
            this.cycleDuration = cycleDuration;
        }

        private void ChangeSpeedsInterface_Load(object sender, EventArgs e)
        {
            // Deshabilita la edición de celdas pero permite hacer clic y seleccionar
            SpeedsGrid.ReadOnly = true;

            // Hace que al hacer clic en cualquier casilla se seleccione la fila entera
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
                SpeedsGrid[0, i].Value = plan.GetAirline();
                SpeedsGrid[1, i].Value = plan.GetId();
                SpeedsGrid[2, i].Value = plan.GetVelocidad();
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
                        if (!reiniciar)
                        { reiniciar = true; }
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
            //Copiamos desde el origen con su nueva velocidad
            FlightPlan origenPrincipal = listaPlanes.GetFlightPlan(this.currentRow);
            FlightPlan planPrincipal = new FlightPlan(
                origenPrincipal.GetAirline(),
                origenPrincipal.GetId(),
                origenPrincipal.GetOrigen().GetX(),
                origenPrincipal.GetOrigen().GetY(),
                origenPrincipal.GetDestino().GetX(),
                origenPrincipal.GetDestino().GetY(),
                Convert.ToDouble(textBox1.Text)
            );

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                if (this.currentRow != i)
                {
                    //Comparamos con el plan original
                    if (HayConflicto(planPrincipal, listaPlanes.GetFlightPlan(i)))
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        // Posicion de un vuelo en el instante t (solo depende del tiempo)
        private Position PosicionEnTiempo(FlightPlan plan, double t)
        {
            Position o = plan.GetOrigen();
            Position d = plan.GetDestino();
            double longitud = o.Distancia(d);
            if (longitud == 0)
            {
                return new Position(o.GetX(), o.GetY());
            }
            //Igual que en mover
            double recorrido = Math.Min(plan.GetVelocidad() * t / 60.0, longitud);
            double fraccion = recorrido / longitud;
            return new Position(o.GetX() + fraccion * (d.GetX() - o.GetX()), o.GetY() + fraccion * (d.GetY() - o.GetY()));
        }

        // Tiempo en llegar
        private double TiempoDeVuelo(FlightPlan plan)
        {
            return plan.GetOrigen().Distancia(plan.GetDestino()) * 60.0 / plan.GetVelocidad();
        }

        // Devuelve true si en algun instante los dos vuelos estan a distancia <= 2 * securityDistance
        private bool HayConflicto(FlightPlan a, FlightPlan b)
        {
            double tPrimero = Math.Min(TiempoDeVuelo(a), TiempoDeVuelo(b)); 
            double tUltimo = Math.Max(TiempoDeVuelo(a), TiempoDeVuelo(b));  

            double minima = Math.Min(DistanciaMinima(a, b, 0, tPrimero), DistanciaMinima(a, b, tPrimero, tUltimo));

            return minima <= 2 * securityDistance;
        }

        // Distancia minima entre los dos vuelos entre los instantes t0 y t1
        private double DistanciaMinima(FlightPlan a, FlightPlan b, double t0, double t1)
        {
            // Vector entre los dos aviones al principio (d0) y al final (d1) del tramo
            double dx0 = PosicionEnTiempo(a, t0).GetX() - PosicionEnTiempo(b, t0).GetX();
            double dy0 = PosicionEnTiempo(a, t0).GetY() - PosicionEnTiempo(b, t0).GetY();
            double dx1 = PosicionEnTiempo(a, t1).GetX() - PosicionEnTiempo(b, t1).GetX();
            double dy1 = PosicionEnTiempo(a, t1).GetY() - PosicionEnTiempo(b, t1).GetY();

            double ex = dx1 - dx0;
            double ey = dy1 - dy0;
            double e2 = ex * ex + ey * ey;

            double s = 0;
            if (e2 > 0)
            {
                s = -(dx0 * ex + dy0 * ey) / e2;
                s = Math.Max(0, Math.Min(1, s)); // nos quedamos dentro del tramo
            }

            double mx = dx0 + s * ex;
            double my = dy0 + s * ey;
            return Math.Sqrt(mx * mx + my * my);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool Instruction()
        { return reiniciar; }
    }
}
