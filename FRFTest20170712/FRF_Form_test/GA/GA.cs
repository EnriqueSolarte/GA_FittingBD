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
        public double pMutation { get; }
        public double[][] rangeFeatures { get; set; }


        #endregion

        #region Public Funtions

        //Population Random generations
        //Run Function 

        #endregion

        #region Privates Fucntion

        private void Selection()
        {

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

        }

       
        #endregion
    }

}

