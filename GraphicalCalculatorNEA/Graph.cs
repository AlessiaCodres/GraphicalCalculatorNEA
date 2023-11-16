using Microsoft.VisualBasic.Devices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.IO;

namespace GraphicalCalculatorNEA
{
    public partial class Graph : Form
    {
        private Function function1 = new Function();
        private Function function2 = new Function();
        private Function function3 = new Function();
        private Function function4 = new Function();
        private Function function5 = new Function();
        private float xoffset;
        private float yoffset;
        private float xorigin;
        private float yorigin;
        private float MinX;
        private float MaxX;
        private float MinY;
        private float MaxY;
        private float xfactor;
        private float yfactor;
        private float xaxisPos = 0;
        private float yaxisPos = 0;
        private Graphics graphObj;
        private string cursor = "default";
        private bool error = false;
        public Graph()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 700);
            if (!File.Exists("Settings.txt"))
            {
                Settings settings = new Settings();
                settings.InitialiseSettings();
            }
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
        private void SetPointArrays(Function function)
        {
            ReadSettings();
            SetOffset();
            Parser parser = new Parser(function.GetExpression());
            if (parser.root.value.Length > 5 && parser.root.value.Substring(0,5) == "ERROR")
            {
                slctFunc1.Checked = false;
                slctFunc2.Checked = false;
                slctFunc3.Checked = false;
                slctFunc4.Checked = false;
                slctFunc5.Checked = false;
                error = true;
                MessageBox.Show(parser.root.value);
            }
            else
            {
                function.SetCartPoints(MaxX, MinX);
                for (int i = 0; i < function.GetCartPoints().Length; i++)
                {
                    function.GetPixPoints()[i].Y = CartToPix(function.GetCartPoints()[i].X, function.GetCartPoints()[i].Y).Y;
                    function.GetPixPoints()[i].X = CartToPix(function.GetCartPoints()[i].X, function.GetCartPoints()[i].Y).X;
                }
            }
        }
        private void DrawAxes(float xgap, float ygap)
        {
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
        private void DrawGraph(Pen pen, PointF[] pixPoints)
        {
            if (error != true)
            {
                DrawGrid();
                try
                {
                    for (int i = 0; i < pixPoints.Length - 1; i++)
                    {
                        if (!double.IsNaN(pixPoints[i].Y) && !double.IsNaN(pixPoints[i + 1].Y) && !double.IsInfinity(pixPoints[i].Y) && !double.IsInfinity(pixPoints[i + 1].Y)
                            && pixPoints[i].Y > 0 && pixPoints[i].Y > 0)
                        {
                            graphObj.DrawLine(pen, pixPoints[i], pixPoints[i + 1]);
                        }
                    }
                }
                catch (OverflowException)
                {

                }
            }
        }
        private void DisplayInfo(ListBox lb, Function function)
        {
            lb.Items.Clear();
            if (error != true)
            {
                if (!double.IsInfinity(function.GetYIntercept()))
                {
                    lb.Items.Add("Y-Intercept: " + function.GetYIntercept());
                }
                if (function.GetMax().Count != 0)
                {
                    if (function.GetMax().Count > 20)
                    {
                        lb.Items.Add("There are too many maximum points to display.");
                    }
                    else
                    {

                        lb.Items.Add("Maximum Points:");
                        for (int i = 0; i < function.GetMax().Count; i++)
                        {
                            lb.Items.Add("(" + function.GetMax()[i].X + ", " + function.GetMax()[i].Y + ")");
                        }
                    }
                }
                if (function.GetMin().Count != 0)
                {
                    if (function.GetMin().Count > 20)
                    {
                        lb.Items.Add("There are too many minimum points to display.");
                    }
                    else
                    {

                        lb.Items.Add("Minimum Points:");
                        for (int i = 0; i < function.GetMin().Count; i++)
                        {
                            lb.Items.Add("(" + function.GetMin()[i].X + ", " + function.GetMin()[i].Y + ")");
                        }
                    }
                }
                if (function.GetRoots().Count > 20)
                {
                    lb.Items.Add("There are too many roots to display.");
                }
                if (function.GetRoots().Count != 0)
                {
                    lb.Items.Add("Roots:");
                    for (int i = 0; i < function.GetRoots().Count; i++)
                    {
                        lb.Items.Add(function.GetRoots()[i]);
                    }
                }
            }
        }
        private void CheckCoord(PointF[] pixPoints, PointF[] cartPoints, Point pixPoint)
        {
            for (int i = 0; i < pixPoints.Length; i++)
            {
                if (Math.Abs(Math.Abs(pixPoint.X) - Math.Abs(pixPoints[i].X)) < 5 &&
                    Math.Abs(Math.Abs(pixPoint.Y) - Math.Abs(pixPoints[i].Y)) < 5)
                {
                    string x = Convert.ToString(Math.Round(cartPoints[i].X, 3));
                    string y = Convert.ToString(Math.Round(cartPoints[i].Y, 3));
                    lbCoords.Text = "(" + x + ", " + y + ")";
                    break;
                }
            }
        }
        public void FuncCheck()
        {
            error = false;
            graphObj = pbGraph.CreateGraphics();
            graphObj.Clear(Color.White);
            ReadSettings();
            SetOffset();
            if (slctFunc1.Checked)
            {
                function1.SetExpression(txtFunc1.Text);
                UpdateFunction(function1);
                Pen pen = new Pen(Func1Colour.BackColor);
                DrawGraph(pen, function1.GetPixPoints());
                DisplayInfo(lbFunc1Info, function1);
            }
            if (slctFunc2.Checked)
            {
                function2.SetExpression(txtFunc2.Text);
                UpdateFunction(function2);
                Pen pen = new Pen(Func2Colour.BackColor);
                DrawGraph(pen, function2.GetPixPoints());
                DisplayInfo(lbFunc2Info, function2);
            }
            if (slctFunc3.Checked)
            {
                function3.SetExpression(txtFunc3.Text);
                UpdateFunction(function3);
                Pen pen = new Pen(Func3Colour.BackColor);
                DrawGraph(pen, function3.GetPixPoints());
                DisplayInfo(lbFunc3Info, function3);
            }
            if (slctFunc4.Checked)
            {
                function4.SetExpression(txtFunc4.Text);
                UpdateFunction(function4);
                Pen pen = new Pen(Func4Colour.BackColor);
                DrawGraph(pen, function4.GetPixPoints());
                DisplayInfo(lbFunc4Info, function4);
            }
            if (slctFunc5.Checked)
            {
                function5.SetExpression(txtFunc5.Text);
                UpdateFunction(function5);
                Pen pen = new Pen(Func5Colour.BackColor);
                DrawGraph(pen, function5.GetPixPoints());
                DisplayInfo(lbFunc5Info, function5);
            }
        }
        private void UpdateFunction(Function function)
        {
            SetPointArrays(function);
            if (!error)
            {
                function.FindYIntercept();
                function.FindRoots();
                function.FindGradients();
                function.FindMaxPoints(MaxY, MinY, MaxX, MinX);
                function.FindMinPoints(MaxY, MinY, MaxX, MinX);
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
            settings[0] = Convert.ToInt32(x - (xdiff / 2));
            settings[1] = Convert.ToInt32(x + (xdiff / 2));
            settings[2] = Convert.ToInt32(y - (ydiff / 2));
            settings[3] = Convert.ToInt32(y + (ydiff / 2));
            for (int i = 0; i < 4; i++)
            {
                if (settings[i] > 100)
                {
                    settings[i] = 100;
                }
                if (settings[i] < -100)
                {
                    settings[i] = -100;
                }
            }
            MinX = settings[0];
            MaxX = settings[1];
            MinY = settings[2];
            MaxY = settings[3];
            WriteSettings();
            FuncCheck();
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
            lbCoords.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);

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
            if (slctFunc1.Checked)
            {
                function1.SetExpression(txtFunc1.Text);
                UpdateFunction(function1);
            }
            FuncCheck();
        }
        private void slctFunc2_CheckedChanged(object sender, EventArgs e)
        {
            if (slctFunc2.Checked)
            {
                function2.SetExpression(txtFunc2.Text);
                UpdateFunction(function2);
            }
            FuncCheck();
        }
        private void slctFunc3_CheckedChanged(object sender, EventArgs e)
        {
            if (slctFunc3.Checked)
            {
                function3.SetExpression(txtFunc3.Text);
                UpdateFunction(function3);
            }
            FuncCheck();
        }
        private void slctFunc4_CheckedChanged(object sender, EventArgs e)
        {
            if (slctFunc4.Checked)
            {
                function4.SetExpression(txtFunc4.Text);
                UpdateFunction(function4);
            }
            FuncCheck();
        }
        private void slctFunc5_CheckedChanged(object sender, EventArgs e)
        {
            if (slctFunc5.Checked)
            {
                function5.SetExpression(txtFunc5.Text);
                UpdateFunction(function5);
            }
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
        private void pbGraph_MouseClick(object sender, MouseEventArgs e)
        {
            if (cursor == "zoomin")
            {
                Zoom(pbGraph.PointToClient(MousePosition).X, pbGraph.PointToClient(MousePosition).Y, float.Parse("0.5"));
            }
            if (cursor == "zoomout")
            {
                Zoom(pbGraph.PointToClient(MousePosition).X, pbGraph.PointToClient(MousePosition).Y, 2);
            }
            lbCoords.Text = "";
            if (Cursor == Cursors.Default)
            {
                Point pixPoint = new Point(e.Location.X, e.Location.Y);
                if (slctFunc1.Checked)
                {
                    CheckCoord(function1.GetPixPoints(), function1.GetCartPoints(), pixPoint);
                }
                if (slctFunc2.Checked)
                {
                    CheckCoord(function2.GetPixPoints(), function2.GetCartPoints(), pixPoint);
                }
                if (slctFunc3.Checked)
                {
                    CheckCoord(function3.GetPixPoints(), function3.GetCartPoints(), pixPoint);
                }
                if (slctFunc4.Checked)
                {
                    CheckCoord(function4.GetPixPoints(), function4.GetCartPoints(), pixPoint);
                }
                if (slctFunc5.Checked)
                {
                    CheckCoord(function5.GetPixPoints(), function5.GetCartPoints(), pixPoint);
                }
            }
        }
        
    }
}
