using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lesson4
{
    public partial class Plot : Form
    {
        Graphics G;
        Bitmap bitmap;
        
        List<Viewport> viewports = new List<Viewport>();
        List<Point> points = new List<Point>();
        ArrayList first_intervals = new ArrayList();
        ArrayList second_intervals = new ArrayList();

        public Plot()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            init_graphics();
        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {


        }

        public void load_values(Variable first, Variable second)
        { 
        
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Image = bitmap;
        }

        private void init_graphics()
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            G = Graphics.FromImage(bitmap);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
    }
}
