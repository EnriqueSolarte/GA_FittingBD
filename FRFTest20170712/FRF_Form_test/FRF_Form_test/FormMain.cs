using System.Collections.Generic;
using System.Windows.Forms;

using Mechatronics.MathToolBox;
using Mechatronics.MSMObject;
using Mechatronics.Analysis;
using Optimization;
using System;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace FRF_Form_test
{
    public partial class FormMain : Form
    {
        FRF[] VClose_ref;
        VelocityResponse VR;
        FittingOptimization FittingOPT;

        public FormMain()
        {
            InitializeComponent();

            //Function object
            VR = new VelocityResponse();

            //Set experiment Control Parameters (C controller)
            Parameters P = new Parameters();
            SetParameters(P);
            VR.P = P;

            //Defining target, read FRF file from csv file, which is created by servoguide
            VClose_ref = VR.ReadServoGuide_FRFdata_csv("Frequency_Response_Axis-1_1_-_1000Hz.csv");
            VR.IsFreqDataSameAsRef = true;
            DrawLine(VClose_ref, 0,chart_mag,chart_phs);

        }

        #region Fixed Code

        public static void CreateModes(List<Mode> VLoopModes)
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
        internal static void DrawLine(FRF[] FRFData, int Channel, Chart Chart_mag, Chart Chart_phs)
        {
            //X AXIS in log scale
            Chart_mag.ChartAreas[0].AxisX.IsLogarithmic = true;
            Chart_phs.ChartAreas[0].AxisX.IsLogarithmic = true;

            //reset
            Chart_mag.Series[Channel].Points.Clear();
            Chart_phs.Series[Channel].Points.Clear();

            for (int i = 0; i < FRFData.Length; i++)
            {
                Chart_mag.Series[Channel].Points.AddXY(FRFData[i].Freq, Mechatronics.MathToolBox.TL.DB(FRFData[i].Mag));
                Chart_phs.Series[Channel].Points.AddXY(FRFData[i].Freq, FRFData[i].Phs);
            }
        }

        private void InformationTipEvent(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint myPoint = (DataPoint)(e.HitTestResult.Object);
                e.Text = "X value: " + myPoint.XValue + Environment.NewLine;
                e.Text += "Y value: " + myPoint.YValues[0] + Environment.NewLine;
            }
        }

        #endregion

        #region Fitting OPT
        public class FittingOptimization
        {     
            
            public List<GeneticAlgorithm.Range> FrequencyRange { get; set; }
            public List<GeneticAlgorithm> GA { get; set; }
            public VelocityResponse VR { get; set; }
            public FRF[] VClose_ref { get; set; }
            public double Sensibility { get; set; }


            public GeneticAlgorithm.Range Mass_range { get; set; }
            public GeneticAlgorithm.Range Zeta_range { get; set; }
            private List<GeneticAlgorithm.Range> Features;// internal Varaiable to GA

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
                for (int i = 0; i < FrequencyRange.Count; i++)
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
                    LocalError = Math.Abs(Target[RangeStatus][i].Mag - RegionEval[RangeStatus][i].Mag) + LocalError;
                }
                Error = Error + LocalError;

                return Sensibility / Error;
            }

            public void SetReference(FRF[] vClose_ref, VelocityResponse vR, List<Mode> initialModes)
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

        #endregion

        #region MyRegion
        public class InitialFrequencyPoints
        {
            public double ResonantMagPeak;
            public double ResonantFreqPeak;
            public GeneticAlgorithm.Range FrequencyRage;

        }

        public static double TryToDouble(string stringValue)
        {
            double auxOut;
            double.TryParse(stringValue, out auxOut);
            return auxOut;
        }

        #endregion

        public void GeneticAlgorithmOptimization(List<InitialFrequencyPoints> ResonantPeak)
        {
            List<Mode> VLoopModes = new List<Mode>();
            CreateModes(VLoopModes);

            GeneticAlgorithm.Range Mass_range = new GeneticAlgorithm.Range { MinValue = 0.001, MaxValue = 1 };
            GeneticAlgorithm.Range Zeta_range = new GeneticAlgorithm.Range { MinValue = 0.01, MaxValue = 0.5 };

            List<GeneticAlgorithm.Range> FrequencyRange = new List<GeneticAlgorithm.Range>();
            for(int i=0; i < ResonantPeak.Count; i++)
            {
                FrequencyRange.Add(ResonantPeak[i].FrequencyRage);
            }

            FittingOPT = new FittingOptimization(Mass_range, Zeta_range, FrequencyRange);

            FittingOPT.SetOptimizationParameters(100, 1, 0.9, 50, 500);

            FittingOPT.SetReference(VClose_ref, VR, VLoopModes);

            VLoopModes = new List<Mode>();
            VLoopModes = FittingOPT.Solve();

            //Create Structure Nature Modes Object

            VR.VLoopModes = VLoopModes;

            //Evaluation
            FRF[] Close = VR.SolveCloseLoopResponse();
            //FRF[] Open = VR.SolveOpenLoopResponse();

            // draw simulated FRF in chart
            DrawLine(Close, 1);
        }

        private void FittingCurveBtnEvent(object sender, EventArgs e)
        {
            buttonCreateFiittingCurve.Enabled = false;
            
            List<InitialFrequencyPoints> InitialValues = GetInitialValues();
            GeneticAlgorithmOptimization(InitialValues);          
            buttonCreateFiittingCurve.Enabled = true;
            buttonOptimizationOptions.Enabled = true;
        }

        private List<InitialFrequencyPoints> GetInitialValues()
        {
            List<InitialFrequencyPoints> _InitialValues = new List<InitialFrequencyPoints>();

            InitialFrequencyPoints ResonantPeakPoint = new InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = TryToDouble(textBoxMag_1.Text);
            ResonantPeakPoint.ResonantFreqPeak = TryToDouble(textBoxFreq_1.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = TryToDouble(textBoxFreqMin_1.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = TryToDouble(textBoxFreqMax_1.Text);
            _InitialValues.Add(ResonantPeakPoint);

            ResonantPeakPoint = new InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = TryToDouble(textBoxMag_2.Text);
            ResonantPeakPoint.ResonantFreqPeak = TryToDouble(textBoxFreq_2.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = TryToDouble(textBoxFreqMin_2.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = TryToDouble(textBoxFreqMax_2.Text);
            _InitialValues.Add(ResonantPeakPoint);

            ResonantPeakPoint = new InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = TryToDouble(textBoxMag_3.Text);
            ResonantPeakPoint.ResonantFreqPeak = TryToDouble(textBoxFreq_3.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = TryToDouble(textBoxFreqMin_3.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = TryToDouble(textBoxFreqMax_3.Text);
            _InitialValues.Add(ResonantPeakPoint);

            return _InitialValues;
        }
       
        private void GeneticAlgorithmOptionsBntEvent(object sender, EventArgs e)
        {
            List<InitialFrequencyPoints> InitialValues = GetInitialValues();
            FormGAOptions form = new FormGAOptions(VClose_ref, VR, FittingOPT, InitialValues);

            form.Show();
        }
    }





}