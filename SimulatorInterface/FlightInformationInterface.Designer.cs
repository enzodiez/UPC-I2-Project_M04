namespace SimulatorInterface
{
    partial class FlightInformationInterface
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
            this.flightsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.flightsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // flightsGrid
            // 
            this.flightsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.flightsGrid.Location = new System.Drawing.Point(217, 80);
            this.flightsGrid.Name = "flightsGrid";
            this.flightsGrid.RowHeadersWidth = 62;
            this.flightsGrid.RowTemplate.Height = 28;
            this.flightsGrid.Size = new System.Drawing.Size(852, 647);
            this.flightsGrid.TabIndex = 0;
            this.flightsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.flightsGrid_CellClick);
            // 
            // FlightInformationInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 928);
            this.Controls.Add(this.flightsGrid);
            this.Name = "FlightInformationInterface";
            this.Text = "FlightInformationInterface";
            this.Load += new System.EventHandler(this.FlightInformationInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flightsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView flightsGrid;
    }
}