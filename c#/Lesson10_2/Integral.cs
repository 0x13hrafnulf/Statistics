using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson10_2
{
    public class Integral
    {
        double m_start;
        double m_finish;
        double m_N;
        double m_steps;
        double m_h;

        public Integral(double start, double finish, double N)
        {
            m_start = start;
            m_finish = finish;
            m_N = N;
        }

        private void rectangle_integral()
        {
            double h;
            h = (m_finish - m_start) / (m_N - 1);


            double theta = 0;
            double sum = 0;
            for (int i = 1; i <= (int)m_N; ++i)
            {
                double O_x = (m_start + (i - 1) * h) * (1 - theta) + (m_start + i * h) * theta;
                sum += ((m_start + i * h) - (m_start + (i - 1) * h)) * function(O_x);
            }


            theta = 0.5;
            sum = 0;
            for (int i = 1; i <= (int)m_N; ++i)
            {
                double O_x = (m_start + (i - 1) * h) * (1 - theta) + (m_start + i * h) * theta;
                sum += ((m_start + i * h) - (m_start + (i - 1) * h)) * function(O_x);
            }


            theta = 1;
            sum = 0;

            for (int i = 1; i <= (int)m_N; ++i)
            {
                double O_x = (m_start + (i - 1) * h) * (1 - theta) + (m_start + i * h) * theta;
                sum += ((m_start + i * h) - (m_start + (i - 1) * h)) * function(O_x);
            }



        }
        private void trapezoid_integral()
        {
            double h;
            h = (m_finish - m_start) / (m_N - 1);


            double sum = 0;
            for (int i = 1; i <= (int)m_N; ++i)
            {

                sum += ((m_start + i * h) - (m_start + (i - 1) * h)) * (function(m_start + (i - 1) * h) + function(m_start + (i - 1) * h)) / 2.0;
            }


        }
        private void lebesque_integral()
        {
            double h;        
            h = (m_finish - m_start) / (m_N - 1);
            double steps = h; // Convert.ToDouble(textBox4.Text)

            double max = 0;

            for (int i = 1; i <= (int)steps; ++i)
            {
                double f = function(m_start + (i - 1) * h);
                max = f > max ? f : max;
            }


            double w = max / (m_N - 1);

            double sum = 0;
            for (int i = 1; i <= (int)m_N; ++i)
            {
                bool a = false;
                bool b = false;
                int x_a = 0;
                int x_b = 0;
                for (int j = 1; j <= (int)steps; ++j)
                {
                    int check = slice_lebesque(i, i - 1, w, m_start + (j - 1) * h);
                    if (!a)
                    {
                        if (check == i)
                        {
                            x_a = j - 1;
                            a = true;
                        }
                    }
                    else if (a)
                    {
                        if (check == i - 1)
                        {
                            x_b = j;
                            b = true;
                            sum += w * (x_b * h - x_a * h);
                            a = false;
                        }
                        else if (j == steps)
                        {
                            x_b = j;
                            sum += w * (x_b * h - x_a * h);
                        }
                    }
                    else if (!a && b)
                    {
                        if (check == i)
                        {
                            x_a = j - 1;
                            b = false;
                            a = true;
                        }
                    }

                }

            }
        }
        private int slice_lebesque(int up_bound, int low_bound, double delta, double x)
        {
            double f = function(x);

            if (f > up_bound * delta)
                return up_bound;

            return low_bound;
        }
        private double function(double x)
        {
            return 0;
        }

    }
}
