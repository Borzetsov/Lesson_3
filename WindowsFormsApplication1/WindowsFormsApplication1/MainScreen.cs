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
        List<Cross> Shapes = new List<Cross>();
        public MainScreen()
        {
            InitializeComponent();
        }

        public class Cross
        {
            Pen p = new Pen(Color.Red);
            public Point c;
            public Cross(Point _c)
            {
                c = _c;
            }
            public void Draw( Graphics g)
            {
                g.DrawLine(p, c.X - 3, c.Y - 3, c.X + 3, c.Y + 3);
                g.DrawLine(p, c.X + 3, c.Y - 3, c.X - 3, c.Y + 3);
            }
        }

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
            foreach (Cross p in Shapes)
            {
                p.Draw(e.Graphics);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Shapes.Add(new Cross(e.Location));
            pictureBox1.Refresh();
        }
    }
}
