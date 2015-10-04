using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public abstract class ReproductionFunction
    {
        protected string _name;
        protected int _numParents;

        private static List<ReproductionFunction> _registry = new List<ReproductionFunction>
        {
            new InterlaceRows(),
            new InterlaceColumns(),
            new Scramble(),
            new Sum(),
            new MakeHorzSymmetric(),
            new MakeVertSymmetric(),
        };

        public static IEnumerable<ReproductionFunction> All
        {
            get
            {
                return _registry;
            }
        }

        public static ReproductionFunction Get(int index)
        {
            return _registry[index];
        }

        public static ReproductionFunction Get(string name)
        {
            return _registry.Find(cs => name == cs.Name);
        }

        public static List<string> FunctionNames
        {
            get { return _registry.Select(item => item.Name).ToList(); }
        }

        public ReproductionFunction(string name, int numParents)
        {
            _name = name;
            _numParents = numParents;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int NumParents
        {
            get
            {
                return _numParents;
            }
        }

        public abstract CellularAutomataRules Generate(List<CellularAutomataRules> parents);

        static public DiscreteWeightedDistribution<CellularAutomataRules> RulesDistribution(List<CellularAutomataRules> parents)
        {
            return new DiscreteWeightedDistribution<CellularAutomataRules>(
                parents.Select(ca => new Tuple<CellularAutomataRules, int>(ca, 1))
            );
        }

        /*
        static public DiscreteWeightedDistribution<CellularAutomataRules> RulesDistribution(List<CellularAutomataRules> parents)
        {
            return new DiscreteWeightedDistribution<CellularAutomataRules>(
                parents.Select( ca => new Tuple<CellularAutomataRules,int>(ca, ca.Fitness) )
            );
        }
        */
    }

    /*---------------------------------------------------------------------------------------------*/

    public class InterlaceRows : ReproductionFunction
    {
        public InterlaceRows() : base("Interlace rows", 2)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents || !parents[0].HasSameDimensions(parents[1]))
                throw new ArgumentException();
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            var rndParent = RulesDistribution(parents);
            for (int row = 0; row < child.NumStates; row++)
            {
                CellularAutomataRules parent = rndParent.Next();
                for (int col = 0; col < child.Range; col++)
                    child[col, row] = parent[col, row];
            }
            return child;
        }
    }

    /*---------------------------------------------------------------------------------------------*/

    public class MakeVertSymmetric : ReproductionFunction
    {
        public MakeVertSymmetric()
            : base("Make vertically symmetric", 1)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents)
                throw new ArgumentException();
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            var parent = parents[0];

            int rows = parents[0].NumStates;
            int halfRows = rows / 2;
            for (int i = 0; i < halfRows; i++ )
                for (int col = 0; col < child.Range; col++)
                {
                    child[col, i] = parent[col, i];
                    child[col, rows - i - 1] = parent[col, i];
                }
            return child;
        }
    }

    /*---------------------------------------------------------------------------------------------*/

    public class MakeHorzSymmetric : ReproductionFunction
    {
        public MakeHorzSymmetric()
            : base("Make horizontally symmetric", 1)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents)
                throw new ArgumentException();
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            var parent = parents[0];

            int columns = parents[0].Range;
            int halfCols = columns / 2;
            for (int row = 0; row < child.NumStates; row++)
                for (int i = 0; i < halfCols; i++)
                {
                    child[i, row] = parent[i, row];
                    child[columns - i - 1, row] = parent[i, row];
                }
            return child;
        }
    }

    /*---------------------------------------------------------------------------------------------*/

    public class InterlaceColumns : ReproductionFunction
    {
        public InterlaceColumns()
            : base("Interlace columns", 2)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents || !parents[0].HasSameDimensions(parents[1]))
                throw new ArgumentException();
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            var rndParent = RulesDistribution(parents);
            for (int col = 0; col < child.Range; col++)
            {
                CellularAutomataRules parent = rndParent.Next();
                for (int row = 0; row < child.NumStates; row++)
                    child[col, row] = parent[col, row];
            }
            return child;
        }
    }

    /*---------------------------------------------------------------------------------------------*/

    public class Scramble : ReproductionFunction
    {
        public Scramble()
            : base("Scramble", 2)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents || !parents[0].HasSameDimensions(parents[1]))
                throw new ArgumentException();
            var rndParent = RulesDistribution(parents);
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            for (int col = 0; col < child.Range; col++)
                for (int row = 0; row < child.NumStates; row++)
                    child[col, row] = (rndParent.Next())[col, row];
            return child;
        }
    }

        /*---------------------------------------------------------------------------------------------*/

    public class Sum : ReproductionFunction
    {
        public Sum()
            : base("Sum", 2)
        {
        }

        public override CellularAutomataRules Generate(List<CellularAutomataRules> parents)
        {
            if (parents.Count() != NumParents || !parents[0].HasSameDimensions(parents[1]))
                throw new ArgumentException();
            var child = new CellularAutomataRules(parents[0].Range, parents[0].NumStates);
            for (int col = 0; col < child.Range; col++)
                for (int row = 0; row < child.NumStates; row++)
                    child[col, row] = (parents[0][col, row] + parents[1][col, row]) % child.NumStates;
            return child;
        }
    }
}
