using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeightedRandom
{
    [Serializable]
    public class WeightedRandomException : Exception 
    {
        public WeightedRandomException()
        {
        }

        public WeightedRandomException(string message) 
            : base(message)
        {
        }

        public WeightedRandomException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}