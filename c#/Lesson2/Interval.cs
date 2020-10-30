using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson2
{
    abstract class Interval
    {



    }
    class IntegerInterval : Interval
    {
        public int m_size;
        public int m_starting_point;
        public int m_ending_point;
        public uint m_count;

        public IntegerInterval(int size, int start, int end)
        {
            m_size = size;
            m_starting_point = start;
            m_ending_point = end;
            m_count = 0;
        }
    }
    class DecimalInterval : Interval
    {
        public double m_size;
        public double m_starting_point;
        public double m_ending_point;
        public uint m_count;
        public DecimalInterval(double size, double start, double end)
        {
            m_size = size;
            m_starting_point = start;
            m_ending_point = end;
            m_count = 0;
        }
    }
}
