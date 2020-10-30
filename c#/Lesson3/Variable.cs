using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{

    enum Variable_Type
    { 
        Integer,
        Double,
        String,
        Boolean,
        DateTime

    }
    class Variable
    {
        public string m_name;
        Variable_Type m_type;
        Variable_Type m_default_type;
        public bool update = false;

        public List<Value> values; 

        public Variable(string name)
        {
            m_name = name;
            values = new List<Value>();
        }
        public void Add(string value)
        {
            Value val = new Value(value);
            values.Add(val);       
        }
        public dynamic get(int i)
        {
            return values[i].get_value();
        }
        public string get_string_value(int i)
        {
            if (values[i].m_value == null) return "";
            
            return values[i].m_value.ToString();
        }

        public void update_values()
        {        
            foreach (Value val in values)
            {
                val.set_type(m_type);
            }

        }
        public Variable_Type get_type()
        {
            return m_type;
        }
        public void set_default_type(Variable_Type type)
        {
            m_default_type = type;
            m_type = type;

            foreach (Value val in values)
            {
                val.set_default_type(type);
            }
        }
        public Variable_Type get_default_type()
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


            if (bool.TryParse(values[0].m_value, out bool_value))
                set_default_type(Variable_Type.Boolean);
            else if (Int32.TryParse(values[0].m_value, out int_value))
            {
                bool check_double = false;
                foreach (Value val in values)
                {
                    check_double = val.check_type() == Variable_Type.Double;
                    if (check_double) break;
                }
                if(check_double) set_default_type(Variable_Type.Double);
                else set_default_type(Variable_Type.Integer);
            }           
            else if (double.TryParse(values[0].m_value, out double_value))
                set_default_type(Variable_Type.Double);
            else if (DateTime.TryParse(values[0].m_value, out date_value))
                set_default_type(Variable_Type.DateTime);
            else
            {
                bool check_double = false;
                bool check_int = false;
                foreach (Value val in values)
                {
                    check_double = val.check_type() == Variable_Type.Double;
                    check_int = val.check_type() == Variable_Type.Integer;
                    if (check_double) break;
                }
                if (check_double) set_default_type(Variable_Type.Double);
                else if(check_int) set_default_type(Variable_Type.Integer);
                else set_default_type(Variable_Type.String);
            }
               

        }
        public void set_type(Variable_Type type)
        {
            m_type = type;
            update_values();
        }
        public void restore_type()
        {
            m_type = m_default_type;
            foreach (Value val in values)
            {
                val.restore_value();
            }
        }
    }
}
