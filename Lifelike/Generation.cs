using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class Generation
    {
        //private List<CellularAutomataRules> _cellularAutomata;
        //private DiscreteWeightedDistribution _discreteDist;
        private DiscreteWeightedDistribution<CellularAutomataRules> _ca;

        public Generation()
        {
            _ca = new DiscreteWeightedDistribution<CellularAutomataRules>();
        }

        public CellularAutomataRules RandomRules
        {
            get
            {
                return (_ca.Count > 0) ?
                    _ca.Next() :
                    null;
            }
        }

        public void Add(CellularAutomataRules rules)
        {
            _ca.Insert(rules, rules.Fitness);
        }

        public int NumItems
        {
            get
            {
                return _ca.Count;
            }
        }

        public List<CellularAutomataRules> GetUniqueRules(int n)
        {
            if (n == 1)
                return new List<CellularAutomataRules>{ RandomRules };
            var used = new HashSet<CellularAutomataRules>();
            for (int i = 0; i < n; i++)
            {
                CellularAutomataRules newRules;
                do
                {
                    newRules = _ca.Next();
                }
                while (used.Contains(newRules));
                used.Add(newRules);
            }
            return used.ToList();
        }
    }
}
