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
        
        public void SetX(float x)
        {
            float[] tempX = new float[CurvePoints.Length];
            for (int i = 0; i < CurvePoints.Count(); i++)
            {
                tempX[i] = CurvePoints[i].X;
                CurvePoints[i].X = x;
            }

            for (int i = 0; i < CurvePoints.Count(); i++)
            {
                if (Childs.Count() > 0)
                {
                    foreach (GraphPolygon child in Childs)
                    {
                        child.SetX(child.CurvePoints[i].X - (tempX[i] - x));
                    }
                }
                else
                    return;
            }
        }

        public void SetY(float y)
        {
            float[] tempY = new float[CurvePoints.Length];
            for (int i = 0; i < CurvePoints.Count(); i++)
            {
                tempY[i] = CurvePoints[i].Y;
                CurvePoints[i].Y = y;
            }

            for (int i = 0; i < CurvePoints.Count(); i++)
            {
                if (Childs.Count() > 0)
                {
                    foreach (GraphPolygon child in Childs)
                    {
                        child.SetY(child.CurvePoints[i].Y - (tempY[i] - y));
                    }
                }
                else
                    return;
            }


            /*
            for (int i = 0; i < CurvePoints.Count(); i++)
            {
                float temp = CurvePoints[i].Y;
                CurvePoints[i].Y = y;
                if (Childs.Count() > 0)
                {
                    foreach (GraphPolygon child in Childs)
                    {
                        child.SetY(child.CurvePoints[i].Y - (temp - y));
                    }
                }
                else
                    return;
            }
            */
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