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
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace Vector_Drawing_Application
{
    public partial class Vector_Drawing_Application : Form
    {
        bool move = false;  //default values are false
        bool draw = false;
        bool select = false;

        Point startlocation;    //rectangles' top left coordinates
        Point endlocation;      //rectangles' bottom right coordinates

        public Vector_Drawing_Application()
        {
            this.DoubleBuffered = true; //reduces flicker

            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            /*  example Rect object
            this.Rects = new List<GraphRect>()
            {
            new GraphRect (100, 100, 100, 300, null)
            };
            */

            InitializeComponent();
            KeyPreview = true;  //necessary for keyboard input
        }

        public List<GraphRect> Rects = new List<GraphRect>();   // main rects list
        public List<GraphRect> tempRects = new List<GraphRect>();   //temperary rects list for undo-draw method

        public List<int> ChildCounts = new List<int>();

        GraphRect SelectedRect = null;  //nothing is selected for default
        GraphRect LastSelectedRect = null;
        MoveInfo Moving = null; //move info for selected rect
        MoveInfo secondMoving = null;   //stores moving for undo-move method
        Point secondE;  //stores coordinates for undo-move

        string lastButton = null;   //stores last pressed button

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                startlocation = e.Location; //stores mouse location for first coordinates
            }
            else if (move)
            {
                draw = false;
                RefreshRectSelection(e.Location);
                if (this.SelectedRect != null && Moving == null)
                {
                    this.Capture = true;
                    Moving = new MoveInfo   //sets Moving class properties for moving rect
                    {
                        Rect = this.SelectedRect,
                        StartRectPoint = SelectedRect.StartPoint,
                        WidthRect = SelectedRect.Width,
                        HeightRect = SelectedRect.Height,
                        StartMoveMousePoint = e.Location
                    };
                    Console.WriteLine("move tuşuna basıldı");
                    secondMoving = Moving;      //stores Moving for undo method
                    secondE = e.Location;   //stores mouse coordinates for undo method
                }
                //    RefreshRectSelection(e.Location);
            }
            else if (select)
            {
                FindRectByPoint(Rects, e.Location);
                RefreshRectSelection(e.Location);
                //   startlocation = e.Location;
                this.Cursor = Cursors.Arrow;
                Refresh();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                endlocation = e.Location;   //stores mouse location while mouse is moving
            }
            else if (move)
            {
                if (Moving != null)
                {
                    Moving.Rect.setX(Moving.StartRectPoint.X + e.X - Moving.StartMoveMousePoint.X); //sets x coordinate of a moving rect
                    Moving.Rect.setY(Moving.StartRectPoint.Y + e.Y - Moving.StartMoveMousePoint.Y); //sets y coordinate of a moving rect
                }
                RefreshRectSelection(e.Location);
            }
        }

        private void DrawShape(Point startlocation, Point endlocation)
        {
            int startPointX = endlocation.X < startlocation.X ? endlocation.X : startlocation.X;    //determines x coordinate of top left corner by determining which is smaller
            int startPointY = endlocation.Y < startlocation.Y ? endlocation.Y : startlocation.Y;    //determines y coordinate of top left corner by determining which is smaller
            GraphRect rectangle = new GraphRect(startPointX, startPointY,
    Math.Abs(startlocation.X - endlocation.X), Math.Abs(startlocation.Y - endlocation.Y), SelectedRect); //width and height is difference of coordinates

            Rects.Add(rectangle);   //adds to Rects list
        }
    
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                endlocation = e.Location;   //stores mouse location for first coordinates
                draw = false;
                DrawShape(startlocation, endlocation);
                Refresh();
            }
            else if (move)
            {
                if (Moving != null)
                {
                    this.Capture = false;
                    Moving = null;
                }
                RefreshRectSelection(e.Location);

            }
        }

        static GraphRect FindRectByPoint(List<GraphRect> rects, Point p)
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

        private void RefreshRectSelection(Point point)
        {
            if (select)
            {
                var selectedRect = FindRectByPoint(Rects, point);
                if (selectedRect != this.SelectedRect)
                {
                    this.SelectedRect = selectedRect;
                    LastSelectedRect = selectedRect;    //stores selectedRect for undo-select method
                    this.Invalidate();
                }
                if (Moving != null)
                    this.Invalidate();

                this.Cursor =
                    Moving != null ? Cursors.Hand :
                    SelectedRect != null ? Cursors.SizeAll : Cursors.Default;
            }
            else if (move)
            {
                if (Moving != null)
                    this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //determines how intermediate values between two endpoints are calculated.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;  //antialiasing

            foreach (var rect in Rects)
            {
                var color = rect == SelectedRect ? Color.Blue : Color.Black;    //SelectedRect is blue, others are black
                var pen = new Pen(color, 10);   //Black pen, size 10
                e.Graphics.DrawRectangle(pen, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //draws rectangle in Rects list
            }
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            lastButton = "draw";
            select = false;
            move = false;
            draw = true;    //makes draw true
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            draw = false;
            move = false;
            select = true;  //makes select true
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            draw = false;
            select = false;
            move = true;    //makes move true
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            lastButton = "delete";
            draw = false;
            select = false;
            move = false;
            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method

            if (Rects.Count != 0)   //Exception for empty list
            {
                if (Rects.Contains(SelectedRect))    //delete selected rect if Rects list contains
                {
                    SelectedRect.deleteRects(Rects);
                    Rects.Remove(SelectedRect);
                }
            }
            Refresh();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            draw = false;
            move = false;
            select = false;
            this.SelectedRect = null;
            Moving = null;
            this.Capture = false;

            Rects.Clear();  //Clear Rects list
            Refresh();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            draw = false;
            select = false;
            move = false;

            switch (lastButton)
            {
                case "draw":
                    Rects.RemoveAt(Rects.Count - 1);    //removes last drawn rect in Rects list
                    Refresh();
                    break;
                case "select":
                    if (LastSelectedRect != null)
                    {
                        var pen = new Pen(Color.Black, 11);
                        Graphics t = this.CreateGraphics();
                        t.DrawRectangle(pen, LastSelectedRect.StartPoint.X, LastSelectedRect.StartPoint.Y, LastSelectedRect.Width, LastSelectedRect.Height);
                    }
                    break;
                case "move":
                    if (secondMoving != null)
                    {
                        secondMoving.Rect.setX(secondMoving.StartMoveMousePoint.X + secondMoving.StartRectPoint.X - secondE.X); //sets x coordinate to first location
                        secondMoving.Rect.setY(secondMoving.StartMoveMousePoint.Y + secondMoving.StartRectPoint.Y - secondE.Y); //sets y coordinate to first location
                        Refresh();
                    }
                    break;
                case "delete":
                    Rects = tempRects.ToList(); //sends all tempRects elements to Rects list
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
                DrawButton.PerformClick();
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

        XmlSerializer serialiser = new XmlSerializer(typeof(List<GraphRect>));
        string path = @"C:\Users\yalin\Desktop\NETCAD\Vector Drawing Application\Vector Drawing Application\Vector Drawing Application\bin\Debug\output1.xml";

        private void WriteText()
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<GraphRect>));
            FileStream file = File.Create(path);
            writer.Serialize(file, Rects);
            file.Close();
        }

        private void ReadText()
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<GraphRect>));
            StreamReader file = new StreamReader(path);
            List<GraphRect> deserializedList = (List<GraphRect>)reader.Deserialize(file);
            Rects = deserializedList.ToList();
            file.Close();
            Refresh();
            Console.WriteLine(deserializedList.Capacity);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            draw = false;
            select = false;
            move = false;

            Console.WriteLine("savebutton click");
            WriteText();
            Console.WriteLine(Rects.Count);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("loadbutton click");
            Console.WriteLine(Rects.Count);
            ReadText();
            Console.WriteLine(Rects.Count);
        }
    }
}