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
        bool Draw_Temp_Line = false;
        Point Shape_start;
        string curFile;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rb_cross.Checked)
            {
                TempShape = new Cross(e.Location, GreenPen);
            }
            if (rb_line.Checked)
            {
                if (Draw_Temp_Line)
                {
                    (Shapes[Shapes.Count - 1] as Line).b = e.Location;
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape p in Shapes)
            {
                p.Draw(e.Graphics);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb_cross.Checked)
            {
                AddShape(new Cross(e.Location, RedPen));
            }
            else if (rb_line.Checked)
            {
                Shape_start = e.Location;
                AddShape(new Line(Shape_start,Shape_start, GreenPen));
                Draw_Temp_Line = true;
            }
            pictureBox1.Refresh();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shapes.Clear();
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

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Draw_Temp_Line)
            {
                    (Shapes[Shapes.Count - 1] as Line).p = RedPen;
                    Draw_Temp_Line = false;
                
                pictureBox1.Refresh();
            }
        }

        private void rb_cross_CheckedChanged(object sender, EventArgs e)
        {
            Draw_Temp_Line = false;
        }
    }
}
//to do: Выделение(подсвечивание)элемента в списке
//       "тень"
//       Окружность с нефикс. радиусом
//       Удаление выделенного элемента из списка