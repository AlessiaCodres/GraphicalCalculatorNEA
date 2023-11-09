using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalCalculatorNEA
{
    internal class Function
    {
        public string expression = "";
        public double yintercept = 0;
        public PointF[] CartPoints = new PointF[5000];
        public PointF[] PixPoints = new PointF[5000];
        public List<string> roots = new List<string>();
        public List<PointF> min = new List<PointF>();
        public List<PointF> max = new List<PointF>();
        public List<double> gradients = new List<double>();
        public void GetYIntercept(Function function)
        {
            Parser parser = new Parser(function.expression);
            function.yintercept = Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(0)).value), 2);
        }
        public string NewtonRaphson(Function function, double x, int index)
        {
            Parser parser = new Parser(function.expression);
            double derivative = (function.CartPoints[index + 1].Y - function.CartPoints[index].Y) / (function.CartPoints[index + 1].X - function.CartPoints[index].X);
            double y = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(x)).value);
            x = Math.Round(x - (y / derivative), 2);
            if (x == -0)
            {
                return "x = " + Convert.ToString(0);
            }
            return "x = " + Convert.ToString(x);
        }
        public void GetRoots(Function function)
        {
            function.roots.Clear();
            string root;
            int count = 0;
            for (int i = 0; i < function.CartPoints.Length - 1; i++)
            {
                if (((function.CartPoints[i].Y * function.CartPoints[i + 1].Y < 0) && Math.Abs(function.CartPoints[i].Y) < 1) || function.CartPoints[i].Y == 0)
                {
                    for (int j = i - 40; j < i + 40; j++)
                    {
                        if (j >= 0 && j < CartPoints.Length)
                        {
                            if (Math.Abs(function.CartPoints[j].Y) < 0.005)
                            {
                                count++;
                            }
                        }
                    }
                    if (count < 20 && function.CartPoints[i].Y != 0)
                    {
                        root = NewtonRaphson(function, function.CartPoints[i].X, i);
                        if (!function.roots.Contains(root))
                        {
                            function.roots.Add(root);
                        }
                    }
                    if (count < 20 && function.CartPoints[i].Y == 0)
                    {
                        root = "x = " + Math.Round(function.CartPoints[i].X, 2);
                        if (root == "x = -0")
                        {
                            root = "x = 0";
                        }
                        if (!function.roots.Contains(root))
                        {
                            function.roots.Add(root);
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
        public void GetMaxPoints(Function function, float MaxY, float MinY)
        {
            function.max.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < function.gradients.Count; i++)
            {
                if (function.gradients[i] > 0)
                {
                    temp = function.gradients[i];
                    index = i + 1;
                }
                if (function.gradients[i] < 0 && temp > 0)
                {
                    double x = Math.Round((function.CartPoints[index].X + function.CartPoints[i + 1].X) / 2, 1);
                    if (x == -0)
                    {
                        x = 0;
                    }
                    double y = Math.Round(function.CartPoints[i - 1].Y, 1);
                    PointF point = new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
                    if (point.Y == 0)
                    {
                        function.roots.Add("x = " + point.X);
                    }
                    function.max.Add(point);
                    temp = 0;
                    index = 0;
                }
            }
            bool asymptote = CheckAsymptote(function.max, MaxY, MinY);
            if (asymptote)
            {
                function.max.Clear();
            }
        }
        public void GetMinPoints(Function function, float MaxY, float MinY)
        {
            function.min.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < function.gradients.Count; i++)
            {
                if (function.gradients[i] < 0)
                {
                    temp = function.gradients[i];
                    index = i + 1;
                }
                if (function.gradients[i] > 0 && temp < 0)
                {
                    double x = Math.Round((function.CartPoints[index].X + function.CartPoints[i + 1].X) / 2, 1);
                    if (x == -0)
                    {
                        x = 0;
                    }
                    double y = Math.Round(function.CartPoints[i - 1].Y, 1);
                    PointF point = new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
                    if (point.Y == 0)
                    {
                        function.roots.Add("x = " + point.X);
                    }
                    function.min.Add(point);
                    temp = 0;
                    index = 0;
                }
            }
            bool asymptote = CheckAsymptote(function.min, MaxY, MinY);
            if (asymptote)
            {
                function.min.Clear();
            }
        }
        public void GetGradients(Function function)
        {
            function.gradients.Clear();
            double m;
            for (int i = 0; i < function.CartPoints.Length - 1; i++)
            {
                m = (function.CartPoints[i + 1].Y - function.CartPoints[i].Y) / (function.CartPoints[i + 1].X - function.CartPoints[i].X);
                if (!double.IsNaN(m) && !double.IsInfinity(m))
                {
                    function.gradients.Add(m);
                }
            }
        }
    }
}
