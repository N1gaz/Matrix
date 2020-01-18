using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace Matrix
{
    class Matrix
    {
        private double[] col;
        private double[] raw;

        public Matrix(int n)
        {
            col = new double[n];
            raw = new double[n];

            Random rnd = new Random();

            col[0] = (double)rnd.NextDouble() + rnd.Next();
            while (col[0] == 0)
            {
                col[0] = (double)rnd.NextDouble() + rnd.Next();
            }
            raw[0] = col[0];

            for (int i = 1; i < n; i++)
            {
                col[i] = (double)rnd.NextDouble() + rnd.Next();
                raw[i] = (double)rnd.NextDouble() + rnd.Next();
            }
        }

        public Matrix(double[] col, double[] raw)
        {
            if(col[0] == 0)
            {
                throw new ArgumentException("На главной диагонали теплицевой матрицы должны стоять ненулевые элементы");
            }

            if (col.Length != raw.Length)
            {
                throw new ArgumentException("Размеры строки и столбца должны быть равны");
            }

            if (col[0] != raw[0])
            {
                throw new ArgumentException("Первые элементы строки и столбца должны быть равны между собой");
            }

            this.col = col;
            this.raw = raw;
        }

        public double[] Equation(double[] right)
        {
            int size = col.Length;

            double[] result = new double[size];

            double[] X = new double[1];
            double[] Y = new double[1];

            double[] Xbuff;
            double[] Ybuff;

            double zero = 1 / col[0];

            

            double F, G, r, s, t;

            X[0] = zero;
            Y[0] = zero;

            int k = 1;

            while (k != size)
            {
                F = 0;
                G = 0;

                for (int i = 0; i < k; i++)
                {
                    F += col[k - i] * X[i];
                    G += raw[i + 1] * Y[i];
                }

                r = 1 / (1 - G * F);
                s = - r * F;
                t =  - r * G;

                Xbuff = new double[k + 1];
                Ybuff = new double[k + 1];

                Xbuff[0] = X[0] * r;
                Ybuff[0] = X[0] * t;

                for (int i = 1; i < k; i++)
                {
                    Xbuff[i] = X[i] * r + Y[i - 1] * s;
                    Ybuff[i] = X[i] * t + Y[i - 1] * r;
                }

                Xbuff[k] = Y[k - 1] * s;
                Ybuff[k] = Y[k - 1] * r;



                X = (double[])Xbuff.Clone();
                Y = (double[])Ybuff.Clone();
                k++;

            }

            double[,] inv = new double[size, size];
            double[,] B1 = new double[size, size];
            double[,] B2 = new double[size, size];
            double[,] B3 = new double[size, size];
            double[,] B4 = new double[size, size];

            bool check;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    check = true;

                    if (i + j < size)
                    {
                        B1[i + j, j] = X[i];
                        B2[i, i + j] = Y[size - 1 - j];
                        check = false;
                    }

                    if (i + j + 1 < size)
                    {
                        check = false;
                        B3[i + 1 + j,j] = Y[i];
                        B4[i, i + 1 + j] = X[size - 1 - j];
                    }

                    if (check)
                    {
                        break;
                    }

                }

            }

            double buff12 = 0, buff34 = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    buff12 = 0;
                    buff34 = 0;
                    for (int l = 0; l < size; l++)
                    {
                        buff12 += B1[i, l] * B2[l, j];
                        buff34 += B3[i, l] * B4[l, j];
                    }
                    inv[i, j] = 1 / X[0] * (buff12 - buff34);
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int l = 0; l < size; l++)
                {
                    result[i] += inv[i, l] * right[l];
                }
            }

            return result;
        }



        

    }
}
