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

        public double pCrossover { get; }


        #endregion

        #region Public Funtions

        //Population Random generations
        //Run Function 

        #endregion

        #region Privates Fucntion

        private void Selection()
        {

        }

        private void CrossOver()
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
                        selectedPopulation[i+1][j] = aux * selectedPopulation[i][j] + (1-aux) * selectedPopulation[i + 1][j];
                    }
                }
            }
        }

        private void Mutation()
        {
        }

        #endregion



        #region Classes
        public class Result
        {

        }

       
        #endregion
    }

}

