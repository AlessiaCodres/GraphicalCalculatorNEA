using Microsoft.VisualBasic.Devices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GraphicalCalculatorNEA
{
    public partial class Graph : Form
    {
        PointF[] CartPoints = new PointF[5000];
        PointF[] PixPoints = new PointF[5000];
        PointF[] CartPoints1;
        PointF[] CartPoints2;
        PointF[] CartPoints3;
        PointF[] CartPoints4;
        PointF[] CartPoints5;
        PointF[] PixPoints1;
        PointF[] PixPoints2;
        PointF[] PixPoints3;
        PointF[] PixPoints4;
        PointF[] PixPoints5;
        float xoffset;
        float yoffset;
        float xorigin;
        float yorigin;
        float MinX;
        float MaxX;
        float MinY;
        float MaxY;
        float xfactor;
        float yfactor;
        Graphics graphObj;
        Parser parser;
        string cursor = "default";
        bool error = false;
        public Graph()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 700);
        }
        private void SetOffset()
        {
            if ((MinX >= 0 && MinY >= 0) || (MinX >= 0 && MaxY <= 0) || (MaxX <= 0 && MaxY <= 0) || (MaxX <= 0 && MinY >= 0))
            {
                xoffset = 0;
                yoffset = 0;
                xorigin = MinX;
                yorigin = MaxY;
            }
            if (MinX <= 0 && MaxX >= 0 && MinY <= 0 && MaxY >= 0)
            {
                xoffset = -MinX * xfactor;
                yoffset = MaxY * yfactor;
                xorigin = 0;
                yorigin = 0;
            }
            if ((MinX >= 0 && MinY <= 0 && MaxY >= 0) || (MaxX <= 0 && MinY <= 0 && MaxY >= 0))
            {
                xoffset = 0;
                yoffset = MaxY * yfactor;
                xorigin = MinX;
                yorigin = 0;
            }
            if ((MinX <= 0 && MaxX >= 0 && MinY >= 0) || (MinX <= 0 && MaxX >= 0 && MaxY <= 0))
            {
                xoffset = -MinX * xfactor;
                yoffset = 0;
                xorigin = 0;
                yorigin = MaxY;
            }
        }
        private PointF CartToPix(float x, float y)
        {
            PointF pix = new PointF(0, 0);
            pix.X = xoffset + ((x - xorigin) * xfactor);
            pix.Y = yoffset + ((yorigin - y) * yfactor);
            return pix;
        }
        private PointF PixToCart(float x, float y)
        {
            PointF cart = new PointF(0, 0);
            cart.X = ((x - xoffset) / xfactor) + xorigin;
            cart.Y = yorigin - ((y - yoffset) / yfactor);
            return cart;
        }
        private void GetPointArray(string function)
        {
            SetOffset();
            float step = (MaxX - MinX) / 5000;
            for (int i = 0; i < CartPoints.Length; i++)
            {
                CartPoints[i].X = MinX + step * i;
            }
            for (int i = 0; i < PixPoints.Length; i++)
            {
                parser = new Parser(function);
                try
                {
                    double y = Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(CartPoints[i].X)).value), 3);
                    CartPoints[i].Y = (float)y;
                }
                catch
                {
                    MessageBox.Show(parser.Evaluate(parser.root, Convert.ToString(CartPoints[i].X)).value);
                    slctFunc1.Checked = false;
                    slctFunc2.Checked = false;
                    slctFunc3.Checked = false;
                    slctFunc4.Checked = false;
                    slctFunc5.Checked = false;
                    error = true;
                    break;
                }
                PixPoints[i].Y = CartToPix(CartPoints[i].X, CartPoints[i].Y).Y;
                PixPoints[i].X = CartToPix(CartPoints[i].X, CartPoints[i].Y).X;
            }
        }
        private void DrawAxes(float xgap, float ygap)
        {
            float xaxisPos = 0;
            float yaxisPos = 0;
            Pen blackpen = new Pen(Color.Black, 2);
            if (MinX <= 0 && MaxX >= 0)
            {
                graphObj.DrawLine(blackpen, -MinX * xfactor, 0, -MinX * xfactor, pbGraph.Height);
                yaxisPos = -MinX * xfactor;
            }
            else if (MaxX <= 0)
            {
                yaxisPos = pbGraph.Width;
            }
            else if (MinX >= 0)
            {
                yaxisPos = 0;
            }
            if (MinY <= 0 && MaxY >= 0)
            {
                graphObj.DrawLine(blackpen, 0, MaxY * yfactor, pbGraph.Width, MaxY * yfactor);
                xaxisPos = MaxY * yfactor;
            }
            else if (MaxY <= 0)
            {
                xaxisPos = 0;
            }
            else if (MinY >= 0)
            {
                xaxisPos = pbGraph.Height;
            }
            if (MinX < 0 && MaxX > 0 && MinY < 0 && MaxY > 0)
            {
                PointF point = new PointF(yaxisPos - 10, xaxisPos);
                Brush brush = new SolidBrush(Color.Black);
                graphObj.DrawString("0", new Font("Segoe UI", 8, FontStyle.Regular), brush, point);
            }
            for (float i = 5 * xgap; i < MaxX; i = i + 5 * xgap)
            {
                if (i > MinX)
                {
                    RectangleF rectangle;
                    float x = CartToPix(i, 0).X;
                    string str = Convert.ToString(i);
                    Font font = new Font("Segoe UI", 8, FontStyle.Regular);
                    SizeF stringSize = new SizeF(graphObj.MeasureString(str, font));
                    if (xaxisPos != pbGraph.Height)
                    {
                        rectangle = new RectangleF(x - (stringSize.Width / 2), xaxisPos, stringSize.Width, stringSize.Height);
                    }
                    else
                    {
                        rectangle = new RectangleF(x - (stringSize.Width / 2), xaxisPos - stringSize.Height - 2, stringSize.Width, stringSize.Height);
                    }
                    Brush brush = new SolidBrush(Color.Black);
                    graphObj.DrawString(str, font, brush, rectangle);
                }
            }
            for (float i = -5 * xgap; i > MinX; i = i - 5 * xgap)
            {
                if (i < MaxX)
                {
                    RectangleF rectangle;
                    float x = CartToPix(i, 0).X;
                    string str = Convert.ToString(i);
                    Font font = new Font("Segoe UI", 8, FontStyle.Regular);
                    SizeF stringSize = new SizeF(graphObj.MeasureString(str, font));
                    if (xaxisPos != pbGraph.Height)
                    {
                        rectangle = new RectangleF(x - (stringSize.Width / 2), xaxisPos, stringSize.Width, stringSize.Height);
                    }
                    else
                    {
                        rectangle = new RectangleF(x - (stringSize.Width / 2), xaxisPos - stringSize.Height - 2, stringSize.Width, stringSize.Height);
                    }
                    Brush brush = new SolidBrush(Color.Black);
                    graphObj.DrawString(str, font, brush, rectangle);
                }
            }
            for (float i = 5 * ygap; i < MaxY; i = i + 5 * ygap)
            {
                if (i > MinY)
                {
                    RectangleF rectangle;
                    float y = CartToPix(0, i).Y;
                    string str = Convert.ToString(i);
                    Font font = new Font("Segoe UI", 8, FontStyle.Regular);
                    SizeF stringSize = new SizeF(graphObj.MeasureString(str, font));
                    if (yaxisPos != 0)
                    {
                        rectangle = new RectangleF(yaxisPos - stringSize.Width - 2, y - 5, stringSize.Width, stringSize.Height);
                    }
                    else
                    {
                        rectangle = new RectangleF(yaxisPos, y - 5, stringSize.Width, stringSize.Height);
                    }
                    Brush brush = new SolidBrush(Color.Black);
                    graphObj.DrawString(str, font, brush, rectangle);
                }
            }
            for (float i = -5 * ygap; i > MinY; i = i - 5 * ygap)
            {
                if (i < MaxY)
                {
                    RectangleF rectangle;
                    float y = CartToPix(0, i).Y;
                    string str = Convert.ToString(i);
                    Font font = new Font("Segoe UI", 8, FontStyle.Regular);
                    SizeF stringSize = new SizeF(graphObj.MeasureString(str, font));
                    if (yaxisPos != 0)
                    {
                        rectangle = new RectangleF(yaxisPos - stringSize.Width - 2, y - 5, stringSize.Width, stringSize.Height);
                    }
                    else
                    {
                        rectangle = new RectangleF(yaxisPos, y - 5, stringSize.Width, stringSize.Height);
                    }
                    Brush brush = new SolidBrush(Color.Black);
                    graphObj.DrawString(str, font, brush, rectangle);
                }
            }
        }
        private void DrawGrid()
        {
            Pen greypen = new Pen(Color.Lavender, 1);
            Pen bluepen = new Pen(Color.LightBlue, 1);
            float[] gaps = new float[] { 0.01f, 0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5 };
            float xgap = gaps[0];
            int track = 0;
            while ((MaxX - MinX) / xgap > 100)
            {
                if (track < 7)
                {
                    track++;
                    xgap = gaps[track];
                }
                else
                {
                    xgap += 5;
                }
            }
            track = 0;
            float ygap = gaps[0];
            while ((MaxY - MinY) / ygap > 100)
            {
                if (track < 7)
                {
                    track++;
                    ygap = gaps[track];
                }
                else
                {
                    ygap += 5;
                }
            }
            for (float i = 0; i < MaxX; i = i + xgap)
            {
                if (i > MinX)
                {
                    float x = CartToPix(i, 0).X;
                    graphObj.DrawLine(greypen, x, 0, x, pbGraph.Height);
                }
            }
            for (float i = 0; i > MinX; i = i - xgap)
            {
                if (i < MaxX)
                {
                    float x = CartToPix(i, 0).X;
                    graphObj.DrawLine(greypen, x, 0, x, pbGraph.Height);
                }
            }
            for (float i = 0; i < MaxX; i = i + 5 * xgap)
            {
                if (i > MinX)
                {
                    float x = CartToPix(i, 0).X;
                    graphObj.DrawLine(bluepen, x, 0, x, pbGraph.Height);
                }
            }
            for (float i = -5 * xgap; i > MinX; i = i - 5 * xgap)
            {
                if (i < MaxX)
                {
                    float x = CartToPix(i, 0).X;
                    graphObj.DrawLine(bluepen, x, 0, x, pbGraph.Height);
                }
            }
            for (float i = 0; i < MaxY; i = i + ygap)
            {
                if (i > MinY)
                {
                    float y = CartToPix(0, i).Y;
                    graphObj.DrawLine(greypen, 0, y, pbGraph.Width, y);
                }
            }
            for (float i = 0; i > MinY; i = i - ygap)
            {
                if (i < MaxY)
                {
                    float y = CartToPix(0, i).Y;
                    graphObj.DrawLine(greypen, 0, y, pbGraph.Width, y);
                }
            }
            for (float i = 5 * ygap; i < MaxY; i = i + 5 * ygap)
            {
                if (i > MinY)
                {

                    float y = CartToPix(0, i).Y;
                    graphObj.DrawLine(bluepen, 0, y, pbGraph.Width, y);
                }
            }
            for (float i = -5 * ygap; i > MinY; i = i - 5 * ygap)
            {
                if (i < MaxY)
                {
                    float y = CartToPix(0, i).Y;
                    graphObj.DrawLine(bluepen, 0, y, pbGraph.Width, y);
                }
            }
            DrawAxes(xgap, ygap);
        }
        private void ReadSettings()
        {
            StreamReader reader = new StreamReader("Settings.txt");
            MinX = float.Parse(reader.ReadLine());
            MaxX = float.Parse(reader.ReadLine());
            MinY = float.Parse(reader.ReadLine());
            MaxY = float.Parse(reader.ReadLine());
            reader.Close();
            xfactor = pbGraph.Width / (MaxX - MinX);
            yfactor = pbGraph.Height / (MaxY - MinY);
        }
        private void WriteSettings()
        {
            StreamWriter writer = new StreamWriter("Settings.txt");
            writer.WriteLine(MinX);
            writer.WriteLine(MaxX);
            writer.WriteLine(MinY);
            writer.WriteLine(MaxY);
            writer.Close();
            xfactor = pbGraph.Width / (MaxX - MinX);
            yfactor = pbGraph.Height / (MaxY - MinY);
        }
        private void DrawGraph(Pen pen, string function)
        {
            ReadSettings();
            GetPointArray(function);
            if (error != true)
            {
                DrawGrid();
                try
                {
                    for (int i = 0; i < PixPoints.Length - 1; i++)
                    {
                        if (double.IsNaN(PixPoints[i].Y) != true && double.IsNaN(PixPoints[i + 1].Y) != true)
                        {
                            try
                            {
                                graphObj.DrawLine(pen, PixPoints[i], PixPoints[i + 1]);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                catch (OverflowException)
                {

                }
            }
        }
        private string GetYIntercept(string function)
        {
            parser = new Parser(function);
            string yintercept = parser.Evaluate(parser.root, Convert.ToString(0)).value;
            return yintercept;
        }
        private string SignChange(double a, double b)
        {
            string root = "";
            double precision = 0.0001;
            while (Math.Abs(b - a) > precision)
            {
                double c = (b - a) / 2;
                double cY = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(c)).value);
                double aY = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(a)).value);
                double bY = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(b)).value);
                if (Math.Abs(cY) < precision)
                {
                    root = "x = " + Convert.ToString(c);
                }
                else if (aY * cY > 0)
                {
                    a = c;
                }
                else if (bY * cY > 0)
                {
                    b = c;
                }
            }
            return root;
        }
        private List<string> GetRoots()
        {
            List<string> roots = new List<string>();
            int index = 0;
            int count = 0;
            for (int i = 1; i < CartPoints.Length - 1; i++)
            {
                if (Convert.ToString(CartPoints[i].Y) == "0")
                {
                    if (index + 1 == i)
                    {
                        count++;
                    }
                    else
                    {
                        roots.Add("x = " + Convert.ToString(CartPoints[i].X));
                    }
                    index = i;
                }
                else if ((CartPoints[i - 1].Y < 0 && CartPoints[i].Y > 0) ||
                   (CartPoints[i - 1].Y > 0 && CartPoints[i].Y < 0))
                {
                    string root = SignChange(CartPoints[i - 1].X, CartPoints[i].X);
                    if (roots.Contains(root) == false)
                    {
                        roots.Add(root);
                    }
                }
            }
            return roots;
        }
        private void DisplayInfo(ListBox lb, string function)
        {
            lb.Items.Clear();
            if (error != true)
            {
                lb.Items.Add("Y-Intercept: " + GetYIntercept(function));
                List<string> roots = GetRoots();
                if (roots.Count > 20)
                {
                    lb.Items.Add("There are too many roots to display.");
                }
                else
                {
                    for (int i = 0; i < roots.Count; i++)
                    {
                        lb.Items.Add(roots[i]);
                    }
                }
            }
        }
        private void Zoom(float x, float y, float multiplier)
        {
            float[] settings = new float[4];
            float xdiff = MaxX - MinX;
            float ydiff = MaxY - MinY;
            xdiff = xdiff * multiplier;
            ydiff = ydiff * multiplier;
            x = PixToCart(x, y).X;
            y = PixToCart(x, y).Y;
            settings[0] = x - (xdiff / 2);
            settings[1] = x + (xdiff / 2);
            settings[2] = y - (ydiff / 2);
            settings[3] = y + (ydiff / 2);
            for (int i = 0; i < 4; i++)
            {
                if (settings[i] > 300)
                {
                    settings[i] = 300;
                }
                if (settings[i] < -300)
                {
                    settings[i] = -300;
                }
            }
            MinX = settings[0];
            MaxX = settings[1];
            MinY = settings[2];
            MaxY = settings[3];
            WriteSettings();
            FuncCheck();
        }
        public void FuncCheck()
        {
            error = false;
            graphObj = pbGraph.CreateGraphics();
            graphObj.Clear(Color.White);
            if (slctFunc1.Checked)
            {
                Pen pen = new Pen(Func1Colour.BackColor);
                DrawGraph(pen, txtFunc1.Text);
                DisplayInfo(lbFunc1Info, txtFunc1.Text);
                CartPoints1 = CartPoints;
                PixPoints1 = PixPoints;
            }
            if (slctFunc2.Checked)
            {
                Pen pen = new Pen(Func2Colour.BackColor);
                DrawGraph(pen, txtFunc2.Text);
                DisplayInfo(lbFunc2Info, txtFunc2.Text);
                CartPoints2 = CartPoints;
                PixPoints2 = PixPoints;
            }
            if (slctFunc3.Checked)
            {
                Pen pen = new Pen(Func3Colour.BackColor);
                DrawGraph(pen, txtFunc3.Text);
                DisplayInfo(lbFunc3Info, txtFunc3.Text);
                CartPoints3 = CartPoints;
                PixPoints3 = PixPoints;
            }
            if (slctFunc4.Checked)
            {
                Pen pen = new Pen(Func4Colour.BackColor);
                DrawGraph(pen, txtFunc4.Text);
                DisplayInfo(lbFunc4Info, txtFunc4.Text);
                CartPoints4 = CartPoints;
                PixPoints4 = PixPoints;
            }
            if (slctFunc5.Checked)
            {
                Pen pen = new Pen(Func5Colour.BackColor);
                DrawGraph(pen, txtFunc5.Text);
                DisplayInfo(lbFunc5Info, txtFunc5.Text);
                CartPoints5 = CartPoints;
                PixPoints5 = PixPoints;
            }
        }
        private void Graph_Load(object sender, EventArgs e)
        {
            btHelpG.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btSettingsG.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btExitG.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbZoomIn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbZoomOut.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbCursor.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbUp.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbDown.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbLeft.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbRight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            graphObj = pbGraph.CreateGraphics();
        }

        private void Graph_Resize(object sender, EventArgs e)
        {
            int gap = ((ClientRectangle.Height - 65) / 5) - 5;
            txtFunc1.SetBounds(36, 80, 170, 23);
            txtFunc2.SetBounds(36, 80 + gap, 170, 23);
            txtFunc3.SetBounds(36, 80 + gap * 2, 170, 23);
            txtFunc4.SetBounds(36, 80 + gap * 3, 170, 23);
            txtFunc5.SetBounds(36, 80 + gap * 4, 170, 23);

            lby1.SetBounds(16, txtFunc1.Location.Y + 3, 24, 15);
            lby2.SetBounds(16, txtFunc2.Location.Y + 3, 24, 15);
            lby3.SetBounds(16, txtFunc3.Location.Y + 3, 24, 15);
            lby4.SetBounds(16, txtFunc4.Location.Y + 3, 24, 15);
            lby5.SetBounds(16, txtFunc5.Location.Y + 3, 24, 15);

            slctFunc1.SetBounds(211, txtFunc1.Location.Y + 5, 15, 14);
            slctFunc2.SetBounds(211, txtFunc2.Location.Y + 5, 15, 14);
            slctFunc3.SetBounds(211, txtFunc3.Location.Y + 5, 15, 14);
            slctFunc4.SetBounds(211, txtFunc4.Location.Y + 5, 15, 14);
            slctFunc5.SetBounds(211, txtFunc5.Location.Y + 5, 15, 14);

            label2.SetBounds(232, txtFunc1.Location.Y + 3, 89, 15);
            label3.SetBounds(232, txtFunc2.Location.Y + 3, 89, 15);
            label4.SetBounds(232, txtFunc3.Location.Y + 3, 89, 15);
            label5.SetBounds(232, txtFunc4.Location.Y + 3, 89, 15);
            label6.SetBounds(232, txtFunc5.Location.Y + 3, 89, 15);

            Func1Colour.SetBounds(323, txtFunc1.Location.Y, 23, 23);
            Func2Colour.SetBounds(323, txtFunc2.Location.Y, 23, 23);
            Func3Colour.SetBounds(323, txtFunc3.Location.Y, 23, 23);
            Func4Colour.SetBounds(323, txtFunc4.Location.Y, 23, 23);
            Func5Colour.SetBounds(323, txtFunc5.Location.Y, 23, 23);

            lbFunc1Info.SetBounds(16, txtFunc1.Location.Y + 30, 330, gap - 40);
            lbFunc2Info.SetBounds(16, txtFunc2.Location.Y + 30, 330, gap - 40);
            lbFunc3Info.SetBounds(16, txtFunc3.Location.Y + 30, 330, gap - 40);
            lbFunc4Info.SetBounds(16, txtFunc4.Location.Y + 30, 330, gap - 40);
            lbFunc5Info.SetBounds(16, txtFunc5.Location.Y + 30, 330, gap - 40);

            int x = pbGraph.Location.X;
            int y = pbGraph.Location.Y;
            pbGraph.SetBounds(x, y, ClientRectangle.Width - x - 10, ClientRectangle.Height - y - 10);

            if (WindowState == FormWindowState.Maximized)
            {
                FuncCheck();
            }
        }

        private void btExitG_Click(object sender, EventArgs e)
        {
            for (int i = 0; i >= 0; i--)
            {
                Application.OpenForms[i].Close();
            }
        }

        private void btSettingsG_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            if (settings.Visible == false)
            {
                settings.ShowDialog();
            }
            else
            {
                settings.Hide();
            }
            graphObj = pbGraph.CreateGraphics();
            graphObj.Clear(Color.White);
            slctFunc1.Checked = false;
            slctFunc2.Checked = false;
            slctFunc3.Checked = false;
            slctFunc4.Checked = false;
            slctFunc5.Checked = false;
        }

        private void btHelpG_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            if (help.Visible == false)
            {
                help.ShowDialog();
            }
            else
            {
                help.Hide();
            }
        }
        private void slctFunc1_CheckedChanged(object sender, EventArgs e)
        {
            FuncCheck();
        }
        private void slctFunc2_CheckedChanged(object sender, EventArgs e)
        {
            FuncCheck();
        }

        private void slctFunc3_CheckedChanged(object sender, EventArgs e)
        {
            FuncCheck();
        }

        private void slctFunc4_CheckedChanged(object sender, EventArgs e)
        {
            FuncCheck();
        }

        private void slctFunc5_CheckedChanged(object sender, EventArgs e)
        {
            FuncCheck();
        }

        private void Graph_ResizeEnd(object sender, EventArgs e)
        {
            FuncCheck();
        }

        private void Func1Colour_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Func1Colour.BackColor = colorDialog1.Color;
            FuncCheck();
        }

        private void Func2Colour_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Func2Colour.BackColor = colorDialog1.Color;
            FuncCheck();
        }

        private void Func3Colour_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Func3Colour.BackColor = colorDialog1.Color;
            FuncCheck();
        }

        private void Func4Colour_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Func4Colour.BackColor = colorDialog1.Color;
            FuncCheck();
        }

        private void Func5Colour_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Func5Colour.BackColor = colorDialog1.Color;
            FuncCheck();
        }

        private void pbZoomIn_Click(object sender, EventArgs e)
        {
            Cursor zoomIn = new Cursor("CursorZoomIn.cur");
            Cursor = zoomIn;
            cursor = "zoomin";
        }

        private void pbZoomOut_Click(object sender, EventArgs e)
        {
            Cursor zoomOut = new Cursor("CursorZoomOut.cur");
            Cursor = zoomOut;
            cursor = "zoomout";
        }

        private void pbCursor_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            cursor = "default";
        }

        private void pbGraph_Click(object sender, EventArgs e)
        {
            if (cursor == "zoomin")
            {
                Zoom(pbGraph.PointToClient(MousePosition).X, pbGraph.PointToClient(MousePosition).Y, float.Parse("0.5"));
            }
            if (cursor == "zoomout")
            {
                Zoom(pbGraph.PointToClient(MousePosition).X, pbGraph.PointToClient(MousePosition).Y, 2);
            }
        }

        private void pbUp_Click(object sender, EventArgs e)
        {
            MinY += Math.Abs((MaxY - MinY) / 10);
            MaxY += Math.Abs((MaxY - MinY) / 10);
            WriteSettings();
            FuncCheck();
        }

        private void pbDown_Click(object sender, EventArgs e)
        {
            MinY -= Math.Abs((MaxY - MinY) / 10);
            MaxY -= Math.Abs((MaxY - MinY) / 10);
            WriteSettings();
            FuncCheck();
        }

        private void pbRight_Click(object sender, EventArgs e)
        {
            MinX += Math.Abs((MaxX - MinX) / 10);
            MaxX += Math.Abs((MaxX - MinX) / 10);
            WriteSettings();
            FuncCheck();
        }

        private void pbLeft_Click(object sender, EventArgs e)
        {
            MinX -= Math.Abs((MaxX - MinX) / 10);
            MaxX -= Math.Abs((MaxX - MinX) / 10);
            WriteSettings();
            FuncCheck();
        }

        private void txtFunc1_TextChanged(object sender, EventArgs e)
        {
            slctFunc1.Checked = false;
        }

        private void txtFunc2_TextChanged(object sender, EventArgs e)
        {
            slctFunc2.Checked = false;
        }

        private void txtFunc3_TextChanged(object sender, EventArgs e)
        {
            slctFunc3.Checked = false;
        }

        private void txtFunc4_TextChanged(object sender, EventArgs e)
        {
            slctFunc4.Checked = false;
        }

        private void txtFunc5_TextChanged(object sender, EventArgs e)
        {
            slctFunc5.Checked = false;
        }

        private void pbGraph_MouseHover(object sender, EventArgs e)
        {
            if (slctFunc1.Checked)
            {
                for (int i = 0; i < PixPoints1.Length; i++)
                {
                    if (PixPoints1[i].X == Cursor.Position.X && PixPoints1[i].Y == Cursor.Position.Y)
                    {
                        
                    }
                }
            }
            if (slctFunc2.Checked)
            {
                for (int i = 0; i < PixPoints2.Length; i++)
                {
                    if (PixPoints1[i].X == Cursor.Position.X && PixPoints1[i].Y == Cursor.Position.Y)
                    {

                    }
                }
            }
            if (slctFunc3.Checked)
            {
                for (int i = 0; i < PixPoints3.Length; i++)
                {
                    if (PixPoints1[i].X == Cursor.Position.X && PixPoints1[i].Y == Cursor.Position.Y)
                    {

                    }
                }
            }
            if (slctFunc4.Checked)
            {
                for (int i = 0; i < PixPoints4.Length; i++)
                {
                    if (PixPoints1[i].X == Cursor.Position.X && PixPoints1[i].Y == Cursor.Position.Y)
                    {

                    }
                }
            }
            if (slctFunc5.Checked)
            {
                for (int i = 0; i < PixPoints5.Length; i++)
                {
                    if (PixPoints1[i].X == Cursor.Position.X && PixPoints1[i].Y == Cursor.Position.Y)
                    {

                    }
                }
            }
        }
    }
}
