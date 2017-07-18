using System.Collections.Generic;
using System.Windows.Forms;

using Mechatronics.MathToolBox;
using Mechatronics.MSMObject;
using Mechatronics.Analysis;
using Optimization;
using System;
using System.Threading.Tasks;

namespace FRF_Form_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        
            //Function object
            VelocityResponse VR = new VelocityResponse();
            
            //Set experiment Control Parameters (C controller)
            Parameters P = new Parameters();
            SetParameters(P);
            VR.P = P;

            //Defining target, read FRF file from csv file, which is created by servoguide
            FRF[] VClose_ref = VR.ReadServoGuide_FRFdata_csv("Frequency_Response_Axis-1_1_-_1000Hz.csv");
            VR.IsFreqDataSameAsRef = true;

            List<Mode> VLoopModes = new List<Mode>();
            CreateModes(VLoopModes);

            #region Defining Optimation

            #region Setting Data

            GeneticAlgorithm.Range Mass_range = new GeneticAlgorithm.Range { MinValue = 0.0001, MaxValue = 1 };
            GeneticAlgorithm.Range Zeta_range = new GeneticAlgorithm.Range { MinValue = 0.01, MaxValue = 0.1 };

            List<GeneticAlgorithm.Range> FrequencyRange = new List<GeneticAlgorithm.Range>();
            FrequencyRange.Add(new GeneticAlgorithm.Range { MinValue = 50, MaxValue = 80 });
            FrequencyRange.Add(new GeneticAlgorithm.Range { MinValue = 110, MaxValue = 250 });
            FrequencyRange.Add(new GeneticAlgorithm.Range { MinValue = 300, MaxValue = 400 });
            #endregion
            
            FittingOptimization FittingOPT = new FittingOptimization(Mass_range, Zeta_range,FrequencyRange);
            
            FittingOPT.SetOptimizationParameters(100,0.9,0.9,100, 1);
          
            FittingOPT.SetReference(VClose_ref, VR, VLoopModes);

            VLoopModes = new List<Mode>();
            VLoopModes = FittingOPT.Solve();


            #endregion

            //Create Structure Nature Modes Object
           
            VR.VLoopModes = VLoopModes;

            //Evaluation
            FRF[] Close = VR.SolveCloseLoopResponse();
            //FRF[] Open = VR.SolveOpenLoopResponse();

            // draw referance FRF in chart
            DrawLine(VClose_ref, 0);
            // draw simulated FRF in chart
            DrawLine(Close, 1);

        }

        void CreateModes(List<Mode> VLoopModes)
        {

            Mode mode;

            mode = new Mode();
            mode.Freq = 55;
            mode.Zeta = 0.1;
            mode.Mass = 0.3;
            VLoopModes.Add(mode);
            

            mode = new Mode();
            mode.Freq = 120;
            mode.Zeta = 0.07;
            mode.Mass = 0.1;
            VLoopModes.Add(mode);

            mode = new Mode();
            mode.Freq = 315;
            mode.Zeta = 0.1;
            mode.Mass = 0.05;
            VLoopModes.Add(mode);


        }

        #region Fixed Parameters
        void SetParameters(Parameters P)
        {//value is from *.prm file (ServoGuide Parameter file)
            P.FANUCs.HRVGain = 300;
            P.FANUCs.VelocityLoopGain = 100;
            P.FANUCs.No2043_KVI_Standard = 198;
            P.FANUCs.No2044_KVP_Standard = -1775;
            P.FANUCs.No2043_KVI_Setting = 198;
            P.FANUCs.No2044_KVP_Setting = -1775;


            P.Jm = 0.012;
            P.Kt = 1.2;
            P.dTzoh = 0.001m;
            P.Tc = 0;
            P.JL = 0.00546;
            P.IsHighCycle = true;
            P.IsFullCloseLoop = true;

            P.ConvertFUNUCParamters();

        }
        void DrawLine(FRF[] FRFData, int Channel)
        {
            //X AXIS in log scale
            chart_mag.ChartAreas[0].AxisX.IsLogarithmic = true;
            chart_phs.ChartAreas[0].AxisX.IsLogarithmic = true;

            //reset
            chart_mag.Series[Channel].Points.Clear();
            chart_phs.Series[Channel].Points.Clear();

            for (int i = 0; i < FRFData.Length; i++)
            {
                chart_mag.Series[Channel].Points.AddXY(FRFData[i].Freq, Mechatronics.MathToolBox.TL.DB(FRFData[i].Mag));
                chart_phs.Series[Channel].Points.AddXY(FRFData[i].Freq, FRFData[i].Phs);

            }

        }
        #endregion

        private class FittingOptimization
        {
            public List<GeneticAlgorithm.Range> FrequencyRange { get; set; }
            public List<GeneticAlgorithm> GA { get; set; }
            public VelocityResponse VR { get; set; }
            public FRF[] VClose_ref { get; set; }
            public double Sensibility { get;  set; }

            private GeneticAlgorithm.Range Mass_range;
            private GeneticAlgorithm.Range Zeta_range;          
            private List<GeneticAlgorithm.Range> Features; // internal Varaiable to GA
            private List<FRF[]> Target;

            private List<Mode> InitialModes;
            private int RangeStatus;
       

            public FittingOptimization(GeneticAlgorithm.Range mass_range, GeneticAlgorithm.Range zeta_range, List<GeneticAlgorithm.Range> frequencyRange)
            {
                Mass_range = mass_range;
                Zeta_range = zeta_range;
                FrequencyRange = frequencyRange;

            } 

            public void SetOptimizationParameters(int population, double pcross, double pmutation, int generations, double sensibility)
            {
                GA = new List<GeneticAlgorithm>();

                Sensibility = sensibility;
                for (int i=0; i < FrequencyRange.Count; i++)
                {
                    GeneticAlgorithm _GA;
                    Features = new List<GeneticAlgorithm.Range>();
                    Features.Add(Mass_range);
                    Features.Add(Zeta_range);
                    Features.Add(FrequencyRange[i]);
                    _GA = new GeneticAlgorithm(population, Features, pcross, pmutation, generations);
                    GA.Add(_GA);
                }

               
               

            }

            public List<Mode> Solve()
            {
                SetReference(VClose_ref, VR, InitialModes);
                List<Mode> result = new List<Mode>();

                Parallel.For(0, FrequencyRange.Count, i =>
                {
                    RangeStatus = i;
                    GA[i].Solve(ObjFunction);
                    double[] BestParameters = GA[i].OPTResult.Parameters;
                    Mode _Mode = new Mode();
                    _Mode.Freq = BestParameters[2];
                    _Mode.Mass = BestParameters[0];
                    _Mode.Zeta = BestParameters[1];
                    result.Add(_Mode);
                });
               
                return result;
            }

            private double ObjFunction(double[] parameters)
            {
                
                List<Mode> VLoopModes = new List<Mode>();

                Mode mode = new Mode();
                mode.Freq = parameters[2];
                //It shoud be in this order
                mode.Mass = parameters[0];
                mode.Zeta = parameters[1];

                VLoopModes = InitialModes;  
                VLoopModes[RangeStatus] = mode;
                
                VR.VLoopModes = VLoopModes;
                FRF[] Eval = VR.SolveCloseLoopResponse();
                List<FRF[]> RegionEval = GettingRegionReference(Eval);


                double Error = 0;
                double LocalError = 0;
                 for (int i = 0; i < RegionEval[RangeStatus].Length; i++)
                {
                    LocalError = Math.Abs(Target[RangeStatus][i].Mag - RegionEval[RangeStatus][i].Mag)+LocalError;
                }
                Error = Error + LocalError;
                
                return Sensibility/Error;
            }

            public void SetReference(FRF[] vClose_ref, VelocityResponse vR, List<Mode> initialModes )
            {
                VClose_ref = vClose_ref;
                VR = vR;
                Target = new List<FRF[]>();
                Target = GettingRegionReference(vClose_ref);

                InitialModes = initialModes;
                               
            }

            private List<FRF[]> GettingRegionReference(FRF[] vClose_ref)
            {
                List<FRF[]> _target = new List<FRF[]>();
                for (int i = 0; i < FrequencyRange.Count; i++)
                {
                    List<FRF> aux = new List<FRF>();
                   
                    for (int data = 0; data < vClose_ref.Length; data++)
                    {
                        if (vClose_ref[data].Freq >= FrequencyRange[i].MinValue && vClose_ref[data].Freq <= FrequencyRange[i].MaxValue)
                        {
                            FRF target = new FRF();
                            target = vClose_ref[data];
                            aux.Add(target);
                        }
                    }
                    _target.Add((FRF[])aux.ToArray().Clone());
                }

                return _target;
            }
        }
    }


    


}
