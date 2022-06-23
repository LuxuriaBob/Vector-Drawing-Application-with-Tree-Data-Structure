using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphPolygon
    {
        public GraphPolygon()
        {

        }
        public PointF[] CurvePoints;
        public PointF[] tempPoints;
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
            this.tempPoints = new PointF[10];
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
            tempPoints[0].X = CurvePoints[0].X;

            for (int i = 1; i < CurvePoints.Count(); i++)
            {
                tempPoints[i].X = CurvePoints[i].X - CurvePoints[0].X;
            }

            CurvePoints[0].X = x;

            for (int i = 1; i < CurvePoints.Count(); i++)
            {
                CurvePoints[i].X = CurvePoints[0].X + tempPoints[i].X;
            }

            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetX(child.CurvePoints[0].X - (tempPoints[0].X - x));
                }
            }
            else
                return;
        }

        public void SetY(float y)
        {
            tempPoints[0].Y = CurvePoints[0].Y;

            for (int i = 1; i < CurvePoints.Count(); i++)
            {
                tempPoints[i].Y = CurvePoints[i].Y - CurvePoints[0].Y;
            }

            CurvePoints[0].Y = y;

            for (int i = 1; i < CurvePoints.Count(); i++)
            {
                CurvePoints[i].Y = CurvePoints[0].Y + tempPoints[i].Y;
            }

            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetY(child.CurvePoints[0].Y - (tempPoints[0].Y - y));
                }
            }
            else
                return;
        }

        public void SetXtrial(float x, int index)
        {
            CurvePoints[index].X = x;

            if (Childs.Count() > 0)
            {
                for (int i = 0; i < Childs.Count; i++)
                {
                    foreach (GraphPolygon child in Childs)
                    {
                        child.SetXtrial(child.CurvePoints[i].X - (tempPoints[i].X - x), child.CurvePoints.Length);
                    }
                }
            }
            else
                return;
        }

        public void SetYtrial(float y, int index)
        {
            CurvePoints[index].Y = y;

            if (Childs.Count() > 0)
            {
                for (int i = 0; i < Childs.Count; i++)
                {
                    foreach (GraphPolygon child in Childs)
                    {
                        child.SetYtrial(child.CurvePoints[i].Y - (tempPoints[i].Y - y), child.CurvePoints.Length);
                    }
                }
            }
            else
                return;
        }

        public void SetX1(float x)
        {
            PointF temp = CurvePoints[0];
            CurvePoints[0].X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetX1(child.CurvePoints[0].X - (temp.X - x));
                }
            }
            else
                return;
        }

        public void SetY1(float y)
        {
            PointF temp = CurvePoints[0];
            CurvePoints[0].Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetY1(child.CurvePoints[0].Y - (temp.Y - y));
                }
            }
            else
                return;
        }

        public void SetX2(float x)
        {
            PointF temp = CurvePoints[1];
            CurvePoints[1].X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetX2(child.CurvePoints[1].X - (temp.X - x));
                }
            }
            else
                return;
        }

        public void SetY2(float y)
        {
            PointF temp = CurvePoints[1];
            CurvePoints[1].Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetY2(child.CurvePoints[1].Y - (temp.Y - y));
                }
            }
            else
                return;
        }

        public void SetX3(float x)
        {
            PointF temp = CurvePoints[2];
            CurvePoints[2].X = x;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetX3(child.CurvePoints[2].X - (temp.X - x));
                }
            }
            else
                return;
        }

        public void SetY3(float y)
        {
            PointF temp = CurvePoints[2];
            CurvePoints[2].Y = y;
            if (Childs.Count() > 0)
            {
                foreach (GraphPolygon child in Childs)
                {
                    child.SetY3(child.CurvePoints[2].Y - (temp.Y - y));
                }
            }
            else
                return;
        }

        public void Stretch(PointF cornerlocation, PointF MouseLocation, int iindex, int jindex)
        {
            CurvePoints[jindex] = MouseLocation;
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