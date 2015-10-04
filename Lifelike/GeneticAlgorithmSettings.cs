using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class GeneticAlgorithmSettings
    {
        private List<double> _initialStateProbabilities;
        private DiscreteProbabilityDistribution<int> _initialStateDistribution;

        private Dictionary<FitnessJudgement, int> _fitnessWeights = new Dictionary<FitnessJudgement, int>{
            {FitnessJudgement.Skip, 0},
            {FitnessJudgement.SuperBad, 1},
            {FitnessJudgement.Bad, 2},
            {FitnessJudgement.Okay, 4},
            {FitnessJudgement.Good, 8},
            {FitnessJudgement.SuperGood, 16}
        };
        private List<double> _stateTableProbabilities;
        private DiscreteProbabilityDistribution<int> _stateTableDistribution;
        private List<double> _mutationFuncProbabilities;
        private DiscreteProbabilityDistribution<MutationFunction> _mutationFuncDistribution;
        private double _mutationTemperature = 0.1;
        private List<double> _reproductionFuncProbabilities;
        private DiscreteProbabilityDistribution<ReproductionFunction> _reproductionFuncDistribution;

        public GeneticAlgorithmSettings(int states)
        {
            SetNumStates(states);
            MutationFunctionProbabilities = Util.GetLopsidedProbabilities(0.90, states);
            int numRepoFuncs = ReproductionFunction.All.Count();
            ReproductionFuncProbabilities = Enumerable.Repeat(1.0 / (double)numRepoFuncs, numRepoFuncs).ToList();
        }

        public int GetWeightOfFitnessJudgement(FitnessJudgement judgement)
        {
            return _fitnessWeights[judgement];
        }

        public double MutationTemperature
        {
            get
            {
                return _mutationTemperature;
            }

            set
            {
                _mutationTemperature = value;
            }
        }

        public DiscreteProbabilityDistribution<int> InitialStateDistribution
        {
            get
            {
                return _initialStateDistribution;
            }
        }

        public List<double> InitialStateProbabilities 
        {
            get
            {
                return _initialStateProbabilities;
            }
            set
            {
                _initialStateProbabilities = value;
                int states = value.Count();
                _initialStateDistribution = new DiscreteProbabilityDistribution<int>(
                    Enumerable.Range(0, states).Select(i => new Tuple<int, double>(i, value[i]))
                );
            }
        }

        public DiscreteProbabilityDistribution<int> StateTableDistribution
        {
            get
            {
                return _stateTableDistribution;
            }
        }

        public List<double> StateTableProbabilities
        {
            get
            {
                return _stateTableProbabilities;
            }
            set
            {
                _stateTableProbabilities = value;
                int states = value.Count();
                _stateTableDistribution = new DiscreteProbabilityDistribution<int>(
                    Enumerable.Range(0, states).Select(i => new Tuple<int, double>(i, value[i]))
                );
            }
        }

        public DiscreteProbabilityDistribution<MutationFunction> MutationFunctionDistribution
        {
            get
            {
                return _mutationFuncDistribution;
            }
        }

        public List<double> MutationFunctionProbabilities 
        { 
            get
            {
                return _mutationFuncProbabilities;
            }
            set
            {
                _mutationFuncProbabilities = value;
                _mutationFuncDistribution = new DiscreteProbabilityDistribution<MutationFunction>(
                    MutationFunction.All.Select(
                        (MutationFunction func, int i) => {
                            return new Tuple<MutationFunction, double>(func, value[i]);
                        }
                    )
                );
            }
        }

        public DiscreteProbabilityDistribution<ReproductionFunction> ReproductionFunctionDistribution
        {
            get
            {
                return _reproductionFuncDistribution;
            }
        }

        public List<double> ReproductionFuncProbabilities
        {
            get
            {
                return _reproductionFuncProbabilities;
            }
            set
            {
                _reproductionFuncProbabilities = value;
                _reproductionFuncDistribution = new DiscreteProbabilityDistribution<ReproductionFunction>(
                    ReproductionFunction.All.Select(
                        (ReproductionFunction func, int i) =>
                        {
                            return new Tuple<ReproductionFunction, double>(func, value[i]);
                        }
                    )
                );
            }
        }

        public void SetNumStates(int states)
        {
            InitialStateProbabilities = Enumerable.Repeat(1.0 / (double)states, states).ToList();
            StateTableProbabilities = Util.GetLopsidedProbabilities(0.5, states);
        }
    }
}
