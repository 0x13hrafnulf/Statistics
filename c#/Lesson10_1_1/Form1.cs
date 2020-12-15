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


namespace Lesson10_1_1
{
    public partial class Form1 : Form
    {

        Graphics G;
        Bitmap bitmap;

        List<Viewport> viewports = new List<Viewport>();
        Dataset Data;
        
        int n_M;
        int n_N;
        int M_old;
        int N_old;
        double mu;
        double sigma;
        double So;
        int intervals;
        int time_index;

        Histogram histo1;
        RegressionPlot reg_plot;
        Histogram histo2;
        Histogram histo3;




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
            this.DoubleBuffered = true; 
            
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            init_graphics();
            n_M = Convert.ToInt32(numericUpDown1.Text);
            n_N = Convert.ToInt32(numericUpDown2.Text);
            sigma = Convert.ToDouble(textBox2.Text);
            mu = Convert.ToDouble(textBox1.Text);
            So = Convert.ToDouble(textBox3.Text);
            intervals = Convert.ToInt32(numericUpDown3.Text);
            time_index = Convert.ToInt32(numericUpDown4.Text);
            Data = new Dataset(n_N);
            generate_points();
            draw_scene();
        }

        private void init_graphics()
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            G = Graphics.FromImage(bitmap);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            reg_plot = new RegressionPlot(1 * pictureBox1.Width / 9, 0, 7 * pictureBox1.Width / 9, pictureBox1.Height);
            //reg_plot = new RegressionPlot(0, 0, 8 * pictureBox1.Width / 9, pictureBox1.Height);
            histo1 = new Histogram(8 * pictureBox1.Width / 9, 0, 1 * pictureBox1.Width / 9, pictureBox1.Height);
            histo2 = new Histogram(0, 0, 1 * pictureBox1.Width / 9, pictureBox1.Height);

            viewports.Add(reg_plot);
            viewports.Add(histo1);

        }
        private void draw_scene()
        {
            G.Clear(Color.FromArgb(128, 112, 128, 144));

            //Draw viewports and other objects objects
            histo2.draw(G, Data.m_intervals[1], 1, Color.BlueViolet);
            histo1.draw(G, Data.m_intervals[0], 1, Color.BlueViolet);
            reg_plot.draw(G, Data);



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
                int dx = e.X - mouse_down.X;
                int dy = e.Y - mouse_down.Y;

                if (v.m_mouse_drag)
                {
                    v.update(v.m_mouse_down_pos.X + dx, v.m_mouse_down_pos.Y + dy);
                    draw_scene();
                }
                else if (v.m_mouse_resize)
                {
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
                    int dx = e.Delta;
                    int dy = e.Delta;


                    //v.m_rectangle.Location = new Point(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    //v.m_rectangle.Size = new Size(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);

                    //v.update(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    //v.resize(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);

                    v.update(dx, dy);
                    v.resize(dx, dy);
                    draw_scene();
                }
            }
        }

        private void generate_points()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < n_M; ++i)
            {
                Variable v = new Variable("RandomBernoulli");
                v.Add(So);
                double wt = 0;
                double dt = 1.0 / n_N;
                for (int j = 1; j < n_N; ++j)
                {
                    double z = Z(rnd);
                    /*double val = wt + Math.Sqrt(dt) * z;
                    wt = val;
                    val = v.get(0) * Math.Exp((mu - sigma * sigma / 2.0) * dt + sigma * wt);
                    */
                    double val = Math.Sqrt(dt) * z;     
                    val = v.get(j-1) + mu * v.get(j - 1) * dt + sigma * v.get(j - 1) * val;

                    v.Add(val);
                }

                Data.add_variable(n_N, v);
                Data.add_GBM_datapoint(i, Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));

            }
            Data.process_GBM_intervals(0, intervals);
            Data.process_GBM_intervals(time_index, intervals);
        }
        private double Z(Random t)
        {
            double u, v, S;
            do
            {
                u = 2.0 * t.NextDouble() - 1.0;
                v = 2.0 * t.NextDouble() - 1.0;
                S = u * u + v * v;
            }
            while (S >= 1.0);

            double fac = Math.Sqrt(-2.0 * Math.Log(S) / S);
            return u * fac;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                n_M = Convert.ToInt32(numericUpDown1.Text);
                n_N = Convert.ToInt32(numericUpDown2.Text);
                sigma = Convert.ToDouble(textBox2.Text);
                mu = Convert.ToDouble(textBox1.Text);
                So = Convert.ToDouble(textBox3.Text);
                intervals = Convert.ToInt32(numericUpDown3.Text);
                time_index = Convert.ToInt32(numericUpDown4.Text);
                Data.clear();
                Data.set(n_N);
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

   

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                time_index = Convert.ToInt32(numericUpDown4.Text);
                Data.process_GBM_intervals(time_index, intervals);
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
    }
}
