using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Lesson4
{
    public class Viewport
    {
        public int m_x;
        public int m_y;
        public int m_width;
        public int m_height;
        
        public Rectangle rectangle;

        public Viewport(int x, int y, int width, int height)
        {
            m_x = x;
            m_y = y;
            m_width = width;
            m_height = height;

            rectangle = new Rectangle(m_x, m_y, m_width, m_height);
        }
    }
}
