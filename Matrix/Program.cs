using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Matrix
{
    class Program
    {
        static void menu()
        {
            Console.WriteLine("\n1. Check solve.");
            Console.WriteLine("2. Check performance");
            Console.WriteLine("3. Load or Save in file");
            Console.WriteLine("4. Exit");
        }
        static void Main(string[] args)
        {
            double[] raw = { 1, 3, 5, 7 };
            double[] col = { 1, 0, -8 ,10 };
            double[] right = { 1, 2, 3, 4 };
            TimeItem buff;
            TimeList list = new TimeList();
            Matrix A = new Matrix(col, raw);
            Matrix B;
            Random rnd = new Random();

            int switchOn = 0;

            do
            {
                menu();
                Console.WriteLine("Enter number:");
                switchOn = Convert.ToInt32(Console.ReadLine());

                switch (switchOn)
                {
                    case 1:
                        try
                        {
                            double[] result = CPPmatrix.Equation(raw, col, right);


                            Console.WriteLine("cpp:");
                            foreach (double i in result)
                            {
                                Console.WriteLine(Convert.ToString(i));
                            }

                            result = A.Equation(right);
                            Console.WriteLine("\n\ncs:");
                            foreach (double i in result)
                            {
                                Console.WriteLine(Convert.ToString(i));
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Enter size of matrix and count of repeats through the scpace");
                            string SRBuff = Console.ReadLine();

                            char[] separator = { ' ' };

                            string[] SRmas = SRBuff.Split(separator);

                            int size = Convert.ToInt32(SRmas[0]);
                            int repeats = Convert.ToInt32(SRmas[1]);
                            double CSTime = 0, CPPTime = 0;

                            Console.WriteLine("Time for CPP:");
                            CPPTime = CPPmatrix.HowLong(size, repeats);
                            Console.WriteLine(CPPTime);
                            Console.WriteLine("Time for CS:");

                            B = new Matrix(size);
                            Stopwatch sw = new Stopwatch();

                            double[] res = new double[size];
                            double[] rgh = new double[size];

                            for (int i = 0; i < size; i++)
                            {
                                rgh[i] = (double)(rnd.Next() + rnd.NextDouble());
                            }

                            sw.Start();
                            for (int i = 0; i < repeats; i++)
                            {
                                res = B.Equation(rgh);
                            }
                            sw.Stop();

                            CSTime = sw.ElapsedMilliseconds;

                            Console.WriteLine(CSTime);

                            buff = new TimeItem(size, repeats, CSTime, CPPTime);
                            list.Add(buff);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }


                        
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Enter filename:");
                            string filename = Console.ReadLine();

                            if (System.IO.File.Exists(filename))
                            {
                                list.Load(filename);
                            }
                            else
                            {
                                list.Save(filename);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 4:;
                        break;
                    default:
                        Console.WriteLine("Error. Try again.");
                        break;
                }

            }
            while (switchOn != 4);

            Console.WriteLine(list.ToString());
        }  
    }
}
