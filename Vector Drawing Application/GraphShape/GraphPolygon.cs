using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Vector_Drawing_Application
{
    public class GraphPolygon
    {
        public GraphPolygon()
        {

        }
        public PointF[] CurvePoints;
        public Color colour;
        public int Fill;
        public int ParentId;
        public int Id;
        public int size;
        GraphPolygon Parent;

        public List<GraphPolygon> Childs = new List<GraphPolygon>();

        public GraphPolygon(GraphPolygon parent, int id, PointF[] curvePoints, int Fill, int Size, Color Colour)
        {
            this.CurvePoints = curvePoints;
            this.colour = Colour;
            this.Fill = Fill;
            this.size = Size;
            this.Id = id;
            this.Parent = parent;
            if (this.Parent != null)
                Parent.Childs.Add(this);
        }

        public void Move(PointF MouseLocation, float MoveX, float MoveY)
        {
            for (int i = 0; i < CurvePoints.Length; i++)
            {
                CurvePoints[i].X += MoveX;
                CurvePoints[i].Y += MoveY;
            }

            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.Move(MouseLocation, MoveX, MoveY);
                }
            }
            else
                return;
        }

        public void Stretch(PointF MouseLocation, int jindex)
        {
            CurvePoints[jindex] = MouseLocation;
        }

        public float Pythagorean_Theorem(PointF point1, PointF point2)
        {
            float x = point1.X - point2.X;
            float y = point1.Y - point2.Y;
            x = x * x;
            y = y * y;
            float z = x + y;
            return (float)Math.Sqrt(z);
        }

        public float GetLength()
        {
            float length = 0;
            for (int i = 0; i < CurvePoints.Length - 1; i++)
            {
                length += Pythagorean_Theorem(CurvePoints[i], CurvePoints[i + 1]);
            }
            length += Pythagorean_Theorem(CurvePoints[CurvePoints.Length - 1], CurvePoints[0]);
            return length;
        }

        public float GetArea()
        {
            // Add the first point to the end.
            int num_points = CurvePoints.Length;
            PointF[] pts = new PointF[num_points + 1];
            CurvePoints.CopyTo(pts, 0);
            pts[num_points] = CurvePoints[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }
            // Return the result.
            return Math.Abs(area);
        }

        public void VerticalSymmetry(PointF cornerlocation)
        {
            for(int i = 0; i < CurvePoints.Length; i++)
            {
                if(CurvePoints[i].X < cornerlocation.X)
                {
                    CurvePoints[i].X += 2*(cornerlocation.X - CurvePoints[i].X);
                }
                else if(CurvePoints[i].X > cornerlocation.X)
                {
                    CurvePoints[i].X -= 2 * (CurvePoints[i].X - cornerlocation.X);
                }
            }
        }

        public void HorizontalSymmetry(PointF cornerlocation)
        {
            for (int i = 0; i < CurvePoints.Length; i++)
            {
                if (CurvePoints[i].Y < cornerlocation.Y)
                {
                    CurvePoints[i].Y += 2 * (cornerlocation.Y - CurvePoints[i].Y);
                }
                else if (CurvePoints[i].Y > cornerlocation.Y)
                {
                    CurvePoints[i].Y -= 2 * (CurvePoints[i].Y - cornerlocation.Y);
                }
            }
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
        public void DeletePolygons(List<GraphPolygon> Polygon)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    Polygon.Remove(child);
                    child.DeletePolygons(Polygon);
                }
            }
            else
                return;
        }
    }
}