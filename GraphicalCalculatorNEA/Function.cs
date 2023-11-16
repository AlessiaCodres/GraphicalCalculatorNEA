using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalCalculatorNEA
{
    internal class Function
    {
        private string expression = "";
        private double yintercept = 0;
        private PointF[] CartPoints = new PointF[5000];
        private PointF[] PixPoints = new PointF[5000];
        private List<string> roots = new List<string>();
        private List<PointF> min = new List<PointF>();
        private List<PointF> max = new List<PointF>();
        private List<double> gradients = new List<double>();
        public void FindYIntercept()
        {
            Parser parser = new Parser(expression);
            yintercept = Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(0)).value), 2);
        }
        public void SetCartPoints(float MaxX, float MinX)
        {
            float step = (MaxX - MinX) / 5000;
            for (int i = 0; i < CartPoints.Length; i++)
            {
                CartPoints[i].X = MinX + step * i;
            }
            for (int i = 0; i < CartPoints.Length; i++)
            {
                Parser parser = new Parser(expression);
                string y = Convert.ToString(Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(CartPoints[i].X)).value), 3));
                CartPoints[i].Y = (float)Convert.ToDouble(y);
            }
        }
        public string NewtonRaphson(double x, int index)
        {
            Parser parser = new Parser(expression);
            double derivative = (CartPoints[index + 1].Y - CartPoints[index].Y) / (CartPoints[index + 1].X - CartPoints[index].X);
            double y = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(x)).value);
            x = Math.Round(x - (y / derivative), 2);
            if (x == -0)
            {
                return "x = " + Convert.ToString(0);
            }
            return "x = " + Convert.ToString(x);
        }
        public void FindRoots()
        {
            roots.Clear();
            string root;
            int count = 0;
            for (int i = 0; i < CartPoints.Length - 1; i++)
            {
                if (((CartPoints[i].Y * CartPoints[i + 1].Y < 0) && Math.Abs(CartPoints[i].Y) < 1) || CartPoints[i].Y == 0)
                {
                    for (int j = i - 40; j < i + 40; j++)
                    {
                        if (j >= 0 && j < CartPoints.Length)
                        {
                            if (Math.Abs(CartPoints[j].Y) < 0.005)
                            {
                                count++;
                            }
                        }
                    }
                    if (count < 20 && CartPoints[i].Y != 0)
                    {
                        root = NewtonRaphson(CartPoints[i].X, i);
                        if (!roots.Contains(root))
                        {
                            roots.Add(root);
                        }
                    }
                    if (count < 20 && CartPoints[i].Y == 0)
                    {
                        root = "x = " + Math.Round(CartPoints[i].X, 2);
                        if (root == "x = -0")
                        {
                            root = "x = 0";
                        }
                        if (!roots.Contains(root))
                        {
                            roots.Add(root);
                        }
                    }
                }
            }
        }
        private bool CheckAsymptote(List<PointF> points, float MaxY, float MinY)
        {
            bool asymptote = false;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y > MaxY || points[i].Y < MinY)
                {
                    asymptote = true;
                    return asymptote;
                }
            }
            return asymptote;
        }
        public void FindMaxPoints(float MaxY, float MinY)
        {
            max.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < gradients.Count; i++)
            {
                if (gradients[i] > 0)
                {
                    temp = gradients[i];
                    index = i + 1;
                }
                if (gradients[i] < 0 && temp > 0)
                {
                    double x = Math.Round((CartPoints[index].X + CartPoints[i + 1].X) / 2, 1);
                    if (x == -0)
                    {
                        x = 0;
                    }
                    double y = Math.Round(CartPoints[i - 1].Y, 1);
                    PointF point = new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
                    if (point.Y == 0)
                    {
                        roots.Add("x = " + point.X);
                    }
                    max.Add(point);
                    temp = 0;
                    index = 0;
                }
            }
            bool asymptote = CheckAsymptote(max, MaxY, MinY);
            if (asymptote)
            {
                max.Clear();
            }
        }
        public void FindMinPoints(float MaxY, float MinY)
        {
            min.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < gradients.Count; i++)
            {
                if (gradients[i] < 0)
                {
                    temp = gradients[i];
                    index = i + 1;
                }
                if (gradients[i] > 0 && temp < 0)
                {
                    double x = Math.Round((CartPoints[index].X + CartPoints[i + 1].X) / 2, 1);
                    if (x == -0)
                    {
                        x = 0;
                    }
                    double y = Math.Round(CartPoints[i - 1].Y, 1);
                    PointF point = new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
                    if (point.Y == 0)
                    {
                        roots.Add("x = " + point.X);
                    }
                    min.Add(point);
                    temp = 0;
                    index = 0;
                }
            }
            bool asymptote = CheckAsymptote(min, MaxY, MinY);
            if (asymptote)
            {
                min.Clear();
            }
        }
        public void CompareMaxMin()
        {
            if (min.Count > 0 && max.Count > 0)
            {
                for (int i = max.Count - 1; i < max.Count; i++)
                {
                    for (int j = min.Count - 1; j < min.Count; j++)
                    {
                        if (max[i].X == min[j].X)
                        {
                            min.RemoveAt(i);
                            max.RemoveAt(i);
                        }
                    }
                }
            }
        }
        public void FindGradients()
        {
            gradients.Clear();
            double m;
            for (int i = 0; i < CartPoints.Length - 1; i++)
            {
                m = (CartPoints[i + 1].Y - CartPoints[i].Y) / (CartPoints[i + 1].X - CartPoints[i].X);
                if (!double.IsNaN(m) && !double.IsInfinity(m))
                {
                    gradients.Add(m);
                }
            }
        }
        public void SetExpression(string Expression) { expression = Expression; }
        public string GetExpression() { return expression; }
        public double GetYIntercept() { return yintercept; }
        public PointF[] GetPixPoints() { return PixPoints; }
        public List<PointF> GetMin() { return min; }
        public List<PointF> GetMax() { return max; }
        public List<double> GetGradients() { return gradients; }
        public PointF[] GetCartPoints() { return CartPoints; }
        public List<string> GetRoots() { return roots; }
    }
}
