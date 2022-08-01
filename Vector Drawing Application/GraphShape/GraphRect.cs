using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphRect
    {
        public GraphRect()
        {

        }
        public PointF StartPoint;
        public float Width;
        public float Height;
        public Color colour;
        public int Fill;
        public int ParentId;
        public int Id;
        public int size;
        GraphRect Parent;

        public List<GraphRect> Childs = new List<GraphRect>();

        public GraphRect(GraphRect parent, int id, float X1, float Y1, float Width, float Height, int Fill, int Size, Color Colour)
        {
            this.StartPoint = new PointF(X1, Y1);
            this.Width = Width;
            this.Height = Height;
            this.colour = Colour;
            this.Fill = Fill;
            this.size = Size;
            this.Id = id;
            this.Parent = parent;
            if (this.Parent != null)
                Parent.Childs.Add(this);
        }

        public void SetX(float x)
        {
            float temp = StartPoint.X;
            StartPoint.X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    child.SetX(child.StartPoint.X - (temp - x));
                }
            }
            else
                return;
        }

        public void SetY(float y)
        {
            float temp = StartPoint.Y;
            StartPoint.Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    child.SetY(child.StartPoint.Y - (temp - y));
                }
            }
            else
                return;
        }

        public void Stretch(PointF cornerlocation, PointF MouseLocation)
        {
            //top left
            if(cornerlocation == StartPoint)
            {
                if (MouseLocation.X < StartPoint.X + Width && MouseLocation.Y < StartPoint.Y + Height)
                {
                    Width += StartPoint.X - MouseLocation.X;
                    Height = (StartPoint.Y + Height - MouseLocation.Y);
                    StartPoint = MouseLocation;
                }
            }
            //bottom left
            else if(cornerlocation.X == StartPoint.X && cornerlocation.Y == (StartPoint.Y + Height))
            {
                if (MouseLocation.Y > StartPoint.Y && MouseLocation.X < StartPoint.X + Width)
                {
                    Width = StartPoint.X + Width - MouseLocation.X;
                    Height = MouseLocation.Y - StartPoint.Y;
                    StartPoint.X = MouseLocation.X;
                }
            }
            //top right
            else if (cornerlocation.X == (StartPoint.X + Width) && cornerlocation.Y == StartPoint.Y)
            {
                if(MouseLocation.X > StartPoint.X && MouseLocation.Y < StartPoint.Y + Height)
                {
                    Height = StartPoint.Y + Height - MouseLocation.Y;
                    Width = MouseLocation.X - StartPoint.X;
                    StartPoint.Y = MouseLocation.Y;
                }
            }
            //bottom right
            else if (cornerlocation.X == (StartPoint.X + Width) && cornerlocation.Y == (StartPoint.Y + Height))
            {
                if (MouseLocation.X > StartPoint.X &&  MouseLocation.Y > StartPoint.Y)
                {
                    Height = MouseLocation.Y - StartPoint.Y;
                    Width = MouseLocation.X - StartPoint.X;
                }
            }
        }

        public float GetLength()
        {
            return ((Height + Width) * 2);
        }

        public float GetArea()
        {
            return (Height*Width);
        }

        public void RotateClockWise()
        {
            float tempHeight = Height;
            float constant = (Width - Height) / 2;
            StartPoint.X += constant;
            StartPoint.Y -= constant;
            Height = Width;
            Width = tempHeight;
        }

        public int GetParentId()
        {
            if (Parent == null)
            {
                return 0;
            }
            return Parent.Id;
        }

        public Color GetColour()
        {
            return colour;
        }
        public void SetColour(Color color)
        {
            colour = color;
        }
        public void DeleteRects(List<GraphRect> Rect)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    Rect.Remove(child);
                    child.DeleteRects(Rect);
                }
            }
            else
                return;
        }
    }
}
