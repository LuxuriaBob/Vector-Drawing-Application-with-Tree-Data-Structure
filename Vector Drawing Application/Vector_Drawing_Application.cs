using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Drawing.Drawing2D;

namespace Vector_Drawing_Application
{
    public partial class Vector_Drawing_Application : Form
    {
        //default values are false
        bool drawRect = false;
        bool drawSquare = false;
        bool drawCircle = false;
        bool drawLine = false;
        bool drawPolygon = false;
        bool select = false;
        bool move = false;
        bool stretch = false;

        Matrix myMatrix = new Matrix();

        public Vector_Drawing_Application()
        {
            this.DoubleBuffered = true; //reduces flicker

            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            InitializeComponent();
            KeyPreview = true;  //necessary for keyboard input
        }

        GraphRect SelectedRect = null;  //nothing is selected for default
        GraphSquare SelectedSquare = null;
        GraphCircle SelectedCircle = null;
        GraphLine SelectedLine = null;
        GraphPolygon SelectedPolygon = null;

        RectMoveInfo RectMoving = null; //move info for selected rect
        SquareMoveInfo SquareMoving = null; //move info for selected square
        CircleMoveInfo CircleMoving = null; //move info for selected circle
        LineMoveInfo LineMoving = null; //move info for selected line
        PolygonMoveInfo PolygonMoving = null;//move info for selected polygon

        RectMoveInfo RectSecondMoving = null;   //stores rect-moving for undo-move method
        Point secondRectE;  //stores rect-coordinates for undo-move

        SquareMoveInfo SquareSecondMoving = null;   //storessquare moving for undo-move method
        Point secondSquareE;  //stores square-coordinates for undo-move

        CircleMoveInfo CircleSecondMoving = null;   //stores circle moving for undo-move method
        Point secondCircleE;  //stores circle-coordinates for undo-move

        LineMoveInfo LineSecondMoving = null;   //stores line moving for undo-move method
        Point secondLineE;  //stores line-coordinates for undo-move

        PolygonMoveInfo PolygonSecondMoving = null;   //stores Polygon moving for undo-move method
        Point secondPolygonE;  //stores Polygon-coordinates for undo-move

        Point startlocation;    //shape's top left coordinates
        Point endlocation;      //shape's bottom right coordinates
        PointF cornerlocation;
        Point polyMoveLocation;
        Point polyStretchLocation;

        int index = 0;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawRect)
            {
                startlocation = e.Location; //stores mouse location for first coordinates
            }
            else if (drawSquare)
            {
                startlocation = e.Location;
            }
            else if (drawCircle)
            {
                startlocation = e.Location;
            }
            else if (drawLine)
            {
                startlocation = e.Location;
            }
            else if (move)
            {
                RefreshRectSelection(e.Location);
                if (this.SelectedRect != null && RectMoving == null)
                {
                    this.Capture = true;
                    RectMoving = new RectMoveInfo   //sets Moving class properties for moving rect
                    {
                        Rect = this.SelectedRect,
                        StartRectPoint = SelectedRect.StartPoint,
                        WidthRect = SelectedRect.Width,
                        HeightRect = SelectedRect.Height,
                        StartMoveMousePoint = e.Location
                    };
                    RectSecondMoving = RectMoving;      //stores Moving for undo method
                    secondRectE = e.Location;   //stores mouse coordinates for undo method
                    this.Cursor = Cursors.SizeAll;
                }

                RefreshSquareSelection(e.Location);
                if (this.SelectedSquare != null && SquareMoving == null)
                {
                    this.Capture = true;
                    SquareMoving = new SquareMoveInfo   //sets Moving class properties for moving square
                    {
                        Square = this.SelectedSquare,
                        StartSquarePoint = SelectedSquare.StartPoint,
                        SquareSide = SelectedSquare.Side,
                        StartMoveMousePoint = e.Location
                    };
                    SquareSecondMoving = SquareMoving;      //stores Moving for undo method
                    secondSquareE = e.Location;   //stores mouse coordinates for undo method
                }

                RefreshCircleSelection(e.Location);
                if (this.SelectedCircle != null && CircleMoving == null)
                {
                    this.Capture = true;
                    CircleMoving = new CircleMoveInfo   //sets Moving class properties for moving circle
                    {
                        Circle = this.SelectedCircle,
                        StartCirclePoint = SelectedCircle.Center,
                        CirclePerimeter = SelectedCircle.Radius,
                        StartMoveMousePoint = e.Location
                    };
                    CircleSecondMoving = CircleMoving;      //stores Moving for undo method
                    secondCircleE = e.Location;   //stores mouse coordinates for undo method
                }

                RefreshLineSelection(e.Location);
                if (this.SelectedLine != null && LineMoving == null)
                {
                    this.Capture = true;
                    LineMoving = new LineMoveInfo   //sets Moving class properties for moving line
                    {
                        Line = this.SelectedLine,
                        StartLinePoint = SelectedLine.StartPoint,
                        EndLinePoint = SelectedLine.EndPoint,
                        StartMoveMousePoint = e.Location
                    };
                    LineSecondMoving = LineMoving;      //stores Moving for undo method
                    secondLineE = e.Location;   //stores mouse coordinates for undo method
                }

                RefreshPolygonSelection(e.Location);
                if (this.SelectedPolygon != null && PolygonMoving == null)
                {
                    this.Capture = true;
                    PolygonMoving = new PolygonMoveInfo   //sets Moving class properties for moving Polygon
                    {
                        Polygon = this.SelectedPolygon,
                        PolygonPoints = SelectedPolygon.CurvePoints,
                        StartMoveMousePoint = e.Location
                    };
                    PolygonSecondMoving = PolygonMoving;      //stores Moving for undo method
                    secondPolygonE = e.Location;   //stores mouse coordinates for undo method

                    for (int i = 0; i < PolygonMoving.PolygonPoints.Length; i++)
                    {
                        Console.WriteLine($"polygonpoints {i}.X = " + PolygonMoving.PolygonPoints[i].X);
                        Console.WriteLine($"polygonpoints {i}.Y = " + PolygonMoving.PolygonPoints[i].Y);
                    }
                }
            }
            else if (select)
            {
                RectangleHitTest(Rects, e.Location);
                RefreshRectSelection(e.Location);

                SquareHitTest(Squares, e.Location);
                RefreshSquareSelection(e.Location);

                CircleHitTest(Circles, e.Location);
                RefreshCircleSelection(e.Location);

                LineHitTest(Lines, e.Location);
                RefreshLineSelection(e.Location);

                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Math.Abs(e.X - Lines[i].StartPoint.X) < 5 && Math.Abs(e.Y - Lines[i].StartPoint.Y) < 5)
                    {
                        cornerlocation = Lines[i].StartPoint;
                    }
                    else if (Math.Abs(e.X - Lines[i].EndPoint.X) < 5 && Math.Abs(e.Y - Lines[i].EndPoint.Y) < 5)
                    {
                        cornerlocation = Lines[i].EndPoint;
                    }
                }

                Console.WriteLine("cornerlocation" + cornerlocation);
                Console.WriteLine("startpoint" + SelectedLine.StartPoint);
                Console.WriteLine("endpoint" + SelectedLine.EndPoint);

                PolygonHitTest(Polygons, e.Location);
                RefreshPolygonSelection(e.Location);

                Refresh();
            }
            else if (stretch)
            {
                Console.WriteLine("stretchte cornerlocation" + cornerlocation);
                Console.WriteLine(cornerlocation == SelectedLine.EndPoint);
                polyStretchLocation = e.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawRect)
            {
                endlocation = e.Location;   //stores mouse location while mouse is moving
            }
            else if (drawSquare)
            {
                endlocation = e.Location;
            }
            else if (drawCircle)
            {
                endlocation = e.Location;
            }
            else if (drawLine)
            {
                endlocation = e.Location;
            }
            else if (move)
            {
                this.Cursor = Cursors.SizeAll;

                if (RectMoving != null)
                {
                    index++;
                    Console.WriteLine("rect index" + index);
                    RectMoving.Rect.SetX(RectMoving.StartRectPoint.X + e.X - RectMoving.StartMoveMousePoint.X); //sets x coordinate of a moving rect
                    RectMoving.Rect.SetY(RectMoving.StartRectPoint.Y + e.Y - RectMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving rect
                }
                RefreshRectSelection(e.Location);

                if (SquareMoving != null)
                {
                    SquareMoving.Square.SetX(SquareMoving.StartSquarePoint.X + e.X - SquareMoving.StartMoveMousePoint.X); //sets x coordinate of a moving square
                    SquareMoving.Square.SetY(SquareMoving.StartSquarePoint.Y + e.Y - SquareMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving square
                }
                RefreshSquareSelection(e.Location);

                if (CircleMoving != null)
                {
                    CircleMoving.Circle.SetX(CircleMoving.StartCirclePoint.X + e.X - CircleMoving.StartMoveMousePoint.X); //sets x coordinate of a moving circle
                    CircleMoving.Circle.SetY(CircleMoving.StartCirclePoint.Y + e.Y - CircleMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving circle
                }
                RefreshCircleSelection(e.Location);

                if (LineMoving != null)
                {
                    LineMoving.Line.SetX1(LineMoving.StartLinePoint.X + e.X - LineMoving.StartMoveMousePoint.X); //sets x coordinate of a moving line
                    LineMoving.Line.SetY1(LineMoving.StartLinePoint.Y + e.Y - LineMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving line
                    LineMoving.Line.SetX2(LineMoving.EndLinePoint.X + e.X - LineMoving.StartMoveMousePoint.X); //sets x coordinate of a moving line
                    LineMoving.Line.SetY2(LineMoving.EndLinePoint.Y + e.Y - LineMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving line
                }
                RefreshLineSelection(e.Location);

                if (PolygonMoving != null)
                {
                    polyMoveLocation = e.Location;

                    /*
                    matrixLocation = e.Location;
                    Console.WriteLine("matrixlocation = " + matrixLocation);
                    Console.WriteLine("secondPolygonE = " + secondPolygonE);
                    Console.WriteLine("startmoveX" + PolygonMoving.StartMoveMousePoint.X);
                    Console.WriteLine("startmoveY" + PolygonMoving.StartMoveMousePoint.Y);
                    */

                    index++;

                    Console.WriteLine("secondPolygonE = " + secondPolygonE);
                    Console.WriteLine("polyMoveLocation = " + polyMoveLocation);

                    PolygonMoving.Polygon.SetX(PolygonMoving.PolygonPoints[0].X + polyMoveLocation.X - secondPolygonE.X);
                    PolygonMoving.Polygon.SetY(PolygonMoving.PolygonPoints[0].Y + polyMoveLocation.X - secondPolygonE.X);

                    //PolygonMoving.Polygon.SetX1(PolygonMoving.PolygonPoints[0].X + e.X - PolygonMoving.StartMoveMousePoint.X);
                    //PolygonMoving.Polygon.SetY1(PolygonMoving.PolygonPoints[0].Y + e.Y - PolygonMoving.StartMoveMousePoint.Y);
                    //PolygonMoving.Polygon.SetX2(PolygonMoving.PolygonPoints[1].X + e.X - PolygonMoving.StartMoveMousePoint.X);
                    //PolygonMoving.Polygon.SetY2(PolygonMoving.PolygonPoints[1].Y + e.Y - PolygonMoving.StartMoveMousePoint.Y);
                    //PolygonMoving.Polygon.SetX3(PolygonMoving.PolygonPoints[2].X + e.X - PolygonMoving.StartMoveMousePoint.X);
                    //PolygonMoving.Polygon.SetY3(PolygonMoving.PolygonPoints[2].Y + e.Y - PolygonMoving.StartMoveMousePoint.Y);

                    /*
                    for(int index2 = 0; index2 < PolygonMoving.PolygonPoints.Length; index2++)
                    {
                        PolygonMoving.Polygon.SetXtrial(PolygonMoving.PolygonPoints[index2].X + e.X - PolygonMoving.StartMoveMousePoint.X, index2);
                        PolygonMoving.Polygon.SetYtrial(PolygonMoving.PolygonPoints[index2].Y + e.Y - PolygonMoving.StartMoveMousePoint.Y, index2);
                    }
                    */

                    for (int i = 0; i < PolygonMoving.PolygonPoints.Length; i++)
                    {
                        Console.WriteLine($"polygonpoints {i}.X = " + PolygonMoving.PolygonPoints[i].X);
                        Console.WriteLine($"polygonpoints {i}.Y = " + PolygonMoving.PolygonPoints[i].Y);
                    }

                    Console.WriteLine("index " + index);

                    /*
                    SizeF StartMoveMouseCoordinate = new SizeF(PolygonMoving.StartMoveMousePoint.X, PolygonMoving.StartMoveMousePoint.Y);
                    SizeF CurrentMouseCoordinate = new SizeF(e.X, e.Y);

                    for(int i = 0; i<PolygonMoving.PolygonPoints.Length; i++)
                    {
                        PolygonMoving.PolygonPoints[i] = PolygonMoving.PolygonPoints[i] + CurrentMouseCoordinate - StartMoveMouseCoordinate;
                    }
                    */
                }
                RefreshPolygonSelection(e.Location);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawRect)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawRect = false;
                CreateRectangle(startlocation, endlocation);
                Refresh();
            }
            else if (drawSquare)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawSquare = false;
                CreateSquare(startlocation, endlocation);
                Refresh();
            }
            else if (drawCircle)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawCircle = false;
                CreateCircle(startlocation, endlocation);
                Refresh();
            }
            else if (drawLine)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawLine = false;
                CreateLine(startlocation, endlocation);
                Refresh();
            }
            else if (move)
            {
                if (RectMoving != null)
                {
                    this.Capture = false;
                    RectMoving = null;
                }
                else if (SquareMoving != null)
                {
                    this.Capture = false;
                    SquareMoving = null;
                }
                else if (CircleMoving != null)
                {
                    this.Capture = false;
                    CircleMoving = null;
                }
                else if (LineMoving != null)
                {
                    this.Capture = false;
                    LineMoving = null;
                }
                else if (PolygonMoving != null)
                {
                    /*
                    matrixLocation = e.Location;
                    
                    if (matrixLocation.X > secondPolygonE.X)
                    {
                        myMatrix.Translate(matrixLocation.X - secondPolygonE.X, 0, MatrixOrder.Append);
                        myMatrix.TransformPoints(PolygonMoving.PolygonPoints);
                        Console.WriteLine("right");
                    }

                    if (matrixLocation.Y > secondPolygonE.Y)
                    {
                        myMatrix.Translate(0, matrixLocation.Y - secondPolygonE.Y, MatrixOrder.Append);
                        myMatrix.TransformPoints(PolygonMoving.PolygonPoints);
                        Console.WriteLine("down");
                    }
                    */
                    this.Capture = false;
                    PolygonMoving = null;
                }
                RefreshRectSelection(e.Location);
                RefreshSquareSelection(e.Location);
                RefreshCircleSelection(e.Location);
                RefreshLineSelection(e.Location);
                RefreshPolygonSelection(e.Location);
            }
        }

        public List<GraphPolygon> Polygons = new List<GraphPolygon>();   // main Polygons list
        public List<PointF> polygonPoints = new List<PointF>();

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawPolygon)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        //get points for polygon
                        polygonPoints.Add((new PointF(e.X, e.Y)));
                        break;
                    case MouseButtons.Right:
                        //finish polygon
                        if (polygonPoints.Count() > 2)
                        {
                            if (FillColorCheckBox.Checked)
                            {
                                int fill = 1;
                                GraphPolygon polygon = new GraphPolygon(SelectedPolygon, Polygons.Count + 1,
                                    polygonPoints.ToArray(), fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                                Polygons.Add(polygon);   //adds to Polygons list
                            }
                            else
                            {
                                int fill = 0;
                                GraphPolygon polygon = new GraphPolygon(SelectedPolygon, Polygons.Count + 1,
                                    polygonPoints.ToArray(), fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                                Polygons.Add(polygon);   //adds to Polygons list
                            }
                            drawPolygon = false;
                            polygonPoints.Clear();
                        }
                        break;
                }
            }
            else if (stretch)
            {
                polyStretchLocation = e.Location;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        SelectedLine.Stretch(polyStretchLocation, cornerlocation);
                        break;
                    case MouseButtons.Right:
                        stretch = false;
                        break;
                }
                Refresh();
            }
        }

        ColorDialog colorPicker = new ColorDialog();

        private void ColorPickerButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            colorPicker.ShowDialog();
        }

        public List<GraphRect> Rects = new List<GraphRect>();   // main rects list

        private void CreateRectangle(Point startlocation, Point endlocation)
        {
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;    //determines x coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    //determines y coordinate of top left corner by determining which is smaller
            int rectWidth = Math.Abs(startlocation.X - endlocation.X);  //width and height is difference of coordinates
            int rectHeight = Math.Abs(startlocation.Y - endlocation.Y);

            if (FillColorCheckBox.Checked)
            {
                int fill = 1;
                GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight,
                    fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Rects.Add(rectangle);   //adds to Rects list
            }
            else
            {
                int fill = 0;
                GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight,
                    fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Rects.Add(rectangle);   //adds to Rects list
            }
        }

        public List<GraphSquare> Squares = new List<GraphSquare>();   // main squares list

        private void CreateSquare(Point startlocation, Point endlocation)
        {
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;    //determines x coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    //determines y coordinate of top left corner by determining which is smaller
            int squareSide = Math.Abs(startlocation.Y - endlocation.Y);

            if (FillColorCheckBox.Checked)
            {
                int fill = 1;
                GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Squares.Add(square);   //adds to Rects list
            }
            else
            {
                int fill = 0;
                GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Squares.Add(square);   //adds to Rects list
            }
        }

        public List<GraphCircle> Circles = new List<GraphCircle>();   // main Circles list

        private void CreateCircle(Point startlocation, Point endlocation)
        {
            int CenterX = startlocation.X;
            int CenterY = startlocation.Y;
            int xDiff = Math.Abs(startlocation.X - endlocation.X);
            int yDiff = Math.Abs(startlocation.Y - endlocation.Y);
            float radius = (float)Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

            if (FillColorCheckBox.Checked)
            {
                int fill = 1;
                GraphCircle circle = new GraphCircle(SelectedCircle, Circles.Count + 1, CenterX, CenterY, radius,
                        fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Circles.Add(circle);   //adds to Circles list
            }
            else
            {
                int fill = 0;
                GraphCircle circle = new GraphCircle(SelectedCircle, Circles.Count + 1, CenterX, CenterY, radius,
                        fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                Circles.Add(circle);   //adds to Circles list
            }
        }

        public List<GraphLine> Lines = new List<GraphLine>();   // main Lines list

        private void CreateLine(Point startlocation, Point endlocation)
        {
            int StartPositionX = startlocation.X;
            int StartPositionY = startlocation.Y;
            int EndPositionX = endlocation.X;
            int EndPositionY = endlocation.Y;

            GraphLine line = new GraphLine(SelectedLine, Lines.Count + 1, StartPositionX, StartPositionY, EndPositionX,
                EndPositionY, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
            Lines.Add(line);   //adds to Lines list
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.High; //determines how intermediate values between two endpoints are calculated.
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;  //antialiasing
            foreach (var rect in Rects)
            {
                var color = rect == SelectedRect ? Color.Blue : rect.GetColour();    //rect.GetColour(square)SelectedSquare is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, rect.size);   //selected colour, rect's size

                if (rect.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //draws square in Squares list

                }
                else
                {
                    e.Graphics.FillRectangle(pen.Brush, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //fills square in Squares list
                }
            }

            foreach (var square in Squares)
            {
                var color = square == SelectedSquare ? Color.Blue : square.GetColour();    //square.GetColour(rect)SelectedSquare is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, square.size);   //selected colour, square's size

                if (square.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   //draws square in Squares list
                }
                else
                {
                    e.Graphics.FillRectangle(pen.Brush, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   //fills square in Squares list
                }
            }

            foreach (var circle in Circles)
            {
                var color = circle == SelectedCircle ? Color.Blue : circle.GetColour();    //circle.GetColour(rect)SelectedCircle is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, circle.size);   //selected colour, circle's size

                if (circle.Fill == 0)
                {
                    e.Graphics.DrawEllipse(pen, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius + circle.Radius,
                        circle.Radius + circle.Radius);   //draws circle in Circles list
                }
                else
                {
                    e.Graphics.FillEllipse(pen.Brush, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, circle.Radius + circle.Radius,
                        circle.Radius + circle.Radius);   //fills square in Rects list
                }
            }

            foreach (var line in Lines)
            {
                var color = line == SelectedLine ? Color.Blue : line.GetColour();    //line.GetColour(rect) SelectedLine is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, line.size);   //selected colour, circle's size

                e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);   //draws circle in Circles list
            }

            foreach (var polygon in Polygons)
            {
                var color = polygon == SelectedPolygon ? Color.Blue : polygon.GetColour();    //polygon.GetColour(rect)Selectedpolygon is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, polygon.size);   //selected colour, polygon's size

                if (polygon.Fill == 0)
                {
                    e.Graphics.DrawPolygon(pen, polygon.CurvePoints);   //draws polygon in polygons list

                    /*
                   // Point[] points = { new Point(100, 1000), new Point(300, 250) };

                    // Draw line connecting two untransformed points.
                    e.Graphics.DrawPolygon(new Pen(Color.Green, 3), polygon.CurvePoints);

                    // Set world transformation of Graphics object to translate.
                    e.Graphics.TranslateTransform(40, 30);

                    // Transform points in array from world to page coordinates.
                    e.Graphics.TransformPoints(CoordinateSpace.Page, CoordinateSpace.World, polygon.CurvePoints);

                    // Reset world transformation.
                    e.Graphics.ResetTransform();

                    // Draw line that connects transformed points.
                    e.Graphics.DrawPolygon(new Pen(Color.Red, 3), polygon.CurvePoints);
                    */
                }
                else
                {
                    e.Graphics.FillPolygon(pen.Brush, polygon.CurvePoints);   //fills polygon in polygons list
                }
            }
        }

        static GraphRect RectangleHitTest(List<GraphRect> rects, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            foreach (var rect in rects)
            {
                //draws each rectangle on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a rectangle by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawRectangle(new Pen(Color.Green, 10), rect.StartPoint.X - p.X + size, rect.StartPoint.Y - p.Y + size, rect.Width, rect.Height);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return rect;
            }
            return null;
        }

        static GraphSquare SquareHitTest(List<GraphSquare> squares, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            foreach (var square in squares)
            {
                //draws each Square on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a Square by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawRectangle(new Pen(Color.Green, 10), square.StartPoint.X - p.X + size, square.StartPoint.Y - p.Y + size, square.Side, square.Side);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return square;
            }
            return null;
        }

        static GraphCircle CircleHitTest(List<GraphCircle> circles, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            foreach (var circle in circles)
            {
                //draws each Circle on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a Circle by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawEllipse(new Pen(Color.Green, 10), circle.Center.X - circle.Radius - p.X + size, circle.Center.Y - circle.Radius - p.Y + size,
                        circle.Radius + circle.Radius, circle.Radius + circle.Radius);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return circle;
            }
            return null;
        }

        static GraphLine LineHitTest(List<GraphLine> lines, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            foreach (var line in lines)
            {
                //draws each Line on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a Line by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawLine(new Pen(Color.Green, 10), line.StartPoint.X - p.X + size, line.StartPoint.Y - p.Y + size,
                        line.EndPoint.X - p.X + size, line.EndPoint.Y - p.Y + size);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return line;
            }
            return null;
        }

        static GraphPolygon PolygonHitTest(List<GraphPolygon> polygons, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            PointF[] curvePoints = new PointF[100];

            foreach (var polygon in polygons)
            {
                for (int i = 0; i < polygon.CurvePoints.Count(); i++)
                {
                    curvePoints[i] = new PointF(polygon.CurvePoints[i].X - p.X + size,
                        polygon.CurvePoints[i].Y - p.Y + size);
                }
            }

            foreach (var polygon in polygons)
            {
                //draws each polygon on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a Polygon by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawPolygon(new Pen(Color.Green, 10), curvePoints);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return polygon;

            }
            return null;
        }

        GraphRect LastSelectedRect = null;

        private void RefreshRectSelection(Point point)
        {
            if (select)
            {
                var selectedRect = RectangleHitTest(Rects, point);
                if (selectedRect != this.SelectedRect)
                {
                    this.SelectedRect = selectedRect;
                    LastSelectedRect = selectedRect;    //stores selectedRect for undo-select method
                    this.Invalidate();
                }
                if (RectMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        GraphSquare LastSelectedSquare = null;

        private void RefreshSquareSelection(Point point)
        {
            if (select)
            {
                var selectedSquare = SquareHitTest(Squares, point);
                if (selectedSquare != this.SelectedSquare)
                {
                    this.SelectedSquare = selectedSquare;
                    LastSelectedSquare = selectedSquare;    //stores selectedSquare for undo-select method
                    this.Invalidate();
                }
                if (SquareMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        GraphCircle LastSelectedCircle = null;

        private void RefreshCircleSelection(Point point)
        {
            if (select)
            {
                var selectedCircle = CircleHitTest(Circles, point);
                if (selectedCircle != this.SelectedCircle)
                {
                    this.SelectedCircle = selectedCircle;
                    LastSelectedCircle = selectedCircle;    //stores selectedCircle for undo-select method
                    this.Invalidate();
                }
                if (CircleMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        GraphLine LastSelectedLine = null;

        private void RefreshLineSelection(Point point)
        {
            if (select)
            {
                var selectedLine = LineHitTest(Lines, point);
                if (selectedLine != this.SelectedLine)
                {
                    this.SelectedLine = selectedLine;
                    LastSelectedLine = selectedLine;    //stores selectedLine for undo-select method
                    this.Invalidate();
                }
                if (LineMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        GraphPolygon LastSelectedPolygon = null;

        private void RefreshPolygonSelection(Point point)
        {
            if (select)
            {
                var selectedPolygon = PolygonHitTest(Polygons, point);
                if (selectedPolygon != this.SelectedPolygon)
                {
                    this.SelectedPolygon = selectedPolygon;
                    LastSelectedPolygon = selectedPolygon;    //stores selectedPolygon for undo-select method
                    this.Invalidate();
                }
                if (PolygonMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        string lastButton = null;   //stores last pressed button

        private void MakeAllFalse(ref bool s, ref bool t, ref bool u, ref bool v, ref bool w, ref bool x, ref bool y, ref bool z)
        {
            //make every parameter false
            s = false;
            t = false;
            u = false;
            v = false;
            w = false;
            x = false;
            y = false;
            z = false;
        }

        private void MakeLastTrue(ref bool s, ref bool t, ref bool u, ref bool v, ref bool w, ref bool x, ref bool y, ref bool z)
        {
            //make last parameter true
            s = false;
            t = false;
            u = false;
            v = false;
            w = false;
            x = false;
            y = false;
            z = true;
        }

        private void MakeSelectionNull(ref GraphRect selectedRect, ref GraphSquare selectedSquare, ref GraphCircle selectedCircle, ref GraphLine selectedLine, ref GraphPolygon selectedPolygon)
        {
            selectedRect = null;
            selectedSquare = null;
            selectedCircle = null;
            selectedLine = null;
            selectedPolygon = null;
        }

        private void MakeMovingNull(ref RectMoveInfo RectMoving, ref SquareMoveInfo SquareMoving, ref CircleMoveInfo CircleMoving, ref LineMoveInfo LiveMoving, ref PolygonMoveInfo PolygonMoving)
        {
            RectMoving = null;
            SquareMoving = null;
            CircleMoving = null;
            LineMoving = null;
            PolygonMoving = null;
        }

        private void MakeListsEmpty(ref List<GraphRect> Rects, ref List<GraphSquare> Squares, ref List<GraphCircle> Circles, ref List<GraphLine> Lines, ref List<GraphPolygon> Polygons)
        {
            Rects.Clear();
            Squares.Clear();
            Circles.Clear();
            Lines.Clear();
            Polygons.Clear();
        }

        private void RectButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawRect";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref move, ref drawRect);
        }

        private void SquareButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawSquare";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawRect, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref move, ref drawSquare);
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawCircle";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawRect, ref drawLine, ref drawPolygon, ref select, ref move, ref drawCircle);
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawLine";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawRect, ref drawCircle, ref drawPolygon, ref select, ref move, ref drawLine);
        }

        private void PolygonButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawPolygon";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawRect, ref drawCircle, ref drawLine, ref select, ref move, ref drawPolygon);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawCircle, ref drawRect, ref drawLine, ref drawPolygon, ref move, ref select);
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            this.Cursor = Cursors.SizeAll;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref drawRect, ref move);
        }

        private void StretchButton_Click(object sender, EventArgs e)
        {
            lastButton = "stretch";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref drawRect, ref move, ref stretch);
        }

        private void Rotate90DegressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastButton = "rotateRight";
            if (SelectedLine != null)
            {
                SelectedLine.RotateClockWise(90);
            }
            if (SelectedRect != null)
            {
                SelectedRect.RotateClockWise();
            }
            Refresh();
        }

        private void RotateHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastButton = "rotateHorizontally";
            if (SelectedLine != null)
            {
                SelectedLine.HorizontalSymmetry();
            }
            Refresh();
        }

        private void RotateVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lastButton = "rotateVertically";
            if (SelectedLine != null)
            {
                SelectedLine.VerticalSymmetry();
            }
            Refresh();
        }

        public List<GraphRect> tempRects = new List<GraphRect>();   //temperary rects list for undo-draw method
        public List<GraphSquare> tempSquares = new List<GraphSquare>();   //temperary rects list for undo-draw method
        public List<GraphCircle> tempCircles = new List<GraphCircle>();   //temperary circles list for undo-draw method
        public List<GraphLine> tempLines = new List<GraphLine>();   //temperary lines list for undo-draw method
        public List<GraphPolygon> tempPolygons = new List<GraphPolygon>();  //temperary polygons list for undo-draw method

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            lastButton = "delete";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref move, ref stretch);

            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method
            tempSquares = Squares.ToList(); //stores Squares list in tempSquares for undo method
            tempCircles = Circles.ToList(); //stores Circles list in tempCircles for undo method
            tempLines = Lines.ToList(); //stores Lines list in tempLines for undo method
            tempPolygons = Polygons.ToList(); //stores Polygons list in tempPolygons for undo method

            if (Rects.Count != 0)   //Exception for empty list
            {
                if (Rects.Contains(SelectedRect))    //delete selected rect if Rects list contains
                {
                    SelectedRect.DeleteRects(Rects);
                    Rects.Remove(SelectedRect);
                }
            }

            if (Squares.Count != 0)   //Exception for empty list
            {
                if (Squares.Contains(SelectedSquare))    //delete selected square if Squares list contains
                {
                    SelectedSquare.DeleteSquares(Squares);
                    Squares.Remove(SelectedSquare);
                }
            }

            if (Circles.Count != 0)   //Exception for empty list
            {
                if (Circles.Contains(SelectedCircle))    //delete selected vircle if Circles list contains
                {
                    SelectedCircle.DeleteCircles(Circles);
                    Circles.Remove(SelectedCircle);
                }
            }

            if (Lines.Count != 0)   //Exception for empty list
            {
                if (Lines.Contains(SelectedLine))    //delete selected vircle if Lines list contains
                {
                    SelectedLine.DeleteLines(Lines);
                    Lines.Remove(SelectedLine);
                }
            }

            if (Polygons.Count != 0)   //Exception for empty list
            {
                if (Polygons.Contains(SelectedPolygon))    //delete selected vircle if Polygons list contains
                {
                    SelectedPolygon.DeletePolygons(Polygons);
                    Polygons.Remove(SelectedPolygon);
                }
            }
            Refresh();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref move, ref stretch);
            MakeSelectionNull(ref this.SelectedRect, ref this.SelectedSquare, ref this.SelectedCircle, ref this.SelectedLine, ref this.SelectedPolygon);
            MakeMovingNull(ref RectMoving, ref SquareMoving, ref CircleMoving, ref LineMoving, ref PolygonMoving);
            MakeListsEmpty(ref Rects, ref Squares, ref Circles, ref Lines, ref Polygons);
            this.Capture = false;
            Refresh();
        }
        Graphics K = null;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, ref move, ref stretch);

            switch (lastButton)
            {
                case "drawRect":
                    if (Rects.Count > 0)
                    {
                        Rects.RemoveAt(Rects.Count - 1);    //removes last drawn rect in Rects list
                    }
                    Refresh();
                    break;
                case "drawSquare":
                    if (Squares.Count > 0)
                    {
                        Squares.RemoveAt(Squares.Count - 1);    //removes last drawn square in Squares list
                    }
                    Refresh();
                    break;
                case "drawCircle":
                    if (Circles.Count > 0)
                    {
                        Circles.RemoveAt(Circles.Count - 1);    //removes last drawn circle in Circles list
                    }
                    Refresh();
                    break;
                case "drawLine":
                    if (Lines.Count > 0)
                    {
                        Lines.RemoveAt(Lines.Count - 1);    //removes last drawn line in Lines list
                    }
                    Refresh();
                    break;
                case "drawPolygon":
                    if (Polygons.Count > 0)
                    {
                        Polygons.RemoveAt(Polygons.Count - 1);    //removes last drawn polygon in Polygons list
                    }
                    Refresh();
                    break;
                case "select":
                    if (LastSelectedRect != null)
                    {
                        var pen = new Pen(LastSelectedRect.colour, LastSelectedRect.size);
                        K = this.CreateGraphics();
                        K.DrawRectangle(pen, LastSelectedRect.StartPoint.X, LastSelectedRect.StartPoint.Y, LastSelectedRect.Width, LastSelectedRect.Height);
                    }
                    if (LastSelectedSquare != null)
                    {
                        var pen = new Pen(LastSelectedSquare.colour, LastSelectedSquare.size);
                        K = this.CreateGraphics();
                        K.DrawRectangle(pen, LastSelectedSquare.StartPoint.X, LastSelectedSquare.StartPoint.Y, LastSelectedSquare.Side, LastSelectedSquare.Side);
                    }
                    if (LastSelectedCircle != null)
                    {
                        var pen = new Pen(LastSelectedCircle.colour, LastSelectedCircle.size);
                        K = this.CreateGraphics();
                        K.DrawEllipse(pen, LastSelectedCircle.Center.X - LastSelectedCircle.Radius, LastSelectedCircle.Center.Y - LastSelectedCircle.Radius,
                        LastSelectedCircle.Radius + LastSelectedCircle.Radius, LastSelectedCircle.Radius + LastSelectedCircle.Radius);
                    }
                    if (LastSelectedLine != null)
                    {
                        var pen = new Pen(LastSelectedLine.colour, LastSelectedLine.size);
                        K = this.CreateGraphics();
                        K.DrawLine(pen, LastSelectedLine.StartPoint.X, LastSelectedLine.StartPoint.Y, LastSelectedLine.EndPoint.X, LastSelectedLine.EndPoint.Y);
                    }
                    if (LastSelectedPolygon != null)
                    {
                        var pen = new Pen(LastSelectedPolygon.colour, LastSelectedPolygon.size);
                        K = this.CreateGraphics();
                        K.DrawPolygon(pen, LastSelectedPolygon.CurvePoints);
                    }
                    break;
                case "move":
                    if (RectSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        RectSecondMoving.Rect.SetX(RectSecondMoving.StartMoveMousePoint.X + RectSecondMoving.StartRectPoint.X - secondRectE.X);
                        //sets y coordinate to first location
                        RectSecondMoving.Rect.SetY(RectSecondMoving.StartMoveMousePoint.Y + RectSecondMoving.StartRectPoint.Y - secondRectE.Y);
                        RectSecondMoving = null;
                        Refresh();
                    }
                    else if (SquareSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        SquareSecondMoving.Square.SetX(SquareSecondMoving.StartMoveMousePoint.X + SquareSecondMoving.StartSquarePoint.X - secondSquareE.X);
                        //sets y coordinate to first location
                        SquareSecondMoving.Square.SetY(SquareSecondMoving.StartMoveMousePoint.Y + SquareSecondMoving.StartSquarePoint.Y - secondSquareE.Y);
                        SquareSecondMoving = null;
                        Refresh();
                    }
                    else if (CircleSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        CircleSecondMoving.Circle.SetX(CircleSecondMoving.StartMoveMousePoint.X + CircleSecondMoving.StartCirclePoint.X - secondCircleE.X);
                        //sets y coordinate to first location
                        CircleSecondMoving.Circle.SetY(CircleSecondMoving.StartMoveMousePoint.Y + CircleSecondMoving.StartCirclePoint.Y - secondCircleE.Y);
                        CircleSecondMoving = null;
                        Refresh();
                    }
                    else if (LineSecondMoving != null)
                    {
                        //sets x1 coordinate to first location
                        LineSecondMoving.Line.SetX1(LineSecondMoving.StartMoveMousePoint.X + LineSecondMoving.StartLinePoint.X - secondLineE.X);
                        //sets y1 coordinate to first location
                        LineSecondMoving.Line.SetY1(LineSecondMoving.StartMoveMousePoint.Y + LineSecondMoving.StartLinePoint.Y - secondLineE.Y);
                        //sets x2 coordinate to first location
                        LineSecondMoving.Line.SetX2(LineSecondMoving.StartMoveMousePoint.X + LineSecondMoving.EndLinePoint.X - secondLineE.X);
                        //sets y2 coordinate to first location
                        LineSecondMoving.Line.SetY2(LineSecondMoving.StartMoveMousePoint.Y + LineSecondMoving.EndLinePoint.Y - secondLineE.Y);
                        LineSecondMoving = null;
                        Refresh();
                    }
                    break;
                case "stretch":
                    Console.WriteLine("strech-undo method");
                    break;
                case "rotateRight":
                    if (LastSelectedRect != null)
                    {
                        LastSelectedRect.RotateClockWise();
                    }
                    if (LastSelectedLine != null)
                    {
                        LastSelectedLine.RotateClockWise(90);
                    }
                    Refresh();
                    break;
                case "rotateHorizontally":
                    if (LastSelectedLine != null)
                    {
                        LastSelectedLine.HorizontalSymmetry();
                    }
                    Refresh();
                    break;
                case "rotateVertically":
                    if (LastSelectedLine != null)
                    {
                        LastSelectedLine.VerticalSymmetry();
                    }
                    Refresh();
                    break;
                case "delete":
                    Rects = tempRects.ToList(); //sends all tempRects elements to Rects list
                    Squares = tempSquares.ToList(); //sends all tempSquares elements to Squares list
                    Circles = tempCircles.ToList(); //sends all tempCircles elements to Circles list
                    Lines = tempLines.ToList(); //sends all tempLines elements to Lines list
                    Polygons = tempPolygons.ToList();//sends all tempPolygons elements to Polygons list
                    Refresh();
                    break;
                case "clear":
                    Console.WriteLine("unexpected error - undo-clear"); // undo-clear method is not implemented
                    break;
                default:
                    Console.WriteLine("unexpected error - beginner coder detected");
                    break;
            }
        }

        private void Help_Button_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            helpForm.Show();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) //using keys are easier than pressing buttons
        {
            if (e.KeyValue == 49)   // 1 in keyboard
                RectangleButton.PerformClick();
            if (e.KeyValue == 50)   // 2 in keyboard
                SquareButton.PerformClick();
            if (e.KeyValue == 51)   // 3 in keyboard
                CircleButton.PerformClick();
            if (e.KeyValue == 52)   // 4 in keyboard
                LineButton.PerformClick();
            if (e.KeyValue == 53)   // 5 in keyboard
                PolygonButton.PerformClick();
            if (e.KeyCode == Keys.S)
                SelectButton.PerformClick();
            if (e.KeyCode == Keys.M)
                MoveButton.PerformClick();
            if (e.KeyCode == Keys.C)
                ClearButton.PerformClick();
            if (e.KeyCode == Keys.E)
                DeleteButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.Z)
                UndoButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.S)
                SaveButton.PerformClick();
        }

        private void WriteToText()
        {
            SaveFileDialog save = new SaveFileDialog
            {
                OverwritePrompt = true, //shows a warning when there is a file with the same name
                CreatePrompt = false,
                Title = "Vector Drawing Application Files",
                DefaultExt = "txt",
                Filter = "txt Files (*.txt)|*.txt|All Files(*.*)|*.*"   //Filters txt files
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.FileName);
                writer.Write("Rects\n");
                for (int i = 0; i < Rects.Count; i++)
                {
                    //writes GraphRect constructor's parameters
                    string rectLine = Rects[i].GetParentId() + " " + Rects[i].Id + " " + Rects[i].StartPoint.X + " "
                        + Rects[i].StartPoint.Y + " " + Rects[i].Width + " " + Rects[i].Height + " " + Rects[i].Fill + " "
                        + Rects[i].size + " " + Rects[i].colour.ToArgb();
                    writer.Write(rectLine + "\n");
                }
                writer.Write("Squares\n");
                for (int i = 0; i < Squares.Count; i++)
                {
                    //writes GraphSquare constructor's parameters
                    string squareLine = Squares[i].GetParentId() + " " + Squares[i].Id + " " + Squares[i].StartPoint.X + " "
                        + Squares[i].StartPoint.Y + " " + Squares[i].Side + " " + Squares[i].Fill + " " + Squares[i].size
                        + " " + Squares[i].colour.ToArgb();
                    writer.Write(squareLine + "\n");
                }
                writer.Write("Circles\n");
                for (int i = 0; i < Circles.Count; i++)
                {
                    //writes GraphCircle constructor's parameters
                    string circleLine = Circles[i].GetParentId() + " " + Circles[i].Id + " " + Circles[i].Center.X + " "
                        + Circles[i].Center.Y + " " + Circles[i].Radius + " " + Circles[i].Fill + " " + Circles[i].size
                        + " " + Circles[i].colour.ToArgb();
                    writer.Write(circleLine + "\n");
                }
                writer.Write("Lines\n");
                for (int i = 0; i < Lines.Count; i++)
                {
                    //writes GraphLine constructor's parameters
                    string lineLine = Lines[i].GetParentId() + " " + Lines[i].Id + " " + Lines[i].StartPoint.X + " "
                        + Lines[i].StartPoint.Y + " " + Lines[i].EndPoint.X + " " + Lines[i].EndPoint.Y + " " + Lines[i].size
                        + " " + Lines[i].colour.ToArgb();
                    writer.Write(lineLine + "\n");
                }
                writer.Write("Polygons\n");

                for (int i = 0; i < Polygons.Count; i++)
                {
                    string polyLine1 = Polygons[i].GetParentId() + " " + Polygons[i].Id + " [";
                    Console.WriteLine("poly1");

                    string[] polyLine2 = new string[100];

                    for (int k = 0; k < Polygons[i].CurvePoints.Length; k++)
                    {
                        //writes GraphPolygon constructor's parameters
                        polyLine2[k] = Polygons[i].CurvePoints[k].X + " " + Polygons[i].CurvePoints[k].Y + " ";
                        Console.WriteLine("poly2");
                    }
                    string polyLine3 = "] " + Polygons[i].size + " " + Polygons[i].colour.ToArgb();
                    writer.Write(polyLine1 + polyLine2[i] + polyLine2[i + 1] + polyLine2[i + 2] + polyLine3 + "\n");
                }

                writer.Close();
            }
        }

        private GraphRect GetRectParent(int p)
        {
            for (int i = 0; i < Rects.Count; i++)
            {
                if (Rects[i].Id == p)
                    return Rects[i];
            }
            return null;
        }

        private GraphSquare GetSquareParent(int p)
        {
            for (int i = 0; i < Squares.Count; i++)
            {
                if (Squares[i].Id == p)
                    return Squares[i];
            }
            return null;
        }

        private GraphCircle GetCircleParent(int p)
        {
            for (int i = 0; i < Circles.Count; i++)
            {
                if (Circles[i].Id == p)
                    return Circles[i];
            }
            return null;
        }

        private GraphLine GetLineParent(int p)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Id == p)
                    return Lines[i];
            }
            return null;
        }

        private GraphPolygon GetPolygonParent(int p)
        {
            for (int i = 0; i < Polygons.Count; i++)
            {
                if (Lines[i].Id == p)
                    return Polygons[i];
            }
            return null;
        }

        string lastFilePath;
        int k, l, m, n;

        private void ReadFromText()
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "txt Files (*.txt)|*.txt|All Files(*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,    //opens the last selected directory
                CheckFileExists = false,
                Title = "Choose a Vector Drawing Application File"
            };

            if (file.ShowDialog() == DialogResult.OK)
            {
                string path = file.FileName;
                string[] lines = File.ReadAllLines(path);

                lastFilePath = path;

                if (lines.Length == 0)
                {
                    return;
                }

                GraphRect rect;
                GraphSquare square;
                GraphCircle circle;
                GraphLine line;
                //GraphPolygon polygon;

                for (int index = 0; index < lines.Length; index++)
                {
                    if (lines[index].Equals("Squares"))
                    {
                        k = index;
                    }

                    if (lines[index].Equals("Circles"))
                    {
                        l = index;
                    }

                    if (lines[index].Equals("Lines"))
                    {
                        m = index;
                    }

                    if (lines[index].Equals("Polygons"))
                    {
                        n = index;
                    }
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] a = lines[i].Split(' ');

                    if (a.Length == 9 && i < k)
                    {
                        //if rectangle doesn't have a parent GraphRect constructor makes parent parameter null
                        if (int.Parse(a[0]) == 0)
                        {
                            rect = new GraphRect(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]), float.Parse(a[5]),
                                int.Parse(a[6]), int.Parse(a[7]), Color.FromArgb(int.Parse(a[8])));
                        }
                        else
                        {
                            //GraphRect initializes parent with GetRectParent() method
                            rect = new GraphRect(GetRectParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]),
                                float.Parse(a[5]), int.Parse(a[6]), int.Parse(a[7]), Color.FromArgb(int.Parse(a[8])));
                        }
                        Rects.Add(rect);
                    }

                    if (a.Length == 8 && i > k && i < l)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            square = new GraphSquare(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]),
                                int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            square = new GraphSquare(GetSquareParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]),
                                int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        Squares.Add(square);
                    }

                    if (a.Length == 8 && i > l && i < m)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            circle = new GraphCircle(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]),
                                int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            circle = new GraphCircle(GetCircleParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]),
                                int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        Circles.Add(circle);
                    }

                    if (a.Length == 8 && i > m && i < n)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            line = new GraphLine(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]),
                                int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            line = new GraphLine(GetLineParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]),
                                int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        Lines.Add(line);
                    }
                    /*
                    if (a.Length == 6 && i > n)
                    {
                        for (int index = 0; i < Polygons.Count; index++)
                        {
                            if (int.Parse(a[0]) == 0)
                            {
                                polygon = new GraphPolygon(null, int.Parse(a[1]), PointF[(a[3])], int.Parse(a[4]),
                                int.Parse(a[5]), Color.FromArgb(int.Parse(a[6])));
                            }
                            else
                            {
                                polygon = new GraphPolygon(GetPolygonParent(int.Parse(a[0])), int.Parse(a[1]), 
                                    PointF.(a[3]), int.Parse(a[4]), int.Parse(a[5]), Color.FromArgb(int.Parse(a[6])));
                            }
                        }
                    }
                    */
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            WriteToText();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            ReadFromText();
            Refresh();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastFilePath != null)
            {
                OpenFileDialog file = new OpenFileDialog();
                string[] alines = File.ReadAllLines(lastFilePath);
                int shapeCount = alines.Count() - 5; //need to increase when new shapelists are added

                if (shapeCount != Rects.Count + Squares.Count + Circles.Count + Lines.Count + Polygons.Count)
                {
                    Console.WriteLine("shapecount" + shapeCount);
                    if (MessageBox.Show("Do you want to save your changes?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SaveButton.PerformClick();
                        this.Close();
                    }
                }
            }
            this.Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            helpForm.Show();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            WriteToText();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            ReadFromText();
            Refresh();
            Console.WriteLine("rects + squares + circles + lines = polygons" + Rects.Count + Squares.Count + Circles.Count + Lines.Count + Polygons.Count);
        }

        private void Vector_Drawing_Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lastFilePath != null)
            {
                OpenFileDialog file = new OpenFileDialog();
                string[] alines = File.ReadAllLines(lastFilePath);
                int shapeCount = alines.Count() - 5; //need to increase when new shapelists are added

                if (shapeCount != Rects.Count + Squares.Count + Circles.Count + Lines.Count + Polygons.Count)
                {
                    Console.WriteLine("shapecount" + shapeCount);
                    if (MessageBox.Show("Do you want to save your changes?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        e.Cancel = true;
                        SaveButton.PerformClick();
                    }
                }
            }
        }
    }
}