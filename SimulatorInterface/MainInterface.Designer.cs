namespace SimulatorInterface
{
    partial class MainInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addANewFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.relojAuto = new System.Windows.Forms.Timer(this.components);
            this.panelBotones = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(10, 10);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1480, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addANewFlightPlanToolStripMenuItem,
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem,
            this.startSimulationToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // addANewFlightPlanToolStripMenuItem
            // 
            this.addANewFlightPlanToolStripMenuItem.Name = "addANewFlightPlanToolStripMenuItem";
            this.addANewFlightPlanToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.addANewFlightPlanToolStripMenuItem.Text = "Add a new Flight Plan";
            this.addANewFlightPlanToolStripMenuItem.Click += new System.EventHandler(this.addANewFlightPlanToolStripMenuItem_Click);
            // 
            // addSecurityDistanceAndCycleDurationToolStripMenuItem
            // 
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Name = "addSecurityDistanceAndCycleDurationToolStripMenuItem";
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Text = "Add security distance and cycle duration";
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Click += new System.EventHandler(this.addSecurityDistanceAndCycleDurationToolStripMenuItem_Click);
            // 
            // startSimulationToolStripMenuItem
            // 
            this.startSimulationToolStripMenuItem.Name = "startSimulationToolStripMenuItem";
            this.startSimulationToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.startSimulationToolStripMenuItem.Text = "Start Simulation";
            this.startSimulationToolStripMenuItem.Click += new System.EventHandler(this.startSimulationToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(190, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 745);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "Mover";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(50, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 51);
            this.button2.TabIndex = 3;
            this.button2.Text = "Mover automático";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(50, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 52);
            this.button3.TabIndex = 4;
            this.button3.Text = "Flight Information";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(50, 281);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 51);
            this.button4.TabIndex = 5;
            this.button4.Text = "Conflicts";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(50, 338);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 51);
            this.button5.TabIndex = 6;
            this.button5.Text = "Change Speeds";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // relojAuto
            // 
            this.relojAuto.Interval = 1000;
            this.relojAuto.Tick += new System.EventHandler(this.relojAuto_Tick);
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.button1);
            this.panelBotones.Controls.Add(this.button5);
            this.panelBotones.Controls.Add(this.button2);
            this.panelBotones.Controls.Add(this.button4);
            this.panelBotones.Controls.Add(this.button3);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBotones.Location = new System.Drawing.Point(10, 34);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(180, 745);
            this.panelBotones.TabIndex = 7;
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 789);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainInterface";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainInterface_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addANewFlightPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSecurityDistanceAndCycleDurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startSimulationToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer relojAuto;
        private System.Windows.Forms.Panel panelBotones;
    }
}

