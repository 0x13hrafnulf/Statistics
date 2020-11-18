using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lesson6
{
    public class SummaryData
    {
        public int m_index;

        public string m_name;
        public double m_min_value;
        public double m_max_value;
        public double m_range;
        public double m_mean;
        public double m_variance;

        public int m_intervals;
        //temp
        public double m_relative_frequency;

    };
    public class Datapoint
    {
        public double m_x;
        public double m_y;

        public Datapoint(double x, double y)
        {
            m_x = x;
            m_y = y;
        }
    };
    public class DatapointsList
    {
        public int m_x_index;
        public int m_y_index;
        public List<Datapoint> m_points;
        public Color m_color;
        public DatapointsList(int x_index, int y_index)
        {
            m_x_index = x_index;
            m_y_index = y_index;

            m_points = new List<Datapoint>();
        }
        public void add_point(Datapoint point)
        {
            m_points.Add(point);
        }
    };

    public class Dataset
    {
        public int m_number_of_variables = 0;
        public int m_number_of_points;


        public List<Variable> m_variables = new List<Variable>();
        public List<DatapointsList> m_points = new List<DatapointsList>();

        //Summary DATA
        public List<IntervalList> m_intervals;
        public List<SummaryData> m_summary_data;
        public List<bivariate_distribution> m_distributions;

        
        public Dataset(int n_points)
        {
            m_number_of_points = n_points;

            m_variables = new List<Variable>();
            m_points = new List<DatapointsList>();

            m_intervals = new List<IntervalList>();
            m_summary_data = new List<SummaryData>();
            m_distributions = new List<bivariate_distribution>();
        }
        public void clear()
        {
            m_variables.Clear();
            m_points.Clear();
            m_intervals.Clear();
            m_summary_data.Clear();
            m_distributions.Clear();
            m_number_of_points = 0;
            m_number_of_variables = 0;
        }
        public void set(int n_points)
        {
            m_number_of_points = n_points;
        }
        public void add_variable(int n_points, Variable one_variable)
        {
            m_variables.Add(one_variable);
            process_variable(m_number_of_variables);

            ++m_number_of_variables;
        }
        public void process_variable(int index)
        {
            SummaryData summary = new SummaryData();
            summary.m_index = index;

            summary.m_max_value = m_variables[index].get(0);
            summary.m_min_value = m_variables[index].get(0);
            summary.m_mean = m_variables[index].get(0);
            summary.m_intervals = 1;

            for (int i = 1; i < m_number_of_points; ++i)
            {
                summary.m_mean += Math.Round((double)(m_variables[index].get(i) - summary.m_mean) / (i + 1), 2);
                summary.m_min_value = m_variables[index].get(i) < summary.m_min_value ? m_variables[index].get(i) : summary.m_min_value;
                summary.m_max_value = m_variables[index].get(i) > summary.m_max_value ? m_variables[index].get(i) : summary.m_max_value;
            }

            summary.m_range = summary.m_max_value - summary.m_min_value;
            m_summary_data.Add(summary);
        }
        public void add_datapoint(int x_index, int y_index)
        {
            DatapointsList list = new DatapointsList(x_index, y_index);
            for (int i = 0; i < m_number_of_points; ++i)
            {
                list.add_point(new Datapoint(m_variables[x_index].get(i), m_variables[y_index].get(i)));
            }
            m_points.Add(list);
        }
        public void add_datapoint(int y_index)
        {
            DatapointsList list = new DatapointsList(-1, y_index);
            for (int i = 0; i < m_number_of_points; ++i)
            {
                list.add_point(new Datapoint(i, m_variables[y_index].get(i)));
            }
            m_points.Add(list);
        }
        public void add_Bernoulli_datapoint(int y_index, Color color)
        {         
            
            DatapointsList list = new DatapointsList(-1, y_index);
            list.m_color = color;

            double value = 0;
            for (int i = 0; i < m_number_of_points; ++i)
            {
                value += Math.Round((double)(m_variables[y_index].get(i) - value) / (i + 1), 2);
                list.add_point(new Datapoint(i, value));
            }
            m_points.Add(list);
        }
        public void process_Bernoulli_intervals(int time_index, int n_intervals, double p, double e)
        {
            m_intervals.Clear();
            IntervalList list = new IntervalList(n_intervals, time_index.ToString(), time_index);

            list.populate_via_existing_interval(n_intervals, p-e, p+e, 0, 1);

            for (int i = 0; i < m_number_of_variables; ++i)
            {

                list.check_intervals(m_points[i].m_points[time_index].m_y);
            }

            list.find_densities();
            m_intervals.Add(list);
        }

        public void process_intervals(int index, int n_intervals)
        {


            m_summary_data[index].m_intervals = n_intervals;
            IntervalList list = new IntervalList(n_intervals, m_summary_data[index].m_name, index);
            list.populate(m_summary_data[index].m_min_value, m_summary_data[index].m_max_value);

            for (int i = 0; i < m_number_of_points; ++i)
            {
                list.check_intervals(m_variables[index].get(i));
            }
            list.find_densities();
            m_intervals.Insert(index, list);
        }
        public void recalculate_intervals(int index, int n_intervals)
        {
            m_summary_data[index].m_intervals = n_intervals;
            m_intervals[index].repopulate(n_intervals, m_summary_data[index].m_min_value, m_summary_data[index].m_max_value);
            for (int i = 0; i < m_number_of_points; ++i)
            {
                m_intervals[index].check_intervals(m_variables[index].get(i));
            }
            m_intervals[index].find_densities();
        }

        public void add_bivariate_distribution(int x_index, int y_index)
        {
            //m_distribution = new bivariate_distribution(m_x_intervals, m_y_intervals);
            //m_distribution.compute_frequencies(m_points);
        }
        public void recalculate_bivariate_distributions(int x_index, int y_index)
        {
           //m_distribution = new bivariate_distribution(m_x_intervals, m_y_intervals);
           //m_distribution.compute_frequencies(m_points);
        }

        public void transform(Viewport chart)
        { 
            
        }
    }

    
}
