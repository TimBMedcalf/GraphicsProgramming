using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsProgramming
{
    class Triangle : Shape
    {
        public Point topPoint, rightPoint, leftPoint;
        public Pen pen { get; set; }

        public Triangle(Point topPoint, Point rightPoint, Point leftPoint, Pen pen)
        {
            this.topPoint = topPoint;
            this.rightPoint = rightPoint;
            this.leftPoint = leftPoint;
            this.pen = pen;
        }

        public override void draw(Graphics g, Pen pen)
        {
            DrawDDALine(g, pen, topPoint, rightPoint);
            DrawDDALine(g, pen, rightPoint, leftPoint);
            DrawDDALine(g, pen, leftPoint, topPoint);

        }

        public override void move(double x, double y, GrafPack grafPack)
        {
            Console.WriteLine("Moving");
            topPoint.X += (int)x;
            topPoint.Y += (int)y;

            rightPoint.X += (int)x;
            rightPoint.Y += (int)y;

            leftPoint.X += (int)x;
            leftPoint.Y += (int)y;
            grafPack.Invalidate();
        }

        public override void rotate(double rotation, GrafPack grafPack)
        {
            Point midPoint = FindCenterPoint(topPoint, leftPoint, rightPoint);
            Console.WriteLine(midPoint.X + " " + midPoint.Y);

            topPoint = rotatePoint(topPoint, midPoint, rotation);
            rightPoint = rotatePoint(rightPoint, midPoint, rotation);
            leftPoint = rotatePoint(leftPoint, midPoint, rotation);

            grafPack.Invalidate();
        }

    }
}
