using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public abstract class Shape
    {
        public abstract void Draw(Graphics g);
    }
    public class Cross : Shape
    {
        Pen p = new Pen(Color.Red);
        public Point c;
        public Cross(Point _c)
        {
            c = _c;
        }
        public override void Draw(Graphics g)
        {
            g.DrawLine(p, c.X - 3, c.Y - 3, c.X + 3, c.Y + 3);
            g.DrawLine(p, c.X + 3, c.Y - 3, c.X - 3, c.Y + 3);
        }
    }
    public class Line : Shape
    {
        Pen p = new Pen(Color.Red);
        public Point a;
        public Point b;
        public Line(Point _a, Point _b)
        {
            a = _a;
            b = _b;
        }
        public override void Draw(Graphics g)
        {
            g.DrawLine(p, a, b);
        }
    }
}
