using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Optimization;

namespace App_Example
{
    public class App_Example
    {

        public double[] FEM_Solver(double[] var)
        {
            //FEM calculation
            double[] results = new double[2];

            return results;
        }

        public double[] SecondObjFunction(double[] var)
        {
            return new double[2];
        }

        public void Run_OPT()
        {
            GeneticAlgorithm Ga = new GeneticAlgorithm();
            Ga.Solve();

        }
    }


}
