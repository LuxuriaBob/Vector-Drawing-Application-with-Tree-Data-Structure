using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphSquare
    {
        public GraphSquare()
        {

        }
        public PointF StartPoint;
        public float Side;
        public Color colour;
        public int Fill;
        public int ParentId;
        public int Id;
        public int size;
        GraphSquare Parent;

        public List<GraphSquare> Childs = new List<GraphSquare>();

        public GraphSquare(GraphSquare parent, int id, float X1, float Y1, float Side, int Fill, int Size, Color Colour)
        {
            this.StartPoint = new PointF(X1, Y1);
            this.Side = Side;
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
                foreach (GraphSquare child in Childs)
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
                foreach (GraphSquare child in Childs)
                {
                    child.SetY(child.StartPoint.Y - (temp - y));
                }
            }
            else
                return;
        }

        public int GetId()
        {
            return Id;
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

        public void DeleteSquares(List<GraphSquare> Square)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphSquare child in Childs)
                {
                    Square.Remove(child);
                    child.DeleteSquares(Square);
                }
            }
            else
                return;
        }
    }
}
