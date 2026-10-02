namespace SimulatorInterface
{
    partial class NewFlightPlanInterface
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
            this.FX = new System.Windows.Forms.TextBox();
            this.Speed = new System.Windows.Forms.TextBox();
            this.IY = new System.Windows.Forms.TextBox();
            this.IX = new System.Windows.Forms.TextBox();
            this.SpeedLbl = new System.Windows.Forms.Label();
            this.InitialPositionLbl = new System.Windows.Forms.Label();
            this.CallSignLbl = new System.Windows.Forms.Label();
            this.FinalPositionLbl = new System.Windows.Forms.Label();
            this.CallSign = new System.Windows.Forms.TextBox();
            this.FY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Guardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FX
            // 
            this.FX.Location = new System.Drawing.Point(183, 432);
            this.FX.Name = "FX";
            this.FX.Size = new System.Drawing.Size(100, 22);
            this.FX.TabIndex = 0;
            // 
            // Speed
            // 
            this.Speed.Location = new System.Drawing.Point(105, 213);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(100, 22);
            this.Speed.TabIndex = 1;
            // 
            // IY
            // 
            this.IY.Location = new System.Drawing.Point(368, 315);
            this.IY.Name = "IY";
            this.IY.Size = new System.Drawing.Size(100, 22);
            this.IY.TabIndex = 2;
            this.IY.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // IX
            // 
            this.IX.Location = new System.Drawing.Point(183, 315);
            this.IX.Name = "IX";
            this.IX.Size = new System.Drawing.Size(100, 22);
            this.IX.TabIndex = 3;
            this.IX.TextChanged += new System.EventHandler(this.IX_TextChanged);
            // 
            // SpeedLbl
            // 
            this.SpeedLbl.AutoSize = true;
            this.SpeedLbl.Location = new System.Drawing.Point(102, 173);
            this.SpeedLbl.Name = "SpeedLbl";
            this.SpeedLbl.Size = new System.Drawing.Size(48, 16);
            this.SpeedLbl.TabIndex = 4;
            this.SpeedLbl.Text = "Speed";
            // 
            // InitialPositionLbl
            // 
            this.InitialPositionLbl.AutoSize = true;
            this.InitialPositionLbl.Location = new System.Drawing.Point(102, 272);
            this.InitialPositionLbl.Name = "InitialPositionLbl";
            this.InitialPositionLbl.Size = new System.Drawing.Size(88, 16);
            this.InitialPositionLbl.TabIndex = 5;
            this.InitialPositionLbl.Text = "Initial Position";
            // 
            // CallSignLbl
            // 
            this.CallSignLbl.AutoSize = true;
            this.CallSignLbl.Location = new System.Drawing.Point(102, 68);
            this.CallSignLbl.Name = "CallSignLbl";
            this.CallSignLbl.Size = new System.Drawing.Size(60, 16);
            this.CallSignLbl.TabIndex = 6;
            this.CallSignLbl.Text = "Call Sign";
            // 
            // FinalPositionLbl
            // 
            this.FinalPositionLbl.AutoSize = true;
            this.FinalPositionLbl.Location = new System.Drawing.Point(102, 373);
            this.FinalPositionLbl.Name = "FinalPositionLbl";
            this.FinalPositionLbl.Size = new System.Drawing.Size(87, 16);
            this.FinalPositionLbl.TabIndex = 7;
            this.FinalPositionLbl.Text = "Final Position";
            // 
            // CallSign
            // 
            this.CallSign.Location = new System.Drawing.Point(105, 109);
            this.CallSign.Name = "CallSign";
            this.CallSign.Size = new System.Drawing.Size(100, 22);
            this.CallSign.TabIndex = 8;
            // 
            // FY
            // 
            this.FY.Location = new System.Drawing.Point(368, 432);
            this.FY.Name = "FY";
            this.FY.Size = new System.Drawing.Size(100, 22);
            this.FY.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(399, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "x";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Guardar
            // 
            this.Guardar.Location = new System.Drawing.Point(237, 529);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(75, 23);
            this.Guardar.TabIndex = 12;
            this.Guardar.Text = "Guardar";
            this.Guardar.UseVisualStyleBackColor = true;
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // NewFlightPlanInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 594);
            this.Controls.Add(this.Guardar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FY);
            this.Controls.Add(this.CallSign);
            this.Controls.Add(this.FinalPositionLbl);
            this.Controls.Add(this.CallSignLbl);
            this.Controls.Add(this.InitialPositionLbl);
            this.Controls.Add(this.SpeedLbl);
            this.Controls.Add(this.IX);
            this.Controls.Add(this.IY);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.FX);
            this.Name = "NewFlightPlanInterface";
            this.Text = "NewFlightPlanInterface";
            this.Load += new System.EventHandler(this.NewFlightPlanInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FX;
        private System.Windows.Forms.TextBox Speed;
        private System.Windows.Forms.TextBox IY;
        private System.Windows.Forms.TextBox IX;
        private System.Windows.Forms.Label SpeedLbl;
        private System.Windows.Forms.Label InitialPositionLbl;
        private System.Windows.Forms.Label CallSignLbl;
        private System.Windows.Forms.Label FinalPositionLbl;
        private System.Windows.Forms.TextBox CallSign;
        private System.Windows.Forms.TextBox FY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Guardar;
    }
}