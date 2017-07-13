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

        public double[][] rangeFeatures { get; set; }

        public double pCrossover { get; }
        public double pMutation { get; }



        #endregion

        #region Public Funtions

        public GA(int population, double[][] rangefeatures, double _pCrossover, double _pMutaion)
        {

            
            pCrossover = _pCrossover;
            pMutation = _pMutaion;

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
            for (int i = 0; i < selectedPopulation.Length; i = i + 2)
            {
                if (rnd.NextDouble() <= pCrossover)
                {
                    for (int j = 0; j < selectedPopulation[0].Length; j++)
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

            for (int i = 0; i < selectedPopulation.Length; i++)
            {
                if (rnd.NextDouble() <= pMutation)
                {
                    for (int j = 0; j < selectedPopulation[0].Length; j++)
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
            //Propieties to Reult
            public double[] maxFitness{ get; }
            public double[] meanFitness { get; }
            public double[][] bestFeatures { get; }
            public double[] theBestFeture { get; set; }
        }

       
        #endregion
    }

}

