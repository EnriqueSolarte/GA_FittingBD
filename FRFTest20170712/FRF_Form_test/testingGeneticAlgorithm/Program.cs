using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimization;

namespace testingGeneticAlgorithm
{
    class Program
    {

         static void Main(string[] args)
        {
            functionOptimization A = new functionOptimization();
            A.target_A = A.myFuction_A(new double[] { 15, 12, 0.35, 0.05 });
            A.RunSolution();

            List<Result> myResult =  A.GA_ListResults;
           
 
        }    

    }

    public class TableOptimization
    {
        private double myObjFunction_B(double[] parameters)
        {
            return (Math.Exp(parameters[1] + 5 * parameters[0]) + 100 * parameters[3]) / (Math.Exp(parameters[1] * parameters[2]));
        }

        public void RunSolution()
        {
            List<GeneticAlgorithm.Range> FeactureRange= new List<GeneticAlgorithm.Range>();
            FeactureRange.Clear();
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 100 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 200 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.1, MaxValue = 100 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.001, MaxValue = 120});

            GeneticAlgorithm GA_1 = new GeneticAlgorithm(1000, FeactureRange, 0.9, 0.9, new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 10 });
            GA_1.GA_Settings.Generations = 100;
            GA_1.Solve(myObjFunction_B);
            
        }

    }

    public class functionOptimization
    {
        public double[][] target_A { get; set; }

        public Result GA_Result { get; set; }
        public List<Result> GA_ListResults { get; set; }

        public double[][] myFuction_A(double[] parameters)
        {
            double[] xVar = new double[101];
            double[] yVar = new double[xVar.Length];
            xVar[0] = 0;
            for (int i = 0; i < xVar.Length; i++)
            {
                
                yVar[i] = Math.Sin(xVar[i] * xVar[i] * parameters[0] + parameters[1]) + Math.Log(xVar[i] * xVar[i] * parameters[2] + parameters[3]);
                if (i != (xVar.Length - 1))
                {
                    xVar[i + 1] = xVar[i] + 0.1;
                }
                
            }

            double[][] result = new double[2][];
            result[0] = xVar;
            result[1] = yVar;
            return result;
        }

        private double myObjFunction_A(double[] parameters)
        {
            double error = double.MaxValue;

            double[][] Evaluation = myFuction_A(parameters);

            double[] LocalError = new double[target_A[0].Length];
            for (int i = 0; i < target_A[0].Length; i++)
            {
                LocalError[i] = Math.Abs(Evaluation[1][i] - target_A[1][i]);
            }

            error = LocalError.Sum();
            return 0.01/error;
        }

        public void RunSolution()
        {
            List<GeneticAlgorithm.Range> FeactureRange = new List<GeneticAlgorithm.Range>();
            FeactureRange.Clear();
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 10, MaxValue = 16 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 30 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.1, MaxValue = 1.5 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.001, MaxValue = 1 });

            GeneticAlgorithm GA_1 = new GeneticAlgorithm(100, FeactureRange, 0.9, 0.4, 1000);   
            GA_1.Solve(myObjFunction_A);

            GA_Result = GA_1.OPTResult;
            GA_ListResults = GA_1.OPTHistoryResult;
        }
    }
}
