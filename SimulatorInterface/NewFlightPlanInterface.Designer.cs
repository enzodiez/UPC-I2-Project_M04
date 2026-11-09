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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.airline = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.fileselectorBtn = new System.Windows.Forms.Button();
            this.routeBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBarLoad = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // FX
            // 
            this.FX.Location = new System.Drawing.Point(174, 394);
            this.FX.Name = "FX";
            this.FX.Size = new System.Drawing.Size(130, 26);
            this.FX.TabIndex = 0;
            // 
            // Speed
            // 
            this.Speed.Location = new System.Drawing.Point(174, 478);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(130, 26);
            this.Speed.TabIndex = 1;
            // 
            // IY
            // 
            this.IY.Location = new System.Drawing.Point(356, 286);
            this.IY.Name = "IY";
            this.IY.Size = new System.Drawing.Size(132, 26);
            this.IY.TabIndex = 2;
            // 
            // IX
            // 
            this.IX.Location = new System.Drawing.Point(174, 286);
            this.IX.Name = "IX";
            this.IX.Size = new System.Drawing.Size(130, 26);
            this.IX.TabIndex = 3;
            // 
            // SpeedLbl
            // 
            this.SpeedLbl.AutoSize = true;
            this.SpeedLbl.Location = new System.Drawing.Point(27, 483);
            this.SpeedLbl.Name = "SpeedLbl";
            this.SpeedLbl.Size = new System.Drawing.Size(114, 20);
            this.SpeedLbl.TabIndex = 4;
            this.SpeedLbl.Text = "Ground Speed";
            // 
            // InitialPositionLbl
            // 
            this.InitialPositionLbl.AutoSize = true;
            this.InitialPositionLbl.Location = new System.Drawing.Point(27, 291);
            this.InitialPositionLbl.Name = "InitialPositionLbl";
            this.InitialPositionLbl.Size = new System.Drawing.Size(106, 20);
            this.InitialPositionLbl.TabIndex = 5;
            this.InitialPositionLbl.Text = "Initial Position";
            // 
            // CallSignLbl
            // 
            this.CallSignLbl.AutoSize = true;
            this.CallSignLbl.Location = new System.Drawing.Point(27, 175);
            this.CallSignLbl.Name = "CallSignLbl";
            this.CallSignLbl.Size = new System.Drawing.Size(71, 20);
            this.CallSignLbl.TabIndex = 6;
            this.CallSignLbl.Text = "Call Sign";
            // 
            // FinalPositionLbl
            // 
            this.FinalPositionLbl.AutoSize = true;
            this.FinalPositionLbl.Location = new System.Drawing.Point(30, 398);
            this.FinalPositionLbl.Name = "FinalPositionLbl";
            this.FinalPositionLbl.Size = new System.Drawing.Size(103, 20);
            this.FinalPositionLbl.TabIndex = 7;
            this.FinalPositionLbl.Text = "Final Position";
            // 
            // CallSign
            // 
            this.CallSign.Location = new System.Drawing.Point(174, 171);
            this.CallSign.Name = "CallSign";
            this.CallSign.Size = new System.Drawing.Size(130, 26);
            this.CallSign.TabIndex = 8;
            // 
            // FY
            // 
            this.FY.Location = new System.Drawing.Point(356, 394);
            this.FY.Name = "FY";
            this.FY.Size = new System.Drawing.Size(132, 26);
            this.FY.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "X";
            // 
            // Guardar
            // 
            this.Guardar.Location = new System.Drawing.Point(356, 449);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(134, 137);
            this.Guardar.TabIndex = 12;
            this.Guardar.Text = "Add Flight Plan";
            this.Guardar.UseVisualStyleBackColor = true;
            this.Guardar.Click += new System.EventHandler(this.Guardar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(540, 37);
            this.label3.TabIndex = 13;
            this.label3.Text = "FLIGHT PLAN CREATION PANEL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Airline";
            // 
            // airline
            // 
            this.airline.Location = new System.Drawing.Point(448, 171);
            this.airline.Name = "airline";
            this.airline.Size = new System.Drawing.Size(132, 26);
            this.airline.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(411, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 560);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Altitude (FL)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(174, 555);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(130, 26);
            this.textBox2.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "Enter Flight Plan data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(30, 628);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 25);
            this.label9.TabIndex = 21;
            this.label9.Text = "Load data from file";
            // 
            // fileselectorBtn
            // 
            this.fileselectorBtn.Location = new System.Drawing.Point(34, 680);
            this.fileselectorBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fileselectorBtn.Name = "fileselectorBtn";
            this.fileselectorBtn.Size = new System.Drawing.Size(147, 152);
            this.fileselectorBtn.TabIndex = 22;
            this.fileselectorBtn.Text = "Select File";
            this.fileselectorBtn.UseVisualStyleBackColor = true;
            this.fileselectorBtn.Click += new System.EventHandler(this.fileselectorBtn_Click);
            // 
            // routeBox
            // 
            this.routeBox.Location = new System.Drawing.Point(234, 705);
            this.routeBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.routeBox.Name = "routeBox";
            this.routeBox.Size = new System.Drawing.Size(346, 26);
            this.routeBox.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(230, 680);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 20);
            this.label10.TabIndex = 24;
            this.label10.Text = "Route of selected file:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(230, 755);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 20);
            this.label11.TabIndex = 25;
            this.label11.Text = "INFORMATION";
            // 
            // progressBarLoad
            // 
            this.progressBarLoad.Location = new System.Drawing.Point(234, 797);
            this.progressBarLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBarLoad.Name = "progressBarLoad";
            this.progressBarLoad.Size = new System.Drawing.Size(348, 35);
            this.progressBarLoad.TabIndex = 26;
            this.progressBarLoad.Visible = false;
            // 
            // NewFlightPlanInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 938);
            this.Controls.Add(this.progressBarLoad);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.routeBox);
            this.Controls.Add(this.fileselectorBtn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.airline);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox airline;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button fileselectorBtn;
        private System.Windows.Forms.TextBox routeBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar progressBarLoad;
    }
}