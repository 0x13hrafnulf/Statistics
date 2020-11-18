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


namespace Lesson6
{
    public partial class Form1 : Form
    {

        Graphics G;
        Bitmap bitmap;

        List<Viewport> viewports = new List<Viewport>();
        Dataset Data;
        
        int n_paths;
        int n_points;
        double epsilon;
        double p;
        int time_interval;

        Histogram histo;
        RegressionPlot reg_plot;




        //EVENTS
        Point mouse_down;
        Size size_when_mouse_down;


        public Form1()
        {

            InitializeComponent();

            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            init_graphics();
            n_paths = Convert.ToInt32(numericUpDown1.Text);
            n_points = Convert.ToInt32(numericUpDown2.Text);
            epsilon = Convert.ToDouble(textBox1.Text);
            p = Convert.ToDouble(textBox2.Text);
            time_interval = Convert.ToInt32(numericUpDown5.Text);

            Data = new Dataset(n_points);
            generate_points();
            draw_scene();
        }
         private void Plot_Paint(object sender, PaintEventArgs e)
        {


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

            reg_plot = new RegressionPlot(0, 0, 8 * pictureBox1.Width / 9, pictureBox1.Height);
            histo = new Histogram(8 * pictureBox1.Width / 9, 0, 1 * pictureBox1.Width / 9, pictureBox1.Height);


            viewports.Add(reg_plot);
            viewports.Add(histo);

        }
        private void draw_scene()
        {
            G.Clear(Color.Gray);

            //Draw viewports and other objects objects

            reg_plot.draw(G, Data, p, epsilon);
            histo.draw(G, Data.m_intervals[0]);

            pictureBox1.Image = bitmap;
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Viewport v in viewports)
            {
                if (v.m_rectangle.Contains(e.Location))
                {
                    mouse_down = e.Location;
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        v.m_mouse_down_pos = v.m_rectangle.Location;
                        v.m_mouse_drag = true;
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        size_when_mouse_down = v.m_rectangle.Size;
                        v.m_mouse_resize = true;
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Viewport v in viewports)
            {
                if (v.m_mouse_drag)
                {
                    int dx = e.X - mouse_down.X;
                    int dy = e.Y - mouse_down.Y;
                    v.update(v.m_mouse_down_pos.X + dx, v.m_mouse_down_pos.Y + dy);
                    draw_scene();
                }
                else if (v.m_mouse_resize)
                {
                    int dx = e.X - mouse_down.X;
                    int dy = e.Y - mouse_down.Y;
                    v.resize(size_when_mouse_down.Width + dx, size_when_mouse_down.Height + dy);
                    draw_scene();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Viewport v in viewports)
            {
                v.m_mouse_drag = false;
                v.m_mouse_resize = false;
                draw_scene();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            
   
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            foreach (Viewport v in viewports)
            {
                if (v.m_rectangle.Contains(e.Location))
                {                  
                    int dx;
                    int dy;
                    if (e.Delta > 0)
                    {
                        dx = 10;
                        dy = 10;
                    }
                    else
                    {
                        dx = -10;
                        dy = -10;
                    }

                    //v.m_rectangle.Location = new Point(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    //v.m_rectangle.Size = new Size(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);

                    //v.update(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    //v.resize(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);

                    v.update(dx, dy);
                    //v.resize(dx, dy);
                    draw_scene();
                }
            }
        }

        private void generate_points()
        {
            

            Random rnd = new Random();
            for (int i = 0; i < n_paths; ++i) 
            {
                Variable v = new Variable("RandomBernoulli");
                for (int j = 0; j < n_points; ++j)
                {
                    double value = rnd.NextDouble();
                    int val = value < p ? 1 : 0;
                    v.Add(val);
                }
               
                Data.add_variable(n_points, v);
                Data.add_Bernoulli_datapoint(i, Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            }

            Data.process_Bernoulli_intervals(time_interval, 5, p, epsilon);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                n_paths = Convert.ToInt32(numericUpDown1.Text);
                n_points = Convert.ToInt32(numericUpDown2.Text);
                epsilon = Convert.ToDouble(textBox1.Text);
                p = Convert.ToDouble(textBox2.Text);
                time_interval = Convert.ToInt32(numericUpDown5.Text);
                Data.clear();
                Data.set(n_points);
                generate_points();
                draw_scene();
            }
            catch (Exception exception)
            {
                richTextBox1.Text = "> Error!!!" + Environment.NewLine;
                richTextBox1.AppendText("> Occured during invoking redrawing! " + Environment.NewLine);
                richTextBox1.AppendText("> Cannot invoke redrawing process!" + Environment.NewLine);
                richTextBox1.AppendText("> Possible causes: " + Environment.NewLine);
                richTextBox1.AppendText(">  1) Incorrect data type of number of intervals, it should be Integer;" + Environment.NewLine);
                richTextBox1.AppendText(">  2) Number of intervals is too big(small) to compute distributions;" + Environment.NewLine);
                richTextBox1.AppendText(">  3) Unexpected behaviour " + Environment.NewLine);
                richTextBox1.AppendText("> Message: " + exception.Message + Environment.NewLine);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            time_interval = Convert.ToInt32(numericUpDown5.Text);
            Data.process_Bernoulli_intervals(time_interval, 5, p, epsilon);
            draw_scene();
        }
    }
}
