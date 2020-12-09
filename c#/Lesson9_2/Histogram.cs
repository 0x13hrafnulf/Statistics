using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lesson9_2
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
        public void draw(Graphics G, IntervalList interval, Dataset dt, int mode)
        {
            

            Brush brush = new SolidBrush(Color.Orange);
            Pen redPen = new Pen(Color.Red, 2);
            Pen qPen = new Pen(Color.Blue, 2);
            Pen mPen = new Pen(Color.DarkGreen, 2);
            Brush fontBrush = new SolidBrush(Color.Black);

            float font_size = 9;
            Font font = new Font(FontFamily.GenericMonospace, font_size, FontStyle.Bold);
            if (mode == 0)
            {
                //AXIS
                Pen blackPen = new Pen(Color.Black, 2);
                G.DrawLine(blackPen, m_horizontal_axis.m_A, m_horizontal_axis.m_B);

                int n_bars = interval.m_count;
                int max_height = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y);
                int width = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X) / n_bars;

                double range = interval.m_intervals[n_bars - 1].m_ending_point - interval.m_intervals[0].m_starting_point;

                for (int i = 0; i < n_bars; ++i)
                {
                    double height = (interval.m_intervals[i].m_density * max_height / interval.m_max_density); //(interval.m_intervals[i].m_relative_frequency)

                    double x = m_vertical_axis.m_A.X + width * i;
                    double y = m_horizontal_axis.m_A.Y - (int)height;

                    G.FillRectangle(brush, new Rectangle((int)x, (int)y, width, (int)(height)));

                    double x_mean = (interval.m_intervals[i].m_mean - (double)interval.m_intervals[i].m_starting_point) * width / interval.m_intervals[i].m_size;

                    G.DrawLine(mPen, (float)(x + x_mean), m_horizontal_axis.m_A.Y - (int)height, (float)(x + x_mean), m_horizontal_axis.m_A.Y);

                    G.DrawString(interval.m_intervals[i].ToString(), font, fontBrush, (int)x, (int)y);
                }

                double q_1 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile1 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q1 = new Line(new Point((int)q_1, m_vertical_axis.m_A.Y), new Point((int)q_1, m_vertical_axis.m_B.Y));

                double q_2 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile2 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q2 = new Line(new Point((int)q_2, m_vertical_axis.m_A.Y), new Point((int)q_2, m_vertical_axis.m_B.Y));

                double q_3 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile3 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q3 = new Line(new Point((int)q_3, m_vertical_axis.m_A.Y), new Point((int)q_3, m_vertical_axis.m_B.Y));

                G.DrawLine(qPen, q1.m_A, q1.m_B);
                G.DrawLine(qPen, q2.m_A, q2.m_B);
                G.DrawLine(qPen, q3.m_A, q3.m_B);

                double mean = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_mean - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line mean_x= new Line(new Point((int)mean, m_vertical_axis.m_A.Y), new Point((int)mean, m_vertical_axis.m_B.Y));

                G.DrawLine(redPen, mean_x.m_A, mean_x.m_B);

                blackPen.Dispose();
            }
            else if (mode == 1)
            {
              
                //AXIS
                Pen blackPen = new Pen(Color.Black, 2);
                G.DrawLine(blackPen, m_vertical_axis.m_A, m_vertical_axis.m_B);

                //BARS
                int n_bars = interval.m_count;
                int width = Math.Abs(m_vertical_axis.m_A.Y - m_vertical_axis.m_B.Y) / n_bars;
                int max_height = Math.Abs(m_horizontal_axis.m_A.X - m_horizontal_axis.m_B.X);

                for (int i = 0; i < n_bars; ++i)
                {
                    float height = interval.m_intervals[i].m_density * max_height / interval.m_max_density;

                    int x = m_vertical_axis.m_A.X;
                    int y = m_vertical_axis.m_A.Y + width*i;
                    G.FillRectangle(brush, new Rectangle(x, y, (int)height, width));

                    double y_mean = (interval.m_intervals[i].m_mean - (double)interval.m_intervals[i].m_starting_point) * width / interval.m_intervals[i].m_size;

                    G.DrawLine(mPen, m_horizontal_axis.m_A.X, (float)(y + width - y_mean), m_horizontal_axis.m_A.X + (int)height, (float)(y + width - y_mean));
                    G.DrawString(interval.m_intervals[i].ToString(), font, fontBrush, (int)x, (int)y);
                }


                double q_1 = (m_y + m_pad) + (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile1 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q1 = new Line(new Point(m_horizontal_axis.m_A.X, (int)(q_1)), new Point(m_horizontal_axis.m_B.X, (int)(q_1)));

                double q_2 = (m_y + m_pad) + (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile2 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q2 = new Line(new Point(m_horizontal_axis.m_A.X, (int)(q_2)), new Point(m_horizontal_axis.m_B.X, (int)(q_2)));

                double q_3 = (m_y + m_pad) + (m_height - 2 * m_pad)  * (dt.m_summary_data[interval.m_index].m_quartile3 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q3 = new Line(new Point(m_horizontal_axis.m_A.X, (int)(q_3)), new Point(m_horizontal_axis.m_B.X, (int)(q_3)));

                G.DrawLine(qPen, q1.m_A, q1.m_B);
                G.DrawLine(qPen, q2.m_A, q2.m_B);
                G.DrawLine(qPen, q3.m_A, q3.m_B);

                double mean = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_mean - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line mean_y = new Line(new Point(m_horizontal_axis.m_A.X, (int)mean), new Point(m_horizontal_axis.m_B.X, (int)mean));

                G.DrawLine(redPen, mean_y.m_A, mean_y.m_B);
                blackPen.Dispose();
            }
            else if (mode == 2)
            {
                //AXIS
                Pen blackPen = new Pen(Color.Black, 1);
                G.DrawLine(blackPen, m_vertical_axis.m_A, m_vertical_axis.m_B);


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

                    double y_mean = (interval.m_intervals[i].m_mean - (double)interval.m_intervals[i].m_starting_point) * width / interval.m_intervals[i].m_size;

                    G.DrawLine(mPen, m_horizontal_axis.m_A.X, (float)(y + width - y_mean), m_horizontal_axis.m_A.X + (int)height, (float)(y + width - y_mean));
                    G.DrawString(interval.m_intervals[i].ToString(), font, fontBrush, (int)x, (int)y);
                }


                double q_1 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile1 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q1 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_1), new Point(m_horizontal_axis.m_B.X, (int)q_1));

                double q_2 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile2 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q2 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_2), new Point(m_horizontal_axis.m_B.X, (int)q_2));

                double q_3 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_quartile3 - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line q3 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_3), new Point(m_horizontal_axis.m_B.X, (int)q_3));

                G.DrawLine(qPen, q1.m_A, q1.m_B);
                G.DrawLine(qPen, q2.m_A, q2.m_B);
                G.DrawLine(qPen, q3.m_A, q3.m_B);

                double mean = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[interval.m_index].m_mean - dt.m_summary_data[interval.m_index].m_min_value) / (dt.m_summary_data[interval.m_index].m_range);
                Line mean_y = new Line(new Point(m_horizontal_axis.m_A.X, (int)mean), new Point(m_horizontal_axis.m_B.X, (int)mean));

                G.DrawLine(redPen, mean_y.m_A, mean_y.m_B);
                blackPen.Dispose();
            }



            fontBrush.Dispose();

            brush.Dispose();
            redPen.Dispose();
            qPen.Dispose();
            mPen.Dispose();
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
