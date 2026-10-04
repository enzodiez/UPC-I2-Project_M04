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
    public partial class AirspaceInterface : Form
    {
        FlightPlanList listaPlanes;
        double securityDistance;
        double cycleDuration;
        PictureBox[] aircraftsPics = new PictureBox[10];
        public AirspaceInterface(FlightPlanList listaPlanes, double securityDistance, double cycleDuration)
        {
            InitializeComponent();
            this.listaPlanes = listaPlanes;
            this.securityDistance = securityDistance;
            this.cycleDuration = cycleDuration;
        }
        private void Airspace_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            panel1.Paint += panel1_Paint;

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                aircraftsPics[i] = new PictureBox();
                aircraftsPics[i].Image = Image.FromFile("plane.png");
                aircraftsPics[i].Size = new Size(20, 20);
                aircraftsPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
                // Se hace un cast a int para que no haya problemas con la ubicación de los aviones en el panel
                // Esto significa que se trucan los valores de las coordenadas X e Y a enteros.
                aircraftsPics[i].Location = new Point((int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX(), (int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY());
                panel1.Controls.Add(aircraftsPics[i]);
                aircraftsPics[i].BringToFront();
                aircraftsPics[i].Click += aircraftsPics_Click; //No puede tener el nombre de la variable tras el +=
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                aircraftsPics[i].Location = new Point((int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX(), (int)listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY());
            }
        }
        //Añadimos el código para poder visualizar la trayectoria del avion con una línea
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;

            // Escogemos un color que en versiones futuras puede ser cambiado
            Pen myPen = new Pen(Color.Red, 2);

            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);

                // Puntos para definir la línea (+10 = centro del icono de 20x20)
                double Xo = plan.GetOrigen().GetX() + 10;
                double Yo = plan.GetOrigen().GetY() + 10;
                double Xd = plan.GetDestino().GetX() + 10;
                double Yd = plan.GetDestino().GetY() + 10;
                //Pasamos a enteros truncando los decimales
                Point origen = new Point((int)Xo, (int)Yo);
                Point destino = new Point((int)Xd, (int)Yd);

                // Dibujamos la línea entre el origen y el destino del avión
                graphics.DrawLine(myPen, origen, destino);
            }

            myPen.Dispose();
        }
        private void aircraftsPics_Click(object sender, EventArgs e)
        {
            PictureBox picClicado=(PictureBox)sender; //Para saber en que avion se ha clicado, hacemos cast a PictureBox.
            int indice = Array.IndexOf(aircraftsPics, picClicado);//para saber que indice clicamos en el array de aviones.
            var avionSeleccionado = listaPlanes.GetFlightPlan(indice);//cogemos la info del avion seleccionado.
            ClickedData fpView = new ClickedData();
            fpView.AvionSeleccionado = avionSeleccionado; //Ahora sale en rojo porque no se ha inicializado la propiedad AvionSeleccionado en ClickedData, hay que hacerlo en el constructor de ClickedData.
            fpView.ShowDialog();
        }
    } 
}