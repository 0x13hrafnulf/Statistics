using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson1
{
    public partial class Lesson1 : Form
    {
        public Lesson1()
        {
            InitializeComponent();
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "This is Lesson1, Task 1_A:a";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.LightBlue;
        }

        private void richTextBox1_MouseLeave(object sender, EventArgs e)
        {
            this.richTextBox1.BackColor = Color.White;
        }
        private void richTextBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void richTextBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            int i;
            String s;

            // Get start position to drop the text.  
            i = richTextBox1.SelectionStart;
            s = richTextBox1.Text.Substring(i);
            richTextBox1.Text = richTextBox1.Text.Substring(0, i);

            // Drop the text on to the RichTextBox.  
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string filePath in files)
            {
                richTextBox1.Text += filePath;
            }    
            richTextBox1.Text = richTextBox1.Text + s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Student student1 = new Student(19, "John", 1);
            Student student2 = new Student(23, "Stevy", 2);
            Student student3 = new Student(27, "Stewart", 3);
            this.richTextBox1.Text = "*******REFERENCE TYPE*******" + Environment.NewLine;
            this.richTextBox1.Text += "> Initial values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Student1: name = " + student1.m_name + "; ID = "+ student1.m_ID + "; Age = " + student1.m_age + ";" +  Environment.NewLine;
            this.richTextBox1.Text += "> Student2: name = " + student2.m_name + "; ID = " + student2.m_ID + "; Age = " + student2.m_age + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Student3: name = " + student3.m_name + "; ID = " + student3.m_ID + "; Age = " + student3.m_age + ";" + Environment.NewLine;

            student1 = student3;
            this.richTextBox1.Text += Environment.NewLine + "> Update" + Environment.NewLine;
            this.richTextBox1.Text += "> Student1 = Student3" + Environment.NewLine;
            this.richTextBox1.Text += "> Updated values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Student1: name = " + student1.m_name + "; ID = " + student1.m_ID + "; Age = " + student1.m_age + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Student2: name = " + student2.m_name + "; ID = " + student2.m_ID + "; Age = " + student2.m_age + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Student3: name = " + student3.m_name + "; ID = " + student3.m_ID + "; Age = " + student3.m_age + ";" + Environment.NewLine;

            student1.m_age = 20;
            this.richTextBox1.Text += Environment.NewLine + "> Update" + Environment.NewLine;
            this.richTextBox1.Text += "> Student1.m_age = 20" + Environment.NewLine;
            this.richTextBox1.Text += "> Updated values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Student1: name = " + student1.m_name + "; ID = " + student1.m_ID + "; Age = " + student1.m_age + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Student2: name = " + student2.m_name + "; ID = " + student2.m_ID + "; Age = " + student2.m_age + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Student3: name = " + student3.m_name + "; ID = " + student3.m_ID + "; Age = " + student3.m_age + ";" + Environment.NewLine;

            //**************************

            Point point1 = new Point(15, 20);
            Point point2 = new Point(10, 13);
            Point point3 = new Point(5, 7);

            this.richTextBox1.Text += Environment.NewLine +  "*********VALUE TYPE*********" + Environment.NewLine;
            this.richTextBox1.Text += "> Initial values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Point1: x = " + point1.m_x + "; y = " + point1.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point2: x = " + point2.m_x + "; y = " + point2.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3: x = " + point3.m_x + "; y = " + point3.m_y + ";" + Environment.NewLine;

            point3 = point1;
            this.richTextBox1.Text += Environment.NewLine + "> Update" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3 = Point1" + Environment.NewLine;
            this.richTextBox1.Text += "> Updated values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Point1: x = " + point1.m_x + "; y = " + point1.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point2: x = " + point2.m_x + "; y = " + point2.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3: x = " + point3.m_x + "; y = " + point3.m_y + ";" + Environment.NewLine;

            point3.m_x = 20;
            point3.m_y = 1;
            this.richTextBox1.Text += Environment.NewLine + "> Update" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3.m_x = 20" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3.m_y = 1" + Environment.NewLine;
            this.richTextBox1.Text += "> Updated values: " + Environment.NewLine;
            this.richTextBox1.Text += "> Point1: x = " + point1.m_x + "; y = " + point1.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point2: x = " + point2.m_x + "; y = " + point2.m_y + ";" + Environment.NewLine;
            this.richTextBox1.Text += "> Point3: x = " + point3.m_x + "; y = " + point3.m_y + ";" + Environment.NewLine;


        }
    }

    public class Student
    {
        public Student(int age, string name, uint id)
        {
            m_age = age;
            m_name = name;
            m_ID = id;

        }
        public int m_age;
        public string m_name;
        public uint m_ID;
    }
    struct Point
    {
        public Point(int x, int y)
        {
            m_x = x;
            m_y = y;

        }
        public int m_x;
        public int m_y;
    }

}
