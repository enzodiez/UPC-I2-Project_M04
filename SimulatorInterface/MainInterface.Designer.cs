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
            this.testerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moverBtn = new System.Windows.Forms.Button();
            this.automoverBtn = new System.Windows.Forms.Button();
            this.infoBtn = new System.Windows.Forms.Button();
            this.conflictsBtn = new System.Windows.Forms.Button();
            this.changeSpeedBtn = new System.Windows.Forms.Button();
            this.relojAuto = new System.Windows.Forms.Timer(this.components);
            this.panelBotones = new System.Windows.Forms.Panel();
            this.undoBtn = new System.Windows.Forms.Button();
            this.restartBtn = new System.Windows.Forms.Button();
            this.guardarBtn = new System.Windows.Forms.Button();
            this.saveSimBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(13, 12);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1684, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addANewFlightPlanToolStripMenuItem,
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem,
            this.startSimulationToolStripMenuItem,
            this.testerToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 26);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // addANewFlightPlanToolStripMenuItem
            // 
            this.addANewFlightPlanToolStripMenuItem.Name = "addANewFlightPlanToolStripMenuItem";
            this.addANewFlightPlanToolStripMenuItem.Size = new System.Drawing.Size(359, 26);
            this.addANewFlightPlanToolStripMenuItem.Text = "Add a new Flight Plan";
            this.addANewFlightPlanToolStripMenuItem.Click += new System.EventHandler(this.addANewFlightPlanToolStripMenuItem_Click);
            // 
            // addSecurityDistanceAndCycleDurationToolStripMenuItem
            // 
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Name = "addSecurityDistanceAndCycleDurationToolStripMenuItem";
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Size = new System.Drawing.Size(359, 26);
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Text = "Add security distance and cycle duration";
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem.Click += new System.EventHandler(this.addSecurityDistanceAndCycleDurationToolStripMenuItem_Click);
            // 
            // startSimulationToolStripMenuItem
            // 
            this.startSimulationToolStripMenuItem.Name = "startSimulationToolStripMenuItem";
            this.startSimulationToolStripMenuItem.Size = new System.Drawing.Size(359, 26);
            this.startSimulationToolStripMenuItem.Text = "Start Simulation";
            this.startSimulationToolStripMenuItem.Click += new System.EventHandler(this.startSimulationToolStripMenuItem_Click);
            // 
            // testerToolStripMenuItem
            // 
            this.testerToolStripMenuItem.Name = "testerToolStripMenuItem";
            this.testerToolStripMenuItem.Size = new System.Drawing.Size(359, 26);
            this.testerToolStripMenuItem.Text = "Tester";
            this.testerToolStripMenuItem.Click += new System.EventHandler(this.testerToolStripMenuItem_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(253, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1444, 786);
            this.panel1.TabIndex = 1;
            // 
            // moverBtn
            // 
            this.moverBtn.Location = new System.Drawing.Point(67, 133);
            this.moverBtn.Margin = new System.Windows.Forms.Padding(4);
            this.moverBtn.Name = "moverBtn";
            this.moverBtn.Size = new System.Drawing.Size(100, 64);
            this.moverBtn.TabIndex = 2;
            this.moverBtn.Text = "Mover";
            this.moverBtn.UseVisualStyleBackColor = true;
            this.moverBtn.Click += new System.EventHandler(this.moverBtn_Click);
            // 
            // automoverBtn
            // 
            this.automoverBtn.Location = new System.Drawing.Point(67, 204);
            this.automoverBtn.Margin = new System.Windows.Forms.Padding(4);
            this.automoverBtn.Name = "automoverBtn";
            this.automoverBtn.Size = new System.Drawing.Size(100, 63);
            this.automoverBtn.TabIndex = 3;
            this.automoverBtn.Text = "Automatic simulation";
            this.automoverBtn.UseVisualStyleBackColor = true;
            this.automoverBtn.Click += new System.EventHandler(this.automoverBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Location = new System.Drawing.Point(67, 274);
            this.infoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(100, 64);
            this.infoBtn.TabIndex = 4;
            this.infoBtn.Text = "Flight Information";
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // conflictsBtn
            // 
            this.conflictsBtn.Location = new System.Drawing.Point(67, 346);
            this.conflictsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.conflictsBtn.Name = "conflictsBtn";
            this.conflictsBtn.Size = new System.Drawing.Size(100, 63);
            this.conflictsBtn.TabIndex = 5;
            this.conflictsBtn.Text = "Conflicts";
            this.conflictsBtn.UseVisualStyleBackColor = true;
            this.conflictsBtn.Click += new System.EventHandler(this.conflictsBtn_Click);
            // 
            // changeSpeedBtn
            // 
            this.changeSpeedBtn.Location = new System.Drawing.Point(67, 416);
            this.changeSpeedBtn.Margin = new System.Windows.Forms.Padding(4);
            this.changeSpeedBtn.Name = "changeSpeedBtn";
            this.changeSpeedBtn.Size = new System.Drawing.Size(100, 63);
            this.changeSpeedBtn.TabIndex = 6;
            this.changeSpeedBtn.Text = "Change Speeds";
            this.changeSpeedBtn.UseVisualStyleBackColor = true;
            this.changeSpeedBtn.Click += new System.EventHandler(this.changeSpeedBtn_Click);
            // 
            // relojAuto
            // 
            this.relojAuto.Interval = 1000;
            this.relojAuto.Tick += new System.EventHandler(this.relojAuto_Tick);
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.saveSimBtn);
            this.panelBotones.Controls.Add(this.guardarBtn);
            this.panelBotones.Controls.Add(this.undoBtn);
            this.panelBotones.Controls.Add(this.restartBtn);
            this.panelBotones.Controls.Add(this.moverBtn);
            this.panelBotones.Controls.Add(this.changeSpeedBtn);
            this.panelBotones.Controls.Add(this.automoverBtn);
            this.panelBotones.Controls.Add(this.conflictsBtn);
            this.panelBotones.Controls.Add(this.infoBtn);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBotones.Location = new System.Drawing.Point(13, 42);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(4);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(240, 786);
            this.panelBotones.TabIndex = 7;
            // 
            // undoBtn
            // 
            this.undoBtn.Enabled = false;
            this.undoBtn.Location = new System.Drawing.Point(67, 556);
            this.undoBtn.Name = "undoBtn";
            this.undoBtn.Size = new System.Drawing.Size(100, 63);
            this.undoBtn.TabIndex = 8;
            this.undoBtn.Text = "Undo last step";
            this.undoBtn.UseVisualStyleBackColor = true;
            this.undoBtn.Click += new System.EventHandler(this.undoBtn_Click);
            // 
            // restartBtn
            // 
            this.restartBtn.Location = new System.Drawing.Point(67, 486);
            this.restartBtn.Margin = new System.Windows.Forms.Padding(4);
            this.restartBtn.Name = "restartBtn";
            this.restartBtn.Size = new System.Drawing.Size(100, 63);
            this.restartBtn.TabIndex = 7;
            this.restartBtn.Text = "Restart Simulation";
            this.restartBtn.UseVisualStyleBackColor = true;
            this.restartBtn.Click += new System.EventHandler(this.restartBtn_Click);
            // 
            // guardarBtn
            // 
            this.guardarBtn.Location = new System.Drawing.Point(67, 625);
            this.guardarBtn.Name = "guardarBtn";
            this.guardarBtn.Size = new System.Drawing.Size(0, 0);
            this.guardarBtn.TabIndex = 9;
            this.guardarBtn.Text = "Save Simulation";
            this.guardarBtn.UseVisualStyleBackColor = true;
            // 
            // saveSimBtn
            // 
            this.saveSimBtn.Enabled = false;
            this.saveSimBtn.Location = new System.Drawing.Point(67, 625);
            this.saveSimBtn.Name = "saveSimBtn";
            this.saveSimBtn.Size = new System.Drawing.Size(100, 63);
            this.saveSimBtn.TabIndex = 10;
            this.saveSimBtn.Text = "Save Simulation";
            this.saveSimBtn.UseVisualStyleBackColor = true;
            this.saveSimBtn.Click += new System.EventHandler(this.saveSimBtn_Click);
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 840);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainInterface";
            this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
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
        private System.Windows.Forms.Button moverBtn;
        private System.Windows.Forms.Button automoverBtn;
        private System.Windows.Forms.Button infoBtn;
        private System.Windows.Forms.Button conflictsBtn;
        private System.Windows.Forms.Button changeSpeedBtn;
        private System.Windows.Forms.Timer relojAuto;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button restartBtn;
        private System.Windows.Forms.ToolStripMenuItem testerToolStripMenuItem;
        private System.Windows.Forms.Button undoBtn;
        private System.Windows.Forms.Button guardarBtn;
        private System.Windows.Forms.Button saveSimBtn;
    }
}