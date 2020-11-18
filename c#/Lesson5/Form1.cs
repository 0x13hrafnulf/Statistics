using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson5
{
    public partial class Form1 : Form
    {
        double N;
        double x_start, x_finish;

        public Form1()
        {
            InitializeComponent();
        }

        private void calculate_integrals()
        {
            x_start = Convert.ToDouble(textBox2.Text);
            x_finish = Convert.ToDouble(textBox3.Text);
            richTextBox1.Text = "Calculation of integrals:" + Environment.NewLine;
            richTextBox1.AppendText("Integral = " + integral(x_start, x_finish).ToString() + Environment.NewLine);
            richTextBox1.AppendText("Rectangle: _____________" + Environment.NewLine);
            rectangle_integral();
            richTextBox1.AppendText("Trapezoid: _____________" + Environment.NewLine);
            trapezoid_integral();
            richTextBox1.AppendText("Lebesque: _____________" + Environment.NewLine);
            lebesque_integral();
        }

        private void rectangle_integral()
        {
            double h;
            N = Convert.ToDouble(textBox1.Text);         
            h = (x_finish - x_start) / (N - 1);
            
           
            double theta = 0;
            double sum = 0;
            for (int i = 1; i <= (int)N; ++i)
            {
                double O_x = (x_start + (i - 1) * h) * (1 - theta) + (x_start + i * h) * theta;
                sum += ((x_start + i * h) - (x_start + (i - 1) * h)) * function(O_x);
            }
            richTextBox1.AppendText("Theta = " + theta.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Sum = " + sum.ToString() + Environment.NewLine);


            theta = 0.5;
            sum = 0;
            for (int i = 1; i <= (int)N; ++i)
            {
                double O_x = (x_start + (i - 1) * h) * (1 - theta) + (x_start + i * h) * theta;
                sum += ((x_start + i * h) - (x_start + (i - 1) * h)) * function(O_x);
            }
            richTextBox1.AppendText("Theta = " + theta.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Sum = " + sum.ToString() + Environment.NewLine);



            theta = 1;
            sum = 0;

            for (int i = 1; i <= (int)N; ++i)
            {
                double O_x = (x_start + (i - 1) * h) * (1 - theta) + (x_start + i * h) * theta;
                sum += ((x_start + i * h) - (x_start + (i - 1) * h)) * function(O_x);
            }

            richTextBox1.AppendText("Theta = " + theta.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Sum = " + sum.ToString() + Environment.NewLine);

        }
        private void trapezoid_integral()
        {
            double h;
            N = Convert.ToDouble(textBox1.Text);
            h = (x_finish - x_start) / (N - 1);


            double sum = 0;
            for (int i = 1; i <= (int)N; ++i)
            {
              
                sum += ((x_start + i * h) - (x_start + (i - 1) * h)) * (function(x_start + (i - 1) * h) + function(x_start + (i - 1) * h)) / 2.0;
            }
            richTextBox1.AppendText("Sum = " + sum.ToString() + Environment.NewLine);

        }
        private void lebesque_integral()
        {
            double h;
            double steps = Convert.ToDouble(textBox4.Text);
            h = (x_finish - x_start) / (steps - 1);


            double max = 0;

            for (int i = 1; i <= (int)steps; ++i)
            {
                double f = function(x_start + (i - 1) * h);
                max = f > max ? f : max;
            }


            N = Convert.ToDouble(textBox1.Text);
            double w = max / (N - 1);

            double sum = 0;
            for (int i = 1; i <= (int)N; ++i)
            {
                bool a = false;
                bool b = false;
                int x_a = 0;
                int x_b = 0;
                for (int j = 1; j <= (int)steps; ++j)
                {
                    int check = slice_lebesque(i, i - 1, w, x_start + (j - 1) * h);
                    if (!a)
                    {
                        if (check == i)
                        {
                            x_a = j-1;
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
                            x_a = j-1;
                            b = false;
                            a = true;              
                        }
                    }

                }

            }


            richTextBox1.AppendText("Sum = " + sum.ToString() + Environment.NewLine);

        }
        private double function(double x)
        {
            return x * 3;
            //return Math.Pow(x, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculate_integrals();
        }

        private double integral(double x_a, double x_b)
        {

            return 3 * Math.Pow(x_b, 2) / 2.0 - 3 * Math.Pow(x_a, 2) / 2.0;
            //return Math.Pow(x_b, 3) / 3.0 - Math.Pow(x_a, 3) / 3.0;
        }
        private int slice_lebesque(int up_bound, int low_bound, double delta, double x)
        {
            double f = function(x);

            if (f > up_bound * delta)
                return up_bound;
        
            return low_bound;
        }

    }


}
