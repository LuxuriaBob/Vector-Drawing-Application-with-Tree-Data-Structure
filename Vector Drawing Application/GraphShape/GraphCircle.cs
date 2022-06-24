using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphCircle
    {
        public GraphCircle()
        {

        }
        public PointF Center;
        public float Radius;
        public Color colour;
        public int Fill;
        public int ParentId;
        public int Id;
        public int size;
        GraphCircle Parent;

        public List<GraphCircle> Childs = new List<GraphCircle>();

        public GraphCircle(GraphCircle parent, int id, float X1, float Y1, float Radius, int Fill, int Size, Color Colour)
        {
            this.Center = new PointF(X1, Y1);
            this.Radius = Radius;
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
            float temp = Center.X;
            Center.X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphCircle child in Childs)
                {
                    child.SetX(child.Center.X - (temp - x));
                }
            }
            else
                return;
        }

        public void SetY(float y)
        {
            float temp = Center.Y;
            Center.Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphCircle child in Childs)
                {
                    child.SetY(child.Center.Y - (temp - y));
                }
            }
            else
                return;
        }

        public void Stretch(PointF MouseLocation)
        {
            float radius = ((MouseLocation.X - Center.X) * (MouseLocation.X - Center.X)) 
                + ((MouseLocation.Y - Center.Y) * (MouseLocation.Y - Center.Y));
            Radius = (float)Math.Sqrt(radius);
        }

        public float GetLength()
        {
            return (float)(2 * Math.PI * Radius);
        }

        public float GetArea()
        {
            return (float)(Math.PI * Radius * Radius);
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

        public void DeleteCircles(List<GraphCircle> Circle)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphCircle child in Childs)
                {
                    Circle.Remove(child);
                    child.DeleteCircles(Circle);
                }
            }
            else
                return;
        }
    }
}
