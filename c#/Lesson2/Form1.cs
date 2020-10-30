using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson2
{
    public partial class Form1 : Form
    {

        ArrayList age_intervals = new ArrayList();
        ArrayList weight_intervals = new ArrayList();
        ArrayList height_intervals = new ArrayList();
        SortedDictionary<int, int> grades = new SortedDictionary<int, int>();
        SortedDictionary<int, int> number_of_exams = new SortedDictionary<int, int>();
        int number_of_students = 0;
        Random rnd;

        //                  Summary
        //age
        double mean_age = 0;
        //weight
        double mean_weight = 0;
        //height
        double mean_height = 0;
        //grade
        double mean_grade = 0;
        //exams
        double mean_exam = 0;


        //In case I need store stat units 
        //SortedList students = new SortedList();
        public Form1()
        {
            InitializeComponent();
            rnd = new Random();
            richTextBox2.Text = "DATA STREAM" + Environment.NewLine;
            richTextBox2.AppendText("[Student#|Age|Weight|Height|Avg_Grade|Number_of_Exams]" + Environment.NewLine);
            richTextBox2.AppendText(Environment.NewLine + Environment.NewLine);

            //Initialize intervals
            int age_interval_size = 5;
            int age_start = 20;
            int age_end = 25;
            age_intervals.Add(new IntegerInterval(age_interval_size, age_start, age_end));


            double weight_interval_size = 10;
            double weight_start = 70;
            double weight_end = 80;
            weight_intervals.Add(new DecimalInterval(weight_interval_size, weight_start, weight_end));


            double height_interval_size = 10;
            double height_start = 170;
            double height_end = 180;
            height_intervals.Add(new DecimalInterval(height_interval_size, height_start, height_end));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            ++number_of_students;
            generate_object();
            print_summary();
        }

        void generate_object()
        {

            double max_weight = 120.00;
            double min_weight = 45.00;

            double max_height = 140.00;
            double min_height = 220.00;

            int age = rnd.Next(21, 35);
            double weight = Math.Round(min_weight + (max_weight - min_weight) * rnd.NextDouble(), 2);
            double height = Math.Round(min_height + (max_height - min_height) * rnd.NextDouble(), 2);
            int avg_grade = rnd.Next(18, 30);
            int number_of_exams = rnd.Next(1, 6);

            Student stud = new Student(age, weight, height, avg_grade, number_of_exams);
            print_object_info(stud);
            
            update_mean(stud);
            update_distribution(stud);
        }
        private void print_object_info(Student stdnt)
        {
            richTextBox2.AppendText(("[ Student#").PadRight(13) + number_of_students 
                                                + " | " + (stdnt.m_age.ToString()).PadRight(2)
                                                + " | " + (stdnt.m_weight.ToString()).PadRight(6)
                                                + " | " + (stdnt.m_height.ToString()).PadRight(6)
                                                + " | " + (stdnt.m_avg_grade.ToString()).PadRight(2)
                                                + " | " + (stdnt.m_number_of_exams.ToString()).PadRight(2)
                                                + " ]"
                                                + Environment.NewLine);
        }

        private void update_mean(Student stdnt)
        {
            //age
            mean_age += Math.Round((double)(stdnt.m_age - mean_age) / number_of_students, 2);
            //weight
            mean_weight += Math.Round((double)(stdnt.m_weight - mean_weight) / number_of_students, 2);
            //height
            mean_height += Math.Round((double)(stdnt.m_height - mean_height) / number_of_students, 2);
            //grade
            mean_grade += Math.Round((double)(stdnt.m_avg_grade - mean_grade) / number_of_students, 2);
            //exams
            mean_exam += Math.Round((double)(stdnt.m_number_of_exams - mean_exam) / number_of_students, 2);
        }
        private void check_decimal_intervals(ArrayList list, double value)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                int size = (int)(((DecimalInterval)list[i]).m_starting_point - value);
                bool forward = size < 0;
                if (((DecimalInterval)list[i]).m_starting_point <= value && value < ((DecimalInterval)list[i]).m_ending_point)
                {
                    ((DecimalInterval)list[i]).m_count += 1;
                    break;
                }
                else if (i == list.Count - 1 && forward)
                {
                    size = -size / (int)((DecimalInterval)list[i]).m_size;
                    update_intervals(list, size, false, forward, i);
                    i =  i + size;
                    ((DecimalInterval)list[i]).m_count += 1;
                    break;
                }
                else if (!forward)
                {
                    size = size / (int)((DecimalInterval)list[0]).m_size + 1;
                    update_intervals(list, size, false, forward, 0);
                    ((DecimalInterval)list[0]).m_count += 1;
                    break;
                }
            }
        }
        private void check_int_intervals(ArrayList list, int value)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                int size = ((IntegerInterval)list[i]).m_starting_point - value;
                bool forward = size < 0;
                if (((IntegerInterval)list[i]).m_starting_point <= value && value < ((IntegerInterval)list[i]).m_ending_point)
                {
                    ((IntegerInterval)list[i]).m_count += 1;
                    break;
                }
                else if (i == list.Count - 1 && forward)
                {
                    
                    size = -size / ((IntegerInterval)list[i]).m_size ;
                    update_intervals(list, size, true, forward, i);
                    i = i + size;
                    ((IntegerInterval)list[i]).m_count += 1;
                    break;
                }
                else if (!forward)
                {
                    size = size / ((IntegerInterval)list[i]).m_size + 1;
                    update_intervals(list, size, false, forward, 0);
                    ((IntegerInterval)list[0]).m_count += 1;
                    break;
                }
            }
        }
        
        private void update_intervals(ArrayList list, int size, bool integertype, bool forward, int index)
        {
            if (integertype)
            {
                for (int i = index; i < index + size; ++i)
                {
                    if (forward)
                    {
                        list.Add(new IntegerInterval(((IntegerInterval)list[i]).m_size,
                                                ((IntegerInterval)list[i]).m_starting_point + ((IntegerInterval)list[i]).m_size,
                                                ((IntegerInterval)list[i]).m_starting_point + ((IntegerInterval)list[i]).m_size * 2));
                    }
                    else 
                    {
                        list.Insert(0, new IntegerInterval(((IntegerInterval)list[0]).m_size,
                                                ((IntegerInterval)list[0]).m_starting_point - ((IntegerInterval)list[0]).m_size,
                                                ((IntegerInterval)list[0]).m_starting_point));
                    }
                }
            }
            else
            {
                for (int i = index; i < index + size; ++i)
                {
                    if (forward)
                    {
                        list.Add(new DecimalInterval(((DecimalInterval)list[i]).m_size,
                                                ((DecimalInterval)list[i]).m_starting_point + ((DecimalInterval)list[i]).m_size,
                                                ((DecimalInterval)list[i]).m_starting_point + ((DecimalInterval)list[i]).m_size * 2));
                    }
                    else
                    {
                        list.Insert(0, new DecimalInterval(((DecimalInterval)list[0]).m_size,
                                                ((DecimalInterval)list[0]).m_starting_point - ((DecimalInterval)list[0]).m_size,
                                                ((DecimalInterval)list[0]).m_starting_point));
                    }
                }
            }
        }
       
        private void update_distribution(Student stdnt)
        {
            //age age_intervals
            check_int_intervals(age_intervals, stdnt.m_age);
            
            //weight  weight_intervals
            check_decimal_intervals(weight_intervals, stdnt.m_weight);
            
            //height  height_intervals
            check_decimal_intervals(height_intervals, stdnt.m_height);
            
            //grade grades
            if (grades.ContainsKey(stdnt.m_avg_grade))
            {
                grades[stdnt.m_avg_grade] += 1;
            } 
            else
            {
                grades.Add(stdnt.m_avg_grade, 1);
            }

            //exams  number_of_exams
            if (number_of_exams.ContainsKey(stdnt.m_number_of_exams))
            {
                number_of_exams[stdnt.m_number_of_exams] += 1;
            }
            else
            {
                number_of_exams.Add(stdnt.m_number_of_exams, 1);
            }

        }
        private void print_summary()
        {

            //GRADE
            richTextBox1.Text = "SUMMARY" + Environment.NewLine;
            richTextBox1.AppendText("AVerage Grade: " + mean_grade.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Grade Distribution" + Environment.NewLine);
            richTextBox1.AppendText("Grade[#]".PadRight(12)+ ": Count" + Environment.NewLine);
            richTextBox1.AppendText("------------------" + Environment.NewLine);
            foreach (var grade in grades)
            {
                richTextBox1.AppendText(("Grade[" + grade.Key + "]").PadRight(12) + ": " + grade.Value + Environment.NewLine);
            }
            richTextBox1.AppendText("__________________" + Environment.NewLine + Environment.NewLine);

            //EXAMS
            richTextBox1.AppendText("Average Number of Exams: " + mean_exam.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Number of Exams Distribution" + Environment.NewLine);
            richTextBox1.AppendText("Exams[#]".PadRight(12) + ": Count" + Environment.NewLine);
            richTextBox1.AppendText("------------------" + Environment.NewLine);
            foreach (var exams in number_of_exams)
            {
                richTextBox1.AppendText(("Exams[" + exams.Key + "]").PadRight(12) + ": " + exams.Value + Environment.NewLine);
            }
            richTextBox1.AppendText("__________________" + Environment.NewLine + Environment.NewLine);

            //AGE
            richTextBox1.AppendText("Average age: " + mean_age.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Age Distribution" + Environment.NewLine);
            richTextBox1.AppendText("Age[#-#]".PadRight(12) + ": Count" + Environment.NewLine);
            richTextBox1.AppendText("------------------" + Environment.NewLine);
            foreach (var ageint in age_intervals)
            {
                richTextBox1.AppendText(("Age[" + ((IntegerInterval)ageint).m_starting_point + "-" + ((IntegerInterval)ageint).m_ending_point + "]").PadRight(12) + ": " + ((IntegerInterval)ageint).m_count + Environment.NewLine);
            }
            richTextBox1.AppendText("__________________" + Environment.NewLine + Environment.NewLine);
            
            //WEIGHT
            richTextBox1.AppendText("Average weight: " + mean_weight.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Weight Distribution" + Environment.NewLine);
            richTextBox1.AppendText("Weight[#-#]".PadRight(12) + ": Count" + Environment.NewLine);
            richTextBox1.AppendText("------------------" + Environment.NewLine);
            foreach (var weightdec in weight_intervals)
            {
                richTextBox1.AppendText(("Weight[" + ((DecimalInterval)weightdec).m_starting_point + "-" + ((DecimalInterval)weightdec).m_ending_point + "]").PadRight(12) + ": " + ((DecimalInterval)weightdec).m_count + Environment.NewLine);
            }
            richTextBox1.AppendText("__________________" + Environment.NewLine + Environment.NewLine);

            //HEIGHT
            richTextBox1.AppendText("Average height: " + mean_height.ToString() + Environment.NewLine);
            richTextBox1.AppendText("Height Distribution" + Environment.NewLine);
            richTextBox1.AppendText("Height[#-#]".PadRight(12) + ": Count" + Environment.NewLine);
            richTextBox1.AppendText("------------------" + Environment.NewLine);
            foreach (var heightdec in height_intervals)
            {
                richTextBox1.AppendText(("Height[" + ((DecimalInterval)heightdec).m_starting_point + "-" + ((DecimalInterval)heightdec).m_ending_point + "]").PadRight(12) + ": " + ((DecimalInterval)heightdec).m_count + Environment.NewLine);
            }
            richTextBox1.AppendText("__________________" + Environment.NewLine + Environment.NewLine);
            
        }



    }
}
