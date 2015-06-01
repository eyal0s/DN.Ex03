using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float m_MaxValue;
        public float m_MinValue;

        public ValueOutOfRangeException()
    {
    }

        public ValueOutOfRangeException(string i_message) : base(i_message)
        {
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Illeagal value was entered. Min value is {0}. Max value is {1}. Please stay within bounds.", i_MinValue, i_MaxValue))
    {
        m_MaxValue = i_MaxValue;
        m_MinValue = i_MinValue;
    }

    public ValueOutOfRangeException(string i_message, float i_MinValue, float i_MaxValue)
        : base(i_message)
    {
        m_MaxValue = i_MaxValue;
        m_MinValue = i_MinValue;
    }

    public ValueOutOfRangeException(string i_message, Exception i_inner)
        : base(i_message, i_inner)
    {
    }
    }
}