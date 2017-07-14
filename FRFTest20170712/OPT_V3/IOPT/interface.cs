using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    public interface IOPTSolver
    {
        ObjectiveFunction ObjFunc { get; set; }

        /// <summary>
        /// 1st value in Responses array is the minimized objective!
        /// </summary>
        Response[] Responses { get; set; }

        Variable[] Variables { get; set; }

        /// <summary>
        /// return Optimal values of each variable
        /// </summary>
        /// <returns></returns>
        double[] Solve();
        
        /// <summary>
        /// record Iteration data during solving process.
        /// </summary>
        List<HistoryItem> OPTHistory { get; set; }

    }

    /// <summary>
    /// return Responses array.
    /// 1st value in Responses array is the minimized objective!
    /// </summary>
    /// <param name="var"></param>
    /// <returns></returns>
    public delegate double[] ObjectiveFunction(double[] var);

    public struct Variable
    {
        public double InitialValue;
        public Constraint Constraints;
    }


    public class Constraint
    {
        public double Max = double.MaxValue;
        public double Min = double.MinValue;
     
    }
    
    public struct Response
    {
        public Constraint Constraints;
    }

    public struct HistoryItem
    {
        public double[] var;
        public double[] response;
        public double OBJ;
    }
}
