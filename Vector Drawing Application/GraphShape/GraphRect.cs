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
