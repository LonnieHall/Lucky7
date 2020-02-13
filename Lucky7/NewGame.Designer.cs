namespace Lucky7
{
    partial class NewGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGame));
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.btnAddHuman = new System.Windows.Forms.Button();
            this.btnAddAI = new System.Windows.Forms.Button();
            this.dgvPlayerGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(20, 38);
            this.txtPlayerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(172, 25);
            this.txtPlayerName.TabIndex = 0;
            // 
            // btnAddHuman
            // 
            this.btnAddHuman.Location = new System.Drawing.Point(199, 38);
            this.btnAddHuman.Name = "btnAddHuman";
            this.btnAddHuman.Size = new System.Drawing.Size(101, 23);
            this.btnAddHuman.TabIndex = 1;
            this.btnAddHuman.Text = "Add Human";
            this.btnAddHuman.UseVisualStyleBackColor = true;
            this.btnAddHuman.Click += new System.EventHandler(this.AddHuman_Click);
            // 
            // btnAddAI
            // 
            this.btnAddAI.Location = new System.Drawing.Point(199, 67);
            this.btnAddAI.Name = "btnAddAI";
            this.btnAddAI.Size = new System.Drawing.Size(101, 23);
            this.btnAddAI.TabIndex = 2;
            this.btnAddAI.Text = "Add AI";
            this.btnAddAI.UseVisualStyleBackColor = true;
            this.btnAddAI.Click += new System.EventHandler(this.AddAI_Click);
            // 
            // dgvPlayerGrid
            // 
            this.dgvPlayerGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlayerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayerGrid.Location = new System.Drawing.Point(20, 114);
            this.dgvPlayerGrid.Name = "dgvPlayerGrid";
            this.dgvPlayerGrid.Size = new System.Drawing.Size(172, 186);
            this.dgvPlayerGrid.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Players";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(199, 228);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(101, 72);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 18);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name";
            // 
            // NewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 312);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPlayerGrid);
            this.Controls.Add(this.btnAddAI);
            this.Controls.Add(this.btnAddHuman);
            this.Controls.Add(this.txtPlayerName);
            this.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewGame";
            this.Text = "New Game";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayerGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnAddHuman;
        private System.Windows.Forms.Button btnAddAI;
        private System.Windows.Forms.DataGridView dgvPlayerGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblName;
    }
}