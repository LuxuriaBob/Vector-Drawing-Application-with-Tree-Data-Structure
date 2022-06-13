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
        bool move = false;  //default values are false
        bool draw = false;
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
        MoveInfo Moving = null; //move info for selected rect

        MoveInfo secondMoving = null;   //stores moving for undo-move method
        Point secondE;  //stores coordinates for undo-move

        Point startlocation;    //rectangles' top left coordinates
        Point endlocation;      //rectangles' bottom right coordinates

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                startlocation = e.Location; //stores mouse location for first coordinates
            }
            else if (move)
            {
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
                    secondMoving = Moving;      //stores Moving for undo method
                    secondE = e.Location;   //stores mouse coordinates for undo method
                }
            }
            else if (select)
            {
                HitTest(Rects, e.Location);
                RefreshRectSelection(e.Location);
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
                    Moving.Rect.SetX(Moving.StartRectPoint.X + e.X - Moving.StartMoveMousePoint.X); //sets x coordinate of a moving rect
                    Moving.Rect.SetY(Moving.StartRectPoint.Y + e.Y - Moving.StartMoveMousePoint.Y); //sets y coordinate of a moving rect
                }
                RefreshRectSelection(e.Location);
            }
        }
    
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                endlocation = e.Location;   //stores mouse location for first coordinates
                draw = false;
                CreateRectangle(startlocation, endlocation);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //determines how intermediate values between two endpoints are calculated.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;  //antialiasing

            foreach (var rect in Rects)
            {
                var color = rect == SelectedRect ? Color.Blue : rect.GetColour();    //rect.GetColour(rect)SelectedRect is blue, others are colors selected from trackbar
                var size = trackBar1.Value;
                var pen = new Pen(color, rect.size);   //selected colour, rectangle's size  rect.colour

                if (rect.Fill == 0)
                {
                    e.Graphics.DrawRectangle(pen, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //draws rectangle in Rects list
                    
                } else {
                    e.Graphics.FillRectangle(pen.Brush, rect.StartPoint.X, rect.StartPoint.Y, rect.Width, rect.Height);   //fills rectangle in Rects list
                }
            }
        }

        static GraphRect HitTest(List<GraphRect> rects, Point p)
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

        GraphRect LastSelectedRect = null;

        private void RefreshRectSelection(Point point)
        {
            if (select)
            {
                var selectedRect = HitTest(Rects, point);
                if (selectedRect != this.SelectedRect)
                {
                    this.SelectedRect = selectedRect;
                    LastSelectedRect = selectedRect;    //stores selectedRect for undo-select method
                    this.Invalidate();
                }
                if (Moving != null)
                    this.Invalidate();
            }
            else if (move)
            {
                this.Cursor =
                    Moving != null ? Cursors.SizeAll : Cursors.Default; //Indicator for showing a shape is moving
                this.Invalidate();      
            }
        }

        string lastButton = null;   //stores last pressed button

        private void MakeAllFalse(ref bool x, ref bool y, ref bool z)
        {
            //make every parameter false
            x = false;
            y = false;
            z = false;
        }

        private void MakeOneTrue(ref bool x, ref bool y, ref bool z)
        {
            //make last parameter true
            x = false;
            y = false;
            z = true;
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            lastButton = "draw";
            MakeOneTrue(ref select, ref move, ref draw);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            lastButton = "select";
            MakeOneTrue(ref draw, ref move, ref select);
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            lastButton = "move";
            MakeOneTrue(ref draw, ref select, ref move);
        }

        public List<GraphRect> tempRects = new List<GraphRect>();   //temperary rects list for undo-draw method

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            lastButton = "delete";
            MakeAllFalse(ref draw, ref select, ref move);

            tempRects = Rects.ToList(); //stores Rects list in tempRects for undo method

            if (Rects.Count != 0)   //Exception for empty list
            {
                if (Rects.Contains(SelectedRect))    //delete selected rect if Rects list contains
                {
                    SelectedRect.DeleteRects(Rects);
                    Rects.Remove(SelectedRect);
                }
            }
            Refresh();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            lastButton = "clear";
            MakeAllFalse(ref draw, ref select, ref move);

            this.SelectedRect = null;
            Moving = null;
            this.Capture = false;

            Rects.Clear();  //Clear Rects list
            Refresh();
        }

        Graphics K = null;

        private void UndoButton_Click(object sender, EventArgs e)
        {
            MakeAllFalse(ref draw, ref select, ref move);

            switch (lastButton)
            {
                case "draw":
                    Rects.RemoveAt(Rects.Count - 1);    //removes last drawn rect in Rects list
                    Refresh();
                    break;
                case "select":
                    if (LastSelectedRect != null)
                    {
                        var pen = new Pen(LastSelectedRect.colour, LastSelectedRect.size);
                        K = this.CreateGraphics();
                        K.DrawRectangle(pen, LastSelectedRect.StartPoint.X, LastSelectedRect.StartPoint.Y, LastSelectedRect.Width, LastSelectedRect.Height);
                    }
                    break;
                case "move":
                    if (secondMoving != null)
                    {
                        //sets x coordinate to first location
                        secondMoving.Rect.SetX(secondMoving.StartMoveMousePoint.X + secondMoving.StartRectPoint.X - secondE.X);
                        //sets y coordinate to first location
                        secondMoving.Rect.SetY(secondMoving.StartMoveMousePoint.Y + secondMoving.StartRectPoint.Y - secondE.Y); 
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
                for (int i = 0; i < Rects.Count; i++)
                {
                    //writes GraphRect constructor's parameters
                    string line = Rects[i].GetParentId() + " " + Rects[i].Id + " " + Rects[i].StartPoint.X + " "
                        + Rects[i].StartPoint.Y + " " + Rects[i].Width + " " + Rects[i].Height + " " + Rects[i].Fill + " "
                        + Rects[i].size + " " + Rects[i].colour.ToArgb();
                    writer.Write(line + "\n");
                }
                writer.Close();
            }
        }

        private GraphRect GetParent(int p)
        {
            for (int i = 0; i < Rects.Count; i++)
            {
                if (Rects[i].GetId() == p)
                    return Rects[i];
            }
            return null;
        }

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

                if (lines.Length == 0)
                {
                    return;
                }

                GraphRect rect;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] a = lines[i].Split(' ');
                    //if rectangle doesn't have a parent GraphRect constructor makes parent parameter null
                    if (int.Parse(a[0]) == 0)
                        rect = new GraphRect(null, int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]), float.Parse(a[5]),
                            int.Parse(a[6]), int.Parse(a[7]), Color.FromArgb(int.Parse(a[8])));
                    else
                    {
                        //GraphRect initializes parent with GetParen() method
                        rect = new GraphRect(GetParent(int.Parse(a[0])), int.Parse(a[1]), float.Parse(a[2]), float.Parse(a[3]), float.Parse(a[4]),
                            float.Parse(a[5]), int.Parse(a[6]), int.Parse(a[7]), Color.FromArgb(int.Parse(a[8])));
                    }
                    Rects.Add(rect);
                }
                Refresh();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MakeAllFalse(ref draw, ref select, ref move);
            WriteToText();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ReadFromText();
        }
    }
}