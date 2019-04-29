using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;


namespace GraphicsProgramming
{
    public partial class GrafPack : Form
    {
        private MainMenu mainMenu;
        private bool selectSquareStatus = false;
        private bool selectTriangleStatus = false;
        private bool selectCircleStatus = false;

        //Creates graphics object to be painted on
        private Graphics g;

        //Keeps track of all shapes
        private List<Shape> shapeList = new List<Shape>();
        private Shape selectedShape;
        private int currShape = 0;

        private int clicknumber = 0;
        private Point one;
        private Point two;
        private Point three;
        private Point mousePoint;

        public GrafPack()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            // The following approach uses menu items coupled with mouse clicks
            MainMenu mainMenu = new MainMenu();
            MenuItem transformItem = new MenuItem();
            MenuItem createItem = new MenuItem();
            MenuItem selectItem = new MenuItem();
            MenuItem squareItem = new MenuItem();
            MenuItem triangleItem = new MenuItem();
            MenuItem circleItem = new MenuItem();
            MenuItem deleteItem = new MenuItem();
            MenuItem moveItem = new MenuItem();

            createItem.Text = "&Create";
            selectItem.Text = "&Select";
            transformItem.Text = "&Transform";
            moveItem.Text = "&Move";
            squareItem.Text = "&Square";
            triangleItem.Text = "&Triangle";
            circleItem.Text = "&Circle";
            deleteItem.Text = "&Delete";

            mainMenu.MenuItems.Add(createItem);
            mainMenu.MenuItems.Add(selectItem);
            mainMenu.MenuItems.Add(transformItem);
            createItem.MenuItems.Add(squareItem);
            createItem.MenuItems.Add(triangleItem);
            createItem.MenuItems.Add(circleItem);
            selectItem.MenuItems.Add(deleteItem);
            transformItem.MenuItems.Add(moveItem);

            squareItem.Click += new EventHandler(this.selectSquare);
            triangleItem.Click += new EventHandler(this.selectTriangle);
            circleItem.Click += new EventHandler(this.selectCircle);
            deleteItem.Click += new EventHandler(this.selectDelete);
            moveItem.Click += new EventHandler(this.moveSelectedShape);

            this.Menu = mainMenu;
            this.MouseClick += mouseClick;
        }



        // Generally, all methods of the form are usually private
        private void selectSquare(object sender, EventArgs e)
        {
            selectSquareStatus = true;
            MessageBox.Show("Click OK and then click once each at two locations to create a square");
        }

        private void selectTriangle(object sender, EventArgs e)
        {
            selectTriangleStatus = true;
            MessageBox.Show("Click OK and then click once each at three locations to create a triangle");
        }

        private void selectCircle(object sender, EventArgs e)
        {
            selectCircleStatus = true;
            MessageBox.Show("Click OK and then click once each at two locations to create a circle");
        }

        private void selectDelete(object sender, EventArgs e)
        {
            if(shapeList != null)
            {
                shapeList.RemoveAt(currShape);
                currShape = 0;
                this.Invalidate();
            }
        }

        private void moveSelectedShape(object sender, EventArgs e)
        {
            if(shapeList != null)
            {
                // Create a prompt to get user inputted coordinates and rotation
                double[] promptValue = Prompt.ShowDialog("Move");
                if(promptValue[0] != 0 || promptValue[1] != 0)
                {
                    //Checks if the prompt value is not set to 0 to prevent a null reference if user only types in one coordinate
                    double x = promptValue[0] != 0 ? promptValue[0] : 0;
                    double y = promptValue[1] != 0 ? promptValue[1] : 0;
                    shapeList[currShape].move(x, y, this);
                }
                if(promptValue[2] !=  0)
                {
                    shapeList[currShape].rotate(promptValue[2], this);
                }   
            }
        }

        /// <summary>
        /// Checks the moouse position against the bounds of each shape which is being rendered
        /// </summary>
        /// <param name="mousePoint"></param>
        private void GetMouseOverShape(Point mousePoint)
        {
            int i = -1;
            foreach (Shape shape in shapeList)
            {
                i++;
                Console.WriteLine(shapeList.Count + " " + i);
                foreach (var point in shape.Bounds)
                {
                    if (mousePoint == point)
                    {
                        currShape = i;
                        Invalidate();
                        break;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = CreateGraphics();
            //Draw each shape in the list and the selected shape with a red pen to show its selected
            foreach (var shape in shapeList)
            {
                shape.draw(g, Pens.Black);
                shapeList[currShape].draw(g, Pens.Red);
            }
        }

        private void GrafPack_MouseUp(object sender, MouseEventArgs e)
        {

        }

        // This method is quite important and detects all mouse clicks - other methods may need
        // to be implemented to detect other kinds of event handling eg keyboard presses.
        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
                if (shapeList.Count >= 1 && !(currShape >= shapeList.Count - 1))
                {
                    currShape++;
                }
                else
                {
                    currShape = 0;
                }
                Console.WriteLine(currShape);
                this.Invalidate();
            }

            if (e.Button == MouseButtons.Left)
            {
                if(!selectSquareStatus && !selectTriangleStatus && !selectCircleStatus)
                {
                    mousePoint = new Point(e.X, e.Y);
                    GetMouseOverShape(mousePoint);
                    shapeList[currShape].move(e.X, e.Y, this);

                }
                // 'if' statements can distinguish different selected menu operations to implement.
                // There may be other (better, more efficient) approaches to event handling,
                // but this approach works.
                if (selectSquareStatus == true)
                {
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber = 0;
                        selectSquareStatus = false;

                        Pen blackpen = new Pen(Color.Black);

                        Square square = new Square(one, two);
                        shapeList.Add(square);
                    }
                }
                else 
                if (selectTriangleStatus == true)
                {
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else if(clicknumber == 1)
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber++;
                    }
                    else if(clicknumber == 2)
                    {
                        three = new Point(e.X, e.Y);
                        clicknumber = 0;
                        selectTriangleStatus = false;

                        Triangle triangle = new Triangle(one, two, three, Pens.Black);
                        shapeList.Add(triangle);
                    }
                }
                else if (selectCircleStatus == true)
                {                    
                    if (clicknumber == 0)
                    {
                        one = new Point(e.X, e.Y);
                        clicknumber = 1;
                    }
                    else
                    {
                        two = new Point(e.X, e.Y);
                        clicknumber = 0;
                        selectCircleStatus = false;

                        Pen blackpen = new Pen(Color.Black);

                        Circle circle = new Circle(one, two);
                        shapeList.Add(circle);
                    }
                }
                this.Invalidate();
            }
        }
    }

}
