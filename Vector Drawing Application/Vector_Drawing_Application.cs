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
using System.Security.Cryptography;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Vector_Drawing_Application
{
    public partial class Vector_Drawing_Application : Form
    {
        public Vector_Drawing_Application()
        {
            InitializeComponent();
            this.DoubleBuffered = true; //reduces flicker
            KeyPreview = true;  //necessary for keyboard input
        }

        //default values are false
        BoolWrapper drawRect = new BoolWrapper(false);
        BoolWrapper drawSquare = new BoolWrapper(false);
        BoolWrapper drawCircle = new BoolWrapper(false);
        BoolWrapper drawLine = new BoolWrapper(false);
        BoolWrapper drawPolygon = new BoolWrapper(false);
        BoolWrapper drawCurve = new BoolWrapper(false);
        BoolWrapper select = new BoolWrapper(false);
        BoolWrapper move = new BoolWrapper(false);
        BoolWrapper stretch = new BoolWrapper(false);
        List<BoolWrapper> boolVariablesList = new List<BoolWrapper>();

        GraphRect SelectedRect = null;  //nothing is selected for default
        GraphSquare SelectedSquare = null;
        GraphCircle SelectedCircle = null;
        GraphLine SelectedLine = null;
        GraphPolygon SelectedPolygon = null;
        GraphCurve SelectedCurve = null;
        private void Vector_Drawing_Application_Load(object sender, EventArgs e)
        {
            boolVariablesList.Add(drawRect);
            boolVariablesList.Add(drawSquare);
            boolVariablesList.Add(drawCircle);
            boolVariablesList.Add(drawLine);
            boolVariablesList.Add(drawPolygon);
            boolVariablesList.Add(drawCurve);
            boolVariablesList.Add(select);
            boolVariablesList.Add(move);
            boolVariablesList.Add(stretch);
            KeyCreate();
        }

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

        PolygonMoveInfo PolygonSecondMoving = null;   //stores polygon moving for undo-move method

        CurveMoveInfo CurveSecondMoving = null;   //stores curve moving for undo-move method

        Point startlocation;    //shape's top left coordinates
        Point endlocation;      //shape's bottom right coordinates
        PointF cornerlocation;  //red corner coordinates
        Point StretchLocation;  //mouselocation for stretch method 
        Point PolyMoveLocation; //stores polygonmove location for move method
        PointF CurveMoveLocation; //stores curvemove location for move method
        PointF SecondCornerLocation;    //stores initial red corner coordinates for undo-move method

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawRect.Value == true || drawSquare.Value == true || drawCircle.Value == true || drawLine.Value == true)
            {
                startlocation = e.Location; //stores mouse location for first coordinates
            }
            else if (move.Value == true)
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
                    startlocation = e.Location;
                    PolygonMoving = new PolygonMoveInfo   //sets Moving class properties for moving Polygon
                    {
                        Polygon = this.SelectedPolygon,
                        PolygonPoints = SelectedPolygon.CurvePoints,
                        StartMoveMousePoint = e.Location
                    };
                    PolygonSecondMoving = PolygonMoving;
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
                    CurveSecondMoving = CurveMoving;
                }
            }
            else if (select.Value == true)
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
                CaptureScreen();
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLabel.Text = e.Location.ToString();
            if (drawRect.Value == true || drawSquare.Value == true || drawCircle.Value == true || drawLine.Value == true)
            {
                endlocation = e.Location;   //stores mouse location while mouse is moving
            }
            else if (move.Value == true)
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
                if (PolygonMoving != null)
                {
                    PolyMoveLocation = e.Location;

                    MoveX = PolyMoveLocation.X - PolygonMoving.PolygonPoints[jindex].X;
                    MoveY = PolyMoveLocation.Y - PolygonMoving.PolygonPoints[jindex].Y;
                    //sets coordinates of a moving polygon
                    PolygonMoving.Polygon.Move(PolyMoveLocation, MoveX, MoveY);
                    cornerlocation = PolygonMoving.PolygonPoints[jindex];
                }
                RefreshPolygonSelection(e.Location);
                if (CurveMoving != null)
                {
                    CurveMoveLocation = e.Location;

                    MoveX = CurveMoveLocation.X - CurveMoving.CurvePoints[jindex].X;
                    MoveY = CurveMoveLocation.Y - CurveMoving.CurvePoints[jindex].Y;
                    //sets coordinates of a moving Curve
                    CurveMoving.Curve.Move(CurveMoveLocation, MoveX, MoveY);
                    cornerlocation = CurveMoving.CurvePoints[jindex];
                }
                RefreshCurveSelection(e.Location);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawRect.Value == true)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawRect.Value = false;
                CreateRectangle(startlocation, endlocation);
                Refresh();
                CaptureScreen();
            }
            else if (drawSquare.Value == true)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawSquare.Value = false;
                CreateSquare(startlocation, endlocation);
                Refresh();
                CaptureScreen();
            }
            else if (drawCircle.Value == true)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawCircle.Value = false;
                CreateCircle(startlocation, endlocation);
                Refresh();
                CaptureScreen();
            }
            else if (drawLine.Value == true)
            {
                endlocation = e.Location;   //stores mouse location for last coordinates
                drawLine.Value = false;
                CreateLine(startlocation, endlocation);
                Refresh();
                CaptureScreen();
            }
            else if (move.Value == true)
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
                CaptureScreen();
            }
        }

        public List<GraphPolygon> Polygons = new List<GraphPolygon>();   // main Polygons list
        public List<PointF> polygonPoints = new List<PointF>();
        public List<GraphCurve> Curves = new List<GraphCurve>();   // main Curves list
        public List<PointF> curvePoints = new List<PointF>();
        float MoveX, MoveY;
        int jindex;
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawPolygon.Value == true)
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
                            drawPolygon.Value = false;
                            polygonPoints.Clear();
                            Refresh();
                            CaptureScreen();
                        }
                        break;
                }
            }
            else if (drawCurve.Value == true)
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
                            drawCurve.Value = false;
                            curvePoints.Clear();
                            Refresh();
                            CaptureScreen();
                        }
                        break;
                }
            }
            else if (select.Value == true)
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
            else if (stretch.Value == true)
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
                CaptureScreen();
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

            if (SelectedRect != null)
            {
                SelectedRect.SetColour(colorPicker.Color);
            }
            if (SelectedSquare != null)
            {
                SelectedSquare.SetColour(colorPicker.Color);
            }
            if (SelectedPolygon != null)
            {
                SelectedPolygon.SetColour(colorPicker.Color);
            }
            if (SelectedLine != null)
            {
                SelectedLine.SetColour(colorPicker.Color);
            }
            if (SelectedCurve != null)
            {
                SelectedCurve.SetColour(colorPicker.Color);
            }
            if (SelectedCircle != null)
            {
                SelectedCircle.SetColour(colorPicker.Color);
            }
            Refresh();
            CaptureScreen();
        }
        public List<GraphRect> Rects = new List<GraphRect>();   // main Rects list
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
        public List<GraphSquare> Squares = new List<GraphSquare>();   // main Squares list
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
                if (!move.Value)
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
                cornerlocation = PointF.Empty;
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
                if (!move.Value)
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
                //circle.GetColour(circle)SelectedCircle is blue, others are colors selected from colorpicker
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
                if (!move.Value)
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
                if (!move.Value)
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
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (myimage != null)
            {
                var rc = new Rectangle(0, 73, myimage.Width, myimage.Height);
                e.Graphics.DrawImage(myimage, rc);
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
            if (select.Value == true)
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
            if (select.Value == true)
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
            if (select.Value == true)
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
            if (select.Value == true)
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
            if (select.Value == true)
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
            if (select.Value == true)
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
        private void MakeAllFalse(params BoolWrapper[] listBools)
        {
            for (int i = 0; i < listBools.Length; i++)
            {
                listBools[i].Value = false;
            }
        }
        private void MakeTrue(params BoolWrapper[] listBools)
        {
            listBools[0].Value = true;
        }
        private void MakeSelectionNull()
        {
            this.SelectedRect = null;
            this.SelectedSquare = null;
            this.SelectedCircle = null;
            this.SelectedLine = null;
            this.SelectedPolygon = null;
            this.SelectedCurve = null;
        }
        private void MakeMovingNull()
        {
            this.RectMoving = null;
            this.SquareMoving = null;
            this.CircleMoving = null;
            this.LineMoving = null;
            this.PolygonMoving = null;
            this.CurveMoving = null;
        }
        private void MakeListsEmpty()
        {
            this.Rects.Clear();
            this.Squares.Clear();
            this.Circles.Clear();
            this.Lines.Clear();
            this.Polygons.Clear();
            this.Curves.Clear();
        }
        private void RectButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawRect";
            rulerLabel.Text = "Rectangle";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawRect);
        }
        private void SquareButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawSquare";
            rulerLabel.Text = "Square";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawSquare);
        }
        private void CircleButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawCircle";
            rulerLabel.Text = "Circle";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawCircle);
        }
        private void LineButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawLine";
            rulerLabel.Text = "Line";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawLine);
        }
        private void PolygonButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawPolygon";
            rulerLabel.Text = "Polygon";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawPolygon);
        }
        private void CurveButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawCurve";
            rulerLabel.Text = "Curve";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(drawCurve);
        }
        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            rulerLabel.Text = "Select";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(select);
        }
        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            rulerLabel.Text = "Move";
            cornerlocation = PointF.Empty;
            this.Cursor = Cursors.SizeAll;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(move);
        }
        private void StretchButton_Click(object sender, EventArgs e)
        {
            lastButton = "stretch";
            rulerLabel.Text = "Stretch";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeTrue(stretch);
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
            CaptureScreen();
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
            CaptureScreen();
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
            CaptureScreen();
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
                if (cornerlocation != null)
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
            CaptureScreen();
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
            MakeAllFalse(boolVariablesList.ToArray());
            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method
            tempSquares = Squares.ToList(); //stores Squares list in tempSquares for undo method
            tempCircles = Circles.ToList(); //stores Circles list in tempCircles for undo method
            tempLines = Lines.ToList(); //stores Lines list in tempLines for undo method
            tempPolygons = Polygons.ToList(); //stores Polygons list in tempPolygons for undo method
            tempCurves = Curves.ToList();//stores Curves list in tempCurves for undo method
            cornerlocation = PointF.Empty;

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
            CaptureScreen();
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            rulerLabel.Text = "Clear";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());
            MakeSelectionNull();
            MakeMovingNull();
            MakeListsEmpty();
            this.Capture = false;
            Refresh();
            CaptureScreen();
        }

        Graphics K = null;
        private void UndoButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Undo";
            this.Cursor = Cursors.Arrow;
            MakeAllFalse(boolVariablesList.ToArray());

            switch (lastButton)
            {
                case "drawRect":
                    if (Rects.Count > 0)
                    {
                        Rects.RemoveAt(Rects.Count - 1);    //removes last drawn rect in Rects list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "drawSquare":
                    if (Squares.Count > 0)
                    {
                        Squares.RemoveAt(Squares.Count - 1);    //removes last drawn square in Squares list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "drawCircle":
                    if (Circles.Count > 0)
                    {
                        Circles.RemoveAt(Circles.Count - 1);    //removes last drawn circle in Circles list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "drawLine":
                    if (Lines.Count > 0)
                    {
                        Lines.RemoveAt(Lines.Count - 1);    //removes last drawn line in Lines list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "drawPolygon":
                    if (Polygons.Count > 0)
                    {
                        Polygons.RemoveAt(Polygons.Count - 1);    //removes last drawn polygon in Polygons list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "drawCurve":
                    if (Curves.Count > 0)
                    {
                        Curves.RemoveAt(Curves.Count - 1);    //removes last drawn Curve in Curves list
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "select":
                    using (K = this.CreateGraphics())
                    {
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
                        CaptureScreen();
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
                        CaptureScreen();
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
                        CaptureScreen();
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
                        CaptureScreen();
                    }
                    else if (PolygonSecondMoving != null)
                    {
                        MoveX = PolygonSecondMoving.PolygonPoints[jindex].X - SecondCornerLocation.X;
                        MoveY = PolygonSecondMoving.PolygonPoints[jindex].Y - SecondCornerLocation.Y;
                        //sets coordinates of a moving polygon
                        PolygonSecondMoving.Polygon.Move(SecondCornerLocation, -MoveX, -MoveY);
                        cornerlocation = SecondCornerLocation;
                        PolygonSecondMoving = null;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (CurveSecondMoving != null)
                    {
                        MoveX = CurveSecondMoving.CurvePoints[jindex].X - SecondCornerLocation.X;
                        MoveY = CurveSecondMoving.CurvePoints[jindex].Y - SecondCornerLocation.Y;
                        //sets coordinates of a moving polygon
                        CurveSecondMoving.Curve.Move(SecondCornerLocation, -MoveX, -MoveY);
                        cornerlocation = SecondCornerLocation;
                        CurveSecondMoving = null;
                        Refresh();
                        CaptureScreen();
                    }
                    break;
                case "stretch":
                    if (SelectedLine != null)
                    {
                        SelectedLine.Stretch(SecondCornerLocation, cornerlocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (SelectedPolygon != null)
                    {
                        SelectedPolygon.Stretch(SecondCornerLocation, jindex);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (SelectedCurve != null)
                    {
                        SelectedCurve.Stretch(SecondCornerLocation, jindex);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (SelectedRect != null)
                    {
                        SelectedRect.Stretch(cornerlocation, SecondCornerLocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (SelectedSquare != null)
                    {
                        SelectedSquare.Stretch(cornerlocation, SecondCornerLocation);
                        cornerlocation = SecondCornerLocation;
                        Refresh();
                        CaptureScreen();
                    }
                    else if (SelectedCircle != null)
                    {
                        SelectedCircle.Stretch(SecondCornerLocation);
                        Refresh();
                        CaptureScreen();
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
                    CaptureScreen();
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
                    CaptureScreen();
                    break;
                case "rotateVertically":
                    if (LastSelectedLine != null)
                    {
                        LastSelectedLine.VerticalSymmetry();
                    }
                    if (LastSelectedPolygon != null)
                    {
                        LastSelectedPolygon.VerticalSymmetry(cornerlocation);
                    }
                    if (LastSelectedCurve != null)
                    {
                        LastSelectedCurve.VerticalSymmetry(cornerlocation);
                    }
                    Refresh();
                    CaptureScreen();
                    break;
                case "delete":
                    Rects = tempRects.ToList(); //sends all tempRects elements to Rects list
                    Squares = tempSquares.ToList(); //sends all tempSquares elements to Squares list
                    Circles = tempCircles.ToList(); //sends all tempCircles elements to Circles list
                    Lines = tempLines.ToList(); //sends all tempLines elements to Lines list
                    Polygons = tempPolygons.ToList();//sends all tempPolygons elements to Polygons list
                    Curves = tempCurves.ToList();//sends all tempCurves elements to Curves list
                    Refresh();
                    CaptureScreen();
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
            if (e.KeyCode == Keys.F)
                ColorPickerButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.Z)
                UndoButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.S)
                SaveButton.PerformClick();
            if (e.Control && e.KeyCode == Keys.O)
                LoadButton.PerformClick();
            if (e.KeyCode == Keys.F5)
                CaptureButton.PerformClick();
            if (e.KeyCode == Keys.F6)
                EncryptionButton.PerformClick();
            if (e.KeyCode == Keys.F7)
                DecryptionButton.PerformClick();
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
        private void SaveButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Save";
            this.Cursor = Cursors.Arrow;
            WriteToText();
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
                            polygon = new GraphPolygon(null, int.Parse(a[1]), polyPoints, int.Parse(a[2 * b + 3]),
                            int.Parse(a[2 * b + 4]), Color.FromArgb(int.Parse(a[2 * b + 5])));
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
                                polyPoints, int.Parse(a[2 * b + 3]), int.Parse(a[2 * b + 4]), Color.FromArgb(int.Parse(a[2 * b + 5])));
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
        private void LoadButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Load";
            this.Cursor = Cursors.Arrow;
            ReadFromText();
            Refresh();
            CaptureScreen();
        }

        private string key;
        private string key1 = "b14c";
        private string key2 = "a589";
        private string key3 = "8a4e";
        private string key4 = "4133";
        private string key5 = "bbce";
        private string key6 = "2ea2";
        private string key7 = "315a";
        private string key8 = "1917";
        public void KeyCreate()
        {
            char[] charArray2 = key2.ToCharArray();
            Array.Reverse(charArray2);
            key2 = new string(charArray2);
            char[] charArray4 = key4.ToCharArray();
            Array.Reverse(charArray4);
            key4 = new string(charArray4);
            char[] charArray6 = key6.ToCharArray();
            Array.Reverse(charArray6);
            key6 = new string(charArray6);
            char[] charArray8 = key8.ToCharArray();
            Array.Reverse(charArray8);
            key8 = new string(charArray8);
            key = key4 + key3 + key2 + key1 + key8 + key7 + key6 + key5;
        }
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        private void EncryptionButton_Click(object sender, EventArgs e)
        {
            var save = new SaveFileDialog
            {
                OverwritePrompt = true, //shows a warning when there is a file with the same name
                CreatePrompt = false,
                Title = "Vector Drawing Application Files",
                DefaultExt = "txt",
                Filter = "txt Files (*.txt)|*.txt|All Files(*.*)|*.*"   //Filters txt files
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                string[] lines = File.ReadAllLines(path);
                var oldText = "";

                using (StreamReader streamReader = File.OpenText(save.FileName))
                {
                    for (int index = 0; index < lines.Length; index++)
                    {
                        string line = lines[index];
                        string encryptedString = EncryptString(key, line);
                        if (line.Contains(oldText))
                        {
                            lines[index] = encryptedString;
                        }
                    }
                }
                File.WriteAllLines(save.FileName, lines);
            }
        }
        private void DecryptionButton_Click(object sender, EventArgs e)
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
                string path = save.FileName;
                string[] lines = File.ReadAllLines(path);
                var oldText = "";

                using (StreamReader streamReader = File.OpenText(save.FileName))
                {
                    for (int index = 0; index < lines.Length; index++)
                    {
                        string line = lines[index];
                        string decryptedString = DecryptString(key, line);
                        if (line.Contains(oldText))
                        {
                            lines[index] = decryptedString;
                        }
                    }
                }
                File.WriteAllLines(save.FileName, lines);
            }
        }
        Image myimage;
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
                myimage = new Bitmap(@path);
            }
        }
        private void CaptureScreen()
        {
            int cropFromY = 0;
            int cropToY = 104;

            using (Bitmap bmp = new Bitmap(this.Width, this.Height))
            {
                this.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
                Bitmap bit = bmp;
                RemoveMenu(ref bit, cropToY, cropFromY);
                rulerLabel.Text = "Screen Captured";
            }
        }
        private static void RemoveMenu(ref Bitmap bp, int cropToY, int cropFromY)
        {
            var destination = new Bitmap(bp.Width, bp.Height - (cropToY - cropFromY), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int i = 8; i < bp.Width - 8; i++)
            {
                for (int j = 1; j < 104; j++)
                {
                    Color c = bp.GetPixel(i, j);
                    bp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
            }
            using (Graphics gSource = Graphics.FromImage(bp))
            {
                using (Graphics gDestination = Graphics.FromImage(destination))
                {
                    gDestination.CompositingMode = CompositingMode.SourceCopy;
                    gDestination.DrawImageUnscaled(bp, 0, 0);

                    gSource.CompositingMode = CompositingMode.SourceCopy;
                    gSource.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, bp.Width, cropToY));

                    gDestination.CompositingMode = CompositingMode.SourceOver;
                    gDestination.DrawImageUnscaled(bp, 0, -(cropToY - cropFromY));
                    
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string path2 = "\\NETCAD\\WebViewer\\img";
                    string fullPath = path + path2;
                    destination.Save($@"{fullPath}\\myBitmap.png", ImageFormat.Png);
                }
            }
        }
        private void CaptureButton_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Capture";
            this.Cursor = Cursors.Arrow;
            CaptureScreen();
        }
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vector_Drawing_Application Vector_Drawing_Form2 = new Vector_Drawing_Application();
            Vector_Drawing_Form2.Show();
            this.Hide();
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Save";
            this.Cursor = Cursors.Arrow;
            WriteToText();
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            ReadFromText();
            Refresh();
            CaptureScreen();
        }
        private void LoadBackgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rulerLabel.Text = "Load Background";
            this.Cursor = Cursors.Arrow;
            LoadBackgroundImage();
            Refresh();
            CaptureScreen();
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
        private void Vector_Drawing_Application_FormClosing(object sender, FormClosingEventArgs e)
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
                        e.Cancel = true;
                        SaveButton.PerformClick();
                    }
                }
            }
        }
    }
}