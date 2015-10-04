using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class IndexPair
    {
        public int Column;
        public int Row;

        public IndexPair(int col, int row)
        {
            Column = col;
            Row = row;
        }
    }

    static class Util
    {
        private static Random _rnd = new Random();

        public static double RndUniformReal
        {
            get
            {
                return _rnd.NextDouble();
            }
        }

        // return a uniformly distributed random integer in [0,...,n)...
        public static int RndUniformInt(int n)
        {
            return _rnd.Next(n);
        }

        // return a uniformly distributed random integer in [n1,...,n2)...
        public static int RndUniformInt(int n1, int n2)
        {
            return _rnd.Next(n1, n2);
        }

        public static double RndNormalReal(double mean, double stddev)
        {
            double u1 = RndUniformReal;
            double u2 = RndUniformReal;
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stddev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

        public static int[,] GetRandomInt2dArray(int cols, int rows, int states, double aliveProbability)
        {
            var ary = new int[cols, rows];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    ary[col, row] = (Util.RndUniformReal < aliveProbability) ?
                        Util.RndUniformInt(states):
                        0;
            return ary;
        }

        public static int[,] GetRandomInt2dArray(int cols, int rows, DiscreteProbabilityDistribution<int> stateDistribution)
        {
            var ary = new int[cols, rows];
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    ary[col, row] = stateDistribution.Next();
            return ary;
        }

        public static List<double> GetLopsidedProbabilities(double probOfZero, int states)
        {
            double probOfNonZero = (1.0 - probOfZero) / ((double)states - 1);
            List<double> probabilities = Enumerable.Repeat(probOfNonZero, states - 1).ToList();
            probabilities.Insert(0, probOfZero);
            return probabilities;
        }
    }
}
