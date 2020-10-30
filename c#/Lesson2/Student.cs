using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    class Student
    {
        public int m_age;
        public uint m_ID;
        public double m_weight;
        public double m_height;
        public int m_avg_grade;
        public int m_number_of_exams;
        public string m_name;

        public Student(int age, double weight, double height, int avg_grade, int n_exams)
        {
            m_age = age;
            m_weight = weight;
            m_height = height;
            m_avg_grade = avg_grade;
            m_number_of_exams = n_exams;
            //m_name = name;
        }

    }
}
