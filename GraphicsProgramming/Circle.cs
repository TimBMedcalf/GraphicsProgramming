using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsProgramming
{
    class Circle : Shape
    {
        Point keyPt, oppPt;
        int diff;
        int radius;
        Point pixelCoord;
        Point centre;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyPt"></param>
        /// <param name="oppPt"></param>
        public Circle(Point keyPt, Point oppPt)
        {
            // This method draws the square by calculating the positions of the other 2 corners
            // range and mid points of x & y 

            this.keyPt = keyPt;
            this.oppPt = oppPt;

            diff = keyPt.X - oppPt.X;
            diff += keyPt.Y - oppPt.Y;

            radius = Math.Abs(diff / 2);        

            centre.X = (keyPt.X + oppPt.X) / 2;
            centre.Y = (keyPt.Y + oppPt.Y) / 2;

            Console.WriteLine(centre);
        }


       public override void draw(Graphics g, Pen pen)
       {
            Brush brush = pen.Brush;
            int x = 0;
            int y = radius;
            
            Console.WriteLine("Called");
            int d = 3 - 2 * radius;

            while(x <= y)
            {
                pixelCoord.X = x + centre.X;
                pixelCoord.Y = y + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = y + centre.X;
                pixelCoord.Y = x + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = y + centre.X;
                pixelCoord.Y = -x + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = x + centre.X;
                pixelCoord.Y = -y + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = -x + centre.X;
                pixelCoord.Y = -y + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = -y + centre.X;
                pixelCoord.Y = -x + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = -y + centre.X;
                pixelCoord.Y = x + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                pixelCoord.X = -x + centre.X;
                pixelCoord.Y = y + centre.Y;
                PlotPoint(g, pixelCoord, brush);

                if (d <= 0)
                {
                    d = d + 4 * x + 6;
                }
                else
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
        }

        public override void move(double x, double y, GrafPack grafPack)
        {
            base.move(x, y, grafPack);
            centre.X += (int)x;
            centre.Y += (int)y;
            grafPack.Invalidate();
        }

        public override void rotate(double rotation, GrafPack grafPack)
        {
            base.rotate(rotation, grafPack);

            keyPt = rotatePoint(keyPt, centre, rotation);
            oppPt = rotatePoint(oppPt, centre, rotation);

            grafPack.Invalidate();

        }
    }
}
