using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson2_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            print_example();
        }

        public void print_example()
        {

            double result_1 = 0;
            double result_2 = 0;
            int n = 100000;
            double value_1 = Math.PI;
            double value_2 = Math.E;
            richTextBox1.Text = "N = 100000";
            richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);
            richTextBox1.AppendText("Naive approach for arithmetic mean: " + Environment.NewLine);
            richTextBox1.AppendText("------------------------------------" + Environment.NewLine);

            for (int i = 0; i < n; ++i)
            {
                result_1 += value_1;
                result_2 += value_2;
            }
            result_1 /= n;
            result_2 /= n;

            richTextBox1.AppendText("(3.1415926535897931 + 3.1415926535897931 + ... + 3.1415926535897931) / N  = " + result_1 + Environment.NewLine);
            richTextBox1.AppendText("(2.7182818284590451 + 2.7182818284590451 + ... + 2.7182818284590451) / N  = " + result_2 + Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);
            result_1 = 0;
            result_2 = 0;
            richTextBox1.AppendText("Knuth's approach for arithmetic mean: " + Environment.NewLine);
            richTextBox1.AppendText("------------------------------------" + Environment.NewLine);
            for (int i = 0; i < n; ++i)
            {
                result_1 += (value_1 - result_1) / (i + 1);
                result_2 += (value_2 - result_2) / (i + 1);
            }


            richTextBox1.AppendText("(3.1415926535897931 + 3.1415926535897931 + ... + 3.1415926535897931) / N  = " + result_1 + Environment.NewLine);
            richTextBox1.AppendText("(2.7182818284590451 + 2.7182818284590451 + ... + 2.7182818284590451) / N  = " + result_2 + Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);
            richTextBox1.AppendText("Kahan's approach for arithmetic mean: " + Environment.NewLine);
            richTextBox1.AppendText("------------------------------------" + Environment.NewLine);

            result_1 = 0;
            result_2 = 0;
            double c1 = 0;
            double c2 = 0;
            for (int i = 0; i < n; ++i)
            {
                double y1 = value_1 - c1;
                double t1 = result_1 + y1;
                c1 = (t1 - result_1) - y1;
                result_1 = t1;

                double y2 = value_2 - c2;
                double t2 = result_2 + y2;
                c2 = (t2 - result_2) - y2;
                result_2 = t2;
            }
            result_1 /= n;
            result_2 /= n;

            richTextBox1.AppendText("(3.1415926535897931 + 3.1415926535897931 + ... + 3.1415926535897931) / N  = " + result_1 + Environment.NewLine);
            richTextBox1.AppendText("(2.7182818284590451 + 2.7182818284590451 + ... + 2.7182818284590451) / N  = " + result_2 + Environment.NewLine);
            richTextBox1.AppendText("____________________________________" + Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine);

            richTextBox1.AppendText("Naive approach for sum: " + Environment.NewLine);
            richTextBox1.AppendText("------------------------------------" + Environment.NewLine);
            
            result_1 = 0;
            result_2 = 0;
            value_1 = 0.1;
            value_2 = 0.3;
            for (int i = 0; i < n; ++i)
            {
                result_1 += value_1;
                result_2 += value_2;
            }

            richTextBox1.AppendText("(0.1 + 0.1 + ... + 0.1) = " + result_1 + Environment.NewLine);
            richTextBox1.AppendText("(0.3 + 0.3 + ... + 0.3) = " + result_2 + Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine + Environment.NewLine);

            richTextBox1.AppendText("Kahan's approach for sum: " + Environment.NewLine);
            richTextBox1.AppendText("------------------------------------" + Environment.NewLine);
            
            result_1 = 0;
            result_2 = 0;
            c1 = 0;
            c2 = 0;
            for (int i = 0; i < n; ++i)
            {
                double y1 = value_1 - c1;
                double t1 = result_1 + y1;
                c1 = (t1 - result_1) - y1;
                result_1 = t1;

                double y2 = value_2 - c2;
                double t2 = result_2 + y2;
                c2 = (t2 - result_2) - y2;
                result_2 = t2;
            }

            richTextBox1.AppendText("(0.1 + 0.1 + ... + 0.1) = " + result_1 + Environment.NewLine);
            richTextBox1.AppendText("(0.3 + 0.3 + ... + 0.3) = " + result_2 + Environment.NewLine);

        }


    }
}
