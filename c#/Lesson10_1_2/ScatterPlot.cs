using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson10_1_2
{
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
}
