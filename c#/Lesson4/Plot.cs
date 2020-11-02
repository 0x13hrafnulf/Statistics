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
        Dataset Data;

        int x_intervals;
        int y_intervals;

        ContingencyTable table;
        Histogram histo1;
        Histogram histo2;
        ScatterPlot scatter;





        //EVENTS
        Point mouse_down;
        Size size_when_mouse_down;
        public Plot()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            init_graphics();
            x_intervals = Convert.ToInt32(textBox1.Text);
            y_intervals = Convert.ToInt32(textBox2.Text);

        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {


        }

        public void load_values(Variable first, Variable second)
        {

            Data = new Dataset(first, second);
            Data.process_data(x_intervals, y_intervals);
            draw_scene();
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

            table = new ContingencyTable(0, 0, pictureBox1.Width / 2, pictureBox1.Height / 2);
            histo1 = new Histogram(0, pictureBox1.Height / 2, pictureBox1.Width / 2, pictureBox1.Height / 4);
            histo2 = new Histogram(0, 3 * pictureBox1.Height / 4, pictureBox1.Width / 2, pictureBox1.Height / 4 - 2);
            scatter = new ScatterPlot(pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height - 3);


        }
        private void draw_scene()
        {
            G.Clear(Color.White);

            //Draw viewports and other objects objects

            table.draw(G, Data);
            histo1.draw(G, Data.m_x_intervals);
            histo2.draw(G, Data.m_y_intervals);
            scatter.draw(G, Data);

            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x_intervals = Convert.ToInt32(textBox1.Text);
            y_intervals = Convert.ToInt32(textBox2.Text);
            Data.recalculate_intervals(x_intervals, y_intervals);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    int d_x = e.X - mouse_down.X;
                    int d_y = e.Y - mouse_down.Y;
                    v.update(v.m_mouse_down_pos.X + d_x, v.m_mouse_down_pos.Y + d_y);             
                    draw_scene();
                }
                else if (v.m_mouse_resize)
                {
                    int d_x = e.X - mouse_down.X;
                    int d_y = e.Y - mouse_down.Y;
                    v.resize(size_when_mouse_down.Width + d_x, size_when_mouse_down.Height + d_y);
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

    }
}
