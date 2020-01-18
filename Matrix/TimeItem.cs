using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Matrix
{
    [Serializable]
    class TimeItem
    {
        private int size;
        private int repeats;
        private double CStime;
        private double CPPtime;
        private double coeff;

        public TimeItem(int size, int repeats, double CStime, double CPPtime)
        {
            this.size = size;
            this.repeats = repeats;
            this.CStime = CStime;
            this.CPPtime = CPPtime;
            this.coeff = CStime / CPPtime;
        }

        public override string ToString()
        {
            return "Matrix with size equal " + Convert.ToString(size) + " for " 
                + Convert.ToString(repeats) + " times calculating is efficienter on cpp in " + Convert.ToString(coeff) + "times.";
        }

        

        
    }
}
