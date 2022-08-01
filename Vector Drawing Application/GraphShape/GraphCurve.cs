using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Vector_Drawing_Application
{
    public class GraphCurve
    {
        public GraphCurve()
        {

        }
        public PointF[] CurvePoints;
        public Color colour;
        public int Fill;
        public int ParentId;
        public int Id;
        public int size;
        GraphCurve Parent;

        public List<GraphCurve> Childs = new List<GraphCurve>();

        public GraphCurve(GraphCurve parent, int id, PointF[] curvePoints, int Fill, int Size, Color Colour)
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
                foreach (GraphCurve child in Childs)
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

        public void VerticalSymmetry(PointF cornerlocation)
        {
            for (int i = 0; i < CurvePoints.Length; i++)
            {
                if (CurvePoints[i].X < cornerlocation.X)
                {
                    CurvePoints[i].X += 2 * (cornerlocation.X - CurvePoints[i].X);
                }
                else if (CurvePoints[i].X > cornerlocation.X)
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
        public void DeleteCurves(List<GraphCurve> Curve)
        {
            if (Childs.Count() > 0)
            {
                foreach (GraphCurve child in Childs)
                {
                    Curve.Remove(child);
                    child.DeleteCurves(Curve);
                }
            }
            else
                return;
        }
    }
}
