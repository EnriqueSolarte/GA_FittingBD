using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Optimization
{
    public struct PSO_Setting
    {//setting parameters
        double convergence;// = 0.001;
        public double Convergence
        {
            get { return convergence; }
            set { convergence = value; }
        }

        public void aaa()
        { }
    }

    public class PSO : IOPTSolver
    {
        public PSO_Setting Settings = new PSO_Setting();

        public PSO(ObjectiveFunction _objf,Response _res,Variable _var)
        {
            this.objf = _objf;
            this.response = _res;
            this.variables = _var;

            Settings.aaa();
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

        Response response;
        public Response Responses
        {
            get
            {
                return response;
                //throw new NotImplementedException();
            }
            set
            {
                response = value;
                //throw new NotImplementedException();
            }
        }

        Variable variables;
        public Variable Variables
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

        public double[] Solve()
        {
            double aaa = variables.Variables[0].MaxValue;
            //......


            return new double[1];
            //throw new NotImplementedException();
        }


        History history;
        public History OPTHistory
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
    }

    
}
