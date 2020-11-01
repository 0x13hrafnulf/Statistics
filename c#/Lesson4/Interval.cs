using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson4
{
    public class Interval
    {
        public dynamic m_size;
        public dynamic m_starting_point;
        public dynamic m_ending_point;
        public uint m_count;
        public Type m_type;

        public Interval(Type type, dynamic start, dynamic size)
        {
            m_type = type;
            m_starting_point = start;
            m_size = size;
            m_ending_point = start + size;
            set_type();
        }
        public void set_type()
        {
            if (m_type == typeof(Int32))
            {
                m_size = Convert.ToInt32(m_size);
                m_starting_point = Convert.ToInt32(m_starting_point);
                m_ending_point = Convert.ToInt32(m_ending_point);
            }
            else if (m_type == typeof(Double))
            {
                m_size = Convert.ToDouble(m_size);
                m_starting_point = Convert.ToDouble(m_starting_point);
                m_ending_point = Convert.ToDouble(m_ending_point);
            }

        }
    }

    //public class IntegerInterval : Interval
    //{
    //    public int m_size;
    //    public int m_starting_point;
    //    public int m_ending_point;
    //    public uint m_count;

    //    public IntegerInterval(int size, int start, int end)
    //    {
    //        m_size = size;
    //        m_starting_point = start;
    //        m_ending_point = end;
    //        m_count = 0;
    //    }
    //}
    //public class DecimalInterval : Interval
    //{
    //    public double m_size;
    //    public double m_starting_point;
    //    public double m_ending_point;
    //    public uint m_count;
    //    public DecimalInterval(double size, double start, double end)
    //    {
    //        m_size = size;
    //        m_starting_point = start;
    //        m_ending_point = end;
    //        m_count = 0;
    //    }
    //}
}
