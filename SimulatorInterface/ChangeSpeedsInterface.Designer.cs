namespace SimulatorInterface
{
    partial class ChangeSpeedsInterface
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
            this.SpeedsGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SpeedsGrid
            // 
            this.SpeedsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SpeedsGrid.Location = new System.Drawing.Point(12, 12);
            this.SpeedsGrid.Name = "SpeedsGrid";
            this.SpeedsGrid.RowTemplate.Height = 28;
            this.SpeedsGrid.Size = new System.Drawing.Size(423, 534);
            this.SpeedsGrid.TabIndex = 0;
            this.SpeedsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SpeedsGrid_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(570, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Set new speed for flight:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(574, 195);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 26);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(609, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 44);
            this.button1.TabIndex = 3;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(609, 502);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 44);
            this.button2.TabIndex = 4;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChangeSpeedsInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 598);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpeedsGrid);
            this.Name = "ChangeSpeedsInterface";
            this.Text = "ChangeSpeedsInterface";
            this.Load += new System.EventHandler(this.ChangeSpeedsInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView SpeedsGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}