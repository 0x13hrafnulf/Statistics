using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson10_1_1
{

    public enum Variable_Type
    { 
        Integer,
        Double,
        String,
        Boolean,
        DateTime

    }
    public class Variable
    {
        public string m_name;
        Type m_type;
        Type m_default_type;
        public bool update = false;

        public List<Value> m_values; 



        public Variable(string name)
        {
            m_name = name;
            m_values = new List<Value>();
        }
        public void Add(string value)
        {
            Value val = new Value(value);
            m_values.Add(val);       
        }
        public void Add(double value)
        {
            Value val = new Value(value);
            m_values.Add(val);
        }
        public void Add(int value)
        {
            Value val = new Value(value);
            m_values.Add(val);
        }
        public dynamic get(int i)
        {
            return m_values[i].get_value();
        }
        public string get_string_value(int i)
        {
            if (m_values[i].m_value == null) return "";
            
            return m_values[i].m_value.ToString();
        }

        public void update_values()
        {        
            foreach (Value val in m_values)
            {
                val.set_type(m_type);
            }

        }
        public Type get_type()
        {
            return m_type;
        }
        public void set_default_type(Type type)
        {
            m_default_type = type;
            m_type = type;

            foreach (Value val in m_values)
            {
                val.set_default_type(type);
            }
        }
        public Type get_default_type()
        {
            return m_default_type;
        }

        public bool Equals(Variable another_variable)
        {
            return this.m_name == another_variable.m_name;
        }

        public override int GetHashCode()
        {
            return this.m_name.GetHashCode();
        }
        public void check_types()
        {
            bool bool_value;
            int int_value;
            double double_value;
            DateTime date_value;


            if (bool.TryParse(m_values[0].m_value, out bool_value))
                set_default_type(typeof(Boolean));
            else if (Int32.TryParse(m_values[0].m_value, out int_value))
            {
                bool check_double = false;
                foreach (Value val in m_values)
                {
                    check_double = val.check_type() == typeof(Double);
                    if (check_double) break;
                }
                if(check_double) set_default_type(typeof(Double));
                else set_default_type(typeof(Int32));
            }           
            else if (double.TryParse(m_values[0].m_value, out double_value))
                set_default_type(typeof(Double));
            else if (DateTime.TryParse(m_values[0].m_value, out date_value))
                set_default_type(typeof(DateTime));
            else
            {
                bool check_double = false;
                bool check_int = false;
                foreach (Value val in m_values)
                {
                    check_double = val.check_type() == typeof(Double);
                    check_int = val.check_type() == typeof(Int32);
                    if (check_double) break;
                }
                if (check_double) set_default_type(typeof(Double));
                else if(check_int) set_default_type(typeof(Int32));
                else set_default_type(typeof(String));
            }
               

        }
        public void set_type(Type type)
        {
            m_type = type;
            update_values();
        }
        public void restore_type()
        {
            m_type = m_default_type;
            foreach (Value val in m_values)
            {
                val.restore_value();
            }
        }

    }
}
