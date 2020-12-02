using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lesson8_2
{
    public class Histogram : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        Axis m_axis;
        public Histogram(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 15;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_axis = new Axis(x, y, width, height, m_pad);

        }
        public void draw(Graphics G, IntervalList interval, Dataset dt)
        {
            m_axis.draw(G);

            Brush brush = new SolidBrush(Color.Orange);
            Pen redPen = new Pen(Color.Red, 4);
            Pen qPen = new Pen(Color.Blue, 2);


            int n_bars = interval.m_count;
            int max_height = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y);
            int width = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X) / n_bars;

            double range = interval.m_intervals[n_bars-1].m_ending_point - interval.m_intervals[0].m_starting_point;

            for (int i = 0; i < n_bars; ++i)
            {
                double height = (interval.m_intervals[i].m_density * max_height / interval.m_max_density); //(interval.m_intervals[i].m_relative_frequency)

                double x = m_vertical_axis.m_A.X + width * i;
                double y = m_vertical_axis.m_A.Y + (max_height - (int)height);

                G.FillRectangle(brush, new Rectangle((int)x, (int)y, width, (int)(height)));

                double x_mean = (interval.m_intervals[i].m_mean - (double)interval.m_intervals[i].m_starting_point) * width / interval.m_intervals[i].m_size;

                G.DrawLine(redPen, (float)(x + x_mean), m_vertical_axis.m_A.Y, (float)(x + x_mean), m_vertical_axis.m_B.Y);
            }

            double q_1 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile1) / dt.m_summary_data[interval.m_index].m_max_value;
            Line q1 = new Line(new Point((int)q_1, m_vertical_axis.m_A.Y), new Point((int)q_1, m_vertical_axis.m_B.Y));

            double q_2 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile2) / dt.m_summary_data[interval.m_index].m_max_value;
            Line q2 = new Line(new Point((int)q_2, m_vertical_axis.m_A.Y), new Point((int)q_2, m_vertical_axis.m_B.Y));

            double q_3 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile3) / dt.m_summary_data[interval.m_index].m_max_value;
            Line q3 = new Line(new Point((int)q_3, m_vertical_axis.m_A.Y), new Point((int)q_3, m_vertical_axis.m_B.Y));

            G.DrawLine(qPen, q1.m_A, q1.m_B);
            G.DrawLine(qPen, q2.m_A, q2.m_B);
            G.DrawLine(qPen, q3.m_A, q3.m_B);




            brush.Dispose();
            redPen.Dispose();
            qPen.Dispose();
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
