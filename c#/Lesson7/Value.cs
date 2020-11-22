using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson7
{
    public class Value
    {
       
        public string m_generic_value;
        Type m_type;
        Type m_default_type;
        public dynamic m_value = 0;

        public Value(string value)
        {        
            m_value = value == "" ? null : value;
            m_generic_value = value == "" ? null : value;
        }
        public Value(double value)
        {
            m_value = value;
            m_generic_value = value.ToString();
        }
        public Value(int value)
        {
            m_value = value;
            m_generic_value = value.ToString();
        }
        public void set_type(Type type)
        {
            m_type = type;
            set_value();

        }
        public Type get_type()
        {
            return m_type;
        }
        public void set_default_type(Type type)
        {
            m_default_type = type;
            m_type = type;
            set_value();
        }
        public Type get_default_type()
        {
            return m_default_type;
        }
        public dynamic get_value()
        {
            return m_value;
        }
        public void set_value()
        {

            if (m_type == typeof(Int32))
            {
                m_value = Convert.ToInt32(m_value);
            }
            else if(m_type == typeof(Double))
            {
                m_value = Convert.ToDouble(m_value);
            }
            else if (m_type == typeof(String))
            {
                m_value = Convert.ToString(m_value);
            }
            else if (m_type == typeof(Boolean))
            {
                m_value = Convert.ToBoolean(m_value);
            }
            else if (m_type == typeof(DateTime))
            {
                m_value = Convert.ToDateTime(m_value);
            }


        }
        public void restore_value()
        {


            m_type = m_default_type;

            if (m_type == typeof(Int32))
            {
                m_value = Convert.ToInt32(m_generic_value);
            }
            else if (m_type == typeof(Double))
            {
                m_value = Convert.ToDouble(m_generic_value);
            }
            else if (m_type == typeof(String))
            {
                m_value = Convert.ToString(m_generic_value);
            }
            else if (m_type == typeof(Boolean))
            {
                m_value = Convert.ToBoolean(m_generic_value);
            }
            else if (m_type == typeof(DateTime))
            {
                m_value = Convert.ToDateTime(m_generic_value);
            }

        }
        public Type check_type()
        {
            bool bool_value;
            int int_value;
            double double_value;
            DateTime date_value;

       
            if (bool.TryParse(m_value, out bool_value))
                return typeof(Boolean);
            else if (Int32.TryParse(m_value, out int_value))
                return typeof(Int32);
            else if (double.TryParse(m_value, out double_value))
                return typeof(Double);
            else if (DateTime.TryParse(m_value, out date_value))
                return typeof(DateTime);
            return typeof(String);
        }
    }
}
