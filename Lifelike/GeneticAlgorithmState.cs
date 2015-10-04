using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class GeneticAlgorithmState
    {
        private CellularAutomataSettings _settingsCa;
        private GeneticAlgorithmSettings _settingsGa;
        private List<Generation> _generations = null;
        private CellularAutomataRules _currentCa;

        public GeneticAlgorithmState()
        {
            _settingsCa = new CellularAutomataSettings(7);
            _settingsGa = new GeneticAlgorithmSettings(7);
        }

        public void Start()
        {
            _generations = new List<Generation>();
            _currentCa = _settingsCa.GetRandomRules(_settingsGa.StateTableDistribution);
        }

        public void Stop()
        {
            _generations = null;
        }

        public bool IsInProgess
        {
            get
            {
                return _generations != null;
            }
        }

        public CellularAutomataSettings CaSettings
        {
            get
            {
                return _settingsCa;
            }
        }

        public GeneticAlgorithmSettings GaSettings
        {
            get
            {
                return _settingsGa;
            }
        }

        public Generation CurrentGeneration
        {
            get
            {
                if (_generations.Count() == 0)
                    _generations.Add(new Generation());
                return _generations.Last();
            }
        }

        public Generation PreviousGeneration
        {
            get
            {
                int n = _generations.Count();
                return (n >= 2) ? _generations[n - 2] : null;
            }
        }

        public int NumRulesInGeneration
        {
            get
            {
                return CurrentGeneration.NumItems;
            }
        }

        public CellularAutomataRules CurrentRules
        {
            get
            {
                return _currentCa;
            }
            set
            {
                _currentCa = value;
            }
        }

        public Cells InitialCells 
        { 
            get
            {
                return _settingsCa.GetInitialCells( _settingsGa.InitialStateDistribution );
            }
        }

        public void ApplyFitnessJudgement(FitnessJudgement judgement)
        {
            int weight = _settingsGa.GetWeightOfFitnessJudgement(judgement);
            if (weight > 0)
            {
                _currentCa.Fitness = weight;
                CurrentGeneration.Add(_currentCa);
            }
            GenerateNewCa();
        }

        private void GenerateNewCa()
        {
            if (PreviousGeneration == null)
                _currentCa = _settingsCa.GetRandomRules( _settingsGa.StateTableDistribution );
            else
                _currentCa = GenerateChildCa(PreviousGeneration);
        }

        private CellularAutomataRules GenerateChildCa(Generation parentGeneration)
        {
            var funcRepro = _settingsGa.ReproductionFunctionDistribution.Next();
            var funcMutate = _settingsGa.MutationFunctionDistribution.Next();

            var parents = parentGeneration.GetUniqueRules(funcRepro.NumParents);
            return funcMutate.Mutate(funcRepro.Generate(parents), _settingsGa);
        }

        public bool CanGoToNextGeneration
        {
            get
            {
                return (CurrentGeneration.NumItems >= ReproductionFunction.All.Max(f => f.NumParents));
            }
        }

        public void GoToNextGeneration()
        {
            if (!CanGoToNextGeneration)
                return;
            _generations.Add(new Generation());
            GenerateNewCa();
        }

        public void GoToPrevGeneration()
        {
            int n = _generations.Count();
            if (n <= 1)
                return;
            _generations.RemoveAt(n - 1);
            GenerateNewCa();
        }

        public int GenerationNumber 
        { 
            get
            {
                return (_generations != null) ? _generations.Count() : 0;
            }
        }

        public int GenerationItemCount { get; set; }
    }

    public enum FitnessJudgement
    {
        Skip,
        SuperBad,
        Bad,
        Okay,
        Good,
        SuperGood
    }
}
