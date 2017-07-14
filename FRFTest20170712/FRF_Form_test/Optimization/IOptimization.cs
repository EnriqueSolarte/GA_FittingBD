using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    public interface IOptimization
    {           
        double[] Solve(ObjectiveFunction ObjFun);

        Result OPTResult { get; set; }

        List<Result> OPTHistoryResult { get; set; }
    }

    public delegate double ObjectiveFunction(double[] parameters);

    public struct Result
    {
        public double[] Parameters;
        public double[] Responses;
        public double OBJ;
    }

   


}
