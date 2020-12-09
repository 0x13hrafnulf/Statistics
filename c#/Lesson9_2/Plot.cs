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

namespace Lesson9_2
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
        Histogram histo3;
        Histogram histo4;
        ScatterPlot scatter;
        bool absolute;




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
            absolute = radioButton1.Checked;
        }

        private void Plot_Paint(object sender, PaintEventArgs e)
        {


        }

        public void load_values(Variable first, Variable second)
        {

            Data = new Dataset(first.m_values.Count);
            Data.add_variable(first);
            Data.add_variable(second);
            Data.add_datapoint(0, 1);
            Data.process_intervals(0, x_intervals);
            Data.process_intervals(1, y_intervals);
            Data.add_bivariate_distribution(0, 1);
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

            table = new ContingencyTable(0, 0, 4 * pictureBox1.Width / 10, 4 * pictureBox1.Height / 5);
            histo1 = new Histogram(4 * pictureBox1.Width / 10, 0, 1 * pictureBox1.Width / 10, 4 * pictureBox1.Height / 5);
            histo2 = new Histogram(0, 4 * pictureBox1.Height / 5, 4 * pictureBox1.Width / 10, 1 * pictureBox1.Height / 5);

            scatter = new ScatterPlot(pictureBox1.Width / 2, 0, 4 * pictureBox1.Width / 10, 4 * pictureBox1.Height / 5);
            histo3 = new Histogram(pictureBox1.Width / 2, 4 * pictureBox1.Height / 5, 4 * pictureBox1.Width / 10, 1 * pictureBox1.Height / 5);
            histo4 = new Histogram(9 * pictureBox1.Width / 10, 0, 1 * pictureBox1.Width / 10, 4 * pictureBox1.Height / 5);
        }
        private void draw_scene()
        {
            G.Clear(Color.Gray);

            //Draw viewports and other objects objects

            table.draw(G, Data, absolute);
            histo1.draw(G, Data.m_intervals[0], Data, 1);
            histo2.draw(G, Data.m_intervals[1], Data, 0);

            scatter.draw(G, Data);
            histo3.draw(G, Data.m_intervals[0], Data, 0);
            histo4.draw(G, Data.m_intervals[1], Data, 2);

            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                x_intervals = Convert.ToInt32(textBox1.Text);
                y_intervals = Convert.ToInt32(textBox2.Text);
                absolute = radioButton1.Checked;

                Data.recalculate_intervals(0, x_intervals);
                Data.recalculate_intervals(1, y_intervals);
                Data.recalculate_bivariate_distributions(0, 1);
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

                    v.m_rectangle.Location = new Point(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    v.m_rectangle.Size = new Size(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);

                    v.update(v.m_rectangle.X - dx, v.m_rectangle.Y - dy);
                    v.resize(v.m_rectangle.Width + 2 * dx, v.m_rectangle.Height + 2 * dy);
                    draw_scene();
                }
            }
        }
    }
}
