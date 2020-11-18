using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson6
{
    public class Line
    {
        public Point m_A;
        public Point m_B;

        public Line(Point A, Point B)
        {
            m_A = A;
            m_B = B;
        }
    }


    public class Viewport
    {
        public int m_x;
        public int m_y;
        public int m_width;
        public int m_height;
        public Rectangle m_rectangle;

        public Viewport m_parent;
        public List<Line> m_lines;

        //For Events
        public bool m_mouse_drag;
        public bool m_mouse_resize;
        public Point m_mouse_down_pos;

        public Viewport(int x, int y, int width, int height)
        {
            m_x = x;
            m_y = y;
            m_width = width;
            m_height = height;
            m_rectangle = new Rectangle(m_x, m_y, m_width, m_height);
            m_lines = new List<Line>();
        }

        public void assign_parent(Viewport parent)
        {
            m_parent = parent;
        }

        public Viewport get_parent()
        {
            return m_parent;
        }

        public void transform()
        {


        }

        public virtual void draw(Graphics G, Dataset dt)
        {

        }
        public virtual void draw(Graphics G, IntervalList interval)
        {

        }
        public virtual void update(int dx, int dy)
        {
            m_rectangle.X = dx;
            m_rectangle.Y = dy;
            m_x = dx;
            m_y = dy;
        }
        public virtual void resize(int dx, int dy)
        {
            m_rectangle.Size = new Size(dx, dy);
            m_width = dx;
            m_height = dy;

        }


    }

    public class Histogram : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        public Histogram(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 15;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
        }
        public override void draw(Graphics G, IntervalList interval)
        {
            Pen blackPen = new Pen(Color.Black, 1);
            Brush blueBrush = new SolidBrush(Color.BlueViolet);
            Brush fontBrush = new SolidBrush(Color.Black);

            float font_size = 9;
            Font font = new Font(FontFamily.GenericMonospace, font_size, FontStyle.Bold);
            G.DrawRectangle(blackPen, m_rectangle);


            blackPen.Width = 2f;

            //AXIS
            G.DrawLine(blackPen, m_vertical_axis.m_A, m_vertical_axis.m_B);
            G.DrawLine(blackPen, m_horizontal_axis.m_A, m_horizontal_axis.m_B);
           

            //BARS

            int n_bars = interval.m_count;
            int width = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y) / n_bars; 
            int max_height = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X) ;

            for (int i = 0; i < n_bars; ++i)
            {
                float height = interval.m_intervals[i].m_density * max_height / interval.m_max_density;

                int x = m_vertical_axis.m_A.X;
                int y = m_vertical_axis.m_A.Y + width * i ;
                G.FillRectangle(blueBrush, new Rectangle(x, y, (int)height, width));

                G.DrawString(interval.m_intervals[i].ToString(), font, fontBrush, x, y + width/2);
            }



            blackPen.Dispose();
            blueBrush.Dispose();
            fontBrush.Dispose();
        }
        public override void update(int dx, int dy)
        {
            base.update(dx, dy);
            m_horizontal_axis.m_A.X = m_x + m_pad;
            m_horizontal_axis.m_A.Y = m_y + m_height - m_pad;
            m_horizontal_axis.m_B.X = m_x + m_width - m_pad;
            m_horizontal_axis.m_B.Y = m_y + m_height - m_pad;

            m_vertical_axis.m_A.X = m_x + m_pad;
            m_vertical_axis.m_A.Y = m_y + m_pad;
            m_vertical_axis.m_B.X = m_x + m_pad;
            m_vertical_axis.m_B.Y = m_y + m_height - m_pad;
        }
        public override void resize(int dx, int dy)
        {
            base.resize(dx, dy);
            m_horizontal_axis.m_A.X = m_x + m_pad;
            m_horizontal_axis.m_A.Y = m_y + m_height - m_pad;
            m_horizontal_axis.m_B.X = m_x + m_width - m_pad;
            m_horizontal_axis.m_B.Y = m_y + m_height - m_pad;

            m_vertical_axis.m_A.X = m_x + m_pad;
            m_vertical_axis.m_A.Y = m_y + m_pad;
            m_vertical_axis.m_B.X = m_x + m_pad;
            m_vertical_axis.m_B.Y = m_y + m_height - m_pad;
        }
    }
    public class ScatterPlot : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        int m_grid;
        public ScatterPlot(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 30;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_grid = 15;

        }
        public override void draw(Graphics G, Dataset dt)
        {
            /*
            Pen blackPen = new Pen(Color.Black, 1);
            Brush blueBrush = new SolidBrush(Color.Blue);
            Brush fontBrush = new SolidBrush(Color.Black);
            Pen redPen = new Pen(Color.Red, 2);

            float font_size = 9;
            Font font = new Font(FontFamily.GenericMonospace, font_size, FontStyle.Bold);
            G.DrawRectangle(blackPen, m_rectangle);

            //GRID
            int grid_w = Math.Abs((m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X)) / m_grid;
            int grid_h = Math.Abs((m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y)) / m_grid;

            for (int i = 1; i <= m_grid; ++i)
            {
                G.DrawLine(blackPen, m_vertical_axis.m_A.X + grid_w * i, m_vertical_axis.m_A.Y, m_vertical_axis.m_B.X + grid_w * i, m_vertical_axis.m_B.Y);
                G.DrawLine(blackPen, m_horizontal_axis.m_A.X, m_horizontal_axis.m_A.Y - grid_h * i, m_horizontal_axis.m_B.X, m_horizontal_axis.m_B.Y - grid_h * i);
            }

            blackPen.Width = 2f;

            //AXIS
            G.DrawLine(blackPen, m_vertical_axis.m_A, m_vertical_axis.m_B);
            G.DrawLine(blackPen, m_horizontal_axis.m_A, m_horizontal_axis.m_B);



            G.DrawString(dt.m_x_name, font, fontBrush, m_horizontal_axis.m_B.X - (m_horizontal_axis.m_B.X - m_horizontal_axis.m_A.X) / 2, m_horizontal_axis.m_A.Y + m_pad / 3);
            G.DrawString(dt.m_y_name, font, fontBrush, m_vertical_axis.m_A.X - m_pad / 2, m_vertical_axis.m_A.Y - m_pad / 2);
            //POINTS


            float point_w = 6;
            foreach (Datapoint data in dt.m_points)
            {
                double x = (m_x + m_pad) + (m_width - 2 * m_pad) * ((data.m_x - dt.m_x_min_value) / dt.m_x_range);
                double y = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * ((data.m_y - dt.m_y_min_value) / dt.m_y_range);
                G.FillEllipse(blueBrush, (float)x - point_w / 2, (float)y - point_w / 2, point_w, point_w);
            }

            //Without Range
            //foreach (Datapoint data in dt.m_points)
            //{
            //    double x = (m_x + m_pad) + (m_width - 2 * m_pad) * (data.m_x) / dt.m_x_max_value;
            //    double y = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (data.m_y) / dt.m_y_max_value;
            //    G.FillEllipse(blueBrush, (float)x - point_w / 2, (float)y - point_w / 2, point_w, point_w);
            //}

            double x_mean = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_x_mean) / dt.m_x_max_value;
            Line mean_x = new Line(new Point((int)x_mean, m_vertical_axis.m_A.Y), new Point((int)x_mean, m_vertical_axis.m_B.Y));


            double y_mean = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_y_mean) / dt.m_y_max_value;
            Line mean_y = new Line(new Point(m_horizontal_axis.m_A.X, (int)y_mean), new Point(m_horizontal_axis.m_B.X, (int)y_mean));

            G.DrawLine(redPen, mean_x.m_A, mean_x.m_B);
            G.DrawLine(redPen, mean_y.m_A, mean_y.m_B);


            blackPen.Dispose();
            blueBrush.Dispose();
            redPen.Dispose();
            fontBrush.Dispose();
            */

        }
        public override void update(int dx, int dy)
        {
            base.update(dx, dy);

            m_horizontal_axis.m_A.X = m_x + m_pad;
            m_horizontal_axis.m_A.Y = m_y + m_height - m_pad;
            m_horizontal_axis.m_B.X = m_x + m_width - m_pad;
            m_horizontal_axis.m_B.Y = m_y + m_height - m_pad;

            m_vertical_axis.m_A.X = m_x + m_pad;
            m_vertical_axis.m_A.Y = m_y + m_pad;
            m_vertical_axis.m_B.X = m_x + m_pad;
            m_vertical_axis.m_B.Y = m_y + m_height - m_pad;
        }
        public override void resize(int dx, int dy)
        {
            base.resize(dx, dy);
            m_horizontal_axis.m_A.X = m_x + m_pad;
            m_horizontal_axis.m_A.Y = m_y + m_height - m_pad;
            m_horizontal_axis.m_B.X = m_x + m_width - m_pad;
            m_horizontal_axis.m_B.Y = m_y + m_height - m_pad;

            m_vertical_axis.m_A.X = m_x + m_pad;
            m_vertical_axis.m_A.Y = m_y + m_pad;
            m_vertical_axis.m_B.X = m_x + m_pad;
            m_vertical_axis.m_B.Y = m_y + m_height - m_pad;
        }
    }
    public class ContingencyTable : Viewport
    {
        int m_pad;
        Rectangle m_table;
        public ContingencyTable(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 30;
            m_table = new Rectangle(x + m_pad, y + m_pad, width - 2 * m_pad, height - 2 * m_pad);
        }
        public override void draw(Graphics G, Dataset dt)
        {
            /*
            Pen blackPen = new Pen(Color.Black, 1);
            Brush fontBrush = new SolidBrush(Color.Black);

            float font_size = 8;
            Font font = new Font(FontFamily.GenericMonospace, font_size, FontStyle.Regular);

            G.DrawRectangle(blackPen, m_rectangle);

            G.DrawRectangle(blackPen, m_table);
            int m_grid_x = dt.m_y_intervals.m_count + 1;
            int m_grid_y = dt.m_x_intervals.m_count + 1;

            int grid_w = m_table.Width / (m_grid_x + 1);
            int grid_h = m_table.Height / (m_grid_y + 1);

            for (int i = 1; i <= m_grid_x; ++i)
            {
                G.DrawLine(blackPen, m_table.X + grid_w * i, m_table.Y, m_table.X + grid_w * i, m_table.Y + m_table.Height);

            }
            for (int i = 1; i <= m_grid_y; ++i)
            {
                G.DrawLine(blackPen, m_table.X, m_table.Y + grid_h * i, m_table.X + m_table.Width, m_table.Y + grid_h * i);
            }

            G.DrawString(dt.m_x_name, font, fontBrush, m_table.X, m_table.Y + m_table.Height + m_pad / 3);
            G.DrawString(dt.m_y_name, font, fontBrush, m_table.X + m_pad / 2 + grid_w, m_table.Y - m_pad / 2);

            for (int i = 1; i <= dt.m_x_intervals.m_count; ++i)
            {
                G.DrawString(dt.m_x_intervals.m_intervals[i - 1].ToString(), font, fontBrush, m_table.X + m_pad / 5, m_table.Y + m_pad / 2 + grid_h * i);
            }
            for (int i = 1; i <= dt.m_y_intervals.m_count; ++i)
            {
                G.DrawString(dt.m_y_intervals.m_intervals[i - 1].ToString(), font, fontBrush, m_table.X + m_pad / 5 + grid_w * i, m_table.Y + m_pad / 2);
            }
            G.DrawString("Total", font, fontBrush, m_table.X + m_pad / 2, m_table.Y + m_pad / 2 + grid_h * m_grid_y);
            G.DrawString("Total", font, fontBrush, m_table.X + m_pad / 2 + grid_w * m_grid_x, m_table.Y + m_pad / 2);


            for (int i = 0; i <= dt.m_x_intervals.m_count; ++i)
            {
                for (int j = 0; j <= dt.m_y_intervals.m_count; ++j)
                {
                    G.DrawString(dt.m_distribution.m_frequency[i, j].ToString(), font, fontBrush, m_table.X + m_pad / 2 + grid_w * (j + 1), m_table.Y + m_pad / 2 + grid_h * (i + 1));
                }
            }


            fontBrush.Dispose();
            blackPen.Dispose();
            */
        }
        public override void update(int dx, int dy)
        {
            base.update(dx, dy);
            m_table.X = dx + m_pad;
            m_table.Y = dy + m_pad;
        }
        public override void resize(int dx, int dy)
        {
            base.resize(dx, dy);
            m_table.Size = new Size(dx - 2 * m_pad, dy - 2 * m_pad);
        }
    }

    public class RegressionPlot : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        int m_grid;
        int m_dx = 0;
        int m_dy = 0;
        int m_dx_resize = 0;
        int m_dy_resize = 0;
        public RegressionPlot(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 20;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_grid = 33;

        }
        public void draw(Graphics G, Dataset dt, double p, double e)
        {
            //PENS
            Pen gridpen = new Pen(Color.White, 1);
            Pen axisPen = new Pen(Color.Black, 2);

            Pen P = new Pen(Color.Blue, 3);
            Pen E = new Pen(Color.Blue, 2);

            Random rColor = new Random();


            //GRID
            int grid_w = Math.Abs((m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X)) / m_grid;
            int grid_h = Math.Abs((m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y)) / m_grid;

            for (int i = 1; i <= m_grid; ++i)
            {
                G.DrawLine(gridpen, m_vertical_axis.m_A.X + grid_w * i, m_vertical_axis.m_A.Y, m_vertical_axis.m_B.X + grid_w * i, m_vertical_axis.m_B.Y);
                G.DrawLine(gridpen, m_horizontal_axis.m_A.X, m_horizontal_axis.m_A.Y - grid_h * i, m_horizontal_axis.m_B.X, m_horizontal_axis.m_B.Y - grid_h * i);
            }

            //AXIS
            G.DrawLine(axisPen, m_vertical_axis.m_A, m_vertical_axis.m_B);
            G.DrawLine(axisPen, m_horizontal_axis.m_A, m_horizontal_axis.m_B);

            //P and Epsillon

            double A_x = (m_x + m_pad);
            double A_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * p;

            double B_x = (m_x + m_pad ) + (m_width - 2 * m_pad);
            double B_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * p;

            Point p_A = new Point((int)A_x, (int)A_y);
            Point P_B = new Point((int)B_x, (int)B_y);
            G.DrawLine(P, p_A, P_B);


            A_x = (m_x + m_pad);
            A_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (p + e);

            B_x = (m_x + m_pad) + (m_width - 2 * m_pad);
            B_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (p + e);

            Point Eps1_A = new Point((int)A_x, (int)A_y);
            Point Eps1_B = new Point((int)B_x, (int)B_y);
            G.DrawLine(E, Eps1_A, Eps1_B);


            A_x = (m_x + m_pad);
            A_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (p - e);

            B_x = (m_x + m_pad) + (m_width - 2 * m_pad);
            B_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (p - e);

            Point Eps2_A = new Point((int)A_x, (int)A_y);
            Point Eps2_B = new Point((int)B_x, (int)B_y);
            G.DrawLine(E, Eps2_A, Eps2_B);

            //LINES
            for (int i = 0; i < dt.m_number_of_variables; ++i)
            {

                Pen line_pen = new Pen(dt.m_points[i].m_color, 2f);

                for (int j = 1; j < dt.m_number_of_points; ++j)
                {
                    A_x = (m_x + m_pad + m_dx) + (m_width - 2 * m_pad) * (dt.m_points[i].m_points[j-1].m_x / dt.m_number_of_points);
                    A_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_points[i].m_points[j - 1].m_y);

                    B_x = (m_x + m_pad + m_dx) + (m_width - 2 * m_pad) * (dt.m_points[i].m_points[j].m_x / dt.m_number_of_points);
                    B_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_points[i].m_points[j].m_y);

                    Point A = new Point((int)A_x, (int)A_y);
                    Point B = new Point((int)B_x, (int)B_y);

                    G.DrawLine(line_pen, A, B);
                }

                line_pen.Dispose();
            }


            P.Dispose();
            E.Dispose();
            axisPen.Dispose();
            gridpen.Dispose();

        }
        public override void update(int dx, int dy)
        {
            m_dx = dx;
            m_dy = dy;

        }
        public override void resize(int dx, int dy)
        {
            m_dx_resize = dx;
            m_dy_resize = dy;
        }
    }
}