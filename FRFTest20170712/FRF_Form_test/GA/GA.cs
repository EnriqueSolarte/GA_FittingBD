using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class GA
    {
        #region Properties

        public List<double[][]> populations { get; }
        public Result result { get; }
        public int generations { get; }

        private double[][] rangeFeatures { get; set; }
        private double pCrossover { get; }
        private double pMutation { get; }
        private int populationSize { get; set; }


        private double[] curretnFitness;
        private double sumatoryFitness;
        

        #endregion

        #region Public Funtions
        //Population Random generations
        private double[][] initializePopulation()
        {
            Random rnd = new Random();
            double[][] newPopulation = new double[populationSize][];
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < rangeFeatures.Length; j++)
                {
                    newPopulation[i][j] = rangeFeatures[j][0] + rnd.NextDouble() * (rangeFeatures[j][1] - rangeFeatures[j][0]);
                }
            }
            return newPopulation;
        }

        public GA(int _populationSize, double[][] _rangefeatures, double _pCrossover, double _pMutaion)
        {
            rangeFeatures = _rangefeatures;
            populationSize = _populationSize;
            pCrossover = _pCrossover;
            pMutation = _pMutaion;
            populations.Add(initializePopulation());

        }

        public void Run()
        {
            double[][] currentPopulation = populations.ElementAt(populations.Count);
            double[][] newPopulation = Selection(currentPopulation);
            newPopulation = Crossover(newPopulation);
            newPopulation = Mutation(newPopulation);
            populations.Add(newPopulation);
        }

        #endregion

        #region Privates Fucntion

        private double[][] Selection(double[][] currentPopulation)
        {
            Random rnd = new Random();
            double[][] selectedPopulation = new double[populationSize][];

            for (int i =0; i < populationSize; i++)
            {
                double test = rnd.NextDouble() * sumatoryFitness;
                double partSum = curretnFitness[0];
                int j = 0;

                while (partSum < test)
                {
                    partSum = partSum + curretnFitness[j + 1];
                    j++;

                    if (j == populationSize) j = 0;
                }
                selectedPopulation[i] = currentPopulation[j];                              
            }

            return selectedPopulation;
        }

        private double[][] Crossover(double[][] currentPopulation)
        {
            Random rnd = new Random();
            double[][] selectedPopulation = currentPopulation;
            for (int i = 0; i < populationSize; i = i + 2)
            {
                if (rnd.NextDouble() <= pCrossover)
                {
                    for (int j = 0; j < rangeFeatures.Length; j++)
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

            for (int i = 0; i < populationSize; i++)
            {
                if (rnd.NextDouble() <= pMutation)
                {
                    for (int j = 0; j < rangeFeatures.Length; j++)
                    {
                        selectedPopulation[i][j] = rangeFeatures[j][0] + rnd.NextDouble() * (rangeFeatures[j][1] - rangeFeatures[j][0]);
                    }
                }
            }

            return selectedPopulation;
        }

        #endregion



        #region Classes
        public class Result
        {
            //Propieties to Reult
            public List<double> maxFitnessRecord { get; }
            public List<double> meanFitnessRecord { get; }
            public List<double[]> bestFeaturesRecord { get; }
            public double[] bestFeature { get; set; }
        }

       
        #endregion
    }

}

