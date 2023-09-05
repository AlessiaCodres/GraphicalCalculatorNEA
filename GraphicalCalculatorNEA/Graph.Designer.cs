namespace GraphicalCalculatorNEA
{
    partial class Graph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Graph));
            label1 = new Label();
            btSettingsG = new Button();
            btHelpG = new Button();
            slctFunc4 = new CheckBox();
            txtFunc5 = new TextBox();
            txtFunc4 = new TextBox();
            txtFunc3 = new TextBox();
            lby2 = new Label();
            lby1 = new Label();
            lby3 = new Label();
            lby4 = new Label();
            lby5 = new Label();
            slctFunc3 = new CheckBox();
            slctFunc2 = new CheckBox();
            slctFunc1 = new CheckBox();
            slctFunc5 = new CheckBox();
            txtFunc2 = new TextBox();
            txtFunc1 = new TextBox();
            btExitG = new Button();
            pbGraph = new PictureBox();
            pbZoomIn = new PictureBox();
            pbZoomOut = new PictureBox();
            pbLeft = new PictureBox();
            pbDown = new PictureBox();
            pbUp = new PictureBox();
            pbRight = new PictureBox();
            Func1Colour = new Button();
            Func5Colour = new Button();
            Func4Colour = new Button();
            Func3Colour = new Button();
            Func2Colour = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            colorDialog1 = new ColorDialog();
            pbCursor = new PictureBox();
            lbFunc1Info = new ListBox();
            lbFunc2Info = new ListBox();
            lbFunc3Info = new ListBox();
            lbFunc4Info = new ListBox();
            lbFunc5Info = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pbGraph).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbZoomIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbZoomOut).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbUp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCursor).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(16, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 37);
            label1.TabIndex = 0;
            label1.Text = "Graphing";
            // 
            // btSettingsG
            // 
            btSettingsG.Location = new Point(729, 9);
            btSettingsG.Name = "btSettingsG";
            btSettingsG.Size = new Size(95, 43);
            btSettingsG.TabIndex = 4;
            btSettingsG.Text = "Settings";
            btSettingsG.UseVisualStyleBackColor = true;
            btSettingsG.Click += btSettingsG_Click;
            // 
            // btHelpG
            // 
            btHelpG.Location = new Point(830, 8);
            btHelpG.Name = "btHelpG";
            btHelpG.Size = new Size(95, 44);
            btHelpG.TabIndex = 5;
            btHelpG.Text = "Help";
            btHelpG.UseVisualStyleBackColor = true;
            btHelpG.Click += btHelpG_Click;
            // 
            // slctFunc4
            // 
            slctFunc4.AutoSize = true;
            slctFunc4.Location = new Point(211, 430);
            slctFunc4.Name = "slctFunc4";
            slctFunc4.Size = new Size(15, 14);
            slctFunc4.TabIndex = 40;
            slctFunc4.UseVisualStyleBackColor = true;
            slctFunc4.CheckedChanged += slctFunc4_CheckedChanged;
            // 
            // txtFunc5
            // 
            txtFunc5.Location = new Point(36, 540);
            txtFunc5.Name = "txtFunc5";
            txtFunc5.Size = new Size(170, 23);
            txtFunc5.TabIndex = 34;
            txtFunc5.TextChanged += txtFunc5_TextChanged;
            // 
            // txtFunc4
            // 
            txtFunc4.Location = new Point(36, 425);
            txtFunc4.Name = "txtFunc4";
            txtFunc4.Size = new Size(170, 23);
            txtFunc4.TabIndex = 33;
            txtFunc4.TextChanged += txtFunc4_TextChanged;
            // 
            // txtFunc3
            // 
            txtFunc3.Location = new Point(36, 310);
            txtFunc3.Name = "txtFunc3";
            txtFunc3.Size = new Size(170, 23);
            txtFunc3.TabIndex = 32;
            txtFunc3.TextChanged += txtFunc3_TextChanged;
            // 
            // lby2
            // 
            lby2.AutoSize = true;
            lby2.Location = new Point(16, 198);
            lby2.Name = "lby2";
            lby2.Size = new Size(24, 15);
            lby2.TabIndex = 30;
            lby2.Text = "y =";
            // 
            // lby1
            // 
            lby1.AutoSize = true;
            lby1.Location = new Point(16, 83);
            lby1.Name = "lby1";
            lby1.Size = new Size(24, 15);
            lby1.TabIndex = 26;
            lby1.Text = "y =";
            // 
            // lby3
            // 
            lby3.AutoSize = true;
            lby3.Location = new Point(16, 313);
            lby3.Name = "lby3";
            lby3.Size = new Size(24, 15);
            lby3.TabIndex = 41;
            lby3.Text = "y =";
            // 
            // lby4
            // 
            lby4.AutoSize = true;
            lby4.Location = new Point(16, 428);
            lby4.Name = "lby4";
            lby4.Size = new Size(24, 15);
            lby4.TabIndex = 42;
            lby4.Text = "y =";
            // 
            // lby5
            // 
            lby5.AutoSize = true;
            lby5.Location = new Point(16, 543);
            lby5.Name = "lby5";
            lby5.Size = new Size(24, 15);
            lby5.TabIndex = 43;
            lby5.Text = "y =";
            // 
            // slctFunc3
            // 
            slctFunc3.AutoSize = true;
            slctFunc3.Location = new Point(211, 315);
            slctFunc3.Name = "slctFunc3";
            slctFunc3.Size = new Size(15, 14);
            slctFunc3.TabIndex = 44;
            slctFunc3.UseVisualStyleBackColor = true;
            slctFunc3.CheckedChanged += slctFunc3_CheckedChanged;
            // 
            // slctFunc2
            // 
            slctFunc2.AutoSize = true;
            slctFunc2.Location = new Point(211, 200);
            slctFunc2.Name = "slctFunc2";
            slctFunc2.Size = new Size(15, 14);
            slctFunc2.TabIndex = 45;
            slctFunc2.UseVisualStyleBackColor = true;
            slctFunc2.CheckedChanged += slctFunc2_CheckedChanged;
            // 
            // slctFunc1
            // 
            slctFunc1.AutoSize = true;
            slctFunc1.Location = new Point(211, 85);
            slctFunc1.Name = "slctFunc1";
            slctFunc1.Size = new Size(15, 14);
            slctFunc1.TabIndex = 46;
            slctFunc1.UseVisualStyleBackColor = true;
            slctFunc1.CheckedChanged += slctFunc1_CheckedChanged;
            // 
            // slctFunc5
            // 
            slctFunc5.AutoSize = true;
            slctFunc5.Location = new Point(211, 545);
            slctFunc5.Name = "slctFunc5";
            slctFunc5.Size = new Size(15, 14);
            slctFunc5.TabIndex = 47;
            slctFunc5.UseVisualStyleBackColor = true;
            slctFunc5.CheckedChanged += slctFunc5_CheckedChanged;
            // 
            // txtFunc2
            // 
            txtFunc2.Location = new Point(36, 195);
            txtFunc2.Name = "txtFunc2";
            txtFunc2.Size = new Size(170, 23);
            txtFunc2.TabIndex = 48;
            txtFunc2.TextChanged += txtFunc2_TextChanged;
            // 
            // txtFunc1
            // 
            txtFunc1.Location = new Point(36, 80);
            txtFunc1.Name = "txtFunc1";
            txtFunc1.Size = new Size(170, 23);
            txtFunc1.TabIndex = 49;
            txtFunc1.TextChanged += txtFunc1_TextChanged;
            // 
            // btExitG
            // 
            btExitG.Location = new Point(931, 9);
            btExitG.Name = "btExitG";
            btExitG.Size = new Size(95, 44);
            btExitG.TabIndex = 50;
            btExitG.Text = "Exit";
            btExitG.UseVisualStyleBackColor = true;
            btExitG.Click += btExitG_Click;
            // 
            // pbGraph
            // 
            pbGraph.BorderStyle = BorderStyle.FixedSingle;
            pbGraph.Location = new Point(369, 65);
            pbGraph.Name = "pbGraph";
            pbGraph.Size = new Size(657, 601);
            pbGraph.TabIndex = 53;
            pbGraph.TabStop = false;
            pbGraph.Click += pbGraph_Click;
            pbGraph.MouseHover += pbGraph_MouseHover;
            // 
            // pbZoomIn
            // 
            pbZoomIn.BorderStyle = BorderStyle.Fixed3D;
            pbZoomIn.Image = (Image)resources.GetObject("pbZoomIn.Image");
            pbZoomIn.Location = new Point(479, 16);
            pbZoomIn.Name = "pbZoomIn";
            pbZoomIn.Size = new Size(30, 30);
            pbZoomIn.SizeMode = PictureBoxSizeMode.StretchImage;
            pbZoomIn.TabIndex = 54;
            pbZoomIn.TabStop = false;
            pbZoomIn.Click += pbZoomIn_Click;
            // 
            // pbZoomOut
            // 
            pbZoomOut.BorderStyle = BorderStyle.Fixed3D;
            pbZoomOut.Image = Properties.Resources.zoom_out;
            pbZoomOut.Location = new Point(515, 16);
            pbZoomOut.Name = "pbZoomOut";
            pbZoomOut.Size = new Size(30, 30);
            pbZoomOut.SizeMode = PictureBoxSizeMode.StretchImage;
            pbZoomOut.TabIndex = 55;
            pbZoomOut.TabStop = false;
            pbZoomOut.Click += pbZoomOut_Click;
            // 
            // pbLeft
            // 
            pbLeft.BorderStyle = BorderStyle.Fixed3D;
            pbLeft.Image = Properties.Resources.arrow;
            pbLeft.Location = new Point(637, 16);
            pbLeft.Name = "pbLeft";
            pbLeft.Size = new Size(30, 30);
            pbLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLeft.TabIndex = 57;
            pbLeft.TabStop = false;
            pbLeft.Click += pbLeft_Click;
            // 
            // pbDown
            // 
            pbDown.BorderStyle = BorderStyle.Fixed3D;
            pbDown.Image = Properties.Resources.arrowdown;
            pbDown.Location = new Point(601, 16);
            pbDown.Name = "pbDown";
            pbDown.Size = new Size(30, 30);
            pbDown.SizeMode = PictureBoxSizeMode.StretchImage;
            pbDown.TabIndex = 58;
            pbDown.TabStop = false;
            pbDown.Click += pbDown_Click;
            // 
            // pbUp
            // 
            pbUp.BorderStyle = BorderStyle.Fixed3D;
            pbUp.Image = Properties.Resources.arrowup;
            pbUp.Location = new Point(565, 16);
            pbUp.Name = "pbUp";
            pbUp.Size = new Size(30, 30);
            pbUp.SizeMode = PictureBoxSizeMode.StretchImage;
            pbUp.TabIndex = 59;
            pbUp.TabStop = false;
            pbUp.Click += pbUp_Click;
            // 
            // pbRight
            // 
            pbRight.BorderStyle = BorderStyle.Fixed3D;
            pbRight.Image = Properties.Resources.arrowright;
            pbRight.Location = new Point(673, 16);
            pbRight.Name = "pbRight";
            pbRight.Size = new Size(30, 30);
            pbRight.SizeMode = PictureBoxSizeMode.StretchImage;
            pbRight.TabIndex = 60;
            pbRight.TabStop = false;
            pbRight.Click += pbRight_Click;
            // 
            // Func1Colour
            // 
            Func1Colour.BackColor = Color.DodgerBlue;
            Func1Colour.Location = new Point(324, 80);
            Func1Colour.Name = "Func1Colour";
            Func1Colour.Size = new Size(23, 23);
            Func1Colour.TabIndex = 61;
            Func1Colour.UseVisualStyleBackColor = false;
            Func1Colour.Click += Func1Colour_Click;
            // 
            // Func5Colour
            // 
            Func5Colour.BackColor = Color.Cyan;
            Func5Colour.Location = new Point(324, 540);
            Func5Colour.Name = "Func5Colour";
            Func5Colour.Size = new Size(23, 23);
            Func5Colour.TabIndex = 62;
            Func5Colour.UseVisualStyleBackColor = false;
            Func5Colour.Click += Func5Colour_Click;
            // 
            // Func4Colour
            // 
            Func4Colour.BackColor = Color.Magenta;
            Func4Colour.Location = new Point(324, 425);
            Func4Colour.Name = "Func4Colour";
            Func4Colour.Size = new Size(23, 23);
            Func4Colour.TabIndex = 63;
            Func4Colour.UseVisualStyleBackColor = false;
            Func4Colour.Click += Func4Colour_Click;
            // 
            // Func3Colour
            // 
            Func3Colour.BackColor = Color.Green;
            Func3Colour.Location = new Point(324, 310);
            Func3Colour.Name = "Func3Colour";
            Func3Colour.Size = new Size(23, 23);
            Func3Colour.TabIndex = 64;
            Func3Colour.UseVisualStyleBackColor = false;
            Func3Colour.Click += Func3Colour_Click;
            // 
            // Func2Colour
            // 
            Func2Colour.BackColor = Color.Red;
            Func2Colour.Location = new Point(324, 195);
            Func2Colour.Name = "Func2Colour";
            Func2Colour.Size = new Size(23, 23);
            Func2Colour.TabIndex = 65;
            Func2Colour.UseVisualStyleBackColor = false;
            Func2Colour.Click += Func2Colour_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(232, 84);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 66;
            label2.Text = "Choose Colour:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(232, 198);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 67;
            label3.Text = "Choose Colour:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(232, 313);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 68;
            label4.Text = "Choose Colour:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(232, 429);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 69;
            label5.Text = "Choose Colour:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(232, 543);
            label6.Name = "label6";
            label6.Size = new Size(89, 15);
            label6.TabIndex = 70;
            label6.Text = "Choose Colour:";
            // 
            // pbCursor
            // 
            pbCursor.BorderStyle = BorderStyle.Fixed3D;
            pbCursor.Image = Properties.Resources.mousecursor2;
            pbCursor.Location = new Point(443, 16);
            pbCursor.Name = "pbCursor";
            pbCursor.Size = new Size(30, 30);
            pbCursor.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCursor.TabIndex = 71;
            pbCursor.TabStop = false;
            pbCursor.Click += pbCursor_Click;
            // 
            // lbFunc1Info
            // 
            lbFunc1Info.FormattingEnabled = true;
            lbFunc1Info.ItemHeight = 15;
            lbFunc1Info.Location = new Point(17, 109);
            lbFunc1Info.Name = "lbFunc1Info";
            lbFunc1Info.Size = new Size(330, 79);
            lbFunc1Info.TabIndex = 77;
            // 
            // lbFunc2Info
            // 
            lbFunc2Info.FormattingEnabled = true;
            lbFunc2Info.ItemHeight = 15;
            lbFunc2Info.Location = new Point(17, 224);
            lbFunc2Info.Name = "lbFunc2Info";
            lbFunc2Info.Size = new Size(330, 79);
            lbFunc2Info.TabIndex = 78;
            // 
            // lbFunc3Info
            // 
            lbFunc3Info.FormattingEnabled = true;
            lbFunc3Info.ItemHeight = 15;
            lbFunc3Info.Location = new Point(17, 339);
            lbFunc3Info.Name = "lbFunc3Info";
            lbFunc3Info.Size = new Size(330, 79);
            lbFunc3Info.TabIndex = 79;
            // 
            // lbFunc4Info
            // 
            lbFunc4Info.FormattingEnabled = true;
            lbFunc4Info.ItemHeight = 15;
            lbFunc4Info.Location = new Point(17, 454);
            lbFunc4Info.Name = "lbFunc4Info";
            lbFunc4Info.Size = new Size(330, 79);
            lbFunc4Info.TabIndex = 80;
            // 
            // lbFunc5Info
            // 
            lbFunc5Info.FormattingEnabled = true;
            lbFunc5Info.ItemHeight = 15;
            lbFunc5Info.Location = new Point(17, 569);
            lbFunc5Info.Name = "lbFunc5Info";
            lbFunc5Info.Size = new Size(330, 79);
            lbFunc5Info.TabIndex = 81;
            // 
            // Graph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1034, 678);
            Controls.Add(lbFunc5Info);
            Controls.Add(lbFunc4Info);
            Controls.Add(lbFunc3Info);
            Controls.Add(lbFunc2Info);
            Controls.Add(lbFunc1Info);
            Controls.Add(pbCursor);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(Func2Colour);
            Controls.Add(Func3Colour);
            Controls.Add(Func4Colour);
            Controls.Add(Func5Colour);
            Controls.Add(Func1Colour);
            Controls.Add(pbRight);
            Controls.Add(pbUp);
            Controls.Add(pbDown);
            Controls.Add(pbLeft);
            Controls.Add(pbZoomOut);
            Controls.Add(pbZoomIn);
            Controls.Add(pbGraph);
            Controls.Add(btExitG);
            Controls.Add(txtFunc1);
            Controls.Add(txtFunc2);
            Controls.Add(slctFunc5);
            Controls.Add(slctFunc1);
            Controls.Add(slctFunc2);
            Controls.Add(slctFunc3);
            Controls.Add(slctFunc4);
            Controls.Add(txtFunc5);
            Controls.Add(txtFunc4);
            Controls.Add(txtFunc3);
            Controls.Add(lby2);
            Controls.Add(lby1);
            Controls.Add(btHelpG);
            Controls.Add(btSettingsG);
            Controls.Add(label1);
            Controls.Add(lby3);
            Controls.Add(lby4);
            Controls.Add(lby5);
            Name = "Graph";
            Text = "Graph";
            Load += Graph_Load;
            ResizeEnd += Graph_ResizeEnd;
            Resize += Graph_Resize;
            ((System.ComponentModel.ISupportInitialize)pbGraph).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbZoomIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbZoomOut).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbUp).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCursor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btSettingsG;
        private Button btHelpG;
        private CheckBox slctFunc4;
        private TextBox txtFunc5;
        private TextBox txtFunc4;
        private TextBox txtFunc3;
        private Label lby2;
        private Label lby1;
        private Label lby3;
        private Label lby4;
        private Label lby5;
        private CheckBox slctFunc3;
        private CheckBox slctFunc2;
        private CheckBox slctFunc1;
        private CheckBox slctFunc5;
        private TextBox txtFunc2;
        private TextBox txtFunc1;
        private Button btExitG;
        private PictureBox pbGraph;
        private PictureBox pbZoomIn;
        private PictureBox pbZoomOut;
        private PictureBox pbLeft;
        private PictureBox pbDown;
        private PictureBox pbUp;
        private PictureBox pbRight;
        private Button Func1Colour;
        private Button Func5Colour;
        private Button Func4Colour;
        private Button Func3Colour;
        private Button Func2Colour;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ColorDialog colorDialog1;
        private PictureBox pbCursor;
        private ListBox lbFunc1Info;
        private ListBox lbFunc2Info;
        private ListBox lbFunc3Info;
        private ListBox lbFunc4Info;
        private ListBox lbFunc5Info;
    }
}