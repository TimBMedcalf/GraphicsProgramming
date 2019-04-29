using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsProgramming
{
    class Square : Shape
    {
        //This class contains the specific details for a square defined in terms of opposite corners
        Point keyPt, oppPt, mid, topRight, topLeft, bottomRight, bottomLeft;    // these points identify opposite corners of the square

        public Square(Point keyPt, Point oppPt)   // constructor
        {
            this.keyPt = keyPt;
            this.oppPt = oppPt;

            // This method draws the square by calculating the positions of the other 2 corners
            double xDiff, yDiff;   // range and mid points of x & y  

            // calculate ranges and mid points
            xDiff = oppPt.X - keyPt.X;
            yDiff = oppPt.Y - keyPt.Y;

            mid.X = (oppPt.X + keyPt.X) / 2;
            mid.Y = (oppPt.Y + keyPt.Y) / 2;

            topLeft.X = keyPt.X;
            topLeft.Y = keyPt.Y;

            topRight.X = (int)(mid.X + yDiff / 2);
            topRight.Y = (int)(mid.Y - xDiff / 2);

            bottomRight.X = oppPt.X;
            bottomRight.Y = oppPt.Y;

            bottomLeft.X = (int)(mid.X - yDiff / 2);
            bottomLeft.Y = (int)(mid.Y + xDiff / 2);
        }

        // You will need a different draw method for each kind of shape. Note the square is drawn
        // from first principles. All other shapes should similarly be drawn from first principles. 
        // Ideally no C# standard library class or method should be used to create, draw or transform a shape
        // and instead should utilse user-developed code.
        public override void draw(Graphics g, Pen blackPen)
        {
            DrawDDALine(g, blackPen, topLeft, topRight);
            DrawDDALine(g, blackPen, topRight, bottomRight);
            DrawDDALine(g, blackPen, bottomRight, bottomLeft);
            DrawDDALine(g, blackPen, bottomLeft, topLeft);
        }

        public override void move(double x, double y, GrafPack grafPack)
        {
            base.move(x, y, grafPack);
            keyPt.X += (int)x;
            keyPt.Y += (int)y;

            oppPt.X += (int)x;
            oppPt.Y += (int)y;
            grafPack.Invalidate();
        }

        public override void rotate(double rotation, GrafPack grafPack)
        {
            Point midPoint = FindCenterPoint(topLeft, topRight, bottomLeft, bottomRight);
            Console.WriteLine(rotation);
            Console.WriteLine("Before: "+ topRight.X + " " + topRight.Y);


            topLeft = rotatePoint(topLeft, midPoint, rotation);
            topRight = rotatePoint(topRight, midPoint, rotation);
            bottomLeft = rotatePoint(bottomLeft, midPoint, rotation);
            bottomRight = rotatePoint(bottomRight, midPoint, rotation);

            Console.WriteLine("After: " + topRight.X + " " + topRight.Y);

            grafPack.Invalidate();
        }

    }
}
