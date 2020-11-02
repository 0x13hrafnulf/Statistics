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
        public int m_size;
        public int m_starting_point;
        public int m_ending_point;
        public double m_mean;
        public int m_count;
        public int m_density;

        public Interval(int start, int size)
        {
            m_starting_point = start;
            m_size = size;
            m_ending_point = start + size;
            m_mean = 0;
        }
       
        public void update_mean(double value)
        {
            m_mean += Math.Round((double)(value - m_mean) / (m_count), 2);
        }
        public override string ToString()
        {
            return string.Format("[{0},{1})", m_starting_point, m_ending_point);
        }
    }
    public class IntervalList
    {
        public int m_count;
        public string m_name;
        public List<Interval> m_intervals;
        public int m_max_density;

        public IntervalList(int count, string name)
        {
            m_count = count;
            m_name = name;
            m_intervals = new List<Interval>();
        }

        public void populate(double min, double max)
        {
            int size = (int)(max - min);
            size = 1 + size/m_count;
            int start = (int)(min);
            for (int i = 0; i < m_count; ++i)
            {
                Interval interv = new Interval(start, size);
                m_intervals.Add(interv);
                start += size;
            }
        }
        public void repopulate(int count, double min, double max)
        {
            m_count = count;
            m_intervals.Clear();
            populate(min, max);
        }

        public void check_intervals(int value)
        {
            int index = (value - m_intervals[0].m_starting_point) / m_intervals[0].m_size;
            if (index == m_count) index = m_count - 1;
            m_intervals[index].m_count += 1;
            m_intervals[index].update_mean(value);

            //for (int i = 0; i < m_count; ++i)
            //{
            //    if (m_intervals[i].m_starting_point <= value && value < m_intervals[i].m_ending_point) 
            //    {
            //        m_intervals[i].m_count += 1;
            //        m_intervals[i].update_mean(value);
            //    } 
            //}   
        }

        public void find_densities()
        {
            
            foreach (Interval i in m_intervals)
            {
                i.m_density = i.m_count / i.m_size;
            }

            m_max_density = m_intervals[0].m_density;

            for (int i = 1; i < m_count; ++i)
            {
                m_max_density = m_max_density > m_intervals[i].m_density ? m_max_density : m_intervals[i].m_density;
            }
        }
    }
       
}
