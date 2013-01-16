using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainScreen : Form
    {
        List<Shape> Shapes = new List<Shape>();
        public MainScreen()
        {
            InitializeComponent();
        }
        Pen RedPen = new Pen(Color.Red);
        Pen GreenPen = new Pen(Color.Green);
        Shape TempShape;
        Pen pSelect = new Pen(Color.Red,2);
        Point Shape_start;
        string curFile;
        bool IsShapeStart = true;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (TempShape != null)
            {
                TempShape.Draw(e.Graphics, GreenPen);
            }
            foreach (Shape p in Shapes)
            {
                p.Draw(e.Graphics, RedPen);
            }
            foreach (int i in Shapes_list.SelectedIndices)
            {
                Shapes[i].Draw(e.Graphics, pSelect);
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb_cross.Checked)
            {
                TempShape = new Cross(e.Location);
            pictureBox1.Refresh();
            }
            else
            {
                Shape_start = e.Location;
                IsShapeStart = false;
            }
            pictureBox1.Refresh();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (TempShape != null) 
            {
                if (TempShape.MayDraw)
                {
                    AddShape(TempShape);
                }
                TempShape = null;                
                pictureBox1.Refresh();
            }
            IsShapeStart = true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rb_line.Checked)
            {
                if (IsShapeStart == false) 
                {
                    TempShape = new Line(Shape_start, e.Location);
                }
            }
            else if (rb_circle.Checked)
            {
                if (IsShapeStart == false)
                {
                    TempShape = new Circle(Shape_start, e.Location);
                }
            }
            pictureBox1.Refresh();
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shapes.Clear();
            TempShape = null;
            Shapes_list.Items.Clear();
            pictureBox1.Refresh();
        }
        private void AddShape(Shape s)
        {
            Shapes.Add(s);
            Shapes_list.Items.Add(s.ConfString);
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                curFile = openFileDialog1.FileName;
                Shapes.Clear();
                StreamReader sr = new StreamReader(curFile);
                while (!sr.EndOfStream)
                {
                    string type = sr.ReadLine();
                    switch (type)
                    {
                        case "Cross":
                            {
                                AddShape(new Cross(sr));
                                break;
                            }
                        case "Line":
                            {
                                AddShape(new Line(sr));
                                break;
                            }
                        case "Circle":
                            {
                                AddShape(new Circle(sr));
                                break;
                            }
                    }
                }
                sr.Close();
                pictureBox1.Refresh();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                curFile = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(curFile);
                foreach (Shape p in this.Shapes)
                {
                    p.SaveTo(sw);
                }
                sw.Close();
            }
        }
        private void rb_cross_CheckedChanged(object sender, EventArgs e)
        {
            IsShapeStart = true;
        }

        private void Shapes_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            pictureBox1.Refresh();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            while (Shapes_list.SelectedIndices.Count > 0)
            {
                Shapes.RemoveAt(Shapes_list.SelectedIndices[0]);
                Shapes_list.Items.RemoveAt(Shapes_list.SelectedIndices[0]);
            }
            button1.Enabled = false;
            TempShape = null;
            this.Refresh();
        }
    }
}