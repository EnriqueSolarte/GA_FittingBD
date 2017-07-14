using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Optimization
{
    public class GeneticAlgorithm : IOptimization
    {
        #region Properties

        #region Interface properties

        public Result OPTResult { get; set; }
        public List<Result> OPTHistoryResult { get; set; }

        //Interface Method
        public double[] Solve(ObjectiveFunction ObjFunc)
        {
            double[] result = new double[GA_Settings.RangeFeatures.Count];
            if (Validation())
            {

                PopulationsHistory.Clear();
                FittnesHistory.Clear();

                #region Initial Population and its Fitness

                double[][] initilaPopulation = initializePopulation();
                PopulationsHistory.Add(initilaPopulation);

                Fitness initialFitness = new Fitness();
                for (int i = 0; i < GA_Settings.PopulationSize; i++)
                {
                    initialFitness.FitnessPopulation[i] = ObjFunc(initilaPopulation[i]);
                }

                initialFitness.SetFitenessData();
                initialFitness.BestFeature = initilaPopulation[initialFitness.MaxFitnessIndex];
                FittnesHistory.Add(initialFitness);

                #endregion

                int gen = 0;

                #region While Loop for Genereations
                while (GA_Settings.Generations <= gen)
                {
                    double[][] currentPopulation = PopulationsHistory.Last();
                    Fitness currentFitness = new Fitness();
                    for (int i = 0; i < GA_Settings.PopulationSize; i++)
                    {
                        currentFitness.FitnessPopulation[i] = ObjFunc(currentPopulation[i]);
                    }
                    currentFitness.SetFitenessData();
                    currentFitness.BestFeature = currentPopulation[currentFitness.MaxFitnessIndex];

                    double[][] newPopulation = new double[GA_Settings.PopulationSize][];

                    newPopulation = Selection(currentPopulation, currentFitness.FitnessPopulation);
                    newPopulation[0] = currentFitness.BestFeature;
                    newPopulation = Crossover(newPopulation);
                    newPopulation = Mutation(newPopulation);
                  
                    Fitness newFitness = new Fitness();
                    for (int i = 0; i < GA_Settings.PopulationSize; i++)
                    {
                        newFitness.FitnessPopulation[i] = ObjFunc(newPopulation[i]);
                    }
                    newFitness.SetFitenessData();


                    #region Saving Best fearture  NEW Vs CURRENT
                    if (newFitness.MaxFitness <= currentFitness.MaxFitness)
                    { 
                        newPopulation[0] = newFitness.BestFeature = currentFitness.BestFeature;
                        newFitness.FitnessPopulation[0] = currentFitness.MaxFitness;
                        newFitness.SetFitenessData();
                    }
                    else
                    {
                        newFitness.BestFeature = newPopulation[newFitness.MaxFitnessIndex];
                    }

                    #endregion


                    PopulationsHistory.Add(newPopulation);
                    FittnesHistory.Add(newFitness);

                    result = newFitness.BestFeature;
                    OPTResult.Parameters = result;
                    OPTResult.target = newFitness.MaxFitness;
                    OPTHistoryResult.Add(OPTResult);
                    gen++;

                }
                #endregion

            }


            return result;
        }

        #endregion


        public Settings GA_Settings { get; set; }

        public List<double[][]> PopulationsHistory { get; }
        public List<Fitness> FittnesHistory { get; }


        #endregion

        #region Consntructors
        public GeneticAlgorithm(int populationSize, List<Range> rangeFeatures, double pCross, double pMutation, int generations)
        {
            GA_Settings.PopulationSize = populationSize;
            GA_Settings.RangeFeatures = rangeFeatures;
            GA_Settings.PCrossover = pCross;
            GA_Settings.PMutation = pMutation;
            GA_Settings.Generations = generations;
        }

        public GeneticAlgorithm(int populationSize, List<Range> rangeFeatures, double pCross, double pMutation, Range range)
        {
            GA_Settings.PopulationSize = populationSize;
            GA_Settings.RangeFeatures = rangeFeatures;
            GA_Settings.PCrossover = pCross;
            GA_Settings.PMutation = pMutation;
            GA_Settings.TargetRange = range;
        }

        public GeneticAlgorithm()
        {

        }
        #endregion

        #region Internal Function to GA

        private double[][] initializePopulation()
        {
            ///Initialize a new population with the related parameters at GA_Settings
            ///IT DOES NOT ADD TO POPULATION LIST
            Random rnd = new Random();
            double[][] newPopulation = new double[GA_Settings.PopulationSize][];
            for (int i = 0; i < GA_Settings.PopulationSize; i++)
            {
                for (int j = 0; j < GA_Settings.RangeFeatures.Count; j++)
                {
                    newPopulation[i][j] = GA_Settings.RangeFeatures.ElementAt(j).MinValue + rnd.NextDouble() * (GA_Settings.RangeFeatures.ElementAt(j).MaxValue - GA_Settings.RangeFeatures.ElementAt(j).MinValue);
                }
            }
            return newPopulation;
        }

        private double[][] Selection(double[][] currentPopulation, double[] currentFitness)
        {
            Random rnd = new Random();
            double sumatoryFitness = currentFitness.Sum();

            double[][] selectedPopulation = new double[GA_Settings.PopulationSize][];

            for (int i = 0; i < GA_Settings.PopulationSize; i++)
            {
                double test = rnd.NextDouble() * sumatoryFitness;
                double partSum = currentFitness[0];
                int j = 0;

                while (partSum < test)
                {
                    partSum = partSum + currentFitness[j + 1];
                    j++;

                    if (j == GA_Settings.PopulationSize) j = 0;
                }
                selectedPopulation[i] = currentPopulation[j];
            }

            return selectedPopulation;
        }

        private double[][] Crossover(double[][] currentPopulation)
        {
            Random rnd = new Random();
            double[][] selectedPopulation = currentPopulation;

            for (int i = 0; i < GA_Settings.PopulationSize; i = i + 2)
            {
                if (rnd.NextDouble() <= GA_Settings.PCrossover)
                {
                    for (int j = 0; j < GA_Settings.RangeFeatures.Count; j++)
                    {
                        double aux = rnd.NextDouble();
                        selectedPopulation[i][j] = (1 - aux) * currentPopulation[i][j] + aux * currentPopulation[i + 1][j];
                        selectedPopulation[i + 1][j] = aux * currentPopulation[i][j] + (1 - aux) * currentPopulation[i + 1][j];
                    }
                }
            }
            return selectedPopulation;
        }

        private double[][] Mutation(double[][] currentPopulation)
        {
            Random rnd = new Random();
            double[][] selectedPopulation = currentPopulation;

            for (int i = 0; i < GA_Settings.PopulationSize; i++)
            {
                if (rnd.NextDouble() <= GA_Settings.PMutation)
                {
                    for (int j = 0; j < GA_Settings.RangeFeatures.Count; j++)
                    {
                        selectedPopulation[i][j] = GA_Settings.RangeFeatures.ElementAt(j).MinValue + rnd.NextDouble() * (GA_Settings.RangeFeatures.ElementAt(j).MaxValue - GA_Settings.RangeFeatures.ElementAt(j).MinValue);
                    }
                }
            }

            return selectedPopulation;
        }

        private bool Validation()
        {
            bool validation = true;
            return validation;
        }

        #endregion

        public class Settings
        {
            public int PopulationSize { get; set; }
            public List<Range> RangeFeatures { get; set; }
            public double PCrossover { get; set; }
            public double PMutation { get; set; }
            public Range TargetRange { get; set; }
            public int Generations { get; set; }
        }

        public class Range
        {
            public double MaxValue { get; set; }
            public double MinValue { get; set; }
        }

        public  class Fitness
        {
            public double MaxFitness { get; set; }
            public double MeanFitness { get; set; }
            public int MaxFitnessIndex { get; set; }
            public double[] BestFeature { get; set; }
            public double[] FitnessPopulation { get; set; }
           

            internal void SetFitenessData()
            {
                MaxFitness = FitnessPopulation.Max();
                MeanFitness = FitnessPopulation.Sum() / FitnessPopulation.Count();
                MaxFitnessIndex = FitnessPopulation.ToList().IndexOf(MaxFitness);
            }

        }

    }

}

