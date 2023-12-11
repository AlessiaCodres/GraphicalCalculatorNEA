using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalCalculatorNEA
{
    internal class Function
    {
        //function's geometric information will be stored in the approapriate variables/arrays/lists initialised here
        private string expression = "";
        private double yintercept = 0;
        private PointF[] CartPoints = new PointF[5000];
        private PointF[] PixPoints = new PointF[5000];
        private List<string> roots = new List<string>();
        private List<PointF> min = new List<PointF>();
        private List<PointF> max = new List<PointF>();
        private List<double> gradients = new List<double>();
        //y-intercept is found by evaluating the expression tree with an x value of 0
        public void FindYIntercept()
        {
            Parser parser = new Parser(expression);
            yintercept = Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(0)).value), 2);
        }
        //Cartesian points found 
        public void SetCartPoints(float MaxX, float MinX)
        {
            float step = (MaxX - MinX) / 5000; //x increments set based on settigns selected by user so that there are 5000 points
            for (int i = 0; i < CartPoints.Length; i++)
            {
                CartPoints[i].X = MinX + step * i; // x coordinates set and saves to array
            }
            for (int i = 0; i < CartPoints.Length; i++) // expression tree evaluated with each x coordinate as input to find the corresponding y coordinate and save it to the array
            {
                Parser parser = new Parser(expression);
                string y = Convert.ToString(Math.Round(Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(CartPoints[i].X)).value), 3));
                CartPoints[i].Y = (float)Convert.ToDouble(y);
            }
        }
        //Newton-Raphson method used to approximate roots
        public string NewtonRaphson(double x, int index)
        {
            Parser parser = new Parser(expression);
            double derivative = (CartPoints[index + 1].Y - CartPoints[index].Y) / (CartPoints[index + 1].X - CartPoints[index].X);
            double y = Convert.ToDouble(parser.Evaluate(parser.root, Convert.ToString(x)).value);
            x = Math.Round(x - (y / derivative), 2);
            if (x == -0) // handles possible rounding error
            {
                return "x = " + Convert.ToString(0);
            }
            return "x = " + Convert.ToString(x);
        }
        //finds roots by calling Newton-Raphson method as approapriate
        public void FindRoots()
        {
            roots.Clear();
            string root;
            int count = 0;
            for (int i = 0; i < CartPoints.Length - 1; i++)
            {
                if (((CartPoints[i].Y * CartPoints[i + 1].Y < 0) && Math.Abs(CartPoints[i].Y) < 1) || CartPoints[i].Y == 0) // sign change or y = 0 indicates root present
                {
                    // handles the case where there is an asymptote to the x-axis, there will be many y-coords stored as 0, but no root present
                    // neighbouring coordinates are checked to ensure that there aren't many y values close to 0, indicating an asymptote
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
                    // if less than half of the points around the "root" are not very close to zero, it can be decided that there is no asymptote
                    // if the y coordinate is not 0, the Newton Raphson is run to get a closer approxiation to the actual root
                    // if the y coordiante is 0, then the x coordiante is close enough to the root, and Newton Raphson does not need to be run
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
        // checks for asymptotes for checking if found max and min points are valid
        private bool CheckAsymptote(List<PointF> points, float MaxY, float MinY, float MaxX, float MinX)
        {
            bool asymptote = false;
            // asymptote present if any y coordinates are smaller outside of the view window settings
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
        // finds the maximum points of the function
        public void FindMaxPoints(float MaxY, float MinY, float MaxX, float MinX)
        {
            // max points occur when there is a transitiion between positive and negative gradients
            max.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < gradients.Count; i++)
            {
                //identifies when a positive gradient is present and saves this as temp
                if (gradients[i] > 0)
                {
                    temp = gradients[i];
                    index = i + 1;
                }
                // identifies when a negative gradient is present and when temp has been assigned a value, there must be a max point
                // the use of temp is important as it ensures that max points are not lost when there are gradients of 0 between positive and negative gradients
                if (gradients[i] < 0 && temp > 0)
                {
                    double x = Math.Round((CartPoints[index].X + CartPoints[i + 1].X) / 2, 1);
                    if (x == -0) // handles rounding of very small negative values
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
                    // it must be checked that the function is continuous around the max point found, otherwise it may be that there is not actually
                    // a maximum point present, but the gradient changes from positive to negative anyway i.e. there is an asymptote over which this happens
                    bool asymptote = CheckAsymptote(max, MaxY, MinY, MaxX, MinX);
                    if (asymptote)
                    {
                        max.Remove(point);
                    }
                    temp = 0;
                    index = 0;
                }
            }
        }
        // finds the minimum points of the function
        public void FindMinPoints(float MaxY, float MinY, float MaxX, float MinX)
        {
            // min points occur when there is a transitiion between negative and positive gradients
            min.Clear();
            double temp = 0;
            int index = 0;
            for (int i = 0; i < gradients.Count; i++)
            {
                //identifies when a negative gradient is present and saves this as temp
                if (gradients[i] < 0)
                {
                    temp = gradients[i];
                    index = i + 1;
                }
                // identifies when a positive gradient is present and when temp has been assigned a value, there must be a min point
                // the use of temp is important as it ensures that min points are not lost when there are gradients of 0 between negative and positive gradients
                if (gradients[i] > 0 && temp < 0)
                {
                    double x = Math.Round((CartPoints[index].X + CartPoints[i + 1].X) / 2, 1);
                    if (x == -0) // handles rounding of very small negative values
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
                    // it must be checked that the function is continuous around the min point found, otherwise it may be that there is not actually
                    // a minimum point present, but the gradient changes from negative to positive anyway i.e. there is an asymptote over which this happens
                    bool asymptote = CheckAsymptote(min, MaxY, MinY, MaxX, MinX);
                    if (asymptote)
                    {
                        min.Remove(point);
                    }
                    temp = 0;
                    index = 0;
                }
            }

        }
        // finds the gradient between neighbouring coordinates for use in finding max and min points
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
        // Geters and Seters used to communicate with Function objects through an interface
        public void SetExpression(string Expression) { expression = Expression; }
        public string GetExpression() { return expression; }
        public double GetYIntercept() { return yintercept; }
        public PointF[] GetPixPoints() { return PixPoints; }
        public List<PointF> GetMin() { return min; }
        public List<PointF> GetMax() { return max; }
        public PointF[] GetCartPoints() { return CartPoints; }
        public List<string> GetRoots() { return roots; }
    }
}
