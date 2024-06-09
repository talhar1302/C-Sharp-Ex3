using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; }
        public float MinValue { get; }

        public ValueOutOfRangeException(float minValue, float maxValue, string message) : base(message)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
