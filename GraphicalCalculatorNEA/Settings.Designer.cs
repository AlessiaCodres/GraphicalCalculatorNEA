namespace GraphicalCalculatorNEA
{
    partial class Settings
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
            this.lbTitleS = new System.Windows.Forms.Label();
            this.btCloseS = new System.Windows.Forms.Button();
            this.btHelpS = new System.Windows.Forms.Button();
            this.lbViewWin = new System.Windows.Forms.Label();
            this.lbMinX = new System.Windows.Forms.Label();
            this.lbMaxX = new System.Windows.Forms.Label();
            this.lbMinY = new System.Windows.Forms.Label();
            this.lbMaxY = new System.Windows.Forms.Label();
            this.lbAngle = new System.Windows.Forms.Label();
            this.rbtRadians = new System.Windows.Forms.RadioButton();
            this.rbtDegrees = new System.Windows.Forms.RadioButton();
            this.tbxMinX = new System.Windows.Forms.TextBox();
            this.tbxMaxY = new System.Windows.Forms.TextBox();
            this.tbxMinY = new System.Windows.Forms.TextBox();
            this.tbxMaxX = new System.Windows.Forms.TextBox();
            this.lbInvalid = new System.Windows.Forms.Label();
            this.lbRejectClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTitleS
            // 
            this.lbTitleS.AutoSize = true;
            this.lbTitleS.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbTitleS.Location = new System.Drawing.Point(12, 9);
            this.lbTitleS.Name = "lbTitleS";
            this.lbTitleS.Size = new System.Drawing.Size(112, 37);
            this.lbTitleS.TabIndex = 0;
            this.lbTitleS.Text = "Settings";
            // 
            // btCloseS
            // 
            this.btCloseS.Location = new System.Drawing.Point(12, 263);
            this.btCloseS.Name = "btCloseS";
            this.btCloseS.Size = new System.Drawing.Size(132, 47);
            this.btCloseS.TabIndex = 1;
            this.btCloseS.Text = "Close Settings";
            this.btCloseS.UseVisualStyleBackColor = true;
            this.btCloseS.Click += new System.EventHandler(this.btCloseS_Click);
            // 
            // btHelpS
            // 
            this.btHelpS.Location = new System.Drawing.Point(305, 263);
            this.btHelpS.Name = "btHelpS";
            this.btHelpS.Size = new System.Drawing.Size(132, 47);
            this.btHelpS.TabIndex = 2;
            this.btHelpS.Text = "Help";
            this.btHelpS.UseVisualStyleBackColor = true;
            this.btHelpS.Click += new System.EventHandler(this.btHelpS_Click);
            // 
            // lbViewWin
            // 
            this.lbViewWin.AutoSize = true;
            this.lbViewWin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbViewWin.Location = new System.Drawing.Point(83, 73);
            this.lbViewWin.Name = "lbViewWin";
            this.lbViewWin.Size = new System.Drawing.Size(169, 21);
            this.lbViewWin.TabIndex = 3;
            this.lbViewWin.Text = "View Window Settings:";
            // 
            // lbMinX
            // 
            this.lbMinX.AutoSize = true;
            this.lbMinX.Location = new System.Drawing.Point(90, 114);
            this.lbMinX.Name = "lbMinX";
            this.lbMinX.Size = new System.Drawing.Size(41, 15);
            this.lbMinX.TabIndex = 4;
            this.lbMinX.Text = "Min X:";
            // 
            // lbMaxX
            // 
            this.lbMaxX.AutoSize = true;
            this.lbMaxX.Location = new System.Drawing.Point(234, 114);
            this.lbMaxX.Name = "lbMaxX";
            this.lbMaxX.Size = new System.Drawing.Size(43, 15);
            this.lbMaxX.TabIndex = 5;
            this.lbMaxX.Text = "Max X:";
            // 
            // lbMinY
            // 
            this.lbMinY.AutoSize = true;
            this.lbMinY.Location = new System.Drawing.Point(90, 155);
            this.lbMinY.Name = "lbMinY";
            this.lbMinY.Size = new System.Drawing.Size(41, 15);
            this.lbMinY.TabIndex = 6;
            this.lbMinY.Text = "Min Y:";
            // 
            // lbMaxY
            // 
            this.lbMaxY.AutoSize = true;
            this.lbMaxY.Location = new System.Drawing.Point(234, 155);
            this.lbMaxY.Name = "lbMaxY";
            this.lbMaxY.Size = new System.Drawing.Size(43, 15);
            this.lbMaxY.TabIndex = 7;
            this.lbMaxY.Text = "Max Y:";
            // 
            // lbAngle
            // 
            this.lbAngle.AutoSize = true;
            this.lbAngle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbAngle.Location = new System.Drawing.Point(83, 194);
            this.lbAngle.Name = "lbAngle";
            this.lbAngle.Size = new System.Drawing.Size(89, 21);
            this.lbAngle.TabIndex = 9;
            this.lbAngle.Text = "Angle Type:";
            // 
            // rbtRadians
            // 
            this.rbtRadians.AutoSize = true;
            this.rbtRadians.Location = new System.Drawing.Point(192, 196);
            this.rbtRadians.Name = "rbtRadians";
            this.rbtRadians.Size = new System.Drawing.Size(66, 19);
            this.rbtRadians.TabIndex = 10;
            this.rbtRadians.TabStop = true;
            this.rbtRadians.Text = "Radians";
            this.rbtRadians.UseVisualStyleBackColor = true;
            this.rbtRadians.CheckedChanged += new System.EventHandler(this.rbtRadians_CheckedChanged);
            // 
            // rbtDegrees
            // 
            this.rbtDegrees.AutoSize = true;
            this.rbtDegrees.Location = new System.Drawing.Point(288, 196);
            this.rbtDegrees.Name = "rbtDegrees";
            this.rbtDegrees.Size = new System.Drawing.Size(67, 19);
            this.rbtDegrees.TabIndex = 11;
            this.rbtDegrees.TabStop = true;
            this.rbtDegrees.Text = "Degrees";
            this.rbtDegrees.UseVisualStyleBackColor = true;
            // 
            // tbxMinX
            // 
            this.tbxMinX.Location = new System.Drawing.Point(137, 111);
            this.tbxMinX.Name = "tbxMinX";
            this.tbxMinX.Size = new System.Drawing.Size(75, 23);
            this.tbxMinX.TabIndex = 12;
            this.tbxMinX.TextChanged += new System.EventHandler(this.tbxMinX_TextChanged);
            // 
            // tbxMaxY
            // 
            this.tbxMaxY.Location = new System.Drawing.Point(288, 152);
            this.tbxMaxY.Name = "tbxMaxY";
            this.tbxMaxY.Size = new System.Drawing.Size(75, 23);
            this.tbxMaxY.TabIndex = 13;
            this.tbxMaxY.TextChanged += new System.EventHandler(this.tbxMaxY_TextChanged);
            // 
            // tbxMinY
            // 
            this.tbxMinY.Location = new System.Drawing.Point(137, 152);
            this.tbxMinY.Name = "tbxMinY";
            this.tbxMinY.Size = new System.Drawing.Size(75, 23);
            this.tbxMinY.TabIndex = 14;
            this.tbxMinY.TextChanged += new System.EventHandler(this.tbxMinY_TextChanged);
            // 
            // tbxMaxX
            // 
            this.tbxMaxX.Location = new System.Drawing.Point(288, 111);
            this.tbxMaxX.Name = "tbxMaxX";
            this.tbxMaxX.Size = new System.Drawing.Size(75, 23);
            this.tbxMaxX.TabIndex = 15;
            this.tbxMaxX.TextChanged += new System.EventHandler(this.tbxMaxX_TextChanged);
            // 
            // lbInvalid
            // 
            this.lbInvalid.AutoSize = true;
            this.lbInvalid.ForeColor = System.Drawing.Color.Red;
            this.lbInvalid.Location = new System.Drawing.Point(144, 9);
            this.lbInvalid.Name = "lbInvalid";
            this.lbInvalid.Size = new System.Drawing.Size(0, 15);
            this.lbInvalid.TabIndex = 16;
            // 
            // lbRejectClose
            // 
            this.lbRejectClose.AutoSize = true;
            this.lbRejectClose.ForeColor = System.Drawing.Color.Red;
            this.lbRejectClose.Location = new System.Drawing.Point(144, 31);
            this.lbRejectClose.Name = "lbRejectClose";
            this.lbRejectClose.Size = new System.Drawing.Size(0, 15);
            this.lbRejectClose.TabIndex = 17;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 323);
            this.Controls.Add(this.lbRejectClose);
            this.Controls.Add(this.lbInvalid);
            this.Controls.Add(this.tbxMaxX);
            this.Controls.Add(this.tbxMinY);
            this.Controls.Add(this.tbxMaxY);
            this.Controls.Add(this.tbxMinX);
            this.Controls.Add(this.rbtDegrees);
            this.Controls.Add(this.rbtRadians);
            this.Controls.Add(this.lbAngle);
            this.Controls.Add(this.lbMaxY);
            this.Controls.Add(this.lbMinY);
            this.Controls.Add(this.lbMaxX);
            this.Controls.Add(this.lbMinX);
            this.Controls.Add(this.lbViewWin);
            this.Controls.Add(this.btHelpS);
            this.Controls.Add(this.btCloseS);
            this.Controls.Add(this.lbTitleS);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.lbSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbTitleS;
        private Button btCloseS;
        private Button btHelpS;
        private Label lbViewWin;
        private Label lbMinX;
        private Label lbMaxX;
        private Label lbMinY;
        private Label lbMaxY;
        private Label lbAngle;
        private RadioButton rbtRadians;
        private RadioButton rbtDegrees;
        private TextBox tbxMinX;
        private TextBox tbxMaxY;
        private TextBox tbxMinY;
        private TextBox tbxMaxX;
        private Label lbInvalid;
        private Label lbRejectClose;
    }
}