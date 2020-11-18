using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public List<Variable> Data = new List<Variable>();
        public List<DatapointsList> m_points = new List<DatapointsList>();

        //Summary DATA
        public List<IntervalList> m_intervals;
        public List<SummaryData> m_summary_data;
        public List<bivariate_distribution> m_distributions;

        
        public Dataset(int n_points)
        {
            m_number_of_points = n_points;

            Data = new List<Variable>();
            m_points = new List<DatapointsList>();

            m_intervals = new List<IntervalList>();
            m_summary_data = new List<SummaryData>();
            m_distributions = new List<bivariate_distribution>();
        }
        public void add_variable(int n_points, Variable one_variable)
        {          
            Data.Add(one_variable);
            process_variable(m_number_of_variables);

            ++m_number_of_variables;
        }
        public void process_variable(int index)
        {
            SummaryData summary = new SummaryData();
            summary.m_index = index;

            summary.m_max_value = Data[index].get(0);
            summary.m_min_value = Data[index].get(0);
            summary.m_mean = Data[index].get(0);
            summary.m_intervals = 1;

            for (int i = 1; i < m_number_of_points; ++i)
            {
                summary.m_mean += Math.Round((double)(Data[index].get(i) - summary.m_mean) / (i + 1), 2);
                summary.m_min_value = Data[index].get(i) < summary.m_min_value ? Data[index].get(i) : summary.m_min_value;
                summary.m_max_value = Data[index].get(i) > summary.m_max_value ? Data[index].get(i) : summary.m_max_value;
            }

            summary.m_range = summary.m_max_value - summary.m_min_value;
            m_summary_data.Add(summary);
        }
        public void add_datapoint(int x_index, int y_index)
        {
            DatapointsList list = new DatapointsList(x_index, y_index);
            for (int i = 0; i < m_number_of_points; ++i)
            {
                list.add_point(new Datapoint(Data[x_index].get(i), Data[y_index].get(i)));
            }
        }
        public void add_datapoint(int y_index)
        {
            DatapointsList list = new DatapointsList(-1, y_index);
            for (int i = 0; i < m_number_of_points; ++i)
            {
                list.add_point(new Datapoint(i, Data[y_index].get(i)));
            }
        }
        public void process_intervals(int index, int n_intervals)
        {

        }
        public void recalculate_intervals(int index, int n_intervals)
        {

        }

        public void add_bivariate_distribution(int x_index, int y_index)
        {

        }
        public void recalculate_bivariate_distributions(int x_index, int y_index)
        {

        }

        public void process_data(int x_intervals, int y_intervals) 
        {
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

    
}
