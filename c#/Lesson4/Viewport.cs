using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Lesson4
{
    public enum Chart_type
    { 
        ContingencyTable,
        Scatterplot,
        Historgram
    }


    public class Viewport
    {
        public int m_x;
        public int m_y;
        public int m_width;
        public int m_height;
        public Chart_type m_type;
        public Rectangle m_rectangle;

        public Viewport m_parent;

        public Viewport(int x, int y, int width, int height, Chart_type type)
        {
            m_x = x;
            m_y = y;
            m_width = width;
            m_height = height;
            m_type = type;
            m_rectangle = new Rectangle(m_x, m_y, m_width, m_height);
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
    }
}
