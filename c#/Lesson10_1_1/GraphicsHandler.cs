using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson10_1_1
{
    public class GraphicsHandler
    {
        public Graphics m_G;
        public Bitmap m_bitmap;
        public PictureBox m_pictureBox;

        public List<Viewport> m_viewports = new List<Viewport>();

        public GraphicsHandler(Graphics G, Bitmap b, PictureBox p)
        {
            m_G = G;
            m_bitmap = b;
            m_pictureBox = p;
        }

    }
}
