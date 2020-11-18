using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson6
{
    public class Interval
    {
        public dynamic m_size;
        public dynamic m_starting_point;
        public dynamic m_ending_point;
        public double m_mean;
        public int m_count;
        public float m_density;


        public Interval(dynamic start, dynamic size)
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
        public float m_max_density;
        public int m_index;

        public double relative_frequency;
        public IntervalList(int count, string name, int index)
        {
            m_count = count;
            m_name = name;
            m_index = index; 
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
        public void populate_via_existing_interval(int n, double start_p, double end_p, double min, double max)
        {
            m_count = n;
            double mid_size = (end_p - start_p);

            Interval mid_interv = new Interval(start_p, mid_size);

            double size = (max - min - mid_size) / (m_count - 1);
            double start = min;
            for (int i = 0; i < m_count; ++i)
            {
                if ((m_count - 1) / 2 == i)
                {
                    m_intervals.Add(mid_interv);
                    start += mid_size;
                    continue;
                }
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

        public void check_intervals(double value)
        {
            //int index = ((int)value - m_intervals[0].m_starting_point) / m_intervals[0].m_size;
            //if (index == m_count) index = m_count - 1;
            //m_intervals[index].m_count += 1;
            //m_intervals[index].update_mean(value);

            for (int i = 0; i < m_count; ++i)
            {
                if (m_intervals[i].m_starting_point <= value && value < m_intervals[i].m_ending_point)
                {
                    m_intervals[i].m_count += 1;
                    m_intervals[i].update_mean(value);
                    break;
                }
                else if (i + 1 == m_count)
                {
                    m_intervals[i].m_count += 1;
                    m_intervals[i].update_mean(value);
                    break;
                }
            }
        }

        public void find_densities()
        {
            
            foreach (Interval i in m_intervals)
            {
                i.m_density = (float)i.m_count / (float)i.m_size;
            }

            m_max_density = m_intervals[0].m_density;

            for (int i = 1; i < m_count; ++i)
            {
                m_max_density = m_max_density > m_intervals[i].m_density ? m_max_density : m_intervals[i].m_density;
            }
        }
        public void calculate_relative_frequency()
        {
            relative_frequency = (double)m_intervals[1].m_count / (double)(m_intervals[1].m_count + m_intervals[0].m_count);
        }
    }

    public class bivariate_distribution
    {
        public int[,] m_frequency;
        public double[,] m_relative_frequency;

        public IntervalList m_x_intervals;
        public IntervalList m_y_intervals;

        public int m_x_index;
        public int m_y_index;

        public bivariate_distribution(IntervalList x_interval, IntervalList y_interval)
        {
            m_x_intervals = x_interval;
            m_y_intervals = y_interval;

            m_frequency = new int[m_x_intervals.m_count + 1, m_y_intervals.m_count + 1];
            m_relative_frequency = new double[m_x_intervals.m_count + 1, m_y_intervals.m_count + 1];
        }

        public void compute_frequencies(List<Datapoint> points)
        {
            
            foreach (Datapoint d in points)
            {
                int i = ((int)d.m_x - m_x_intervals.m_intervals[0].m_starting_point) / m_x_intervals.m_intervals[0].m_size;
                int j = ((int)d.m_y - m_y_intervals.m_intervals[0].m_starting_point) / m_y_intervals.m_intervals[0].m_size;
                if (i == m_x_intervals.m_count) i = m_x_intervals.m_count - 1;
                if (j == m_y_intervals.m_count) j = m_y_intervals.m_count - 1;

                m_frequency[i, j] += 1;

            }

            for (int i = 0, sum = 0, j = 0; i < m_x_intervals.m_count + 1;)
            {
                if (j < m_y_intervals.m_count)
                {
                    sum += m_frequency[i, j];
                    ++j;
                }
                else
                {
                    m_frequency[i, j] = sum;
                    j = 0;
                    sum = 0;
                    ++i;
                }              
            }

            for (int i = 0, sum = 0, j = 0; j < m_y_intervals.m_count + 1;)
            {
                if (i < m_x_intervals.m_count)
                {
                    sum += m_frequency[i, j];
                    ++i;
                }
                else
                {
                    m_frequency[i, j] = sum;
                    i = 0;
                    sum = 0;
                    ++j;
                }
            }


        }

    }
       
}
