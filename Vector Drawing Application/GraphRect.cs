using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    [Serializable]
    public class GraphRect
    {
        public GraphRect()
        {

        }
        public PointF StartPoint;
        public float Width;
        public float Height;
        GraphRect Parent;

        public List<GraphRect> Childs = new List<GraphRect>();

        public GraphRect(float X1, float Y1, float Width, float Height, GraphRect parent)
        {
            this.StartPoint = new PointF(X1, Y1);
            this.Width = Width;
            this.Height = Height;
            this.Parent = parent;
            if (this.Parent != null)
                Parent.Childs.Add(this);
        }

        public void setX(float x)
        {
            float temp = StartPoint.X;
            StartPoint.X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    child.setX(child.StartPoint.X - (temp - x));
                }
            }
            else
                return;
        }

        public void setY(float y)
        {
            float temp = StartPoint.Y;
            StartPoint.Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    child.setY(child.StartPoint.Y - (temp - y));
                }
            }
            else
                return;
        }

        public void deleteRects(List<GraphRect> Rect)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphRect child in Childs)
                {
                    Rect.Remove(child);
                    child.deleteRects(Rect);
                }
            }
            else
                return;
        }
    }
}
