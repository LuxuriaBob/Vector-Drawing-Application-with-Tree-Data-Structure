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
        bool drawCurve = false;
        bool select = false;
        bool move = false;
        bool stretch = false;

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
        GraphCurve SelectedCurve = null;

        RectMoveInfo RectMoving = null; //move info for selected rect
        SquareMoveInfo SquareMoving = null; //move info for selected square
        CircleMoveInfo CircleMoving = null; //move info for selected circle
        LineMoveInfo LineMoving = null; //move info for selected line
        PolygonMoveInfo PolygonMoving = null;//move info for selected polygon
        CurveMoveInfo CurveMoving = null;//move info for selected curve

        RectMoveInfo RectSecondMoving = null;   //stores rect-moving for undo-move method
        Point secondRectE;  //stores rect-coordinates for undo-move

        SquareMoveInfo SquareSecondMoving = null;   //storessquare moving for undo-move method
        Point secondSquareE;  //stores square-coordinates for undo-move

        CircleMoveInfo CircleSecondMoving = null;   //stores circle moving for undo-move method
        Point secondCircleE;  //stores circle-coordinates for undo-move

        LineMoveInfo LineSecondMoving = null;   //stores line moving for undo-move method
        Point secondLineE;  //stores line-coordinates for undo-move

        Point startlocation;    //shape's top left coordinates
        Point endlocation;      //shape's bottom right coordinates
        PointF cornerlocation;  //red corner coordinates
        Point StretchLocation;  //mouselocation for stretch method 
        Point PolyMoveLocation; //stores polygonmove location for move method
        PointF CurveMoveLocation; //stores curvemove location for move method
        PointF SecondCornerLocation;    //stores initial red corner coordinates for undo-move method

        int jindex;
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
                }

                RefreshCurveSelection(e.Location);
                if (this.SelectedCurve != null && CurveMoving == null)
                {
                    this.Capture = true;
                    CurveMoving = new CurveMoveInfo   //sets Moving class properties for moving Curve
                    {
                        Curve = this.SelectedCurve,
                        CurvePoints = SelectedCurve.CurvePoints,
                        StartMoveMousePoint = e.Location
                    };
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

                PolygonHitTest(Polygons, e.Location);
                RefreshPolygonSelection(e.Location);

                CurveHitTest(Curves, e.Location);
                RefreshCurveSelection(e.Location);

                Refresh();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.Location.ToString();
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
                    //sets x coordinate of a moving rect
                    RectMoving.Rect.SetX(RectMoving.StartRectPoint.X + e.X - RectMoving.StartMoveMousePoint.X);
                    //sets y coordinate of a moving rect
                    RectMoving.Rect.SetY(RectMoving.StartRectPoint.Y + e.Y - RectMoving.StartMoveMousePoint.Y); 
                }
                RefreshRectSelection(e.Location);

                if (SquareMoving != null)
                {
                    //sets x coordinate of a moving square
                    SquareMoving.Square.SetX(SquareMoving.StartSquarePoint.X + e.X - SquareMoving.StartMoveMousePoint.X);
                    //sets y coordinate of a moving square
                    SquareMoving.Square.SetY(SquareMoving.StartSquarePoint.Y + e.Y - SquareMoving.StartMoveMousePoint.Y); 
                }
                RefreshSquareSelection(e.Location);

                if (CircleMoving != null)
                {
                    //sets x coordinate of a moving circle
                    CircleMoving.Circle.SetX(CircleMoving.StartCirclePoint.X + e.X - CircleMoving.StartMoveMousePoint.X);
                    //sets y coordinate of a moving circle
                    CircleMoving.Circle.SetY(CircleMoving.StartCirclePoint.Y + e.Y - CircleMoving.StartMoveMousePoint.Y); 
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
                RefreshPolygonSelection(e.Location);
                RefreshCurveSelection(e.Location);
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
                    this.Capture = false;
                    PolygonMoving = null;
                }
                else if (CurveMoving != null)
                {
                    this.Capture = false;
                    CurveMoving = null;
                }
                RefreshRectSelection(e.Location);
                RefreshSquareSelection(e.Location);
                RefreshCircleSelection(e.Location);
                RefreshLineSelection(e.Location);
                RefreshPolygonSelection(e.Location);
                RefreshCurveSelection(e.Location);
            }
        }

        public List<GraphPolygon> Polygons = new List<GraphPolygon>();   // main Polygons list
        public List<PointF> polygonPoints = new List<PointF>();

        public List<GraphCurve> Curves = new List<GraphCurve>();   // main Curves list
        public List<PointF> curvePoints = new List<PointF>();

        float MoveX;
        float MoveY;

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawPolygon)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        //get points for polygon
                        polygonPoints.Add((new PointF(e.X, e.Y)));
                        if (polygonPoints.Count > 1)
                        {
                            //draw line
                            this.DrawLine(polygonPoints[polygonPoints.Count - 2], polygonPoints[polygonPoints.Count - 1]);
                        }
                        break;
                    case MouseButtons.Right:
                        //finish polygon
                        if (polygonPoints.Count() > 2)
                        {
                            this.DrawLine(polygonPoints[polygonPoints.Count - 1], polygonPoints[0]);
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
                            Refresh();
                        }
                        break;
                }
            }
            else if (drawCurve)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        //get points for curve
                        curvePoints.Add((new PointF(e.X, e.Y)));
                        break;
                    case MouseButtons.Right:
                        //finish curve
                        if (curvePoints.Count() > 2)
                        {
                            if (FillColorCheckBox.Checked)
                            {
                                int fill = 1;
                                GraphCurve curve = new GraphCurve(SelectedCurve, Curves.Count + 1,
                                    curvePoints.ToArray(), fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                                Curves.Add(curve);   //adds to Curves list
                            }
                            else
                            {
                                int fill = 0;
                                GraphCurve curve = new GraphCurve(SelectedCurve, Curves.Count + 1,
                                    curvePoints.ToArray(), fill, Convert.ToInt32(numericUpDown1.Value), colorPicker.Color);
                                Curves.Add(curve);   //adds to Curves list
                            }
                            drawCurve = false;
                            curvePoints.Clear();
                            Refresh();
                        }
                        break;
                }
            }
            else if (select)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (Math.Abs(e.X - Lines[i].StartPoint.X) < 20 && Math.Abs(e.Y - Lines[i].StartPoint.Y) < 20)
                    {
                        cornerlocation = Lines[i].StartPoint;
                        SecondCornerLocation = cornerlocation;
                    }
                    else if (Math.Abs(e.X - Lines[i].EndPoint.X) < 20 && Math.Abs(e.Y - Lines[i].EndPoint.Y) < 20)
                    {
                        cornerlocation = Lines[i].EndPoint;
                        SecondCornerLocation = cornerlocation;
                    }
                }

                for (int i = 0; i < Rects.Count; i++)
                {
                    //top left corner of rectangle
                    if (Math.Abs(e.X - Rects[i].StartPoint.X) < 20 && Math.Abs(e.Y - Rects[i].StartPoint.Y) < 20)
                    {
                        cornerlocation = Rects[i].StartPoint;
                        SecondCornerLocation = cornerlocation;
                    }
                    //bottom left corner of rectangle
                    else if (Math.Abs(e.X - Rects[i].StartPoint.X) < 20 && Math.Abs(e.Y - (Rects[i].StartPoint.Y + Rects[i].Height)) < 20)
                    {
                        cornerlocation = new PointF(Rects[i].StartPoint.X, Rects[i].StartPoint.Y + Rects[i].Height);
                        SecondCornerLocation = cornerlocation;
                    }
                    //top right corner of rectangle
                    else if (Math.Abs(e.X - (Rects[i].StartPoint.X + Rects[i].Width)) < 20 && Math.Abs(e.Y - Rects[i].StartPoint.Y) < 20)
                    {
                        cornerlocation = new PointF(Rects[i].StartPoint.X + Rects[i].Width, Rects[i].StartPoint.Y);
                        SecondCornerLocation = cornerlocation;
                    }
                    //bottom right corner of rectangle
                    else if (Math.Abs(e.X - (Rects[i].StartPoint.X + Rects[i].Width)) < 20 && Math.Abs(e.Y - (Rects[i].StartPoint.Y + Rects[i].Height)) < 20)
                    {
                        cornerlocation = new PointF(Rects[i].StartPoint.X + Rects[i].Width, Rects[i].StartPoint.Y + Rects[i].Height);
                        SecondCornerLocation = cornerlocation;
                    }
                }

                for (int i = 0; i < Squares.Count; i++)
                {
                    //top left corner of square
                    if (Math.Abs(e.X - Squares[i].StartPoint.X) < 20 && Math.Abs(e.Y - Squares[i].StartPoint.Y) < 20)
                    {
                        cornerlocation = Squares[i].StartPoint;
                        SecondCornerLocation = cornerlocation;
                    }
                    //bottom left corner of square
                    else if (Math.Abs(e.X - Squares[i].StartPoint.X) < 20 &&
                        Math.Abs(e.Y - (Squares[i].StartPoint.Y + Squares[i].Side)) < 20)
                    {
                        cornerlocation = new PointF(Squares[i].StartPoint.X, Squares[i].StartPoint.Y + Squares[i].Side);
                        SecondCornerLocation = cornerlocation;
                    }
                    //top right corner of square
                    else if (Math.Abs(e.X - (Squares[i].StartPoint.X + Squares[i].Side)) < 20 &&
                        Math.Abs(e.Y - Squares[i].StartPoint.Y) < 20)
                    {
                        cornerlocation = new PointF(Squares[i].StartPoint.X + Squares[i].Side, Squares[i].StartPoint.Y);
                        SecondCornerLocation = cornerlocation;
                    }
                    //bottom right corner of square
                    else if (Math.Abs(e.X - (Squares[i].StartPoint.X + Squares[i].Side)) < 20 &&
                        Math.Abs(e.Y - (Squares[i].StartPoint.Y + Squares[i].Side)) < 20)
                    {
                        cornerlocation = new PointF(Squares[i].StartPoint.X + Squares[i].Side, Squares[i].StartPoint.Y + Squares[i].Side);
                        SecondCornerLocation = cornerlocation;
                    }
                }

                for (int i = 0; i < Polygons.Count; i++)
                {
                    for (int j = 0; j < Polygons[i].CurvePoints.Length; j++)
                    {
                        if (Math.Abs(e.X - Polygons[i].CurvePoints[j].X) < 20 && (Math.Abs(e.Y - Polygons[i].CurvePoints[j].Y) < 20))
                        {
                            cornerlocation = Polygons[i].CurvePoints[j];
                            jindex = j;
                            SecondCornerLocation = cornerlocation;
                        }
                    }
                }

                for (int i = 0; i < Curves.Count; i++)
                {
                    for (int j = 0; j < Curves[i].CurvePoints.Length; j++)
                    {
                        if (Math.Abs(e.X - Curves[i].CurvePoints[j].X) < 20 && (Math.Abs(e.Y - Curves[i].CurvePoints[j].Y) < 20))
                        {
                            cornerlocation = Curves[i].CurvePoints[j];
                            jindex = j;
                            SecondCornerLocation = cornerlocation;
                        }
                    }
                }

                for (int i = 0; i < Circles.Count; i++)
                {
                    if (SelectedCircle != null)
                    {
                        cornerlocation = SelectedCircle.Center;
                        SecondCornerLocation = new PointF(SelectedCircle.Center.X + SelectedCircle.Radius, SelectedCircle.Center.Y);
                    }
                }
            }
            else if (stretch)
            {
                StretchLocation = e.Location;

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (SelectedLine != null)
                        {
                            if (cornerlocation == SelectedLine.StartPoint)
                            {
                                SelectedLine.Stretch(StretchLocation, cornerlocation);
                                cornerlocation = SelectedLine.StartPoint;
                            }
                            else if (cornerlocation == SelectedLine.EndPoint)
                            {
                                SelectedLine.Stretch(StretchLocation, cornerlocation);
                                cornerlocation = SelectedLine.EndPoint;
                            }
                        }
                        else if (SelectedRect != null)
                        {
                            //top left
                            if (cornerlocation == SelectedRect.StartPoint)
                            {
                                SelectedRect.Stretch(cornerlocation, StretchLocation);
                                cornerlocation = SelectedRect.StartPoint;
                            }
                            //bottom left
                            else if (cornerlocation.X == SelectedRect.StartPoint.X
                                && cornerlocation.Y == SelectedRect.StartPoint.Y + SelectedRect.Height)
                            {
                                SelectedRect.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedRect.StartPoint.X;
                                cornerlocation.Y = SelectedRect.StartPoint.Y + SelectedRect.Height;
                            }
                            //top right
                            else if (cornerlocation.X == SelectedRect.StartPoint.X + SelectedRect.Width
                                && cornerlocation.Y == SelectedRect.StartPoint.Y)
                            {
                                SelectedRect.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedRect.StartPoint.X + SelectedRect.Width;
                                cornerlocation.Y = SelectedRect.StartPoint.Y;
                            }
                            //bottom right
                            else if (cornerlocation.X == SelectedRect.StartPoint.X + SelectedRect.Width
                                && cornerlocation.Y == SelectedRect.StartPoint.Y + SelectedRect.Height)
                            {
                                SelectedRect.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedRect.StartPoint.X + SelectedRect.Width;
                                cornerlocation.Y = SelectedRect.StartPoint.Y + SelectedRect.Height;
                            }
                        }
                        else if (SelectedSquare != null)
                        {
                            //top left
                            if (cornerlocation == SelectedSquare.StartPoint)
                            {
                                SelectedSquare.Stretch(cornerlocation, StretchLocation);
                                cornerlocation = SelectedSquare.StartPoint;
                            }
                            //bottom left
                            else if (cornerlocation.X == SelectedSquare.StartPoint.X
                                && cornerlocation.Y == SelectedSquare.StartPoint.Y + SelectedSquare.Side)
                            {
                                SelectedSquare.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedSquare.StartPoint.X;
                                cornerlocation.Y = SelectedSquare.StartPoint.Y + SelectedSquare.Side;
                            }
                            //top right
                            else if (cornerlocation.X == SelectedSquare.StartPoint.X + SelectedSquare.Side
                                && cornerlocation.Y == SelectedSquare.StartPoint.Y)
                            {
                                SelectedSquare.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedSquare.StartPoint.X + SelectedSquare.Side;
                                cornerlocation.Y = SelectedSquare.StartPoint.Y;
                            }
                            //bottom right
                            else if (cornerlocation.X == SelectedSquare.StartPoint.X + SelectedSquare.Side
                                && cornerlocation.Y == SelectedSquare.StartPoint.Y + SelectedSquare.Side)
                            {
                                SelectedSquare.Stretch(cornerlocation, StretchLocation);
                                cornerlocation.X = SelectedSquare.StartPoint.X + SelectedSquare.Side;
                                cornerlocation.Y = SelectedSquare.StartPoint.Y + SelectedSquare.Side;
                            }
                        }
                        else if (SelectedPolygon != null)
                        {
                            SelectedPolygon.Stretch(StretchLocation, jindex);
                            cornerlocation = SelectedPolygon.CurvePoints[jindex];
                        }
                        else if (SelectedCurve != null)
                        {
                            SelectedCurve.Stretch(StretchLocation, jindex);
                            cornerlocation = SelectedCurve.CurvePoints[jindex];
                        }
                        else if (SelectedCircle != null)
                        {
                            SelectedCircle.Stretch(e.Location);
                            cornerlocation = SelectedCircle.Center;
                        }
                        break;
                }
                Refresh();
            }
            else if (move)
            {
                PolyMoveLocation = e.Location;

                if (SelectedPolygon != null)
                {
                    MoveX = PolyMoveLocation.X - SelectedPolygon.CurvePoints[jindex].X;
                    MoveY = PolyMoveLocation.Y - SelectedPolygon.CurvePoints[jindex].Y;

                    SelectedPolygon.Move(PolyMoveLocation, MoveX, MoveY);
                    cornerlocation = SelectedPolygon.CurvePoints[jindex];
                }

                CurveMoveLocation = e.Location;

                if (SelectedCurve != null)
                {
                    MoveX = CurveMoveLocation.X - SelectedCurve.CurvePoints[jindex].X;
                    MoveY = CurveMoveLocation.Y - SelectedCurve.CurvePoints[jindex].Y;

                    SelectedCurve.Move(CurveMoveLocation, MoveX, MoveY);
                    cornerlocation = SelectedCurve.CurvePoints[jindex];
                }
            }
        }

        private void DrawLine(PointF p1, PointF p2)
        {
            using (Graphics G = this.CreateGraphics())
            {
                G.InterpolationMode = InterpolationMode.High; //determines how intermediate values between two endpoints are calculated.
                G.SmoothingMode = SmoothingMode.HighQuality;  //antialiasing

                Pen pen = new Pen(colorPicker.Color)
                {
                    Width = Convert.ToInt32(numericUpDown1.Value)
                };
                G.DrawLine(pen, p1, p2);
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
            //determines x coordinate of top left corner by determining which is smaller
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;
            //determines y coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    
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
            //determines x coordinate of top left corner by determining which is smaller
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;
            //determines y coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    
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
                var color = rect == SelectedRect ? Color.Blue : rect.GetColour();    
                //rect.GetColour(square)SelectedSquare is blue, others are colors selected from colorpicker
                var pen = new Pen(color, rect.size);   //selected colour, rect's size

                if (rect.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   
                    //draws rect in Rects list
                }
                else
                {
                    e.Graphics.FillRectangle(pen.Brush, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   
                    //fills rect in Rrects list
                }
            }

            if (SelectedRect != null)
            {
                if (!move)
                {
                    if (cornerlocation != null)
                    {
                        var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                        e.Graphics.FillRectangle(pen.Brush, cornerlocation.X - 10, cornerlocation.Y - 10, 20, 20);
                    }
                }
            }
            else if (SelectedRect == null && SelectedLine == null && SelectedPolygon == null &&
                SelectedCircle == null && SelectedSquare == null && SelectedCurve == null)
            {
                cornerlocation = new PointF(0, 0);
            }

            foreach (var square in Squares)
            {
                var color = square == SelectedSquare ? Color.Blue : square.GetColour();    
                //square.GetColour(rect)SelectedSquare is blue, others are colors selected from colorpicker
                var size = Convert.ToInt32(numericUpDown1.Value);
                var pen = new Pen(color, square.size);   //selected colour, square's size

                if (square.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   
                    //draws square in Squares list
                }
                else
                {
                    e.Graphics.FillRectangle(pen.Brush, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   //fills square in Squares list
                }
            }

            if (SelectedSquare != null)
            {
                if (!move)
                {
                    if (cornerlocation != null)
                    {
                        var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                        e.Graphics.FillRectangle(pen.Brush, cornerlocation.X - 10, cornerlocation.Y - 10, 20, 20);
                    }
                }
            }

            foreach (var circle in Circles)
            {
                var color = circle == SelectedCircle ? Color.Blue : circle.GetColour();    
                //circle.GetColour(rect)SelectedCircle is blue, others are colors selected from colorpicker
                var pen = new Pen(color, circle.size);   //selected colour, circle's size

                if (circle.Fill == 0)
                {
                    e.Graphics.DrawEllipse(pen, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, 
                        circle.Radius + circle.Radius, circle.Radius + circle.Radius);   //draws circle in Circles list
                }
                else
                {
                    e.Graphics.FillEllipse(pen.Brush, circle.Center.X - circle.Radius, circle.Center.Y - circle.Radius, 
                        circle.Radius + circle.Radius, circle.Radius + circle.Radius);   //fills square in Rects list
                }
            }

            if (SelectedCircle != null)
            {
                if (!move)
                {
                    if (cornerlocation == SelectedCircle.Center)
                    {
                        var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                        if (SelectedCircle.Fill == 0)
                        {
                            e.Graphics.DrawEllipse(pen, SelectedCircle.Center.X - SelectedCircle.Radius, SelectedCircle.Center.Y - SelectedCircle.Radius,
                            SelectedCircle.Radius + SelectedCircle.Radius, SelectedCircle.Radius + SelectedCircle.Radius);
                        }
                        else
                        {
                            e.Graphics.FillEllipse(pen.Brush, SelectedCircle.Center.X - SelectedCircle.Radius, SelectedCircle.Center.Y - SelectedCircle.Radius,
                            SelectedCircle.Radius + SelectedCircle.Radius, SelectedCircle.Radius + SelectedCircle.Radius);
                        }
                    }
                }
            }

            foreach (var line in Lines)
            {
                var color = line == SelectedLine ? Color.Blue : line.GetColour();    
                //line.GetColour(rect) SelectedLine is blue, others are colors selected from colorpicker
                var pen = new Pen(color, line.size);   //selected colour, circle's size

                e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);   //draws circle in Circles list
            }

            if (SelectedLine != null)
            {
                if (!move)
                {
                    if (cornerlocation != null)
                    {
                        var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                        e.Graphics.FillRectangle(pen.Brush, cornerlocation.X - 7, cornerlocation.Y - 7, 15, 15);
                    }
                }
            }

            foreach (var polygon in Polygons)
            {
                var color = polygon == SelectedPolygon ? Color.Blue : polygon.GetColour();    
                //polygon.GetColour(polygon)Selectedpolygon is blue, others are colors selected from colorpicker
                var pen = new Pen(color, polygon.size);   //selected colour, polygon's size

                if (polygon.Fill == 0)
                {
                    e.Graphics.DrawPolygon(pen, polygon.CurvePoints);   //draws polygon in polygons list
                }
                else
                {
                    e.Graphics.FillPolygon(pen.Brush, polygon.CurvePoints);   //fills polygon in polygons list
                }
            }

            if (SelectedPolygon != null)
            {
                if (cornerlocation != null)
                {
                    var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                    e.Graphics.FillRectangle(pen.Brush, cornerlocation.X - 9, cornerlocation.Y - 9, 20, 20);
                }
            }

            foreach (var curve in Curves)
            {
                var color = curve == SelectedCurve ? Color.Blue : curve.GetColour();    
                //curve.GetColour(curve)Selectedcurve is blue, others are colors selected from colorpicker
                var pen = new Pen(color, curve.size);   //selected colour, curve's size

                if (curve.Fill == 0)
                {
                    e.Graphics.DrawCurve(pen, curve.CurvePoints);   //draws curve in curves list
                }
                else
                {
                    e.Graphics.FillClosedCurve(pen.Brush, curve.CurvePoints);   //fills curve in curves list
                }
            }

            if (SelectedCurve != null)
            {
                if (cornerlocation != null)
                {
                    var pen = new Pen(Brushes.Red, 10);   //selected colour, rect's size
                    e.Graphics.FillRectangle(pen.Brush, cornerlocation.X - 9, cornerlocation.Y - 9, 20, 20);
                }
            }
        }

        private GraphRect RectangleHitTest(List<GraphRect> rects, Point p)
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

        private GraphSquare SquareHitTest(List<GraphSquare> squares, Point p)
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

        private GraphCircle CircleHitTest(List<GraphCircle> circles, Point p)
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

        private GraphLine LineHitTest(List<GraphLine> lines, Point p)
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

        private GraphPolygon PolygonHitTest(List<GraphPolygon> polygons, Point p)
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

        private GraphCurve CurveHitTest(List<GraphCurve> curves, Point p)
        {
            var size = 10;
            var buffer = new Bitmap(size * 2, size * 2);
            PointF[] curvePoints = new PointF[100];

            foreach (var curve in curves)
            {
                for (int i = 0; i < curve.CurvePoints.Count(); i++)
                {
                    curvePoints[i] = new PointF(curve.CurvePoints[i].X - p.X + size,
                        curve.CurvePoints[i].Y - p.Y + size);
                }

                //draws each curve on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a Curve by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawCurve(new Pen(Color.Green, 10), curvePoints);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return curve;
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

        GraphCurve LastSelectedCurve = null;

        private void RefreshCurveSelection(Point point)
        {
            if (select)
            {
                var selectedCurve = CurveHitTest(Curves, point);
                if (selectedCurve != this.SelectedCurve)
                {
                    this.SelectedCurve = selectedCurve;
                    LastSelectedCurve = selectedCurve;    //stores selectedCurve for undo-select method
                    this.Invalidate();
                }
                if (CurveMoving != null)
                    this.Invalidate();
            }
            this.Invalidate();
        }

        string lastButton = null;   //stores last pressed button
        
        private void MakeAllFalse(ref bool r, ref bool s, ref bool t, ref bool u, ref bool v, ref bool w, ref bool x, 
            ref bool y, ref bool z)
        {
            //make every parameter false
            r = false;
            s = false;
            t = false;
            u = false;
            v = false;
            w = false;
            x = false;
            y = false;
            z = false;
        }

        private void MakeLastTrue(ref bool r, ref bool s, ref bool t, ref bool u, ref bool v, ref bool w, ref bool x, 
            ref bool y, ref bool z)
        {
            //make last parameter true
            r = false;
            s = false;
            t = false;
            u = false;
            v = false;
            w = false;
            x = false;
            y = false;
            z = true;
        }

        private void MakeSelectionNull(ref GraphRect selectedRect, ref GraphSquare selectedSquare, 
            ref GraphCircle selectedCircle, ref GraphLine selectedLine, ref GraphPolygon selectedPolygon, 
            ref GraphCurve selectedCurve)
        {
            selectedRect = null;
            selectedSquare = null;
            selectedCircle = null;
            selectedLine = null;
            selectedPolygon = null;
            SelectedCurve = null;
        }

        private void MakeMovingNull(ref RectMoveInfo RectMoving, ref SquareMoveInfo SquareMoving, 
            ref CircleMoveInfo CircleMoving, ref LineMoveInfo LiveMoving, ref PolygonMoveInfo PolygonMoving, ref CurveMoveInfo CurveMoving)
        {
            RectMoving = null;
            SquareMoving = null;
            CircleMoving = null;
            LineMoving = null;
            PolygonMoving = null;
            CurveMoving = null;
        }

        private void MakeListsEmpty(ref List<GraphRect> Rects, ref List<GraphSquare> Squares, 
            ref List<GraphCircle> Circles, ref List<GraphLine> Lines, ref List<GraphPolygon> Polygons, 
            ref List<GraphCurve> Curves)
        {
            Rects.Clear();
            Squares.Clear();
            Circles.Clear();
            Lines.Clear();
            Polygons.Clear();
            Curves.Clear();
        }

        private void RectButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawRect";
            rulerLabel.Text = "Rectangle";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, 
                ref select, ref move, ref drawRect);
        }

        private void SquareButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawSquare";
            rulerLabel.Text = "Square";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawRect, ref drawCircle, ref drawLine, ref drawPolygon, 
                ref select, ref move, ref drawSquare);
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawCircle";
            rulerLabel.Text = "Circle";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawRect, ref drawLine, ref drawPolygon, 
                ref select, ref move, ref drawCircle);
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawLine";
            rulerLabel.Text = "Line";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawRect, ref drawCircle, ref drawPolygon, 
                ref select, ref move, ref drawLine);
        }

        private void PolygonButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawPolygon";
            rulerLabel.Text = "Polygon";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawRect, ref drawCircle, ref drawLine, 
                ref select, ref move, ref drawPolygon);
        }

        private void CurveButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawCurve";
            rulerLabel.Text = "Curve";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref stretch, ref drawSquare, ref drawRect, ref drawCircle, ref drawLine,
                ref select, ref move, ref drawPolygon, ref drawCurve);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            rulerLabel.Text = "Select";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawCircle, ref drawRect, ref drawLine, 
                ref drawPolygon, ref move, ref select);
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            rulerLabel.Text = "Move";
            cornerlocation = new PointF(0, 0);
            this.Cursor = Cursors.SizeAll;
            MakeLastTrue(ref drawCurve, ref stretch, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, 
                ref select, ref drawRect, ref move);
        }

        private void StretchButton_Click(object sender, EventArgs e)
        {
            lastButton = "stretch";
            rulerLabel.Text = "Stretch";
            this.Cursor = Cursors.Arrow;
            MakeLastTrue(ref drawCurve, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref select, 
                ref drawRect, ref move, ref stretch);
        }

        private void RulerButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Ruler";
            this.Cursor = Cursors.Arrow;
            if (SelectedRect != null)
            {
                rulerLabel.Text += "\nRectangle's length is " + SelectedRect.GetLength().ToString("0.00") + "u" + "\n" +
                    "Rectangle's area is " + SelectedRect.GetArea().ToString("0.00") + "u²";
            }
            if (SelectedSquare != null)
            {
                rulerLabel.Text += "\nSquare's length is " + SelectedSquare.GetLength().ToString("0.00") + "u" + "\n" +
                    "Square's area is " + SelectedSquare.GetArea().ToString("0.00") + "u²";
            }
            if (SelectedCircle != null)
            {
                rulerLabel.Text += "\nCircle's length is " + SelectedCircle.GetLength().ToString("0.00") + "u" + "\n" +
                    "Circle's area is " + SelectedCircle.GetArea().ToString("0.00") + "u²";
            }
            if (SelectedLine != null)
            {
                rulerLabel.Text += "\nLine's length is " + SelectedLine.GetLength().ToString("0.00") + "u";
            }
            if (SelectedPolygon != null)
            {
                rulerLabel.Text += "\nPolygon's length is " + SelectedPolygon.GetLength().ToString("0.00") + "u" + "\n" +
                    "Polygon's area is " + SelectedPolygon.GetArea().ToString("0.00") + "u²";
            }
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
            if (SelectedPolygon != null)
            {
                if (cornerlocation != null)
                {
                    SelectedPolygon.HorizontalSymmetry(cornerlocation);
                }
            }
            if (SelectedCurve != null)
            {
                if (cornerlocation != null)
                {
                    SelectedCurve.HorizontalSymmetry(cornerlocation);
                }
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
            if (SelectedPolygon != null)
            {
                if(cornerlocation != null)
                {
                    SelectedPolygon.VerticalSymmetry(cornerlocation);
                }
            }
            if (SelectedCurve != null)
            {
                if (cornerlocation != null)
                {
                    SelectedCurve.VerticalSymmetry(cornerlocation);
                }
            }
            Refresh();
        }

        #region Test

        public List<GraphRect> tempRects = new List<GraphRect>();   //temporary rects list for undo-draw method
        public List<GraphSquare> tempSquares = new List<GraphSquare>();   //temporary rects list for undo-draw method
        public List<GraphCircle> tempCircles = new List<GraphCircle>();   //temporary circles list for undo-draw method
        public List<GraphLine> tempLines = new List<GraphLine>();   //temporary lines list for undo-draw method
        public List<GraphPolygon> tempPolygons = new List<GraphPolygon>();  //temporary polygons list for undo-draw method
        public List<GraphCurve> tempCurves = new List<GraphCurve>();  //temporary Curves list for undo-draw method

        #endregion

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            lastButton = "delete";
            rulerLabel.Text = "Delete";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref drawCurve, ref select, ref move, ref stretch);

            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method
            tempSquares = Squares.ToList(); //stores Squares list in tempSquares for undo method
            tempCircles = Circles.ToList(); //stores Circles list in tempCircles for undo method
            tempLines = Lines.ToList(); //stores Lines list in tempLines for undo method
            tempPolygons = Polygons.ToList(); //stores Polygons list in tempPolygons for undo method
            tempCurves = Curves.ToList();//stores Curves list in tempCurves for undo method

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
                if (Circles.Contains(SelectedCircle))    //delete selected circle if Circles list contains
                {
                    SelectedCircle.DeleteCircles(Circles);
                    Circles.Remove(SelectedCircle);
                }
            }

            if (Lines.Count != 0)   //Exception for empty list
            {
                if (Lines.Contains(SelectedLine))    //delete selected line if Lines list contains
                {
                    SelectedLine.DeleteLines(Lines);
                    Lines.Remove(SelectedLine);
                }
            }

            if (Polygons.Count != 0)   //Exception for empty list
            {
                if (Polygons.Contains(SelectedPolygon))    //delete selected polygon if Polygons list contains
                {
                    SelectedPolygon.DeletePolygons(Polygons);
                    Polygons.Remove(SelectedPolygon);
                }
            }

            if (Curves.Count != 0)   //Exception for empty list
            {
                if (Curves.Contains(SelectedCurve))    //delete selected Curve if Curves list contains
                {
                    SelectedCurve.DeleteCurves(Curves);
                    Curves.Remove(SelectedCurve);
                }
            }
            Refresh();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            rulerLabel.Text = "Clear";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref drawCurve, 
                ref select, ref move, ref stretch);
            MakeSelectionNull(ref this.SelectedRect, ref this.SelectedSquare, ref this.SelectedCircle, ref this.SelectedLine,
                ref this.SelectedPolygon, ref this.SelectedCurve);
            MakeMovingNull(ref RectMoving, ref SquareMoving, ref CircleMoving, ref LineMoving, ref PolygonMoving, ref CurveMoving);
            MakeListsEmpty(ref Rects, ref Squares, ref Circles, ref Lines, ref Polygons, ref Curves);
            this.Capture = false;
            Refresh();
        }

        Graphics K = null;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Undo";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(ref drawRect, ref drawSquare, ref drawCircle, ref drawLine, ref drawPolygon, ref drawCurve, 
                ref select, ref move, ref stretch);

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
                case "drawCurve":
                    if (Curves.Count > 0)
                    {
                        Curves.RemoveAt(Curves.Count - 1);    //removes last drawn Curve in Curves list
                    }
                    Refresh();
                    break;
                case "select":
                    using (K = this.CreateGraphics()) { 
                        if (LastSelectedRect != null)
                        {
                            var pen = new Pen(LastSelectedRect.colour, LastSelectedRect.size);
                            K.DrawRectangle(pen, LastSelectedRect.StartPoint.X, LastSelectedRect.StartPoint.Y,
                                LastSelectedRect.Width, LastSelectedRect.Height);
                        }
                        if (LastSelectedSquare != null)
                        {
                            var pen = new Pen(LastSelectedSquare.colour, LastSelectedSquare.size);
                            K.DrawRectangle(pen, LastSelectedSquare.StartPoint.X, LastSelectedSquare.StartPoint.Y,
                                LastSelectedSquare.Side, LastSelectedSquare.Side);
                        }
                        if (LastSelectedCircle != null)
                        {
                            var pen = new Pen(LastSelectedCircle.colour, LastSelectedCircle.size);
                            K.DrawEllipse(pen, LastSelectedCircle.Center.X - LastSelectedCircle.Radius,
                                LastSelectedCircle.Center.Y - LastSelectedCircle.Radius,
                                LastSelectedCircle.Radius + LastSelectedCircle.Radius,
                                LastSelectedCircle.Radius + LastSelectedCircle.Radius);
                        }
                        if (LastSelectedLine != null)
                        {
                            var pen = new Pen(LastSelectedLine.colour, LastSelectedLine.size);
                            K.DrawLine(pen, LastSelectedLine.StartPoint.X, LastSelectedLine.StartPoint.Y,
                                LastSelectedLine.EndPoint.X, LastSelectedLine.EndPoint.Y);
                        }
                        if (LastSelectedPolygon != null)
                        {
                            var pen = new Pen(LastSelectedPolygon.colour, LastSelectedPolygon.size);
                            K.DrawPolygon(pen, LastSelectedPolygon.CurvePoints);
                        }
                        if (LastSelectedCurve != null)
                        {
                            var pen = new Pen(LastSelectedCurve.colour, LastSelectedCurve.size);
                            K.DrawCurve(pen, LastSelectedCurve.CurvePoints);
                        }
                    }
                    break;
                case "move":
                    if (RectSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        RectSecondMoving.Rect.SetX(RectSecondMoving.StartMoveMousePoint.X + 
                            RectSecondMoving.StartRectPoint.X - secondRectE.X);
                        //sets y coordinate to first location
                        RectSecondMoving.Rect.SetY(RectSecondMoving.StartMoveMousePoint.Y + 
                            RectSecondMoving.StartRectPoint.Y - secondRectE.Y);
                        RectSecondMoving = null;
                        Refresh();
                    }
                    else if (SquareSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        SquareSecondMoving.Square.SetX(SquareSecondMoving.StartMoveMousePoint.X + 
                            SquareSecondMoving.StartSquarePoint.X - secondSquareE.X);
                        //sets y coordinate to first location
                        SquareSecondMoving.Square.SetY(SquareSecondMoving.StartMoveMousePoint.Y + 
                            SquareSecondMoving.StartSquarePoint.Y - secondSquareE.Y);
                        SquareSecondMoving = null;
                        Refresh();
                    }
                    else if (CircleSecondMoving != null)
                    {
                        //sets x coordinate to first location
                        CircleSecondMoving.Circle.SetX(CircleSecondMoving.StartMoveMousePoint.X + 
                            CircleSecondMoving.StartCirclePoint.X - secondCircleE.X);
                        //sets y coordinate to first location
                        CircleSecondMoving.Circle.SetY(CircleSecondMoving.StartMoveMousePoint.Y + 
                            CircleSecondMoving.StartCirclePoint.Y - secondCircleE.Y);
                        CircleSecondMoving = null;
                        Refresh();
                    }
                    else if (LineSecondMoving != null)
                    {
                        //sets x1 coordinate to first location
                        LineSecondMoving.Line.SetX1(LineSecondMoving.StartMoveMousePoint.X + 
                            LineSecondMoving.StartLinePoint.X - secondLineE.X);
                        //sets y1 coordinate to first location
                        LineSecondMoving.Line.SetY1(LineSecondMoving.StartMoveMousePoint.Y + 
                            LineSecondMoving.StartLinePoint.Y - secondLineE.Y);
                        //sets x2 coordinate to first location
                        LineSecondMoving.Line.SetX2(LineSecondMoving.StartMoveMousePoint.X + 
                            LineSecondMoving.EndLinePoint.X - secondLineE.X);
                        //sets y2 coordinate to first location
                        LineSecondMoving.Line.SetY2(LineSecondMoving.StartMoveMousePoint.Y + 
                            LineSecondMoving.EndLinePoint.Y - secondLineE.Y);
                        LineSecondMoving = null;
                        Refresh();
                    }
                    else if (SelectedPolygon != null)
                    {
                        SelectedPolygon.Move(SecondCornerLocation, MoveX, MoveY);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedCurve != null)
                    {
                        SelectedCurve.Move(SecondCornerLocation, MoveX, MoveY);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    break;
                case "stretch":
                    if (SelectedLine != null)
                    {
                        SelectedLine.Stretch(SecondCornerLocation, cornerlocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedPolygon != null)
                    {
                        SelectedPolygon.Stretch(SecondCornerLocation, jindex);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedCurve != null)
                    {
                        SelectedCurve.Stretch(SecondCornerLocation, jindex);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedRect != null)
                    {
                        SelectedRect.Stretch(cornerlocation, SecondCornerLocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedSquare != null)
                    {
                        SelectedSquare.Stretch(cornerlocation, SecondCornerLocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                    }
                    else if (SelectedCircle != null)
                    {
                        SelectedCircle.Stretch(SecondCornerLocation);
                        Refresh();
                    }
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
                    if (LastSelectedPolygon != null)
                    {
                        LastSelectedPolygon.HorizontalSymmetry(cornerlocation);
                    }
                    if (LastSelectedCurve != null)
                    {
                        LastSelectedCurve.HorizontalSymmetry(cornerlocation);
                    }
                    Refresh();
                    break;
                case "rotateVertically":
                    if (LastSelectedLine != null)
                    {
                        LastSelectedLine.VerticalSymmetry();
                    }
                    if (LastSelectedPolygon != null)
                    {
                        LastSelectedPolygon.HorizontalSymmetry(cornerlocation);
                    }
                    if (LastSelectedCurve != null)
                    {
                        LastSelectedCurve.VerticalSymmetry(cornerlocation);
                    }
                    Refresh();
                    break;
                case "delete":
                    Rects = tempRects.ToList(); //sends all tempRects elements to Rects list
                    Squares = tempSquares.ToList(); //sends all tempSquares elements to Squares list
                    Circles = tempCircles.ToList(); //sends all tempCircles elements to Circles list
                    Lines = tempLines.ToList(); //sends all tempLines elements to Lines list
                    Polygons = tempPolygons.ToList();//sends all tempPolygons elements to Polygons list
                    Curves = tempCurves.ToList();//sends all tempCurves elements to Curves list
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
            rulerLabel.Text = "Help";
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
            if (e.KeyValue == 54)   // 6 in keyboard
                CurveButton.PerformClick();
            if (e.KeyCode == Keys.S)
                SelectButton.PerformClick();
            if (e.KeyCode == Keys.M)
                MoveButton.PerformClick();
            if (e.KeyCode == Keys.C)
                ClearButton.PerformClick();
            if (e.KeyCode == Keys.E)
                DeleteButton.PerformClick();
            if (e.KeyCode == Keys.R)
                RulerButton.PerformClick();
            if (e.KeyCode == Keys.G)
                StretchButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.Z)
                UndoButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.S)
                SaveButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.O)
                LoadButton.PerformClick();
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
                    string polyLine1 = Polygons[i].GetParentId() + " " + Polygons[i].Id + " " + Polygons[i].CurvePoints.Length + " ";

                    string[] polyLine2 = new string[100];

                    for (int k = 0; k < Polygons[i].CurvePoints.Length; k++)
                    {
                        //writes GraphPolygon constructor's parameters
                        polyLine2[k] = Polygons[i].CurvePoints[k].X + " " + Polygons[i].CurvePoints[k].Y + " ";
                    }
                    string polyLine3 = Polygons[i].Fill + " " + Polygons[i].size + " " + Polygons[i].colour.ToArgb();
                    string allPolyLine2 = "";

                    for (int l = 0; l < Polygons[i].CurvePoints.Length; l++)
                    {
                        allPolyLine2 += polyLine2[l];
                    }
                    writer.Write(polyLine1 + allPolyLine2 + polyLine3 + "\n");
                }

                writer.Write("Curves\n");

                for (int i = 0; i < Curves.Count; i++)
                {
                    string curveLine1 = Curves[i].GetParentId() + " " + Curves[i].Id + " " + Curves[i].CurvePoints.Length + " ";

                    string[] curveLine2 = new string[100];

                    for (int k = 0; k < Curves[i].CurvePoints.Length; k++)
                    {
                        //writes GraphPolygon constructor's parameters
                        curveLine2[k] = Curves[i].CurvePoints[k].X + " " + Curves[i].CurvePoints[k].Y + " ";
                    }
                    string curveLine3 = Curves[i].Fill + " " + Curves[i].size + " " + Curves[i].colour.ToArgb();
                    string allCurveLine2 = "";

                    for (int l = 0; l < Curves[i].CurvePoints.Length; l++)
                    {
                        allCurveLine2 += curveLine2[l];
                    }
                    writer.Write(curveLine1 + allCurveLine2 + curveLine3 + "\n");
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
                if (Polygons[i].Id == p)
                    return Polygons[i];
            }
            return null;
        }

        private GraphCurve GetCurveParent(int p)
        {
            for (int i = 0; i < Curves.Count; i++)
            {
                if (Curves[i].Id == p)
                    return Curves[i];
            }
            return null;
        }

        string lastFilePath;
        int k, l, m, n, o;

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
                GraphPolygon polygon;
                GraphCurve curve;

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

                    if (lines[index].Equals("Curves"))
                    {
                        o = index;
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
                            rect = new GraphRect(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), 
                                float.Parse(a[4]), float.Parse(a[5]), int.Parse(a[6]), int.Parse(a[7]), 
                                Color.FromArgb(int.Parse(a[8])));
                        }
                        else
                        {
                            //GraphRect initializes parent with GetRectParent() method
                            rect = new GraphRect(GetRectParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), 
                                float.Parse(a[3]), float.Parse(a[4]), float.Parse(a[5]), int.Parse(a[6]), int.Parse(a[7]), 
                                Color.FromArgb(int.Parse(a[8])));
                        }
                        Rects.Add(rect);
                    }

                    if (a.Length == 8 && i > k && i < l)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            square = new GraphSquare(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), 
                                float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            square = new GraphSquare(GetSquareParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), 
                                float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), 
                                Color.FromArgb(int.Parse(a[7])));
                        }
                        Squares.Add(square);
                    }

                    if (a.Length == 8 && i > l && i < m)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            circle = new GraphCircle(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), 
                                float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            circle = new GraphCircle(GetCircleParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), 
                                float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), 
                                Color.FromArgb(int.Parse(a[7])));
                        }
                        Circles.Add(circle);
                    }

                    if (a.Length == 8 && i > m && i < n)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            line = new GraphLine(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), 
                                float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), Color.FromArgb(int.Parse(a[7])));
                        }
                        else
                        {
                            line = new GraphLine(GetLineParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), 
                                float.Parse(a[3]), float.Parse(a[4]), int.Parse(a[5]), int.Parse(a[6]), 
                                Color.FromArgb(int.Parse(a[7])));
                        }
                        Lines.Add(line);
                    }

                    if (a.Length >= 11 && i > n && i < o)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            float polyPointsLength = float.Parse(a[2]);

                            PointF[] polyPoints = new PointF[(int)polyPointsLength];
                            int b = 0;

                            for (int index = 0; index < (2 * polyPointsLength); index += 2)
                            {
                                polyPoints[b] = new PointF(float.Parse(a[index + 3]), float.Parse(a[index + 4]));
                                b++;
                            }
                            polygon = new GraphPolygon(null, int.Parse(a[1]), polyPoints, int.Parse(a[2*b+3]),
                            int.Parse(a[2*b+4]), Color.FromArgb(int.Parse(a[2*b+5])));
                            b = 0;
                        }
                        else
                        {
                            float polyPointsLength = float.Parse(a[2]);
                            PointF[] polyPoints = new PointF[(int)polyPointsLength];
                            int b = 0;

                            for (int index = 0; index < (2 * polyPointsLength); index += 2)
                            {
                                polyPoints[b] = new PointF(float.Parse(a[index + 3]), float.Parse(a[index + 4]));
                                b++;
                            }
                            polygon = new GraphPolygon(GetPolygonParent(int.Parse(a[0])), int.Parse(a[1]),
                                polyPoints, int.Parse(a[2*b+3]), int.Parse(a[2*b+4]), Color.FromArgb(int.Parse(a[2*b+5])));
                        }
                        Polygons.Add(polygon);
                    }

                    if (a.Length >= 11 && i > o)
                    {
                        if (int.Parse(a[0]) == 0)
                        {
                            float curvePointsLength = float.Parse(a[2]);

                            PointF[] curvePoints = new PointF[(int)curvePointsLength];
                            int b = 0;

                            for (int index = 0; index < (2 * curvePointsLength); index += 2)
                            {
                                curvePoints[b] = new PointF(float.Parse(a[index + 3]), float.Parse(a[index + 4]));
                                b++;
                            }
                            curve = new GraphCurve(null, int.Parse(a[1]), curvePoints, int.Parse(a[2 * b + 3]),
                            int.Parse(a[2 * b + 4]), Color.FromArgb(int.Parse(a[2 * b + 5])));
                            b = 0;
                        }
                        else
                        {
                            float curvePointsLength = float.Parse(a[2]);
                            PointF[] curvePoints = new PointF[(int)curvePointsLength];
                            int b = 0;

                            for (int index = 0; index < (2 * curvePointsLength); index += 2)
                            {
                                curvePoints[b] = new PointF(float.Parse(a[index + 3]), float.Parse(a[index + 4]));
                                b++;
                            }
                            curve = new GraphCurve(GetCurveParent(int.Parse(a[0])), int.Parse(a[1]),
                                curvePoints, int.Parse(a[2 * b + 3]), int.Parse(a[2 * b + 4]), Color.FromArgb(int.Parse(a[2 * b + 5])));
                        }
                        Curves.Add(curve);
                    }
                }
            }
        }

        private void LoadBackgroundImage()
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "jpg Files (*.jpg)|*.jpg|All Files(*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,    //opens the last selected directory
                CheckFileExists = false,
                Title = "Choose Background Image"
            };

            if (file.ShowDialog() == DialogResult.OK)
            {
                string path = file.FileName;
                Image myimage = new Bitmap(@path);
                this.BackgroundImage = myimage;
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vector_Drawing_Application vector_Drawing_Form2 = new Vector_Drawing_Application();
            vector_Drawing_Form2.Show();
            this.Hide();
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

        private void LoadBackgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            LoadBackgroundImage();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastFilePath != null)
            {
                OpenFileDialog file = new OpenFileDialog();
                string[] alines = File.ReadAllLines(lastFilePath);
                int shapeCount = alines.Count() - 6; //need to increase when new shapelists are added

                if (shapeCount != Rects.Count + Squares.Count + Circles.Count + Lines.Count + Polygons.Count + Curves.Count)
                {
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
            rulerLabel.Text = "Save";
            this.Cursor = Cursors.Arrow;
            WriteToText();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Load";
            this.Cursor = Cursors.Arrow;
            ReadFromText();
            Refresh();
        }

        private void Vector_Drawing_Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lastFilePath != null)
            {
                OpenFileDialog file = new OpenFileDialog();
                string[] alines = File.ReadAllLines(lastFilePath);
                int shapeCount = alines.Count() - 5; //need to increase when new shapelists are added

                if (shapeCount != Rects.Count + Squares.Count + Circles.Count + Lines.Count + Polygons.Count + Curves.Count)
                {
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