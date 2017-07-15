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
            TableOptimization A = new TableOptimization();
            A.RunSolution();
            
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
            GA_1.Solve(myObjFunction_B);
            
        }

    }

    public class functionOptimization
    {
        double[][] target_A;

        public double[][] myFuction_A(double[] parameters)
        {
            double[] xVar = new double[10];
            double[] yVar = new double[xVar.Length];
            for (int i = 0; i < xVar.Length; i++)
            {
                xVar[i] = xVar[i] + 0.1;
                yVar[i] = Math.Sin(xVar[i] * xVar[i] * parameters[0] + parameters[1]) + Math.Log(xVar[i] * xVar[i] * parameters[2] + parameters[3]);
            }

            double[][] result = new double[2][];
            result[0] = xVar;
            result[1] = yVar;
            return result;
        }

        public double myObjFunction_A(double[] parameters)
        {
            double error = double.MaxValue;

            double[][] Evaluation = myFuction_A(parameters);

            double[] LocalError = new double[target_A.Length];
            for (int i = 0; i < target_A.Length; i++)
            {
                LocalError[i] = Math.Abs(Evaluation[1][i] - target_A[1][i]);
            }

            error = LocalError.Sum();
            return error;
        }

        public void RunSolution()
        {
            List<GeneticAlgorithm.Range> FeactureRange = new List<GeneticAlgorithm.Range>();
            FeactureRange.Clear();
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 10 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 20 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.1, MaxValue = 0.5 });
            FeactureRange.Add(new GeneticAlgorithm.Range { MinValue = 0.001, MaxValue = 0.1 });

            GeneticAlgorithm GA_1 = new GeneticAlgorithm(100, FeactureRange, 0.9, 0.01, new GeneticAlgorithm.Range { MinValue = 0, MaxValue = 10 });
            target_A = myFuction_A(new double[] {15,12,0.35,0.05});
            GA_1.Solve(myObjFunction_A);
        }
    }
}
