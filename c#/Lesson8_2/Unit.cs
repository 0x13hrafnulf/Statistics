using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson8_2
{
    class Unit
    {
        public int m_index;
        public string m_name;

        public Unit(int index, string name)
        {
            m_index = index;
            m_name = name;
        }

        public int get_index()
        {
            return m_index;
        }
        public string get_name()
        {
            return m_name;
        }
    }
}
