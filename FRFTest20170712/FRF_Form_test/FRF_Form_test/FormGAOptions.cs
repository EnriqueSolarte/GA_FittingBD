using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mechatronics.Analysis;
using Mechatronics.MathToolBox;
using Optimization;
using System.Windows.Forms.DataVisualization.Charting;

namespace FRF_Form_test
{
    public partial class FormGAOptions : Form
    {
        private FormMain.FittingOptimization FittingOPT;
        private FRF[] VClose_ref;
        private VelocityResponse VR;

        public FormGAOptions(FRF[] vClose_ref, VelocityResponse vR, FormMain.FittingOptimization fittingOPT, List<FormMain.InitialFrequencyPoints> initialValues)
        {
            InitializeComponent();
            VClose_ref = vClose_ref;
            VR = vR;
            FittingOPT = fittingOPT;
            FormMain.DrawLine(VClose_ref, 0, chart_mag,chart_phs);

            PlottingGAData();

            textBoxFreq_1.Text = initialValues[0].ResonantFreqPeak.ToString();
            textBoxMag_1.Text = initialValues[0].ResonantMagPeak.ToString();
            textBoxFreqMax_1.Text = initialValues[0].FrequencyRage.MaxValue.ToString();
            textBoxFreqMin_1.Text = initialValues[0].FrequencyRage.MinValue.ToString();

            textBoxFreq_2.Text = initialValues[1].ResonantFreqPeak.ToString();
            textBoxMag_2.Text = initialValues[1].ResonantMagPeak.ToString();
            textBoxFreqMax_2.Text = initialValues[1].FrequencyRage.MaxValue.ToString();
            textBoxFreqMin_2.Text = initialValues[1].FrequencyRage.MinValue.ToString();

            textBoxFreq_3.Text = initialValues[2].ResonantFreqPeak.ToString();
            textBoxMag_3.Text = initialValues[2].ResonantMagPeak.ToString();
            textBoxFreqMax_3.Text = initialValues[2].FrequencyRage.MaxValue.ToString();
            textBoxFreqMin_3.Text = initialValues[2].FrequencyRage.MinValue.ToString();

            textBoxPCross.Text = FittingOPT.GA[0].GA_Settings.PCrossover.ToString();
            textBoxPMutation.Text = FittingOPT.GA[0].GA_Settings.PMutation.ToString();
            textBoxPop.Text = FittingOPT.GA[0].GA_Settings.PopulationSize.ToString();
            textBoxGen.Text = FittingOPT.GA[0].GA_Settings.Generations.ToString();
        }

        private void PlottingGAData()
        {
            FormMain.DrawLine(VR.SolveCloseLoopResponse(), 1, chart_mag, chart_phs);

            if (radioButtonGA1.Checked)
            {
                DrawLine(FittingOPT.GA[0].GetMaxFitnessHistory(),0,chartFitness);
                DrawLine(FittingOPT.GA[0].GetMeanFitnessHistory(), 1, chartFitness);

                if (radioButtonMass.Checked) {DrawLine(FittingOPT.GA[0].GetFeatureHistory(0), 0, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonZeta.Checked) { DrawLine(FittingOPT.GA[0].GetFeatureHistory(1), 1, chartParameters); chartParameters.Series[0].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonFreq.Checked) { DrawLine(FittingOPT.GA[0].GetFeatureHistory(2), 2, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[0].Points.Clear(); }
                

            }
            if (radioButtonGA2.Checked)
            {
                DrawLine(FittingOPT.GA[1].GetMaxFitnessHistory(), 0, chartFitness);
                DrawLine(FittingOPT.GA[1].GetMeanFitnessHistory(), 1, chartFitness);

                if (radioButtonMass.Checked) { DrawLine(FittingOPT.GA[1].GetFeatureHistory(0), 0, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonZeta.Checked) { DrawLine(FittingOPT.GA[1].GetFeatureHistory(1), 1, chartParameters); chartParameters.Series[0].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonFreq.Checked) { DrawLine(FittingOPT.GA[1].GetFeatureHistory(2), 2, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[0].Points.Clear(); }

            }
            if (radioButtonGA3.Checked)
            {
                DrawLine(FittingOPT.GA[2].GetMaxFitnessHistory(), 0, chartFitness);
                DrawLine(FittingOPT.GA[2].GetMeanFitnessHistory(), 1, chartFitness);

                if (radioButtonMass.Checked) { DrawLine(FittingOPT.GA[2].GetFeatureHistory(0), 0, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonZeta.Checked) { DrawLine(FittingOPT.GA[2].GetFeatureHistory(1), 1, chartParameters); chartParameters.Series[0].Points.Clear(); chartParameters.Series[2].Points.Clear(); }
                if (radioButtonFreq.Checked) { DrawLine(FittingOPT.GA[2].GetFeatureHistory(2), 2, chartParameters); chartParameters.Series[1].Points.Clear(); chartParameters.Series[0].Points.Clear(); }

            }


        }

        private void chart_mag_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint myPoint = (DataPoint)(e.HitTestResult.Object);
                e.Text = "X value: " + myPoint.XValue + Environment.NewLine;
                e.Text += "Y value: " + myPoint.YValues[0] + Environment.NewLine;
            }
        }

        private void buttonRunGA_Click(object sender, EventArgs e)
        {
            buttonRunGA.Enabled = false;
            List<FormMain.InitialFrequencyPoints> InitialValues = GetInitialValues();
            GeneticAlgorithmOptimization(InitialValues);
            buttonRunGA.Enabled = true;
            PlottingGAData();

            textBoxFeatureResults.Text = "GA1 :" + FittingOPT.GA[0].OPTResult.Parameters[0].ToString("0.00") + " - " + FittingOPT.GA[0].OPTResult.Parameters[1].ToString("0.00") + " -  " + FittingOPT.GA[0].OPTResult.Parameters[2].ToString("0.00") +
                                         "\t GA2 :" + FittingOPT.GA[1].OPTResult.Parameters[0].ToString("0.00") + " - " + FittingOPT.GA[1].OPTResult.Parameters[1].ToString("0.00") + " -  " + FittingOPT.GA[1].OPTResult.Parameters[2].ToString("0.00") +
                                         "\t GA3 :" + FittingOPT.GA[2].OPTResult.Parameters[0].ToString("0.00") + " - " + FittingOPT.GA[2].OPTResult.Parameters[1].ToString("0.00") + " -  " + FittingOPT.GA[2].OPTResult.Parameters[2].ToString("0.00");

        }
            private void GeneticAlgorithmOptimization(List<FormMain.InitialFrequencyPoints> ResonantPeak)
        {
            List<Mode> VLoopModes = new List<Mode>();
            FormMain.CreateModes(VLoopModes);

            GeneticAlgorithm.Range Mass_range = new GeneticAlgorithm.Range { MinValue = FormMain.TryToDouble(textBoxMassMin.Text), MaxValue = FormMain.TryToDouble(textBoxMassMax.Text) };
            GeneticAlgorithm.Range Zeta_range = new GeneticAlgorithm.Range { MinValue = FormMain.TryToDouble(textBoxZetaMin.Text), MaxValue = FormMain.TryToDouble(textBoxZetaMax.Text) };

            List<GeneticAlgorithm.Range> FrequencyRange = new List<GeneticAlgorithm.Range>();
            for (int i = 0; i < ResonantPeak.Count; i++)
            {
                FrequencyRange.Add(ResonantPeak[i].FrequencyRage);
            }

            FittingOPT = new FormMain.FittingOptimization(Mass_range, Zeta_range, FrequencyRange);

            FittingOPT.SetOptimizationParameters((int)FormMain.TryToDouble(textBoxPop.Text), FormMain.TryToDouble(textBoxPCross.Text), FormMain.TryToDouble(textBoxPMutation.Text), (int)FormMain.TryToDouble(textBoxGen.Text), 500);

            FittingOPT.SetReference(VClose_ref, VR, VLoopModes);

            VLoopModes = new List<Mode>();
            VLoopModes = FittingOPT.Solve();

            //Create Structure Nature Modes Object

            VR.VLoopModes = VLoopModes;

            //Evaluation
            FRF[] Close = VR.SolveCloseLoopResponse();
            //FRF[] Open = VR.SolveOpenLoopResponse();

            // draw simulated FRF in chart
            FormMain.DrawLine(Close, 1,chart_mag, chart_phs);
        }

        private List<FormMain.InitialFrequencyPoints> GetInitialValues()
        {
            List<FormMain.InitialFrequencyPoints> _InitialValues = new List<FormMain.InitialFrequencyPoints>();

            FormMain.InitialFrequencyPoints ResonantPeakPoint = new FormMain.InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = FormMain.TryToDouble(textBoxMag_1.Text);
            ResonantPeakPoint.ResonantFreqPeak = FormMain.TryToDouble(textBoxFreq_1.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = FormMain.TryToDouble(textBoxFreqMin_1.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = FormMain.TryToDouble(textBoxFreqMax_1.Text);
            _InitialValues.Add(ResonantPeakPoint);

            ResonantPeakPoint = new FormMain.InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = FormMain.TryToDouble(textBoxMag_2.Text);
            ResonantPeakPoint.ResonantFreqPeak = FormMain.TryToDouble(textBoxFreq_2.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = FormMain.TryToDouble(textBoxFreqMin_2.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = FormMain.TryToDouble(textBoxFreqMax_2.Text);
            _InitialValues.Add(ResonantPeakPoint);

            ResonantPeakPoint = new FormMain.InitialFrequencyPoints();
            ResonantPeakPoint.ResonantMagPeak = FormMain.TryToDouble(textBoxMag_3.Text);
            ResonantPeakPoint.ResonantFreqPeak = FormMain.TryToDouble(textBoxFreq_3.Text);
            ResonantPeakPoint.FrequencyRage.MinValue = FormMain.TryToDouble(textBoxFreqMin_3.Text);
            ResonantPeakPoint.FrequencyRage.MaxValue = FormMain.TryToDouble(textBoxFreqMax_3.Text);
            _InitialValues.Add(ResonantPeakPoint);

            return _InitialValues;
        }

        void DrawLine(double[] Data, int Channel, Chart chart)
        {

            //reset
            chart.Series[Channel].Points.Clear();

            for (int i = 0; i < Data.Length; i++)
            {
                chart.Series[Channel].Points.AddXY(i,Data[i]);
            }

        }


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            PlottingGAData();
        }

        private void buttonPlotGAData(object sender, EventArgs e)
        {
            PlottingGAData();
        }
    }
}
