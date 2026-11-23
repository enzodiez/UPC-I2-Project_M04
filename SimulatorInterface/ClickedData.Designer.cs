namespace SimulatorInterface
{
    partial class ClickedData
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
            this.fpView = new System.Windows.Forms.DataGridView();
            this.returnBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fpView)).BeginInit();
            this.SuspendLayout();
            // 
            // fpView
            // 
            this.fpView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fpView.Location = new System.Drawing.Point(28, 25);
            this.fpView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.fpView.Name = "fpView";
            this.fpView.RowHeadersWidth = 62;
            this.fpView.Size = new System.Drawing.Size(1168, 221);
            this.fpView.TabIndex = 0;
            // 
            // returnBut
            // 
            this.returnBut.Location = new System.Drawing.Point(475, 312);
            this.returnBut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.returnBut.Name = "returnBut";
            this.returnBut.Size = new System.Drawing.Size(279, 35);
            this.returnBut.TabIndex = 1;
            this.returnBut.Text = "Return To Main Interface";
            this.returnBut.UseVisualStyleBackColor = true;
            this.returnBut.Click += new System.EventHandler(this.returnBut_Click);
            // 
            // ClickedData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 397);
            this.Controls.Add(this.returnBut);
            this.Controls.Add(this.fpView);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ClickedData";
            this.Text = "Flight Plan Data";
            this.Load += new System.EventHandler(this.ClickedData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView fpView;
        private System.Windows.Forms.Button returnBut;
    }
}