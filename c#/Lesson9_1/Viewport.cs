using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson9_1
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

    public class Axis : Viewport
    {
        int m_pad;
        Line m_vertical_axis;
        Line m_horizontal_axis;
        int m_grid;
        int m_dx = 0;
        int m_dy = 0;
        int m_dx_resize = 0;
        int m_dy_resize = 0;
        public Axis(int x, int y, int width, int height, int pad) : base(x, y, width, height)
        {
            m_pad = pad;
            m_horizontal_axis = new Line(new Point(m_x + m_pad, m_y + m_height - m_pad), new Point(m_x + m_width - m_pad, m_y + m_height - m_pad));
            m_vertical_axis = new Line(new Point(m_x + m_pad, m_y + m_pad), new Point(m_x + m_pad, m_y + m_height - m_pad));
            m_grid = 33;

        }
        public void draw(Graphics G)
        {
            //PENS
            Pen gridpen = new Pen(Color.White, 1);
            Pen axisPen = new Pen(Color.Black, 2);

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