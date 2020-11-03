using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{
    public class Dataset
    {
        public int m_count;

        //X
        public string m_x_name;
        public double m_x_min_value;
        public double m_x_max_value;
        public double m_x_range;
        public double m_x_mean;

        //Y
        public string m_y_name;
        public double m_y_min_value;
        public double m_y_max_value;
        public double m_y_range;
        public double m_y_mean;

        public List<Datapoint> m_points = new List<Datapoint>();
        public IntervalList m_x_intervals;
        public IntervalList m_y_intervals;
        public bivariate_distribution m_distribution;

        public Dataset(Variable first, Variable second)
        {
            m_count = first.m_values.Count;
            m_x_name = first.m_name;
            m_y_name = second.m_name;

            for (int i = 0; i < m_count; ++i)
            {
                m_points.Add(new Datapoint(first.get(i), second.get(i)));
            }


        }

        public void process_data(int x_intervals, int y_intervals) 
        {
            m_x_mean = m_points[0].m_x;
            m_x_min_value = m_points[0].m_x;
            m_x_max_value = m_points[0].m_x;

            m_y_mean = m_points[0].m_y;
            m_y_min_value = m_points[0].m_y;
            m_y_max_value = m_points[0].m_y;

            for (int i = 1; i < m_count; ++i)
            { 
                m_x_mean += Math.Round((double)(m_points[i].m_x - m_x_mean) / (i + 1), 2);
                m_y_mean += Math.Round((double)(m_points[i].m_y - m_y_mean) / (i + 1), 2);

                m_x_min_value = m_points[i].m_x < m_x_min_value ? m_points[i].m_x : m_x_min_value;
                m_x_max_value = m_points[i].m_x > m_x_max_value ? m_points[i].m_x : m_x_max_value;

                m_y_min_value = m_points[i].m_y < m_y_min_value ? m_points[i].m_y : m_y_min_value;
                m_y_max_value = m_points[i].m_y > m_y_max_value ? m_points[i].m_y : m_y_max_value;              
            }

            m_x_range = m_x_max_value - m_x_min_value;
            m_y_range = m_y_max_value - m_y_min_value;

            m_x_intervals = new IntervalList(x_intervals, m_x_name);
            m_y_intervals = new IntervalList(y_intervals, m_y_name);

            m_x_intervals.populate(m_x_min_value, m_x_max_value);
            m_y_intervals.populate(m_y_min_value, m_y_max_value);

            for (int i = 0; i < m_count; ++i)
            {
                m_x_intervals.check_intervals(m_points[i].m_x);
                m_y_intervals.check_intervals(m_points[i].m_y);
            }
            m_x_intervals.find_densities();
            m_y_intervals.find_densities();
            m_distribution = new bivariate_distribution(m_x_intervals, m_y_intervals);
            m_distribution.compute_frequencies(m_points);
        }
        public void recalculate_intervals(int x_intervals, int y_intervals)
        {
            m_x_intervals.repopulate(x_intervals, m_x_min_value, m_x_max_value);
            m_y_intervals.repopulate(y_intervals, m_y_min_value, m_y_max_value);
            for (int i = 0; i < m_count; ++i)
            {
                m_x_intervals.check_intervals(m_points[i].m_x);
                m_y_intervals.check_intervals(m_points[i].m_y);
            }
            m_x_intervals.find_densities();
            m_y_intervals.find_densities();
            m_distribution = new bivariate_distribution(m_x_intervals, m_y_intervals);
            m_distribution.compute_frequencies(m_points);

        }
        public void transform(Viewport chart)
        { 
            
        }
    }

    public class Datapoint
    {
        public double m_x;
        public double m_y;

        public Datapoint(double x, double y)
        {
            m_x = x;
            m_y = y;
        }
    }
}
