using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FlightLib;

namespace SimulatorInterface
{
    public partial class MainInterface : Form
    {
        FlightPlanList listaPlanes = new FlightPlanList();
        double securityDistance;
        double cycleDuration;
        PictureBox[] aircraftsPics = new PictureBox[10];
        Image planeImage = Image.FromFile("plane.png");
        
        // Tamaño del plano lógico (las coordenadas reales de los aviones irán dentro de este rango)
        const double VirtualWidth = 1500;
        const double VirtualHeight = 1000;

        public MainInterface()
        {
            InitializeComponent();
        }

        private void MainInterface_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Deshabilitamos los controles hasta que la simulación se inicie formalmente
            HabilitarBotonesSimulacion(false);

            panel1.Paint += panel1_Paint;
            panel1.Paint += panel1_Paint_1;
            panel1.Resize += panel1_Resize; // Evento de redimensionado
        }

        // Convierte una coordenada del plano virtual a píxeles en pantalla
        private Point WorldToScreen(double worldX, double worldY)
        {
            // Evitamos división por cero si el panel no se ha renderizado aún
            if (panel1.Width == 0 || panel1.Height == 0) return new Point(0, 0);

            int screenX = Convert.ToInt32((worldX / VirtualWidth) * panel1.Width);
            int screenY = Convert.ToInt32((worldY / VirtualHeight) * panel1.Height);
            return new Point(screenX, screenY);
        }

        // Escala distancias (como la distancia de seguridad) según el ancho actual del panel
        private float ScaleDistance(double distance)
        {
            if (panel1.Width == 0) return (float)distance;
            return (float)((distance / VirtualWidth) * panel1.Width);
        }

        // =======================================================
        // OPCIONES DEL MENÚ SUPERIOR (CARGA DE DATOS)
        // =======================================================

        private void addANewFlightPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFlightPlanInterface form = new NewFlightPlanInterface();
            form.ShowDialog();
            
            FlightPlan plan = form.DamePlan();
            if (plan != null)
            {
                int index = listaPlanes.GetIndex();
                if (listaPlanes.AddFlightPlan(plan) == 0)
                {
                    // Dibujamos el PictureBox del nuevo avión inmediatamente en el panel
                    CrearIconoAvion(index, plan);
                    panel1.Invalidate();
                    MessageBox.Show("Flight plan has been added.");

                    // Si los parámetros de seguridad ya están definidos, predecimos si este nuevo avión creará un conflicto
                    if (securityDistance > 0 && cycleDuration > 0)
                    {
                        string aviso = listaPlanes.PredecirConflictos(securityDistance, cycleDuration);
                        if (aviso != "" && !aviso.Contains("No hay conflictos"))
                        {
                            MessageBox.Show($"¡Atención! El vuelo añadido generará conflictos:\n{aviso}", "Alerta de Conflicto Futuro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se pueden añadir más aviones.");
                }
            }
            if (form.listacargadosfile!=null&&form.listacargadosfile.GetNumero()>0)
            {
                int avionesañadidos = 0; //usamos la variable publica creada antes en el creador de planes de vuelo
                for(int i=0;i<form.listacargadosfile.GetNumero();i++)
                {
                    
                    FlightPlan planarchivo = form.listacargadosfile.GetFlightPlan(i);
                    int index = listaPlanes.GetIndex();

                    if (listaPlanes.AddFlightPlan(planarchivo) == 0) // y rehacemos la simulacion entera con lo nuevo
                    {
                        CrearIconoAvion(index, planarchivo);
                        avionesañadidos++;
                    }
                    else
                    {
                        MessageBox.Show("Límite de aviones alcanzado.");
                        break;
                    }
                }

                if (avionesañadidos > 0)
                {
                    panel1.Invalidate();
                    MessageBox.Show("You successfully added {avionesañadidos} flights from the file.");
                }
            }
        }
        
        private void addSecurityDistanceAndCycleDurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecurityCiclesInterface form = new SecurityCiclesInterface();
            form.ShowDialog();

            if (form.DameSecurityDistance() != 0 && form.DameCycleDuration() != 0)
            {
                securityDistance = form.DameSecurityDistance();
                cycleDuration = form.DameCycleDuration();

                if (securityDistance != 0 && cycleDuration != 0)
                {
                    panel1.Invalidate();
                    MessageBox.Show("Security distance and cycle duration have been set.");
                }
            }
        }
        private void startSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listaPlanes.GetIndex() == 0)
            {
                MessageBox.Show("No flight plans have been added.");
            }
            else if (securityDistance == 0 || cycleDuration == 0)
            {
                MessageBox.Show("Security distance and cycle duration have not been set.");
            }
            else
            {
                // Comprobar conflictos antes de empezar
                if (listaPlanes.CompruebaConflicto(securityDistance, cycleDuration))
                {
                    DialogResult respuesta = MessageBox.Show("Se ha detectado un conflicto, ¿Desea resolverlo?", "Conflicto Detectado", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        if (listaPlanes.ResolverConflicto(securityDistance, cycleDuration))
                        {
                            MessageBox.Show("Conflicto resuelto. La nueva velocidad de " + listaPlanes.GetFlightPlan(1).GetId() + " es " + listaPlanes.GetFlightPlan(1).GetVelocidad());
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido resolver el conflicto cambiando la velocidad.");
                        }
                    }
                }
                // Se han superado las validaciones: desbloqueamos los controles
                HabilitarBotonesSimulacion(true);

                AlarmaConflictos();
                panel1.Invalidate();
            }
        }

        // =======================================================
        // GESTIÓN Y CREACIÓN DE AVIONES
        // =======================================================

        private void CrearIconoAvion(int i, FlightPlan plan)
        {
            // Calculamos dirección y ángulo
            double dx = plan.GetDestino().GetX() - plan.GetOrigen().GetX();
            double dy = plan.GetDestino().GetY() - plan.GetOrigen().GetY();
            float angulo = (float)(Math.Atan2(dy, dx) * (180.0 / Math.PI)) + 90;

            aircraftsPics[i] = new PictureBox();
            aircraftsPics[i].Image = RotateImage(planeImage, angulo);
            aircraftsPics[i].Size = new Size(20, 20);
            aircraftsPics[i].SizeMode = PictureBoxSizeMode.StretchImage;
            aircraftsPics[i].BackColor = Color.Transparent;

            // Convertimos las coordenadas virtuales a píxeles de pantalla y restamos 10 para centrar
            Point screenPos = WorldToScreen(plan.GetCurrentPosition().GetX(), plan.GetCurrentPosition().GetY());
            aircraftsPics[i].Location = new Point(screenPos.X - 10, screenPos.Y - 10);

            panel1.Controls.Add(aircraftsPics[i]);
            aircraftsPics[i].BringToFront();
            aircraftsPics[i].Click += aircraftsPics_Click;
        }

        private void ActualizarPosicionAviones()
        {
            for (int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);
                Point screenPos = WorldToScreen(plan.GetCurrentPosition().GetX(), plan.GetCurrentPosition().GetY());

                aircraftsPics[i].Location = new Point(screenPos.X - 10, screenPos.Y - 10);
            }
            panel1.Invalidate();
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

        // =======================================================
        // DIBUJO EN EL PANEL (PAINT)
        // =======================================================

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(panel1.BackColor);
            Graphics graphics = e.Graphics;

            using (Pen myPen = new Pen(Color.Green, 2))
            using (Pen myPen2 = new Pen(Color.Green, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (Font fontLabel = new Font("Consolas", 8, FontStyle.Bold))
            using (Brush brushText = new SolidBrush(Color.Lime))
            using (Pen penGrid = new Pen(Color.FromArgb(150, Color.Green), 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
            {
                // Líneas verticales cada 100 unidades del mundo
                for (double x = 100; x < VirtualWidth; x += 100)
                {
                    Point p1 = WorldToScreen(x, 0);
                    Point p2 = WorldToScreen(x, VirtualHeight);
                    graphics.DrawLine(penGrid, p1, p2);
                }

                // Líneas horizontales cada 100 unidades del mundo
                for (double y = 100; y < VirtualHeight; y += 100)
                {
                    Point p1 = WorldToScreen(0, y);
                    Point p2 = WorldToScreen(VirtualWidth, y);
                    graphics.DrawLine(penGrid, p1, p2);
                }

                for (int i = 0; i < listaPlanes.GetIndex(); i++)
                {
                    FlightPlan plan = listaPlanes.GetFlightPlan(i);

                    // Mapeamos los tres puntos a la pantalla
                    Point origen = WorldToScreen(plan.GetOrigen().GetX(), plan.GetOrigen().GetY());
                    Point current = WorldToScreen(plan.GetCurrentPosition().GetX(), plan.GetCurrentPosition().GetY());
                    Point destino = WorldToScreen(plan.GetDestino().GetX(), plan.GetDestino().GetY());

                    // Línea continua: Origen -> Posición actual
                    graphics.DrawLine(myPen, origen, current);
                    // Línea discontinua: Posición actual -> Destino
                    graphics.DrawLine(myPen2, current, destino);

                    // Dibujamos el ID y la velocidad al lado del avión (+12 px para no tapar el icono)
                    string info = $"{plan.GetId()} ({Math.Round(plan.GetVelocidad(), 0)} kts)";
                    graphics.DrawString(info, fontLabel, brushText, current.X + 12, current.Y - 6);
                }
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            int number = listaPlanes.GetNumero();
            for (int i = 0; i < number; i++)
            {
                FlightPlan plan = listaPlanes.GetFlightPlan(i);

                // Mapeamos el centro y escalamos el radio
                Point center = WorldToScreen(plan.GetCurrentPosition().GetX(), plan.GetCurrentPosition().GetY());
                float radioPx = ScaleDistance(this.securityDistance);

                e.Graphics.DrawEllipse(Pens.Red, center.X - radioPx, center.Y - radioPx, radioPx * 2, radioPx * 2);
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            // Si la simulación tiene aviones, los recolocamos al cambiar el tamaño del panel
            if (listaPlanes != null && listaPlanes.GetIndex() > 0)
            {
                ActualizarPosicionAviones();
            }
        }

        // =======================================================
        // BOTONES DE CONTROL DE SIMULACIÓN Y EVENTOS
        // =======================================================

        private void moverBtn_Click(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            ActualizarPosicionAviones();
        }

        private void automoverBtn_Click(object sender, EventArgs e)
        {
            if (automoverBtn.Text == "Automatic simulation")
            {
                relojAuto.Start();
                automoverBtn.Text = "Stop";
            }
            else if (automoverBtn.Text == "Stop")
            {
                relojAuto.Stop();
                automoverBtn.Text = "Automatic simulation";
            }
        }

        private void relojAuto_Tick(object sender, EventArgs e)
        {
            listaPlanes.Mover(cycleDuration);
            ActualizarPosicionAviones();
        }

        private void infoBtn_Click(object sender, EventArgs e)
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

        private void conflictsBtn_Click(object sender, EventArgs e)
        {
            string mensaje = listaPlanes.PredecirConflictos(securityDistance, cycleDuration);
            MessageBox.Show(mensaje, "Predicción de conflictos");
        }

        private void changeSpeedBtn_Click(object sender, EventArgs e)
        {
            ChangeSpeedsInterface csi = new ChangeSpeedsInterface(this.listaPlanes, this.securityDistance, this.cycleDuration);
            csi.ShowDialog();
            if (csi.Instruction())
            {
                listaPlanes.ResetAll(); // Reiniciamos todos los vuelos para que vuelvan a su origen después de cambiar las velocidades
                for (int i = 0; i < listaPlanes.GetIndex(); i++)
                {
                    double xinicial = listaPlanes.GetFlightPlan(i).GetOrigen().GetX();
                    double yinicial = listaPlanes.GetFlightPlan(i).GetOrigen().GetY();
                    Point screenPos = WorldToScreen(xinicial, yinicial);
                    aircraftsPics[i].Location = new Point(screenPos.X - 10, screenPos.Y - 10); //Centramos el png del avion en la posicion inicial
                }
                panel1.Invalidate();
            }
        }

        private void aircraftsPics_Click(object sender, EventArgs e)
        {
            PictureBox picClicado = (PictureBox)sender;
            int indice = Array.IndexOf(aircraftsPics, picClicado);
            var avionSeleccionado = listaPlanes.GetFlightPlan(indice);
            ClickedData fpView = new ClickedData();
            fpView.AvionSeleccionado = avionSeleccionado;
            fpView.ShowDialog();
        }

        private void AlarmaConflictos()
        {
            string mensaje = listaPlanes.ComprobarConflictos(securityDistance);

            if (mensaje != "")
            {
                relojAuto.Stop();
                automoverBtn.Text = "Mover automático";
                MessageBox.Show(mensaje, "Alarm");
            }
        }

        private void HabilitarBotonesSimulacion(bool estado)
        {
            moverBtn.Enabled = estado; // Mover
            automoverBtn.Enabled = estado; // Mover automático
            infoBtn.Enabled = estado; // Flight Information
            conflictsBtn.Enabled = estado; // Conflicts
            changeSpeedBtn.Enabled = estado; // Change Speeds
            restartBtn.Enabled = estado; // Restart
        }

        private void restartBtn_Click(object sender, EventArgs e)
        {
            listaPlanes.ResetAll();//reiniciamos todos los vuelos para que vuelvan a su origen PERO HAY QUE MOVER LOS PICTUREBOX!
            for(int i = 0; i < listaPlanes.GetIndex(); i++)
            {
                double xinicial = listaPlanes.GetFlightPlan(i).GetOrigen().GetX();
                double yinicial = listaPlanes.GetFlightPlan(i).GetOrigen().GetY();
                Point screenPos = WorldToScreen(xinicial, yinicial);
                aircraftsPics[i].Location = new Point(screenPos.X - 10, screenPos.Y - 10); //Centramos el png del avion en la posicion inicial

            }
            panel1.Invalidate();
        }

        private void testerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FlightPlan plane1 = new FlightPlan("TEST1", 100, 100, 900, 900, 100);
            FlightPlan plane2 = new FlightPlan("TEST2", 100, 900, 900, 100, 100);
            FlightPlan plane3 = new FlightPlan("TEST3", 700, 900, 700, 100, 100);
            if (plane1 != null)
            {
                int index = listaPlanes.GetIndex();
                if (listaPlanes.AddFlightPlan(plane1) == 0)
                {
                    // Dibujamos el PictureBox del nuevo avión inmediatamente en el panel
                    CrearIconoAvion(index, plane1);
                    panel1.Invalidate();
                }
                else
                {
                    MessageBox.Show("No se pueden añadir más aviones.");
                }
            }
            if (plane2 != null)
            {
                int index = listaPlanes.GetIndex();
                if (listaPlanes.AddFlightPlan(plane2) == 0)
                {
                    // Dibujamos el PictureBox del nuevo avión inmediatamente en el panel
                    CrearIconoAvion(index, plane2);
                    panel1.Invalidate();
                }
                else
                {
                    MessageBox.Show("No se pueden añadir más aviones.");
                }
            }
            if (plane3 != null)
            {
                int index = listaPlanes.GetIndex();
                if (listaPlanes.AddFlightPlan(plane3) == 0)
                {
                    // Dibujamos el PictureBox del nuevo avión inmediatamente en el panel
                    CrearIconoAvion(index, plane3);
                    panel1.Invalidate();
                }
                else
                {
                    MessageBox.Show("No se pueden añadir más aviones.");
                }
            }
        }
    }
}