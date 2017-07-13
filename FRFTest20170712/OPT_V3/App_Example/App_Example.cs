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

        public void Run_OPT()
        {
            
            Response RES = new Response();
            Variable VARS = new Variable();


            GA GA_Solver = new GA(FEM_Solver, RES, VARS);

            GA_Solver.Solve();

        }
    }


}
