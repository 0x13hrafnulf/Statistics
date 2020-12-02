using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson8_2
{
    public class ScatterPlot : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        int m_grid;
        Axis m_axis;
        public ScatterPlot(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 30;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_grid = 15;
            m_axis = new Axis(x, y, width, height, m_pad);
        }
        public override void draw(Graphics G, Dataset dt)
        {
            m_axis.draw(G);

            Pen blackPen = new Pen(Color.Black, 1);
            Brush blackBrush = new SolidBrush(Color.Black);
            Pen redPen = new Pen(Color.Red, 4);
            Pen qPen = new Pen(Color.Blue, 2);

            float font_size = 9;
            Font font = new Font(FontFamily.GenericMonospace, font_size, FontStyle.Bold);
            G.DrawRectangle(blackPen, m_rectangle);


            G.DrawString(dt.m_summary_data[0].m_name, font, blackBrush, m_horizontal_axis.m_B.X - (m_horizontal_axis.m_B.X - m_horizontal_axis.m_A.X) / 2, m_horizontal_axis.m_A.Y + m_pad / 3);
            G.DrawString(dt.m_summary_data[1].m_name, font, blackBrush, m_vertical_axis.m_A.X - m_pad / 2, m_vertical_axis.m_A.Y - m_pad / 2);
            //POINTS


            float point_w = 6;
            foreach(Datapoint data in dt.m_points[0].m_points)
            {
                double x = (m_x + m_pad) + (m_width - 2 * m_pad) * ((data.m_x - dt.m_summary_data[0].m_min_value) / dt.m_summary_data[0].m_range);
                double y = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * ((data.m_y - dt.m_summary_data[1].m_min_value) / dt.m_summary_data[1].m_range);
                G.FillEllipse(blackBrush, (float)x - point_w / 2, (float)y - point_w / 2, point_w, point_w);
            }

            //Without Range
            //foreach (Datapoint data in dt.m_points)
            //{
            //    double x = (m_x + m_pad) + (m_width - 2 * m_pad) * (data.m_x) / dt.m_x_max_value;
            //    double y = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (data.m_y) / dt.m_y_max_value;
            //    G.FillEllipse(blueBrush, (float)x - point_w / 2, (float)y - point_w / 2, point_w, point_w);
            //}

            double x_mean = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[0].m_mean) / dt.m_summary_data[0].m_max_value;
            Line mean_x = new Line(new Point((int)x_mean, m_vertical_axis.m_A.Y), new Point((int)x_mean, m_vertical_axis.m_B.Y));


            double y_mean = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[1].m_mean) / dt.m_summary_data[1].m_max_value;
            Line mean_y = new Line(new Point(m_horizontal_axis.m_A.X, (int)y_mean), new Point(m_horizontal_axis.m_B.X, (int)y_mean));

            G.DrawLine(redPen, mean_x.m_A, mean_x.m_B);
            G.DrawLine(redPen, mean_y.m_A, mean_y.m_B);

            double q_1 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[0].m_quartile1) / dt.m_summary_data[0].m_max_value;
            Line q1 = new Line(new Point((int)q_1, m_vertical_axis.m_A.Y), new Point((int)q_1, m_vertical_axis.m_B.Y));

            double q_2 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[0].m_quartile2) / dt.m_summary_data[0].m_max_value;
            Line q2 = new Line(new Point((int)q_2, m_vertical_axis.m_A.Y), new Point((int)q_2, m_vertical_axis.m_B.Y));

            double q_3 = (m_x + m_pad) + (m_width - 2 * m_pad) * (dt.m_summary_data[0].m_quartile3) / dt.m_summary_data[0].m_max_value;
            Line q3 = new Line(new Point((int)q_3, m_vertical_axis.m_A.Y), new Point((int)q_3, m_vertical_axis.m_B.Y));

            G.DrawLine(qPen, q1.m_A, q1.m_B);
            G.DrawLine(qPen, q2.m_A, q2.m_B);
            G.DrawLine(qPen, q3.m_A, q3.m_B);


            q_1 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[1].m_quartile1) / dt.m_summary_data[1].m_max_value;
            q1 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_1), new Point(m_horizontal_axis.m_B.X, (int)q_1));

            q_2 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[1].m_quartile2) / dt.m_summary_data[1].m_max_value;
            q2 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_2), new Point(m_horizontal_axis.m_B.X, (int)q_2));

            q_3 = (m_y + m_pad) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_summary_data[1].m_quartile3) / dt.m_summary_data[1].m_max_value;
            q3 = new Line(new Point(m_horizontal_axis.m_A.X, (int)q_3), new Point(m_horizontal_axis.m_B.X, (int)q_3));

            G.DrawLine(qPen, q1.m_A, q1.m_B);
            G.DrawLine(qPen, q2.m_A, q2.m_B);
            G.DrawLine(qPen, q3.m_A, q3.m_B);



            blackPen.Dispose();
            redPen.Dispose();
            blackBrush.Dispose();
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
