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
        Image planeImage = Image.FromFile("plane.png");

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
                FlightPlan plan = listaPlanes.GetFlightPlan(i);

                // Calculamos la diferencia de coordenadas
                double dx = plan.GetDestino().GetX() - plan.GetOrigen().GetX();
                double dy = plan.GetDestino().GetY() - plan.GetOrigen().GetY();

                // Calculamos el ángulo en grados
                float angulo = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI)) + 90;

                aircraftsPics[i] = new PictureBox();

                // Asignamos la imagen rotada
                aircraftsPics[i].Image = RotateImage(planeImage, angulo);

                aircraftsPics[i].Size = new Size(20, 20);
                aircraftsPics[i].SizeMode = PictureBoxSizeMode.StretchImage;

                // Centramos la imagen de 20x20 en la coordenada teórica (-10)
                int posX = Convert.ToInt32(plan.GetCurrentPosition().GetX()) - 10;
                int posY = Convert.ToInt32(plan.GetCurrentPosition().GetY()) - 10;

                aircraftsPics[i].Location = new Point(posX, posY);
                panel1.Controls.Add(aircraftsPics[i]);
                aircraftsPics[i].BringToFront();
                aircraftsPics[i].Click += aircraftsPics_Click;
            }

            AlarmaConflictos();
        }
        private Image RotateImage(Image img, float angle)
        {
            // Método para rotar la imagen de los aviones y orientarlos correctamente
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Trasladamos el origen al centro del lienzo para rotar sobre el centro de la imagen
                g.TranslateTransform(img.Width / 2f, img.Height / 2f);      // Trasladamos al centro
                g.RotateTransform(angle);                                   // Rotamos el ángulo indicado
                g.TranslateTransform(-img.Width / 2f, -img.Height / 2f);    // Deshacemos el traslado

                // Dibujamos la imagen original rotada
                g.DrawImage(img, new Point(0, 0));
            }
            return bmp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                int posX = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX()) - 10;
                int posY = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY()) - 10;
                aircraftsPics[i].Location = new Point(posX, posY);
            }
            panel1.Invalidate();
            AlarmaConflictos();
        }

        //Añadimos el código para poder visualizar la trayectoria del avion con una línea
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Uso de 'using' para asegurar que ambos 'Pen' se liberen correctamente al terminar
            // Escogemos el color verde. Más adelante se podrá cambiar.
            using (Pen myPen = new Pen(Color.Green, 2))
            using (Pen myPen2 = new Pen(Color.Green, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                for (int i = 0; i < listaPlanes.GetIndex(); i++)
                {
                    FlightPlan plan = listaPlanes.GetFlightPlan(i);

                    // Las tres coordenadas teóricas apuntan al centro exacto del trayecto
                    Point origen = new Point(
                        Convert.ToInt32(plan.GetOrigen().GetX()),
                        Convert.ToInt32(plan.GetOrigen().GetY())
                    );

                    Point current = new Point(
                        Convert.ToInt32(plan.GetCurrentPosition().GetX()),
                        Convert.ToInt32(plan.GetCurrentPosition().GetY())
                    );

                    Point destino = new Point(
                        Convert.ToInt32(plan.GetDestino().GetX()),
                        Convert.ToInt32(plan.GetDestino().GetY())
                    );

                    // Línea continua: Origen -> Posición actual
                    graphics.DrawLine(myPen, origen, current);

                    // Línea discontinua: Posición actual -> Destino
                    graphics.DrawLine(myPen2, current, destino);
                }
            }
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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            
            int number = listaPlanes.GetNumero(); //sacamos el numero total de planes de vuelo
            for(int i = 0; i<number; i++) // y hacemos que el for los coja todos, y que cree un objeto FP para cada uno de ellos
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                float x = (float)plan.GetCurrentPosition().GetX(); //hay que convertir a float para que la elipse funcione
                float y = (float)plan.GetCurrentPosition().GetY();
                float radio = (float)this.securityDistance;

                //Dibujamos el círculo centrado en (x, y):
                e.Graphics.DrawEllipse(Pens.Red, x - radio, y - radio, radio * 2, radio * 2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Mover automático")
            {
                relojAuto.Start();
                button2.Text = "Stop";
            }
            else if (button2.Text == "Stop")
            {
                relojAuto.Stop();
                button2.Text = "Mover automático";
            }
        }

        private void relojAuto_Tick(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                int posX = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetX()) - 10;
                int posY = Convert.ToInt32(listaPlanes.GetFlightPlan(i).GetCurrentPosition().GetY()) - 10;
                aircraftsPics[i].Location = new Point(posX, posY);
            }
            panel1.Invalidate();
            AlarmaConflictos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FlightInformationInterface fii = new FlightInformationInterface(this.listaPlanes);
            fii.ShowDialog();
            int pos1 = fii.GetSelectedRow1();
            int pos2 = fii.GetSelectedRow2();
            if (pos1 != -1 && pos2 != -1)
            {
                MessageBox.Show($"Distance between flight {listaPlanes.GetFlightPlan(pos1).GetId()} and {listaPlanes.GetFlightPlan(pos2).GetId()} is: {listaPlanes.GetFlightPlan(pos1).Distance(listaPlanes.GetFlightPlan(pos2))}");
            }
        }
        private void AlarmaConflictos()
        {
            string mensaje = listaPlanes.ComprobarConflictos(securityDistance);

            if (mensaje != "")
            {
                relojAuto.Stop();
                button2.Text = "Mover automático";
                MessageBox.Show(mensaje, "Alarm");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = listaPlanes.PredecirConflictos(securityDistance, cycleDuration);
            MessageBox.Show(mensaje, "Predicción de conflictos");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeSpeedsInterface csi = new ChangeSpeedsInterface(this.listaPlanes, this.securityDistance, this.cycleDuration);
            csi.ShowDialog();
            restart();
        }

        private void restart()
        {

        }
    } 
}