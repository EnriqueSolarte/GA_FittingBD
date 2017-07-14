using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimization
{
    
    public class GA : IOPTSolver
    {

        List<HistoryItem> optHistory;
        public List<HistoryItem> OPTHistory
        {
            get
            {
                return optHistory;
                //throw new NotImplementedException();
            }
            set
            {
                optHistory = value;
                //throw new NotImplementedException();
            }
        }

        ObjectiveFunction objf;
        public ObjectiveFunction ObjFunc
        {
            get
            {
                return objf;
                //throw new NotImplementedException();
            }
            set
            {
                objf = value;
                //throw new NotImplementedException();
            }
        }

        Response[] responses;
        public Response[] Responses
        {
            get
            {
                return responses;
                //throw new NotImplementedException();
            }
            set
            {
                responses = value;
                throw new NotImplementedException();
            }
        }


        public double[] Solve()
        {
            throw new NotImplementedException();
        }

        Variable[] variables;
        Variable[] Variables
        {
            get
            {
                return variables;
                //throw new NotImplementedException();
            }
            set
            {
                variables = value;
                //throw new NotImplementedException();
            }
        }

        Variable[] IOPTSolver.Variables
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

        GA_Setting Settings = new GA_Setting();

   
        
        public GA(ObjectiveFunction parOBJ)
        {

        }
    }

    public class GA_Setting
    {
        int population = 1000;

        public int Population
        {
            get { return population; }
            set { population = value; }
        }

        double convergence = 0.001;

        public double Convergence
        {
            get { return convergence; }
            set { convergence = value; }
        }
    }



}
