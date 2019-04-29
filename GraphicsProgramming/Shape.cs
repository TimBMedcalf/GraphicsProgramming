using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsProgramming
{
    abstract class Shape
    {
        // This is the base class for Shapes in the application. It should allow an array or LL
        // to be created containing different kinds of shapes.
        private List<Point> bounds = new List<Point>();

        public List<Point> Bounds { get => bounds; set => bounds = value; }

        public Shape() { }

        public virtual void move(Graphics g, Pen pen, int x, int y) { }

        public virtual void move(double x, double y, GrafPack grafPack) { }

        public virtual void rotate(double rotation, GrafPack grafPack) { }

        public virtual void draw(Graphics g, Pen pen) { }


        /// <summary>
        /// Takes in two points and an angle to return a new rotated point
        /// </summary>
        /// <param name="point">The point that you desire to rotate</param>
        /// <param name="rotatePoint">The point which you intend to rotate around</param>
        /// <param name="angle">The amount of degrees you wish you rotate around by</param>
        /// <returns>Returns a new rotated point</returns>
        public Point rotatePoint(Point point, Point rotatePoint, double angle)
        {
            //Turn the angle in degrees to radians
            double angleRadians = angle * (Math.PI / 180);
            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            //rotate the points and return a newly rotated point
            return new Point
            {
                X = (int)(cos * (point.X - rotatePoint.X) - sin * (point.Y - rotatePoint.Y) + rotatePoint.X),
                Y = (int)(sin * (point.X - rotatePoint.X) + cos * (point.Y - rotatePoint.Y) + rotatePoint.Y)
            };
        }

        /// <summary>
        /// Draws a line between two points using Digital differential analyzer algorithm (DDA)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void DrawDDALine(Graphics g, Pen pen, Point p1, Point p2)
        {
            Brush brush = pen.Brush;
            int numOfSteps;

            //Calculate point x & point y
            int px = p2.X - p1.X;
            int py = p2.Y - p1.Y;

            //step calculation for line
            if(Math.Abs(px) > Math.Abs(py))
            {
                numOfSteps = Math.Abs(px);
            }
            else
            {
                numOfSteps = Math.Abs(py);
            }

            //Increment x and y with each step 
            float xIncrement = px / (float)numOfSteps;
            float yIncrement = py / (float)numOfSteps;

            // Put pixel for each step 
            float x = p1.X;
            float y = p1.Y;

            for (int i = 0; i <= numOfSteps; i++)
            {
                //Plot the point
                PlotPoint(g, new Point((int)x, (int)y), brush);  
                x += xIncrement;
                y += yIncrement; 
            }
        }

        /// <summary>
        /// Plots a point with a specified brush
        /// </summary>
        /// <param name="g"></param>
        /// <param name="point"></param>
        /// <param name="brush"></param>
        public void PlotPoint(Graphics g, Point point, Brush brush)
        {
            g.FillRectangle(brush, point.X, point.Y, 1, 1);
            Bounds.Add(point);
        }

        /// <summary>
        /// Returns the mean center point between three points
        /// </summary>
        /// <param name="topPoint"></param>
        /// <param name="leftPoint"></param>
        /// <param name="rightPoint"></param>
        /// <returns></returns>
        public Point FindCenterPoint(Point topPoint, Point leftPoint, Point rightPoint)
        {
            return new Point
            {
                X = (topPoint.X + leftPoint.X + rightPoint.X) / 3,
                Y = (topPoint.Y + leftPoint.Y + rightPoint.Y) / 3
            };
        }


        /// <summary>
        /// Returns the mean center point between 4 points
        /// </summary>
        /// <param name="topLeftPoint"></param>
        /// <param name="topRightPoint"></param>
        /// <param name="bottomLeftPoint"></param>
        /// <param name="botttomRightPoint"></param>
        /// <returns></returns>
        public Point FindCenterPoint(Point topLeftPoint, Point topRightPoint, Point bottomLeftPoint, Point botttomRightPoint)
        {
            return new Point
            {
                X = (topLeftPoint.X + topRightPoint.X + bottomLeftPoint.X + botttomRightPoint.X) / 4,
                Y = (topLeftPoint.Y + topRightPoint.Y + bottomLeftPoint.Y + botttomRightPoint.Y) / 4
            };
        }

    }
}
