﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace WindowsFormsApplication1
{
    public abstract class Shape
    {
        public abstract void Draw(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract string ConfString { get; }
    }
    public class Cross : Shape
    {
        private Point c;
        public Cross(Point _c)
        {
            c = _c;
        }
        public override void Draw(Graphics g, Pen p)
        {
            g.DrawLine(p, c.X - 3, c.Y - 3, c.X + 3, c.Y + 3);
            g.DrawLine(p, c.X + 3, c.Y - 3, c.X - 3, c.Y + 3);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Cross");
            sw.Write(Convert.ToString(c.X));
            sw.Write(' ');
            sw.WriteLine(Convert.ToString(c.Y));
        }
        public Cross(StreamReader _sr)
        {
            string line = _sr.ReadLine();
            string[] str = line.Split(' ');
            c.X = Convert.ToInt32(str[0]);
            c.Y = Convert.ToInt32(str[1]);
        }
        public override string ConfString
        {
            get
            {
                return "Cross " + Convert.ToString(c);
            }
        }
    }
    public class Line : Shape
    {
        private Point a;
        public Point b;
        public Line(Point _a, Point _b)
        {
            a = _a;
            b = _b;
        }
        public override void Draw(Graphics g, Pen p)
        {
            g.DrawLine(p, a, b);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.Write(Convert.ToString(a.X));
            sw.Write(' ');
            sw.Write(Convert.ToString(a.Y));
            sw.Write(' ');
            sw.Write(Convert.ToString(b.X));
            sw.Write(' ');
            sw.WriteLine(Convert.ToString(b.Y));
        }
        public Line(StreamReader _sr)
        {
            string line = _sr.ReadLine();
            string[] str = line.Split(' ');
            a.X = Convert.ToInt32(str[0]);
            a.Y = Convert.ToInt32(str[1]);
            b.X = Convert.ToInt32(str[2]);
            b.Y = Convert.ToInt32(str[3]);
        }
        public override string ConfString
        {
            get
            {
                return "Line " + Convert.ToString(a) + " : " + Convert.ToString(b);
            }
        }
    }
}
