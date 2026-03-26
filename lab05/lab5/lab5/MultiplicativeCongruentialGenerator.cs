using System;
using System.Collections.Generic;

namespace lab5
{
    public class MultiplicativeCongruentialGenerator
    {
        private ulong x;
        private const ulong M = 9223372036854775808UL;   
        private const ulong Beta = 4294967299UL;          

        public MultiplicativeCongruentialGenerator(ulong seed = 4294967299)
        {
            x = seed;
        }

        public double NextDouble()
        {
            x = (Beta * x) % M;
            return (double)x / M;
        }
    }
}
    
