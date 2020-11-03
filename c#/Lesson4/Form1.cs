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
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;


namespace Lesson4
{
    public partial class Form1 : Form
    {
        string file;
        string filename;
        List<Variable> Data;
        List<Type> types = new List<Type> { typeof(Int32), typeof(Double), typeof(String), typeof(Boolean), typeof(DateTime) };

        public Form1()
        {
            InitializeComponent();
            this.openFileDialog1.Multiselect = false;
            this.openFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            //this.listView1.LabelEdit = true;
            //this.listView1.AllowColumnReorder = true;
            //this.listView1.CheckBoxes = true;
            //this.listView1.FullRowSelect = true;
            //this.listView1.MultiSelect = true;
            //this.listView1.HideSelection = false;
            //this.listView1.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Type t in types)
            {
                this.comboBox1.Items.Add(t.ToString());
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Please Select a File(s)";
            
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            file = openFileDialog1.FileName;
            filename = openFileDialog1.SafeFileName;
            richTextBox1.Text = file;
            Data = new List<Variable>();
            open_CSV_file(file);
            try
            {
                set_default_types();
            }
            catch (Exception exception)
            {
                richTextBox3.Text = "> Error!!!" + Environment.NewLine;
                richTextBox3.AppendText("> Occured during reading the file! " + Environment.NewLine);
                richTextBox3.AppendText("> Possible causes: " + Environment.NewLine);
                richTextBox3.AppendText(">  1) There might be a header (converting string to other data type (int, double etc.)." + Environment.NewLine);
                richTextBox3.AppendText(">      1.1) Please uncheck 'No header' and try opening file again. Skipping this step might cause crash of the application!" + Environment.NewLine);
                richTextBox3.AppendText(">  2) Invalid data types." + Environment.NewLine);
                richTextBox3.AppendText("> Message: " + exception.Message + Environment.NewLine);
            }
            print_Listview();
            print_Treeview();


        }
        private void open_CSV_file(string path)
        {
            treeView1.Nodes.Clear();
            listView1.Clear();
            Data.Clear();
            richTextBox3.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            using (TextFieldParser csvReader = new TextFieldParser(path)) //TextFieldParser(path,System.Text.Encoding.Default)
            {
                
                string delimiter = "";
 

                if (radioButton1.Checked) delimiter = ","; 
                else if(radioButton2.Checked) delimiter = ";";

                //csvReader.TextFieldType = FieldType.Delimited;
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { delimiter });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Row with the column names
                string[] fields = csvReader.ReadFields();
                int number_of_columns = fields.Length;


                bool auto_header = check_header(fields);
                bool manual_header = radioButton4.Checked;
                bool header = (!manual_header && auto_header) || (manual_header && !checkBox1.Checked);


                if (header) 
                {
                    foreach (string column in fields)
                    {
                        Variable v = new Variable(column);
                        Data.Add(v);
                    }
                    fields = csvReader.ReadFields();
                }
                else
                {
                    for (int i = 0; i < number_of_columns; ++i)
                    {
                        string column = "Variable#" + i;
                        Variable v = new Variable(column);
                        Data.Add(v);
                    }

                }

                do
                {
                    for (int i = 0; i < number_of_columns; ++i)
                    {
                        Data[i].Add(fields[i]);
                    }
                    fields = csvReader.ReadFields();
                } while (!csvReader.EndOfData);

            }

        }
        private void print_Listview()
        {
            int padding = 30;
            int list_width = listView1.Width;
            int column_width = padding + list_width / Data.Count;
            int number_of_rows = Data[0].m_values.Count;
            int number_of_columns = Data.Count;
            listView1.BeginUpdate();
            for (int i = 0; i < number_of_columns; ++i)
            {
                listView1.Columns.Add(Data[i].m_name, column_width);
                comboBox2.Items.Add(Data[i].m_name);
                comboBox3.Items.Add(Data[i].m_name);
            }

            for (int i = 0; i < number_of_rows; ++i)
            {              
                var row_item = new ListViewItem(Data[0].get_string_value(i));
                for (int j = 1; j < number_of_columns; ++j)
                {
                    row_item.SubItems.Add(Data[j].get_string_value(i));
                }
                listView1.Items.Add(row_item);
            }
            listView1.EndUpdate();
        }
        private void update_Listview(int index)
        {
            int number_of_rows = Data[0].m_values.Count;
            listView1.BeginUpdate();
            for (int i = 0; i < number_of_rows; ++i)
            {
               if(index == 0) 
                    listView1.Items[i].Text = Data[index].get_string_value(i);
               else 
                    listView1.Items[i].SubItems[index].Text = Data[index].get_string_value(i);
            }
            listView1.EndUpdate();
        }
        private void print_Treeview()
        {
            TreeNode file_node = new TreeNode(filename);
            treeView1.Nodes.Add(file_node);
            
            int number_of_rows = Data[0].m_values.Count;
            int number_of_columns = Data.Count;
            
            treeView1.BeginUpdate();
            for (int i = 0; i < number_of_columns; ++i)
            {
                TreeNode v = new TreeNode(Data[i].m_name);
                treeView1.Nodes[0].Nodes.Add(v);

                for (int j = 0; j < number_of_rows; ++j)
                {
                    TreeNode node = new TreeNode(Data[i].get_string_value(j));
                    treeView1.Nodes[0].Nodes[i].Nodes.Add(node);
                }
            }
            treeView1.Nodes[0].Expand();
            treeView1.EndUpdate();     
        }
        private void update_Treeview(int index)
        {
            int number_of_rows = Data[0].m_values.Count;
            treeView1.BeginUpdate();
            for (int i = 0; i < number_of_rows; ++i)
            {
                treeView1.Nodes[0].Nodes[index].Nodes[i].Text = Data[index].get_string_value(i);
            }
            treeView1.Nodes[0].Expand();
            treeView1.EndUpdate();
        }

        private bool check_header(string[] values)
        {
            bool bool_value;
            int int_value;
            double double_value;
            DateTime date_value;

            foreach (string value in values)
            {
                if (bool.TryParse(value, out bool_value))
                    return false;
                else if (Int32.TryParse(value, out int_value))
                    return false;
                else if (double.TryParse(value, out double_value))
                    return false;
                else if (DateTime.TryParse(value, out date_value))
                    return false;
            }
            return true;
        }
        private void set_default_types()
        {
            int number_of_columns = Data.Count;
            for (int i = 0; i < number_of_columns; ++i)
            {
                Data[i].check_types();
            }

            
        }
        private void set_types(int index, Type type)
        {
            Data[index].set_type(type);
        }
        private void restore(int index)
        {
            Data[index].restore_type();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected_item = treeView1.SelectedNode;

            if (selected_item != null && selected_item.Text != filename && selected_item.Level == 1)
            {
                int index = selected_item.Index;

                textBox1.Text = selected_item.Text;
                textBox2.Text = Data[index].get_default_type().ToString();
                textBox3.Text = Data[index].get_type().ToString();

               
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            TreeNode selected_item = treeView1.SelectedNode;
            if (selected_item != null && selected_item.Text != filename && selected_item.Level == 1)
            {
                int index = selected_item.Index;
                Type type = types[comboBox1.SelectedIndex];

                if (type != Data[index].get_type())
                {
                    try
                    {
                        set_types(index, type);
                    }
                    catch (Exception exception)
                    {
                        richTextBox3.Text = "> Error!!!" + Environment.NewLine;
                        richTextBox3.AppendText("> Cannot convert to " + comboBox1.SelectedItem.ToString() + Environment.NewLine);
                        richTextBox3.AppendText("> Message: " + exception.Message + Environment.NewLine);
                        restore(index);
                    }

                    textBox3.Text = Data[index].get_type().ToString();
                    update_Listview(index);
                    update_Treeview(index);
                }             
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TreeNode selected_item = treeView1.SelectedNode;
            if (selected_item != null && selected_item.Text != filename && selected_item.Level == 1)
            {
                int index = selected_item.Index;            
                restore(index);
                textBox3.Text = Data[index].get_type().ToString();
                update_Listview(index);
                update_Treeview(index);              
            }
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true) checkBox1.Enabled = true;
            else checkBox1.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {             
            try
            {
                int first_index = comboBox2.SelectedIndex;
                int second_index = comboBox3.SelectedIndex;
                Plot m_plot = new Plot();
                m_plot.load_values(Data[first_index], Data[second_index]);
                m_plot.Show();
            }
            catch (Exception exception)
            {
                richTextBox3.Text = "> Error!!!" + Environment.NewLine;
                richTextBox3.AppendText("> Occured during invoking drawing! " + Environment.NewLine);
                richTextBox3.AppendText("> Cannot invoke drawing process!" + Environment.NewLine);
                richTextBox3.AppendText("> Possible causes: " + Environment.NewLine);
                richTextBox3.AppendText(">  1) Select variables to draw;" + Environment.NewLine);
                richTextBox3.AppendText(">  2) Variables selected are non-numeric." + Environment.NewLine);
                richTextBox3.AppendText("> Message: " + exception.Message + Environment.NewLine);
            }
        }
    }
}
