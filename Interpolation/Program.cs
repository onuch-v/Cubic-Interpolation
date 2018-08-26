using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathLibrary;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 40;
            double Fs = 20.0;
            double[] x = new double[N];
            double[] y = new double[N];

            double[] x1 = new double[2 * N - 1];

            for ( int i = 0; i < N; i++)
            {
                x[i] = i / Fs;
                y[i] = Math.Sin(2.0 * Math.PI * x[i]);
            }

            for (int i = 0; i < x1.Length; i++)
                x1[i] = i / (2.0 * Fs);

            double[] y1 = Interpolation.CubicInterpolation(x, y, x1);
            Console.WriteLine("Enjoy!");
            Console.ReadKey();
        }
    }
}
