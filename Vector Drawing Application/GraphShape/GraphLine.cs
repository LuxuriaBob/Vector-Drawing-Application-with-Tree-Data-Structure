using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphLine
    {
        public GraphLine()
        {

        }
        public PointF StartPoint;
        public PointF EndPoint;
        public Color colour;
        public int ParentId;
        public int Id;
        public int size;
        GraphLine Parent;

        public List<GraphLine> Childs = new List<GraphLine>();

        public GraphLine(GraphLine parent, int id, float X1, float Y1, float X2, float Y2, int Size, Color Colour)
        {
            this.StartPoint = new PointF(X1, Y1);
            this.EndPoint = new PointF(X2, Y2);
            this.colour = Colour;
            this.size = Size;
            this.Id = id;
            this.Parent = parent;
            if (this.Parent != null)
                Parent.Childs.Add(this);
        }

        public void SetX1(float x)
        {
            float temp = StartPoint.X;
            StartPoint.X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphLine child in Childs)
                {
                    child.SetX1(child.StartPoint.X - (temp - x));
                }
            }
            else
                return;
        }

        public void SetY1(float y)
        {
            float temp = StartPoint.Y;
            StartPoint.Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphLine child in Childs)
                {
                    child.SetY1(child.StartPoint.Y - (temp - y));
                }
            }
            else
                return;
        }

        public void SetX2(float x)
        {
            float temp = EndPoint.X;
            EndPoint.X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphLine child in Childs)
                {
                    child.SetX2(child.EndPoint.X - (temp - x));
                }
            }
            else
                return;
        }

        public void SetY2(float y)
        {
            float temp = EndPoint.Y;
            EndPoint.Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphLine child in Childs)
                {
                    child.SetY2(child.EndPoint.Y - (temp - y));
                }
            }
            else
                return;
        }

        public void Stretch(PointF mousePoint, PointF CornerPoint)
        {
            if(CornerPoint == EndPoint)
            {
                EndPoint = mousePoint;
            }
            else if(CornerPoint == StartPoint)
            {
                StartPoint = mousePoint;
            }
        }

        public void RotateClockWise(float rad)
        {
            if (rad == 90)
            {
                //find the center
                PointF Center = new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);

                //move the line to center on the origin
                StartPoint.X -= Center.X; StartPoint.Y -= Center.Y;
                EndPoint.X -= Center.X; EndPoint.Y -= Center.Y;

                //rotate both points
                float TempX = StartPoint.X; float TempY = StartPoint.Y;
                StartPoint.X = -TempY; StartPoint.Y = TempX;

                TempX = EndPoint.X; TempY = EndPoint.Y;
                EndPoint.X = -TempY; EndPoint.Y = TempX;

                //move the center point back to where it was
                StartPoint.X += Center.X; StartPoint.Y += Center.Y;
                EndPoint.X += Center.X; EndPoint.Y += Center.Y;
            }
        }

        public void HorizontalSymmetry()
        {
            PointF Center = new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            StartPoint.Y -= (StartPoint.Y - Center.Y) * 2;
            EndPoint.Y -= (EndPoint.Y - Center.Y) * 2;
        }

        public void VerticalSymmetry()
        {
            PointF Center = new PointF((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            StartPoint.X += (Center.X - StartPoint.X) * 2;
            EndPoint.X += (Center.X - EndPoint.X) * 2;
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

        public void DeleteLines(List<GraphLine> Line)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphLine child in Childs)
                {
                    Line.Remove(child);
                    child.DeleteLines(Line);
                }
            }
            else
                return;
        }
    }
}
