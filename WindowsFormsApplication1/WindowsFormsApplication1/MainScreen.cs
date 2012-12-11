using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                Shapes.Add(new Cross(e.Location));
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
                    Shapes.Add(new Line(Shape_start, e.Location));
                    Is_Shape_start = !Is_Shape_start;
                }
            }
            pictureBox1.Refresh();
        }

        private void rb_cross_CheckedChanged(object sender, EventArgs e)
        {
            Is_Shape_start = true;
        }
    }
}
