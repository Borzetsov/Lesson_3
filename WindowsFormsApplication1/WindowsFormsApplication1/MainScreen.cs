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
        bool Is_Shape_start = true;
        Point Shape_start;
        string curFile;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.Location);
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
                AddShape(new Cross(e.Location));
            }
            else if (rb_line.Checked)
            {
                if (Is_Shape_start)
                {
                    Shape_start = e.Location;
                    Is_Shape_start = !Is_Shape_start;
                }
                else
                {
                    AddShape(new Line(Shape_start, e.Location));
                    Is_Shape_start = !Is_Shape_start;
                }
            }
            pictureBox1.Refresh();
        }

        private void rb_cross_CheckedChanged(object sender, EventArgs e)
        {
            Is_Shape_start = true;
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
    }
}
