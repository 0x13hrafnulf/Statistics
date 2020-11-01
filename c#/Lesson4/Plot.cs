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
        Dataset data;

        int x_intervals;
        int y_intervals;


        public Plot()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            init_graphics();
            x_intervals = Convert.ToInt32(textBox1.Text);
            y_intervals = Convert.ToInt32(textBox2.Text);
        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {


        }

        public void load_values(Variable first, Variable second)
        { 
            data = new Dataset(first, second);
            data.process_data();
            data.calculate_intervals(x_intervals, y_intervals);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void init_graphics()
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            G = Graphics.FromImage(bitmap);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }
        private void draw_scene()
        {
            G.Clear(Color.White);

            //Draw viewports and other objects objects



            pictureBox1.Image = bitmap;
        }

        private void draw_table(Viewport chart)
        { 
        
        }
        private void draw_histogram(Viewport chart)
        { 
        
        }
        private void draw_scatterplot(Viewport chart)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            x_intervals = Convert.ToInt32(textBox1.Text);
            y_intervals = Convert.ToInt32(textBox2.Text);
            data.calculate_intervals(x_intervals, y_intervals);
        }
    }
}
