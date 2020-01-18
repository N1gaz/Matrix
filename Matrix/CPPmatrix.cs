using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Matrix
{
    class CPPmatrix
    {
        [DllImport("MyDll.dll")]
        public static extern double HowLong(int size, int repeats);

        [DllImport("MyDll.dll")]
        private static extern IntPtr Answer([MarshalAs(UnmanagedType.LPArray), In, Out]double[] raw,
        [MarshalAs(UnmanagedType.LPArray), In, Out] double[] col,
        [MarshalAs(UnmanagedType.LPArray), In, Out] double[] right, int size);

        public static double[] Equation(double[] raw, double[] col, double[] right)
        {
            double[] result = new double[raw.Length];

            IntPtr pointer = Answer(raw, col, right, raw.Length);

            Marshal.Copy(pointer, result, 0, raw.Length);

            return result;
        }
    }
}
