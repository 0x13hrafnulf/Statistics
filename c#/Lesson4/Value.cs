using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3
{
    class Value
    {
       
        public string m_generic_value;
        Variable_Type m_type;
        Variable_Type m_default_type;
        public dynamic m_value = 0;

        public Value(string value)
        {        
            m_value = value == "" ? null : value;
            m_generic_value = value == "" ? null : value;
        }

        public void set_type(Variable_Type type)
        {
            m_type = type;
            set_value();

        }
        public Variable_Type get_type()
        {
            return m_type;
        }
        public void set_default_type(Variable_Type type)
        {
            m_default_type = type;
            m_type = type;
            set_value();
        }
        public Variable_Type get_default_type()
        {
            return m_default_type;
        }
        public dynamic get_value()
        {
            return m_value;
        }
        public void set_value()
        {

            switch (m_type)
            {
                case Variable_Type.Integer:
                    m_value = Convert.ToInt32(m_value);
                    break;
                case Variable_Type.Double:
                    m_value = Convert.ToDouble(m_value);
                    break;
                case Variable_Type.String:
                    m_value = Convert.ToString(m_value);
                    break;
                case Variable_Type.Boolean:
                    m_value = Convert.ToBoolean(m_value);
                    break;
                case Variable_Type.DateTime:
                    m_value = Convert.ToDateTime(m_value);
                    break;
            }

        }
        public void restore_value()
        {
            m_type = m_default_type;
            switch (m_default_type)
            {
                case Variable_Type.Integer:
                    m_value = Convert.ToInt32(m_generic_value);
                    break;
                case Variable_Type.Double:
                    m_value = Convert.ToDouble(m_generic_value);
                    break;
                case Variable_Type.String:
                    m_value = Convert.ToString(m_generic_value);
                    break;
                case Variable_Type.Boolean:
                    m_value = Convert.ToBoolean(m_generic_value);
                    break;
                case Variable_Type.DateTime:
                    m_value = Convert.ToDateTime(m_generic_value);
                    break;
            }
        }
        public Variable_Type check_type()
        {
            bool bool_value;
            int int_value;
            double double_value;
            DateTime date_value;

       
            if (bool.TryParse(m_value, out bool_value))
                return Variable_Type.Boolean;
            else if (Int32.TryParse(m_value, out int_value))
                return Variable_Type.Integer;
            else if (double.TryParse(m_value, out double_value))
                return Variable_Type.Double;
            else if (DateTime.TryParse(m_value, out date_value))
                return Variable_Type.DateTime;
            return Variable_Type.String;
        }


            /*
            public T get_value<T>()
            {

                return (T)Convert.ChangeType(m_value, typeof(T));
            }
            */
    }
}
