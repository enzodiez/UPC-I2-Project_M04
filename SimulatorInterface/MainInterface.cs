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
        List<PictureBox> aircraftsPics = new List<PictureBox>();
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
            bool estabaAutomatico = relojAuto.Enabled;
            relojAuto.Stop();

            int vuelosAntes = listaPlanes.GetIndex();

            NewFlightPlanInterface form = new NewFlightPlanInterface(listaPlanes);
            form.ShowDialog();

            if (form.EsSimulacionCargada())
            {
                securityDistance = form.DameDistanciaCargada();
                cycleDuration = form.DameCicloCargado();

                for (int i=0; i<aircraftsPics.Count; i++)
                {
                    panel1.Controls.Remove(aircraftsPics[i]);
                    aircraftsPics[i].Dispose();
                }

                aircraftsPics.Clear();

                // Crear los iconos de los vuelos cargados
                for (int i = 0; i < listaPlanes.GetIndex(); i++)
                {
                    CrearIconoAvion(i, listaPlanes.GetFlightPlan(i));
                }

                automoverBtn.Text = "Automatic simulation";
                HabilitarBotonesSimulacion(true);
                ActualizarPosicionAviones();
            }

            else
            {
                // Funcionamiento original: añadir nuevos aviones
                int vuelosDespues = listaPlanes.GetIndex();

                if (vuelosDespues>vuelosAntes)
                {
                    for (int i=vuelosAntes; i<vuelosDespues; i++)
                    {
                        CrearIconoAvion(i,listaPlanes.GetFlightPlan(i));
                    }
                    panel1.Invalidate();
                    VerificarConflictosFuturos();
                }

                if (estabaAutomatico && listaPlanes.HayVuelosEnCurso())
                {
                    relojAuto.Start();
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
                panel1.Invalidate();
                MessageBox.Show("Security distance and cycle duration have been set.");
                VerificarConflictosFuturos();
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
                if (listaPlanes.CompruebaConflicto(securityDistance))
                {
                    DialogResult respuesta = MessageBox.Show("Se ha detectado un conflicto, ¿Desea resolverlo?", "Conflicto Detectado", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        if (listaPlanes.ResolverConflicto(securityDistance))
                        {
                            MessageBox.Show("Conflicto resuelto. La nueva velocidad de " + listaPlanes.GetFlightPlan(1).GetCompany() + " " + listaPlanes.GetFlightPlan(1).GetId() + " es " + listaPlanes.GetFlightPlan(1).GetVelocidad());
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

        private void testerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AgregarVueloAlMapa(new FlightPlan("TEST1", "AIRLINE1", 100, 100, 900, 900, 100));
            AgregarVueloAlMapa(new FlightPlan("TEST2", "AIRLINE2", 100, 900, 900, 100, 100));
            AgregarVueloAlMapa(new FlightPlan("TEST3", "AIRLINE3", 700, 900, 700, 100, 100));
            panel1.Invalidate();
            VerificarConflictosFuturos();
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

            aircraftsPics.Add(new PictureBox());
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
            undoBtn.Enabled = listaPlanes.PuedeDeshacer();
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

                // Calculamos el radio en píxeles una sola vez antes del bucle
                float radioPx = ScaleDistance(this.securityDistance);

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
                    string info = $"{plan.GetCompany()} {plan.GetId()} ({Math.Round(plan.GetVelocidad(), 0)} kts)";
                    graphics.DrawString(info, fontLabel, brushText, current.X + 12, current.Y - 6);

                    // Dibujamos la esfera de seguridad reutilizando 'current'
                    graphics.DrawEllipse(Pens.Red, current.X - radioPx, current.Y - radioPx, radioPx * 2, radioPx * 2);
                }
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
            if (!listaPlanes.HayVuelosEnCurso())
            {
                relojAuto.Stop();
                MessageBox.Show("Todos los vuelos han finalizado!");
            }
            else if (automoverBtn.Text == "Automatic simulation")
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

            if (!listaPlanes.HayVuelosEnCurso())
            {
                relojAuto.Stop();
                automoverBtn.Text = "Automatic simulation";
                MessageBox.Show("Todos los vuelos han finalizado!");
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            FlightInformationInterface fii = new FlightInformationInterface(this.listaPlanes);
            fii.ShowDialog();
        }

        private void conflictsBtn_Click(object sender, EventArgs e)
        {
            string mensaje = listaPlanes.PredecirConflictos(securityDistance);
            MessageBox.Show(mensaje, "Predicción de conflictos");
        }

        private void changeSpeedBtn_Click(object sender, EventArgs e)
        {
            ChangeSpeedsInterface csi = new ChangeSpeedsInterface(this.listaPlanes, this.securityDistance);
            csi.ShowDialog();
            if (csi.Instruction())
            {
                ReiniciarSimulacion();
            }
        }

        private void aircraftsPics_Click(object sender, EventArgs e)
        {
            PictureBox picClicado = (PictureBox)sender;
            int indice = aircraftsPics.IndexOf(picClicado);
            ClickedData fpView = new ClickedData(listaPlanes.GetFlightPlan(indice));
            fpView.ShowDialog();
        }

        private void AlarmaConflictos()
        {
            string mensaje = listaPlanes.ComprobarConflictos(securityDistance);

            if (mensaje != "")
            {
                relojAuto.Stop();
                automoverBtn.Text = "Automatic simulation";
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
            saveSimBtn.Enabled = estado;
            undoBtn.Enabled = estado && listaPlanes.PuedeDeshacer();
        }

        private void restartBtn_Click(object sender, EventArgs e)
        {
            ReiniciarSimulacion();
        }

        // =======================================================
        // MÉTODOS AUXILIARES
        // =======================================================

        private void AgregarVueloAlMapa(FlightPlan plan)
        {
            int index = listaPlanes.GetIndex();
            listaPlanes.AddFlightPlan(plan);
            CrearIconoAvion(index, plan);
        }

        private void VerificarConflictosFuturos()
        {
            if (securityDistance > 0 && cycleDuration > 0)
            {
                string aviso = listaPlanes.PredecirConflictos(securityDistance);
                if (aviso != "" && !aviso.Contains("No se han detectado conflictos futuros."))
                {
                    MessageBox.Show($"¡Atención! El vuelo añadido generará conflictos:\n{aviso}", "Alerta de Conflicto Futuro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ReiniciarSimulacion()
        {
            if (relojAuto.Enabled)
            {
                relojAuto.Stop();
                automoverBtn.Text = "Automatic simulation";
            }

            listaPlanes.ResetAll();
            ActualizarPosicionAviones();
        }

           private void undoBtn_Click(object sender, EventArgs e)
        {
            relojAuto.Stop();
            automoverBtn.Text = "Automatic simulation";
            if (listaPlanes.DeshacerUltimoPaso())
            {
                ActualizarPosicionAviones();
            }
        }

        private void saveSimBtn_Click(object sender, EventArgs e)
        {
            bool estabaAutomatico = relojAuto.Enabled;
            relojAuto.Stop();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.FileName = "simulacion.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    listaPlanes.GuardarEnArchivo(dialog.FileName, securityDistance, cycleDuration);
                    MessageBox.Show("Simulation saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving simulation: " + ex.Message);
                }
            }
            dialog.Dispose();
            if (estabaAutomatico && listaPlanes.HayVuelosEnCurso())
            {
                relojAuto.Start();
            }
        }
    }
}