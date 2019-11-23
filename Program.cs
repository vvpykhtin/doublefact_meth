using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Vladyslav Pykhtin
// +38(050)6422555
// https://github.com/vvpykhtin
namespace kurs
{
    class Program
    {
        public void FileRead(double[,] Om, double[] Amp, int n)
        {
            string[] res = File.ReadAllLines("R.txt");
            Console.WriteLine("resistances matrix:");
            for (int i = 0; i < n; i++)
            {
                string[] r = res[i].Split(',');
                for (int j = 0; j < n; j++)
                {
                    Om[i, j] = Convert.ToDouble(r[j]);
                }
            }
            for (int a = 0; a < n; a++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("\t{0}", Om[a, j]);
                Console.WriteLine();
            }

            Console.WriteLine("currents matrix:");
            string[] arr = File.ReadAllLines("I.txt");
            for (int j = 0; j < n; j++)
            {
                Amp[j] = Convert.ToDouble(arr[j]);
            }
            for (int j = 0; j < n; j++)
            {
                Console.WriteLine("\t{0}",Amp[j]);
            }

        }
        public void Matrix(double[,] Matrix, double[,] r, int n)
        {

            Console.WriteLine("Matrix :");
            for (int  i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (r[i, j] == 0)
                    {
                        r[i, j] *= 1;
                    }
                    else
                    {
                        Matrix[i, j] = 1 / r[i, j];
                    }
                    double summ = 0;
                    for (int k = 0; k < n; k++)
                    {
                        if (r[i, k] != 0)
                            summ += 1 / r[i, k];
                    }
                    Matrix[i, i] = -1 * (summ);
                    Console.Write("\t {0:F3}", Matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
       

        static void Main(string[] args)
        {
            Console.Write("Please, enter size of matrix(n):");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            double[,] resistance = new double[n, n];
            double[,] cond = new double[n, n];
            double[] current = new double[n];
            Console.Write("Press 1 if you want to enter values or press 2 to read file :");
            string ch = Console.ReadLine();
            if (ch == "2")
            {
                Program Network = new Program();
          

                Network.FileRead(cond, current, n);
                Network.Matrix(resistance, cond,n);
                //Console.ReadKey();
            }
            Console.WriteLine();
            if (ch == "1") {
                Program Network = new Program();

                for (int k = 0; k < n; k++)
                {
                    Console.Write("Please, enter {0} line(with whitespaces) :", (k + 1));
                    string a = Console.ReadLine();
                    Console.WriteLine();
                    string[] r = a.Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        resistance[k, j] = Convert.ToDouble(r[j]);
                    }
                    Console.WriteLine();
                }
                Console.Write("please, enter currents(with whitespaces):");
                string q = Console.ReadLine();
                Console.WriteLine();
                string[] qq = q.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    current[j] = Convert.ToDouble(qq[j]);
                }
                Network.Matrix(cond, resistance,n);
                Console.WriteLine();
                Console.WriteLine("resistances matrix:");
                for (int a = 0; a < n; a++)
                {
                    for (int j = 0; j < n; j++)
                        Console.Write("\t{0}", resistance[a, j]);
                    Console.WriteLine();
                }
                Console.WriteLine("currents matrix:");
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine("\t{0}", current[j]);
                }
            }
            Console.WriteLine("Calculation of a system " +
                "of linear equations that describe" +
                " the operation mode of an electric circuit with " +
                "Double factorization method :");
         
            for (int k = 0; k < n; k++)
            {
                Console.WriteLine("{0} step(supporting element k{0}{0} = {1:F3})", k + 1, resistance[k,k]);
                Console.WriteLine();
                resistance[k, k] = 1 / resistance[k, k];
                Console.WriteLine("y{0}{0}({0}) = 1 / y{0}{0} = {1:F3}", k + 1,resistance[k, k]);
                for (int i = k+1; i < n; i++)
                {

                    resistance[i, k] *= -resistance[k, k];
                    Console.WriteLine("y{0}{1}({1}) = y{0}{1} * -(y{1}{1}) = {2:F3}", i+1, k+1, resistance[i, k]);
                    for (int j = k + 1; j < n; j++)
                    {
                        resistance[i, j] += resistance[i, k] * resistance[k, j];
                        Console.WriteLine("y{0}{1}({2}) = y{0}{1} + y{0}{2} * y{2}{1} = {3:F3}", i + 1,j+1, k + 1, resistance[i, j]);
                    }
                }
                for (int i = k + 1; i < n; i++)
                {
                    resistance[k, i] *= -resistance[k, k];
                    Console.WriteLine("y{0}{1}({0}) = y{0}{1} * -(y{0}{0}) = {2:F3}",  k + 1, i+1, resistance[k, i]);
                }
                Console.WriteLine();
                for (int z = 0; z < n; z++)
                {
                    for (int c = 0; c < n; c++)
                        Console.Write("\t {0:F3}", resistance[z, c]);
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();

            }
            Console.WriteLine("Now we solve a system of equations.We separate and form factor matrices:");
            Console.WriteLine();
            double[,] lr = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    lr[i, j] = 0;
                    lr[i, i] = 1;
                }
            }
            Console.WriteLine();
            for (int i = 0; i < n - 1; i++)
            {
                Console.WriteLine("R{0} Matrix:", i+1);
                for (int j = i+1; j < n; j++)
                {

                        lr[i, j] = resistance[i, j];
                    
                }
                for (int z = 0; z < n; z++)
                {
                    for (int c = 0; c < n; c++)
                        Console.Write("\t {0:F3}", lr[z, c]);
                    Console.WriteLine();
                }
                for (int o = 0; o < n; o++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        lr[o, j] = 0;
                        lr[o, o] = 1;
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("L{0} Matrix:", i + 1);
                for (int j = i; j < n; j++)
                {
                    lr[j, i] = resistance[j, i];
                }
                for (int z = 0; z < n; z++)
                {
                    for (int c = 0; c < n; c++)
                        Console.Write("\t {0:F3}", lr[z, c]);
                    Console.WriteLine();
                }
                for (int o = 0; o < n; o++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        lr[o, j] = 0;
                        lr[o, o] = 1;
                    }
                }
            }
            Console.WriteLine("B Matrix:");
            for (int j = 0; j < n; j++)
            {
                Console.Write("\t {0}",current[j]);
            }
            Console.WriteLine();
            Console.WriteLine("L * B -> B");
            Console.WriteLine();
            for (int k = 0; k < n; k++)
            {
                for (int j = n-1; j > k; j--)
                {
                    current[j] += resistance[j, k] * current[k];
                }
                current[k] *= resistance[k, k];
                //Console.WriteLine("B={0:F3}", current[k]);
            }
            Console.WriteLine();
            Console.WriteLine("R * B -> B");
            Console.WriteLine();
            for (int k = n - 2; k >= 0; k--)
                for (int j = n - 1; j > k; j--)
                {
                    current[k] += resistance[k, j] * current[j];
                    //Console.WriteLine("B={0:F3}", current[k]);
                }
            Console.WriteLine();
            Console.WriteLine("RESULT:");
            for (int i = 0; i < n; i++) {
                Console.Write("\t {0:F3}", current[i]);
                
            }
            Console.ReadKey();
        }
        }
}
