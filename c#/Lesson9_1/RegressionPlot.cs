using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lesson9_1
{
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
        Axis m_axis;

        public RegressionPlot(int x, int y, int width, int height) : base(x, y, width, height)
        {
            m_pad = 20;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_grid = 33;
            m_axis = new Axis(x, y, width, height, m_pad);

        }
        public override void draw(Graphics G, Dataset dt)
        {
            m_axis.draw(G);

            Pen P = new Pen(Color.Blue, 1);

            //LINES

            for (int i = 0; i < dt.m_number_of_variables; ++i)
            {

                Pen line_pen = new Pen(dt.m_points[i].m_color, 2);

                for (int j = 1; j < dt.m_number_of_points; ++j)
                {
                    double A_x = (m_x + m_pad + m_dx) + (m_width - 2 * m_pad) * (dt.m_points[i].m_points[j - 1].m_x / dt.m_number_of_points);
                    double A_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_points[i].m_points[j - 1].m_y - dt.m_min) /(dt.m_max - dt.m_min);

                    double B_x = (m_x + m_pad + m_dx) + (m_width - 2 * m_pad) * (dt.m_points[i].m_points[j].m_x / dt.m_number_of_points);
                    double B_y = (m_y + m_pad + m_dy) + (m_height - 2 * m_pad) - (m_height - 2 * m_pad) * (dt.m_points[i].m_points[j].m_y - dt.m_min) /(dt.m_max - dt.m_min);

                    Point A = new Point((int)A_x, (int)A_y);
                    Point B = new Point((int)B_x, (int)B_y);

                    G.DrawLine(line_pen, A, B);
                }

                line_pen.Dispose();
            }


            P.Dispose();


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
