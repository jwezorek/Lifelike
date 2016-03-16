using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public abstract class MutationFunction
    {
        protected string _name;

        private static List<MutationFunction> _registry = new List<MutationFunction>
        {
            new NoMutation(),
            new RandomUnconstrainedChanges(),
            new DuplicateRows(),
            new DuplicateColumns(),
            new Unzero(),
            new Zero()
        };

        public static IEnumerable<MutationFunction> All
        {
            get
            {
                return _registry;
            }
        }

        public static MutationFunction Get(int index)
        {
            return _registry[index];
        }

        public static MutationFunction Get(string name)
        {
            return _registry.Find(cs => name == cs.Name);
        }

        public static List<string> FunctionNames
        {
            get { return _registry.Select(item => item.Name).ToList(); }
        }

        public MutationFunction(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public abstract CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings);
    }

    public class NoMutation : MutationFunction
    {
        public NoMutation() : base( "No mutation" )
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            return ca;
        }
    }

    public class RandomUnconstrainedChanges : MutationFunction
    {
        public RandomUnconstrainedChanges() : base( "Random unconstrained changes" )
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            var mutant = new CellularAutomataRules(ca);
            int n = Util.RndUniformInt((int)(ca.Range * ca.NumStates * settings.MutationTemperature))+1;
            for (int i = 0; i < n; i++)
            {
                int row = Util.RndUniformInt(ca.NumStates);
                int col = Util.RndUniformInt(ca.Range);

                mutant[col, row] = settings.StateTableDistribution.Next();
            }
            return mutant;
        }
    }

    public class DuplicateRows : MutationFunction
    {
        public DuplicateRows()
            : base("Duplicate rows")
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            var mutant = new CellularAutomataRules(ca);
            int n = Util.RndUniformInt((int)(ca.NumStates * settings.MutationTemperature)) + 1;
            for (int i = 0; i < n; i++)
            {
                int rowSrc = Util.RndUniformInt(ca.NumStates);
                int rowDest = rowSrc;
                while (rowDest == rowSrc)
                    rowDest = Util.RndUniformInt(ca.NumStates);

                for (int column = 0; column < ca.Range; column++)
                    mutant[column, rowDest] = mutant[column, rowSrc];
            }
            return mutant;
        }
    }

    public class Unzero : MutationFunction
    {
        public Unzero()
            : base("Unzero a few state table states")
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            var mutant = new CellularAutomataRules(ca);
            int n = Util.RndUniformInt((int)(10.0 * settings.MutationTemperature)) + 1;
            for (int i = 0; i < n; i++)
            {
                int row, col;
                int count = 0;
                do
                {
                    row = Util.RndUniformInt(ca.NumStates);
                    col = Util.RndUniformInt(ca.Range);
                } while (mutant[col, row] != 0 && count++ < 20);
                mutant[col, row] = Util.RndUniformInt(ca.NumStates - 1) + 1;
            }
            return mutant;
        }
    }

    public class Zero : MutationFunction
    {
        public Zero()
            : base("Zero out a few state table states")
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            var mutant = new CellularAutomataRules(ca);
            int n = Util.RndUniformInt((int)(10.0 * settings.MutationTemperature)) + 1;
            for (int i = 0; i < n; i++)
            {
                int row, col;
                int count = 0;
                do
                {
                    row = Util.RndUniformInt(ca.NumStates);
                    col = Util.RndUniformInt(ca.Range);
                } while (mutant[col, row] == 0 && count++ < 20);
                mutant[col, row] = 0;
            }
            return mutant;
        }
    }

    public class DuplicateColumns : MutationFunction
    {
        public DuplicateColumns()
            : base("Duplicate columns")
        {
        }

        public override CellularAutomataRules Mutate(CellularAutomataRules ca, GeneticAlgorithmSettings settings)
        {
            var mutant = new CellularAutomataRules(ca);
            int n = Util.RndUniformInt((int)(ca.Range * settings.MutationTemperature)) + 1;
            for (int i = 0; i < n; i++)
            {
                int colSrc = Util.RndUniformInt(ca.Range);
                int colDest = colSrc;
                while (colDest == colSrc)
                    colDest = Util.RndUniformInt(ca.Range);

                for (int row = 0; row < ca.NumStates; row++)
                    mutant[colDest, row] = mutant[colSrc, row];
            }
            return mutant;
        }
    }
}
