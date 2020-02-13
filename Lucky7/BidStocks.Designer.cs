namespace Lucky7
{
    partial class BidStocks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BidStocks));
            this.nudBid = new System.Windows.Forms.NumericUpDown();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblCoins = new System.Windows.Forms.Label();
            this.lblCurrentBid = new System.Windows.Forms.Label();
            this.btnPass = new System.Windows.Forms.Button();
            this.btnBid = new System.Windows.Forms.Button();
            this.lblAuction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudBid)).BeginInit();
            this.SuspendLayout();
            // 
            // nudBid
            // 
            this.nudBid.Location = new System.Drawing.Point(165, 105);
            this.nudBid.Name = "nudBid";
            this.nudBid.Size = new System.Drawing.Size(77, 25);
            this.nudBid.TabIndex = 0;
            this.nudBid.ValueChanged += new System.EventHandler(this.nudBid_ValueChanged);
            this.nudBid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudBid_KeyPress);
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(12, 49);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(70, 18);
            this.lblPlayerName.TabIndex = 30;
            this.lblPlayerName.Text = "Incognito";
            // 
            // lblCoins
            // 
            this.lblCoins.AutoSize = true;
            this.lblCoins.Location = new System.Drawing.Point(175, 49);
            this.lblCoins.Name = "lblCoins";
            this.lblCoins.Size = new System.Drawing.Size(67, 18);
            this.lblCoins.TabIndex = 31;
            this.lblCoins.Text = "Coins: 10";
            // 
            // lblCurrentBid
            // 
            this.lblCurrentBid.AutoSize = true;
            this.lblCurrentBid.Location = new System.Drawing.Point(12, 107);
            this.lblCurrentBid.Name = "lblCurrentBid";
            this.lblCurrentBid.Size = new System.Drawing.Size(102, 18);
            this.lblCurrentBid.TabIndex = 32;
            this.lblCurrentBid.Text = "Current Bid: 1";
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(10, 161);
            this.btnPass.Margin = new System.Windows.Forms.Padding(4);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(112, 31);
            this.btnPass.TabIndex = 33;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // btnBid
            // 
            this.btnBid.Location = new System.Drawing.Point(130, 161);
            this.btnBid.Margin = new System.Windows.Forms.Padding(4);
            this.btnBid.Name = "btnBid";
            this.btnBid.Size = new System.Drawing.Size(112, 31);
            this.btnBid.TabIndex = 34;
            this.btnBid.Text = "Bid";
            this.btnBid.UseVisualStyleBackColor = true;
            this.btnBid.Click += new System.EventHandler(this.btnBid_Click);
            // 
            // lblAuction
            // 
            this.lblAuction.AutoSize = true;
            this.lblAuction.Location = new System.Drawing.Point(70, 9);
            this.lblAuction.Name = "lblAuction";
            this.lblAuction.Size = new System.Drawing.Size(63, 18);
            this.lblAuction.TabIndex = 35;
            this.lblAuction.Text = "Auction:";
            // 
            // BidStocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 245);
            this.ControlBox = false;
            this.Controls.Add(this.lblAuction);
            this.Controls.Add(this.btnBid);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.lblCurrentBid);
            this.Controls.Add(this.lblCoins);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.nudBid);
            this.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BidStocks";
            this.Text = "BidStocks";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nudBid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudBid;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblCoins;
        private System.Windows.Forms.Label lblCurrentBid;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnBid;
        private System.Windows.Forms.Label lblAuction;
    }
}