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

        //Run Function 

        #endregion

        #region Privates Fucntion

        private void Selection()
        {

        }

        private void Crossover()
        {
            Random rnd = new Random();
            double[][] selectedPopulation = populations.ElementAt(populations.Count);
            for (int i = 0; i < populationSize; i = i + 2)
            {
                if (rnd.NextDouble() <= pCrossover)
                {
                    for (int j = 0; j < rangeFeatures.Length; j++)
                    {
                        double aux = rnd.NextDouble();
                        selectedPopulation[i][j] = (1 - aux) * selectedPopulation[i][j] + aux * selectedPopulation[i + 1][j];
                        selectedPopulation[i + 1][j] = aux * selectedPopulation[i][j] + (1 - aux) * selectedPopulation[i + 1][j];
                    }
                }
            }
        }

        private void Mutation()
        {
            Random rnd = new Random();
            double[][] selectedPopulation = populations.ElementAt(populations.Count);

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
        }

        #endregion



        #region Classes
        public class Result
        {

        }

       
        #endregion
    }

}

