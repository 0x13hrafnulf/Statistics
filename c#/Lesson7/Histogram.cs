using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lesson7
{
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
            Brush blueBrush = new SolidBrush(Color.BlueViolet);

            int n_bars = interval.m_count;
            int max_height = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y);
            int width = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X) / n_bars;

            double range = interval.m_intervals[n_bars-1].m_ending_point - interval.m_intervals[0].m_starting_point;

            for (int i = 0; i < n_bars; ++i)
            {
                double height = (interval.m_intervals[i].m_density * max_height / interval.m_max_density); //(interval.m_intervals[i].m_relative_frequency)

                double x = m_vertical_axis.m_A.X + width * i;
                double y = m_vertical_axis.m_A.Y - m_pad/2 + (max_height - (int)height);

                G.FillRectangle(blueBrush, new Rectangle((int)x, (int)y, width, (int)(height)));
               
            }

            blueBrush.Dispose();

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
