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

namespace Vector_Drawing_Application
{
    public partial class Vector_Drawing_Application : Form
    {
        bool drawRect = false;  //default values are false
        bool drawSquare = false;
        bool move = false;  
        bool select = false;

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

        RectMoveInfo RectMoving = null; //move info for selected rect
        SquareMoveInfo SquareMoving = null; //move info for selected square

        RectMoveInfo RectSecondMoving = null;   //stores rect-moving for undo-move method
        Point secondRectE;  //stores rect-coordinates for undo-move

        SquareMoveInfo SquareSecondMoving = null;   //storessquare moving for undo-move method
        Point secondSquareE;  //stores square-coordinates for undo-move

        Point startlocation;    //shape's top left coordinates
        Point endlocation;      //shape's bottom right coordinates

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
                    SquareMoving = new SquareMoveInfo   //sets Moving class properties for moving rect
                    {
                        Square = this.SelectedSquare,
                        StartSquarePoint = SelectedSquare.StartPoint,
                        SquareSide = SelectedSquare.Side,
                        StartMoveMousePoint = e.Location
                    };
                    SquareSecondMoving = SquareMoving;      //stores Moving for undo method
                    secondSquareE = e.Location;   //stores mouse coordinates for undo method
                }
            }
            else if (select)
            {
                RectangleHitTest(Rects, e.Location);
                RefreshRectSelection(e.Location);
                
                SquareHitTest(Squares, e.Location);
                RefreshSquareSelection(e.Location);
                
                this.Cursor = Cursors.Arrow;
                Refresh();
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
            else if (move)
            {
                if (RectMoving != null)
                {
                    RectMoving.Rect.SetX(RectMoving.StartRectPoint.X + e.X - RectMoving.StartMoveMousePoint.X); //sets x coordinate of a moving rect
                    RectMoving.Rect.SetY(RectMoving.StartRectPoint.Y + e.Y - RectMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving rect
                }
                this.Cursor = Cursors.SizeAll;
                RefreshRectSelection(e.Location);

                if (SquareMoving != null)
                {
                    SquareMoving.Square.SetX(SquareMoving.StartSquarePoint.X + e.X - SquareMoving.StartMoveMousePoint.X); //sets x coordinate of a moving rect
                    SquareMoving.Square.SetY(SquareMoving.StartSquarePoint.Y + e.Y - SquareMoving.StartMoveMousePoint.Y); //sets y coordinate of a moving rect
                }
                RefreshSquareSelection(e.Location);
            }
        }
    
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawRect)
            {
                endlocation = e.Location;   //stores mouse location for first coordinates
                drawRect = false;
                CreateRectangle(startlocation, endlocation);
                Refresh();
            }
            else if (drawSquare)
            {
                endlocation = e.Location;   //stores mouse location for first coordinates
                drawSquare = false;
                CreateSquare(startlocation, endlocation);
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
                RefreshRectSelection(e.Location);
                RefreshSquareSelection(e.Location);
            }
        }

        ColorDialog colorPicker = new ColorDialog();

        private void ColorPickerButton_Click(object sender, EventArgs e)
        {
            colorPicker.ShowDialog();
        }

        public List<GraphRect> Rects = new List<GraphRect>();   // main rects list

        private void CreateRectangle(Point startlocation, Point endlocation)
        {
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;    //determines x coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    //determines y coordinate of top left corner by determining which is smaller
            int rectWidth = Math.Abs(startlocation.X - endlocation.X);  //width and height is difference of coordinates
            int rectHeight = Math.Abs(startlocation.Y - endlocation.Y);

            if (SelectedRect == null)
            {
                if (FillColorCheckBox.Checked)
                {
                    int fill = 1;
                    GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight, 
                        fill, trackBar1.Value, colorPicker.Color);
                    Rects.Add(rectangle);   //adds to Rects list
                }
                else
                {
                    int fill = 0;
                    GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight, 
                        fill, trackBar1.Value, colorPicker.Color);
                    Rects.Add(rectangle);   //adds to Rects list
                }
            }
                
            else if (FillColorCheckBox.Checked)
            {
                int fill = 1;
                GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight, 
                    fill, trackBar1.Value, colorPicker.Color);
                Rects.Add(rectangle);   //adds to Rects list
            }
            else
            {
                int fill = 0;
                GraphRect rectangle = new GraphRect(SelectedRect, Rects.Count + 1, startPointX, startPointY, rectWidth, rectHeight, 
                    fill, trackBar1.Value, colorPicker.Color);
                Rects.Add(rectangle);   //adds to Rects list
            }
        }

        public List<GraphSquare> Squares = new List<GraphSquare>();   // main squares list

        private void CreateSquare(Point startlocation, Point endlocation)
        {
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;    //determines x coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    //determines y coordinate of top left corner by determining which is smaller
            int squareSide = Math.Abs(startlocation.Y - endlocation.Y);

            if (SelectedSquare == null)
            {
                if (FillColorCheckBox.Checked)
                {
                    int fill = 1;
                    GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, trackBar1.Value, colorPicker.Color);
                    Squares.Add(square);   //adds to Rects list
                }
                else
                {
                    int fill = 0;
                    GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, trackBar1.Value, colorPicker.Color);
                    Squares.Add(square);   //adds to Rects list
                }
            }

            else if (FillColorCheckBox.Checked)
            {
                int fill = 1;
                GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, trackBar1.Value, colorPicker.Color);
                Squares.Add(square);   //adds to Rects list
            }
            else
            {
                int fill = 0;
                GraphSquare square = new GraphSquare(SelectedSquare, Squares.Count + 1, startPointX, startPointY, squareSide,
                        fill, trackBar1.Value, colorPicker.Color);
                Squares.Add(square);   //adds to Rects list
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //determines how intermediate values between two endpoints are calculated.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;  //antialiasing

            foreach (var rect in Rects)
            {
                var color = rect == SelectedRect ? Color.Blue : rect.GetColour();    //rect.GetColour(square)SelectedSquare is blue, others are colors selected from trackbar
                var size = trackBar1.Value;
                var pen = new Pen(color, rect.size);   //selected colour, square's size  rect.colour

                if (rect.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //draws square in Rects list

                } else {
                    e.Graphics.FillRectangle(pen.Brush, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //fills square in Rects list
                }
            }

            foreach (var square in Squares)
            {
                var color = square == SelectedSquare ? Color.Blue : square.GetColour();    //square.GetColour(rect)SelectedSquare is blue, others are colors selected from trackbar
                var size = trackBar1.Value;
                var pen = new Pen(color, square.size);   //selected colour, square's size

                if (square.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   //draws square in Rects list
                }
                else
                {
                    e.Graphics.FillRectangle(pen.Brush, square.StartPoint.X, square.StartPoint.Y, square.Side, square.Side);   //fills square in Rects list
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
                //draws each rectangle on small region around current point p and check pixel in point p 
                using (var g = Graphics.FromImage(buffer))  //draws a rectangle by using Point p which is mouse location
                {
                    g.Clear(Color.Black);
                    g.DrawRectangle(new Pen(Color.Green, 10), square.StartPoint.X - p.X + size, square.StartPoint.Y - p.Y + size, square.Side, square.Side);
                }

                if (buffer.GetPixel(size, size).ToArgb() != Color.Black.ToArgb())   //checks whether mouse is on a black pixel
                    return square;
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
            else if (move)
            {
                this.Invalidate();      
            }
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
            else if (move)
            {
                this.Invalidate();
            }
        }

        string lastButton = null;   //stores last pressed button

        private void MakeAllFalse(ref bool v, ref bool x, ref bool y, ref bool z)
        {
            //make every parameter false
            v = false;
            x = false;
            y = false;
            z = false;
        }

        private void MakeOneTrue(ref bool v, ref bool x, ref bool y, ref bool z)
        {
            //make last parameter true
            v = false;
            x = false;
            y = false;
            z = true;
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawRect";
            MakeOneTrue(ref drawSquare, ref select, ref move, ref drawRect);
        }

        private void SquareButton_Click(object sender, EventArgs e)
        {
            lastButton = "drawSquare";
            MakeOneTrue(ref drawRect, ref select, ref move, ref drawSquare);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            MakeOneTrue(ref drawRect, ref drawSquare, ref move, ref select);
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            MakeOneTrue(ref drawRect, ref drawSquare, ref select, ref move);
        }

        public List<GraphRect> tempRects = new List<GraphRect>();   //temperary rects list for undo-draw method
        public List<GraphSquare> tempSquares = new List<GraphSquare>();   //temperary rects list for undo-draw method

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            lastButton = "delete";
            MakeAllFalse(ref drawRect, ref drawSquare, ref select, ref move);

            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method
            tempSquares = Squares.ToList(); //stores Squares list in tempSquares for undo method

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
                if (Squares.Contains(SelectedSquare))    //delete selected rect if Rects list contains
                {
                    SelectedSquare.DeleteSquares(Squares);
                    Squares.Remove(SelectedSquare);
                }
            }

            Refresh();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            MakeAllFalse(ref drawRect, ref drawSquare, ref select, ref move);

            this.SelectedRect = null;
            RectMoving = null;

            this.SelectedSquare = null;
            SquareMoving = null;
            this.Capture = false;

            Rects.Clear();  //Clear Rects list
            Squares.Clear(); //Clear Squares list
            Refresh();
        }

        Graphics K = null;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            MakeAllFalse(ref drawRect, ref drawSquare, ref select, ref move);

            switch (lastButton)
            {
                case "drawRect":
                    if(Rects.Count > 0)
                    {
                        Rects.RemoveAt(Rects.Count - 1);    //removes last drawn rect in Rects list
                    }
                    Refresh();
                    break;
                case "drawSquare":
                    if(Squares.Count > 0)
                    {
                        Squares.RemoveAt(Squares.Count - 1);    //removes last drawn square in Squares list
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
                    break;
                case "delete":
                    Rects = tempRects.ToList(); //sends all tempRects elements to Rects list
                    Squares = tempSquares.ToList(); //sends all tempSquares elements to Squares list
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

        private void Form1_KeyUp(object sender, KeyEventArgs e) //instead of pressing buttons, using keys are easier
        {
            if (e.KeyCode == Keys.D)
                RectangleButton.PerformClick();
            if (e.KeyCode == Keys.R)
                SquareButton.PerformClick();
            if (e.KeyCode == Keys.S)
                SelectButton.PerformClick();
            if (e.KeyCode == Keys.M)
                MoveButton.PerformClick();
            if (e.KeyCode == Keys.C)
                ClearButton.PerformClick();
            if (e.KeyCode == Keys.E)
                DeleteButton.PerformClick();
            if (e.KeyCode == Keys.Z)
                UndoButton.PerformClick();
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
                writer.Close();
            }
        }

        private GraphRect GetRectParent(int p)
        {
            for (int i = 0; i < Rects.Count; i++)
            {
                if (Rects[i].GetId() == p)
                    return Rects[i];
            }
            return null;
        }

        private GraphSquare GetSquareParent(int p)
        {
            for (int i = 0; i < Squares.Count; i++)
            {
                if (Squares[i].GetId() == p)
                    return Squares[i];
            }
            return null;
        }

        string lastFilePath;

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

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] a = lines[i].Split(' ');

                    if(a.Length == 9)
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

                    if (a.Length == 8)
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
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            WriteToText();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ReadFromText();
            Refresh();
            Console.WriteLine("rects + square = "+ Rects.Count + Squares.Count);
        }

        private void Vector_Drawing_Application_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(lastFilePath != null)
            {
                OpenFileDialog file = new OpenFileDialog();
                string[] alines = File.ReadAllLines(lastFilePath);
                int shapeCount = alines.Count() - 2; //need to increase when new shapelists are added

                if (shapeCount != Rects.Count + Squares.Count)
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