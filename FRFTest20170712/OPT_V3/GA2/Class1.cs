using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    public class myAlgorithm : IOPTSolver
    {
        public ObjectiveFunction ObjFunc
        {
            get
            {
                return ObjFunc;
            }

            set
            {
                ObjFunc = value;
            }
        }

        public List<HistoryItem> OPTHistory
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Response[] Responses
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Variable[] Variables
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double[] Solve()
        {
            throw new NotImplementedException();
        }

        public myAlgorithm(ObjectiveFunction obFunction)
        {

        }
    }


    
}
