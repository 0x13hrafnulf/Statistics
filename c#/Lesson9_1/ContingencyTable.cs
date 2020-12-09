using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson9_1
{
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
}
