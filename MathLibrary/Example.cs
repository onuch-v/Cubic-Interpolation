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
            Console.WriteLine("Start.\n");

            ExampleWithArrays(out double[] x11, out double[] y11);

            ExampleWithoutArrays(out double[] x12, out double[] y12);

            Console.WriteLine();
            Console.WriteLine("Good Interpolation. Use It.");
            Console.WriteLine("|-------|-------|-------|-------|-------|");
            Console.WriteLine("|   i   |   x1  |   x2  |   y1  |   y2  |");
            Console.WriteLine("|-------|-------|-------|-------|-------|");
            
            for (long i = 0; i < x11.LongLength; i++)
            {
                Console.Write("|");
                Console.Write("{0,-7:F2}|", i);
                Console.Write(x11[i] < 0 ? "{0,-7:F2}|" : " {0,-6:F2}|", x11[i]);
                Console.Write(x12[i] < 0 ? "{0,-7:F2}|" : " {0,-6:F2}|", x12[i]);
                Console.Write(y11[i] < 0 ? "{0,-7:F2}|" : " {0,-6:F2}|", y11[i]);
                Console.Write(y12[i] < 0 ? "{0,-7:F2}|" : " {0,-6:F2}|", y12[i]);

                Console.WriteLine();
            }
            Console.WriteLine("|-------|-------|-------|-------|-------|");
            Console.WriteLine("Enjoy!");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">Interpolated Data OX (new abscissa)</param>
        /// <param name="y1">Interpolated Data OY (new ordinate)</param>
        static void ExampleWithArrays(out double[] x1, out double[] y1)
        {
            Console.WriteLine("Example With Arrays : Start.\n");
            x1 = null;
            y1 = null;
            int N = 40;
            double Fs = 20.0;

            double[] x = new double[N];
            double[] y = new double[N];

            x1 = new double[2 * N - 1];                             /* new data X */

            for (int i = 0; i < N; i++)
            {
                x[i] = i / Fs;
                y[i] = Math.Sin(2.0 * Math.PI * x[i]);
            }

            for (int i = 0; i < x1.Length; i++)
                x1[i] = i / (2.0 * Fs);

            y1 = Interpolation.CubicInterpolation(x, y, x1);        /* new data Y */

            Console.WriteLine("Example With Arrays : Enjoy.\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">Interpolated Data OX (new abscissa)</param>
        /// <param name="y1">Interpolated Data OY (new ordinate)</param>
        static void ExampleWithoutArrays(out double[] x1, out double[] y1)
        {
            Console.WriteLine("Example Without Arrays : Start.\n");

            x1 = null;
            y1 = null;

            int N = 40;
            double Fs = 20.0;

            double[] x = new double[N];
            double[] y = new double[N];


            for (int i = 0; i < N; i++)
            {
                x[i] = i / Fs;
                y[i] = Math.Sin(2.0 * Math.PI * x[i]);
            }

            CubicInterpolation cubic = new CubicInterpolation();
            cubic.CalcCoefficients(x, y);

            x1 = new double[2 * N - 1];
            y1 = new double[2 * N - 1];
            for (int i = 0; i < 2 * N - 1; i++)
            {
                x1[i] = i / (2.0 * Fs);
                y1[i] = cubic.Interpolate(x1[i]);
            }

            Console.WriteLine("WARNING!");
            Console.WriteLine("\tYou will do this at your own risk.");
            double newX = -2.0;                                                                     /* Never do it */
            double newY = cubic.Interpolate(newX);                                                  /* Never do it */
            Console.WriteLine("\tNew (X < sourceX[0], Y) = ({0,3:F2} ; {1,3:F2})", newX, newY);

            newX = 4 * N / Fs;                                                                      /* Never do it */
            newY = cubic.Interpolate(newX);                                                         /* Never do it */
            Console.WriteLine("\tNew (X > sourceX[end], Y) = ({0,3:F2} ; {1,3:F2})", newX, newY);

            Console.WriteLine("Example Without Arrays : Enjoy.\n");
        }
    }
}
