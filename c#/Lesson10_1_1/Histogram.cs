using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lesson10_1_1
{
    public class Histogram : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        Axis m_axis;
        public Histogram(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 10;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_axis = new Axis(x, y, width, height, m_pad);
        }
        public void draw(Graphics G, IntervalList interval, int mode, Color color)
        {
               
            if (mode == 0)
            {
                m_axis.draw(G);
                Brush brush = new SolidBrush(color);

                int n_bars = interval.m_count;
                int max_height = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y);
                double width = 1 + Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X) / n_bars;
                if (width == 0) width = 1;

                double range = interval.m_intervals[n_bars - 1].m_ending_point - interval.m_intervals[0].m_starting_point;
                for (int i = 0; i < n_bars; ++i)
                {
                    double height = interval.m_intervals[i].m_density * max_height / interval.m_max_density; //(interval.m_intervals[i].m_relative_frequency)

                    double x = m_pad + m_x + width * i;
                    double y = m_vertical_axis.m_A.Y + (max_height - (int)height);

                    G.FillRectangle(brush, new Rectangle((int)x, (int)y, (int)width, (int)(height)));

                }

                brush.Dispose();

            }
            else if (mode == 1)
            {
                Pen blackPen = new Pen(Color.Black, 1);
                Brush brush = new SolidBrush(color);
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
                int max_height = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X);

                for (int i = 0; i < n_bars; ++i)
                {
                    float height = interval.m_intervals[i].m_density * max_height / interval.m_max_density;

                    int x = m_vertical_axis.m_A.X;
                    int y = m_horizontal_axis.m_A.Y - width * i - width;
                    G.FillRectangle(brush, new Rectangle(x, y, (int)height, width));

                    G.DrawString(interval.m_intervals[i].ToString(), font, fontBrush, x, y + width / 4);
                }

                blackPen.Dispose();
                brush.Dispose();
                fontBrush.Dispose();
            }


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
}
