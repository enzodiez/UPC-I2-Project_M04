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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addANewFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSecurityDistanceAndCycleDurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
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
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainInterface";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainInterface_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addANewFlightPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSecurityDistanceAndCycleDurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startSimulationToolStripMenuItem;
    }
}

