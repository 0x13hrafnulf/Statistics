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
        public double m_x_min_value;
        public double m_x_max_value;
        public double m_x_range;
        public double m_x_mean;
        
        //Y
        public double m_y_min_value;
        public double m_y_max_value;
        public double m_y_range;
        public double m_y_mean;

        public List<Datapoint> m_points = new List<Datapoint>();
        public List<Interval> m_x_intervals = new List<Interval>();
        public List<Interval> m_y_intervals = new List<Interval>();

        public Dataset(Variable first, Variable second)
        {
            m_count = first.m_values.Count;

            m_x_mean = 0;
            m_x_min_value = 0;
            m_x_max_value = 0;

            m_y_mean = 0;
            m_y_min_value = 0;
            m_y_max_value = 0;

            for (int i = 0; i < m_count; ++i)
            {
                m_points.Add(new Datapoint(first.get(i), second.get(i)));
            }    
        }

        public void process_data() 
        {

            for(int i = 0; i < m_count; ++i)
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
        }
        public void calculate_intervals(int x_intervals, int y_intervals)
        { 
        
        
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
